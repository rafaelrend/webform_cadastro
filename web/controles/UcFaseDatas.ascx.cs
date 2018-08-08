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

public partial class UserControls_UcFaseDatas : UserControlCadastroBase, IField, IFieldDataHora
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.setMaskData(txtDtFim);
        base.setMaskData(txtDtInicio);

        base.setMaskHora(txtDtInicioHora, 5);
        base.setMaskHora(txtDtFimHora, 5);
    }

    public string Titulo
    {
        set {
            this.tdTitulo.InnerHtml = value;
            trTitulo.Visible = true;
        }

    }

    public bool mostraHora
    {
        get
        {
            return txtDtInicioHora.Visible;
        }
        set
        {
            txtDtInicioHora.Visible = value;
            txtDtFimHora.Visible = value;
        }
    }


    private bool verBotaoEditar = false;

    public bool VerBotaoEditar
    {
        get { return verBotaoEditar; }
        set { verBotaoEditar = value;
        //tdEditar.Visible = value;
        }
    }

    public EventHandler onEditar;

    /// <summary>
    /// Indica se os campos estarão permitidos para edição ou não..
    /// </summary>
    public bool PodeEditar
    {
        get {
            if (ViewState["PodeEditar"] == null)
            {
                ViewState["PodeEditar"] = false;
            }


            return (Boolean)ViewState["PodeEditar"];

        }
        set
        {
            ViewState["PodeEditar"] = value;
            setaPermissaoEditar(value);
        
        }
    }

    private bool quebra = false;

    public bool Quebra
    {
        get { return quebra; }
        set { quebra = value;
        //if (quebra)
        //{
        //    spanQubra.InnerHtml = "<br>";
        //    spanQubra2.InnerHtml = "<br>";
        //}
        //else
        //{
        //    spanQubra.InnerHtml = "";
        //    spanQubra2.InnerHtml = "";
        //}
        }
    }
    public bool visualizaColuna2
    {
        get { return tdCol2.Visible; }
        set { tdCol2.Visible = value; }
    }


    private void setaPermissaoEditar(bool perm)
    {
        if (perm)
        {
            txtDtFim.Attributes.Remove("readonly");
            txtDtInicio.Attributes.Remove("readonly");


            CalendarExtender1.Enabled = true;
            CalendarExtender2.Enabled = true;

        }
        else
        {

            txtDtFim.Attributes.Add("readonly","");
            txtDtInicio.Attributes.Add("readonly","");

            CalendarExtender1.Enabled = false;
            CalendarExtender2.Enabled = false;

        }



    }

    public bool forcaHora = false;
    public bool ForcaHora
    {
        get
        {
            return forcaHora;
        }
        set { forcaHora = value; }
    }




    public DateTime? DataInicio
    {
        get {
            //if (forcaHora)
            //    return getDate(txtDtInicio, true, " 00:00");
            //else
                return getDate(txtDtInicio,true, base.Nvl( getValor(txtDtInicioHora),"00:00").ToString());
        
        }
        set {
            if (value == null)
                txtDtInicio.Text = String.Empty;
            else
                setValor(txtDtInicio, value.Value.ToShortDateString());
        }
    }

    public DateTime? DataFim
    {
        get {
            //if (forcaHora)
            //    return getDate(txtDtFim, true, " 23:59"); 
            //else
                return getDate(txtDtFim, true, base.Nvl( getValor(txtDtFimHora),"23:59").ToString());
        }
        set
        {
            if (value == null)
                txtDtFim.Text = String.Empty;
            else
                setValor(txtDtFim, value.Value.ToShortDateString());
        }
    }

    public string textoDataInicio
    {
        get
        {
            return Label4.Text;
        }
        set
        {
            Label4.Text = value;
        }
    }
    public string textoDataFim
    {
        get
        {
            return Label6.Text;
        }
        set
        {
            Label6.Text = value;
        }
    }



    protected void imgEditar_Click(object sender, ImageClickEventArgs e)
    {
        //if (onEditar != null)
        //    onEditar(imgEditar, EventArgs.Empty);
    }

    public TextBox getTxt1()
    {
        return this.txtDtInicio;
    }

    public TextBox getTxtHora1()
    {
        return this.txtDtInicioHora;
    }

    public TextBox getTxt2()
    {
        return this.txtDtFim;
    }

    public TextBox getTxtHora2()
    {
        return this.txtDtFimHora;
    }

    #region IField Members

    public string Value
    {
        get
        {
            if (txtDtInicio.Text.Trim() == String.Empty &&
                 txtDtFim.Text.Trim() == String.Empty)
                return String.Empty;

            return txtDtInicio.Text.Trim() + "|" + txtDtFim.Text.Trim();
        }
        set
        {
            if (value.IndexOf("|") < 0)
            {
                txtDtInicio.Text = String.Empty;
                txtDtFim.Text = String.Empty;
                return;
            }
            string[] args = value.Split('|');

            txtDtInicio.Text = args[0];
            txtDtFim.Text = args[1];

        }
    }

    public string Auxiliar
    {
        set
        {
            //Removo os calendários..
            if (value == "N")
            {
                CalendarExtender1.Enabled = false;
                CalendarExtender2.Enabled = false;
            }



        }
        get
        {
            return String.Empty;
        }
    }
   

    #endregion
}
