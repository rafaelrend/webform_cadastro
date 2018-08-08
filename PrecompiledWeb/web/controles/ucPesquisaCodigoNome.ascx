<%@ control language="C#" autoeventwireup="true" inherits="controles_ucPesquisaCodigoNome, App_Web_4rvqzmy4" %>
<table runat="server" id="tbDados"  border="0">
<tr>
<td id="tdDesc" runat="server" valign="top"></td>
<td id="tdTab" runat="server" valign="top"><asp:TextBox ID="txtCodigo" runat="server" Width="60px" MaxLength="30"></asp:TextBox></td>
<td id="tdTab2" runat="server" valign="top"><asp:TextBox ID="txtDescricao" runat="server" Width="190px"></asp:TextBox></td>
<td id="tdTab3" runat="server" style="width: 20px" valign="top"><asp:Image ID="Image1" runat="server" 
    ImageUrl="~/img/application_form_magnify.png" ToolTip="Pesquisar" />
    
    
    </td>
</tr></table>

<asp:HiddenField ID="hdTabela" runat="server" />
<asp:HiddenField ID="hdColunaCodigoConsulta" runat="server" />
<asp:HiddenField ID="hdColunaIDConsulta" runat="server" />
<asp:HiddenField ID="hdEhTexto" runat="server" />

<asp:HiddenField ID="hdIDRegistro" runat="server" />
<asp:HiddenField ID="hdColunaDescricao" runat="server" />
<asp:HiddenField ID="hdPaginaPesquisa" runat="server" />
<asp:HiddenField ID="hdEntidade" runat="server" />
