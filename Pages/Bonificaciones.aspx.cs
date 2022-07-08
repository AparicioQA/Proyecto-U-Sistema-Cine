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
    public partial class Bonificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ConsultarBonificaciones();
                MostrarPuntajesUsuario();
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
        
        public void ConsultarBonificaciones()
        {
            string error = null;
            BonificacionBL bonificacionBl = new BonificacionBL();

            Bonificacion bonificacion = bonificacionBl.ConsultarBonificacion(ref error);

            if (error == null)
            {
                ViewState["id_Bonificacion"] = bonificacion.Id;
                TxtPuntos.Text = bonificacion.Puntos.ToString();
                TxtCanjes.Text = bonificacion.Canjes.ToString();

            }
            else
            {
                Mensaje(error);
            }
        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            
            ModificarBonificaciones();
            
        }
        public void ModificarBonificaciones()
        {

           BonificacionBL bonificacionBl = new BonificacionBL();

            int puntos;
            int canjes;

            if (int.TryParse(TxtPuntos.Text, out puntos) && int.TryParse(TxtCanjes.Text, out canjes) &&
                puntos > 0 && canjes > 0)
            {
                Bonificacion bonificacion = new Bonificacion();
                bonificacion.Id = Convert.ToInt32(ViewState["id_Bonificacion"]);
                bonificacion.Puntos = puntos;
                bonificacion.Canjes = canjes;
                bonificacion.Usuario = Convert.ToInt32(Session["id_Usuario"]);

                string error = null;
                new BonificacionBL().ModificarBonificacion(bonificacion, ref error);
                if (error != null)
                {
                    Mensaje(error);
                }
            }
            else
            {
                Mensaje("Ingrese la informacion en los campos de texto, no se admite caracteres de tipo " +
                    "alfanumerico en los campos de texto destinados para numeros. Los puntos y Canjes deben ser mayores a 0");
            }
            ConsultarBonificaciones();
        }

        public void MostrarPuntajesUsuario()
        {
            Cliente cliente = Session["cliente"] as Cliente;
            LblCanjes.Text = $"Canjes Realizados: {cliente.Canjes}";
            LblPuntos.Text = $"Puntos Obtenidos: {cliente.Puntos}";
            ModificarBonificaciones();
            if (cliente.Puntos >= Convert.ToInt32(TxtPuntos.Text))
            {
                LblMensajePuntos.Text = "Felicidades, Tiene una Bonificación 2D Disponible";
            }
            else
            {
                LblMensajePuntos.Text = $"No aplica para la Bonificación, Requiere {TxtPuntos.Text} Puntos";
            }

            if (cliente.Canjes >= Convert.ToInt32(TxtCanjes.Text))
            {
                LblMensajeCanjes.Text = "Felicidades, Tiene una Bonificación IMAX Disponible";
            }
            else
            {
                LblMensajeCanjes.Text = $"No aplica para la Bonificación, Requiere {TxtCanjes.Text} Canjes";
            }

        }

    }
}