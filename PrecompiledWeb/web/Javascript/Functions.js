// JScript File

function showMessage(msg)
{   
    //document.getElementById('divMessenger').style.color='red'
    document.getElementById('divMessenger').innerHTML=msg;
    if (msg != " ") setTimeout("showMessage(' ')",15000);
}

//Efeitos de mudança de cor da linha no grid ao passar o mouse
var cor;
var corTxt;
var cursor = 'pointer';
var bgColor = '#D2E9FF';
var txtColor = '#013231'




function mouseOver(obj, changeCursor) {
    cor = obj.style.backgroundColor;
    corTxt = obj.style.color;
    obj.style.backgroundColor = bgColor;
    obj.style.color = txtColor;
    if (changeCursor) {
        obj.style.cursor = cursor;
    }
}
function mouseOut(obj) {
    obj.style.backgroundColor = cor;
    obj.style.color = corTxt;
}

//TRATAR CAMPO HORA
function formataHora(hora)
{
    if (hora.value.length == 2)
	{
	   hora.value = hora.value + ":"
	}
}

 // Insere a máscara de CPF
function Mascara (formato, objeto)
     {
    campo = objeto;
    alert(campo);
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
function noSpace(event)
{
     Tecla = event.which;
     if(Tecla == null)
	 Tecla = event.keyCode;	
	 if (Tecla==32){ return false; }
	 else
	 {
		 return true; 
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
	
   	alert(Tecla);
	if((Tecla > 47 && Tecla <= 57)||((code==2)&&(Tecla==47))||((Tecla==44)||(Tecla==46)) ){
		return true;
		}
	else{
		return false;
		}
    return true;
}

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
function SoNum(event) /*So aceita numeros*/
{
    Tecla = event.which;
    if(Tecla == null)
	Tecla = event.keyCode;
   	//alert(Tecla);
	if(Tecla > 47 && Tecla <= 57){
		return true;
		}
	else{
		return false;
		}
    return true;
}


//LIMITAR CARACTERES
function limiteCaracter(txt, limite)
{
    
    if (txt.value.length >= limite)
	{
	   var str;
	   str = txt.value;
	   txt.value = str.substring(0, limite-1);
	}
}

//percorre a grid em busca dos checkboxes e marca ou desmarca todos
//function AllChecked(spanChk, itemChk)
//{ var oItem = spanChk.children;
//  var oBox = (spanChk.type=="checkbox") ? 
//  spanChk : spanChk.children.item[0];
//  xState = oBox.checked; elm = oBox.form.elements;
//  for(i=0;i<elm.length;i++)
//    {   var elmId = elm[i].id; var elmLen = elmId.length-itemChk.length;
//        elmId = elmId.substr(elmLen,itemChk.length);
//        if(elm[i].type=="checkbox" && elmId == itemChk)
//            {if(elm[i].checked!=xState) elm[i].checked = xState;}
//        if(elm[i].id!=spanChk.id && elmId != itemChk)
//            {elm[i].checked = false;}
//    }
//}


function txtBoxFormat(objeto, sMask, evtKeyPress) {
    var i, nCount, sValue, fldLen, mskLen,bolMask, sCod, nTecla;


if(document.all) { // Internet Explorer
    nTecla = evtKeyPress.keyCode;
} else if(document.layers) { // Nestcape
    nTecla = evtKeyPress.which;
} else {
    nTecla = evtKeyPress.which;
    if (nTecla == 8) {
        return true;
    }
}

    sValue = objeto.value;

    // Limpa todos os caracteres de formatação que
    // já estiverem no campo.
    sValue = sValue.toString().replace( "-", "" );
    sValue = sValue.toString().replace( "-", "" );
    sValue = sValue.toString().replace( ".", "" );
    sValue = sValue.toString().replace( ".", "" );
    sValue = sValue.toString().replace( "/", "" );
    sValue = sValue.toString().replace( "/", "" );
    sValue = sValue.toString().replace( ":", "" );
    sValue = sValue.toString().replace( ":", "" );
    sValue = sValue.toString().replace( "(", "" );
    sValue = sValue.toString().replace( "(", "" );
    sValue = sValue.toString().replace( ")", "" );
    sValue = sValue.toString().replace( ")", "" );
    sValue = sValue.toString().replace( " ", "" );
    sValue = sValue.toString().replace( " ", "" );
    fldLen = sValue.length;
    mskLen = sMask.length;

    i = 0;
    nCount = 0;
    sCod = "";
    mskLen = fldLen;

    while (i <= mskLen) {
      bolMask = ((sMask.charAt(i) == "-") || (sMask.charAt(i) == ".") || (sMask.charAt(i) == "/") || (sMask.charAt(i) == ":"))
      bolMask = bolMask || ((sMask.charAt(i) == "(") || (sMask.charAt(i) == ")") || (sMask.charAt(i) == " "))

      if (bolMask) {
        sCod += sMask.charAt(i);
        mskLen++; }
      else {
        sCod += sValue.charAt(nCount);
        nCount++;
      }

      i++;
    }

    objeto.value = sCod;

    if (nTecla != 8) { // backspace
      if (sMask.charAt(i-1) == "9") { // apenas números...
        return ((nTecla > 47) && (nTecla < 58)); }
      else { // qualquer caracter...
        return true;
      }
    }
    else {
      return true;
    }
  }

/*
</script>

<body>
Telefone:<input type="text" size="20" onkeypress="return txtBoxFormat(this, '(99)9999-9999', event);">
</body>
*/