using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCenter.Data;
using MovieCenter.Logic;
namespace MovieCenter.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }
        public void Mensaje(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{mensaje}')", true);

        }
        public void InicioSesion()
        {
            string error = null;
            Usuario user = new Usuario();

            if (!string.IsNullOrWhiteSpace(TxtUsuario.Text) &&
                !string.IsNullOrWhiteSpace(TxtClave.Text))
            {
                user.NombreUsuario = TxtUsuario.Text;
                user.Clave = TxtClave.Text;
                Cliente cliente = new ClienteBL().Login(user, ref error);
              
                if (error == null && cliente.Id > 0 )
                {
                    cliente.UsuarioP.Rol.Mantenimientos = new MantenimientoBL().ConsultarMantenimientos(ref error);

                    foreach (var li in cliente.UsuarioP.Rol.Mantenimientos)
                    {
                        li.Permisos = new PermisoBL().ConsultarPermisos2(li, cliente.UsuarioP.Rol, ref error);
                    }
                    Session["cliente"] = cliente;
                    Session["id_Usuario"] = cliente.UsuarioP.Id;

                    Response.Redirect("Peliculas.aspx");
                }
                else
                {
                    Mensaje("Nombre de usuario o clave no coinciden, por favor intentelo de nuevo.");
                }
            }
            else
            {
                Mensaje("Debe ingresar sus datos");
            }

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            InicioSesion();
        }
    }
}