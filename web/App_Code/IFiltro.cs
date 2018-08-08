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
/// Summary description for IFiltro
/// </summary>
public abstract class IFiltro   : UserControlCadastroBase
{
    public abstract  void carregaCamposFiltro();

    public abstract  void carregaCamposFiltro2();

    public abstract void loadFields();
    

    public abstract string CaminhoExcel { get; set; }

    public abstract string Tabela { get; set; }

    public abstract void addHiddenFiltro(string nome, string valor);

    public abstract string Alias { get; set; }

    public abstract string NoClearAction { get; set; }

    /// <summary>
    /// obtém o sql referente ao filtro..
    /// </summary>
    /// <returns></returns>
    public abstract string getSqlFiltro();

    public abstract void garantePostBack();

    public abstract bool TemFiltro { get; set; }

    public abstract bool bloquear { set; }

    public abstract void setFiltroPadrao(System.Collections.Specialized.NameValueCollection valoresPadrao);

}
