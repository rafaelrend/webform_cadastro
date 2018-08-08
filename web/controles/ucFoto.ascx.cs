using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class controles_ucFoto : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        File1.Attributes.Remove("onchange");

        File1.Attributes.Add("onchange",
                     " upload_imagem('" +
                       Image1.ClientID + "'," +
                       "'" + File1.ClientID + "','" +
                       divMostraFile.ClientID + "','" +
                       divLoad.ClientID + "','" + this.Pasta + "'," +
                       "'" + hd_imagem.ClientID + "',"+
                       "'"+hd_unique_name.ClientID+"');");

        HyperLink1.Attributes.Remove("onclick");
        HyperLink1.Attributes.Add("onclick", "mostraDiv('" + divMostraFile.ClientID + "');");

        HyperLink1.Attributes.Remove("style");
        HyperLink1.Attributes.Add("style", "cursor:pointer");

        LinkButton1.Attributes.Remove("style");
        LinkButton1.Attributes.Add("style", "cursor:pointer; text-decoration: none");
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (Page.IsPostBack)
        {
            if (hd_imagem.Value.Trim() != String.Empty)
            {
                Image1.ImageUrl = "~/" + hd_imagem.Value;
            }

        }

    }
    private string url_clean = "~/icons/no-photo2.png";
    public void limpar()
    {
        Image1.ImageUrl = url_clean;
        hd_imagem.Value = String.Empty;

    }

    public void setImagem(object valor)
    {
        if (valor == null)
            return;

        if (valor.ToString() == String.Empty)
            return;

        Image1.ImageUrl = "~/" + Pasta + "/" + valor.ToString();
        hd_imagem.Value = Pasta + "/" + valor.ToString();

    }
    public string Pasta
    {
        set
        {
            hd_pasta.Value = value;
        }
        get
        {
            return hd_pasta.Value;
        }
    }

    public string Imagem
    {
        set
        {
            hd_imagem.Value = value;
            Image1.ImageUrl = value;

        }
        get
        {
            return trazNome(hd_imagem.Value.Replace("/","\\"));
        }

    }

    public string trazNome(string str)
    {
        string[] arp = str.Split('\\');

        if (arp.Length <= 0)
            return str;

        return arp[arp.Length - 1];

    }

    public string UniqueName{
        set
        {
            hd_unique_name.Value = value;
        }
   }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        limpar();
    }
}
