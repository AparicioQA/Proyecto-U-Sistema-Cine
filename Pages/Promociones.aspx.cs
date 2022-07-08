using MovieCenter.Data;
using MovieCenter.Logic;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace MovieCenter.Pages
{
    public partial class Promociones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ConsultarPromociones();
                ViewState["modo"] = "I";
                ConsultarTiposSala();
                ListarDias();
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

        protected void GrdPromociones_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["modo"] = "A"; //modo A = Actualizar

            GridViewRow row = GrdPromociones.SelectedRow;
            int id;
            int.TryParse(row.Cells[0].Text, out id);
            ViewState["id_Promocion"] = id;
            ConsultarTiposSala(Convert.ToInt32(row.Cells[1].Text));
            TxtPrecio.Text = row.Cells[3].Text;
            ListarDias(row.Cells[4].Text);
        }

        public void ConsultarPromociones()
        {
            string error = null;
            PromocionBL promocion = new PromocionBL();

            List<Promocion> lista = new List<Promocion>();
            lista = promocion.ConsultarPromociones(ref error);


            if (error == null)
            {
                GrdPromociones.DataSource = null;
                GrdPromociones.DataBind();
                GrdPromociones.DataSource = lista;
                GrdPromociones.DataBind();
            }
            else
            {
                Mensaje(error);
            }
        }

        public void ConsultarTiposSala(int salaSeleccionada = 0)
        {
            string error = null;
            TipoSalaBL salas = new TipoSalaBL();

            List<TipoSala> lista = new List<TipoSala>();
            lista = salas.ConsultarTiposSala(ref error);

            if (error == null)
            {

                DolistSala.DataSource = lista;
                DolistSala.DataBind();
                for (int i = 0; i < lista.Count; i++)
                {

                    if (DolistSala.Items[i].Value == salaSeleccionada.ToString())
                    {
                        DolistSala.Items[i].Selected = true;
                    }
                }

            }
            else
            {
                Mensaje(error);
            }
        }


        public void ListarDias(string diaSeleccionado = "Lunes")
        {
            List<string> dias = new List<string>() { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" };

            DolistDia.DataSource = dias;
            DolistDia.DataBind();
            for (int i = 0; i < DolistDia.Items.Count; i++)
            {
                if (DolistDia.Items[i].Text.Equals(diaSeleccionado))
                {
                    DolistDia.SelectedIndex = i;
                }
            }

        }



        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            string modo = ViewState["modo"].ToString();

            if (modo == "I") //Modo == "I" Modo Insercion
            {
                RegistrarPromocion();
            }
            else if (modo == "A")
            {
                ModificarPromocion();
            }
        }

        protected void GrdPromociones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GrdPromociones.Rows[e.RowIndex];
            ViewState["id_Promocion"] = row.Cells[0].Text;
            EliminarPromocion();
        }

        public void EliminarPromocion()
        {
            PromocionBL promoBl = new PromocionBL();
            Promocion promocion = new Promocion();

            string error = null;

            promocion.Usuario = Convert.ToInt32(Session["id_Usuario"]);
            promocion.Id = Convert.ToInt32(ViewState["id_Promocion"]);

            promoBl.EliminarPromocion(promocion, ref error);

            if (error != null)
            {
                Mensaje(error);
            }
            ConsultarPromociones();
            Limpiar();
        }

        public void Limpiar()
        {
            ViewState["modo"] = "I";
            ViewState["id_Promocion"] = 0;
            TxtPrecio.Text = "";
            ConsultarTiposSala();
            ListarDias();
        }

        public void BtnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        public void RegistrarPromocion()
        {

            PromocionBL promoBl = new PromocionBL();
            TipoSala sala = new TipoSala();
            sala.Id = Convert.ToInt32(DolistSala.SelectedItem.Value);
            decimal precio;

            if (decimal.TryParse(TxtPrecio.Text, out precio))
            {
                Promocion promocion = new Promocion();
                promocion.Precio = precio;
                promocion.Sala = sala;
                promocion.Usuario = Convert.ToInt32(Session["id_Usuario"]);
                promocion.Dia = DolistDia.SelectedValue;
                string error = null;
                promoBl.RegistrarPromocion(promocion, ref error);
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
            ConsultarPromociones();

        }

        public void ModificarPromocion()
        {

            PromocionBL promoBl = new PromocionBL();
            TipoSala sala = new TipoSala();
            sala.Id = Convert.ToInt32(DolistSala.SelectedItem.Value);

            decimal precio;

            if (decimal.TryParse(TxtPrecio.Text, out precio))
            {
                Promocion promocion = new Promocion();
                promocion.Id = Convert.ToInt32(ViewState["id_Promocion"]);
                promocion.Precio = precio;
                promocion.Sala = sala;
                promocion.Usuario = Convert.ToInt32(Session["id_Usuario"]);
                promocion.Dia = DolistDia.SelectedValue;

                string error = null;
                promoBl.ModificarPromocion(promocion, ref error);
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
            ConsultarPromociones();
        }

    }
}