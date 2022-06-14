<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Consulta.ascx.cs" Inherits="UserControl_WebUserControl" %>
<script runat="server">

</script>

<style type="text/css">

    .auto-style1 {
        width: 53%;
    }
    .auto-style8 {
        height: 75px;
        width: 350px;
    }
    .auto-style7 {
        height: 75px;
    }
    .auto-style9 {
        text-align: center;
    }
    .auto-style10 {
        width: 350px;
        height: 28px;
        text-align: center;
    }
    .auto-style11 {
        height: 28px;
    }
    .auto-style12 {
        width: 350px;
        height: 25px;
        text-align: right;
    }
    .auto-style13 {
        height: 25px;
    }
    .auto-style14 {
        width: 350px;
        text-align: center;
    }
    .auto-style16 {
        margin-left: 0px;
    }
    .auto-style17 {
        width: 350px;
        text-align: left;
    }
</style>

<table border="1" class="auto-style1">
    <tr>
        <td class="auto-style14">DATOS NOTICIAS</td>
        <td class="auto-style9">DATOS DE PERIODISTAS</td>
    </tr>
    <tr>
        <td class="auto-style17">Codigo: <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style16" Width="232px" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="auto-style17">Titulo:&nbsp;
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style17">Fecha:
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style10">Cuerpo:
            <asp:TextBox ID="TextBox4" runat="server" Height="158px" Width="211px"></asp:TextBox>
        </td>
        <td class="auto-style11">
        <asp:GridView ID="GVPeriodistas" runat="server" Height="186px" Width="426px" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GVPeriodistas_SelectedIndexChanged">
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="auto-style12">Importancia: <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        </td>
        <td class="auto-style13"></td>
    </tr>
    <tr>
        <td class="auto-style8">Periodistas que Escriben:
            <asp:ListBox ID="ListBox1" runat="server" Height="96px" Width="268px"></asp:ListBox>
        </td>
        <td class="auto-style7"></td>
    </tr>
    <tr>
        <td class="auto-style17">Usuario que modifica:
            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style9" colspan="2">&nbsp;</td>
    </tr>
</table>

