<%@ control language="C#" autoeventwireup="true" inherits="UserControls_UcFaseDatas, App_Web_4rvqzmy4" %>
<table>
<tr id="trTitulo" runat="server" style="font-weight: normal;" ><td colspan="2" id="tdTitulo" runat="server"></td>
</tr>
<tr>
<td><asp:Label ID="Label4" runat="server" Font-Bold="false" Text="Dt. Início:"></asp:Label><span id="span1" runat="server"></span><asp:TextBox
    ID="txtDtInicio" runat="server" CssClass="txtPadrao" MaxLength="10" Width="70px"></asp:TextBox>
   <asp:TextBox  runat="server" ID="txtDtInicioHora" Visible="false" CssClass="txtPadrao" MaxLength="5" Width="40px"></asp:TextBox>
    </td>
    <td runat="server" id="tdCol2"><asp:Label ID="Label6" runat="server"
        Font-Bold="false" Text="Dt. Final:"></asp:Label><span id="spanQubra2" runat="server"></span><asp:TextBox
    ID="txtDtFim" runat="server" MaxLength="10" CssClass="txtPadrao" Width="70px"></asp:TextBox>

   <asp:TextBox runat="server" ID="txtDtFimHora" Visible="false"  CssClass="txtPadrao" MaxLength="5" Width="40px"></asp:TextBox>
    &nbsp;

</td>
</tr>
</table>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" 
runat="server" Format="dd/MM/yyyy" TargetControlID="txtDtInicio">
</ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" 
runat="server" Format="dd/MM/yyyy" TargetControlID="txtDtFim">
</ajaxToolkit:CalendarExtender>