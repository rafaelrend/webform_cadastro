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

public partial class controles_marcacao : UserControlCadastroBase, IField
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #region IField Members

    public string Value
    {
        get
        {
            return getValoresCheckList(chk_opcoes, ",");
        }
        set
        {

            base.setValoresCheckList(value, chk_opcoes);
        }
    }

    public string Width
    {
        set
        {
            chk_opcoes.Width = Unit.Parse(value);
        }
    }



    public string Auxiliar
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public string Auxiliar2
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
