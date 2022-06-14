<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logueo.aspx.cs" Inherits="Logueo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
            text-decoration: underline;
        }
        .auto-style2 {
            height: 1px;
            width: 347px;
        }
        .auto-style3 {
            width: 347px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div style="text-align: left">
        <asp:Button ID="BtnVolver" runat="server" OnClick="Volver_Click" Text="Volver" />
    </div>
    <div style="text-align: center">
                <strong><em><span class="style1">LOGUEO</span></em></strong><br />
        <br />
        <br />
    <div style="text-align: center">
        <table style="width: 271px" align="center" >
            <tr>
                <td style="text-align: right" class="auto-style3">
                    &nbsp;</td>
                <td style="width: 347px; text-align: right" rowspan="4">
                    <asp:Login ID="Login1" runat="server" DisplayRememberMe="False"
                        LoginButtonText="Login" PasswordLabelText="Contraseña" RenderOuterTable="false"
                        TitleText="Ingreso" UserNameLabelText="Usuario"
                        onauthenticate="Login1_Authenticate">
                    </asp:Login>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="auto-style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="auto-style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="auto-style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 347px; height: 21px; text-align: center" colspan="2">
                    <asp:Label ID="LblError" runat="server" Width="320px"></asp:Label></td>
            </tr>
        </table>
    
    </div>
        <br />   
    
    </div>
    </form>
</body>
</html>
