using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Admin
{
    public partial class Representante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    DDLRepresentantel();
                    Representantescreados(long.Parse(DDLRepresentante.SelectedValue));
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar Listado de Procesos en DropDownList
        protected void DDLRepresentantel()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_RCH00701_2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                DDLRepresentante.DataSource = cmd.ExecuteReader();
                DDLRepresentante.DataTextField = "Representante";
                DDLRepresentante.DataValueField = "Id_Representante";
                DDLRepresentante.DataBind();
                con.Close();
                DDLRepresentante.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Función al seleccionar el Proceso
        protected void DDLRepresentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            Representantescreados(long.Parse(DDLRepresentante.SelectedValue));
        }
        // Trae el texto de las funciones por Proceso y puesto
        protected void Representantescreados(long IdRepresentante)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Info1,Cargo,Entidad,Sede,Direccion FROM RCH00701 WHERE Id_Representante=@Id_Representante", con);
                cmd.Parameters.AddWithValue("@Id_Representante", IdRepresentante);
                SqlDataReader FuncionReader = cmd.ExecuteReader();
                if (FuncionReader.Read())
                {
                    string info1 = FuncionReader["Info1"].ToString();
                    string cargo = FuncionReader["Cargo"].ToString();
                    string entidad = FuncionReader["Entidad"].ToString();
                    string sede = FuncionReader["Sede"].ToString();
                    string direccion = FuncionReader["Direccion"].ToString();

                    Info1.Text = info1;
                    Cargo.Text = cargo;
                    Entidad.Text = entidad;
                    Sede.Text = sede;
                    Direccion.Text = direccion;
                }
                FuncionReader.Close();
                //con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        protected void Limpiar_Texbox()
        {
            Info1.Text = "";
            Cargo.Text = "";
            Entidad.Text = "";
            Sede.Text = "";
            Direccion.Text = "";
        }
        protected void Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_RCH00701", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Info1", System.Data.SqlDbType.VarChar).Value = Info1.Text;
                cmd.Parameters.Add("@Cargo", System.Data.SqlDbType.VarChar).Value = Cargo.Text;
                cmd.Parameters.Add("@Entidad", System.Data.SqlDbType.VarChar).Value = Entidad.Text;
                cmd.Parameters.Add("@Sede", System.Data.SqlDbType.VarChar).Value = Sede.Text;
                cmd.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = Direccion.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("Representante.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void CancelarAg_Click(object sender, EventArgs e)
        {

        }
        protected void Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00701", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Representante", System.Data.SqlDbType.VarChar).Value = DDLRepresentante.SelectedValue;
                cmd.Parameters.Add("@Info1", System.Data.SqlDbType.VarChar).Value = Info1.Text;
                cmd.Parameters.Add("@Cargo", System.Data.SqlDbType.VarChar).Value = Cargo.Text;
                cmd.Parameters.Add("@Entidad", System.Data.SqlDbType.VarChar).Value = Entidad.Text;
                cmd.Parameters.Add("@Sede", System.Data.SqlDbType.VarChar).Value = Sede.Text;
                cmd.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = Direccion.Text;
                cmd.Parameters.Add("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("Representante.aspx");
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