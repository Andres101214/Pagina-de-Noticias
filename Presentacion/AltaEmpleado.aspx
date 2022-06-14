<%@ Page Title="" Language="C#" MasterPageFile="~/MPUsuario.master" AutoEventWireup="true" CodeFile="AltaEmpleado.aspx.cs" Inherits="AltaEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-right: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
                <h1>Alta Empleados</h1>
    <table style="width: 55%;" border="1" align="center">
        <tr>
            <td colspan="1">
                Usuario:</td>
            <td class="style10">
                <asp:TextBox ID="TxtUsuario" runat="server" Width="105px" 
                    ></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="BtnBuscarUsu" runat="server" Text="Buscar" 
                    onclick="BtnBuscarUsu_Click" />
                <br />
                &nbsp;
                <asp:Label ID="LblUsuario" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="1">
                Constraseña: </td>
            <td>
                <asp:TextBox ID="TxtContraseña" runat="server" Width="110px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="style8" colspan="1">
                &nbsp;</td>
            <td>
&nbsp;</td>
 
        </tr>
        <tr>
            <td class="style3" colspan="2">
                <asp:Label ID="LblError" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Button ID="BtnAltaUsu" runat="server" Text="       Alta             " 
                    onclick="BtnAltaUsu_Click" style="text-align: center" Height="35px" 
                    Width="105px" />
            &nbsp;<%--<asp:Button ID="BtnModificarUsu" runat="server" CssClass="auto-style1" Height="35px" Text="Modificar" Width="105px" />--%>
                <%--<asp:Button ID="BtnBajaUsu" runat="server" Height="35px" Text="Baja" Width="76px" />--%>
            &nbsp;&nbsp;
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <br />
                <br />
                <asp:Button ID="BtnLimpiarUsu" runat="server" Text="    Limpiar     " 
                   style="text-align: center" onclick="BtnLimpiarUsu_Click" Height="30px" 
                    Width="103px" />
            </td>
        </tr>
    </table>
    </p>
</asp:Content>

