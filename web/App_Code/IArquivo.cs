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
/// Summary description for IArquivo
/// </summary>
public interface IArquivo
{
    string SizeOfFile { get; set; }

    string Type { get; set; }
    //string Width { get; set; }
    string File { get; set; }
    string Separador { get; set; }
    string UrlPost { get; set; }
    string Pasta { get; set; }

    void setFile(object valor);
    void carregaDados_ini();

    string RetornoTipo { get; set; }

    int PkId { get; set; }
    
    
}
