using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    DDLHorarios();
                    Horarioscreados(long.Parse(DDLHorario.SelectedValue));
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar Listado de Procesos en DropDownList
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
                DDLHorario.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Función al seleccionar el Proceso
        protected void DDLHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            Horarioscreados(long.Parse(DDLHorario.SelectedValue));
        }
        // Trae el texto de las funciones por Proceso y puesto
        protected void Horarioscreados(long horario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Horario,Descripcion FROM RCH00702 WHERE Id_Horario=@Id_Horario", con);
                cmd.Parameters.AddWithValue("@Id_Horario", horario);
                SqlDataReader FuncionReader = cmd.ExecuteReader();
                if (FuncionReader.Read())
                {
                    string Horarios = FuncionReader["Horario"].ToString();
                    string Descripcion = FuncionReader["Descripcion"].ToString();

                    ModHorario.Text = Horarios;
                    ModDesHorario.Text = Descripcion;
                }
                FuncionReader.Close();
                //con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        protected void Limpiar_Texbox()
        {
            ModHorario.Text = "";
            ModDesHorario.Text = "";
        }
        protected void Agregar_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_AG_RCH00702", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Horario", System.Data.SqlDbType.VarChar).Value = AgHorario.Text;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = AgDescHorario.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Horarios.aspx");
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                       "swal('Error!', 'Error en validación de datos!', 'error')", true);
                }
            }
        }
        protected void CancelarAg_Click(object sender, EventArgs e)
        {

        }
        protected void Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00702", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Horario", System.Data.SqlDbType.VarChar).Value = ModHorario.Text;
                cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = ModDesHorario.Text;
                cmd.Parameters.Add("@Id_Horario", System.Data.SqlDbType.Int).Value = DDLHorario.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("Horarios.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void CancelarMod_Click(object sender, EventArgs e)
        {

        }
    }
}