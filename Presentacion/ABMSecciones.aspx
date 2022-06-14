<%@ Page Title="" Language="C#" MasterPageFile="~/MPUsuario.master" AutoEventWireup="true" CodeFile="ABMSecciones.aspx.cs" Inherits="ABMSecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
                <h1>Secciones</h1>
    <table style="width: 55%;" border="1" align="center">
        <tr>
            <td colspan="1">
                Código:</td>
            <td class="style10">
                <asp:TextBox ID="TxtCodigo" runat="server" Width="105px" 
                    ></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="BtnBuscarSec" runat="server" Text="Buscar" 
                    onclick="BtnBuscarSec_Click" />
                <br />
                &nbsp;
                <asp:Label ID="LblSeccion" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="1">
                Nombre: </td>
            <td>
                <asp:TextBox ID="TxtNombre" runat="server" Width="110px"></asp:TextBox>
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
                <asp:Button ID="BtnAlta" runat="server" Text="       Alta             " 
                    onclick="BtnAlta_Click" style="text-align: center" Height="26px" Width="78px" />
            &nbsp;&nbsp;
                <asp:Button ID="BtnEliminar" runat="server" Height="26px" Text="Eliminar" Width="78px" OnClick="BtnEliminar_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnModificar" runat="server" Text="Modificar" OnClick="BtnModificar_Click" />
                <br />
                <br />
                <asp:Button ID="BtnLimpiar" runat="server" Text="       Limpiar     " 
                   style="text-align: center" onclick="BtnLimpiar_Click" Height="25px" Width="109px" />
            </td>
        </tr>
    </table>
    </p>
</asp:Content>

