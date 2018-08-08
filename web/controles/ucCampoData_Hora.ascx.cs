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

public partial class controles_ucCampoData_Hora : UserControlCadastroBase, IField
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setMaskData(txtDtData);
        setMaskHora(txtHora);


        if (sPkId > 0)//Escondo o campo hora..
        {
            td_hora.Visible = false;
        }
    }

    public bool HabilitaCalendario
    {
        set
        {
            CalendarExtender1.Enabled = value;
        }
    }

    #region IField Members

    public string Value
    {
        get
        {
            string saida = string.Empty;

            if (getValor(txtDtData) != String.Empty)
                saida = getValor(txtDtData);

            if (getValor(txtHora) != String.Empty)
                saida += " " + getValor(txtHora);

            return saida;

        }
        set
        {
            string data = string.Empty;
            string hora = string.Empty;
            if (value.IndexOf(" ") > -1)
            {
                try
                {
                    string[] ar = value.Split(' ');

                    data = ar[0];
                    hora = ar[1].Substring(0,5);
                }
                catch { }
            }

            setValor(txtDtData, data);
            setValor(txtHora, hora);
        }
    }

    public string Auxiliar
    {
        get
        {
            return getValor(txtHora);
        }
        set
        {
            setValor(txtHora, value);
        }
    }

    #endregion
}
