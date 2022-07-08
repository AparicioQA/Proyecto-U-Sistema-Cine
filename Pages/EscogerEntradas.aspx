<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="EscogerEntradas.aspx.cs" Inherits="MovieCenter.Pages.EscogerEntradas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="box3">
        <asp:Label runat="server" ID="LblTitulo" CssClass="mantenimiento__title box3__item"></asp:Label>
        <asp:Label runat="server" ID="LblSinopsis" CssClass="mantenimiento__title box3__item"></asp:Label>
        <div>
            <asp:Label runat="server" ID="LblClasificacion" CssClass="mantenimiento__title box3__item"></asp:Label>
            <asp:Label runat="server" ID="LblFecha" CssClass="mantenimiento__title box3__item"></asp:Label>
        </div>
    </div>

    <div class="box6">

        <div class="box6__item">
            <p class="mantenimiento__title">Dia</p>
            <asp:RadioButtonList ID="RbtnlstDias" runat="server">
                <asp:ListItem>Lunes</asp:ListItem>
                <asp:ListItem>Martes</asp:ListItem>
                <asp:ListItem>Miercoles</asp:ListItem>
                <asp:ListItem>Jueves</asp:ListItem>
                <asp:ListItem>Viernes</asp:ListItem>
                <asp:ListItem>Sabado</asp:ListItem>
                <asp:ListItem>Domingo</asp:ListItem>
            </asp:RadioButtonList>
        </div>

            <div class="box6__item">
                <p class="mantenimiento__title">Sala</p>
                <asp:RadioButtonList ID="RbtnlstSalas" runat="server" OnSelectedIndexChanged="RbtnlstSalas_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
            </div>

            <div class="box6__item">
                <p class="mantenimiento__title">Horario</p>
                <asp:RadioButtonList ID="RbtnlstHorarios" runat="server"></asp:RadioButtonList>
            </div>

            <div class="box5">
                <p class="mantenimiento__title">Cantidad Entradas</p>
                <asp:TextBox ID="TxtGeneral" TextMode="Number" placeholder="Entrada General" runat="server" CssClass="box5__item"></asp:TextBox>
                <asp:TextBox  ID="TxtNinos" TextMode="Number" placeholder="Entrada Niños" runat="server" CssClass="box5__item"></asp:TextBox>
                <asp:TextBox  ID="TxtMayores" TextMode="Number" placeholder="Entrada Adulto Mayor" runat="server" CssClass="box5__item"></asp:TextBox>
            </div>
           
    </div>
    <div class="box"><asp:Button runat="server" ID="Continuar" Text="Continuar" CssClass="button button--action button--bigAction" OnClick="Continuar_Click" /></div>
</asp:Content>
