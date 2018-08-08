using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using js = Utilities.JavaScript;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Globalization;
using DataAccess;

/// <summary>
/// Summary description for PageCadastroBase
/// </summary>
public class PageCadastroBase : System.Web.UI.Page
{

    protected bool validaLoginSenha = true;
    public string Iif(bool situacao, string verdadeiro, string falso)
    {
        if (situacao)
            return verdadeiro;
        else
            return falso;

    }

    /// <summary>
    /// Escreve um datatable em formato de tabela HTML
    /// </summary>
    /// <param name="dt"></param>
    public virtual void descreveTabela(DataTable dt)
    {

        Response.Write("<table border='1'>");
        Response.Write("<tr>");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            Response.Write("\n <th>" + dt.Columns[i].ColumnName + "</th> ");
        }

        Response.Write("</tr>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Response.Write("\n \n <tr>");
            for (int z = 0; z < dt.Columns.Count; z++)
            {
                Response.Write("\n <td>" + dt.Rows[i][dt.Columns[z].ColumnName].ToString() + "</td> ");
            }

            Response.Write("\n</tr>");
        }


        Response.Write("<table>");
    }


    /// <summary>
    /// Traz o HTML de um DataTable
    /// 
    /// </summary>
    /// <param name="targetTable"></param>
    /// <returns></returns>
    public static string ConvertToHtmlFile(DataTable targetTable)
    {
        string myHtmlFile = "";



        if (targetTable == null)
        {
            throw new System.ArgumentNullException("targetTable");
        }
        else
        {
            //Continue.
        }



        //Get a worker object.
        System.Text.StringBuilder myBuilder = new System.Text.StringBuilder();



        //Open tags and write the top portion.

        myBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
        myBuilder.Append("style='border: solid 1px Silver;'>");



        //Add the headings row.



        myBuilder.Append("<tr align='left' valign='top'>");



        foreach (DataColumn myColumn in targetTable.Columns)
        {
            myBuilder.Append("<th>");
            myBuilder.Append(myColumn.ColumnName);
            myBuilder.Append("</th>");
        }



        myBuilder.Append("</tr>");



        //Add the data rows.
        foreach (DataRow myRow in targetTable.Rows)
        {
            myBuilder.Append("<tr align='left' valign='top'>");



            foreach (DataColumn myColumn in targetTable.Columns)
            {
                myBuilder.Append("<td align='left' valign='top'>");
                myBuilder.Append(myRow[myColumn.ColumnName].ToString());
                myBuilder.Append("</td>");
            }



            myBuilder.Append("</tr>");
        }



        //Close tags.
        myBuilder.Append("</table>");



        //Get the string for return.
        myHtmlFile = myBuilder.ToString();



        return myHtmlFile;
    }

    /// <summary>
    /// Vai trazer todos os elementos dentro de um controle, com o nome = valor
    /// </summary>
    /// <param name="controlP"></param>
    /// <param name="sep"></param>
    /// <returns></returns>
    public static void getValoresDoControle(ref string texto, Control controlP, string sep)
    {

        foreach (Control ctl in controlP.Controls)
        {
            if (ctl is TextBox)
            {
                if (((TextBox)ctl).Visible == true)
                {
                    texto += sep + ((TextBox)ctl).ID + "=" + ((TextBox)ctl).Text;
                }
            }
            if (ctl is DropDownList)
            {
                if (((DropDownList)ctl).Visible == true)
                {
                    texto += sep + ((DropDownList)ctl).ID + "=" + ((DropDownList)ctl).SelectedValue;
                }
            }
            if (ctl is CheckBox)
            {
                if (((CheckBox)ctl).Visible == true)
                {
                    texto += sep + ((CheckBox)ctl).ID + "=" + ((CheckBox)ctl).Checked.ToString();
                }
            }
            if (ctl is HtmlInputCheckBox)
            {
                if (((HtmlInputCheckBox)ctl).Checked == true)
                {
                    texto += sep + ((HtmlInputCheckBox)ctl).ID + "=" + ((HtmlInputCheckBox)ctl).Checked.ToString();
                }
            }
            if (ctl is HiddenField)
            {

                texto += sep + ((HiddenField)ctl).ID + "=" + ((HiddenField)ctl).Value;

            }
            if (ctl.Controls.Count > 0)
            {
                getValoresDoControle(ref texto, ctl, sep);
            }
        }
    }

    /// <summary>
    /// Transforma uma string em um NameValueCollection 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sep1"></param>
    /// <param name="sep2"></param>
    /// <returns></returns>
    public static  System.Collections.Specialized.NameValueCollection getNameValueByString(string str, string sep1, string sep2)
    {
        System.Collections.Specialized.NameValueCollection arr = new System.Collections.Specialized.NameValueCollection();

        string[] ar = str.Split(new string[] { sep1 }, System.StringSplitOptions.None);

        for (int i = 0; i < ar.Length; i++)
        {
            string[] col = ar[i].Split(new string[] { sep2 }, System.StringSplitOptions.None); ;

            arr.Add(col[0], col[1]);
        }

        return arr;

    }


    /// <summary>
    ///Seta valores de todos os elementos dentro de um controle, com o nome = valor
    /// </summary>
    /// <param name="controlP"></param>
    /// <param name="sep"></param>
    /// <returns></returns>
    public void setValoresDoControle(Control controlP, System.Collections.Specialized.NameValueCollection cole)
    {

        foreach (Control ctl in controlP.Controls)
        {
            if (ctl is TextBox)
            {
                if (((TextBox)ctl).Visible == true)
                {
                    if (cole[((TextBox)ctl).ID] != null)
                        setValor(ctl, cole[((TextBox)ctl).ID]);
                }
            }
            if (ctl is DropDownList)
            {
                if (((DropDownList)ctl).Visible == true)
                {

                    if (cole[((DropDownList)ctl).ID] != null)
                        setValor(ctl, cole[((DropDownList)ctl).ID]);
                }
            }
            if (ctl is CheckBox)
            {
                if (((CheckBox)ctl).Visible == true)
                {
                    if (cole[((CheckBox)ctl).ID] != null)
                        setValor(ctl, cole[((CheckBox)ctl).ID]);
                }
            }
            if (ctl is HiddenField)
            {

                if (cole[((HiddenField)ctl).ID] != null)
                    setValor(ctl, cole[((HiddenField)ctl).ID]);

            }
            if (ctl.Controls.Count > 0)
            {
                setValoresDoControle(ctl, cole);
            }
        }
    }




    /// <summary>
    /// Efetua a formataçao de uma informação, para o link do git hub editor
    /// </summary>
    public static string formataLinkGitHUB(string dado)
    {
        return "<a contenteditable=\"false\" class=\"custom\" title=\"" + dado + "\" href=\"#\" tabindex=\"-1\">@" + dado + "</a> ";

    }


    /// <summary>
    /// Localiza quais drivers odbc temos no servidor..
    /// </summary>
    /// <returns></returns>
    public static List<String> GetSystemDriverList()
    {
        List<string> names = new List<string>();
        // get system dsn's
        Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.LocalMachine).OpenSubKey("Software");
        if (reg != null)
        {
            reg = reg.OpenSubKey("ODBC");
            if (reg != null)
            {
                reg = reg.OpenSubKey("ODBCINST.INI");
                if (reg != null)
                {

                    reg = reg.OpenSubKey("ODBC Drivers");
                    if (reg != null)
                    {
                        // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                        foreach (string sName in reg.GetValueNames())
                        {
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
    public static string nome_driver_excel = "";
    /// <summary>
    /// Traz o driver odbc para excel
    /// </summary>
    /// <returns></returns>
    public static string getExcelDriver()
    {
        if (nome_driver_excel != "")
            return nome_driver_excel;


        System.Collections.Generic.List<string> drivers = GetSystemDriverList();

        for (int i = drivers.Count-1; i >= 0 ; i--)
        {
            if (drivers[i].ToLower().IndexOf("excel") > -1)
            {
                nome_driver_excel = drivers[i];
                return nome_driver_excel;
                break;
            }

        }

        nome_driver_excel = "Microsoft Excel Driver (*.xls)";
        return nome_driver_excel;
    }


    /// <summary>
    /// obtem o filtro principal, a partir do masterpage
    /// </summary>
    /// <returns></returns>
    public IFiltro getFiltroPrincipal()
    {


       System.Web.HttpContext Gcontext = System.Web.HttpContext.Current;

       Control myFiltro = null;

       if (Gcontext.Items["_ucFiltro"] == null)
       {
           myFiltro = Page.LoadControl("~/controles/ucFiltroBasico.ascx");
           myFiltro.ID = "UcFiltroBasico1";

           Gcontext.Items["_ucFiltro"] = myFiltro;

       }
      
           return ((IFiltro)Gcontext.Items["_ucFiltro"]);
       
        
        //myFiltro = Page.LoadControl("controles/ucFiltroBasico.ascx");
        //myFiltro.ID = "UcFiltroBasico1";

        //Control td_g_filtro = Utilities.Format.localizaControl("td_g_filtro", this.Form);

        //if (td_g_filtro != null)
        //    td_g_filtro.Controls.Add(myFiltro);

        //if (myFiltro != null)
        //{
        //    return ((IFiltro)myFiltro);
        //}
        
        return null;
    }


    /// <summary>
    /// Seta permissões de acesso e carrega o filtro..
    /// </summary>
    /// <param name="tabela"></param>
    /// <param name="funcionalidade"></param>
    public void setaPermissao(string tabela, string funcionalidade)
    {
        setaPermissao(tabela, funcionalidade, "estrutura_banco.xls",String.Empty);
    }


    /// <summary>
    /// Seta permissões de acesso e carrega o filtro..
    /// </summary>
    /// <param name="tabela"></param>
    /// <param name="funcionalidade"></param>
    public void setaPermissao(string tabela, string funcionalidade, string caminho)
    {
        setaPermissao(tabela, funcionalidade, caminho, String.Empty);
    }

    /// <summary>
    /// Seta permissões de acesso e carrega o filtro..
    /// </summary>
    /// <param name="tabela"></param>
    /// <param name="funcionalidade"></param>
    public void setaPermissao(string tabela, string funcionalidade, string caminho, string NoClearAction)
    {

        Control myFiltro = (Control)getFiltroPrincipal();

            if (myFiltro != null)
            {
                IFiltro filt = (IFiltro)myFiltro;

                if (filt != null)
                {

                    filt.Tabela = tabela;
                    filt.NoClearAction = NoClearAction;
                    filt.CaminhoExcel = Server.MapPath(caminho);
                    try
                    {
                        if ( filt != null )
                            filt.carregaCamposFiltro();
                    }
                    catch { }

                    Control td_g_filtro = Utilities.Format.localizaControl("td_g_filtro", this.Form);

                    if (td_g_filtro != null)
                        td_g_filtro.Controls.Add(myFiltro);

                    //Control td_g_filtro = Utilities.Format.localizaControl("td_g_filtro", this.Form);

                    //if (td_g_filtro != null)
                    //    td_g_filtro.Controls.Add(myFiltro);
                }
            }



    }
    public void limpaExcessoarquivosTmp(string path)
    {
        string pasta = Server.MapPath(path);

        string[] files = System.IO.Directory.GetFiles(pasta);

        for (int i = 0; i < files.Length; i++)
        {

            try
            {
                string arquivo = pasta + "\\" + files[i];

                System.IO.FileInfo fil = new System.IO.FileInfo(arquivo);

                if (fil.CreationTime < (DateTime.Now.AddDays(-1)))
                {
                    System.IO.File.Delete(fil.FullName);

                }

            }
            catch { }
        }

    }

  
    protected int getIntValidoRamdom()
    {

        int rand = new Random().Next(20);

        try
        {

            if (System.IO.File.Exists(Server.MapPath("tmp/exp_tab" + rand.ToString() + ".xls")))
            {
                System.IO.File.Delete(Server.MapPath("tmp/exp_tab" + rand.ToString() + ".xls"));
            }
            return rand;
        }
        catch
        {
            return getIntValidoRamdom();
        }


    }

    protected void processaConsulta(object botao, object dataSource, GridView grv)
    {
        processaConsulta(botao, dataSource, grv, "tmp");


    }

    /// <summary>
    /// Executa algum processament ode acordo ao tipo de botão apertado..
    /// </summary>
    /// <param name="?"></param>
    protected void processaConsulta(object botao, object dataSource, GridView grv, string path){

        //Vamos exportar para excel
        if ( botao is Button && ((Button)botao).ID == "btExportar")
        {
            if (dataSource is DataTable)
            {
                DataTable dt = (DataTable)dataSource;

                if (dt.Rows.Count.Equals(0))
                {
                    Alert("Não há registros para exportar!");
                    return;
                }
                else
                {
                   
                    ArrayList colexportar = new ArrayList();

                    for (int i = 0; i < grv.Columns.Count; i++)
                    {
                        if (!grv.Columns[i].Visible)
                            continue;

                        try
                        {
                            BoundField bound = (BoundField)grv.Columns[i];

                            if (    dt.Columns.Contains(bound.DataField) || dt.Columns.Contains(bound.HeaderText))
                            {
                                if (dt.Columns[bound.DataField] != null)
                                {
                                    try
                                    {
                                        dt.Columns[bound.DataField].ColumnName = bound.HeaderText;
                                        colexportar.Add(dt.Columns[bound.DataField].ColumnName);
                                    }
                                    catch
                                    {

                                        colexportar.Add(bound.HeaderText);
                                        colexportar.Add(bound.DataField);
                                    }
                                }
                                else
                                {
                                    colexportar.Add(bound.HeaderText);
                                    colexportar.Add(bound.DataField);
                                }
                               // bound.DataField = bound.HeaderText;
                            }
                        }
                        catch { }
                    }

                removeextras:

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!colexportar.Contains(dt.Columns[i].ColumnName))
                        {
                            dt.Columns.Remove(dt.Columns[i].ColumnName);

                            goto removeextras;
                        }

                    }

                  
                    string exc = Server.MapPath(path);
                    DataSet ds = new DataSet();

                    IFiltro myFiltro = getFiltroPrincipal();

                    if (myFiltro != null)
                    {
                        dt.TableName = myFiltro.Tabela;
                    }

                    ds.Tables.Add(dt);
                    string nm_file = "exp_tab" +
                           getIntValidoRamdom().ToString() + ".xls";
                    try
                    {
                        ExcelLibrary.DataSetHelper.CreateWorkbook(exc + "\\" + nm_file, ds);

                    }
                    catch (Exception exp) {
                        Alert("Excesso de linhas!. Capacidade do driver para excel excedida. Por favor refine mais sua busca!." + exp.ToString());
                    
                    }
                    Utilities.JavaScript.ExecuteScript(this.Page,
                        "document.getElementById('frameProcess').src='" + path+"/"+ nm_file + "'; ", true);
                }


            }
        }
        
    }


    
 
    public string decimalToInsert(object valor)
    {
        if (valor == null || valor == DBNull.Value)
            return "NULL";

        return valor.ToString().Replace(',', '.');

    }

    /// <summary>
    /// Remove HTML tags from string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string RemoveHTMLFromString(string text)
    {

        return System.Text.RegularExpressions.Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
    }


    public System.Collections.ArrayList geraArrList(string paginas)
    {
        string[] ar = paginas.Split(',');

        System.Collections.ArrayList ret = new System.Collections.ArrayList();
        for (int i = 0; i < ar.Length; i++)
        {

            ret.Add(ar[i].Trim());
        }
        return ret;
    }

    public bool temInArray(System.Collections.ArrayList ar, string pag)
    {
        for (int i = 0; i < ar.Count; i++)
        {

            if (ar[i].ToString().IndexOf(pag.Replace("~/", "")) > -1)
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Quem estiver com formatação R$ ou 0,00 vai automaticamente para a direita.
    /// </summary>
    /// <param name="gvw"></param>
    public void setaEstiloDireitaNumeros( GridView gvw ){

        for (int i = 0; i < gvw.Rows.Count; i++)
        {
            GridViewRow dr = gvw.Rows[i];
            for (int z = 0; z < gvw.Columns.Count; z++)
            {
                try
                {
                    BoundField co = (BoundField)gvw.Columns[z];
                    if (co.DataFormatString.IndexOf("{0:N2}") > -1
                        ||
                        co.DataFormatString.IndexOf("{0:c}") >-1
                         ||
                        co.DataFormatString.IndexOf("{0}") > -1
                        )
                    {
                        dr.Cells[z].Attributes.Add("style", "text-align: right; padding-right: 6px;");

                    }
                }
                catch { }
            }

        }
    }

    public Decimal totalizaDataSet(DataSet ds, string prop)
    {
        decimal tot = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            tot += Convert.ToDecimal(ds.Tables[0].Rows[i][prop]);
        }

        return tot;

    }

    /// <summary>
    /// Obtem valor de uma coluna de um datatable
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    public string getValorDataTable(DataTable dt, string prop, string sep)
    {
        string propriedade = string.Empty;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (propriedade == string.Empty)
            {
                propriedade += dt.Rows[i][prop].ToString();
            }
            else
            {

                propriedade += sep + dt.Rows[i][prop].ToString();
            }

        }
        dt.Dispose();
        return propriedade;

    }
    public string getValorDataTable(DataTable dt, string prop)
    {
        return getValorDataTable(dt, prop, ",");
    }


    public void carregaRequest(Control pai)
    {

        foreach (Control ctl in pai.Controls)
            {
                if (ctl is TextBox || ctl is DropDownList)
                {
                    if (getRequest(ctl.ID) != String.Empty)
                        setValor(ctl, getRequest(ctl.ID));

                }

                if (ctl.Controls.Count > 0)
                {
                    carregaRequest( ctl);
                    
                } 
            }
  
    }
  
        public String nomeMes(int mes)
    {
        switch (mes)
        {
            case 1:
                return "Janeiro";
            case 2:
                return "Fevereiro";
            case 3:
                return "Março";
            case 4:
                return "Abril";
            case 5:
                return "Maio";
            case 6:
                return "Junho";
            case 7:
                return "Julho";
            case 8:
                return "Agosto";
            case 9:
                return "Setembro";
            case 10:
                return "Outubro";
            case 11:
                return "Novembro";
            case 12:
                return "Dezembro";

        }
        return "Janeiro";

    }



    /// <summary>
    /// Traz o índice da coluna pelo conteúdo do HeaderTExt - útil pra não precisar sabermos qual é o índice das coisas.
    /// </summary>
    /// <param name="grw"></param>
    /// <param name="header"></param>
    /// <returns></returns>
    public int indxByGridHeader(GridView grw, string header)
    {
        for (int i = 0; i < grw.Columns.Count; i++)
        {
            if (grw.Columns[i].HeaderText.ToUpper() == header.ToUpper())
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Traz o índice da coluna pelo conteúdo do HeaderTExt - útil pra não precisar sabermos qual é o índice das coisas.
    /// </summary>
    /// <param name="grw"></param>
    /// <param name="header"></param>
    /// <returns></returns>
    public static int getIndxByGridHeader(GridView grw, string header)
    {
        for (int i = 0; i < grw.Columns.Count; i++)
        {
            if (grw.Columns[i].HeaderText.ToUpper() == header.ToUpper())
                return i;
        }
        return -1;
    }


    public void loadDropDownLetra(DropDownList combo)
    {
        //IList<Entities.SimplesCodigoNome> lista = new List<Entities.SimplesCodigoNome>();

        //lista.Add(new Entities.SimplesCodigoNome("A", "A"));
        //lista.Add(new Entities.SimplesCodigoNome("B", "B"));
        //lista.Add(new Entities.SimplesCodigoNome("C", "C"));
        //lista.Add(new Entities.SimplesCodigoNome("D", "D"));
        //lista.Add(new Entities.SimplesCodigoNome("E", "E"));

        combo.Items.Clear();

        combo.Items.Add(new ListItem("--SELECIONE--", ""));
        combo.Items.Add(new ListItem("A", "A"));
        combo.Items.Add(new ListItem("B", "B"));
        combo.Items.Add(new ListItem("C", "C"));
        combo.Items.Add(new ListItem("D", "D"));
        combo.Items.Add(new ListItem("E", "E"));

        //for (int i = Inicio; i <= Fim; i++)
        //{
        //    combo.Items.Add(i.ToString());
        //}
        //if (sel != null && sel.ToString() != String.Empty)
        //    combo.SelectedValue = sel.ToString();

    }


    public  void loadDropDownInteger(DropDownList combo, int Inicio, int Fim, object sel)
    {
        for (int i = Inicio; i <= Fim; i++)
        {
            combo.Items.Add(i.ToString());
        }
        if (sel != null && sel.ToString() != String.Empty)
            combo.SelectedValue = sel.ToString();

    }
    public  void loadDropDowMes(DropDownList combo, object sel)
    {


        carregaCombo(combo, this.listaMes, "Codigo", "Nome", (sel == null ? string.Empty : sel.ToString())); 
   


    }
    public string getDescricaoByCodigo(string cod, IList<Entities.SimplesCodigoNome>  lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].Codigo.Equals(cod))
                return lst[i].Nome;
        }

        return string.Empty;
    }


    public IList<Entities.SimplesCodigoNome> listaMes
    {
        get
        {
            IList<Entities.SimplesCodigoNome> lst = new List<Entities.SimplesCodigoNome>();
            for (int i = 1; i <= 12; i++)
            {
                Entities.SimplesCodigoNome item = new Entities.SimplesCodigoNome(i.ToString(),
                     this.nomeMes(i));

                lst.Add(item);

            }

            return lst;
        }

    }

    public string getDadosQueryString()
    {
        string str = "";

        for (int i = 0; i < Request.QueryString.AllKeys.Length; i++)
        {

            if (str != "")
                str += "&";
            else
                str += "?";

            try
            {


                
                    str += Request.QueryString.AllKeys[i] + "=" +
                        Request.QueryString[Request.QueryString.AllKeys[i]].Trim();


            }
            catch { }
        }
        return str;

    }

    public string getRequest(string prop){

        if (Request.QueryString[prop] != null &&
            Request.QueryString[prop].ToString() != String.Empty)
            return Request.QueryString[prop].ToString();


        if (Request.Form != null)
        {
            for (int i = 0; i < Request.Form.AllKeys.Length; i++)
            {
                try
                {
                    if (Request.Form.AllKeys[i].IndexOf(prop) > -1)
                    {
                        return Request.Form[Request.Form.AllKeys[i]].Trim();

                    }
                }
                catch { }
            }
        }
        if (Request.QueryString != null)
        {

            for (int i = 0; i < Request.QueryString.AllKeys.Length; i++)
            {
                try
                {
                    if (Request.QueryString.AllKeys[i].IndexOf(prop) > -1)
                    {
                        return Request.QueryString[Request.QueryString.AllKeys[i]].Trim();

                    }
                }
                catch { }
            }
        }
        return String.Empty;

    }
    public void botaoVoltar()
    {
        botaoVoltar(String.Empty);
    }
    public string executeOnLoad = "";

    public void botaoVoltar(string pref)
    {
        if (Session["_url_lista" + pref] != null)
        {
            Control div = Utilities.Format.localizaControl("dvVoltar", this.Form);



            if (Session["_url_arrays_def" + pref] == null)
            {
                if (div != null)
                {
                    ((HtmlGenericControl)div).InnerHtml =
                        "<input type='button' value='&nbsp;Voltar' class='botaoVoltar' onclick='document.location.href=\"" + Session["_url_lista" + pref].ToString() + "\"'>";

                }
            }
            else
            {
                ArrayList arr = (ArrayList)Session["_url_arrays_def" + pref];
                ArrayList arrValores = (ArrayList)Session["_url_arrays_val" + pref];

                string campos = "";

                for (int i = 0; i < arr.Count; i++)
                {
                    if (arrValores[i].ToString().Trim() != String.Empty)
                    {
                        campos += "<input type='hidden' name='" + arr[i].ToString() + "' " +
                                 " value='" + arrValores[i].ToString().Replace("'", "\"") + "' > ";
                    }
                }

                string form = "<form name='frmVoltar' method='post' action='" + Session["_url_lista_limpa" + pref].ToString() + "' >" + campos + "</form>";

                if (div != null)
                {
                    ((HtmlGenericControl)div).InnerHtml =
                           "<input type='button' value='&nbsp;Voltar' class='botaoVoltar' onclick='document.frmVoltar.submit()'>";

                    string varBarra = "\\";


                    string js = " window.onload = function(){ " + this.executeOnLoad + " var div_form_voltar =  document.getElementById('div_form_voltar'); " + System.Environment.NewLine + System.Environment.NewLine +
                       " if ( div_form_voltar != null ){ " + System.Environment.NewLine +
                       " div_form_voltar.innerHTML=\"" + form.Replace("\"", varBarra + "\"") + "\"; } } " + System.Environment.NewLine + System.Environment.NewLine;



                    //js = " alert('oi'); ";
                    // Utilities.JavaScript.ExecuteFinalScript(this.Page, "key_bt_back", js, true);
                    Utilities.JavaScript.ExecuteScript(this.Page, "key_bt_back", js, true);

                }
            }
        }
        else
        {
            if (this.executeOnLoad != String.Empty)
            {
                string js = " window.onload = function(){ " + this.executeOnLoad + " } " + System.Environment.NewLine + System.Environment.NewLine;
               // js = " alert('oiiii'); "; 
                Utilities.JavaScript.ExecuteScript(this.Page, "key_bt_back", js, true);

            }

        }

    }
    public void guardaSessao(string prop)
    {
        guardaSessao(prop, "");
    }



    public void guardaSessao(string prop, string pref)
    {

        string url = Request.ServerVariables["URL"].ToString();

        string queryString = string.Empty;
        string sep = "?";

        Control gvw = Utilities.Format.localizaControl("gvwDados", this.Form);

        if (gvw != null)
        {
            queryString += sep + "page=" + ((GridView)gvw).PageIndex.ToString();
            sep = "&";

        }

        ArrayList arrForm = new ArrayList();
        ArrayList valoresForm = new ArrayList();
        ArrayList arrGET = new ArrayList();
        ArrayList valoresGET = new ArrayList();

        try
        {
            if (Request != null && Request.Form != null)
            {
                for (int i = 0; i < Request.Form.AllKeys.Length; i++)
                {

                    if (Request.Form.AllKeys[i] == null)
                        break;


                    if (queryString.IndexOf("&" + Request.Form.AllKeys[i] + "=") > -1)
                        continue;


                    if (queryString.IndexOf("?" + Request.Form.AllKeys[i] + "=") > -1)
                        continue;


                    if (Request.Form.AllKeys[i].IndexOf("EVENTTARGET") > -1)
                        continue;


                    if (Request.Form.AllKeys[i].IndexOf("EVENTARGUMENT") > -1)
                        continue;


                    if (Request.Form.AllKeys[i].IndexOf("__EVENTVALIDATION") > -1)
                        continue;


                    if (Request.Form.AllKeys[i].IndexOf("__PREVIOUSPAGE") > -1)
                        continue;

                    if (Request.Form.AllKeys[i].IndexOf("btPesquisar") > -1)
                        continue;

                    if (Request.Form.AllKeys[i] == "ctl00$sm")
                        continue;

                    if (Request.Form.AllKeys[i].IndexOf("ClientState") > -1)
                        continue;



                    if (Request.Form.AllKeys[i].IndexOf("VIEWSTATE") > -1)
                        continue;


                    if (queryString != String.Empty)
                        sep = "&";

                    //if (Request.Form[Request.Form.AllKeys[i]].ToString() == String.Empty)
                    //{
                    //    continue;
                    //}



                    queryString += sep + Request.Form.AllKeys[i] + "=" + Request.Form[Request.Form.AllKeys[i]];
                    arrForm.Add(Request.Form.AllKeys[i]);
                    valoresForm.Add(Request.Form[Request.Form.AllKeys[i]]);

                }
            }
            if (Request != null && Request.QueryString != null)
            {

                for (int i = 0; i < Request.QueryString.AllKeys.Length; i++)
                {


                    if (Request.QueryString.AllKeys[i].IndexOf("ClientState") > -1)
                        continue;

                    if (Request.QueryString.AllKeys[i] == null)
                        break;

                    if (queryString.IndexOf("&" + Request.QueryString.AllKeys[i] + "=") > -1)
                        continue;


                    if (queryString.IndexOf("?" + Request.QueryString.AllKeys[i] + "=") > -1)
                        continue;



                    if (queryString != String.Empty)
                        sep = "&";

                    if (Request.QueryString[Request.QueryString.AllKeys[i]].ToString() == String.Empty)
                    {
                        continue;
                    }

                    if (!arrForm.Contains(Request.QueryString.AllKeys[i]))
                    {
                        queryString += sep + Request.QueryString.AllKeys[i] + "=" + Request.QueryString[Request.QueryString.AllKeys[i]];
                        arrGET.Add(Request.QueryString.AllKeys[i]);
                        valoresGET.Add(Request.QueryString[Request.QueryString.AllKeys[i]]);
                    }
                }
            }
        }
        catch { }


        queryString = string.Empty;
        sep = "?";

        for (int i = 0; i < arrForm.Count; i++)
        {


            if (queryString != String.Empty)
                sep = "&";

            if (arrForm[i].ToString() != String.Empty && valoresForm[i] != null &&
                 valoresForm[i].ToString() != String.Empty)
            {
                queryString += sep + arrForm[i].ToString() + "=" + valoresForm[i].ToString();
            }
        }

        for (int i = 0; i < arrGET.Count; i++)
        {


            if (queryString != String.Empty)
                sep = "&";

            if (arrGET[i].ToString() != String.Empty && valoresGET[i] != null &&
                 valoresGET[i].ToString() != String.Empty)
            {
                queryString += sep + arrGET[i].ToString() + "=" + valoresGET[i].ToString();

                //A preferência é sempre da variável POST
                if (!arrForm.Contains(arrGET[i].ToString()))
                {
                    arrForm.Add(arrGET[i].ToString());
                    valoresForm.Add(valoresGET[i].ToString());
                }
            }
        }

        Session["_url_lista" + pref] = url + queryString;

        Session["_url_arrays_def" + pref] = arrForm;
        Session["_url_arrays_val" + pref] = valoresForm;

        Session["_url_lista_limpa" + pref] = Request.ServerVariables["URL"].ToString();
        //return String.Empty;

    }

    public IList<T> SafeList<T>(IList<T> input) where T : class, ICloneable
    {
        //Symply check the type of the first element.
        if (input.Count > 0) input[0] = (T)input[0].Clone();
        return input;
    }

    /// <summary>
    /// Adiciona um valor, garantindo que as vírgulas não ficarão nem no início nem se repetirão.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="valor"></param>
    /// <param name="separador"></param>
    /// <returns></returns>
    public string adicionaStr(string str, string valor, string separador)
    {
        if (str == string.Empty)
        {
            str += valor;
            return str;
        }
        else
        {
            str += separador + valor;
        }
        return str;
    }


    public void setValoresCheckList(string valor, CheckBoxList chk)
    {
        if (valor == null)
            valor = string.Empty;

        string[] ar = valor.Split(',');

        for (int z = 0; z < chk.Items.Count; z++)
        {

            chk.Items[z].Selected = false;
        }


        for (int i = 0; i < ar.Length; i++)
        {

            for (int z = 0; z < chk.Items.Count; z++)
            {
                if (chk.Items[z].Value == ar[i])
                {
                    chk.Items[z].Selected = true;

                }
            }
            
        }


    }


    /// <summary>
    /// Traz os valores selecionados em um CheckBoxList
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="sep"></param>
    /// <returns></returns>
    public string getValoresCheckList(CheckBoxList lst, string sep)
    {
        string ret = string.Empty;
        for (int i = 0; i < lst.Items.Count; i++)
        {
            if (lst.Items[i].Selected)
            {
                if (ret == string.Empty)
                    ret = lst.Items[i].Value;
                else
                    ret += sep + lst.Items[i].Value;
            }
        }
        return ret;
    }

    /// <summary>
    /// Traz os textos selecionados em um CheckBoxList
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="sep"></param>
    /// <returns></returns>
    public string getValoresCheckList2(CheckBoxList lst, string sep)
    {
        string ret = string.Empty;
        for (int i = 0; i < lst.Items.Count; i++)
        {
            if (lst.Items[i].Selected)
            {
                if (ret == string.Empty)
                    ret = lst.Items[i].Text;
                else
                    ret += sep + lst.Items[i].Text;
            }
        }
        return ret;
    }

    
    /// <summary>
    /// Chama o método validar() ao tentar salvar - Default: True.
    /// </summary>
    /// 


    protected bool validaSalvar = true;

    /// <summary>
    /// Simula um request com o mesmo comportamento do asp
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string request(String str, bool soqueristring)
    {
        string Str = string.Empty;

        if (Page.Request.QueryString[str] != null && Page.Request.QueryString[str].ToString() != String.Empty)
            return Page.Request.QueryString[str].ToString();

        if (soqueristring)
            return Str;

        if (Page.Request.Form[str] != null && Page.Request.Form[str].ToString() != String.Empty)
            return Page.Request.Form[str].ToString();


        return Str;

    }
    public string request(String str)
    {
        return request(str, false);
    }

    /// <summary>
    /// Converte datatable para texto
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    protected string DataTableToText(DataTable dt, string prop)
    {
        string ret = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (ret == String.Empty)
                ret += dt.Rows[i][prop].ToString();
            else
                ret += "," + dt.Rows[i][prop].ToString();
        }
        return ret;
    }

    protected void setaTitulo(string titulo)
    {

        Control divTitulo = Utilities.Format.localizaControl("h2_tela_sistema", this.Page.Form);
         if (divTitulo != null)
         {
             ((HtmlGenericControl)divTitulo).InnerHtml = titulo;

         }

    }
     protected void setaTipoCadastro(string titulo)
    {

        Control divTitulo = Utilities.Format.localizaControl("b_tipo_cadastro", this.Page.Form);
         if (divTitulo != null)
         {
             ((HtmlGenericControl)divTitulo).InnerHtml = titulo;

         }

    }
    
    /// <summary>
    /// Antes de renderizar o final..
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        
            Control myFiltro = Utilities.Format.localizaControl("UcFiltroBasico1", this.Form);

            //if (myFiltro != null)
            //{
            //    IFiltro filt = (IFiltro)myFiltro;
            //    try
            //    {
            //        if (filt != null)
            //            filt.carregaCamposFiltro();
            //    }
            //    catch { }
            //}

        //Page.Title = Entities.Variaveis.getValor("Titulo");
        string url = Request.ServerVariables["URL"];


        Control div = Utilities.Format.localizaControl("dvMensagem", this.Page.Form);
        if (div != null)
        {
            ((HtmlGenericControl)div).InnerHtml = String.Empty;
            if (Session["st_Mensagem"] != null)
            {
                ((HtmlGenericControl)div).InnerHtml = Session["st_Mensagem"].ToString();

                Session["st_Mensagem"] = null;

                //Utilities.JavaScript.ExecuteScript(this.Page, "window.onload = totopo; function totopo(){ document.location.href='#atopo'; } ", true);
            }
        }

       // Control myFiltro = Utilities.Format.localizaControl("UcFiltroBasico1", this.Form);

        if (myFiltro != null)
        {

            //((IFiltro)myFiltro).garantePostBack();
        }
        Control divExp = Utilities.Format.localizaControl("divExplicaFiltro", this.Page.Form);

        if (divExp != null )
        {
            ((HtmlGenericControl)divExp).InnerHtml = String.Empty;
            if (Session["_divExplicaFiltro"] != null && Session["_divExplicaFiltro"].ToString() != String.Empty)
            {
                ((HtmlGenericControl)divExp).InnerHtml = "<span class='ordenacao'>Filtro</span> &gt;&gt; " + Session["_divExplicaFiltro"].ToString();
                ((HtmlGenericControl)divExp).Visible = true;

                if (Session["_divExplicaFiltro"].ToString() == "NOTHING")
                {
                    ((HtmlGenericControl)divExp).InnerHtml = "<span class='ordenacao'> <i> Consulta sem filtro </i> -> Exibindo apenas 200 registros </span>  >> Clique <a href='"+
                        Request.ServerVariables["URL"].ToString()+"?todos=1"
                        +"'>aqui</a> para ver todos.";
                }
            }

            if (getRequest("UcFiltroBasico1$ImageButton1.y") != String.Empty) //o botão de esconder filtro foi o responávle pelo postback, então.. nada de apagar isso.
            {
            }
            else
            {

                Session["_divExplicaFiltro"] = null;
            }

        }

        fechaConn();


    }

    public void fechaConn()
    {
        IDbPersist oConn = ConnAccess.getConn();
        oConn.disconnect();
    }


      public void localizaControleEHabilita(string nome, bool habilita)
    {
        Control cotr = Utilities.Format.localizaControl(nome, this.Page.Form);

         if ( cotr != null ){

             if ( cotr is WebControl){

                 ((WebControl)cotr).Enabled = habilita;

             }
             if ( cotr is HtmlTable){
                 
                 ((HtmlTable)cotr).Disabled  = !habilita;
                 Utilities.Format.EnableControl(cotr, false);
             }

         }

     
    }

    public string limita(string str, int limit)
    {
        if (str.Length > limit)
            return str.Substring(0, limit) + "...";
        else
            return str;

    }




    #region Parametros pro log
    protected Label labelOperador;
    protected Label labelOperadorDataAtualizacao;
    protected Type tpEntBase;

    private HtmlControl divInfolog;

    /*
    public virtual void MontaMenu(HtmlGenericControl divMenu)
    {
       // if (Session["Menu"] == null || Session["Menu"].ToString() == String.Empty)
       // {

        Business.CategoryBusiness pCat = new CategoryBusiness();
        Business.ProductBusiness pProd = new ProductBusiness();
            IList<Entities.Category> listCategory = pCat.listCategory(null);
            StringBuilder strMenu = new StringBuilder();
            if (listCategory != null && listCategory.Count > 0)
            {
                strMenu.Append("<div style='color:#FF7800;text-align:left;' >");


                foreach (Entities.Category categ in listCategory)
                {
                    
                string strMais = string.Empty;
                
                    if (categ.IdFather == null)
                    {

                        if (pProd.QtdeProduto( categ.Id) > 0 )
                        {
                            strMais = "document.location='Products.aspx?Category=" +
                                categ.Id.ToString() + "';";
                        }

                        strMenu.Append(@"<br><b><a href='#'  " +
                          "  onclick=\"menuMontar('" + categ.Id + "','Products.aspx?Category=" +
                          categ.Id + "');" + strMais + "\">" + categ.Description + "</a></b> <br>");
                        foreach (Entities.Category subcateg in listCategory)
                        {
                            if (subcateg.IdFather != null && subcateg.IdFather.Id == categ.Id)
                            {
                        
                                strMenu.Append("<div id='divCat_" + subcateg.IdFather.Id + "' style='font-size:smaller;text-align:left;display:none;'>");
                                strMenu.Append(@"<b><a href='Products.aspx?Category=" + subcateg.Id + "'>  &nbsp;&nbsp;&nbsp;&nbsp;" + subcateg.Description + "</a></b> <br>");
                                foreach (Entities.Category subcateg2 in listCategory)
                                {
                                    if (subcateg2.IdFather != null && subcateg2.IdFather.Id == subcateg.Id && subcateg2.IdFather.IdFather != null)
                                    {
                                        strMenu.Append("<div id='divCat_" + subcateg2.IdFather.Id + "' style='font-size:smaller;text-align:left;display:none;' >");
                                        strMenu.Append(@"<a href='Products.aspx?Category=" + subcateg2.Id + "'> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;" + subcateg2.Description + "</a><br>");
                                        strMenu.Append("</div>");
                                    }
                                }
                                strMenu.Append("</div>");
                            }
                        }

                        strMenu.Append(@"......................................... <br>");
                    }
                }

                strMenu.Append("</div>");
            }

            Session.Remove("Menu");
            Session.Add("Menu", strMenu.ToString());
        //}
        divMenu.InnerHtml = Session["Menu"].ToString();
    }
    */


    protected HtmlControl divInfoLog
    {
        set
        {
            divInfolog = value;
            if (value != null && !Page.IsPostBack)
            {
                divInfolog.Visible = false;
            }
        }
        get
        {
            return divInfolog;
        }
    }
    #endregion
    public PageCadastroBase()
    {
        //
        // TODO: Add constructor logic here
        //
       // Page.Title = Entities.Variaveis.getValor("Titulo");




    }

    /// <summary>
    /// Exibe texto da Data
    /// </summary>
    public static string TextoData
    {
        get
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            string data = dtfi.GetDayName(DateTime.Now.DayOfWeek);

            return data + ", " + DateTime.Now.ToShortDateString();
        }

    }

    public void validaLoginExpirado()
    {
        if (validaLoginSenha)
        {
            if (SessionFacade.Id <= 0)
            {
                Alert("Login expirado!");
                js.ExecuteScript(this.Page, "window.location.href='login.aspx';", true);
                // Response.End();
            }

        }
    }

    public Boolean limpaFormulario = false;

    public string toMySqlDate(string data)
    {
        return toMySqlDate(data, 0);
    }


    public string toMySqlDate(string data, int dia)
    {
        if (data == string.Empty)
            return string.Empty;

        DateTime dt = DateTime.Parse(data).AddDays(Convert.ToDouble(dia));

        return "'" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "'";
    }

    /// <summary>
    /// Converte uma data para o formato que o banco entenda..
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dia"></param>
    /// <param name="hora"></param>
    /// <returns></returns>
    public string toMySqlDate(string data, int dia, string hora)
    {
        if (data == string.Empty)
            return string.Empty;

        DateTime dt = DateTime.Parse(data).AddDays(Convert.ToDouble(dia));

        return "'" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + hora +"'";
    }

    public string toMySqlDate(string data, string hora)
    {
        if (data == string.Empty)
            return string.Empty;

        DateTime dt = DateTime.Parse(data);

        return "'" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + hora + "'";
    }
    public string toMySqlDate2(string data)
    {
        if (data == string.Empty)
            return string.Empty;

        DateTime dt = DateTime.Parse(data);

        return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss")+ "'";
    }
  

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            VsLista.Clear();



        }

    }

    /// <summary>
    /// Se o checkbox estiver checado, vai retornar 1, senão retorna 0
    /// </summary>
    /// <param name="chk"></param>
    /// <returns></returns>
    protected Int16 getNumeroFromCheckBox(CheckBox chk)
    {
        if (chk.Checked)
            return 1;

        return 0;
    }


    /// <summary>
    /// Testa se o campo foi preenchido, e aí dá uma mensagem...
    /// </summary>
    /// <param name="ctr">Control que será validado</param>
    /// <param name="MsgAlert">Mensagem que deverá ser exibida</param>
    /// <returns></returns>
    protected bool campoPreenchido(Control ctr, String MsgAlert)
    {
        if (getValor(ctr) == String.Empty || getValor(ctr) == "")
        {
            if (MsgAlert != String.Empty)
                Alert(MsgAlert);

            if (ctr is TextBox)
            {
                ((TextBox)ctr).Focus();
                setaFocus(ctr);
            }
            if (ctr is DropDownList)
            {
                ((DropDownList)ctr).Focus();
                setaFocus(ctr);
            }


            return false;
        }
        return true;
    }
    /// <summary>
    /// Seta o foco, por meio de javascript e ajax, para um controle.
    /// </summary>
    /// <param name="ctr"></param>
    protected void setaFocus(Control ctr)
    {

        js.ExecuteScript(this.Page,"set_focus", " try { document.getElementById('" + ctr.ClientID + "').focus(); } catch (Exp){} ", true);

    }

    /// <summary>
    /// Obtem o valor de um webcontrol, não importa de que tipo ele seja..
    /// </summary>
    /// <param name="ctr"></param>
    /// <param name="valorSeVazio"></param>
    /// <returns></returns>
    public string getValor(Control ctr, string valorSeVazio)
    {
        string valor = getValor(ctr);
        if (valor == String.Empty)
        {
            valor = valorSeVazio;
        }

        return valor;
    }
    public string getValor(Control ctr)
    {
        if (ctr is TextBox)
            return ((TextBox)ctr).Text;

        if (ctr is DropDownList)
            return ((DropDownList)ctr).SelectedValue;

        if (ctr is RadioButtonList)
            return ((RadioButtonList)ctr).SelectedValue;

        if (ctr is HiddenField)
            return ((HiddenField)ctr).Value;

        if (ctr is CheckBoxList)
            return ((CheckBoxList)ctr).SelectedValue;


        if (ctr is Label)
            return ((Label)ctr).Text;


        if (ctr is IField)
            return ((IField)ctr).Value;

        return string.Empty;

    }

    public void setValor(Control ctr, DateTime? dt, String formato)
    {
        if (dt != null)
        {
            try
            {
                setValor(ctr, dt.Value.ToString(formato));
                return;
            }
            catch { }
        }
    }


    public void setValor(Control ctr, DataRow oBase, String propriedade)
    {

        if (oBase == null || oBase[propriedade] == DBNull.Value )
        {
            setValor(ctr, String.Empty);
            return;
        }
        try
        {

            if (oBase[propriedade] == DBNull.Value ||
                 oBase[propriedade].ToString() == String.Empty)
            {
                setValor(ctr, String.Empty);
                return;
            }

            setValor(ctr, oBase[propriedade]);
        }
        catch { }
    }


    public void setValor(Control ctr, object valor, object formato, bool ehData)
    {
        string sel = String.Empty;
        if (valor != null)
            sel = valor.ToString();

        try
        {
            if (ctr is TextBox)
            {
                if (valor != null && valor is DateTime && ehData)
                {
                    sel = Convert.ToDateTime(valor).ToString(formato.ToString());

                    ((TextBox)ctr).Text = sel;
                }

            }

        }
        catch { }

    }



    public void setValor(Control ctr, object valor)
    {
        string sel = String.Empty;
        if (valor != null)
            sel = valor.ToString();

        try
        {
            if (ctr is TextBox)
            {
                
                if (valor != null && valor is double)
                {
                    sel = Convert.ToDouble(valor).ToString("N2");

                }
                if (valor != null && valor is decimal)
                {
                    sel = Convert.ToDecimal(valor).ToString("N2");

                }
                if (valor != null && valor is DateTime)
                {
                    sel = Convert.ToDateTime(valor).ToString("dd/MM/yyyy");

                }


               ((TextBox)ctr).Text = sel;
            }
            if (ctr is DropDownList)
                ((DropDownList)ctr).SelectedValue = sel;

            if (ctr is RadioButtonList)
                ((RadioButtonList)ctr).SelectedValue = sel;

            if (ctr is CheckBoxList)
                ((CheckBoxList)ctr).SelectedValue = sel;

            if (ctr is HiddenField)
                ((HiddenField)ctr).Value = sel;

            if (ctr is CheckBox)
            {
                try
                {
                    ((CheckBox)ctr).Checked = Convert.ToBoolean(Nvl(sel, "False").ToString());
                }
                catch {

                    if (sel != null && sel.ToString().Equals("1"))
                        ((CheckBox)ctr).Checked = true;
                    else
                        ((CheckBox)ctr).Checked = false;

                }

            }
            if (ctr is Label)
                ((Label)ctr).Text = sel;


            if (ctr is IField)
                ((IField)ctr).Value = sel;

        }
        catch { }

    }

    public void AddVsLista(Object value)
    {


        if (VsLista.Contains(value))
            return;

        try
        {
            if (VsLista.Contains(int.Parse(value.ToString())))
                return;
        }
        catch { }


        VsLista.Add(value);
    }
    public ArrayList VsLista
    {
        get
        {
            ArrayList lista;

            if (ViewState["vsLista"] == null)
            {
                lista = new ArrayList();
                ViewState["vsLista"] = lista;
            }
            else
                lista = (ArrayList)ViewState["vsLista"];

            return lista;

        }
    }
    public String strVsLista(string separador)
    {
        string str = string.Empty;
        for (int i = 0; i < this.VsLista.Count; i++)
        {
            if (str.Equals(string.Empty))
                str += this.VsLista[i].ToString();
            else
                str += separador + this.VsLista[i].ToString();
        }

        return str;

    }
    /// <summary>
    /// Equivalente ao Isnull do Sql Server e ao NVL do Oracle
    /// </summary>
    public object Nvl(object valor, object valor2)
    {
        if (valor == null)
            return valor2;



        if (string.IsNullOrEmpty(valor.ToString()))
            return valor2;
        else
            return valor;

        return valor;

    }

    /// <summary>
    /// Permite o registro de uma PK em ViewState.
    /// </summary>
    public int PkId
    {
        get
        {
            if (ViewState["PkId"] == null)
                ViewState["PkId"] = 0;

            return Convert.ToInt32(ViewState["PkId"]);
        }
        set
        {
            ViewState["PkId"] = value;
        }
    }


    /// <summary>
    /// Permite o registro de uma PK em ViewState.
    /// </summary>
    public Int64 PkId64
    {
        get
        {
            if (ViewState["PkId"] == null)
                ViewState["PkId"] = 0;

            return Convert.ToInt64(ViewState["PkId"]);
        }
        set
        {
            ViewState["PkId"] = value;
        }
    }

    public int sPkId
    {
        get
        {
            if (ViewState["sPkId"] == null)
                ViewState["sPkId"] = -1;

            return Convert.ToInt32(ViewState["sPkId"]);
        }
        set
        {
            ViewState["sPkId"] = value;
        }
    }

    public IList<T> getblistaCadastro <T>()
    {
          if (Session[this.ToString()+ "blistaCadastro"] == null)
                Session.Add(this.ToString()+"blistaCadastro" , new List<T>());

            return (IList<T>)Session[this.ToString()+ "blistaCadastro"];
     }
       
     public void setblistaCadastro <T>(object value)
    {
        Session.Remove(this.ToString() + "blistaCadastro");
            if (value != null)
                Session.Add(this.ToString() + "blistaCadastro", value);
    }


    public Boolean GridCarregado
    {
        get
        {
            if (ViewState["GridCarregado"] == null)
                ViewState["GridCarregado"] = false;

            return Convert.ToBoolean(ViewState["GridCarregado"]);
        }
        set
        {
            ViewState["GridCarregado"] = value;
        }
    }


    public void carregaCombo(DropDownList combo, Object dataSource, String CampoValor, String CampoTexto, String valorSelecionado)
    {
        carregaCombo(combo, dataSource, CampoValor, CampoTexto, valorSelecionado, false);
    }


    /// <summary>
    /// Carrega combo
    /// </summary>
    /// <param name="combo"></param>
    /// <param name="dataSource"></param>
    /// <param name="CampoValor"></param>
    /// <param name="CampoTexto"></param>
    /// <param name="valorSelecionado"></param>
    public void carregaCombo(DropDownList combo, Object dataSource, String CampoValor, String CampoTexto, String valorSelecionado, Boolean addSelecione)
    {
        combo.DataValueField = CampoValor;
        combo.DataTextField = CampoTexto;
        combo.DataSource = dataSource;
        try
        {
            combo.DataBind();
        }
        catch { }


        if (addSelecione)
            combo.Items.Insert(0, new ListItem("--SELECIONE--", ""));




        if (valorSelecionado != String.Empty)
        {
            try
            {
                combo.SelectedValue = valorSelecionado;
            }
            catch { }

        }
    }

    public void carregaCombo(object combo, Object
       dataSource, String CampoValor, String CampoTexto,
      String valorSelecionado)
    {
        if (combo == null)
            return;

        if (combo is ListControl)
        {
            carregaCombo(((ListControl)combo), dataSource,
                CampoValor, CampoTexto, String.Empty);

        }
        //if (combo is DropDownList)
        //{
        //    carregaCombo(((DropDownList)combo), dataSource,
        //        CampoValor, CampoTexto, String.Empty);

        //}

    }
    public void carregaCombo(ListControl combo, Object
         dataSource, String CampoValor, String CampoTexto, 
        String valorSelecionado)
    {
        combo.DataValueField = CampoValor;
        combo.DataTextField = CampoTexto;
        combo.DataSource = dataSource;
        try
        {
            combo.DataBind();
        }
        catch { }

        if (valorSelecionado != String.Empty)
        {
            try
            {
                combo.SelectedValue = valorSelecionado;
            }
            catch { }

        }
    }


    public void comboBoxJqueryComplete(string nomeCombo, string nome_var ,  bool mostraInclude){
		
		string script = "";
		
        if ( mostraInclude )
		   script += System.Environment.NewLine +  " window.dhx_globalImgPath = 'scripts/htmlxcombo/imgs/'; ";



       script += "var combo_"+nome_var + " = dhtmlXComboFromSelect('" + nomeCombo + "'); " + System.Environment.NewLine +
             " combo_"+nome_var+".enableFilteringMode(true); ";

       Utilities.JavaScript.ExecuteScript(this.Page, "key_" + nomeCombo, script, true);
	}   

    private int indexColunasInvisivel = 0;

    /// <summary>
    ///  1 - deverá ser usado o método validar() para efetuar a validação, 2 - Deverá ser usar o método validar(entidade e) 
    /// </summary>
    private int tipovalidacao = 1;

    private Boolean houveErro = false;

    public Boolean HouveErro
    {
        get { return houveErro; }
        set { houveErro = value; }
    }

    public int Tipovalidacao
    {
        get { return tipovalidacao; }
        set { tipovalidacao = value; }
    }

    /// <summary>
    /// Indica até que indíce de colunas devem ficar invisível para o Grid, default 0 - apenas a primeira coluna
    /// </summary>
    public int IndexColunasInvisivel
    {
        get { return indexColunasInvisivel; }
        set { indexColunasInvisivel = value; }
    }

    /// <summary>
    /// Emite alerta ao usuário: Usa o MsgBox do Ajax ToolKit se este estiver setado, senão usa Javascript Alert
    /// </summary>
    /// <param name="msg"></param>
    public void Alert(string msg)
    {
        if (ScriptManager.GetCurrent(this.Page) == null)
        {
            Response.Write("<script>alert('" + msg + "');</script>");
            return;

        }
       
            js.Alert(msg, this.Page);
    }

    public void Alert(string msg, string Titulo, string textBotaoFechar)
    {

            js.Alert(msg, this.Page);
    }


    public virtual void carregaGrid() {
        GridCarregado = true;
    }

    public virtual void carregaForm(DataRow entidade) {
       
    }
    public virtual DataRow obtemForm()
    {
        throw new Exception("Método não implementado!");
    }

    public virtual void setaEstiloGrid(GridView gridDados)
    {
        gridDados.RowCreated += new GridViewRowEventHandler(gridDados_RowCreated);
        gridDados.RowDataBound += new GridViewRowEventHandler(gridDados_RowDataBound);
        gridDados.PageIndexChanging += new GridViewPageEventHandler(gridDados_PageIndexChanging);
        gridDados.RowCommand += new GridViewCommandEventHandler(gridDados_RowCommand);
    }


    /// <summary>
    /// Seta estilo em tabela de cadastro.. para ajudar no uso do sistema..
    /// </summary>
    /// <param name="tb"></param>
    public void setaEstiloTable(HtmlTable tb)
    {
        int conta = 1;
        for (int i = 0; i < tb.Rows.Count; i++)
        {
            string estilo = tb.Rows[i].Attributes["style"];

            if (estilo == null)
                estilo = string.Empty;

            if (tb.Rows[i].Visible && (estilo.IndexOf("display:none") < 0 && estilo.IndexOf("display: none") < 0))
            {
                conta++;
                if ((conta % 2) > 0)
                {
                    tb.Rows[i].Attributes.Remove("class");
                    tb.Rows[i].Attributes.Add("class", "td_alter");
                }
            }
        }
    }


    public virtual void gridDados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        return;
    }

    public object MainSource
    {
        set
        {
            SessionFacade.createCache("chDados", value);
        }
        get
        {
            return Cache["chDados"];
        }
    }

    public virtual void gridDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (!e.NewPageIndex.Equals(-1))
            {
                ((GridView)sender).PageIndex = e.NewPageIndex;
                if (this.MainSource != null)
                {
                    ((GridView)sender).DataSource = this.MainSource;
                }
                else
                    carregaGrid(); 

            }
        }
        catch
        {
            ((GridView)sender).PageIndex = 1;
            carregaGrid(); 
        }
    }

    public virtual void gridDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            for (int i = 0; i <= this.IndexColunasInvisivel; i++)
            {
                e.Row.Cells[i].Visible = false;
            }


            LinkButton button;
            button = (LinkButton)e.Row.Cells[0].Controls[0];
            e.Row.Attributes.Add("onclick", this.Page.ClientScript.GetPostBackClientHyperlink(button, ""));
        }
        else if (e.Row.RowType.Equals(DataControlRowType.Header))
        {
            for (int i = 0; i <= this.IndexColunasInvisivel; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
        }
    }

    protected virtual void gridDados_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            e.Row.Attributes.Add("onMouseOver", "mouseOver(this,true)");
            e.Row.Attributes.Add("onMouseOut", "mouseOut(this)");
        }
    }

    protected string getDDDTel(string val)
    {
        if (String.IsNullOrEmpty(val))
            return string.Empty;


        val = val.Replace("(", "").Replace(")", "");

        if (val.Length >= 2)
            return val.Substring(0, 2);

        return string.Empty;
    }

    protected DateTime? getDate(Control cr)
    {
        try
        {
            string dt = getValor(cr);
            return Convert.ToDateTime(dt);
        }
        catch
        {
            return null;
        }

    }

    protected DateTime? getDate(string dt)
    {
        try
        {
            //string dt = getValor(cr);
            return Convert.ToDateTime(dt);
        }
        catch
        {
            return null;
        }

    }

    protected string getTel(string val)
    {
        if (String.IsNullOrEmpty(val))
            return string.Empty;

        val = val.Replace("(", "").Replace(")", "");

        if (val.Length > 2)
            return val.Substring(2,val.Length - 2);

        return string.Empty;
    }

    protected string getTel(TextBox DDD, TextBox Tel)
    {
        if (DDD.Text.Trim() == String.Empty)
            return string.Empty;

        if (Tel.Text.Trim() == String.Empty)
            return string.Empty;

        string tel = "(" + DDD.Text.Trim() + ") " + Tel.Text.Trim();
        return tel;
    }


    protected virtual void limpaForm() {
        Utilities.Format.ClearControl(this.Form);
        PkId = 0;
    }
    protected virtual Boolean validar()
    {
        return true;
    }
    protected virtual Boolean validar(DataRow entidade)
    {
        return true;
    }



    
    protected virtual void setReadOnly(Control ctr)
    {
        if (ctr is TextBox)
        {
            TextBox text = (TextBox)ctr;
            text.Attributes.Remove("readonly");
            text.Attributes.Add("readonly", "");
            text.CssClass = "txt_disabled";
        }
        if (ctr is DropDownList)
        {
            DropDownList text = (DropDownList)ctr;
            text.Attributes.Remove("disabled");
            text.Attributes.Add("disabled", "true");
        }
        if (ctr is CheckBox)
        {
            CheckBox text = (CheckBox)ctr;
            text.Attributes.Remove("disabled");
            text.Attributes.Add("disabled", "true");
        }

       
       }

    /// <summary>
    /// Bloqueia todos os controles que existem dentro de outro..
    /// </summary>
    /// <param name="controlP"></param>
    /// <param name="arExceto"></param>
		public  void bloqueiaControles(Control controlP, ArrayList arExceto) 
        { 
        
            foreach ( Control ctl  in controlP.Controls) {

                
                if (ctl.ID != null && arExceto.Contains(ctl.ID.ToString()))
                    continue;

                if (ctl is ImageButton || ctl is Image)
                {
                    ctl.Visible = false;
                }
          
                if (ctl is TextBox) { 
                   setReadOnly(ctl);
                    } 
               
                if (ctl is DropDownList) { 
                    if (((DropDownList)ctl).Visible == true) 
                    {
						 setReadOnly(ctl);
                    } 
                } 
                if (ctl is CheckBox) { 
                    setReadOnly( ctl);
                }

                if (ctl is AjaxControlToolkit.CalendarExtender)
                {
                    ((AjaxControlToolkit.CalendarExtender)ctl).Enabled = false;
                }

                if (ctl.Controls.Count > 0) { 
                    bloqueiaControles(ctl, arExceto); 
                } 
            } 
        }


        /// <summary>
        /// Habilita todos os controles que existem dentro de outro..
        /// </summary>
        /// <param name="controlP"></param>
        /// <param name="arExceto"></param>
        public void habilitaControles(Control controlP, ArrayList arExceto)
        {

            foreach (Control ctl in controlP.Controls)
            {


                if (ctl.ID != null && arExceto.Contains(ctl.ID.ToString()))
                    continue;

                if (ctl is ImageButton || ctl is Image)
                {
                    ctl.Visible = true;
                }

                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Attributes.Remove("readonly");
                    
                }

                if (ctl is DropDownList)
                {
                    ((DropDownList)ctl).Enabled = true;
                    ((DropDownList)ctl).Attributes.Remove("disabled");
                }
                if (ctl is CheckBox)
                {
                    ((CheckBox)ctl).Enabled = true;
                    ((CheckBox)ctl).Attributes.Remove("disabled");
            
                }

                if (ctl is AjaxControlToolkit.CalendarExtender)
                {
                    ((AjaxControlToolkit.CalendarExtender)ctl).Enabled = true;
                }

                if (ctl.Controls.Count > 0)
                {
                    habilitaControles(ctl, arExceto);
                }
            }
        }

        /// <summary>
        /// Indica se o insert poderá ser feito pressupondo auto-increment na tabela. Default para este projeto: Não.
        /// </summary>
        public Boolean save_autoincrement = false;

        /// <summary>
        /// Cuida de salvar informações provenientes da página
        /// </summary>
        /// <param name="bas"></param>
        /// <param name="entidade"></param>
        /// <param name="carregaGrid"></param>
        /// <param name="msg"></param>
        protected virtual void salvar(DataRow entidade, Boolean carregaGrid, string msg)
        {
            salvar( entidade, carregaGrid, msg, String.Empty);
        }

        /// <summary>
        /// Cuida de salvar informações provenientes da página
        /// </summary>
        /// <param name="bas"></param>
        /// <param name="entidade"></param>
        /// <param name="carregaGrid"></param>
        /// <param name="msg"></param>
        /// <param name="msgSemSucesso"></param>
        protected virtual void salvar(DataRow entidade, Boolean carregaGrid,
            string msg, string msgSemSucesso)
        {
            Boolean validado = true;

            IDbPersist oConn = ConnAccess.getConn();

            if (validaSalvar)
            {
                if (this.Tipovalidacao.Equals(1))
                    validado = validar();
                else
                    validado = validar(entidade);
            }
            try
            {

                if (!validado)
                {
                    return;
                }

                string nomepk = ConnAccess.getNomePrimaryKey(entidade.Table);
                //entidade[nomepk] != DBNull.Value && Convert.ToInt32(entidade[nomepk])
                //entidade[nomepk] != DBNull.Value && PkId > 0

                if (entidade[nomepk] != DBNull.Value && ( PkId64 > 0 )   )
                {
                    object cod = ConnAccess.executeScalar(oConn, " select " + nomepk + " from " + entidade.Table.TableName + " where " + nomepk + " = " +
                            entidade[nomepk].ToString());

                    if (cod == null || cod == DBNull.Value)
                    {
                        PkId64 = 0; PkId = 0;
                    }

                }
                if (entidade[nomepk] != DBNull.Value &&  PkId64 > 0  )
                {
                    ConnAccess.Update(oConn, entidade, nomepk);
                }
                else
                {

                    ConnAccess.Insert(oConn, entidade, nomepk, this.save_autoincrement);
                    if (this.save_autoincrement)
                    {
                        entidade[nomepk] = ConnAccess.getMax(oConn, nomepk, entidade.Table.TableName, "");
                    }
                }


                if (carregaGrid)
                    this.carregaGrid(); 

                if (msg != String.Empty)
                    Alert(msg);

                if (this.limpaFormulario)
                    this.limpaForm();
            }
            catch (Exception e)
            {
                this.HouveErro = true;



                if (msgSemSucesso != String.Empty || e is System.Data.Odbc.OdbcException)
                {
                    string innerMsg = string.Empty;
                    if (e.InnerException != null)
                        innerMsg = " ( " + e.InnerException.Message + ") ";
                    if (msgSemSucesso != String.Empty)
                        Alert(msgSemSucesso + " :" + e.Message + innerMsg);
                    else
                        Alert(e.Message + innerMsg);
                }

            }
        }
        protected virtual void excluir(DataRow entidade, Boolean carregaGrid, string msg)
        {
            try
            {
                IDbPersist oConn = ConnAccess.getConn();

                ConnAccess.executeCommand(oConn, " delete from " + entidade.Table.TableName + " where id = " +
                      entidade["id"].ToString());

                if (carregaGrid)
                    this.carregaGrid();

                if (msg != String.Empty)
                    Alert(msg);

                this.limpaForm();
            }
            catch (System.Data.OleDb.OleDbException bex)
            {
                Alert("Não foi possível excluir este registro, pois " + bex.Message + "!");
                this.HouveErro = true;
                //Response.End();
            }
            catch (Exception e)
            {
                throw (e);
            }

        }

    #region Manipulando controles de tela




    protected virtual void setOnKeyPress(TextBox text, string texto)
    {

        text.Attributes.Remove("onkeypress");
        text.Attributes.Add("onkeypress", texto);
    }
    /// <summary>
    /// Força a escrita de apenas números no textbox
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setSoNumero(TextBox text)
    {

        text.Attributes.Remove("onkeypress");
        text.Attributes.Add("onkeypress", "return SoNumero(event)");
    }

    /// <summary>
    /// Força a escrita de apenas números no textbox
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setSoNumero(Control txt)
    {

        if (txt == null)
            return;

        if (txt is TextBox)
        {
            TextBox text = (TextBox)txt;
            setSoNumero(text);
        }
     }



     /// <summary>
     /// FOrça a escrita de apenas número e vírgula no text box
     /// </summary>
     protected virtual void setSoDecimal(Control txt)
     {

         if (txt == null)
             return;


         if (txt is TextBox)
         {
             TextBox text = (TextBox)txt;
             setSoDecimal(text);
         }
     }


    /// <summary>
    /// FOrça a escrita de apenas número e vírgula no text box
    /// </summary>
    protected virtual void setSoDecimal(TextBox text)
    {

        text.Attributes.Remove("onkeypress");
        text.Attributes.Add("onkeypress", "return Numerico(event,2);");

        //text.Attributes.Add("style", "text-align: right");
    }
    protected virtual void setSoDecimal(object text, int MaxLength)
    {
        if (text == null)
            return;

        if (text is TextBox)
        {
            setSoDecimal(((TextBox)text) , 18);
        }
    }

    protected virtual void setSoDecimal(TextBox text, int MaxLength)
    {

        text.Attributes.Remove("onkeypress");
        text.Attributes.Add("onkeypress", "return Numerico(event,2);");
        text.MaxLength = MaxLength;
    }

    /// <summary>
    /// Obtem valor decimal de um webControl
    /// </summary>
    /// <param name="ctr">control</param>
    /// <param name="returnNullIfEmpty">Se for vazio, retorne nulo - Default false</param>
    /// <returns></returns>
    protected decimal? getDec(Control ctr, Boolean returnNullIfEmpty)
    {

        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;

        return decimal.Parse(Nvl(getValor(ctr), 0).ToString());
    }
    protected decimal? getDec(Control ctr)
    {
        return getDec(ctr, false);
    }
    /// <summary>
    /// Obtem um Int32 de um webcontrol
    /// </summary>
    /// <param name="ctr"></param>
    /// <param name="returnNullIfEmpty"></param>
    /// <returns></returns>
    protected int? getInt32(Control ctr, Boolean returnNullIfEmpty)
    {
        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;

        return int.Parse(Nvl(getValor(ctr), 0).ToString());
    }

    protected int? getInt32(Control ctr)
    {
        return getInt32(ctr, false);
    }

    public void setPaginacao(GridView gvwDados)
    {
        
       gvwDados.PageSize = 30;
       gvwDados.AllowPaging = true;

    }

    protected Int16? getInt16(Control ctr, Boolean returnNullIfEmpty)
    {
        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;

        return short.Parse(Nvl(getValor(ctr), 0).ToString());
    }

    protected Int16? getInt16(Control ctr)
    {
        if (ctr is CheckBox)
        {
            if (((CheckBox)ctr).Checked)
                return 1;
            else
                return 0;

        }

         if (ctr is RadioButtonList)
        {
            RadioButtonList myrd = (RadioButtonList)ctr;

            if (myrd.SelectedItem == null)
                return null;

            if (myrd.SelectedValue.Equals("0"))
                return 0;


            if (myrd.SelectedValue.Equals("1"))
                return 1; 

        }

        return getInt16(ctr, false);
    }

    protected double? getDbl(Control ctr, Boolean returnNullIfEmpty)
    {
        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;


        return double.Parse(Nvl(getValor(ctr), 0).ToString());
    }

    protected double? getDbl(Control ctr)
    {
        return getDbl(ctr, false);
    }



    /// <summary>
    /// Obtem um Long de um webcontrol
    /// </summary>
    /// <param name="ctr"></param>
    /// <param name="returnNullIfEmpty"></param>
    /// <returns></returns>
    protected long? getInt64(Control ctr, Boolean returnNullIfEmpty)
    {
      


        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;

        return long.Parse(Nvl(getValor(ctr), 0).ToString());
    }


    protected virtual void setMaskData(object text)
    {
        if (text == null)
            return;

        if (text is TextBox)
        {
            setMaskData( ((TextBox)text) );
        }
    }

    /// <summary>
    /// Seta a máscara de data
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setMaskData(TextBox text)
    {
        text.Attributes.Remove("onkeypress");
        text.Attributes.Add("onkeypress", "return mascara(event,this,'##/##/####');");
        text.MaxLength = 10;
    }


    /// <summary>
    /// Seta a máscara de hora
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setMaskHora(TextBox text)
    {
        text.Attributes.Add("onkeypress", "return mascara(event,this,'##:##:##');");
        text.MaxLength = 8;

    }

    protected virtual void setMaskTelefone(TextBox text)
    {
        // return txtBoxFormat(this, '(99)9999-9999', event);
        text.Attributes.Add("onkeypress", "return txtBoxFormat(this, '(99)9999-9999', event);");

    }


    /// <summary>
    /// Seta qualquer tipo de máscara. Exemplo: ###.###.###-## (cpf)
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setMask(TextBox text, String Mask)
    {
        text.Attributes.Add("onkeypress", "return mascara(event,this,'" + Mask + "');");

    }
    /// <summary>
    /// Converte um valor para real..
    /// </summary>
    /// <param name="cod"></param>
    /// <returns></returns>
    protected virtual string ToReal(object cod)
    {
        if (cod == null || cod == String.Empty)
            return string.Empty;

        return double.Parse(cod.ToString()).ToString("c").Replace("R$", "");
    }



    #endregion

    //public override void Dispose()
    //{
    //    //Business.CompanyBusiness bc = new CompanyBusiness();
    //    //bc.Disconnect();

    //    //base.Dispose();
    //}

    /// <summary>
    /// Coloca a data na div my_calendar
    /// </summary>
    public void setDataMyCalendarDiv(HtmlGenericControl div)
    {
        string mes = this.nomeMes( DateTime.Now.Month ).Substring(0,3).ToUpper();

        div.InnerHtml = "<p>"+mes+"<br />"+DateTime.Now.Day.ToString().PadLeft(2,'0')+"</p>";
        
        
    }

    /// <summary>
    /// Camel case em um texto..
    /// </summary>
    /// <param name="inputCamelCaseString"></param>
    /// <returns></returns>
    public static string SplitCamelCase(string inputCamelCaseString)
    {

        string tx = inputCamelCaseString.ToLower();

        string[] ar = tx.Split("_".ToCharArray());

        string ret = string.Empty;
        for (int i = 0; i < ar.Length; i++)
        {
            ret += ar[i].Substring(0, 1).ToUpper() +
                    ar[i].Substring(1, ar[i].Length - 1);
        }

        return ret;

        // return System.Text.RegularExpressions.Regex.Replace(inputCamelCaseString, 
        //     "([A-Z])",         " $1",  System.Text.RegularExpressions.RegexOptions.Compiled).Trim();        
    }

    /// <summary>
    /// Pega tudo  oque está preenchido na tela e faz um datatable no formato campo / valor ..
    /// os valores serão os mais textuais possíveis, sem envolver códigos.. isso para 
    /// faclitar o uso nos logs do sistema.
    /// </summary>
    /// <param name="frm"></param>
    /// <param name="dtRegistro"></param>
    /// <returns></returns>
    public virtual DataTable getRegistroTela(HtmlForm frm, DataTable dtRegistro)
    {
        DataTable dtSaida = new DataTable();
        dtSaida.Columns.Add(new DataColumn("campo", typeof(string)));
        dtSaida.Columns.Add(new DataColumn("valor", typeof(string)));
        dtSaida.Columns.Add(new DataColumn("campotela", typeof(string)));
        dtSaida.Columns.Add(new DataColumn("tipocampotela", typeof(string)));
        dtSaida.Columns.Add(new DataColumn("dt_registro", typeof(DateTime)));

        for (int i = 0; i < dtRegistro.Columns.Count; i++)
        {
            DataRow dr = dtSaida.NewRow();
            dr["campo"] = dtRegistro.Columns[i].ColumnName;
            dr["dt_registro"] = DateTime.Now;

            Control ctr =  Utilities.Format.localizaControl("txt" +
                SplitCamelCase(dtRegistro.Columns[i].ColumnName), frm);

            if (ctr != null)
            {
                dr["campotela"] = ctr.ID;
                dr["tipocampotela"] = ctr.GetType().Name;

                if (ctr is DropDownList || ctr is RadioButtonList)
                {
                    ListControl dow = (ListControl)ctr;
                    if (getValor(dow) == String.Empty)
                    {
                        dr["valor"] = String.Empty;
                    }
                    else
                    {
                        dr["valor"] = dow.Items[dow.SelectedIndex].Text;
                    }
                }
                else if (ctr is CheckBoxList)
                {
                    dr["valor"] = getValoresCheckList2((CheckBoxList)ctr, ", ");
                }
                else
                {
                    dr["valor"] = getValor(ctr);
                }


            }

            dtSaida.Rows.Add(dr);
        }


        return dtSaida;
    }

    /// <summary>
    /// Obtem uma pk para cadastro da tabela.. exclusivo para MidiaClip.
    /// </summary>
    /// <param name="tabela"></param>
    /// <param name="sequencial"></param>
    /// <param name="ano"></param>
    /// <returns></returns>
    public int getPKTabela(object sequencial, int ano)
    {
        string pkk = SessionFacade.Servidor.ToString() + ano.ToString() +
                     sequencial.ToString();

        return Convert.ToInt32(pkk);    
    }


    /// <summary>
    /// Get e Set uma data para representar a hora em que a tela foi carregada pela primeira vez.
    /// </summary>
    public DateTime DataCarregamentoDaTela
    {
        get
        {
            if (ViewState["DataCarregamentoDaTela"] == null)
                ViewState["DataCarregamentoDaTela"] = DateTime.Now;

            return Convert.ToDateTime(ViewState["DataCarregamentoDaTela"]);
        }
        set
        {
            ViewState["DataCarregamentoDaTela"] = value;
        }
    }

    /// <summary>
    /// Get e Set uma data para representar a hora em que a tela vai ser salva, isto será usado no relatório.
    /// </summary>
    public DateTime DataSalvamentoDaTela
    {
        get
        {
            if (ViewState["DataSalvamentoDaTela"] == null)
                ViewState["DataSalvamentoDaTela"] = DateTime.Now;

            return Convert.ToDateTime(ViewState["DataSalvamentoDaTela"]);
        }
        set
        {
            ViewState["DataSalvamentoDaTela"] = value;
        }
    }

    /// <summary>
    /// Retornará uma ação do que está acontecendo na tela.. 1 - Inserir, 2 - Update, 3 - Exclusão
    /// </summary>
    public int AcaoID
    {
        get
        {
            if (ViewState["AcaoID"] == null)
                ViewState["AcaoID"] = 0;

            return Convert.ToInt32(ViewState["AcaoID"]);
        }
        set
        {
            ViewState["AcaoID"] = value;
        }
    }



    /// <summary>
    /// Neste momento poderá ser feito alguma formatação ao data row antes de enviá-lo para a persistência dos dados
    /// 
    /// </summary>
    /// <param name="dr"></param>
    public virtual void formataRowAntesSalvar(ref DataRow dr)
    {
       // Se a tabela for auto incrementável, não precisamos gerar uma id.
        if (!this.save_autoincrement)
        {
           // Entities.Chaves.garanteChaves(ref dr);
        }
    }


    #region "Estáticas para lidar com hora"
    public static string TempoParaTexto(int segundos, bool testaSeMaiorQue24)
    {
        return TempoParaTexto(segundos, testaSeMaiorQue24, false);
    }

    /// <summary>
    /// Função para converter o tempo em segundos para texto 
    /// Posição 0 e 1 são as horas -> converter para inteiro e multiplicar por 3600.
    /// Posição 3 e 4 são os minutos -> converter para inteiro e multiplicar por 60.
    /// As 2 ultimas casas são segundos -> apenas somar com o restante
    /// </summary>
    /// <param name="segundos"></param>
    /// <param name="testaSeMaiorQue24">Testa se é maior que 24.. caso true então vai subtrair por 24</param>
    /// <returns></returns>
    public static string TempoParaTexto(int segundos, bool testaSeMaiorQue24, bool forcaZeroSegundo)
    {
        if (testaSeMaiorQue24)
        {
            while (Convert.ToInt32(segundos / 3600) >= 24)
            {
                //reduzimos 24 horas.
                segundos -= (24 * 3600);

            }
        }


        string horas = Convert.ToInt32(segundos / 3600).ToString();
        string minutos = Convert.ToInt32((segundos % 3600) / 60).ToString();
        string seg = Convert.ToInt32((segundos % 3600) % 60).ToString();
        if (forcaZeroSegundo)
            seg = "0";

        if (horas.Length == 1)
        {
            horas = "0" + horas;
        }
        if (minutos.Length == 1)
        {
            minutos = "0" + minutos;
        }
        if (seg.Length == 1)
        {
            seg = "0" + seg;
        }
        string tempo = horas + ":" + minutos + ":" + seg;
        return tempo;
    }

    /// <summary>
    /// o texto em formato HH:MM:SS segundos para TimeSpan..
    /// </summary>
    /// <param name="tempo"></param>
    /// <returns></returns>
    public static TimeSpan ConverteTextoParaTimeSpan(string tempo)
    {
        string[] ar_tm = tempo.Split(':');

        TimeSpan time = new TimeSpan(
            Convert.ToInt32(ar_tm[0]),
            Convert.ToInt32(ar_tm[1]),
            Convert.ToInt32(ar_tm[2]));

        return time;


    }

    /// <summary>
    /// o texto em formato HH:MM:SS para segundos..
    /// </summary>
    /// <param name="tempo"></param>
    /// <returns></returns>
    public static int ConverteTextoParaSegundos(string tempo)
    {
        if (tempo == "")
            tempo = "00:00:00";

        string[] ar_tm = tempo.Split(':');
        int segundos = 0;

        //   Posição 0 e 1 são as horas -> converter para inteiro e multiplicar por 3600.
        /// Posição 3 e 4 são os minutos -> converter para inteiro e multiplicar por 60.
        /// As 2 ultimas casas são segundos -> apenas somar com o restante

        if (ar_tm.Length > 2)
        {
            segundos =
                  (Convert.ToInt32(ar_tm[0]) * 3600) +
                   (Convert.ToInt32(ar_tm[1]) * 60) +
                   (Convert.ToInt32(ar_tm[2]));
        }
        else
        {
            segundos =                  
                   (Convert.ToInt32(ar_tm[0]) * 60) +
                   (Convert.ToInt32(ar_tm[1]));
        }
        return segundos;


    }

    #endregion
}
