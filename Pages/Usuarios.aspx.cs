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
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ConsultarUsuarios();
                ViewState["modo"] = "I";
                ConsultarTiposRol();

            }
        }
        public void Mensaje(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{mensaje}')", true);

        }
        protected void GrdUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["modo"] = "A"; //modo A = Actualizar

            GridViewRow row = GrdUsuarios.SelectedRow;
            int id;
            int.TryParse(row.Cells[0].Text, out id);
            ViewState["id_Usuario"] = id;
            ConsultarTiposRol(Convert.ToInt32(row.Cells[2].Text));
            TxtNombreUsuario.Text = row.Cells[1].Text;
        }

        public void ConsultarUsuarios()
        {
            string error = null;
            UsuarioBL usuario  = new UsuarioBL();

            List<Usuario> lista = new List<Usuario>();
            lista = usuario.ConsultarUsuarios(ref error);


            if (error == null)
            {
                GrdUsuarios.DataSource = null;
                GrdUsuarios.DataBind();
                GrdUsuarios.DataSource = lista;
                GrdUsuarios.DataBind();
            }
            else
            {
                Mensaje(error);
            }
        }

        public void ConsultarTiposRol(int rolSeleccionado = 0)
        {
            string error = null;
            TipoRolBL rol = new TipoRolBL();

            List<TipoRol> lista = new List<TipoRol>();
            lista = rol.ConsultarTiposRol(ref error);

            if (error == null)
            {

                DolistRol.DataSource = lista;
                DolistRol.DataBind();
                for (int i = 0; i < lista.Count; i++)
                {

                    if (DolistRol.Items[i].Value == rolSeleccionado.ToString())
                    {
                        DolistRol.Items[i].Selected = true;
                    }
                }

            }
            else
            {
                Mensaje(error);
            }
        }

        protected void GrdUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GrdUsuarios.Rows[e.RowIndex];
            ViewState["id_Usuario"] = row.Cells[0].Text;
            EliminarUsuario();
        }
        public void EliminarUsuario()
        {
            UsuarioBL usuaBl = new UsuarioBL();
            Usuario usuario = new Usuario();

            string error = null;

            usuario.User = Convert.ToInt32(Session["id_Usuario"]);
            usuario.Id = Convert.ToInt32(ViewState["id_Usuario"]);

            usuaBl.EliminarUsuario(usuario, ref error);

            if (error != null)
            {
                Mensaje(error);
            }
            ConsultarUsuarios();
            Limpiar();
        }

        public void Limpiar()
        {
            ViewState["modo"] = "I";
            ViewState["id_Usuario"] = 0;
            TxtNombreUsuario.Text = "";
            TxtClave.Text = "";
            ConsultarTiposRol();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            string modo = ViewState["modo"].ToString();

            if (modo == "I") //Modo == "I" Modo Insercion
            {
                RegistrarUsuario();
            }
            else if (modo == "A")
            {
                ModificarUsuario();
            }
        }

        public void RegistrarUsuario()
        {

            UsuarioBL usuaBl = new UsuarioBL();
            TipoRol rol = new TipoRol();
            rol.Id = Convert.ToInt32(DolistRol.SelectedItem.Value);
            
            if (!string.IsNullOrWhiteSpace(TxtNombreUsuario.Text) &&
                 !string.IsNullOrWhiteSpace(TxtClave.Text) && TxtClave.Text.Length >= 8)
            {
                Usuario usuario = new Usuario();
                usuario.NombreUsuario = TxtNombreUsuario.Text;
                usuario.Clave = TxtClave.Text;
                usuario.User = Convert.ToInt32(Session["id_Usuario"]);
                usuario.Rol = rol;
                string error = null;
                usuaBl.RegistrarUsuario(usuario, ref error);
                if (error != null)
                {
                    Mensaje(error);
                }

            }
            else
            {
                Mensaje("Ingrese la informacion en los campos de texto, no se admite caracteres de tipo " +
                    "alfanumerico en los campos de texto destinados para numeros");
            }
            Limpiar();
            ConsultarUsuarios();

        }

        public void ModificarUsuario()
        {

            UsuarioBL usuaBl = new UsuarioBL();
            TipoRol rol = new TipoRol();
            rol.Id = Convert.ToInt32(DolistRol.SelectedItem.Value);
      
            if (!string.IsNullOrWhiteSpace(TxtNombreUsuario.Text) &&
                !string.IsNullOrWhiteSpace(TxtClave.Text) && TxtClave.Text.Length >= 8)
            {
                Usuario usuario = new Usuario();
                usuario.Id = Convert.ToInt32(ViewState["id_Usuario"]);
                usuario.NombreUsuario = TxtNombreUsuario.Text;
                usuario.Clave = TxtClave.Text;
                usuario.Rol = rol;
                usuario.User = Convert.ToInt32(Session["id_Usuario"]);
                string error = null;
                usuaBl.ModificarUsuario(usuario, ref error);
                if (error != null)
                {
                    Mensaje(error);
                }

            }
            else
            {
                Mensaje("Ingrese la informacion en los campos de texto, no se admite caracteres de tipo " +
                    "alfanumerico en los campos de texto destinados para números");
            }
            ConsultarUsuarios();
        }


        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}