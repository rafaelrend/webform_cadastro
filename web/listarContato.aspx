<%@ Page Language="C#" MasterPageFile="~/GeneralMasterPage.master" enableeventvalidation="false" AutoEventWireup="true" CodeFile="listarContato.aspx.cs" Inherits="listarContato" Title="Contato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">

<div style="width: 100%; text-align: center;">

<table id="Table1" runat="server" class="divBox" style="width: 100%;" >

  <tr style="display:none;">
  
      <th class="divCollapsibleHeader" colspan="2">FILTRO</th>
  </tr>
  
  <tr style="display:none;"><td>
  <span class='lb_campo'>Código ou Nome</span> &nbsp;<br />    
  <asp:TextBox id="txtDescricao" runat="server"    CssClass="txtPadrao" MaxLength="30"  
     Width="70%"></asp:TextBox> 
 
    <asp:Button runat="server" ID="btPesquisar" Text="Pesquisar" OnClick="btPesquisar_Click" CssClass="botaoPesquisar" />&nbsp;
    
    
    <asp:Button runat="server" ID="Button1" Text=" Search " OnClick="btPesquisar_Click"  />
    <asp:Button runat="server" ID="btExportar" Text="Exportar" OnClick="btPesquisar_Click" CssClass="botaoExportar" Visible="false" />&nbsp;
</td>
 </tr>
 

<tr>

<td colspan="2" align="right" style="height: 25px">
  <div style="text-align: right">

    <asp:Button
        id="btNovo" runat="server" CssClass="botaoNovo" PostBackUrl="cadContato.aspx"
        
        Text="&nbsp;Novo" />

</div>
</td>

</tr>


</table>

<div id="divExplicaFiltro" visible="false" runat="server" class="divExplicaFiltro">
    &nbsp;</div>
    <asp:GridView  id="gvwDados" runat="server" AutoGenerateColumns="False"
     CssClass="gridDados" Width="100%" OnPageIndexChanging="gvwDados_PageIndexChanging" OnSelectedIndexChanging="gvwDados_SelectedIndexChanging" OnRowDataBound="gvwDados_RowDataBound">
        <pagersettings position="TopAndBottom"></pagersettings>
        <columns>
<asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/cadContato.aspx?id={0}&amp;acao=LOAD" NavigateUrl="~/cadContato.aspx" Target="_self" Text="&lt;img alt='Alterar' title='Alterar' src='img/page_edit.png' border='0' /&gt;">
<ItemStyle Width="20px"></ItemStyle>
</asp:HyperLinkField>

 
      <asp:BoundField DataField="id" HeaderText="ID" >
			<ItemStyle HorizontalAlign="Right" ></ItemStyle>
			</asp:BoundField>
       
 
      <asp:BoundField DataField="nome" HeaderText="Nome" >
			<ItemStyle HorizontalAlign="Left" ></ItemStyle>
			</asp:BoundField>
       
 
      <asp:BoundField DataField="empresa" HeaderText="Empresa" >
			<ItemStyle HorizontalAlign="Left" ></ItemStyle>
			</asp:BoundField>
       
 
      <asp:BoundField DataField="telefone_pessoal" HeaderText="Tel. Pessoal" >
			<ItemStyle HorizontalAlign="Left" ></ItemStyle>
			</asp:BoundField>
       
 
      <asp:BoundField DataField="telefone_comercial" HeaderText="Tel. Comercial" >
			<ItemStyle HorizontalAlign="Left" ></ItemStyle>
			</asp:BoundField>
       
 
      <asp:BoundField DataField="emails" HeaderText="Emails" >
			<ItemStyle HorizontalAlign="Left" ></ItemStyle>
			</asp:BoundField>
       
 
      <asp:BoundField DataField="data_cadastro" HeaderText="Data Cadastro" DataFormatString="{0:dd/MM/yyyy HH:mm}">
			<ItemStyle HorizontalAlign="Center" ></ItemStyle>
			</asp:BoundField>
       

</columns>
        <pagerstyle cssclass="gridPager" />
        <emptydatatemplate>
Não há dados a serem exibidos
</emptydatatemplate>
        <headerstyle cssclass="divCollapsibleHeader" />
        <alternatingrowstyle cssclass="gridItemVerde" />
    </asp:GridView>

<!--Novo -->
<div style="text-align: right; width: 97%" id="dv_qtde_registros" class="dv_qtde_registros" runat="server"></div> 

</div>



</asp:Content>

