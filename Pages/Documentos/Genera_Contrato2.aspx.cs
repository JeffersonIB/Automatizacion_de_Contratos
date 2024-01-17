using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Humanizer;
using System.Data.SqlClient;
using System.Data;

namespace IT_Contratos.Pages.Documentos
{
    public partial class Genera_Contrato2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null && CheckBoxListEmpleados.Items.Count == 0)
                {
                    DDLCargarRepresentante();
                    CargarEmpleados();
                    CheckBoxListEmpleados.DataBind();
                    DDLHorarios();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void DDLCargarRepresentante()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_RCH00701", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                DDLRepresentante.DataSource = cmd.ExecuteReader();
                DDLRepresentante.DataTextField = "Representante";
                DDLRepresentante.DataValueField = "Representante";
                DDLRepresentante.DataBind();
                con.Close();
                //DDLRepresentante.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void DDLRepresentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí podrías realizar acciones adicionales cuando se seleccione un representante en el DropDownList.
        }
        private void CargarEmpleados()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RCH00701", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = IdEmpresa;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> empleados = new List<Employee>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //int idEmpleado = reader.GetInt32(0);
                        string idEmpleado = reader.GetString(1);
                        string nombreEmpleado = reader.GetString(1);
                        empleados.Add(new Employee { Id_Empleado = idEmpleado, Nombre_Completo = nombreEmpleado });
                    }
                }
                reader.Close();
                CheckBoxListEmpleados.DataSource = empleados;
                CheckBoxListEmpleados.DataTextField = "Nombre_Completo";
                CheckBoxListEmpleados.DataValueField = "Id_Empleado";
                CheckBoxListEmpleados.DataBind();
                // Generar el código JavaScript para cargar los nombres de empleados en el cliente
                List<string> empleadosNombres = new List<string>();
                foreach (Employee empleado in empleados)
                {
                    empleadosNombres.Add(empleado.Nombre_Completo);
                }
                string empleadosNombresJson = string.Join(",", empleadosNombres.Select(name => "\"" + name + "\""));
                Page.ClientScript.RegisterStartupScript(GetType(), "LoadEmployees", $"var empleados = [{empleadosNombresJson}];", true);
            }
        }
        public class Employee
        {
            public string Id_Empleado { get; set; }
            public string Nombre_Completo { get; set; }
        }
        protected void DDLHorarios()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_RCH00702", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                DDLHorario.DataSource = cmd.ExecuteReader();
                DDLHorario.DataTextField = "Horario";
                DDLHorario.DataValueField = "Id_Horario";
                DDLHorario.DataBind();
                con.Close();
                //DDLHorario.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void DDLHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí podrías realizar acciones adicionales cuando se seleccione un representante en el DropDownList.
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //string Sociedad = DDLSociedad.SelectedValue;
            string Representante = DDLRepresentante.SelectedValue;              // Representante legal seleccionado desde DropDownList
            string selectedEmployeeId = CheckBoxListEmpleados.SelectedValue;    // Empleado seleccionado desde CheckBoxList
            string fecharelacion = frelacion.Value;                             // Fecha incio de relación
            string fechacontrato = fcontrato.Value;                             // Fecha en la que se firma el contrato
            string fileName = NombreDocumento.Text;                             // Nombre del archivo
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // Información de empleado
                SqlCommand employeeCmd = new SqlCommand("SELECT Nombre_Completo, Edad, Txt_Edad, Sexo, Estado_Civil, Direccion, Sueldo,Txt_Sueldo,Entero,Txt_Entero,Decimal,Txt_Decimal, Proceso, Puesto, Descriptor, DPI, Primeros, Txt_Primeros, Centro, Txt_Centro, Ultimos, Txt_Ultimos, Profecion_Oficio FROM VW_RCH00701_7 WHERE  ' ' + UPPER(RTRIM(Id_Empleado)+' - '+RTRIM(Nombre_Completo)) = @Id_Empleado ", con);
                employeeCmd.Parameters.AddWithValue("@Id_Empleado", System.Data.SqlDbType.VarChar).Value = selectedEmployeeId;
                SqlDataReader employeeReader = employeeCmd.ExecuteReader();

                if (employeeReader.Read())
                {
                    string nombre_empleado = employeeReader["Nombre_Completo"].ToString();  // Nombre empleado
                    string edad = employeeReader["Edad"].ToString();                        // Edad empleado
                    string txtedad = employeeReader["Txt_Edad"].ToString();                 // Txt Edad empleado
                    string sexo = employeeReader["Sexo"].ToString();                        // Sexo empleado
                    string estadocivil = employeeReader["Estado_Civil"].ToString();         // Estado Civil empleado
                    string direccion = employeeReader["Direccion"].ToString();              // Dirección empleado                    
                    //string sueldo = employeeReader["Sueldo"].ToString();                    // Sueldo empleado
                    decimal sueldoDecimal = (decimal)employeeReader["Sueldo"];
                    string sueldoFormateado = sueldoDecimal.ToString("N2");
                    string txtsueldo = employeeReader["Txt_Sueldo"].ToString();             // Txt_Sueldo empleado
                    string entero = employeeReader["Entero"].ToString();                    // Entero empleado
                    string txtentero = employeeReader["Txt_Entero"].ToString();             // Txt_Entero empleado
                    string decimals = employeeReader["Decimal"].ToString();                 // Decimal empleado
                    string txtdecimal = employeeReader["Txt_Decimal"].ToString();           // Txt_Decimal empleado
                    string proceso = employeeReader["Proceso"].ToString();                  // Proceso al que petenece
                    string puesto = employeeReader["Puesto"].ToString();                    // Puesto
                    string descriptor = employeeReader["Descriptor"].ToString();            // Descriptor de puesto
                    string dpi = employeeReader["DPI"].ToString();                          // DPI empleado
                    string primeros = employeeReader["Primeros"].ToString();                // DPI empleado
                    string txtprimeros = employeeReader["Txt_Primeros"].ToString();         // DPI empleado
                    string centro = employeeReader["Centro"].ToString();                    // DPI empleado
                    string txtcentro = employeeReader["Txt_Centro"].ToString();             // DPI empleado
                    string ultimos = employeeReader["Ultimos"].ToString();                  // DPI empleado
                    string txtultimos = employeeReader["Txt_Ultimos"].ToString();           // DPI empleado
                    string profesion = employeeReader["Profecion_Oficio"].ToString();       // Profesión u oficio
                    employeeReader.Close();
                    // Información de representante legal
                    SqlCommand representativeCmd = new SqlCommand("SELECT Representante, Edad, Txt_Edad, Info1, Cargo, Entidad, Sede, Direccion FROM VW_RCH00701_0 WHERE Representante = @Representante", con);
                    representativeCmd.Parameters.AddWithValue("@Representante", System.Data.SqlDbType.VarChar).Value = Representante;
                    SqlDataReader representativeReader = representativeCmd.ExecuteReader();

                    if (representativeReader.Read())
                    {
                        string representante = representativeReader["Representante"].ToString();// Nombre Representante
                        string edad_numeros = representativeReader["Edad"].ToString();          // Edad_Numeros
                        string edad_letras = representativeReader["Txt_Edad"].ToString();       // Edad_Letras
                        string info_1 = representativeReader["Info1"].ToString();               // Información
                        string cargo = representativeReader["Cargo"].ToString();                // Cargo
                        string entidad = representativeReader["Entidad"].ToString();            // Entidad
                        string sede = representativeReader["Sede"].ToString();                  // Dirección legal de la empresa
                        string direcciont = representativeReader["Direccion"].ToString();       // Dirección  donde trabajará
                        representativeReader.Close();
                        // Horarios
                        SqlCommand horariosCmd = new SqlCommand("SELECT Descripcion FROM RCH00702 WHERE Id_Horario = @Id_Horario", con);
                        horariosCmd.Parameters.Add("@Id_Horario", System.Data.SqlDbType.Int).Value = DDLHorario.Text;
                        SqlDataReader horariosReader = horariosCmd.ExecuteReader();
                        if (horariosReader.Read())
                        {
                            string descripcionhorario = horariosReader["Descripcion"].ToString();// Descripcion de horarios
                            horariosReader.Close();

                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
                                {
                                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                                    mainPart.Document = new Document();
                                    Body body = mainPart.Document.AppendChild(new Body());

                                    // Add paragraphs and text using Open XML SDK
                                    Paragraph titleParagraph = new Paragraph(new Run(new Text("CONTRATO INDIVIDUAL DE TRABAJO SUSCRITO ENTRE")));
                                    Paragraph subTitleParagraph = new Paragraph(new Run(new Text("EXPORTADORA ENLASA, SOCIEDAD ANONIMA\nY EL TRABAJADOR " + " " + nombre_empleado + "\n")));
                                    body.Append(titleParagraph, subTitleParagraph);
                                    if (DateTime.TryParse(fecharelacion, out DateTime fechaSeleccionadarelacion))
                                    {
                                        int ayorelacion = fechaSeleccionadarelacion.Year;
                                        int mesrelacion = fechaSeleccionadarelacion.Month;
                                        int diarelacion = fechaSeleccionadarelacion.Day;
                                        string ayoletrasrelacion = ayorelacion.ToWords();
                                        string mesletrasrelacion = GetMonthName(mesrelacion);
                                        string dialetrasrelacion = diarelacion.ToWords();
                                        if (DateTime.TryParse(fechacontrato, out DateTime fechaSeleccionadacontrato))
                                        {
                                            int ayocontrato = fechaSeleccionadacontrato.Year;
                                            int mescontrato = fechaSeleccionadacontrato.Month;
                                            int diacontrato = fechaSeleccionadacontrato.Day;
                                            string ayoletrascontrato = ayorelacion.ToWords();
                                            string mesletrascontrato = GetMonthName(mescontrato);
                                            string dialetrascontarto = diacontrato.ToWords();
                                            // Add more paragraphs and text as needed
                                            string textoAdicional =
                                              "Nosotros: Por una parte, " + representante + " de " + edad_letras + " (" + edad_numeros + ")                                                                                                                                                                                                       " + info_1 + " " + cargo
                                            + " de la entidad " + entidad + sede + " " + nombre_empleado + " de " + txtedad + " (" + edad + ") " + " años, " + estadocivil + ", guatemalteco (a), "
                                            + profesion + ", sexo " + sexo
                                            + ", me identifico con el Documento Personal de Identificación (DPI) con el Código Único de Identificación (CUI) número "
                                            + txtprimeros + ", " + txtcentro + ", " + txtultimos + " (" + primeros + " " + centro + " " + ultimos + ") "
                                            + "extendido por el Registro Nacional de las Personas de la República de Guatemala, actuando en nombre propio, con dirección en "
                                            + direccion + " dirección que señalo para recibir notificaciones. En lo sucesivo y para los efectos de este contrato, nos denominaremos, "
                                            + "“EL PATRONO” y “EL (LA) TRABAJADOR(A)”, respectivamente. Ambos aseguramos ser de los datos de identificación personal consignados, "
                                            + "hallarnos en el libre ejercicio de nuestros derechos civiles y que la representación que se ejercita es amplia y suficiente de "
                                            + "conformidad con la ley y a nuestro juicio para la celebración de este instrumento, y por este acto celebramos el presente CONTRATO "
                                            + "INDIVIDUAL DE TRABAJO contenido en las cláusulas siguientes: PRIMERA: INICIO DE LA RELACIÓN LABORAL: La relación de trabajo dio inicio el día "
                                            + dialetrasrelacion + " (" + diarelacion + ") de " + mesletrasrelacion + " de " + ayoletrasrelacion + " (" + ayorelacion + "). "
                                            + "SEGUNDA: PLAZO DEL CONTRATO: La duración del presente contrato es por tiempo indefinido. TERCERA:"
                                            + "CARGO, SERVICIOS Y RESPONSABILIDAD DEL (LA) TRABAJADOR(A): El (la) trabajador(a) deberá desempeñar su trabajo con la eficiencia "
                                            + "y el esmero apropiados y en la forma, tiempo y lugar indicados por el patrono, ocupando el cargo de " + puesto
                                            + ", correspondiéndole, en consecuencia, prestar los servicios inherentes al mismo y que, de manera enunciativa y no limitativa, son los siguientes "
                                            + descriptor
                                            + " El (la) trabajador(a) cumplirá con las obligaciones que sean inherentes a su cargo, las establecidas en el Código de Trabajo, "
                                            + "el Reglamento Interior de Trabajo y demás disposiciones legales y reglamentarias de la materia, así como todos los servicios y funciones que le instruya"
                                            + " el patrono por escrito o verbalmente. El (la) trabajador(a) puede ser trasladado de un puesto a otro de la misma índole, rama o categoría, sin que esto"
                                            + " constituya despido por parte del patrono, siempre y cuando las condiciones de la prestación de servicios y necesidades de éste así lo requieran. Asimismo,"
                                            + " el patrono podrá asignar al trabajador(a) una nueva área de trabajo, siempre que esto se encuentre dentro de sus capacidades, en igualdad de condiciones "
                                            + "y dentro del giro del negocio que presta el patrono, sin que ello pueda ser considerado como un cambio de condiciones de trabajo, pues así se pacta"
                                            + " expresamente. CUARTA: LUGAR DE PRESTACIÓN DE LOS SERVICIOS: Los servicios serán prestados en " + direcciont
                                            + " o en cualquier lugar dentro de la República de Guatemala en que fuere asignado. QUINTA: JORNADA DE TRABAJO: La jornada ordinaria de trabajo se desarrollará en los siguientes turnos:"
                                            + descripcionhorario
                                            + " Asimismo, acuerdan las partes que los turnos de trabajo podrán ser rotativos, aceptando el (la) trabajador(a) laborar en cualquiera de ellos, "
                                            + "de conformidad con la programación que para el efecto el patrono elabore y que se podrán crear otros turnos según las necesidades de la empresa. "
                                            + "SEXTA: SALARIO ACTUAL, BONIFICACIÓN INCENTIVO Y FORMA DE PAGO: Ambas partes convienen expresamente que el salario ordinario actual será de "
                                            + txtentero + " con " + txtdecimal + " centavos" + " (Q." + sueldoFormateado + ")"
                                            + ", el cual será pagado mediante un anticipo el día quince de cada mes y el resto el último día laborable del mes y en moneda nacional; en el caso de que "
                                            + "el día de pago sea inhábil el pago se efectuará el día hábil anterior o posterior. Adicionalmente, el empleador pagará al trabajador(a), en concepto de "
                                            + "Bonificación Incentivo contenida en el Decreto 78-89 del Congreso de la República y sus reformas (Decretos 7-2000 y 37-2001 del Congreso de la República), "
                                            + "una suma no menor de doscientos cincuenta quetzales (Q.250.00) mensuales, en las mismas condiciones de forma y tiempo pactadas para el pago del salario ordinario."
                                            + " El patrono podrá determinar que el pago tenga lugar dentro del centro de trabajo o por medio de depósito directo en la cuenta bancaria del trabajador(a). "
                                            + "SÉPTIMA: DÍAS DE ASUETO Y SÉPTIMOS DÍAS: Los séptimos días y los días de asueto remunerado que contemplan los artículos 126 y 127 del Código de Trabajo, "
                                            + "respectivamente, serán cancelados en la forma que establece el artículo 129 del mismo cuerpo legal. OCTAVA: OTRAS PRESTACIONES: Las vacaciones, la bonificación "
                                            + "anual contemplada en el Decreto 42-92 del Congreso de la República y el aguinaldo se pagarán al trabajador(a) conforme a la ley. NOVENA: USO DE EQUIPO, MATERIALES "
                                            + "Y HERRAMIENTAS: El equipo, material o herramientas que el patrono le proporciona al (la) trabajador(a) son para uso exclusivo de la realización de su trabajo, "
                                            + "los cuales consisten en: a) dinero; b) bienes; c) mobiliario; d) servicio telefónico; e) servicio eléctrico; f) Internet; g) insumos; h) papelería; i) productos "
                                            + "de limpieza; j) equipo de seguridad industrial; k) uniformes; l) vehículos, entre otros. El (la) trabajador(a) reconoce que es su obligación cuidar, darle mantenimiento"
                                            + " –según pudiera corresponder– y velar por la buena conservación del equipo, materiales y herramientas que sean suministradas por el patrono. De igual forma el (la)"
                                            + " trabajador(a) expresa que comprende que su patrono le suministrará el equipo, materiales y herramientas de trabajo, y que renovará dichos insumos por el desgaste "
                                            + "normal de los mismos, por lo que, si dichos éstos se dañaren por mal uso, es responsabilidad y obligación del trabajador(a) reemplazarlos inmediatamente. Las partes"
                                            + " acuerdan que el (la) trabajador(a) no podrá emplear útiles, herramientas de trabajo, implementos o materiales suministrados por el patrono para usos distintos de"
                                            + " aquel al que están normalmente destinados. DÉCIMA: CONFIDENCIALIDAD: El (la) trabajador(a) se obliga a mantener en confidencia, durante su relación laboral y aún"
                                            + " después de haber finalizado dicha relación, toda información o material, de cualquier naturaleza, a que tenga acceso por razón de su cargo, dentro de la prestación"
                                            + " de sus servicios, por las relaciones personales que establezca durante la vigencia de este contrato y por cualquier otra circunstancia. Dicha información comprende,"
                                            + " de manera enunciativa y no exclusiva o limitativa, todos aquellos datos escritos, en forma verbal, digital, electrónica, impresa, en fotografías, videos, informes, "
                                            + "computadoras, servidores en cintas, dispositivos o registros magnéticos, o en cualquier otra forma o modo susceptible de ser conocida, accesada o comunicada, la que "
                                            + "incluye pero no se limita a los datos de clientes nacionales e internacionales, proveedores, socios o trabajadores de Exportadora Enlasa, Sociedad Anónima,"
                                            + " y/o cualquier otra entidad con quien el patrono tenga relaciones comerciales y con cualquiera otra situación interna del patrono relacionada con el sistema "
                                            + "administrativo del mismo, el manejo de los negocios, la capacidad financiera o capacidad técnica, o cualquier otra información con valor comercial e importancia "
                                            + "económica, incluyendo procesos de fabricación, formulación, producción, elaboración y distribución, fórmulas, marcas y distintivos, precios, publicidad, "
                                            + "información de cuentas bancarias, programas de software en cualquier etapa de desarrollo y documentación relacionada con los mismos que se implemente dentro de "
                                            + "la entidad para cualquier propósito; especificaciones técnicas, metodología, know how (conocimientos y experiencia), organigramas, materiales de entrenamiento, "
                                            + "planillas, estrategias y planes, finanzas, documentos y archivos contables; todas las operaciones e información que se encuentren relacionadas con el patrono y su "
                                            + "actividad, así como cualquier otra información que el (la) trabajador(a) haya conocido durante todo el tiempo que prestare sus servicios en las instalaciones de "
                                            + "Exportadora Enlasa, Sociedad Anónima, o a entidades relacionadas o subsidiarias y cualquier otra entidad o entidades que tuvieren relación comercial con ésta. "
                                            + "En virtud de la obligación de confidencialidad y de conformidad con el artículo 63 literal g) del Código de Trabajo, el (la) trabajador(a) tiene prohibido: "
                                            + "a) Revelar, informar, publicar, divulgar o transferir, directa o indirectamente, por cualquier medio, la información a que tenga acceso a cualesquiera personas,"
                                            + " individuales o jurídicas, nacionales o extranjeras, para cualquier propósito; b) Utilizar la información que obtenga en el ejercicio de su cargo para propósitos"
                                            + " distintos a la fiel y exacta prestación de sus servicios; c) Remover, reproducir, resumir, alterar, mutilar o copiar cualquiera información o material de la entidad"
                                            + " empleadora. El (la) trabajador(a) entiende que la información a que se hace referencia en esta cláusula incluye, sin limitación, los aspectos enumerados en la primera "
                                            + "parte de la misma y está advertida de las responsabilidades penales en que pudiere incurrir si incumple con lo dispuesto en esta cláusula, de conformidad con lo "
                                            + "establecido en los artículos 274 “A”, 274 “B”, 274 “C”, 274 “D”, 274 “E”, 274 “F”, 274 “G” y 275 del Código Penal y cualquiera otra disposición legal aplicable. "
                                            + "DÉCIMA PRIMERA: EXCLUSIVIDAD EN LA PRESTACIÓN DE SERVICIOS: El (la) trabajador(a) se obliga expresamente a prestar sus servicios exclusivamente al patrono, salvo que "
                                            + "exista autorización expresa de éste por escrito; en consecuencia, el (la) trabajador(a) en ningún momento durante la vigencia de este contrato: a) Aceptará otro empleo "
                                            + "ni prestará sus servicios de naturaleza mercantil, profesional o comercial a ninguna otra persona o entidad; b) Se dedicará a ningún negocio o actividad por sí o por"
                                            + " medio de un tercero, que el patrono de buena fe pueda considerar que le es adversa; y c) Se dedicará a ningún negocio o actividad que el patrono de buena fe, pueda "
                                            + "considerar que interfiere con el rendimiento del empleado; d) Toda la información o documentación que se genere en la relación de trabajo es propiedad de Exportadora "
                                            + "Enlasa, S.A. DÉCIMA SEGUNDA: USO INFORMÁTICO: Las partes acuerdan que el (la) trabajador(a) no podrá utilizar el equipo informático, internet o el correo electrónico en "
                                            + "tareas personales, juegos o comunicación con personas fuera de la empresa. Es por ello que el (la) trabajador(a) para los efectos del presente contrato permitirá que en "
                                            + "cualquier momento la persona que designe el patrono evalúe o inspeccione los servicios y demás actividades que realice, así como el equipo que utiliza para ello. DÉCIMA "
                                            + "TERCERA: PROHIBICIONES ESPECIALES: De conformidad con lo dispuesto en el Capítulo VII, del Título VI, del Libro Segundo del Código Penal, sin perjuicio de la "
                                            + "responsabilidad penal en que incurra, el (la) trabajador(a) le queda prohibido: a) Destruir, borrar o de cualquier modo inutilizar registros informáticos; b) Alterar,"
                                            + " borrar o de cualquier modo inutilizar las instrucciones o programas que utilizan las computadoras; c) Copiar o de cualquier modo reproducir las instrucciones o programas "
                                            + "de computación, sin autorización del autor; d) Crear un banco de datos o un registro informático con datos que puedan afectar la intimidad de las personas; e) Utilizar "
                                            + "registros informáticos o programas de computación para ocultar, alterar o distorsionar información requerida para una actividad comercial, para el cumplimiento de una "
                                            + "obligación respecto al Estado o para ocultar, falsear o alterar los estados contables o la situación patrimonial de una persona física o jurídica; f) Utilizar los registros "
                                            + "informáticos de otro, o ingresar, por cualquier medio, a su banco de datos o archivos electrónicos, sin autorización; y g) Destruir o poner en circulación programas o"
                                            + " instrucciones destructivas, que puedan causar perjuicio a los registros, programas o equipos de computación. DÉCIMA CUARTA: TERMINACIÓN DEL CONTRATO: Las partes acuerdan"
                                            + " que de conformidad con la ley, son causas justas para terminar el contrato individual de trabajo, sin responsabilidad para el patrono, además de las indicadas en el "
                                            + "artículo setenta y siete (77) del Código de Trabajo, las siguientes: a) Si el (la) trabajador(a) incumple con la cláusula de exclusividad convenida en este contrato; b) "
                                            + "Si el (la) trabajador(a) violare la cláusula de confidencialidad convenida en este contrato; c) Si el (la) trabajador(a) violare las obligaciones contenidas en la"
                                            + " cláusula del uso de equipo, materiales y herramientas contenida en este contrato; d) Si el (la) trabajador(a) incumple alguna de sus obligaciones o atribuciones "
                                            + "pactadas en este contrato; e) Si el (la) trabajador(a) violare la cláusula referente a prohibiciones especiales; f) Si el (la) trabajador(a), al momento de celebrar "
                                            + "el presente contrato, proporcionó información falsa para ser contratado; y g) Cualquier otra causal que el patrono invoque por incumplimiento del presente contrato o "
                                            + "de las obligaciones principales contenidas en la ley laboral y sus reglamentos. El presente contrato individual de trabajo se suscribe en el Municipio de Villa"
                                            + " Nueva, Departamento de Guatemala " + dialetrascontarto + " (" + diacontrato + ") de " + mesletrascontrato + " de " + ayoletrascontrato + " (" + ayocontrato + "). "
                                            + "\n\n\n\n"
                                            ;

                                            Paragraph additionalParagraph = new Paragraph(new Run(new Text(textoAdicional)));
                                            body.Append(additionalParagraph);
                                            mainPart.Document.Save();
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                string tempFilePath = Path.Combine(Path.GetTempPath(), "Numbered_" + fileName + ".docx");
                                File.WriteAllBytes(tempFilePath, memoryStream.ToArray());

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".docx");
                                Response.WriteFile(tempFilePath);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                }
            }
        }
        private string GetMonthName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }
    }
}