// JScript File

function prevent_enter(){

   var tables = document.getElementsByTagName("table");

      for ( var i = 0; tables.length; i++ ){
      
          if ( tables[i].id.indexOf("tbCadastro") > -1 ){
                prevent_enter_bytable( tables[i] );
                break;
          }
      }
}
/*
function prevent_enter_bytable(table){

    
   var inputs = table.getElementsByTagName("input");


              var onkeypress_function() = function(inputs){

                                  for ( var i = 0; inputs.length; i++ ){
                                  
                                           if ( inputs[i].type == "text" || inputs[i].type == "textarea"  || inputs[i].type == "select" ){
                                           
                                              if ( inputs[i].
                                           }
                                  }
                  
                  }
}
*/

function prevent_enter_input(event, input){

  if ( input.type == "textarea" )
    return true;

   
    Tecla = event.which;
    if(Tecla == null)
	Tecla = event.keyCode;
      
    if ( Tecla == 13 )
       return false;
       
       
       return true;  
      

}