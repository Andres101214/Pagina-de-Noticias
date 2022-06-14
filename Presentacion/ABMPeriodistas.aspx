<%@ Page Title="" Language="C#" MasterPageFile="~/MPUsuario.master" AutoEventWireup="true" CodeFile="ABMPeriodistas.aspx.cs" Inherits="ABMPeriodistas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<p>
                <h1>Periodistas</h1>
    <table style="width: 55%;" border="1" align="center">
        <tr>
            <td colspan="1">
                Cédula:</td>
            <td class="style10">
                <asp:TextBox ID="TxtCedula" runat="server" Width="105px" 
                    ></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="BtnBuscarPer" runat="server" Text="Buscar" 
                    onclick="BtnBuscarPer_Click" />
                <br />
                &nbsp;
                <asp:Label ID="LblPeriodista" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="1">
                Nombre: </td>
            <td>
                <asp:TextBox ID="TxtNombrePer" runat="server" Width="181px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="style8" colspan="1">
                Mail:</td>
            <td>
                <asp:TextBox ID="TxtMail" runat="server" Width="181px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="style3" colspan="2">
                <asp:Label ID="LblError" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Button ID="BtnAltaPer" runat="server" Text="       Alta             " 
                    onclick="BtnAltaPer_Click" style="text-align: center" Height="26px" 
                    Width="78px" />
            &nbsp;&nbsp;
                <asp:Button ID="BtnEliminarPer" runat="server" Height="26px" Text="Eliminar" 
                    Width="78px" OnClick="BtnEliminarPer_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnModificarPer" runat="server" Text="Modificar" 
                    OnClick="BtnModificarPer_Click" />
                <br />
                <br />
                <asp:Button ID="BtnLimpiarPer" runat="server" Text="       Limpiar     " 
                   style="text-align: center" onclick="BtnLimpiarPer_Click" Height="25px" 
                    Width="109px" />
            </td>
        </tr>
    </table>
    </p>
</asp:Content>

