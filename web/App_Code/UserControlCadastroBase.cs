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
using DataAccess;

/// <summary>
/// Summary description for UserControlCadastroBase
/// </summary>
public class UserControlCadastroBase : System.Web.UI.UserControl
{

    /// <summary>
    /// Chama o método validar() ao tentar salvar - Default: True.
    /// </summary>
    protected bool validaSalvar = true;

    /// <summary>
    /// Obtem valor de uma coluna de um datatable
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    protected string getValorDataTable(DataTable dt, string prop)
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

                propriedade += "," + dt.Rows[i][prop].ToString();
            }

        }
       // dt.Dispose();
        return propriedade;

    }
    public string getRequest(string prop)
    {
        return getRequest(prop, false);
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



    public string getRequest(string prop, bool precisao)
    {

        if (Request.Form != null)
        {
            for (int i = 0; i < Request.Form.AllKeys.Length; i++)
            {
                try
                {
                    if (Request.Form.AllKeys[i].Length >= prop.Length)
                    {
                        if (Request.Form.AllKeys[i].Substring(
                               Request.Form.AllKeys[i].Length - prop.Length,
                               prop.Length) == prop)
                        {

                            return Request.Form[Request.Form.AllKeys[i]].Trim();
                        }

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
                    if (Request.QueryString.AllKeys[i].Length >= prop.Length)
                    {
                        if (Request.QueryString.AllKeys[i].Substring(
                               Request.QueryString.AllKeys[i].Length - prop.Length,
                               prop.Length) == prop)
                        {

                            return Request.QueryString[Request.QueryString.AllKeys[i]].Trim();
                        }

                    }


                }
                catch { }
            }
        }
        return String.Empty;

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
    /// Simula um request com o mesmo comportamento do asp
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string request(String str)
    {
        string Str = string.Empty;

        if (Page.Request.QueryString[str] != null && Page.Request.QueryString[str].ToString() != String.Empty)
            return Page.Request.QueryString[str].ToString();

        if (Page.Request.Form[str] != null && Page.Request.Form[str].ToString() != String.Empty)
            return Page.Request.Form[str].ToString();


        return Str;

    }

    public IList<T> getblistaCadastro<T>()
    {
        if (Session[this.ToString() + "ucblistaCadastro"] == null)
            Session.Add(this.ToString() + "ucblistaCadastro", new List<T>());

        return (IList<T>)Session[this.ToString() + "ucblistaCadastro"];
    }

    public void setblistaCadastro<T>(object value)
    {
        Session.Remove(this.ToString() + "ucblistaCadastro");
        if (value != null)
            Session.Add(this.ToString() + "ucblistaCadastro", value);
    }




    public static void loadDropDownInteger(DropDownList combo, int Inicio, int Fim, object sel)
    {
        for (int i = Inicio; i <= Fim; i++)
        {
            combo.Items.Add(i.ToString());
        }
        if (sel != null && sel.ToString() != String.Empty)
            combo.SelectedValue = sel.ToString();

    }


    public static void loadDropDowMes(DropDownList combo, object sel)
    {
    //    for (int i = 1; i <= 12; i++)
    //    {
    //        combo.Items.Add(i.ToString());
    //    }
    //    if (sel != null && sel.ToString() != String.Empty)
    //        combo.SelectedIndex = int.Parse(sel.ToString()) - 1;


        UserControlCadastroBase mybase = new UserControlCadastroBase();

        mybase.carregaCombo(combo, mybase.listaMes, "Codigo", "Nome", (sel == null ? string.Empty : sel.ToString())); 
    }


    protected DateTime? getDate(Control ctr, Boolean returnNullIfEmpty, string hora)
    {
        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;
        try
        {
            if (hora.IndexOf(":") > -1)
                return DateTime.Parse(Nvl(getValor(ctr), DateTime.MinValue).ToString());
            else
               return DateTime.Parse(Nvl(getValor(ctr) +" "+ hora, DateTime.MinValue).ToString());
        }
        catch { return null; }
    }

    protected DateTime? getDate(Control ctr)
    {
        return this.getDate(ctr, true, " 00:00");
    }




    private AjaxControlToolkit.TabPanel ajaxTabAnexo;
    /// <summary>
    /// Tab Panel da página que contém o anexo
    /// </summary>
    public AjaxControlToolkit.TabPanel AjaxTabAnexo
    {
        get { return ajaxTabAnexo; }
        set { ajaxTabAnexo = value; }
    }




    #region Parametros pro log
    protected Label labelOperador;
    protected Label labelOperadorDataAtualizacao;
    protected Type tpEntBase;

    private HtmlControl divInfolog;

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

    public UserControlCadastroBase()
    {
        //
        // TODO: Add constructor logic here
        //
      //  logBase = new LogSistemaBusiness();
    }
    /// <summary>
    /// Define se, apos salvar, a tela deverá limpar o formulário ou não..
    /// </summary>
    public Boolean limpaFormulario = true;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            VsLista.Clear();
        }
    }

    protected bool campoPreenchido(Control ctr, String MsgAlert)
    {
        if (getValor(ctr) == String.Empty || getValor(ctr) == "")
        {
            if (MsgAlert != String.Empty)
                Alert(MsgAlert);

            return false;
        }
        return true;
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

    public void setVisible(HtmlGenericControl ctr, string display)
    {
        //ctr.Attributes.Remove("style");
        //ctr.Attributes.Add("style", "display:" + display);
        ctr.Style.Value = "display:" + display;
    }


    public void setValor(Control ctr, DataRow oBase, String propriedade)
    {

        if (oBase == null || oBase[propriedade] == DBNull.Value)
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


    public void setValorNs(TextBox ctr, object valor, string Ns)
    {
        string sel = String.Empty;
        if (valor != null)
            sel = valor.ToString();

        if (valor != null && valor is double)
        {
            sel = Convert.ToDouble(valor).ToString("N"+Ns);

        }
        if (valor != null && valor is decimal)
        {
            sel = Convert.ToDecimal(valor).ToString("N"+Ns);

        }
        if (sel.IndexOf(",") > -1)
        {

        inicio:

            //Função que retira os zeros depois da vírgula.
            for (int i = sel.Length - 1; i >= 0; i++)
            {



                if (sel.Substring(i, 1) == "0")
                {
                    sel = sel.Substring(0, i);
                    goto inicio;
                }
                else
                {
                    if (sel.Substring(i, 1) == ",")
                    {
                        sel = sel.Substring(0, i);
                        goto inicio;
                    }

                    break;
                }


            }
        }

        ctr.Text = sel;

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
    /// Permite o registro de uma PK - 2 em ViewState.
    /// </summary>
    public int sPkId
    {
        get
        {
            if (ViewState["sPkId"] == null)
                ViewState["sPkId"] = 0;

            return Convert.ToInt32(ViewState["sPkId"]);
        }
        set
        {
            ViewState["sPkId"] = value;
        }
    }

    /// <summary>
    /// Permite o registro de uma PK - 3 em ViewState.
    /// </summary>
    public int sPkId2
    {
        get
        {
            if (ViewState["sPkId2"] == null)
                ViewState["sPkId2"] = 0;

            return Convert.ToInt32(ViewState["sPkId2"]);
        }
        set
        {
            ViewState["sPkId2"] = value;
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
        //Não vou usar o MSgBox por enquanto..
        //if (this.MsgBox != null)
        //{
        //    MsgBox.setVisible(true);
        //    MsgBox.setIsVisible(true);
        //    MsgBox.setTitulo("Atenção");
        //    MsgBox.setTextButton3("OK");
        //    MsgBox.setMsg(msg);
        //    MsgBox.ShowMe();
        //}
        //else
        js.Alert(msg, this.Page);
    }

    public void Alert(string msg, string Titulo, string textBotaoFechar)
    {

        //if (this.MsgBox != null)
        //{
        //    MsgBox.setVisible(true);
        //    MsgBox.setIsVisible(true);
        //    MsgBox.setTitulo(Titulo);
        //    MsgBox.setTextButton3(textBotaoFechar);
        //    MsgBox.setMsg(msg);
        //    MsgBox.ShowMe();
        //}
        //else
        js.Alert(msg, this.Page);
    }


    protected bool EhData(String data, string msg)
    {
        try
        {
            DateTime dt = DateTime.Parse(data);
            return true;
        }
        catch
        {
            Alert(msg);
            return false;
        }

        //return false;

    }

    protected bool EhInteiro(String data)
    {
        try
        {
            Int64 dt = Int64.Parse(data);
            
            return true;
        }
        catch
        {
            return false;
        }

        //return false;

    }

    public virtual void carregaGrid() { }

    public virtual void carregaForm(DataRow entidade) { }
    public virtual DataRow obtemForm()
    {

        throw new Exception("Método não implementado!");
    }

    //public virtual void DadosLogAtualizacao(Entities.LogSistema log)
    //{

    //    labelOperadorDataAtualizacao.Visible = false;
    //    labelOperador.Visible = false;

    //    if (divInfoLog != null)
    //        divInfoLog.Visible = false;

    //    if (log == null)
    //    {


    //        setValor(labelOperadorDataAtualizacao, String.Empty);
    //        setValor(labelOperador, String.Empty);

    //        return;
    //    }

    //    if (logBase != null && labelOperador != null && labelOperadorDataAtualizacao != null &&
    //         tpEntBase != null)
    //    {



    //        Entities.LogSistema logg =
    //            logBase.obterUltimoLog(tpEntBase, log.RegistroId, log.NomeTabela);

    //        if (logg == null)
    //        {
    //            setValor(labelOperadorDataAtualizacao, String.Empty);
    //            setValor(labelOperador, String.Empty);
    //            return;
    //        }
    //        labelOperadorDataAtualizacao.Visible = true;
    //        labelOperador.Visible = true;

    //        if (divInfoLog != null)
    //            divInfoLog.Visible = true;


    //        if (logg.User != null)
    //            labelOperador.Text = logg.User.UserName;
    //        else
    //            labelOperador.Text = "Usuário já excluído, id: " + logg.UsuarioId.ToString() + " ";

    //        labelOperadorDataAtualizacao.Text = logg.Data.ToShortDateString() + " " + logg.Data.ToShortTimeString();

    //    }

    //}

    public virtual void setaEstiloGrid(GridView gridDados)
    {
        gridDados.RowCreated += new GridViewRowEventHandler(gridDados_RowCreated);
        gridDados.RowDataBound += new GridViewRowEventHandler(gridDados_RowDataBound);
        gridDados.PageIndexChanging += new GridViewPageEventHandler(gridDados_PageIndexChanging);
        gridDados.RowCommand += new GridViewCommandEventHandler(gridDados_RowCommand);
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

    protected virtual void limpaForm() { }
    protected virtual Boolean validar()
    {
        return true;
    }
    protected virtual Boolean validar(DataRow entidade)
    {
        return true;
    }

    /// <summary>
    /// Cuida de salvar informações provenientes da página
    /// </summary>
    /// <param name="bas"></param>
    /// <param name="entidade"></param>
    /// <param name="carregaGrid"></param>
    /// <param name="msg"></param>
    protected virtual void salvar(DataRow entidade, Boolean carregaGrid, string msg)
    {
        salvar(entidade, carregaGrid, msg, String.Empty);
    }

    /// <summary>
    /// Cuida de salvar informações provenientes da página
    /// </summary>
    /// <param name="bas"></param>
    /// <param name="entidade"></param>
    /// <param name="carregaGrid"></param>
    /// <param name="msg"></param>
    /// <param name="msgSemSucesso"></param>
    protected virtual void salvar(DataRow entidade,  Boolean carregaGrid, 
        string msg, string msgSemSucesso)
    {
        Boolean validado = true;

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



            IDbPersist oConn = ConnAccess.getConn();


            if (entidade[nomepk] != DBNull.Value && Convert.ToInt32(entidade[nomepk]) > 0)
            {
                ConnAccess.Update(oConn, entidade, nomepk);
            }
            else
            {

                ConnAccess.Insert(oConn, entidade, nomepk, true);
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



            if (msgSemSucesso != String.Empty || e is System.Data.OleDb.OleDbException)
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
            //ConnAccess.Delete(entidade);

            string nomepk = ConnAccess.getNomePrimaryKey(entidade.Table);

            string sql = " delete from " + entidade.Table.TableName + " where " +nomepk + " = " +  
                entidade[nomepk].ToString();

            ConnAccess.executeCommand(ConnAccess.getConn(), sql);

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


    protected virtual void setReadOnly(Control ctr)
    {
        if (ctr is TextBox)
        {
            TextBox text = (TextBox)ctr;
            text.Attributes.Remove("readonly");
            text.Attributes.Add("readonly", "");
        }
        if (ctr is DropDownList)
        {
            DropDownList text = (DropDownList)ctr;
            text.Attributes.Remove("disabled");
            text.Attributes.Add("disabled", "true");
        }



    }


    protected virtual void setOnKeyPress(TextBox text, string texto)
    {

        text.Attributes.Remove("onkeypress");
        text.Attributes.Add("onkeypress", texto);
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
    protected virtual void setSoDecimal(Control txt, int max)
    {

        if (txt == null)
            return;


        if (txt is TextBox)
        {
            TextBox text = (TextBox)txt;
            setSoDecimal(text, max);
        }
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
    /// FOrça a escrita de apenas número e vírgula no text box
    /// </summary>
    protected virtual void setSoDecimal(TextBox text)
    {

        text.Attributes.Remove("onkeypress");
        text.Attributes.Add("onkeypress", "return Numerico(event,2);");
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

    /// <summary>
    /// Obtem um int16 de um webcontrol
    /// </summary>
    /// <param name="ctr"></param>
    /// <param name="returnNullIfEmpty"></param>
    /// <returns></returns>
    protected short? getInt16(Control ctr, Boolean returnNullIfEmpty)
    {
        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;

        return short.Parse(Nvl(getValor(ctr), 0).ToString());
    }

    protected short? getInt16(Control ctr)
    {
        return getInt16(ctr, false);
    }


    protected double? getDbl(Control ctr, Boolean returnNullIfEmpty)
    {
        if (getValor(ctr) == String.Empty && returnNullIfEmpty)
            return null;

        try
        {
            return double.Parse(Nvl(getValor(ctr), 0).ToString());
        }
        catch
        {
            return 0;
        }
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
    /// Seta a máscara de data
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setMaskData(Control ctr)
    {

        if (ctr is TextBox)
        {

            setMaskData((TextBox)ctr);
        }
    }

    /// <summary>
    /// Seta a máscara de hora
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setMaskHora(TextBox text)
    {
        setMaskHora(text, 5);
    }

    /// <summary>
    /// Seta a máscara de hora
    /// </summary>
    /// <param name="text"></param>
    protected virtual void setMaskHora(TextBox text, int tam)
    {
        if (text == null)
            return;

        if (tam > 5)
        {

            text.Attributes.Add("onkeypress", "return mascara(event,this,'##:##:##');");
        }
        else
        {

            text.Attributes.Add("onkeypress", "return mascara(event,this,'##:##');");
        }
        text.MaxLength = tam;

    }

    protected virtual void setMaskTelefone(TextBox text)
    {
        text.Attributes.Add("onkeypress", "return mascara(event,this,'(##)####-####');");

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

    /// <summary>
    /// Caso o valor seja Nulo, converteremos para DBNULL 
    /// </summary>
    /// <param name="valor"></param>
    /// <returns></returns>
    protected virtual object nullToDNNull(object valor)
    {
        if (valor == null)
            return DBNull.Value;

        return valor;

    }

    #endregion
}
