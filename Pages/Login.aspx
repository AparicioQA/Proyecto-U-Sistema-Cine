<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MovieCenter.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <link href="../Styles/normalize.css" rel="stylesheet" /> 
     <link href="../Styles/Styles.css" rel="stylesheet" /> 
    <title>Login</title>
</head>
<body class="mantenimiento login">
    <form id="form1" runat="server">
        <h1 class="login__title">Acceso de Usuario</h1>
        <p>Usuario: Brian</p>
        <p>Contraseña: 12345678</p>
        <div class="box6">
            <div class="box7">
                <asp:TextBox ID="TxtUsuario" runat="server" placeholder="Usuario" CssClass="box7__item"></asp:TextBox>
                <asp:TextBox ID="TxtClave" runat="server" placeholder="Clave" CssClass="box7__item" TextMode="Password"></asp:TextBox>
            </div>
            <img src="../Images/cine.png" class="img--login"/>
        </div>
        <div class="box">
        <asp:Button ID="BtnLogin" Text="Iniciar Sesion" runat="server" CssClass="button button--action" OnClick="BtnLogin_Click"/>

      </div>

    </form>
</body>
</html>
