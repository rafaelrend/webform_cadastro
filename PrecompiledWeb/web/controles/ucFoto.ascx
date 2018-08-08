<%@ control language="C#" autoeventwireup="true" inherits="controles_ucFoto, App_Web_4rvqzmy4" %>
<asp:Image ID="Image1" ImageUrl="~/icons/no-photo2.png" Width="180px" runat="server" /><br />
<div style="width:180px; text-align: center">


<asp:HyperLink ID="HyperLink1" CssClass="link_envia_foto" runat="server">Selecionar Imagem</asp:HyperLink> &nbsp;-&nbsp; 
    <asp:LinkButton ID="LinkButton1" CssClass="link_envia_foto" runat="server" OnClick="LinkButton1_Click">Remover</asp:LinkButton>

</div>
<div style="width:180px; text-align: center; display:none" runat="server" id="divLoad">
   
    <img src="~/icons/ajax-loader.gif" id="ajax_load" runat="server" alt="carregando" />
</div>
<div style="width:180px; text-align: center; display:none" runat="server" id="divMostraFile"  >

    <asp:FileUpload ID="File1" Width="180px" runat="server" EnableViewState="false" />
</div>
<input id="hd_pasta" runat="server" type="hidden" />
<input id="hd_imagem" runat="server" type="hidden" />
<input id="hd_unique_name" runat="server" type="hidden" />
