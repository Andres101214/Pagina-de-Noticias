<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
            color: #FF0000;
        }
        .style2
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: right">
        <asp:Button ID="BtnLogueo" runat="server" OnClick="Ingresar_Click" Text="Ingresar" />
    </div>
    <div style="text-align: center">
                <strong><em><span class="style1">NOTICIAS YA</span></em></strong><br />
        <br />
        <br />
        <table style="width: 55%;" border="1" align="center">
        <tr>
            <td colspan="1">
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
            <td class="style8" colspan="1">
                Nombre de Seccion</td>
            <td>
                <asp:TextBox ID="TxtFiltro" runat="server"></asp:TextBox>
                <asp:Button ID="BtnBuscarSeccion" runat="server" OnClick="BtnBuscarSeccion_Click" Text="Buscar Seccion" />
                <asp:Label ID="LblSeccion" runat="server"></asp:Label>
            </td>
 
        </tr>
        <tr>
            <td class="style8" colspan="1">
                Filtrar:</td>
            <td>
                <asp:Button ID="BtnFiltrar" runat="server" OnClick="BtnFiltrar_Click" Text="Filtrar" />
            </td>
 
        </tr>
        <tr>
            <td>Filtro Por Fecha</td>
            <td class="style3">
                <asp:TextBox ID="TxtFecha" runat="server"></asp:TextBox>
                <asp:Button ID="BtnFiltroFecha" runat="server" OnClick="BtnFiltroFecha_Click" Text="Filtro por Fecha" />
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
        <%--<asp:GridView ID="GVNoticias" runat="server" AutoGenerateColumns="False" Height="236px" OnSelectedIndexChanged="GVNoticias_SelectedIndexChanged1" Width="476px">
            <Columns>
                <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" />
                <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                <asp:BoundField HeaderText="Tipo" />
                <asp:HyperLinkField HeaderText="Consulta" Text="Ver" />
            </Columns>
        </asp:GridView>--%>
        <asp:Label ID="LblError" runat="server"></asp:Label>
        <asp:GridView ID="GVNoticias" runat="server" Height="186px" Width="426px" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GVNoticias_SelectedIndexChanged">
        </asp:GridView>
    </form>
    </body>
</html>
