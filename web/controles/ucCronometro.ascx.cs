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

/* Author: Rafael Álvares Rend
 * Creation Date: 2012-02-01
 * Description: Chronometer - This UserControl simulates a chronometer, with buttons: Play, Pause and Reset
 * 
 * 
 */
public partial class controles_ucCronometro : System.Web.UI.UserControl, IField
{

    protected void Page_Init(object sender, EventArgs e)
    {

        this.TextBox1.Attributes.Remove("onkeypress");
        TextBox1.Attributes.Add("onkeypress", "return mascara(event,this,'##:##');");

        botaoplay.Attributes.Remove("onclick");
        botaoplay.Attributes.Add("onclick", "crono_play('"+TextBox1.ClientID+"','"+botaoplay.ClientID+"','"+botaopause.ClientID+"'); ");

        botaopause.Attributes.Remove("onclick");
        botaopause.Attributes.Add("onclick", "crono_pause('"+TextBox1.ClientID+"','"+botaoplay.ClientID+"','"+botaopause.ClientID+"'); ");
        
        botaoreset.Attributes.Remove("onclick");
        botaoreset.Attributes.Add("onclick", "crono_reset('" + TextBox1.ClientID + "','" + botaoplay.ClientID + "','" + botaopause.ClientID + "'); ");

        try
        {
            //TextBox1.Attributes.Remove("readonly");
            //TextBox1.Attributes.Add("readonly", "readonly");

        }
        catch { }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Value
    {
        get
        {
            return this.TextBox1.Text;
        }
        set
        {
            this.TextBox1.Text = value;
        }
    }
    public string Auxiliar
    {
        get
        {
            return this.TextBox1.Text;
        }
        set
        {
            this.TextBox1.Text = value;
        }
    }
}
