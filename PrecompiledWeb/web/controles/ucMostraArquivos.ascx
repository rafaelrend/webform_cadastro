<%@ control language="C#" autoeventwireup="true" inherits="controles_ucMostraArquivos, App_Web_4rvqzmy4" %>
<div>
  <b>Pasta:</b> <span id="sp_nome_pasta" runat="server"></span>
</div>
<asp:GridView ID="GridView1" runat="server" Width="400" BackColor="White" BorderColor="#999999" 
BorderStyle="Solid" BorderWidth="1px" EmptyDataText="Não há arquivos para serem exibidos!" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
>
    <FooterStyle BackColor="#CCCCCC" />
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
    <HeaderStyle  CssClass="divCollapsibleHeader" />
    <AlternatingRowStyle BackColor="#CCCCCC" />
    <Columns>
        <asp:BoundField DataField="nome" HeaderText="Nome do arquivo" />
        <asp:BoundField DataField="extensao" HeaderText="Extens&#227;o" />
        <asp:BoundField DataField="url" HeaderText="URL completa" />
        <asp:BoundField DataField="tamanho" HeaderText="Tamanho (Kb)" />
        <asp:HyperLinkField Text="Abrir" />
         <asp:HyperLinkField Text="Excluir" />
    </Columns>
</asp:GridView>

<input id="hd_pasta" runat="server" type="hidden" />
<input id="hd_uc_acao" name="hd_uc_acao"  type="hidden" />
<input id="hd_uc_arquivo" name="hd_uc_arquivo"  type="hidden" />


<input id="hd_uc_tabela" name="hd_uc_tabela"  runat="server" type="hidden" />
<input id="hd_ic_id_origem" name="hd_ic_id_origem" runat="server"  type="hidden" />
<script type="text/javascript">

function excluir_uc_arquivo(arquivo, pasta){
   
   if ( !confirm("Deseja realmente excluir o arquivo " + arquivo + "?"))
       return;
       
   var frm = document.forms[0];
   
   frm.hd_uc_acao.value = "excluir";
   frm.hd_uc_arquivo.value = arquivo;
   frm.submit();

}


</script>