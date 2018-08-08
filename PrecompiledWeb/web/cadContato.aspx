<%@ page language="C#" masterpagefile="~/GeneralMasterPage.master" autoeventwireup="true" inherits="cadContato, App_Web_c55ppa01" title="Contato" %>


<%@ Register src="UcListarContatoEmail.ascx" tagname="UcListarContatoEmail" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">

<div class="dvMensagem" runat="server" id="dvMensagem"></div>

<div class="divTitGrupo">Dados Contato</div>
 <table runat="server" id="tbCadastro"  style='width: 97%; border: none'> 
 <tr>
   <td style="width: 150px">
   </td>
   <td></td>
 </tr>
  
			<tr   id="tr_id" runat="server" >
			    <td>ID<span class="campoObrigatorio"  style="display:none" > * </span>
				</td>
				
				<td> 
			  <asp:Label ID="txtId" runat="server"  
                    CssClass="lbDados" ></asp:Label>
		      
				</td>
			
			</tr>
			
			 
 
			<tr   id="tr_nome" runat="server" >
			    <td>Nome<span class="campoObrigatorio" > * </span>
				</td>
				
				<td> 
			  <asp:TextBox ID="txtNome" runat="server" Width="300px"  CssClass="txtPadrao" 
                     MaxLength="300"></asp:TextBox>
		      
				</td>
			
			</tr>
			
			 
 
			<tr   id="tr_empresa" runat="server" >
			    <td>Empresa<span class="campoObrigatorio"  style="display:none" > * </span>
				</td>
				
				<td> 
			  <asp:TextBox ID="txtEmpresa" runat="server" Width="300px"  CssClass="txtPadrao" 
                     MaxLength="300"></asp:TextBox>
		      
				</td>
			
			</tr>
			
			 
 
			<tr   id="tr_telefone_pessoal" runat="server" >
			    <td>Tel. Pessoal<span class="campoObrigatorio"   style="display:none" > * </span>
				</td>
				
				<td> 
			  <asp:TextBox ID="txtTelefonePessoal" Width="110px" runat="server"  CssClass="txtPadrao" 
                     MaxLength="30"></asp:TextBox>
		      
				</td>
			
			</tr>
			
			 
 
			<tr   id="tr_telefone_comercial" runat="server" >
			    <td>Tel. Comercial<span class="campoObrigatorio"  style="display:none" > * </span>
				</td>
				
				<td> 
			  <asp:TextBox ID="txtTelefoneComercial" Width="110px" runat="server"  CssClass="txtPadrao" 
                     MaxLength="30"></asp:TextBox>
		      
				</td>
			
			</tr>
			
			 
 
			<tr  style="display:none"   id="tr_emails" runat="server" >
			    <td>Emails<span class="campoObrigatorio"  style="display:none" > * </span>
				</td>
				
				<td> 
			  <asp:TextBox ID="txtEmails" runat="server"  CssClass="txtPadrao" 
                    ></asp:TextBox>
		      
				</td>
			
			</tr>
			
			 <tr>
                 <td colspan="2">
                     <label>Emails</label>


                     <uc1:UcListarContatoEmail ID="UcListarContatoEmail1" runat="server" />


                 </td>

			 </tr>
 
			<tr   id="tr_data_cadastro" runat="server" >
			    <td>Data Cadastro<span class="campoObrigatorio"  style="display:none" > * </span>
				</td>
				
				<td> 
			  <asp:Label ID="txtDataCadastro" runat="server"  
                    CssClass="lbDados" ></asp:Label>
		      
				</td>
			
			</tr>
			
			 
 
  
 
 </table>
 
 
 

    <br />
    <div id="divButtom">
        <div class="LabelCampoObrigatorio">
            * Campos Obrigat&oacute;rios
        </div>
        <asp:Button id="btNovo" runat="server" CssClass="botao" onclick="btNovo_Click" Text="Novo" />
        <asp:Button id="btSalvar" runat="server" CssClass="botao" onclick="btSalvar_Click"
            OnClientClick="return validar();" Text="Salvar" />
        <asp:Button id="btExcluir" runat="server" CssClass="botao" onclick="btExcluir_Click"
            Text="Excluir" OnClientClick="return excluir()" /></div>

  <div runat="server" class="divVoltar" id="dvVoltar">
 
 </div>

<script>
function validar(){
     return true;
}

function excluir(){
    
    if ( !confirm("Deseja realmente excluir ?. "))
        return false;
        
        
    return true;    
            
}
/*
$(function() {
		$('.temData').datepicker({ changeMonth: true, changeYear: true  });
		
		//$('#tabs').tabs();

});
*/
</script>

</asp:Content>

