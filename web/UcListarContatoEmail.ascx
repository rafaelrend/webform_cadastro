<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcListarContatoEmail.ascx.cs" Inherits="UcListarContatoEmail" %>

<div style="width: 100%; text-align: center;">


<table id="Table1" runat="server" class="divBox" style="width: 100%; display: none" >
  
  <tr><td>
  <span class='lb_campo'></span> 
 </td>
 </tr>
 

<tr>

<td colspan="2" align="right" style="height: 25px">
  <div style="text-align: right">
    &nbsp;<asp:Button
        id="btSalvar" runat="server" CssClass="botaoNovo"
        
        Text="Salvar" OnClick="btSalvar_Click" />

</div>
</td>

</tr>


</table>

<div class="dvMensagem" enableviewstate="false" runat="server" id="my_dvMensagem"></div>

<div style="width: 550px; text-align: right">
    <asp:ImageButton id="ImageButton1" runat="server" ImageUrl="~/img/application_add.png" ToolTip="Adicionar" OnClick="ImageButton1_Click">
    </asp:ImageButton>
    &nbsp; &nbsp;
</div>
    <asp:GridView  id="gvwDados" runat="server" AutoGenerateColumns="False"
     CssClass="gridDados" Width="550px" OnPageIndexChanging="gvwDados_PageIndexChanging" OnSelectedIndexChanging="gvwDados_SelectedIndexChanging" OnRowDataBound="gvwDados_RowDataBound">
        <pagersettings position="TopAndBottom"></pagersettings>
        <columns>

  
    <asp:BoundField DataField="id" HeaderText="ID">
      <ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
    </asp:BoundField>
      
 
			    <asp:TemplateField HeaderText="id_contato" Visible="false"><ItemTemplate>
	               <asp:Label ID="lb_txtIdContato" runat="server"  CssClass="lbDadosGrid" 
                 Visible="<%# (bool)this.Consulta %>"></asp:Label> 

	   
			  <asp:HiddenField ID="txtIdContato" runat="server"  
                     Visible="<%# !(bool)this.Consulta %>"></asp:HiddenField>
		      

     </ItemTemplate>

       <ItemStyle  HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
			
			 
 
			    <asp:TemplateField HeaderText="Email"><ItemTemplate>
	               <asp:Label ID="lb_txtEmail" runat="server"  CssClass="lbDadosGrid" 
                 Visible="<%# (bool)this.Consulta %>"></asp:Label> 

	   
			  <asp:TextBox ID="txtEmail" runat="server"  CssClass="txtPadrao" 
                     MaxLength="300" Width="350px" Visible="<%# !(bool)this.Consulta %>"></asp:TextBox>
		      

     </ItemTemplate>

       <ItemStyle  HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
			
			 
 
			    <asp:TemplateField HeaderText="ordem_cadastro" Visible="false"><ItemTemplate>
	               <asp:Label ID="lb_txtOrdemCadastro" runat="server"  CssClass="lbDadosGrid" 
                 Visible="<%# (bool)this.Consulta %>"></asp:Label> 

	   
			  <asp:TextBox ID="txtOrdemCadastro" runat="server"  CssClass="txtPadrao" 
                     MaxLength="6" Visible="<%# !(bool)this.Consulta %>"></asp:TextBox>
		      

     </ItemTemplate>

       <ItemStyle  HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
			
			 
<asp:CommandField SelectImageUrl="~/img/bin.png" ShowSelectButton="True" ButtonType="Image">
<ItemStyle Width="4%"></ItemStyle>
</asp:CommandField>
</columns>
        <pagerstyle cssclass="gridPager" />
        <emptydatatemplate>
Não h&aacute; dados a serem exibidos
</emptydatatemplate>
        <headerstyle cssclass="divCollapsibleHeader" />
        <alternatingrowstyle cssclass="gridItemVerde" />
    </asp:GridView>


</div>
