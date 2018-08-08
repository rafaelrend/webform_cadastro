<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDoubleListBox.ascx.cs" Inherits="ucDoubleListBox" %>
<table style="width: 99%" runat="server" id="tb_general">
<tr>
<td style="width: 45%" valign="top">
    <asp:ListBox ID="listOptions1" Width="100%" runat="server"></asp:ListBox>
</td>
<td style="width: 10%; text-align: center">

	
    <asp:HiddenField ID="hdTemp" runat="server" />
    
    
    
   
 <input id="bt_right1" runat="server" class="botao2" name="bt_right1" type="button"
                value=" > " />
            <br />
            <input id="bt_right2" runat="server" class="botao2" name="bt_right2" type="button"
                value=">>" />
            <br />
            <input id="bt_left1" runat="server" class="botao2" name="bt_left1" type="button"
                value=" < " />
            <br />
            <input id="bt_left2" runat="server" class="botao2" name="bt_left2" 
                type="button" value="<<" /> 
			   
    <asp:HiddenField ID="hd_values" runat="server" />
    <asp:HiddenField ID="hd_text" runat="server" />
</td>


<td style="width: 45%" valign="top">
    <asp:ListBox ID="listOptions2" Width="100%" runat="server"></asp:ListBox>
</td>


</tr>


</table>