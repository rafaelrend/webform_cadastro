using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// Classe auxiliar para  contato_email
    /// </summary>
    public class ContatoEmail
    {
        public ContatoEmail()
        {
           


            
        }


         
  
         public static List<Entities.SimplesCodigoNome> listaPropriedade; 
         
        /// <summary> 
        /// Irá ler as colunas do banco de dados, e, de acordo ao nome da  
        /// coluna e o comentário, irá trazer como Código e Nome 
        /// Código - nome da coluna, Nome -> Comentário que existe no banco.. 
        /// </summary> 
        /// <returns></returns> 
		 public static List<SimplesCodigoNome> getPropriedades() 
		 { 
			 if ( Entities.ContatoEmail.listaPropriedade == null ) { 
				 List<Entities.SimplesCodigoNome> lst = new List<Entities.SimplesCodigoNome>();  
				   
				  
          lst.Add( new Entities.SimplesCodigoNome("id","ID") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("id_contato","id_contato") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("email","Email") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("ordem_cadastro","ordem_cadastro") );  
      
		 
				 Entities.ContatoEmail.listaPropriedade = lst; 
				 }
		 
                 return Entities.ContatoEmail.listaPropriedade; 
         }




          
 

    }
}
