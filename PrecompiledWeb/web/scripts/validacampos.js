  //Transforma todos os caracteres digitados em mai�scula
function maiuscula(objeto){
	 objeto.value =  objeto.value.toUpperCase()

}


function DoCal(elTarget) {
	if (showModalDialog) {
		var sRtn;
	    sRtn = showModalDialog("../_include/calendar.htm","","center=yes;dialogWidth=310px;dialogHeight=220px");
		if (sRtn!="") {
			elTarget.value = sRtn;
		}
	} else {
	    alert("Internet Explorer 4.0 ou superior � necess�rio!");
	}
}

//Faz o c�lculo do n�mero de dias entre datas...
var dateDif = {
// Fonte: http://www.bigbold.com/snippets/posts/show/2501
dateDiff: function(strDate1,strDate2){
return (((Date.parse(strDate2))-(Date.parse(strDate1)))/(24*60*60*1000)).toFixed(0);
}
}

function openPopUp(url, name, w, h)
{
    popWidth = w;
    popHeight = h;
    popLeft = (screenWidth - popWidth)/2;
    popTop = (screenHeight - popHeight)/2;

    features = 'directories=1,location=0,menubar=1,toolbar=1,status=0,dependent=0,width=' + popWidth + 
               ',height=' + popHeight + 
               ',innerWidth=' + popWidth + 
               ',innerHeight=' + popHeight + 
               ',left=' + popLeft + 
               ',top=' + popTop;
    var win = null;
    win = window.open(url, name, features);
    if (window.focus) { win.focus(); }
}

var screenWidth, screenHeight;
screenWidth = window.screen.width;
screenHeight = window.screen.height;

function openPop(url, name, w, h) {
    popWidth = w;
    popHeight = h;
    popLeft = (screenWidth - popWidth) / 2;
    popTop = (screenHeight - popHeight) / 2;

    features = 'resizable=yes, directories=0,location=0,menubar=0,toolbar=0,status=0,dependent=0, scrollbars=1, width=' + popWidth +
               ',height=' + popHeight +
               ',left=' + popLeft +
               ',top=' + popTop;
    var win = null;
    window.open(url, name, features);
   // if (window.focus) { win.focus(); }
}

function abreGaleria(url, idpedido, idtabela){

    openPop(url, "galeria_"+idpedido+"_"+idtabela, 800, 620);
}	

function adicionarDias(data, dias){
     return new Date(data.getTime() + (dias * 24 * 60 * 60 * 1000));
  }
  
  
// Autor da l�gica de data: hunternh 
// Documentador: Leonardo Nobre
// Data: 31/01/2008 - 11:45h

// txtData - � a data inicial.
// DiasAdd - � quantos dias voc� quer adicionar a txtData.
function SomarData(txtData,DiasAdd) 
{
                // Tratamento das Variaveis.
                // var txtData = "01/01/2007"; //poder ser qualquer outra
                // var DiasAdd = 10 // Aqui vem quantos dias voc� quer adicionar a data
                var d = new Date();
                        // Aqui eu "mudo" a configura��o de datas.
                        // Crio um obj Date e pego o campo txtData e 
                        // "recorto" ela com o split("/") e depois dou um
                        // reverse() para deixar ela em padr�o americanos YYYY/MM/DD
                        // e logo em seguida eu coloco as barras "/" com o join("/")
                        // depois, em milisegundos, eu multiplico um dia (86400000 milisegundos)
                        // pelo n�mero de dias que quero somar a txtData.
                        d.setTime( Date.parse( txtData.split("/").reverse().join("/") ) +( 86400000*(DiasAdd)) );
             
                // Crio a var da DataFinal                      
                var DataFinal;
                // Aqui comparo o dia no objeto d.getDate() e vejo se � menor que dia 10.                       
                if(d.getDate() < 10)
                {
                        // Se o dia for menor que 10 eu coloca o zero no inicio
                        // e depois transformo em string com o toString()
                        // para o zero ser reconhecido como uma string e n�o
                        // como um n�mero.
                        DataFinal = "0"+d.getDate().toString();
                }
                else
                {       
                        // Aqui a mesma coisa, por�m se a data for maior do que 10
                        // n�o tenho necessidade de colocar um zero na frente.
                        DataFinal = d.getDate().toString();     
                }
                
                // Aqui, j� com a soma do m�s, vejo se � menor do que 10
                // se for coloco o zero ou n�o.
                if((d.getMonth()+1) < 10){
                        DataFinal += "/"+"0"+(d.getMonth()+1).toString()+"/"+d.getFullYear().toString();
                }
                else
                {
                        DataFinal += "/"+( (d.getMonth()+1).toString())+"/"+d.getFullYear().toString();
                }
                
                return DataFinal;
                
}


