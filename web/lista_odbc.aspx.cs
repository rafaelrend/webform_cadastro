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
using System.Collections.Generic;

public partial class importacao_lista_odbc : PageCadastroBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Collections.Generic.List<string> drivers = GetSystemDriverList();

            for (int i = 0; i < drivers.Count; i++)
            {
                Response.Write("<br>" + drivers[i]);

            }

            drivers = listaVersoesNet();

            Response.Write("<br><br><b>Versões do .Net Instaladas</b>");
            for (int i = 0; i < drivers.Count; i++)
            {
                Response.Write("<br>" + drivers[i]);

            }
        }
        catch { }


        String path = Server.MapPath(".") + "\\estrutura_banco.xls";

        DataTable dt =  ExcelLibrary.DataSetHelper.CreateDataTable(path, "contato");

        Response.Write("Quantidade de rows " + dt.Rows.Count.ToString());

        Control myFiltro = Page.LoadControl("~/controles/ucFiltroBasico.ascx");
        myFiltro.ID = "UcFiltroBasico1";

        IFiltro filt = (IFiltro)myFiltro;

        if (filt != null)
        {

            filt.Tabela = "contato";
            filt.NoClearAction = String.Empty;
            filt.CaminhoExcel = path;
            filt.carregaCamposFiltro();

            div_filtro.Controls.Add(filt);

        }
    }


    public List<string> listaVersoesNet()
    {

      // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP

        List<string> names = new List<string>();
        // get system dsn's
        Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.LocalMachine).OpenSubKey("Software");
        
        if (reg != null)
        {
            reg = reg.OpenSubKey("Microsoft");
            if (reg != null)
            {
                reg = reg.OpenSubKey("NET Framework Setup");
                if (reg != null)
                {

                    reg = reg.OpenSubKey("NDP");
                    if (reg != null)
                    {

                        //Response.Write("<br>Aqui, o NDP achou algo.. ");
                        // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                        //GetValueNames
                        foreach (string sName in reg.GetSubKeyNames() )
                        {

                           // Response.Write("<br>" + sName);

                            //if (sName.ToLower().IndexOf("sql") > -1)
                            //{
                            names.Add(sName);
                            //}
                        }
                    }
                    try
                    {
                        reg.Close();
                    }
                    catch { /* ignore this exception if we couldn't close */ }
                }
            }
        }

        return names;
    }
}
