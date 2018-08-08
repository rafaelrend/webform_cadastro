<%@ control language="C#" autoeventwireup="true" inherits="controles_ucMyDoubleListBox, App_Web_4rvqzmy4" %>
<table id="tb_general" runat="server" style="width: 99%">
    <tr>
        <td style="width: 45%" valign="top">
            <asp:ListBox ID="listOptions1" SelectionMode="Multiple" runat="server" Width="100%"></asp:ListBox>
        </td>
        <td style="width: 10%; text-align: center">
            <asp:HiddenField ID="hdTemp" runat="server" />
           
              <input type="button" style="margin-top: 3px;" class="botao2" id="bt_right1" runat="server" name="bt_right1" value=" &gt; " onserverclick="bt_right1_ServerClick"
			     />
			  <!--   
              <br />
              <input type="button" style="margin-top: 3px;" class="botao2" id="bt_right2" runat="server" name="bt_right2"  value="&gt;&gt;" onserverclick="bt_right2_ServerClick"  />
              -->
              <br />
              <input type="button" style="margin-top: 3px;" class="botao2" id="bt_left1" runat="server" name="bt_left1"  value=" &lt; " onserverclick="bt_left1_ServerClick"  />
              <!--
              <br />
              <input type="button" style="margin-top: 3px;" class="botao2" id="bt_left2" runat="server" name="bt_left2" value="&lt;&lt;"  onserverclick="bt_left2_ServerClick" />	
			  -->
            <asp:HiddenField ID="hd_values" runat="server" />
            <asp:HiddenField ID="hd_text" runat="server" />
        </td>
        <td style="width: 45%" valign="top">
            <asp:ListBox ID="listOptions2"  SelectionMode="Multiple"  runat="server" Width="100%"></asp:ListBox>
        </td>
    </tr>
</table>