function getDataBr(dataUs)
{
   var str = zeroE(dataUs.getDate())+"/"+zeroE(dataUs.getMonth())+"/"+ dataUs.getFullYear();

   return str;
}
function zeroE(num)
{
    if ( num < 10)
       return "0"+num;
       
    return num;
}

function getDataUs(dataBr)
{
   if ( dataBr == "")
      return null;
      
    var stDt = dataBr.split("/");
    
    var data = new Date(stDt[2], stDt[1], stDt[0]);
    
    return data;
}

//Formata a data para um formato do Javascript, onde ele possa manipular e calcular.
function formataDataJs(dataInfo)
{
         mes = [];
        mes[0] = "January";
        mes[1] = "February";
        mes[2] = "March";
        mes[3] = "April";
        mes[4] = "May";
        mes[5] = "June";
        mes[6] = "July";
        mes[7] = "August";
        mes[8] = "September";
        mes[9] = "October";
        mes[10] = "November";
        mes[11] = "December";



   arrDataInfo = dataInfo.split('/');
   // Formata a data para o seguinte formato: November 22 2006
   novaDataInfo = mes[(arrDataInfo[1] - 1)] + ' ' + arrDataInfo[0] + ' ' + arrDataInfo[2];

   return novaDataInfo;
}

function validarData(obj){
     if (obj.value!=""){
	 if (!eData(obj.value)){
	  obj.focus();
	  alert('Data inv�lida');	 
	  return false;
	  }
	 }
	  return true;
   
   }

 function isVazio(obj, mensagem){
  if (obj != undefined){  
	if (obj.value=='' && obj.disabled == false){
	
	alert(mensagem);
		try
		{
	obj.focus();
		} catch (ex) { }
	return true;
	   }
	}
   return false;
 }
function VerificaValorValido(valor){
      if (valor!='' && valor!='.' && valor!=','){
         if (valor.substring(0,0) !=',' && valor.substring(0,0) !='.'){
		 return true;
	     }
	  } else { 
	     return false;
	  }
 return false;
}

function MM_openBrWindow(theURL,winName,features) { 
  window.open(theURL,winName,features);
}

function AlfaNumerico(event)
{
    Tecla = event.which;
    if(Tecla == null)
	Tecla = event.keyCode;
    if((Tecla >64  && Tecla <= 65+25)||(Tecla >96  && Tecla <= 96+26)||(Tecla==32)){
		return true;
	}
	else{
	    return false;
	}
}
//Limpa o CPF pra ficar apenas os n�meros 
  function ClearCPF(str)
      {
      while((cx=str.indexOf('.'))!=-1)
      {		
      str = str.substring(0,cx)+str.substring(cx+1);
      }
	  while((cx=str.indexOf('-'))!=-1)
      {		
      str = str.substring(0,cx)+str.substring(cx+1);
      }
     return str;
  }
  function VerNumeracao(CPF){
    if ((CPF== 11111111111) || (CPF==22222222222) || (CPF==33333333333) || (CPF==44444444444) || (CPF==55555555555) || (CPF==66666666666) ||
	    (CPF==77777777777) || (CPF==88888888888) || (CPF==99999999999)){
		 return false;		
		}  
   return true;
  } 
  
