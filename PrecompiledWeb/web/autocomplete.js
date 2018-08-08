
	


function setaAutoCompletar(){

        $( ".autocomplete_agencia" ).autocomplete({
		    //Página onde vamos buscar os nomes dos clientes..
			source: "consultaJson.aspx?tabela=fornecedor&campodesc=nome&campoid=&campocod=id&comp="+  encodeURIComponent(" and id_tipo_fornecedor=12 "),
			minLength: 2,
			select: function( event, ui ) {
				item_selecionado(  ui.item.value, this );
			}
		});
		
		
			$( ".autocomplete_fornecedor" ).autocomplete({
		    //Página onde vamos buscar os nomes dos clientes..
			source: "consultaJson.aspx?tabela=fornecedor&campodesc=nome&campoid=&campocod=id&comp="+  encodeURIComponent(" and nome is not null  "),
			minLength: 2,
			select: function( event, ui ) {
				item_selecionado(  ui.item.value, this );
			}
		});
		
		
		
		$( ".autocomplete_clienteativa" ).autocomplete({
		    //Página onde vamos buscar os nomes dos clientes..
			source: "consultaJson.aspx?tabela=cliente&campodesc="+ encodeURIComponent("concat(nome_cliente,' ',sobrenome)")+"&campoid=&campocod=id&comp="+  encodeURIComponent(" and 1 = 1  "),
			minLength: 2,
			select: function( event, ui ) {
				cliente_selecionado(  ui.item.value, this );
			}
		});
		
				$( ".autocomplete_clientecomum" ).autocomplete({
		    //Página onde vamos buscar os nomes dos clientes..
			source: "consultaJson.aspx?tabela=cliente&campodesc="+ encodeURIComponent("concat(nome_cliente,' ',sobrenome)")+"&campoid=&campocod=id&comp="+  encodeURIComponent(" and 1 = 1  "),
			minLength: 2,
			select: function( event, ui ) {
				item_selecionado(  ui.item.value, this );
			}
		});
		
		
}



function getButtonCliente( nome1, nome2, nome3){

    var ips = document.getElementsByTagName("input");
 
 
     for ( var i = 0; i < ips.length; i++){
     
             if ( ips[i].name == null)
               continue;
               
               var nome = ips[i].name;
                
               if ( nome.indexOf(nome1) > -1 && nome.indexOf(nome2) > -1  && nome.indexOf(nome3) > -1  ){
                          return ips[i];
               }
     
     }
     
     return null;
    
}


	
function cliente_selecionado( message , obj ) {
				var campocod = obj.id.replace("_txtNomeCliente","_hdClienteReg");
			    var campoid = obj.id.replace("_txtNomeCliente","_hdClienteReg");
			    var campoindex = obj.id.replace("_txtNomeCliente","_hdGridReg");
		
		
		var but = getButtonCliente("UcPax","UcPax","Button1");
		
		var regPai = getButtonCliente("UcPax","UcPax","hdClienteReg");
		
		
		var buttonid = obj.id.replace("_txtNomeCliente","_Button1"); // ctl00_cph_tabAssociacao_tabPAX_UcPax1_Button1
		//alert( obj.id );
		//alert( buttonid );
         //alert( but.name );
			var id = obj.id;			
			var frag = id.split("_");
			
			//---------------------
			var f = document.forms[0];
			
		

			
			var ar2 = message.split('-COD:');
			var ar = new Array();
			
			if ( ar2[1].indexOf("-ID:") > 0 ){
			        ar = ar2[1].split("-ID:");	
			}else{
			        ar = new Array(ar2[1], ar2[1]);
			}
			
		
			var obj_p = document.getElementById(campocod);
			obj_p.value = ar[0];
			
			var obj_p2 = document.getElementById(campoid);
			obj_p2.value = ar[1];
			
			regPai.value = ar[0]+";"+document.getElementById(campoindex).value;
			
			//alert( document.getElementById(campoindex).value ); alert( obj_p.value );
			//ctl00$cph$tabAssociacao$tabPAX$UcPax1$Button1
		    g_doPostBack(  but.name , document.getElementById(campoindex).value );
			
		}

	
function item_selecionado( message , obj ) {
				var campocod = obj.id.replace("_txtDescricao","_txtCodigo");
			    var campoid = obj.id.replace("_txtDescricao","_hdIDRegistro");
		
			
			var id = obj.id;			
			var frag = id.split("_");
			
			//---------------------
			var f = document.forms[0];
			
		

			
			var ar2 = message.split('-COD:');
			var ar = new Array();
			
			if ( ar2[1].indexOf("-ID:") > 0 ){
			        ar = ar2[1].split("-ID:");	
			}else{
			        ar = new Array(ar2[1], ar2[1]);
			}
			
		
			var obj_p = document.getElementById(campocod);
			obj_p.value = ar[0];
			
			var obj_p2 = document.getElementById(campoid);
			obj_p2.value = ar[1];
			
		
			
		}// JScript File

	//$(function() {
    //      setaAutoCompletar();
	//});
	