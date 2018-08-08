using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for IFieldDataHora
/// </summary>
public interface IFieldDataHora
{
	
		bool mostraHora{ get; set; }


    bool PodeEditar { get; set; }


    bool visualizaColuna2 { get; set; }

    DateTime? DataInicio { get; set; }

    DateTime? DataFim { get; set; }

    string textoDataInicio { get; set; }


    string textoDataFim { get; set; }

    TextBox getTxtHora1();

    TextBox getTxtHora2();

}
