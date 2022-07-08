using MovieCenter.Data;
using MovieCenter.Logic;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieCenter.Pages
{
    public partial class Entradas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ConsultarEntradas();
                ViewState["modo"] = "I";
                ConsultarTiposSala();

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

        protected void GrdEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["modo"] = "A"; //modo A = Actualizar

            GridViewRow row = GrdEntradas.SelectedRow;
            int id;
            int.TryParse(row.Cells[0].Text, out id);
            ViewState["id_Entrada"] = id;
            ConsultarTiposSala(Convert.ToInt32(row.Cells[1].Text));
            TxtPrecioGeneral.Text = row.Cells[3].Text;
            TxtPrecioNino.Text = row.Cells[4].Text;
            TxtPrecioMayores.Text = row.Cells[5].Text;
        }
        public void ConsultarEntradas()
        {
            string error = null;
            EntradaBL entrada = new EntradaBL();

            List<Entrada> lista = new List<Entrada>();
            lista = entrada.ConsultarEntradas(ref error);


            if (error == null)
            {
                GrdEntradas.DataSource = null;
                GrdEntradas.DataBind();
                GrdEntradas.DataSource = lista;
                GrdEntradas.DataBind();
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
        protected void GrdEntradas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GrdEntradas.Rows[e.RowIndex];
            ViewState["id_Entrada"] = row.Cells[0].Text;
            EliminarEntrada();
        }
        public void EliminarEntrada()
        {
            EntradaBL entradaBl = new EntradaBL();
            Entrada entrada = new Entrada();

            string error = null;

            entrada.Usuario = Convert.ToInt32(Session["id_Usuario"]);
            entrada.Id = Convert.ToInt32(ViewState["id_Entrada"]);

            entradaBl.EliminarEntrada(entrada, ref error);

            if (error != null)
            {
                Mensaje(error);
            }
            ConsultarEntradas();
            Limpiar();
        }

        public void Limpiar()
        {
            ViewState["modo"] = "I";
            ViewState["id_Entradas"] = 0;
            TxtPrecioGeneral.Text = "";
            TxtPrecioNino.Text = "";
            TxtPrecioMayores.Text = "";
            ConsultarTiposSala();

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            string modo = ViewState["modo"].ToString();

            if (modo == "I") //Modo == "I" Modo Insercion
            {
                RegistrarEntrada();
            }
            else if (modo == "A")
            {
                ModificarEntrada();
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        public void RegistrarEntrada()
        {

            EntradaBL entradaBl = new EntradaBL();
            TipoSala sala = new TipoSala();
            sala.Id = Convert.ToInt32(DolistSala.SelectedItem.Value);
            decimal PrecioGen;
            decimal PrecioNino;
            decimal PrecioMayor;

            if (decimal.TryParse(TxtPrecioGeneral.Text, out PrecioGen) && decimal.TryParse(TxtPrecioNino.Text, out PrecioNino)
                && decimal.TryParse(TxtPrecioMayores.Text, out PrecioMayor))
            {
                Entrada entrada = new Entrada();
                entrada.PrecioGeneral = PrecioGen;
                entrada.PrecioNino = PrecioNino;
                entrada.PrecioMayores = PrecioMayor;
                entrada.Sala = sala;
                entrada.Usuario = Convert.ToInt32(Session["id_Usuario"]);
                string error = null;
                entradaBl.RegistrarEntrada(entrada, ref error);
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
            ConsultarEntradas();
        }

        public void ModificarEntrada()
        {

            EntradaBL entradaBl = new EntradaBL();
            TipoSala sala = new TipoSala();
            sala.Id = Convert.ToInt32(DolistSala.SelectedItem.Value);

            decimal precioGen;
            decimal precioNino;
            decimal precioMayor;

            if (decimal.TryParse(TxtPrecioGeneral.Text, out precioGen) && decimal.TryParse(TxtPrecioNino.Text, out precioNino)
                && decimal.TryParse(TxtPrecioMayores.Text, out precioMayor) && precioGen > 0 &&
                precioNino > 0 && precioMayor > 0)

            {
                Entrada entrada = new Entrada();
                entrada.Id = Convert.ToInt32(ViewState["id_Entrada"]);
                entrada.PrecioGeneral = precioGen;
                entrada.PrecioNino = precioNino;
                entrada.PrecioMayores = precioMayor;
                entrada.Sala = sala;
                entrada.Usuario = Convert.ToInt32(Session["id_Usuario"]);


                string error = null;
                entradaBl.ModificarEntrada(entrada, ref error);
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
            ConsultarEntradas();
        }

    }
}
