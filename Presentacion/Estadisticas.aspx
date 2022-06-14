<%@ Page Title="" Language="C#" MasterPageFile="~/MPUsuario.master" AutoEventWireup="true" CodeFile="Estadisticas.aspx.cs" Inherits="Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style1
        {
            font-size: xx-large;
            color: #FF0000;
        }
        .auto-style1 {
            width: 263px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
                &nbsp;<h1>Estadisticas</h1>
    <div style="text-align: center">
                <br />
        <br />
        <br />
        <table style="width: 55%;" border="1" align="center">
        <tr>
            <td colspan="1" class="auto-style1">
                Seleccionar tipo de búsqueda</td>
            <td class="style10">
            &nbsp;&nbsp;<br />
                <asp:DropDownList ID="DDLTipoBusqueda" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLTipoBusqueda_SelectedIndexChanged">
                    <asp:ListItem>Nacional</asp:ListItem>
                    <asp:ListItem>Internacional</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
 
        </tr>
        <tr>
            <td class="auto-style1" colspan="1">
                Filtrar:</td>
            <td>
                <asp:Button ID="BtnFiltrar" runat="server" OnClick="BtnFiltrar_Click" Text="Filtrar" />
            </td>
 
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="style3">
                <asp:Button ID="BtnFiltrotipo" runat="server" OnClick="BtnFiltrotipo_Click" Text="Filtro Tipo y año" />
            </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
            &nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="BtnLimpiar" runat="server" Text="       Limpiar     " 
                   style="text-align: center" onclick="BtnLimpiar_Click" Height="25px" 
                    Width="109px" />
            </td>
        </tr>
    </table>
    
    </div>
        </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" Height="143px" Width="402px">
        </asp:GridView>
    </p>
    <p>
        <asp:Label ID="LblError" runat="server"></asp:Label>
    </p>
    <p>
    </p>
    <p>
        &nbsp;</p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

