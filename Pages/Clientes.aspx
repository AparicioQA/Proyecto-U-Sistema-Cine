<%@ Page Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="MovieCenter.Pages.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <h1 class="mantenimiento__title">Clientes</h1>

     <asp:GridView CssClass="mantenimiento__table" ID="GrdClientes" runat="server" AutoGenerateColumns="false" PageSize="20"
            DataKeyNames="Id" OnSelectedIndexChanged="GrdClientes_SelectedIndexChanged" OnRowDeleting="GrdClientes_RowDeleting">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Cedula" ItemStyle-CssClass="table__item" />
                 <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-CssClass="table__item"/>
                 <asp:BoundField DataField="Apellido1" HeaderText="Primero Apellido" ItemStyle-CssClass="table__item"/>
                 <asp:BoundField DataField="Apellido2" HeaderText="Segundo Apellido" ItemStyle-CssClass="table__item"/>
                 <asp:BoundField DataField="UsuarioP.Id"  HeaderStyle-CssClass="table__item--hidden" ItemStyle-CssClass="table__item--hidden"
                    ControlStyle-CssClass="table__item--hidden"/>
                <asp:BoundField DataField="UsuarioP.NombreUsuario" HeaderText="Usuario" ItemStyle-CssClass="table__item"/> 
                <asp:BoundField DataField="Correo" HeaderText="Correo" ItemStyle-CssClass="table__item"/>
                  <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" ItemStyle-CssClass="table__item"/>
                 <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ItemStyle-CssClass="table__item"/>
                 <asp:BoundField DataField="Puntos" HeaderText="Puntos" ItemStyle-CssClass="table__item"/>
                <asp:BoundField DataField="Canjes" HeaderText="Canjes" ItemStyle-CssClass="table__item"/>
                <asp:CommandField ShowSelectButton="true" HeaderStyle-Width="60px" ItemStyle-ForeColor="Black" />
              <asp:CommandField ShowDeleteButton="true"  ButtonType="Button"/>
            </Columns>
        </asp:GridView>

  <div class="box">
     <div class="mantenimiento__form">
          <div>
           <asp:TextBox ID="TxtCedula" runat="server" placeholder="Cédula"></asp:TextBox>
          <asp:TextBox ID="TxtNombre" runat="server" placeholder="Nombre"></asp:TextBox>
          <asp:TextBox ID="TxtApellido1" runat="server" placeholder="Primer Apellido"></asp:TextBox>
          <asp:TextBox ID="TxtApellido2" runat="server" placeholder="Segundo Apellido"></asp:TextBox>         
          <asp:TextBox ID="TxtCorreo" runat="server" placeholder="Correo"></asp:TextBox>
          </div>
         <div>
              <asp:TextBox ID="TxtFechaNacimiento" runat="server" TextMode="Date"></asp:TextBox>
          <asp:TextBox ID="TxtTelefono" runat="server" placeholder="Teléfono"></asp:TextBox>

        <asp:DropDownList ID="DolistUsuario" runat="server" DataTextField="NombreUsuario" DataValueField="Id"></asp:DropDownList>
         </div>
          
        <div>
            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="button button--action" OnClick="BtnGuardar_Click1"/>
            <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="button button--action" OnClick="BtnNuevo_Click"/>
            
        </div>
    </div>
  </div>
     
</asp:Content>