//Valida o cpf formatado com os pontos e tra�os
function valida_cpf(CPF)
{
   var NumCPF =  ClearCPF(CPF);
   if (!VerNumeracao(NumCPF)){
       return false; 
   }
   dig_1 = 0;
   dig_2 = 0;
   controle_1 = 10;
   controle_2 = 11;
   lsucesso = 1;

   if ((CPF.length != 14) || (CPF.substring(11, 12) != "-"))
    return false
   else 
   {
    CPF = CPF.substring(0,3) + CPF.substring(4,7) + CPF.substring(8,14);
   for (i=0 ; i < 9 ; i++) 
   {
   dig_1 = dig_1 + parseInt(CPF.substring(i, i+1) * controle_1);
   controle_1 = controle_1 - 1;
   }

   resto = dig_1 % 11;
   dig_1 = 11 - resto;

   if ((resto == 0) || (resto == 1)) dig_1 = 0;
   for ( i=0 ; i < 9 ; i++)
   {
   dig_2 = dig_2 + parseInt(CPF.substring(i, i + 1) * controle_2);
   controle_2 = controle_2 - 1;
   }
   dig_2 = dig_2 + 2 * dig_1;
   resto = dig_2 % 11;
   dig_2 = 11 - resto;

   if ((resto == 0) || (resto == 1)) dig_2 = 0;
   dig_ver = (dig_1 * 10) + dig_2;

   if (dig_ver != parseFloat(CPF.substring(CPF.length-2,CPF.length))) return false;
   }
  return true;
  } 
  
  
  
 // Insere a m�scara de CPF
function Mascara (formato, objeto)
     {
	   //  alert(objeto);
	     
    campo = eval (objeto);
    if (formato=='CPF')
    {
     caracteres = '01234567890';
     separacoes = 3;
     separacao1 = '.';
     separacao2 = '-';
     conjuntos = 4;
     conjunto1 = 3;
     conjunto2 = 7;
     conjunto3 = 11;
     conjunto4 = 14;
    if ((caracteres.search(String.fromCharCode (window.event.keyCode))!=-1) && campo.value.length < 
        (conjunto4))
       {
    if (campo.value.length == conjunto1) 
      campo.value = campo.value + separacao1;
      else if (campo.value.length == conjunto2) 
      campo.value = campo.value + separacao1;
      else if (campo.value.length == conjunto3) 
      campo.value = campo.value + separacao2; 
     }
    else 
    event.returnValue = false;
  } 
}

function Numerico(event,code) /*aceita numeros, ponto e virgula*/
{
    Tecla = event.which;
       if(Tecla == null)
	Tecla = event.keyCode;

	if ( Tecla == 0 || Tecla == 8 )
	   return true;
	   
	if ( Tecla == 13 )//enter filho da puta
	   return false;

	if((Tecla > 47 && Tecla <= 57)||((code==2)&&(Tecla==47))||
	((Tecla==44)||(Tecla==46)) || ( ( Tecla==45) ) ) {
		return true;
		}
	else{
		return false;
		}
    return true;
}
function SoNumero(event,code) /*So aceita numeros*/
{
    Tecla = event.which;
    if(Tecla == null)
	Tecla = event.keyCode;
	
	if ( Tecla == 0 || Tecla == 8 )
	   return true;
	
	if ( Tecla == 13 )//enter filho da puta
	   return false;
	
	
   	//alert(Tecla);
	if((Tecla > 47 && Tecla <= 57)||
	   ((code==2)&&(Tecla==47)) ||  ((code==2)&&(Tecla==44)) ){
		return true;
		}
	else{
		return false;
		}
    return true;
}

