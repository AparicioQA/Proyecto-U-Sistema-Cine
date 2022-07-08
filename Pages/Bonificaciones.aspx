<%@ Page Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Bonificaciones.aspx.cs" Inherits="MovieCenter.Pages.Bonificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <h1 class="mantenimiento__title">Bonificaciones</h1>
    <div>
        <div>
            <asp:Label ID="LblPuntos" runat="server" CssClass="mantenimiento__title"></asp:Label>
            <asp:Label ID="LblMensajePuntos" runat="server" CssClass="mantenimiento__title"></asp:Label>
                
        </div>
         <div>
             <asp:Label ID="LblCanjes" runat="server" CssClass="mantenimiento__title"></asp:Label>
            <asp:Label ID="LblMensajeCanjes" runat="server" CssClass="mantenimiento__title"></asp:Label>
                
        </div>
        <div></div>
    </div>
     
  <div class="box">
     <div class="mantenimiento__form">
          <h2 class="mantenimiento__title">Condiciones Recompensa</h2>
        <asp:TextBox ID="TxtPuntos" runat="server">Puntos Necesarios </asp:TextBox>
        <asp:TextBox ID="TxtCanjes" runat="server">Canjes Necesarios</asp:TextBox>
        <div>
            <asp:Button ID="BtnActualizar" runat="server" Text="Actualizar" CssClass="button button--action" OnClick ="BtnActualizar_Click"/>
        </div>
    </div>
  </div>
     
</asp:Content>

