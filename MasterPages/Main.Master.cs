using MovieCenter.Data;
using System;
using System.Data;
using System.Web.UI;

namespace MovieCenter.MasterPages
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cliente"] == null)
            {
                Response.Redirect("Login.aspx");
            }


            Cliente client = Session["cliente"] as Cliente;
            LblUsuario.Text = client.UsuarioP.NombreUsuario;

            if (client.UsuarioP.Rol.Mantenimientos[0].Permisos[0].Estado == false)
            {
                HyRoles.Visible = false;
            }
            if (client.UsuarioP.Rol.Mantenimientos[1].Permisos[0].Estado == false)
            {
                HyUsuarios.Visible = false;
            }
            if (client.UsuarioP.Rol.Mantenimientos[2].Permisos[0].Estado == false)
            {
                HyClientes.Visible = false;
            }
            if (client.UsuarioP.Rol.Mantenimientos[3].Permisos[0].Estado == false)
            {
                HyPromociones.Visible = false;
            }
            if (client.UsuarioP.Rol.Mantenimientos[4].Permisos[0].Estado == false)
            {
                HyEntradas.Visible = false;
            }
            if (client.UsuarioP.Rol.Mantenimientos[6].Permisos[0].Estado == false)
            {
                HyBonificaciones.Visible = false;
            }

            //cr.fi.bccr.gee.wsindicadoreseconomicos cliente = new cr.fi.bccr.gee.wsindicadoreseconomicos();
            //DataSet tipoCambio = cliente.ObtenerIndicadoresEconomicos("317", DateTime.Now.ToString("dd/MM/yyyy"),
            //DateTime.Now.ToString("dd/MM/yyyy"), "Brian", "N", "aparicio3099@gmail.com", "523MGMLA30");
            WebServiceBancoCentral.wsindicadoreseconomicosSoapClient WS = new WebServiceBancoCentral.wsindicadoreseconomicosSoapClient();

            DataSet tipoCambio = WS.ObtenerIndicadoresEconomicos("317", DateTime.Now.ToString("dd/MM/yyyy"),
            DateTime.Now.ToString("dd/MM/yyyy"), "Brian", "N", "aparicio3099@gmail.com", "523MGMLA30");

            decimal cambio = Math.Round(decimal.Parse(tipoCambio.Tables[0].Rows[0].ItemArray[2].ToString()), 2);
            Session["tipo_Cambio"] = cambio;
                LblTipoCambio.Text = $"Tipo de cambio: {cambio}";

        }
        public void Mensaje(String pMensaje)
        {
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))

            {
                string cstext = $"alert('{pMensaje}');";
                cs.RegisterStartupScript(cstype, "PoputScript", cstext, true);
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        public void Logout()
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}