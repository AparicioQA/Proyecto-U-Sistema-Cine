<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Peliculas.aspx.cs" Inherits="MovieCenter.Pages.Peliculas" 
    Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="boxPeliculas">
       <asp:Repeater ID="RepPeliculas" runat="server" OnItemCommand="RepPeliculas_ItemCommand">
       <ItemTemplate>
            <div class="box3">
                <p class="mantenimiento__title"> <%#  Eval("Nombre") %></p>
              
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Selecciona" 
                      CommandArgument='<%# Eval("Id") %>'>

                      <img src="<%# Eval("Imagen")%>" class="imgPelicula"/>
                </asp:LinkButton>
              
                 <asp:Button ID="BtnSeleccionar" runat="server" Text="Seleccionar" EnableTheming="true" CssClass="button button--action" CommandName="Click" 
                      CommandArgument='<%# Eval("Id") %>'/>
                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" EnableTheming="true" CssClass="button button--action"  CommandName="Delete" 
                      CommandArgument='<%# Eval("Id") %>' />
            </div>
       </ItemTemplate>
  </asp:Repeater>  
  </div>
  <div class="box4">
       <div class="box5">
        <asp:TextBox type="text" ID="TxtTitulo" runat="server" CssClass="box5__item" placeholder="Ingrese el titulo" ></asp:TextBox>
    <asp:TextBox ID="TxtSinopsis" runat="server" CssClass="box5__item textarea" placeholder="Ingrese la sinopsis" TextMode="MultiLine"></asp:TextBox>
    <asp:TextBox ID="TxtFecha" runat="server" TextMode="Date" CssClass="box5__item"></asp:TextBox>
   </div>
    <div class="box6">
          <div class="box6__item">
              <p class="mantenimiento__title">Salas</p>
        <asp:RadioButtonList ID="RbtnlstSalas"  runat="server" OnSelectedIndexChanged="RbtnlstSalas_SelectedIndexChanged" AutoPostBack="true" ></asp:RadioButtonList>
          </div>
       <div class="box6__item">
                 <p class="mantenimiento__title">Horarios</p>
        <asp:CheckBoxList ID="CbxListHorarios" runat="server"></asp:CheckBoxList>
       </div>
           <div class="box6__item">
                 <p class="mantenimiento__title">Categorias</p>
        <asp:RadioButtonList ID="RbtnlstCategorias" runat="server"></asp:RadioButtonList>
           </div>
    </div>
  </div>
    <div class="box">
        <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" EnableTheming="true" CssClass="button button--action" OnClick="BtnAceptar_Click"/>
         <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" EnableTheming="true" CssClass="button button--action" OnClick="BtnNuevo_Click"/>
    </div>

</asp:Content>
