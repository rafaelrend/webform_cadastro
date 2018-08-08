// JScript File

function getNum(tx)
{
    var totalH = 0;
    try{
    var stx = replaceTotal( tx.value , ".","");
    
    stx = parseFloat( replaceTotal( stx , ",","."));
           if ( stx.toString() != "NaN")
           totalH += stx;
     return     totalH;  
     }catch(exp){
       return 0;
     }
     
}


function formataNumBr(val)
{
       var stx = "";
       var tx = "";
    val = Math.round(val*100)/100;
    
    tx = val.toString();
    
    
       stx = replaceTotal( tx , ".","|");
       stx =  replaceTotal( stx , ",",".");
       
       stx = replaceTotal( stx , "|",",");
            
           
     return     stx;  
}


function getNumBr(tx)
{
    var totalH = 0;
    //var stx = replaceTotal( tx , ".",",");
    
    
    var stx = parseFloat( replaceTotal( tx , ",","."));
    
           if ( stx.toString() != "NaN")
           totalH += stx;
    
    stx = replaceTotal( totalH.toString() , ".",",");
            
           
     return     stx;  
}

function calculaTotal( obj , nome){
 //ctl00_cph_ucItemPecas_gvwDados_ctl03_txtValorTotal
 //ctl00_cph_ucItemPecas_gvwDados_ctl03_txtValorDesconto
 //ctl00_cph_ucItemPecas_gvwDados_ctl03_txtValorUnitario
 //ctl00$cph$ucItemPecas$gvwDados$ctl03$txtQtde
 
  if ( obj != null ){
             
              var prefixo = obj.id.split(nome);
              var pref = prefixo[0];
              
              var qt = getNum(  document.getElementById(pref + "txtQtde" ) );
              var val_unit = getNum(  document.getElementById(pref + "txtValorUnitario" ) );
              var desc = getNum(  document.getElementById(pref + "txtValorDesconto" ) );
              
              var tot = qt * val_unit - desc;
              document.getElementById(pref + "txtValorTotal" ).value = formataNumBr( tot );
              
  }
  
  var ar_pedacos = new Array();
  
  ar_pedacos[0] =  "Servicos";
  ar_pedacos[1] = "Pecas";
  
 // alert("oiii");
    var st_pref = "Pecas";
          var arqt = totais( localiza(new Array("ucItem"+st_pref+"$","txtValorTotal"), false));
          var arqt1 = arqt;
           document.getElementById("ctl00_cph_txtSubtotal"+st_pref+"").value = formataNumBr( arqt );
          
          
           var desct = totais( localiza(new Array("ucItem"+st_pref+"$","txtValorDesconto"), false));
          document.getElementById("ctl00_cph_txtDesconto"+st_pref+"").value = formataNumBr( desct );
          
           var subt = arqt + desct;
          document.getElementById("ctl00_cph_txtTotal"+st_pref+"").value = formataNumBr( subt );
           
          
      st_pref = "Servicos";
            var arqt = totais( localiza(new Array("ucItem"+st_pref+"$","txtValorTotal"), false));
          
           document.getElementById("ctl00_cph_txtSubtotal"+st_pref+"").value = formataNumBr( arqt );
          
          
           var desct = totais( localiza(new Array("ucItem"+st_pref+"$","txtValorDesconto"), false));
          document.getElementById("ctl00_cph_txtDesconto"+st_pref+"").value = formataNumBr( desct );
          
           var subt = arqt + desct;
          document.getElementById("ctl00_cph_txtTotal"+st_pref+"").value = formataNumBr( subt );
          
   
    document.getElementById("ctl00$cph$txtTotalGeral").value = formataNumBr( arqt1 + arqt );
  
 }
 
 
 function totais( ar ){
    var ret = 0;
    for (i=0; i < ar.length; i++)
        {
        ret += getNum( ar[i] );
        }
    return ret;    
 }

function localiza(arrfiltro, retorna1, exceto )
{

    var arp = document.getElementsByTagName("input");
   var ars = document.getElementsByTagName("select");
   var arst = document.getElementsByTagName("textarea");
   
   var ar = new Array();
   var arr = new Array();
 for (gz = 0 ; gz <= 2 ; gz++)
 {
  if ( gz == 0)
       ar = ars;
  
  if ( gz == 1)
       ar = arp;
   
  if ( gz == 2)
       ar = arst;
       
   for (i=0; i <= ar.length; i++)
        {
        
			retorna = false;
			 for (z =0; z < arrfiltro.length ; z++)
			 {
			
			    if ( ar[i] == null || ar[i].name == null || 
			       ar[i].name == undefined)
			        continue;
			 
			   if ( exceto != null && ar[i].name.indexOf( exceto ) > -1)
			      continue;
			 
				if ( ar[i].name.indexOf( arrfiltro[z] ) > -1)
				   retorna = true;
				else
				{
				   retorna = false;
				   break;
				}

		     }
			if (retorna == true && retorna1)
				return ar[i];
			
			if (retorna == true && !retorna1)
			    arr[  arr.length ] = ar[i];
		    // if ( strpos($key,
			
		}
	}
		
		
		if ( !retorna1)
		    return arr;
		else
		    return null;
   
}