function formata_dec(campo,tammax,teclapres) {
	var tecla = teclapres.keyCode;
	//vr = campo.value;
	vr = campo;
	vr = vr.replace( ",", "" );
	vr = vr.replace( ".", "" );
	vr = vr.replace( ",", "" );
	vr = vr.replace( ".", "" );
	vr = vr.replace( ".", "" );
	vr = vr.replace( ".", "" );
	vr = vr.replace( ".", "" );
	
	tam = vr.length;

	if ( tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105 ){
		if ( tam <= 2 ){ 
	 		campo.value = vr ; }
	 	if ( (tam > 2) && (tam <= 5) ){
	 		campo.value = vr.substr( 0, tam - 2 ) + ',' + vr.substr( tam - 2, tam ) ; 

}
	 	if ( (tam >= 6) && (tam <= 8) ){
	 		campo.value = vr.substr( 0, tam - 5 ) + '.' + vr.substr( tam - 5, 3 ) + 

',' + vr.substr( tam - 2, tam ) ; }
	 	if ( (tam >= 9) && (tam <= 11) ){
	 		campo.value = vr.substr( 0, tam - 8 ) + '.' + vr.substr( tam - 8, 3 ) + 

'.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
	 	if ( (tam >= 12) && (tam <= 14) ){
	 		campo.value = vr.substr( 0, tam - 11 ) + '.' + vr.substr( tam - 11, 3 ) + 

'.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; 

}
	 	if ( (tam >= 15) && (tam <= 17) ){
	 		campo.value = vr.substr( 0, tam - 14 ) + '.' + vr.substr( tam - 14, 3 ) + 

'.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + 

',' + vr.substr( tam - 2, tam ) ;}
	}		
	
}

function replaceTotal(texto,strI,strF){
	var tam, vet, i;
	vet = texto.split(strI);
	tam = vet.length;
	for(i=0;i<tam;i++){
		texto = texto.replace(strI,strF);
	}
	return texto;
}

function ValorParaBanco(valor){
	valor = replaceTotal(valor,".","");
	return replaceTotal(valor,",",".");
}

/* Fun�ao s� ir� permitir escrever numero */
function SNumerico(event){
	var t=event.wich;
	if(t==null){
		t=event.keyCode;
	}
	return eNumerico(String.fromCharCode(t));
}

function eNumerico(t){
var n="0123456789";
var ret=false;
	for(var j=0;j<t.length;j++){
		ret=false;
		for(var i=0;i<n.length;i++){
			if(n.charAt(i)==t.charAt(j)){
				ret=true;
				break;
			}
		}
		if(!ret){
			break;
		}
	}
return ret;
}
//Permite escrever numeros e ponto e v�rgula
function testKey2(e)
{
    chars= "0123456789,.";
   	e = window.event;
   	if(chars.indexOf(String.fromCharCode(e.keyCode))==-1)
   	{
   		window.event.keyCode=0;
	}
}
function testKey3(e)
{
    chars= "0123456789,";
   	e = window.event;
   	if(chars.indexOf(String.fromCharCode(e.keyCode))==-1)
   	{
   		window.event.keyCode=0;
	}
}
function testKey1(e)
{
    chars= "0123456789";
   	e = window.event;
   	if(chars.indexOf(String.fromCharCode(e.keyCode))==-1)
   	{
   		window.event.keyCode=0;
	}
}
function testKeyLetra(e)
{
    chars= "qwertyuiopasdfghjklzxcvbnm";
   	e = window.event;
   	if(chars.indexOf(String.fromCharCode(e.keyCode))==-1)
   	{
   		window.event.keyCode=0;
	}
}
/* Fun�ao para colocar a barra da data : 01/01/2000 */
function barra(obj){
   if(obj.value.length == 2 || obj.value.length == 5)
   		obj.value += "/";
}

/* Fun�ao para colocar os par�nteses e tra�o no telefone: (71)9999-9999 */
function tel(obj){
   if(obj.value.length == 2)
   		obj.value = "("+ obj.value +")";
   if(obj.value.length == 8)
   		obj.value += "-";
}

/* Fun�ao s� ir� permitir escrever numero */
function STel(event){
	var t=event.wich;
	if(t==null){
		t=event.keyCode;
	}
	return eTel(String.fromCharCode(t));
}

function eTel(t){
var n=" 0123456789";
var ret=false;
	for(var j=0;j<t.length;j++){
		ret=false;
		for(var i=0;i<n.length;i++){
			if(n.charAt(i)==t.charAt(j)){
				ret=true;
				break;
			}
		}
		if(!ret){
			break;
		}
	}
return ret;
}


function eData(d){
var d1="0123";
var d2="0123456789";
var m1="01";
var m2="0123456789";
var n31Dias="01;03;05;07;08;10;12";
var v31=n31Dias.split(";");
var a1="12";
var ret=false;
var i=0;
	//Checa o tamanho
	if(d.length!=10){
		return false;
	}
	//Checa as barras
	if(d.charAt(2)!="/"||d.charAt(5)!="/"){
		return false;
	}
	
	//Checa m�s
	//Primeiro digito do mes
		for(i=0;i<m1.length;i++){
			if(d.charAt(3)==m1.charAt(i)){
				ret=true;
			}
		}
		if(!ret){return false;}
	//checa 2� digito do mes
		ret=false;
		for(i=0;i<m2.length;i++){
			if(d.charAt(4)==m2.charAt(i)){
				ret=true;
			}
		}
		if(!ret){return false;}
		if(d.charAt(3)=="0"&&d.charAt(4)=="0"){
			return false;
		}
		if(d.charAt(3)=="1"){
			if(d.charAt(4)!="0"
				&& d.charAt(4)!="1"
				&& d.charAt(4)!="2"){
					return false;
			}
		}
	//checa 1� digito do dia
		ret=false;
		for(i=0;i<d1.length;i++){
			if(d.charAt(0)==d1.charAt(i)){
				ret=true;
			}
		}
		if(!ret){return false;}
	//checa 2� digito do dia
		ret=false;
		for(i=0;i<d2.length;i++){
			if(d.charAt(1)==d2.charAt(i)){
				ret=true;
			}
		}
		if(!ret){
			return false;
		}
		
		if(d.charAt(0)=="3"){
			if(d.charAt(1)!="0"&&d.charAt(1)!="1"){
				return false;
			}
			if(d.charAt(1)=="1"){
				var aux0=false;
				for(var j=0;j<v31.length;j++){
					if(v31[j]==d.substring(3,5)){
						aux0=true;
						break;
					}
				}
				if(!aux0){
					return false;
				}
			}
		}
		
		
		if(d.charAt(0)=="3"
			&&d.charAt(3)=="0"
			&&d.charAt(4)=="2"){
			return false;
		}
		
		if(d.charAt(6)!="1"&&d.charAt(6)!="2"){
			return false;
		}
		//checa se ano eh menor que 1800
		if(d.charAt(6)=="1"&&d.charAt(7)<="7"){
			return false;
		}
		
		for(i=7;i<10;i++){
			if(!eNumerico(d.charAt(i))){
				return false;
			}
		}
		return true;
}

function eBissexto(d)
{    
    dia=d.substring(0,2)
    mes=d.substring(3,5) 	 	
    ano=d.substring(6,10)
  
   		
    if (mes=="02")
    {	
      if (ano % 4 != 0 && dia > 28) {
		  return(false);
      }
      else if (dia > 29) {
		 return(false);
      }	
    }
    return(true);

}

function valida_data(obj){
  if (obj.value != ""){
  	if (eData(obj.value) == false){
  		alert("Data inv�lida.");
		obj.value = "";
		obj.focus();
  	}else{
		if(eBissexto(obj.value) == false){
		alert("Data inv�lida.");
		obj.value = "";
		obj.focus();
		}
	}
  }
}
function checa_tamanho(op,campo,tam_maximo){
// fun��o para limitar a quantidade de caracteres digitados num TEXTAREA

  var valorcampo = campo.value;
  if(op == 1){
	 if (valorcampo.length >= tam_maximo){
		var retira = (valorcampo.length-(tam_maximo-1));
		alert("Limite de carecteres excedido !");
		campo.value = "";
		campo.value = valorcampo.substring(0,valorcampo.length-(retira));
		campo.focus();
		return false;
	 }
  }
  else{
	 if (valorcampo.length > tam_maximo){
		var retira = valorcampo.length-tam_maximo;
		alert("Limite de caracteres excedido !");
		campo.value = ""
		campo.value = valorcampo.substring(0,valorcampo.length-(retira));
		campo.focus();
		return false;
	 }
  }
}
function DataBanco(valor){
  var Val;
  var Dia = valor.substring(0,2);
  var Mes = valor.substring(3,5);
  var Ano = valor.substring(6,10);
  Val = Ano + "-" + Mes + "-" + Dia
  return Val;

}

function popup(url,name,windowWidth,windowHeight){   
myleft=(screen.width)?(screen.width-windowWidth)/2:100;	mytop=(screen.height)?(screen.height-windowHeight)/2:100;	properties = "width="+windowWidth+",height="+windowHeight+",scrollbars=yes, top="+mytop+",left="+myleft;    window.open(url,name,properties);
}

function Robj(nome)
{
  return document.getElementById(nome);	
}

function robj(nome){

     var f = document.forms[0];
     var i = 0;

      for ( i = 0; i < f.elements.length; i++)
      {

                 
                  if ( f.elements[i].id != null && 
                       f.elements[i].id.indexOf(nome) > -1 )
                  {
                     return  f.elements[i];
                  }
                  
      
      }

     return null;
}

function getValorBySep(nome, sep, valorteste)
{
 var str = "";
 var f = document.forms[0];
 var i = 0;
 
 if ( valorteste == null || valorteste == undefined ){
 
 
    valorteste = true;
 
 }
 
 
 for ( i = 0; i < f.elements.length; i++)
 {

     if ( f.elements[i].name != null )
     
      if ( f.elements[i].name != null && 
           f.elements[i].name.indexOf(nome) > -1 )
      {
        if (f.elements[i].type == "checkbox" &&  
             f.elements[i].checked == valorteste)
             { 
                      if ( str == "")
                         str += f.elements[i].value;
                      else
                         str += sep +  f.elements[i].value;
          }
     
      
      } 
 }
  
   return str;  

}



function validaPeriodo(p1,p2)
{

   p1_dia=p1.substring(0,2)
   p1_mes=p1.substring(3,5)
   p1_ano=p1.substring(6,10)
      
   p2_dia=p2.substring(0,2)
   p2_mes=p2.substring(3,5)
   p2_ano=p2.substring(6,10)
   
   if (p1_ano > p2_ano)
      return(false);          
   else
   {
     if (p1_ano == p2_ano){
       if (p1_mes > p2_mes){ 
          return(false);
       }    
       else { 
        if (p1_mes == p2_mes){
          if (p1_dia > p2_dia) 
            return(false);
        }
       }   
     }     
   } 
   return(true);
} 



function CurrencyFormatted(amount)
{
	var i = parseFloat(amount);
	if(isNaN(i)) { i = 0.00; }
	var minus = '';
	if(i < 0) { minus = '-'; }
	i = Math.abs(i);
	i = parseInt((i + .005) * 100);
	i = i / 100;
	s = new String(i);
	if(s.indexOf('.') < 0) { s += '.00'; }
	if(s.indexOf('.') == (s.length - 2)) { s += '0'; }
	s = minus + s;
	return s;
}

function CommaFormatted(amount)
{
	var delimiter = ","; // replace comma if desired
	var a = amount.split('.',2)
	var d = a[1];
	var i = parseInt(a[0]);
	if(isNaN(i)) { return ''; }
	var minus = '';
	if(i < 0) { minus = '-'; }
	i = Math.abs(i);
	var n = new String(i);
	var a = [];
	while(n.length > 3)
	{
		var nn = n.substr(n.length-3);
		a.unshift(nn);
		n = n.substr(0,n.length-3);
	}
	if(n.length > 0) { a.unshift(n); }
	n = a.join(delimiter);
	if(d.length < 1) { amount = n; }
	else { amount = n + '.' + d; }
	amount = minus + amount;
	return amount;
}

// end of function CurrencyFormatted()

function mask_cpfcnpj(obj, e)
{
   if ( obj.value.length > 14 )
       MascaraCNPJ(obj, e);
   else
       MascaraCPF(obj, e);
}


//adiciona mascara ao CPF
function MascaraCPF(cpf, e){
	
        if(mascaraInteiro(cpf, e)==false){
                event.returnValue = false;
        }       
        return formataCampo(cpf, '000.000.000-00', e);
}


function MascaraCNPJ(cnpj, e){
        if(mascaraInteiro(cnpj, e)==false){
                event.returnValue = false;
        }       
        return formataCampo(cnpj, '00.000.000/0000-00', e);
}
function MascaraRG(cnpj, e){
        if(mascaraInteiro(cnpj, e)==false){
                event.returnValue = false;
        }       
        return formataCampo(cnpj, '00000000-00', e);
}

//adiciona mascara de cep
function MascaraCep(cep, e){
                if(mascaraInteiro(cep, e)==false){
                event.returnValue = false;
        }       
        return formataCampo(cep, '00.000-000', e);
}

//adiciona mascara de data
function MascaraData(data, e){

  var codigo = (e.which ? e.which : e.keyCode ? e.keyCode : e.charCode);
	
	

if ( codigo == 0 || codigo == 8 )
	   return true;





        if(mascaraInteiro(data, e)==false){
                event.returnValue = false;
        }       
        return formataCampo(data, '00/00/0000', e);
}

//adiciona mascara ao telefone
function MascaraTelefone(tel, e){  
        if(mascaraInteiro(tel, e)==false){
                event.returnValue = false;
        }       
        return formataCampo(tel, '(00) 0000-0000', e);
}



//valida telefone
function ValidaTelefone(tel){
        exp = /\(\d{2}\)\ \d{4}\-\d{4}/
        if(!exp.test(tel.value))
                alert('Numero de Telefone Invalido!');
}

//valida CEP
function ValidaCep(cep){
        exp = /\d{2}\.\d{3}\-\d{3}/
        if(!exp.test(cep.value))
                alert('Numero de Cep Invalido!');               
}

//valida data
function ValidaData(data){
        exp = /\d{2}\/\d{2}\/\d{4}/
        if(!exp.test(data.value))
                alert('Data Invalida!');                        
}

//valida o CPF digitado
function ValidarCPF(Objcpf){
        var cpf = Objcpf.value;
        exp = /\.|\-/g
        cpf = cpf.toString().replace( exp, "" ); 
        var digitoDigitado = eval(cpf.charAt(9)+cpf.charAt(10));
        var soma1=0, soma2=0;
        var vlr =11;
        
        for(i=0;i<9;i++){
                soma1+=eval(cpf.charAt(i)*(vlr-1));
                soma2+=eval(cpf.charAt(i)*vlr);
                vlr--;
        }       
        soma1 = (((soma1*10)%11)==10 ? 0:((soma1*10)%11));
        soma2=(((soma2+(2*soma1))*10)%11);
        
        var digitoGerado=(soma1*10)+soma2;
        if(digitoGerado!=digitoDigitado)        
                alert('CPF Invalido!');   
                
        return (digitoGerado ==digitoDigitado) ;            
}

//valida numero inteiro com mascara
function mascaraInteiro(obj, e){
	

     var tecla = e;

     if (tecla == null ||  typeof(tecla) == undefined)	
	tecla  = window.event;
	
    //alert(tecla);
	
     var codigo = (tecla.which ? tecla.which : tecla.keyCode ? tecla.keyCode : tecla.charCode);
	
	
	if ( codigo == 0 || codigo == 8 )
	   return true;
	
	
        if (codigo< 48 || codigo > 57){
                window.event.returnValue = false;
                return false;
        }
        return true;
}

//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj){
        var cnpj = ObjCnpj.value;
        var valida = new Array(6,5,4,3,2,9,8,7,6,5,4,3,2);
        var dig1= new Number;
        var dig2= new Number;
        
        exp = /\.|\-|\//g
        cnpj = cnpj.toString().replace( exp, "" ); 
        var digito = new Number(eval(cnpj.charAt(12)+cnpj.charAt(13)));
                
        for(i = 0; i<valida.length; i++){
                dig1 += (i>0? (cnpj.charAt(i-1)*valida[i]):0);  
                dig2 += cnpj.charAt(i)*valida[i];       
        }
        dig1 = (((dig1%11)<2)? 0:(11-(dig1%11)));
        dig2 = (((dig2%11)<2)? 0:(11-(dig2%11)));
        
        if(((dig1*10)+dig2) != digito)  
                alert('CNPJ Invalido!');
                
         
         return (((dig1*10)+dig2) == digito);
                
}

