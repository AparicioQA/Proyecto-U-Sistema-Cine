<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="MovieCenter.Pages.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="mantenimiento__title">Seleccione sus Butacas</h1>
    <div class="box--peliculas">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpPanel" runat="server" UpdateMode="Always"></asp:UpdatePanel>
    </div>
    <div class="box8">
        <div class="box5">
            <asp:Label runat="server" ID="LblTotalEntradas" CssClass="txt--white"></asp:Label>
            <asp:Label runat="server" ID="LblEntradasGeneral" CssClass="txt--white"></asp:Label>
            <asp:Label runat="server" ID="LblEntradasNino" CssClass="txt--white"></asp:Label>
            <asp:Label runat="server" ID="LblEntradasMayor" CssClass="txt--white"></asp:Label>

            <asp:Label runat="server" ID="LblTotalColones" CssClass="txt--white"></asp:Label>
            <asp:Label runat="server" ID="LblTotalDolares" CssClass="txt--white"></asp:Label>
        </div>
       <%-- <div class="box5">
            <asp:Button ID="Btn2D" OnClick="Btn2D_Click" runat="server" Text="Bonificacion 2D" CssClass="button button--Bonification" />
            <asp:Button ID="BtnIMAX" OnClick="BtnIMAX_Click" runat="server" Text="Bonificacion IMAX" CssClass="button button--Bonification" />
        </div>--%>
    </div>

    <div class="box">
        <asp:Button runat="server" ID="BtnPagar" Text="Pagar" CssClass="button button--action button--bigAction" OnClick="BtnPagar_Click"
           /></div>
</asp:Content>
