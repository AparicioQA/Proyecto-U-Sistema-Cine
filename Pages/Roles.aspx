<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="MovieCenter.Pages.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >

     <h1 class="mantenimiento__title">Roles</h1>

   <div >
       <div class="dnlistbox">
           <asp:DropDownList ID="DolistRoles" runat="server" DataTextField="Nombre" DataValueField="Id" OnSelectedIndexChanged="DolistRoles_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList ID="DolistMantenimientos" runat="server" DataTextField="Nombre" DataValueField="Id" OnSelectedIndexChanged="DolistMantenimientos_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
       </div>
        <asp:CheckBoxList ID="CbxListAcciones" runat="server" CssClass="cbxlist"></asp:CheckBoxList>
       <div class="actionsbox">
           <asp:Button CssClass="button button--action"  ID="BtnAplicar" runat="server" Text="Aplicar" OnClick="BtnAplicar_Click"/>
           <asp:Button CssClass="button button--action"  ID="BtnEliminar" runat="server" Text="Eliminar" OnClick="BtnEliminar_Click"/>
       </div>
      <div class="box2">
            <asp:TextBox ID="TxtRol" runat="server" >Rol</asp:TextBox>
            <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="button button--action" OnClick="BtnNuevo_Click"/>
      </div>
   </div>
</asp:Content>