//formata de forma generica os campos
function formataCampo(campo, Mascara, evento) { 
        var boleanoMascara; 
        
        var Digitato = evento.keyCode;
        exp = /\-|\.|\/|\(|\)| /g
        campoSoNumeros = campo.value.toString().replace( exp, "" ); 
   
        var posicaoCampo = 0;    
        var NovoValorCampo="";
        var TamanhoMascara = campoSoNumeros.length;; 
        
        if (Digitato != 8) { // backspace 
                for(i=0; i<= TamanhoMascara; i++) { 
                        boleanoMascara  = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
                                                                || (Mascara.charAt(i) == "/")) 
                        boleanoMascara  = boleanoMascara || ((Mascara.charAt(i) == "(") 
                                                                || (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " ")) 
                        if (boleanoMascara) { 
                                NovoValorCampo += Mascara.charAt(i); 
                                  TamanhoMascara++;
                        }else { 
                                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo); 
                                posicaoCampo++; 
                          }              
                  }      
                campo.value = NovoValorCampo;
                  return true; 
        }else { 
                return true; 
        }
}


//Injeta qualquer tipo de m�scara nos campos
function mascara(e,src,mask) {
        if(window.event) {
        _TXT = e.keyCode;
        } else
        if(e.which) {
        _TXT = e.which;
        }
        if(_TXT > 47 && _TXT < 58) {
        var i = src.value.length;
        var saida = mask.substring(0,1);
        var texto = mask.substring(i);
        if(texto.substring(0,1) != saida) {
        src.value += texto.substring(0,1);
        }
        return true;
        } else {
        if (_TXT != 8) {
        return false;
        } else {
        return true;
        }
        }
}


//------------------------------------
function findPos(id_obj) {
//----------------------------------------
    
    var obj = document.getElementById(id_obj);
    
    if ( obj == null )
       return;
    
    var curleft = curtop = 0;
    if (obj.offsetParent) {
    do {
    		curleft += obj.offsetLeft;
    		curtop += obj.offsetTop;
    } while (obj = obj.offsetParent);
    return [curleft,curtop];
    }
}


  // Resize iframe to full height
  function resizeIframe(height, iframe_name)
  {
    // "+60" is a general rule of thumb to allow for differences in
    // IE & and FF height reporting, can be adjusted as required..
    
    
    document.getElementById(iframe_name).style.height = (parseInt(height)+20 ).toString() +"px";
  
  }