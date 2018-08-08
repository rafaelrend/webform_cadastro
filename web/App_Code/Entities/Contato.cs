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
    /// Classe auxiliar para  Contato
    /// </summary>
    public class Contato
    {
        public Contato()
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
			 if ( Entities.Contato.listaPropriedade == null ) { 
				 List<Entities.SimplesCodigoNome> lst = new List<Entities.SimplesCodigoNome>();  
				   
				  
          lst.Add( new Entities.SimplesCodigoNome("id","ID") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("nome","Nome") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("empresa","Empresa") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("telefone_pessoal","Tel. Pessoal") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("telefone_comercial","Tel. Comercial") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("emails","Emails") );  
      
 
          lst.Add( new Entities.SimplesCodigoNome("data_cadastro","Data Cadastro") );  
      
		 
				 Entities.Contato.listaPropriedade = lst; 
				 }
		 
                 return Entities.Contato.listaPropriedade; 
         }




          
 

    }
}
