<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCronometro.ascx.cs" Inherits="controles_ucCronometro" %>
<table><tr><td><asp:TextBox ID="TextBox1" runat="server" Width="50px" MaxLength="5"></asp:TextBox></td><td>
<input id="botaoplay" class="botaoplay" type="button" title="Iniciar" runat="server" /></td>
<td><input id="botaopause" class="botaopause" type="button" title="Pausar" runat="server" style="display:none" /></td>
<td><input id="botaoreset" class="botaoreset" type="button" title="Reiniciar" runat="server" />
</td></tr></table>