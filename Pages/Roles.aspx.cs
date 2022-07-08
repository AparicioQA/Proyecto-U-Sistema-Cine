using MovieCenter.Data;
using MovieCenter.Helper;
using MovieCenter.Logic;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace MovieCenter.Pages
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ConsultarRoles();
                ConsultarMantenimientos();
                ViewState["id_Rol"] = DolistRoles.SelectedItem.Value;
                ViewState["id_Mantenimiento"] = DolistMantenimientos.SelectedValue;
                ConsultarPermisos();
                TxtRol.Text = DolistRoles.SelectedItem.Text;

            }
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

        public void ConsultarRoles()
        {
            string error = null;
            RolBL rol = new RolBL();

            List<Rol> lista = rol.ConsultarRoles(ref error);

            if (error == null)
            {
                DolistRoles.DataSource = null;
                DolistRoles.DataBind();
                DolistRoles.DataSource = lista;
                DolistRoles.DataBind();
                if (ViewState["id_Rol"] != null)
                {
                    DolistRoles.SelectedValue = ViewState["id_Rol"].ToString();
                }
            }
            else
            {
                Mensaje(error);
            }
        }

        public void ConsultarMantenimientos()
        {
            string error = null;
            MantenimientoBL mante = new MantenimientoBL();

            List<Mantenimiento> lista = mante.ConsultarMantenimientos(ref error);

            if (error == null)
            {
                DolistMantenimientos.DataSource = null;
                DolistMantenimientos.DataBind();
                DolistMantenimientos.DataSource = lista;
                DolistMantenimientos.DataBind();
            }
            else
            {
                Mensaje(error);
            }
        }

        public void ConsultarPermisos()
        {

            string error = null;
            PermisoBL permiso = new PermisoBL();

            Mantenimiento mantenimiento = new Mantenimiento();
            mantenimiento.Id = Convert.ToInt32(ViewState["id_Mantenimiento"]);

            Rol rol = new Rol();
            rol.Id = Convert.ToInt32(ViewState["id_Rol"]);

            List<Permiso> lista = permiso.ConsultarPermisos(mantenimiento, rol, ref error);
            CbxListAcciones.Items.Clear();
            if (error == null)
            {
                foreach (Permiso permi in lista)
                {
                    CbxListAcciones.Items.Add(new ListItem(permi.AccionP.Nombre, permi.Id.ToString(), true));
                    CbxListAcciones.Items[CbxListAcciones.Items.Count - 1].Selected = permi.Estado;
                }

            }
            else
            {
                Mensaje(error);
            }
        }

        protected void BtnAplicar_Click(object sender, EventArgs e)
        {
            ModificarRol();
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRol();
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            RegistrarRol();
        }



        protected void DolistRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["id_Rol"] = DolistRoles.SelectedItem.Value;
            ConsultarPermisos();
            TxtRol.Text = DolistRoles.SelectedItem.Text;
        }

        protected void DolistMantenimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["id_Mantenimiento"] = DolistMantenimientos.SelectedValue;
            ConsultarPermisos();
        }

        public void RegistrarRol()
        {

            RolBL rolbl = new RolBL();

            if (!string.IsNullOrWhiteSpace(TxtRol.Text) && !Verificaciones.TieneNumeros(TxtRol.Text))
            {

                string error = null;

                Rol rol = new Rol();
                rol.Nombre = TxtRol.Text;
                rol.Usuario = Convert.ToInt32(Session["id_Usuario"]);

                rolbl.RegistrarRol(rol, ref error);
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

            ConsultarRoles();
        }

        public void EliminarRol()
        {
            string error = null;
            RolBL rolbl = new RolBL();

            Rol rol = new Rol();
            rol.Id = Convert.ToInt32(ViewState["id_Rol"]);
            rol.Usuario = Convert.ToInt32(Session["id_Usuario"]);
            rolbl.EliminarRol(rol, ref error);
            if (error != null)
            {
                Mensaje(error);
            }
            ConsultarRoles();
            ConsultarPermisos();

        }

        public void ModificarRol()
        {

            if (!string.IsNullOrWhiteSpace(TxtRol.Text) && !Verificaciones.TieneNumeros(TxtRol.Text))
            {
                string error = null;
                RolBL rolbl = new RolBL();
                Rol rol = new Rol();
                rol.Nombre = TxtRol.Text;
                rol.Id = Convert.ToInt32(ViewState["id_Rol"]);
                rol.Usuario = Convert.ToInt32(Session["id_Usuario"]);
                rolbl.ModificarRol(rol, ref error);
                PermisoBL permibl = new PermisoBL();
                Permiso permiso = new Permiso();

                for (int i = 0; i < CbxListAcciones.Items.Count; i++)
                {
                    permiso.Id = Convert.ToInt32(CbxListAcciones.Items[i].Value);

                    permiso.Estado = CbxListAcciones.Items[i].Selected;
                    permiso.Usuario = Convert.ToInt32(Session["id_Usuario"]);
                    permibl.ModificarPermiso(permiso, ref error);
                }
                if (error != null)
                {
                    Mensaje(error);
                }
                ConsultarRoles();
                ConsultarPermisos();
                TxtRol.Text = DolistRoles.SelectedItem.Text;
            }

        }
    }
}