using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCenter.Data;
using MovieCenter.Logic;
using MovieCenter.Pages;

namespace MovieCenter.Pages
{
    public partial class Peliculas : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                ViewState["modo"] = "I";

                CbxListHorarios.Enabled = false;
                RbtnlstSalas.Enabled = false;
               
                ConsultarHorarios();
                ConsultarPeliculas();
                ConsultarSalas();
                ConsultarCategorias();
                Cliente client = Session["cliente"] as Cliente;
                if (client.UsuarioP.Rol.Mantenimientos[5].Permisos[1].Estado == false)
                {
                    BtnNuevo.Enabled = false;
                    ViewState["insertar"] = false;
                    TxtFecha.CssClass = "table__item--hidden";
                    TxtSinopsis.CssClass = "table__item--hidden";
                    TxtTitulo.CssClass = "table__item--hidden";
                    RbtnlstCategorias.CssClass = "table__item--hidden";
                    RbtnlstSalas.CssClass = "table__item--hidden";
                    CbxListHorarios.CssClass = "table__item--hidden";
                } 
                if (client.UsuarioP.Rol.Mantenimientos[5].Permisos[2].Estado == false) {

                    ViewState["editar"] = false;
                   
                    TxtFecha.CssClass = "table__item--hidden";
                    TxtSinopsis.CssClass = "table__item--hidden";
                    TxtTitulo.CssClass = "table__item--hidden";
                    RbtnlstCategorias.CssClass = "table__item--hidden";
                    RbtnlstSalas.CssClass = "table__item--hidden";
                    CbxListHorarios.CssClass = "table__item--hidden";

                }
                 if (client.UsuarioP.Rol.Mantenimientos[5].Permisos[3].Estado == false)
                {
                    ViewState["eliminar"] = false;
               }
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

        public void ConsultarPeliculas()
        {
            string error = null;
            PeliculaBL pelibl = new PeliculaBL();
            List<Pelicula> pelis = pelibl.ConsultarPeliculas(ref error);

            if (error != null)
            {
                Mensaje(error);
            }
            RepPeliculas.DataSource = null;
            RepPeliculas.DataBind();
            RepPeliculas.DataSource = pelis;
            RepPeliculas.DataBind();
        }


        protected async void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (ViewState["modo"].ToString() == "I")
            {
                await Registrar();
                Limpiar();
            }
            else if (ViewState["modo"].ToString() == "A" && ViewState["editar"] == null)
            {
                await Actualizar();
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            ViewState["modo"] = "I";
            Limpiar();
        }

        public void ConsultarSalas()
        {
            string error = null;
            SalaBL salabl = new SalaBL();
            List<Sala> salas = salabl.ConsultarSalas(ref error);

            if (error != null)
            {
                Mensaje(error);
            }
            RbtnlstSalas.Items.Clear();
            foreach (var li in salas)
            {
                RbtnlstSalas.Items.Add(new ListItem(li.Tipo.Clasificacion, li.Id.ToString()));
            }
            RbtnlstSalas.SelectedIndex = 0;
            ViewState["id_Sala"] = RbtnlstSalas.SelectedValue;

        }

        public void ConsultarHorarios()
        {
            string error = null;
            HorarioBL hoariobl = new HorarioBL();
            List<Horario> horarios;
            if (ViewState["modo"].ToString() == "A")
            {
                int peli = Convert.ToInt32(ViewState["id_Pelicula"]);
                int sala = Convert.ToInt32(ViewState["id_Sala"]);

                horarios = new SesionBL().ConsultarSesiones(peli, sala, ref error);
            }
            else
            {
                horarios = hoariobl.ConsultarHorarios(ref error);
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                Mensaje(error);
            }
            else
            {
                CbxListHorarios.Items.Clear();

                for (int i = 0; i < horarios.Count; i++)
                {
                    CbxListHorarios.Items.Add(new ListItem(horarios[i].Hora.ToString(@"hh\:mm"),
                        horarios[i].Id.ToString()));
                    CbxListHorarios.Items[i].Selected = horarios[i].Estado;
                }

            }

        }

        public void ConsultarCategorias(int seleccionada = 0)
        {
            string error = null;
            CategoriaBL categoriabl = new CategoriaBL();
            List<Categoria> horarios = categoriabl.ConsultarCategorias(ref error);

            if (!string.IsNullOrWhiteSpace(error))
            {
                Mensaje(error);
            }
            else
            {
                RbtnlstCategorias.Items.Clear();

                foreach (var li in horarios)
                {
                    RbtnlstCategorias.Items.Add(new ListItem(li.Clasificacion, li.Id.ToString()));
                }
                if (seleccionada == 0)
                {
                    RbtnlstCategorias.SelectedIndex = 0;
                }
                else
                {
                    RbtnlstCategorias.SelectedValue = seleccionada.ToString();
                }

            }

        }

