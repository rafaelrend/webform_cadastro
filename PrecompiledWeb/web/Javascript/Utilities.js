// JScript File
// TRATAMENTO DE CEP

  //Transforma todos os caracteres digitados em maiúscula
function maiuscula(objeto){
	 objeto.value =  objeto.value.toUpperCase()

}

 function robj(nome)
 {
      var f = document.forms[0];
      for (i=0; i< f.elements.length; i++)
      {
       
           //alert(f.elements[i].name);
        if (f.elements[i].name.indexOf(nome) > -1)
        {
           //alert(f.elements[i].name + "---"+ nome);
           //if (
           //f.elements[i].name.substring( nome.length,  
           //f.elements[i].name.length - nome.length) == nome)
           //{
               return f.elements[i];
           //}
         }
       
      }
      
      var ar = document.getElementsByTagName("div");
     // alert(ar.length);
      for (i=0; i< ar.length; i++)
      {
      
        if (ar[i].id != null &&  ar[i].id.indexOf(nome) > -1)
        {
               return ar[i];
           
         }
       
      }
      
      return undefined;
 }


 function isVazio(obj, mensagem){
  if (obj != undefined){  
	if (obj.value==''){
	
	//alert(obj.type);
	
	if (obj.type != 'hidden')
	   { 
	   try{
	     obj.focus();  } catch(exp){} } 
	alert(mensagem);
	return false;
	   }
	}
   return true
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
//Limpa o CPF pra ficar apenas os números 
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
  
//Valida o cpf formatado com os pontos e traços
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
 // Insere a máscara de CPF
function Mascara (formato, objeto)
     {
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
   	
   		if ( Tecla == 45 ){
	   return true;
	}
	
   	//alert(Tecla);
   	
   	
	if((Tecla > 47 && Tecla <= 57)||((code==2)&&(Tecla==47))||((Tecla==44)||(Tecla==46)) ){
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
   	//alert(Tecla);
	if((Tecla > 47 && Tecla <= 57)||((code==2)&&(Tecla==47))){
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

/* Funçao só irá permitir escrever numero */
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
//Permite escrever numeros e ponto e vírgula
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
/* Funçao para colocar a barra da data : 01/01/2000 */
function barra(obj){
   if(obj.value.length == 2 || obj.value.length == 5)
   		obj.value += "/";
}

/* Funçao para colocar os parênteses e traço no telefone: (71)9999-9999 */
function tel(obj){
   if(obj.value.length == 2)
   		obj.value = "("+ obj.value +")";
   if(obj.value.length == 8)
   		obj.value += "-";
}

/* Funçao só irá permitir escrever numero */
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
	
	//Checa mês
	//Primeiro digito do mes
		for(i=0;i<m1.length;i++){
			if(d.charAt(3)==m1.charAt(i)){
				ret=true;
			}
		}
		if(!ret){return false;}
	//checa 2º digito do mes
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
	//checa 1º digito do dia
		ret=false;
		for(i=0;i<d1.length;i++){
			if(d.charAt(0)==d1.charAt(i)){
				ret=true;
			}
		}
		if(!ret){return false;}
	//checa 2º digito do dia
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
  		alert("Data inválida.");
		obj.value = "";
		obj.focus();
  	}else{
		if(eBissexto(obj.value) == false){
		alert("Data inválida.");
		obj.value = "";
		obj.focus();
		}
	}
  }
}
function checa_tamanho(op,campo,tam_maximo){
// função para limitar a quantidade de caracteres digitados num TEXTAREA

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


function cepSemMascara(cep)
{  
    Vr = cep.value;
    Vr = Vr.toString().replace( "-", "" );

    cep.value = Vr;
    cep.select();
}
function cepComMascara(cep)
{ 
    var Vr
    var Tam
    Vr = cep.value;
    Vr = Vr.toString().replace( "-", "" );

    Tam = Vr.length ;
    if (Tam == 8) 
    { 
        Vr = Vr.substr(0, 5) + '-' + Vr.substr(5,3);
        cep.value = Vr;
    }
}
function validarCep(cep)
{
    var Vr
    var Tam
    Vr = cep.value;
    Tam = Vr.length ;
    switch (Tam)
    {
        case 8:
            cepComMascara(cep);
           break;
        case 0:
            break;
        default:
            alert("Um número de CEP deve conter 8 dígitos!") ;
            cep.focus();
            cep.select();
            cepSemMascara(cep);
            break;
    }
}
//TRATAMENTO DE CNPJ
function validarCnpj(cnpj)
{   
    var pcnpj= cnpj.value;
    
    if (pcnpj.length = 14)
    {
        var erro;
        var a = [];
        var b = new Number;
        var c = [6,5,4,3,2,9,8,7,6,5,4,3,2];
        for (i=0; i<12; i++)
        {
            a[i] = pcnpj.charAt(i);
            b += a[i] * c[i+1];
        }
        if ((x = b % 11) < 2) 
        { 
            a[12] = 0 
        } 
        else { a[12] = 11-x }
        
        b = 0;
        
        for (y=0; y<13; y++) 
            {b += (a[y] * c[y]);}
        if ((x = b % 11) < 2) 
            { a[13] = 0; } 
        else { a[13] = 11-x; }
        if ((pcnpj.charAt(12) != a[12]) || (pcnpj.charAt(13) != a[13]))
            {erro = true; }
        if (erro)
        {   
            alert("CNPJ inválido!") ;
            cnpj.focus();
            cnpj.select();
            cnpjSemMascara(cnpj);
        } 
        else 
        {   
            cnpjComMascara(cnpj);
        }
    }
    else
    {
        alert("CNPJ inválido!") ;
        cnpj.focus();
        cnpj.select();
        cnpjSemMascara(cnpj);
    }
}

function cnpjSemMascara(cnpj)
{  Vr = cnpj.value;
   Vr = Vr.toString().replace( "-", "" );
   Vr = Vr.toString().replace( ".", "" );
   Vr = Vr.toString().replace( ".", "" );
   Vr = Vr.toString().replace( "/", "" );

   cnpj.value = Vr;
   cnpj.select();
}
function cnpjComMascara(cnpj)
{ var Vr
  var Tam
  Vr = cnpj.value;
  Vr = Vr.toString().replace( "-", "" );
  Vr = Vr.toString().replace( ".", "" );
  Vr = Vr.toString().replace( ".", "" );
  Vr = Vr.toString().replace( "/", "" );

  Tam = Vr.length ;
  if (Tam == 14) 
    { 
        Vr = Vr.substr(0, 2) + '.' + Vr.substr(2,3) + '.' + Vr.substr(5,3) + '/' + Vr.substr(8,4) + '-' + Vr.substr(12,2);
        cnpj.value = Vr;
    }
}

//TRATAMENTO DE CPF
function validarCpf(cpf)
{ 
    var pcpf= cpf.value;
    if (pcpf.length != 11) {sim=false}
    else {sim=true}
    
    if (sim )  // valida o primeiro digito
    { 
        for (i=0;((i<=(pcpf.length-1))&& sim); i++)
        { 
            val = pcpf.charAt(i);
            if ((val!="9")&&(val!="0")&&(val!="1")&&(val!="2")&&(val!="3")&&(val!="4")
                &&    
                (val!="5")&&(val!="6")&&(val!="7")&&(val!="8")) 
            {
                sim=false;
            }
        }
        if (sim)
        { 
            soma = 0;
            for (i=0;i<=8;i++)
            {
                val = eval(pcpf.charAt(i));
                soma = soma + (val*(i+1));
            }
            resto = soma % 11;
            if (resto>9) dig = resto -10;
            else  dig = resto
            if (dig != eval(pcpf.charAt(9))) { sim=false }
            else   // valida o segundo digito
            {
                soma = 0;
                for (i=0;i<=7;i++)
                {
                    val = eval(pcpf.charAt(i+1));
                    soma = soma + (val*(i+1));
                }
                soma = soma + (dig * 9);
                resto = soma % 11;
                if (resto>9) dig = resto -10;
                else  dig = resto
                if (dig != eval(pcpf.charAt(10))) 
                    { sim = false }
                else sim = true
             }
        }
    }
    if (!sim) 
    {   
        alert("CPF inválido!") ;
        cpf.focus();
        cpf.select();
        cpfSemMascara(cpf);
    } 
    else 
    {   
        cpfComMascara(cpf);
    }
}

function cpfSemMascara(cpf)
{  Vr = cpf.value;
   Vr = Vr.toString().replace( "-", "" );
   Vr = Vr.toString().replace( ".", "" );
   Vr = Vr.toString().replace( ".", "" );
   cpf.value = Vr;
   cpf.select();
}
function cpfComMascara(cpf)
{ var Vr
  var Tam
  Vr = cpf.value;
  Vr = Vr.toString().replace( "-", "" );
  Vr = Vr.toString().replace( ".", "" );
  Vr = Vr.toString().replace( ".", "" );
  Tam = Vr.length ;
  if (Tam == 11) 
    { 
        Vr = Vr.substr(0, 3) + '.' + Vr.substr(3,3) + '.' + Vr.substr(6,3) + '-' + Vr.substr(9,2);
        cpf.value = Vr;
    }
}

//Valida email
//function validarEmail(email)
//{ 
//    var mail = email.value;
//	if (mail != "")
//	{ var espac	= ' ';		
//	  Al = mail.indexOf(espac);	
//	  if (Al > 0) 
//	    { alert("E-mail inválido, não é permitido espaço!");
//	      email.focus();
//	      email.select();
//	    }
//	  else
//	    {
//	    var arroba	= "@";		
//	    Al = mail.indexOf(arroba);	
//	    if (Al < 0) 
//	      { alert("E-mail inválido, @ é necessário!");
//	        email.focus();
//	        email.select();
//	      }
//	    else
//	    {	cliente  =  mail.substring(0,Al);
//	      provedor =  mail.substring(Al+1,mail.length);
//	      ponto = ".";
//	      Pl	= provedor.indexOf(ponto);
//	      if (Pl < 0) 
//	      {
//		      alert("E-mail inválido, o ponto é necessário!");
//		      email.focus();
//		      email.select();
//	      }
//	      else
//	      {
//	        com = provedor.substring(Pl+1, provedor.length);
//	        if (com == "") 
//	        {
//		        alert("E-mail inválido, o provedor é necessário!");
//		        email.focus();;
//		        email.select();
//	        }
//	      }
//	    }
//    }
//  }
//}

