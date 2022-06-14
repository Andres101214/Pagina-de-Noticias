<%@ Page Title="" Language="C#" MasterPageFile="~/MPUsuario.master" AutoEventWireup="true" CodeFile="AltaModNotInt.aspx.cs" Inherits="AltaModNotInt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
        height: 25px;
        width: 119px;
    }
        .auto-style2 {
            height: 28px;
        width: 119px;
    }
        .auto-style3 {
            height: 29px;
        }
        .auto-style4 {
            height: 30px;
        }
        .auto-style5 {
            height: 31px;
        }
        .auto-style6 {
        height: 86px;
        width: 119px;
        }
        .auto-style7 {
            height: 86px;
            text-align: left;
        }
        .auto-style8 {
            height: 29px;
            width: 119px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
                <h1>Alta y Modificación Noticia Internacional</h1>
    <table border="1" align="center" class="auto-style5">
        <tr>
            <td colspan="1" class="auto-style2">
                Codigo Interno:</td>
            <td class="auto-style3">
                <asp:TextBox ID="TxtCodInt" runat="server" Width="222px" 
                    ></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="BtnBuscarInter" runat="server" Text="Buscar" 
                    onclick="BtnBuscar_Click" />
                <br />
                <asp:Label ID="LblInternacional" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="1">
                País: </td>
            <td>
                <asp:TextBox ID="TxtPaisInter" runat="server" Width="222px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="auto-style1" colspan="1">
                Fecha:</td>
            <td>
                <asp:TextBox ID="TxtFechaInter" runat="server" Width="222px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="auto-style1" colspan="1">
                Importancia:</td>
            <td>
                <asp:TextBox ID="TxtImportanciaInter" runat="server" Width="222px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="auto-style1" colspan="1">
                Título:</td>
            <td>
                <asp:TextBox ID="TxtTituloInter" runat="server" Width="222px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="auto-style1" colspan="1">
                Cuerpo:</td>
            <td>
                <asp:TextBox ID="TxtCuerpoInter" runat="server" Height="160px" Width="381px"></asp:TextBox>
            </td>
 
        </tr>
        <tr>
            <td class="auto-style6" colspan="1">
                Agregar Periodista: </td>
            <td class="auto-style7">
                <asp:TextBox ID="TxtAgregarPer" runat="server" Width="222px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Button ID="AgregarPer" runat="server" Text="Agregar" Height="30px" OnClick="AgregarPer_Click" Width="86px" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnQuitarPer" runat="server" Height="30px" OnClick="BtnQuitarPer_Click" Text="Quitar" Width="86px" />
            </td>
 
        </tr>
        <tr>
            <td class="auto-style8" colspan="1">
                Periodista/s: </td>
            <td class="auto-style3">
                <asp:ListBox ID="LbPeriodistas" runat="server" Height="110px" Width="291px" OnSelectedIndexChanged="LbPeriodistas_SelectedIndexChanged"></asp:ListBox>
            </td>
 
        </tr>
        <tr>
            <td class="auto-style8" colspan="1">
                Última modificación por:</td>
            <td class="auto-style3">
                <asp:Label ID="LbUsuMod" runat="server"></asp:Label>
            </td>
 
        </tr>
        <tr>
            <td class="style3" colspan="2">
                <asp:Label ID="LblError" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Button ID="BtnAltaInter" runat="server" Text="       Alta             " 
                    onclick="BtnAlta_Click" style="text-align: center" Height="30px" 
                    Width="109px" />&nbsp;&nbsp;
                &nbsp;&nbsp;<asp:Button ID="BtnModificarInter" runat="server" Text="Modificar" 
                    OnClick="BtnModificar_Click" Height="30px" Width="109px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="BtnLimpiar" runat="server" Text="   Limpiar     " 
                   style="text-align: center" onclick="BtnLimpiar_Click" Height="30px" 
                    Width="109px" CssClass="auto-style4" />
            </td>
        </tr>
    </table>
    </p>
</asp:Content>

