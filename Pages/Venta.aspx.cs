using MovieCenter.Data;
using MovieCenter.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

namespace MovieCenter.Pages
{
    public partial class Venta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Session["butacas_Seleccionadas"] = new List<int>();
                ViewState["butacas"] = new List<string>();
                ConsultarButacas();
                MostrarPrecios();
            }

            if (Session["lst_Botones"] != null)
            {
                List<Button> lista = (List<Button>)Session["lst_Botones"];
                foreach (var btn in lista)
                {
                    btn.Click += new EventHandler(Btn_Click);
                    UpPanel.ContentTemplateContainer.Controls.Add(btn);
                }
            }
        }

        public void Mensaje(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{mensaje}')", true);
        }

        protected void BtnPagar_Click(object sender, EventArgs e)
        {
            GenerarFactura();
        }

        public void RegistrarVentas()
        {
            string error = null;
            MovieCenter.Data.Venta vnta = new MovieCenter.Data.Venta();
            Cliente client = Session["cliente"] as Cliente;
            vnta.Cliente = client.Id;
            Sesion sesion = Session["sesion"] as Sesion;
            vnta.Sesion = sesion.Id;

            List<Entrada> listaEntradas = new EntradaBL().ConsultarEntradas(ref error);
            List<Promocion> listaPromociones = new PromocionBL().ConsultarPromociones(ref error);

            int entradasGeneral = Convert.ToInt32(Session["entradas_General"]);
            int entradasNino = Convert.ToInt32(Session["entradas_Nino"]);
            int entradasMayor = Convert.ToInt32(Session["entradas_Mayor"]);

            TipoSala sala = Session["sala"] as TipoSala;
            string dia = Session["dia"].ToString();
            List<int> lstButacas = Session["butacas_Seleccionadas"] as List<int>;
            int indicePromo = listaPromociones.FindIndex(x => x.Sala.Id == sala.Id && x.Dia == dia);
            bool esBonificacion = Convert.ToBoolean(ViewState["bonificacion"]);
            int totalEntradas = Convert.ToInt32(Session["total_Entradas"]);
            if (indicePromo >= 0)
            {

                for (int i = 0; i < lstButacas.Count; i++)
                {
                    vnta.Butaca = lstButacas[i];
                    vnta.Precio = listaPromociones[indicePromo].Precio;
                    new VentaBL().InsertarVenta(vnta, ref error);
                    if (!esBonificacion && totalEntradas > 2)
                    {
                        client.Puntos++;
                        new ClienteBL().ActualizarPuntaje(client, ref error);
                        Session["cliente"] = client;
                    }
                }
                Session["total_Entradas"] = 0;
            }
            else
            {
                int indiceEntrada = listaEntradas.FindIndex(x => x.Sala.Id == sala.Id);
                for (int i = 0; i < lstButacas.Count; i++)
                {
                    vnta.Butaca = lstButacas[i];
                    if (entradasGeneral > 0)
                    {
                        vnta.Precio = listaEntradas[indiceEntrada].PrecioGeneral;
                        new VentaBL().InsertarVenta(vnta, ref error);
                        entradasGeneral--;
                        if (!esBonificacion && totalEntradas > 2)
                        {
                            client.Puntos++;
                            new ClienteBL().ActualizarPuntaje(client, ref error);
                            Session["cliente"] = client;
                        }
                    }
                    else if (entradasMayor > 0)
                    {
                        vnta.Precio = listaEntradas[indiceEntrada].PrecioMayores;
                        new VentaBL().InsertarVenta(vnta, ref error);
                        entradasMayor--;
                        if (!esBonificacion && totalEntradas > 2)
                        {
                            client.Puntos++;
                            new ClienteBL().ActualizarPuntaje(client, ref error);
                            Session["cliente"] = client;
                        }
                    }
                    else if (entradasNino > 0)
                    {
                        vnta.Precio = listaEntradas[indiceEntrada].PrecioMayores;
                        new VentaBL().InsertarVenta(vnta, ref error);
                        entradasNino--;
                        if (!esBonificacion && totalEntradas > 2)
                        {
                            client.Puntos++;
                            new ClienteBL().ActualizarPuntaje(client, ref error);
                            Session["cliente"] = client;
                        }
                    }

                }
                Session["total_Entradas"] = 0;

            }

        }

        public void GenerarFactura()
        {

            List<int> list = Session["butacas_Seleccionadas"] as List<int>;
            int entradas = Convert.ToInt32(Session["total_Entradas"]);
            if (list.Count == entradas)
            {
                try
                {
                    RegistrarVentas();

                    Cliente client = Session["cliente"] as Cliente;

                    int entradasGeneral = Convert.ToInt32(Session["entradas_General"]);
                    int entradasNino = Convert.ToInt32(Session["entradas_Nino"]);
                    int entradasMayor = Convert.ToInt32(Session["entradas_Mayor"]);
                    decimal dolares = Convert.ToDecimal(ViewState["dolares"]);
                    decimal colones = Convert.ToDecimal(ViewState["colones"]);
                    TipoSala tipoSala = Session["sala"] as TipoSala;
                    string peli = Session["pelicula"].ToString();
                    string clasificacion = Session["clasificacion"].ToString();
                    string hora = Session["horario"].ToString();
                    string dia = Session["dia"].ToString();
                    Sesion sesion = Session["sesion"] as Sesion;

                    string butacas = "";
                    List<string> lstButacas = ViewState["butacas"] as List<string>;
                    for (int i = 0; i < lstButacas.Count; i++)
                    {
                        if (i == lstButacas.Count - 1)
                        {
                            butacas += $"{lstButacas[i]}";
                        }
                        else
                        {
                            butacas += $"{lstButacas[i]}-";
                        }

                    }

                    string factura = $"Factura MovieCenter\nCliente: {client.Id} {client.Nombre} {client.Apellido1} {client.Apellido2}" +
                        $"\nPelicula: {peli}\nClasificacion: {clasificacion}\n" +
                        $"Sala: {sesion.SalaId}-{tipoSala.Clasificacion}\n" + $"Butacas: {butacas}\n" +
                        $"Dia: {dia}\nHorario: {hora}\n" +
                        $"Entradas General: {entradasGeneral}\nEntradas Niño: {entradasNino}" +
                        $"\nEntradas Mayor: {entradasMayor}\nTotal Colones: {colones}\n" +
                        $"Total Dolares: {dolares}";

                    using (PdfDocument document = new PdfDocument())
                    {
                        //Add a page to the document
                        PdfPage page = document.Pages.Add();

                        //Create PDF graphics for the page
                        PdfGraphics graphics = page.Graphics;

                        //Set the standard font
                        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                        //Draw the text
                        graphics.DrawString(factura, font, PdfBrushes.Black, new PointF(0, 0));

                        // Open the document in browser after saving it
                        document.Save("Factura.pdf", HttpContext.Current.Response, HttpReadType.Save);
                    }

                }
                catch (Exception e)
                {
                    Mensaje(e.Message);

                }
                Response.Redirect("Peliculas.aspx");

            }
            else
            {
                Mensaje("Antes de proceder a la compra debe de seleccionar todas las butacas que indico");
            }

        }

        public void ConsultarButacas()
        {
            string error = null;
            TipoSala sala = Session["sala"] as TipoSala;
            UpPanel.ContentTemplateContainer.Controls.Clear();

            List<Butaca> listaButacas = new ButacaBL().ConsultarButacas(sala, ref error);
            if (error == null)
            {
                List<Button> botones = new List<Button>();

                foreach (Butaca li in listaButacas)
                {
                    Button btn = new Button();
                    btn.Text = $"{li.Fila}{li.Columna}";
                    btn.ID = $"{li.Id}";

                    btn.Click += Btn_Click;

                    btn.Width = 30;
                    btn.BorderStyle = BorderStyle.None;
                    Sesion sesion = Session["sesion"] as Sesion;
                    List<Butaca> butacasVendidas = new VentaBL().ConsultarButacasVendidas(sesion, ref error);
                    if (butacasVendidas.Exists(butacaVendida => butacaVendida.Id == li.Id))
                    {
                        btn.BackColor = System.Drawing.Color.Red;
                        btn.Enabled = false;
                    }
                    else
                    {
                        List<int> butacasSeleccionados = Session["butacas_Seleccionadas"] as List<int>;
                        if (butacasSeleccionados.Exists(x => x == li.Id))
                        {
                            btn.BackColor = System.Drawing.Color.Yellow;
                        }
                        else
                        {
                            btn.BackColor = System.Drawing.Color.Green;
                        }

                    }
                    UpPanel.ContentTemplateContainer.Controls.Add(btn);
                    ScriptManager1.RegisterAsyncPostBackControl(btn);
                    botones.Add(btn);

                }
                Session["lst_Botones"] = botones;
            }
        }

        public void Btn_Click(object sender, EventArgs e)
        {

            List<int> list = Session["butacas_Seleccionadas"] as List<int>;
            List<string> list2 = ViewState["butacas"] as List<string>;
            Button btnSeleccionado = (Button)sender;
            int id = Convert.ToInt32(btnSeleccionado.ID);
            string txt = btnSeleccionado.Text;
            if (!list.Remove(id) || !list2.Remove(txt))
            {
                int entradas = Convert.ToInt32(Session["total_Entradas"]);
                if (list.Count < entradas)
                {
                    list.Add(id);
                    list2.Add(txt);
                }
                else
                {
                    Mensaje("No puede seleccionar mas Butacas de las que indico en la pagina anterior");

                }
            }

            ConsultarButacas();
        }

        public void MostrarPrecios()
        {
            string error = null;

            int totalEntradas = Convert.ToInt32(Session["total_Entradas"]);
            LblTotalEntradas.Text = $"Total de Entradas: {totalEntradas}";

            int entradasGeneral = Convert.ToInt32(Session["entradas_General"]);
            LblEntradasGeneral.Text = $"General: {entradasGeneral}";

            int entradasNino = Convert.ToInt32(Session["entradas_Nino"]);
            LblEntradasNino.Text = $"Niños: {entradasNino}";

            int entradasMayor = Convert.ToInt32(Session["entradas_Mayor"]);
            LblEntradasMayor.Text = $"Adulto Mayor: {entradasMayor}";

            TipoSala sala = Session["sala"] as TipoSala;
            string dia = Session["dia"].ToString();

            List<Entrada> listaEntradas = new EntradaBL().ConsultarEntradas(ref error);
            List<Promocion> listaPromociones = new PromocionBL().ConsultarPromociones(ref error);

            decimal tipoCambio = Convert.ToDecimal(Session["tipo_Cambio"]);
            decimal totalPagar;
            int indicePromo = listaPromociones.FindIndex(x => x.Sala.Id == sala.Id && x.Dia == dia);
            if (indicePromo >= 0)
            {
                totalPagar = listaPromociones[indicePromo].Precio * totalEntradas;
                ViewState["colones"] = Math.Round(totalPagar, 2);
                LblTotalColones.Text = $"Total a Pagar Colones: {totalPagar}";
                decimal dolares = Math.Round(totalPagar / tipoCambio, 2);
                LblTotalDolares.Text = $"Total a Pagar Dolares: {dolares}";
                ViewState["dolares"] = dolares;
            }
            else
            {
                int indiceEntrada = listaEntradas.FindIndex(x => x.Sala.Id == sala.Id);
                totalPagar = listaEntradas[indiceEntrada].PrecioGeneral * entradasGeneral;
                totalPagar += listaEntradas[indiceEntrada].PrecioNino * entradasNino;
                totalPagar += listaEntradas[indiceEntrada].PrecioMayores * entradasMayor;
                ViewState["colones"] = totalPagar;
                LblTotalColones.Text = $"Total a Pagar Colones: {totalPagar}";
                decimal dolares = Math.Round(totalPagar / tipoCambio, 2);
                LblTotalDolares.Text = $"Total a Pagar Dolares: {dolares}";
                ViewState["dolares"] = dolares;
            }

        }



        protected void Btn2D_Click(object sender, EventArgs e)
        {
            string error = null;
            Bonificacion boni = new BonificacionBL().ConsultarBonificacion(ref error);
            Cliente client = Session["cliente"] as Cliente;

            if (client.Puntos >= boni.Puntos)
            {
                int totalEntradas = Convert.ToInt32(Session["total_Entradas"]);
                if (totalEntradas == 2)
                {
                    Session["butacas_Seleccionadas"] = new List<int>();

                    ViewState["butacas"] = new List<string>();
                    ConsultarButacas();
                    List<Button> listaBotones = (List<Button>)Session["lst_Botones"];
                    List<int> bSeleccionadas = new List<int>();
                    List<string> bSeleccionadasTxt = new List<String>();
                    int i = 0;
                    while (i < 2)
                    {
                        foreach (var li in listaBotones)
                        {
                            if (li.BackColor.Equals(System.Drawing.Color.Green) && i < 2)
                            {
                                bSeleccionadas.Add(Convert.ToInt32(li.ID));
                                bSeleccionadasTxt.Add(li.Text);
                                i++;

                            }
                        }
                    }
                    Session["butacas_Seleccionadas"] = bSeleccionadas;
                    ViewState["butacas"] = bSeleccionadasTxt;
                    client.Puntos -= boni.Puntos;
                    client.Canjes += 1;
                    new ClienteBL().ActualizarPuntaje(client, ref error);
                    ViewState["bonificacion"] = true;
                    Session["cliente"] = client;

                    GenerarFactura();
                }
                else
                {
                    Mensaje("La cantidad de entradas seleccionada es mayor a la recompensa otorgada de la bonificacion 2D");
                }

            }
            else
            {
                Mensaje("No cumple los requisitos");
            }
        }

        protected void BtnIMAX_Click(object sender, EventArgs e)
        {
            string error = null;
            Bonificacion boni = new BonificacionBL().ConsultarBonificacion(ref error);
            Cliente client = Session["cliente"] as Cliente;

            if (client.Canjes >= boni.Canjes)
            {
                int totalEntradas = Convert.ToInt32(Session["total_Entradas"]);
                if (totalEntradas == 4)
                {
                    Session["butacas_Seleccionadas"] = new List<int>();

                    ViewState["butacas"] = new List<string>();
                    ConsultarButacas();
                    List<Button> listaBotones = (List<Button>)Session["lst_Botones"];
                    List<int> bSeleccionadas = new List<int>();
                    List<string> bSeleccionadasTxt = new List<String>();
                    int i = 0;
                    while (i < 4)
                    {
                        foreach (var li in listaBotones)
                        {
                            if (li.BackColor.Equals(System.Drawing.Color.Green) && i < 4)
                            {
                                bSeleccionadas.Add(Convert.ToInt32(li.ID));
                                bSeleccionadasTxt.Add(li.Text);
                                i++;

                            }
                        }
                    }
                    Session["butacas_Seleccionadas"] = bSeleccionadas;
                    ViewState["butacas"] = bSeleccionadasTxt;
                    client.Canjes -= boni.Canjes;
                    new ClienteBL().ActualizarPuntaje(client, ref error);
                    ViewState["bonificacion"] = true;
                    Session["cliente"] = client;

                    GenerarFactura();
                }
                else
                {
                    Mensaje("La cantidad de entradas seleccionada es mayor o menor a la recompensa otorgada de la bonificacion IMAX");
                }

            }
            else
            {
                Mensaje("No cumple los requisitos");
            }
        }
    }
}