<%@ Page Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Entradas.aspx.cs" Inherits="MovieCenter.Pages.Entradas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <h1 class="mantenimiento__title">Entradas</h1>

     <asp:GridView CssClass="mantenimiento__table" ID="GrdEntradas" runat="server" AutoGenerateColumns="false" PageSize="20"
            DataKeyNames="Id" OnSelectedIndexChanged ="GrdEntradas_SelectedIndexChanged" OnRowDeleting ="GrdEntradas_RowDeleting">

            <Columns >
                <asp:BoundField DataField="Id"  HeaderStyle-CssClass="table__item--hidden" ItemStyle-CssClass="table__item--hidden"
                    ControlStyle-CssClass="table__item--hidden"/>
                <asp:BoundField DataField="Sala.Id"  HeaderStyle-CssClass="table__item--hidden" ItemStyle-CssClass="table__item--hidden"
                    ControlStyle-CssClass="table__item--hidden"/>
                <asp:BoundField DataField="Sala.Clasificacion" HeaderText="Sala" ItemStyle-CssClass="table__item"/>
                <asp:BoundField DataField="PrecioGeneral" HeaderText="Precio General" ItemStyle-CssClass="table__item"/>
                 <asp:BoundField DataField="PrecioNino" HeaderText="Precio niño" ItemStyle-CssClass="table__item"/>
                <asp:BoundField DataField="PrecioMayores" HeaderText="Precio Adulto Mayor" ItemStyle-CssClass="table__item"/>
                <asp:CommandField ShowSelectButton="true" HeaderStyle-Width="60px" ItemStyle-ForeColor="Black" />
              <asp:CommandField ShowDeleteButton="true"  ButtonType="Button"/>
            </Columns>
        </asp:GridView>
  <div class="box">
     <div class="mantenimiento__form">
        <asp:TextBox ID="TxtPrecioGeneral" runat="server" placeholder="Precio General"></asp:TextBox>
         <asp:TextBox ID="TxtPrecioNino" runat="server" placeholder="Precio Niño"></asp:TextBox>
         <asp:TextBox ID="TxtPrecioMayores" runat="server" placeholder="Precio Adulto Mayor"></asp:TextBox>
        <asp:DropDownList ID="DolistSala" runat="server" DataTextField="Clasificacion" DataValueField="Id"></asp:DropDownList>
        <div>
            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="button button--action" OnClick ="BtnGuardar_Click"/>
            <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="button button--action" OnClick ="BtnNuevo_Click"/>
        </div>
    </div>
  </div>
     
</asp:Content>