        public async Task Registrar()
        {
            string error = null;
            PeliculaBL pelibl = new PeliculaBL();
            Pelicula peli = new Pelicula();
            DateTime fechaex;
            DataTable responseObj = new DataTable();
            if (DateTime.TryParse(TxtFecha.Text, out fechaex) && !string.IsNullOrWhiteSpace(TxtTitulo.Text) &&
                !string.IsNullOrWhiteSpace(TxtSinopsis.Text)  && ViewState["insertar"] == null)
            {

                string imgUrl = await ObtenerImagenURL();
                peli.Imagen = imgUrl;
                peli.Fecha = fechaex;
                peli.Nombre = TxtTitulo.Text;
                peli.Sinopsis = TxtSinopsis.Text;
                peli.Usuario = Convert.ToInt32(Session["id_Usuario"]);
                peli.CategoriaP = new Categoria { Id = Convert.ToInt32(RbtnlstCategorias.SelectedValue) };
                pelibl.InsertarPelicula(peli, ref error);
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
            ConsultarPeliculas();

        }

        public async Task<string> ObtenerImagenURL()
        {
            string url = "https://imdb-api.com/es/API/SearchTitle/k_62xci328/" + TxtTitulo.Text;
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                JsonDocument doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;
                var pelis = root.GetProperty("results");
                if (pelis.GetArrayLength() > 0)
                {
                    return pelis[0].GetProperty("image").ToString();
                }
            }
            return "";
        }

        public void Eliminar()
        {
            string error = null;
            PeliculaBL pelibl = new PeliculaBL();
            Pelicula peli = new Pelicula();
            peli.Usuario = Convert.ToInt32(Session["id_Usuario"]);
            peli.Id = Convert.ToInt32(ViewState["id_Pelicula"]);
            pelibl.EliminarPelicula(peli, ref error);
            if (error != null)
            {
                Mensaje(error);
            }
            ConsultarPeliculas();
        }

        protected void RepPeliculas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Click":

                    ViewState["id_Pelicula"] = id;
                    ViewState["modo"] = "A";
                    RbtnlstSalas.Enabled = true;
                    CbxListHorarios.Enabled = true;
                    ConsultarPelicula();

                    break;
                case "Delete":
                    ViewState["id_Pelicula"] = id;
                    if(ViewState["eliminar"] == null)
                    {
                        Eliminar();
                    }
                       

                    break;
                case "Selecciona":
                    Session["id_Pelicula"] = id;

                    Response.Redirect("EscogerEntradas.aspx");

                    break;
            }
        }
        public async Task Actualizar()
        {
            string error = null;
            PeliculaBL pelibl = new PeliculaBL();
            Pelicula peli = new Pelicula();
            DateTime fechaex;

            if (DateTime.TryParse(TxtFecha.Text, out fechaex) && !string.IsNullOrWhiteSpace(TxtTitulo.Text) &&
                !string.IsNullOrWhiteSpace(TxtSinopsis.Text))
            {
                string imgUrl = await ObtenerImagenURL();
                peli.Imagen = imgUrl;
                peli.Id = Convert.ToInt32(ViewState["id_Pelicula"]);
                peli.Fecha = fechaex;
                peli.Nombre = TxtTitulo.Text;
                peli.Sinopsis = TxtSinopsis.Text;
                peli.Usuario = Convert.ToInt32(Session["id_Usuario"]);
                peli.CategoriaP = new Categoria { Id = Convert.ToInt32(RbtnlstCategorias.SelectedValue) };
                pelibl.ModificarPelicula(peli, ref error);

                int sala = Convert.ToInt32(RbtnlstSalas.SelectedValue);

                for (int i = 0; i < CbxListHorarios.Items.Count; i++)
                {
                    new SesionBL().ModificarSesion(peli.Id, sala, Convert.ToInt32(CbxListHorarios.Items[i].Value),
                          CbxListHorarios.Items[i].Selected, ref error);

                }

                if (error != null)
                {
                    Mensaje(error);
                }
                else
                {
                    ConsultarPelicula();
                }
            }
            else
            {
                Mensaje("Ingrese la informacion en los campos de texto, no se admite caracteres de tipo " +
                    "alfanumerico en los campos de texto destinados para numeros");
            }
            ConsultarPeliculas();
        }

        public void Limpiar()
        {
            ViewState["modo"] = "I";
            CbxListHorarios.Enabled = false;
            RbtnlstSalas.Enabled = false;
            TxtFecha.Text = "";
            TxtSinopsis.Text = "";
            TxtTitulo.Text = "";
        }

        protected void RbtnlstSalas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["id_Sala"] = RbtnlstSalas.SelectedValue;
            ConsultarHorarios();
        }

        protected void ConsultarPelicula()
        {
            string error = null;
            PeliculaBL pelibl = new PeliculaBL();
            Pelicula peli = new Pelicula();
            peli.Id = Convert.ToInt32(ViewState["id_Pelicula"]);

            Pelicula pelicula = pelibl.ConsultarPelicula(peli, ref error);
            if (error != null)
            {
                Mensaje(error);
            }
            else
            {
                TxtFecha.Text = pelicula.Fecha.ToString("yyyy-MM-dd");
                TxtSinopsis.Text = pelicula.Sinopsis;
                TxtTitulo.Text = pelicula.Nombre;

                ConsultarCategorias(pelicula.CategoriaP.Id);
                ConsultarHorarios();
            }
        }
    }
}