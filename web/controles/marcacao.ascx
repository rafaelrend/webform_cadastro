<%@ Control Language="C#" AutoEventWireup="true" CodeFile="marcacao.ascx.cs" Inherits="controles_marcacao" %>
<asp:CheckBoxList ID="chk_opcoes" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">

 <asp:ListItem Value="C" Text="Consultar"></asp:ListItem>
 <asp:ListItem Value="S" Text="Salvar / Editar"></asp:ListItem>
 <asp:ListItem Value="E" Text="Excluir"></asp:ListItem>

</asp:CheckBoxList>
