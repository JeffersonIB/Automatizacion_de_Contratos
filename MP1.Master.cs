using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Contratos
{
    public partial class MP1 : System.Web.UI.MasterPage
    {
        int Id_Rol = 0;
        int Id_Proceso = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Configura la respuesta HTTP para evitar que se almacene en caché
            Response.AppendHeader("Cache-Control", "no-store");
            // Verifica si el usuario está autenticado y configura los elementos de la página
            if (Session["Usuario"] != null)
            {
                lblNombre.Text = Session["Nombre"].ToString();
                lblApellido.Text = Session["Apellido"].ToString();
                Id_Rol = Convert.ToInt32(Session["Id_Rol"].ToString());
                Id_Proceso = Convert.ToInt32(Session["Id_Proceso"].ToString());
                divuser.Visible = true;
                Permisos();
                // Actualiza el tiempo de última actividad de la sesión
                Session["LastActivity"] = DateTime.Now;
                DateTime lastActivity = (DateTime)Session["LastActivity"];
                TimeSpan timeSinceLastActivity = DateTime.Now - lastActivity;
                if (timeSinceLastActivity.TotalMinutes > Session.Timeout)
                {
                    // La sesión ha expirado
                    FormsAuthentication.SignOut();
                    HttpContext.Current.Session.Abandon();
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                // Si el usuario no está autenticado, oculta los elementos de la página y lo dirigien al Login
                divuser.Visible = false;
                lblNombre.Text = string.Empty;
                lblApellido.Text = string.Empty;
                Response.Redirect("~/Default.aspx");
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Roles Permisos
        void Permisos()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_RCH00501", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Proceso"].ToString());
                //cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Proceso"].ToString()); 
                cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Empresa"].ToString());
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                bool PrimeraPag, SegundaPag, TercerPag, CuartaPag, QuintaPag, SextaPag, SeptimaPag, OctavaPag, NovenaPag, DecimaPag, DecimaprimeraPag, DecimasegundaPag;

                while (reader.Read())
                {
                    switch (reader[0].ToString())
                    {
                        case "Primera_Pag":
                            PrimeraPag = Convert.ToBoolean(reader[1].ToString());
                            if (PrimeraPag)
                            {
                                Primera_Pag.Visible = true;
                                Primer_Men.Visible = true;
                            }
                            else
                            {
                                Primera_Pag.Visible = false;
                                Primer_Men.Visible = false;
                            }
                            break;
                        case "Segunda_Pag":
                            SegundaPag = Convert.ToBoolean(reader[1].ToString());
                            if (SegundaPag)
                            {
                                Segunda_Pag.Visible = true;
                                Primer_Men.Visible = true;
                            }
                            else
                            {
                                Segunda_Pag.Visible = false;
                                Primer_Men.Visible = false;
                            }
                            break;
                        case "Tercera_Pag":
                            TercerPag = Convert.ToBoolean(reader[1].ToString());
                            if (TercerPag)
                            {
                                Tercera_Pag.Visible = true;
                                Segundo_Men.Visible = true;
                            }
                            else
                            {
                                Tercera_Pag.Visible = false;
                                Segundo_Men.Visible = false;
                            }
                            break;
                        case "Cuarta_Pag":
                            CuartaPag = Convert.ToBoolean(reader[1].ToString());
                            if (CuartaPag)
                            {
                                Cuarta_Pag.Visible = true;
                                Segundo_Men.Visible = true;
                            }
                            else
                            {
                                Cuarta_Pag.Visible = false;
                                Segundo_Men.Visible = false;
                            }
                            break;
                        case "Quinta_Pag":
                            QuintaPag = Convert.ToBoolean(reader[1].ToString());
                            if (QuintaPag)
                            {
                                Quinta_Pag.Visible = true;
                                //Tercer_Men.Visible = true;
                            }
                            else
                            {
                                Quinta_Pag.Visible = false;
                                //Tercer_Men.Visible = false;
                            }
                            break;
                        case "Sexta_Pag":
                            SextaPag = Convert.ToBoolean(reader[1].ToString());
                            if (SextaPag)
                            {
                                Sexta_Pag.Visible = true;
                                //Tercer_Men.Visible = true;
                            }
                            else
                            {
                                Sexta_Pag.Visible = false;
                                //Tercer_Men.Visible = false;
                            }
                            break;
                        case "Septima_Pap":
                            SeptimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (SeptimaPag)
                            {
                                Septima_Pag.Visible = true;
                                //Tercer_Men.Visible = true;
                            }
                            else
                            {
                                Septima_Pag.Visible = false;
                                //Tercer_Men.Visible = false;
                            }
                            break;
                        case "Octava_Pag":
                            OctavaPag = Convert.ToBoolean(reader[1].ToString());
                            if (OctavaPag)
                            {
                                Octava_Pag.Visible = true;
                                //Tercer_Men.Visible = true;
                            }
                            else
                            {
                                Octava_Pag.Visible = false;
                                //Tercer_Men.Visible = false;
                            }
                            break;
                        case "Novena_Pag":
                            NovenaPag = Convert.ToBoolean(reader[1].ToString());
                            if (NovenaPag)
                            {
                                Novena_Pag.Visible = true;
                            }
                            else
                            {
                                Novena_Pag.Visible = false;
                            }
                            break;
                        case "Decima_Pag":
                            DecimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaPag)
                            {
                                Decima_Pag.Visible = true;
                            }
                            else
                            {
                                Decima_Pag.Visible = false;
                            }
                            break;
                        case "DecimaPrimera_Pag":
                            DecimaprimeraPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaprimeraPag)
                            {
                                DecimaPrimera_Pag.Visible = true;
                            }
                            else
                            {
                                DecimaPrimera_Pag.Visible = false;
                            }
                            break;
                        case "DecimaSegunda_Pag":
                            DecimasegundaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimasegundaPag)
                            {
                                DecimaSegunda_Pag.Visible = true;
                            }
                            else
                            {
                                DecimaSegunda_Pag.Visible = false;
                            }
                            break;
                    }
                }
                con.Close();
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Boton Salir
        protected void Salir_Click(object sender, EventArgs e)
        {
            Session["Id_Empresa"] = null;
            Session["Empresa"] = null;
            Session["Id_Usuario"] = null;
            Session["Nombre"] = null;
            Session["Apellido"] = null;
            Session["Usuario"] = null;
            Session["Clave"] = null;
            Session["Id_Rol"] = null;
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}