<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Promociones.aspx.cs" Inherits="MovieCenter.Pages.Promociones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <h1 class="mantenimiento__title">Promociones</h1>

     <asp:GridView CssClass="mantenimiento__table" ID="GrdPromociones" runat="server" AutoGenerateColumns="false" PageSize="20"
            DataKeyNames="Id" OnSelectedIndexChanged="GrdPromociones_SelectedIndexChanged" OnRowDeleting="GrdPromociones_RowDeleting">

            <Columns >
                <asp:BoundField DataField="Id"  HeaderStyle-CssClass="table__item--hidden" ItemStyle-CssClass="table__item--hidden"
                    ControlStyle-CssClass="table__item--hidden"/>
                <asp:BoundField DataField="Sala.Id"  HeaderStyle-CssClass="table__item--hidden" ItemStyle-CssClass="table__item--hidden"
                    ControlStyle-CssClass="table__item--hidden"/>
                <asp:BoundField DataField="Sala.clasificacion" HeaderText="Sala" ItemStyle-CssClass="table__item"/>
                <asp:BoundField DataField="Precio" HeaderText="Precio" ItemStyle-CssClass="table__item"/>
                 <asp:BoundField DataField="Dia" HeaderText="Dia" ItemStyle-CssClass="table__item"/>
                <asp:CommandField ShowSelectButton="true" HeaderStyle-Width="60px" ItemStyle-ForeColor="Black" />
              <asp:CommandField ShowDeleteButton="true"  ButtonType="Button"/>
            </Columns>
        </asp:GridView>
  <div class="box">
     <div class="mantenimiento__form">
        <asp:TextBox ID="TxtPrecio" runat="server">Precio</asp:TextBox>
        <asp:DropDownList ID="DolistDia" runat="server" ></asp:DropDownList>
        <asp:DropDownList ID="DolistSala" runat="server" DataTextField="Clasificacion" DataValueField="Id"></asp:DropDownList>
        <div>
            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="button button--action" OnClick="BtnGuardar_Click"/>
            <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="button button--action" OnClick="BtnNuevo_Click"/>
        </div>
    </div>
  </div>
     
</asp:Content>
