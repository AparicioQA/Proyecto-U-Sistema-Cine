<%@ Page Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="MovieCenter.Pages.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <h1 class="mantenimiento__title">Usuarios</h1>

     <asp:GridView CssClass="mantenimiento__table" ID="GrdUsuarios" runat="server" AutoGenerateColumns="false" PageSize="20"
            DataKeyNames="Id" OnSelectedIndexChanged ="GrdUsuarios_SelectedIndexChanged" OnRowDeleting ="GrdUsuarios_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Id"  HeaderStyle-CssClass="table__item--hidden" ItemStyle-CssClass="table__item--hidden"
                    ControlStyle-CssClass="table__item--hidden"/>
                  <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" ItemStyle-CssClass="table__item"/>
                <asp:BoundField DataField="Rol.Id"  HeaderStyle-CssClass="table__item--hidden" ItemStyle-CssClass="table__item--hidden"
                    ControlStyle-CssClass="table__item--hidden"/>
                <asp:BoundField DataField="Rol.Nombre" HeaderText="Rol" ItemStyle-CssClass="table__item"/>
             
                <asp:CommandField ShowSelectButton="true" HeaderStyle-Width="60px" ItemStyle-ForeColor="Black" />
              <asp:CommandField ShowDeleteButton="true"  ButtonType="Button"/>
            </Columns>
        </asp:GridView>

  <div class="box">
     <div class="mantenimiento__form">
        <asp:TextBox ID="TxtNombreUsuario" runat="server" placeholder="Nombre Usuario"></asp:TextBox>
         <asp:TextBox ID="TxtClave" runat="server" placeholder="Clave"></asp:TextBox>
        <asp:DropDownList ID="DolistRol" runat="server" DataTextField="nombre" DataValueField="Id"></asp:DropDownList>
        <div>
            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="button button--action" OnClick ="BtnGuardar_Click"/>
            <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="button button--action" OnClick ="BtnNuevo_Click"/>
        </div>
    </div>
  </div>
     
</asp:Content>