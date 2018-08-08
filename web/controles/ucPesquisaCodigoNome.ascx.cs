using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DataAccess;

public partial class controles_ucPesquisaCodigoNome : UserControlCadastroBase, IField
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if ( txtDescricao.CssClass == String.Empty )
              setReadOnly(txtDescricao);

        carregaLink();

    }

    private void carregaLink()
    {
        /*
         *  <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server"  
    TargetControlID="txtDescricao"  
    WatermarkText="Clique no botão ao lado para pesquisar ->"  
    WatermarkCssClass="watermarked" /> */

        string strPesquisa = "";
        string sep = "?";

        if (hdPaginaPesquisa.Value.IndexOf("?") > -1)
            sep = "&";

        strPesquisa += this.hdPaginaPesquisa.Value + sep +"cr_id=" + hdIDRegistro.ClientID + "&cr_cod=" + txtCodigo.ClientID + "&cr_descr=" + txtDescricao.ClientID + "&fromUC=1";

        Control txtIdColigada  =   Utilities.Format.localizaControl("txtIdColigada", this.Page.Form);

        if (txtIdColigada != null)
        {
            strPesquisa += "&coligada='+document.getElementById('" + txtIdColigada.ClientID + "').value+'";
        }

        //if (Auxiliar2 != String.Empty)
        //    strPesquisa += "&coligada=" + Auxiliar2.ToString();

        Image1.Attributes.Remove("style");
        Image1.Attributes.Remove("onclick");


        Image1.Attributes.Add("style", "cursor:pointer");
        Image1.Attributes.Add("onclick", "window.open('" + strPesquisa + "','cons" + this.Entidade + "')");


        if (this.Entidade != String.Empty)
        {
            txtCodigo.ToolTip = "Código " + this.Entidade;
            txtDescricao.ToolTip = "Nome " + this.Entidade;

        }

        this.txtCodigo.AutoPostBack = true;
        this.txtCodigo.TextChanged += new EventHandler(txtCodigo_TextChanged);


    }

    void txtCodigo_TextChanged(object sender, EventArgs e)
    {
        carregaNome("0");
    }

    #region IField Members

    public string Value
    {
        get
        {
            return hdIDRegistro.Value;
        }
        set
        {
            hdIDRegistro.Value = value;
            carregaNome("1");
        }
    }

    public TextBox getTxtCodigo()
    {
        return this.txtCodigo;
    }


    public TextBox getTxtDescricao()
    {
        return this.txtDescricao;
    }

    private void carregaNome(string opcao)
    {

        if ( getValor(txtCodigo)== String.Empty && opcao != "1" )
        {
                setValor(txtDescricao,String.Empty );
                setValor(hdIDRegistro,String.Empty);
                return;

        }

        IDbPersist oConn = ConnAccess.getConn();


        string filtro = " and " + this.ColunaCodigoConsulta + " = " + valorFormatado();

        if (opcao == "1")
        {

            filtro = " and " + this.ColunaIDConsulta + " = " + getValor(hdIDRegistro);
        }

        Control txtIdColigada = Utilities.Format.localizaControl("txtIdColigada", this.Page.Form);

        if (txtIdColigada != null && getValor(txtIdColigada) != String.Empty)
        {
            filtro += " and CODCOLIGADA = " + getValor(txtIdColigada);
        }

        if (TabelaConsulta.Substring(0, 1) == ".")
        {
           // TabelaConsulta = SessionFacade.getApp("bancoRM") + TabelaConsulta;
        }

        DataTable dt =  ConnAccess.fetchData(oConn, "select " + this.ColunaIDConsulta + " as id, " +
            this.ColunaCodigoConsulta +" as cod, " +
             this.ColunaDescricao + " as descr  from " + this.TabelaConsulta + " where 1 = 1 " + filtro);

        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];

            setValor(txtCodigo, dr["cod"]);
            setValor(hdIDRegistro, dr["id"]);
            setValor(txtDescricao, dr["descr"]);

            if (DispararAposLocalizar != null)
            {
                DispararAposLocalizar(dr, EventArgs.Empty);
            }

        }
        else
        {
            Alert("Registro não localizado!");
            setValor(txtDescricao, String.Empty);
            setValor(hdIDRegistro, String.Empty);
            return;

        }


    }

    private string valorFormatado()
    {
        if (this.ehTexto == "1")
            return "'" + this.Auxiliar + "'";

        return this.Auxiliar;
    }

    public string Auxiliar
    {
        get
        {
            return txtCodigo.Text;
        }
        set
        {
            txtCodigo.Text = value;
            carregaNome("2");
        }
    }

    public string Auxiliar2
    {
        get
        {
            if (ViewState["_auxiliar2"] == null)
                ViewState["_auxiliar2"] = String.Empty;

            return ViewState["_auxiliar2"].ToString();
        }
        set
        {
            ViewState["_auxiliar2"] = value;
            carregaLink();
        }
    }

    public string TabelaConsulta
    {
        set
        {
            hdTabela.Value = value;
        }
         get
        {
           return  hdTabela.Value;
        }
    }
    /// <summary>
    /// Coluna por onde deverá consultar o código
    /// </summary>
    public string ColunaCodigoConsulta
    {
        set
        {
            hdColunaCodigoConsulta.Value = value;
        }
        get
        {
            return hdColunaCodigoConsulta.Value;
        }
    }
    /// <summary>
    /// Coluna por onde deverá consultar a ID
    /// </summary>
    public string ColunaIDConsulta
    {
        set
        {
            hdColunaIDConsulta.Value = value;
        }
        get
        {
            return hdColunaIDConsulta.Value;
        }
    }
    /// <summary>
    /// Indica se o valor do código deve ser pesquisado comoum texto 1 - Sim, 0 -Não
    /// </summary>
    public string ehTexto
    {
        set
        {
            hdEhTexto.Value = value;
        }
        get
        {
            return hdEhTexto.Value;
        }
    }
    /// <summary>
    /// Coluna por onde deverá consultar a ID
    /// </summary>
    public string ColunaDescricao
    {
        set
        {
            hdColunaDescricao.Value = value;
        }
        get
        {
            return hdColunaDescricao.Value;
        }
    }

    #endregion

    /// <summary>
    /// Nome do que estamos pesquisando
    /// </summary>
    public string Entidade
    {
        set
        {
            hdEntidade.Value = value;
        }
        get
        {
            return hdEntidade.Value;
        }
    }


    /// <summary>
    /// CSS do campo de descrição
    /// </summary>
    public string CssDescricao
    {
        set
        {
             txtDescricao.CssClass = value;
             txtDescricao.Attributes.Remove("readonly");
        }
        get
        {
            return txtDescricao.CssClass;
        }
    }



    /// <summary>
    /// Nome da página por onde fazemos nossas pesquisas
    /// </summary>
    public string Descricao
    {
        set
        {
            tdDesc.InnerHtml = value;
            if (value == "")
                tdDesc.Visible = false;
        }
        get
        {
            return tdDesc.InnerHtml;
        }
    }


    /// <summary>
    /// Nome da página por onde fazemos nossas pesquisas
    /// </summary>
    public string PaginaPesquisa
    {
        set
        {
            hdPaginaPesquisa.Value = value;
        }
        get
        {
            return hdPaginaPesquisa.Value;
        }
    }
    public string TamanhoDescricao
    {
        set
        {
            tdTab2.Width = value;
           // txtDescricao.Width = Unit.Parse("98%");
            txtDescricao.Width = Unit.Parse(value);
        }
    }
    public string TamanhoCodigo
    {
        set
        {
            tdTab.Width = value;
            // txtCodigo.Width = Unit.Parse("96%");
            txtCodigo.Width = Unit.Parse(value);
        }
    }
    public string TamanhoTabela
    {
        set
        {
            tbDados.Width = value;
        }
    }

    public void escondeLinkPesquisa()
    {
        Image1.Visible = false;
    }

    public event EventHandler DispararAposLocalizar;
}
