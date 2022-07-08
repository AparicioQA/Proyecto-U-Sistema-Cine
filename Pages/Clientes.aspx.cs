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
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ConsultarClientes();
                ConsultarUsuarios();
                ViewState["modo"] = "I";
                TxtCedula.Enabled = true;
            }
         
        }

        protected void GrdClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["modo"] = "A"; //modo A = Actualizar

            GridViewRow row = GrdClientes.SelectedRow;
            int id;
            int.TryParse(row.Cells[0].Text, out id);
            ViewState["cedula"] = id;
            TxtCedula.Text = id.ToString();
            TxtCedula.Enabled = false;
            TxtNombre.Text = row.Cells[1].Text;
            TxtApellido1.Text = row.Cells[2].Text;
            TxtApellido2.Text = row.Cells[3].Text;
            TxtCorreo.Text = row.Cells[6].Text;
            DateTime fecha = Convert.ToDateTime(row.Cells[7].Text);
            TxtFechaNacimiento.Text = fecha.ToString("yyyy-MM-dd");
            TxtTelefono.Text = row.Cells[8].Text;
            DolistUsuario.Visible = false;

        }
        public void Mensaje(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{mensaje}')", true);
        }
        public void ConsultarClientes()
        {
            string error = null;
            ClienteBL cliente = new ClienteBL();

            List<Cliente> lista = new List<Cliente>();
            lista = cliente.ConsultarClientes(ref error);


            if (error == null)
            {
                GrdClientes.DataSource = null;
                GrdClientes.DataBind();
                GrdClientes.DataSource = lista;
                GrdClientes.DataBind();
            }
            else
            {
                Mensaje(error);
            }
        }

        protected void GrdClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GrdClientes.Rows[e.RowIndex];
            ViewState["id_Cliente"] = row.Cells[0].Text;
            EliminarCliente();
        }

        public void ConsultarUsuarios()
        {
            string error = null;

            UsuarioBL usuarioBl = new UsuarioBL();
            List<Usuario> lista = usuarioBl.ConsultarUsuariosSinCliente(ref error);
            DolistUsuario.Items.Clear();
            if(error == null)
            {
                foreach (var li in lista)
                {
                    DolistUsuario.Items.Add(new ListItem(li.NombreUsuario, li.Id.ToString()));
                }
            }
            else
            {
                Mensaje(error);
            }
        }

        public void EliminarCliente()
        {
            ClienteBL clientBl = new ClienteBL();
            Cliente cliente = new Cliente();

            string error = null;

            cliente.User = Convert.ToInt32(Session["id_Usuario"]);
            cliente.Id = Convert.ToInt32(ViewState["id_Cliente"]);

            clientBl.EliminarCliente(cliente, ref error);

            if (error != null)
            {
                Mensaje(error);
            }
            ConsultarClientes();
            Limpiar();
        }
        public void Limpiar()
        {
            ViewState["modo"] = "I";
            ViewState["cedula"] = 0;
            TxtCedula.Enabled = true;
            TxtCedula.Text = "";
            TxtNombre.Text = "";
            TxtApellido1.Text = "";
            TxtApellido2.Text = "";
            TxtCorreo.Text = "";
            TxtFechaNacimiento.Text = "";
            TxtTelefono.Text = "";
            DolistUsuario.Visible = true;
            ConsultarUsuarios();
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        public void RegistrarCliente()
        {

            ClienteBL clienteBl = new ClienteBL();
            Usuario usuario = new Usuario();

            DateTime fechaNacimiento;
            int telefono, cedula;

            if (int.TryParse(TxtTelefono.Text, out telefono) && int.TryParse(TxtCedula.Text, out cedula) &&
                cedula.ToString().Length == 9 &&
                DateTime.TryParse(TxtFechaNacimiento.Text, out fechaNacimiento)
                && !string.IsNullOrWhiteSpace(TxtNombre.Text) && !string.IsNullOrWhiteSpace(TxtApellido1.Text)
                && !string.IsNullOrWhiteSpace(TxtApellido2.Text) && !string.IsNullOrWhiteSpace(TxtCorreo.Text)
                && !string.IsNullOrWhiteSpace(DolistUsuario.SelectedValue))
            {
                Cliente cliente = new Cliente();
                cliente.User = Convert.ToInt32(Session["id_Usuario"]);
                cliente.Id = cedula;
                cliente.Nombre = TxtNombre.Text;
                cliente.Apellido1 = TxtApellido1.Text;
                cliente.Apellido2 = TxtApellido2.Text;
                cliente.Correo = TxtCorreo.Text;
                cliente.FechaNacimiento = fechaNacimiento;
                cliente.Telefono = telefono;
                cliente.UsuarioP = new Usuario { Id = Convert.ToInt32(DolistUsuario.SelectedValue) };
                string error = null;
                clienteBl.RegistrarCliente(cliente, ref error);
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
            ConsultarClientes();

        }

        public void ModificarCliente()
        {
            ClienteBL clienteBl = new ClienteBL();
            Usuario usuario = new Usuario();

            DateTime fechaNacimiento;
            int telefono, cedula;

            if (int.TryParse(TxtTelefono.Text, out telefono) && int.TryParse(TxtCedula.Text, out cedula) &&
                cedula.ToString().Length == 9 &&
                DateTime.TryParse(TxtFechaNacimiento.Text, out fechaNacimiento)
                && !string.IsNullOrWhiteSpace(TxtNombre.Text) && !string.IsNullOrWhiteSpace(TxtApellido1.Text)
                && !string.IsNullOrWhiteSpace(TxtApellido2.Text) && !string.IsNullOrWhiteSpace(TxtCorreo.Text))
            {
                Cliente cliente = new Cliente();
                cliente.User = Convert.ToInt32(Session["id_Usuario"]);
                cliente.Id = cedula;
                cliente.Nombre = TxtNombre.Text;
                cliente.Apellido1 = TxtApellido1.Text;
                cliente.Apellido2 = TxtApellido2.Text;
                cliente.Correo = TxtCorreo.Text;
                cliente.FechaNacimiento = fechaNacimiento;
                cliente.Telefono = telefono;
                cliente.User = Convert.ToInt32(Session["id_Usuario"]);
                string error = null;
                clienteBl.ModificarCliente(cliente, ref error);
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
            ConsultarClientes();
        }
       

        protected void BtnGuardar_Click1(object sender, EventArgs e)
        {
            string modo = ViewState["modo"].ToString();

            if (modo == "I") //Modo == "I" Modo Insercion
            {
                RegistrarCliente();
            }
            else if (modo == "A")
            {
                ModificarCliente();
            }
        }
    }
}