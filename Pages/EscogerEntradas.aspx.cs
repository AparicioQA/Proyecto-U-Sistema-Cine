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
    public partial class EscogerEntradas: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ConsultarPelicula();
                ConsultarSalas();
                ConsultarHorarios();
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

        protected void RbtnlstSalas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultarHorarios();
        }

        protected void ConsultarPelicula()
        {
            string error = null;
            PeliculaBL pelibl = new PeliculaBL();
            Pelicula peli = new Pelicula();
            peli.Id = Convert.ToInt32(Session["id_Pelicula"]);

            Pelicula pelicula = pelibl.ConsultarPelicula(peli, ref error);
            if (error != null)
            {
                Mensaje(error);
            }
            else
            {
                LblFecha.Text = pelicula.Fecha.ToString("yyyy-MM-dd");

                LblSinopsis.Text = pelicula.Sinopsis;
                LblTitulo.Text = pelicula.Nombre;
                LblClasificacion.Text = pelicula.CategoriaP.Clasificacion;

            }
        }

        public void ConsultarSalas()
        {
            string error = null;

            SesionBL sesionbl = new SesionBL();
            int pelicula = Convert.ToInt32(Session["id_Pelicula"]);
            List<TipoSala> lista = sesionbl.ConsultarSesionesSalas(pelicula, ref error);
            ViewState["lstSalas"] = lista;
            if (error != null)
            {
                Mensaje(error);
            }
            else
            {
                foreach (var li in lista)
                {
                    RbtnlstSalas.Items.Add(new ListItem(li.Clasificacion, li.Id.ToString()));
                }
                if (RbtnlstSalas.Items.Count > 0)
                {
                    RbtnlstSalas.SelectedIndex = 0;
                }
            }
        }

        public void ConsultarHorarios()
        {
            string error = null;

            SesionBL sesionbl = new SesionBL();
            int pelicula = Convert.ToInt32(Session["id_Pelicula"]);
            if (!string.IsNullOrWhiteSpace(RbtnlstSalas.SelectedValue))
            {
                int sala = Convert.ToInt32(RbtnlstSalas.SelectedValue);

                List<Horario> lista = sesionbl.ConsultarSesionesHorarios(pelicula, sala, ref error);

                if (error != null)
                {
                    Mensaje(error);
                }
                else
                {
                    RbtnlstHorarios.Items.Clear();
                    foreach (var li in lista)
                    {
                        RbtnlstHorarios.Items.Add(new ListItem(li.Hora.ToString(@"hh\:mm"), li.Id.ToString()));
                    }
                    if (RbtnlstHorarios.Items.Count > 0)
                    {
                        RbtnlstHorarios.SelectedIndex = 0;
                    }
                }
            }

        }

        protected void Continuar_Click(object sender, EventArgs e)
        {
            ContinuarCompra();
        }

        public void ContinuarCompra()
        {
            string error = null;

            int general, ninos, mayor;
            if(!RbtnlstDias.SelectedValue.Equals("") && RbtnlstSalas.Items.Count > 0 
                && RbtnlstHorarios.Items.Count > 0)
           {
                if (int.TryParse(TxtGeneral.Text, out general) && int.TryParse(TxtMayores.Text, out mayor) &&
           int.TryParse(TxtNinos.Text, out ninos))
                {
                    List<TipoSala> lista = ViewState["lstSalas"] as List<TipoSala>;
                    TipoSala sala = lista.Find(x => x.Id == Convert.ToInt32(RbtnlstSalas.SelectedValue));

                    int pelicula = Convert.ToInt32(Session["id_Pelicula"]);
                    int horario = Convert.ToInt32(RbtnlstHorarios.SelectedValue);

                    SesionBL sesionbl = new SesionBL();
                    Sesion sesion = sesionbl.Consultar(sala.Id, pelicula, horario, ref error);
                    if (error == null)
                    {
                        int CapacidadDisponible = sala.Capacidad - sesion.Ventas;
                        int totalEntradas = general + ninos + mayor;
                        if (totalEntradas <= CapacidadDisponible)
                        {
                            if (totalEntradas > 0)
                            {
                                Session["total_Entradas"] = totalEntradas;
                                Session["entradas_General"] = general;
                                Session["entradas_Nino"] = ninos;
                                Session["entradas_Mayor"] = mayor;
                                Session["sesion"] = sesion;
                                Session["sala"] = sala;
                                Session["pelicula"] = LblTitulo.Text;
                                Session["clasificacion"] = LblClasificacion.Text;
                                Session["horario"] = RbtnlstHorarios.SelectedItem.Text;
                                Session["dia"] = RbtnlstDias.SelectedItem.Text;
                                
                                Response.Redirect("Venta.aspx");
                            }
                            else
                            {
                                Mensaje("La cantidad de entradas deseada debe ser mayor a 0!");
                            }
                        }
                        else
                        {
                            Mensaje("La cantidad de entradas deseada es mayor a la cantidad de butacas disponibles!");
                        }
                    }
                    else
                    {
                        Mensaje(error);

                    }
                }
                else
                {
                    Mensaje("Por favor llenar los campos. Solo se permiten numeros en los campos de texto");
                }
            }
            else
            {
                Mensaje("En caso de que no exista un horario asignado alguna de la salas entonces no podra continuar");
            }

            

        }

    }
}