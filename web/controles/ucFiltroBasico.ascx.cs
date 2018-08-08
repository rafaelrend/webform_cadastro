using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using DataAccess;

public partial class ucFiltroBasico :  IFiltro
{
    protected void Page_Load(object sender, EventArgs e)
    {
        imgMostraFiltroAvancado.OnClientClick =
             " document.getElementById('" +
              div_filtro_avancado.ClientID + "').style.display='block'; return false; ";


        ImageButton1.OnClientClick = "setEventTarget('img_fechar',true); return false ";
        ImageButton2.OnClientClick = "setEventTarget('img_limpar',true); return false ";

        for (int zz = 0; zz < g_glob_tipo_filtro.Items.Count; zz++)
        {
            g_glob_tipo_filtro.Items[zz].Attributes.Remove("onclick");
            g_glob_tipo_filtro.Items[zz].Attributes.Add("onclick", "setEventTarget('g_glob_tipo_filtro',true); return false ");

        }

        if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].ToString() == "g_glob_tipo_filtro")
        {
            g_glob_tipo_filtro_SelectedIndexChanged(g_glob_tipo_filtro, EventArgs.Empty);
        }
        if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].ToString() == "img_fechar")
        {
            ImageButton1_Click(ImageButton1, EventArgs.Empty);
        }
        if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].ToString() == "img_limpar")
        {
            ImageButton2_Click(ImageButton2, EventArgs.Empty);
        }
      
       



        //ImageButton1.OnClientClick =
        //     " document.getElementById('" +
        //      div_filtro_avancado.ClientID + "').style.display='none'; return false; ";
    }
    //ArrayList arrAddFiltro = ArrayList
    public override void addHiddenFiltro(string nome, string valor)
    {
        cmpAdicional.InnerHtml = "<input type='hidden' name='" + nome + "' value=" +
            "'" + valor + "' > ";
    }
    //Hora: 29/11/2012 12:35:17
//Erro: Sys.WebForms.PageRequestManagerServerErrorException: Uma coluna chamada 'link' já pertence a esta DataTable.
//Arquivo-fonte: http://mdclipweb.midiaclip.com.br/ScriptResource.axd?d=GcdA6tA455CF2bHIBlp7fIaKkUltG6JiKddDsu5CTWugowcFQ4rltpoZBwL6g6rjU8yEKxxBPdsDf4CZ7CtISqnZWiaS_Qb1ooUXok0OxVuZt0Mz_yC1oZ4omrSM5ihsk3CUsxlt_dJ2UQ121S1rNN2HKKsQLHxhHujrpRWwzOlXPlfC0&t=ffffffffb868b5f4
//Linha: 1111
    public override string Tabela
    {
        
        set { ViewState["_hd_tabela"] = value; }
        get
        {
            if (ViewState["_hd_tabela"] == null)
                ViewState["_hd_tabela"] = String.Empty;

           return  ViewState["_hd_tabela"].ToString();
        }

    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override string NoClearAction
    {

        set { ViewState["_NoClearAction"] = value; }
        get
        {
            if (ViewState["_NoClearAction"] == null)
                ViewState["_NoClearAction"] = String.Empty;

            return ViewState["_NoClearAction"].ToString();
        }

    }

    public override string Alias
    {
        set { ViewState["_alias"] = value; }
        get
        {
            if (ViewState["_alias"] == null)
                ViewState["_alias"] = String.Empty;

           return  ViewState["_alias"].ToString();
        }
    }


    public override string CaminhoExcel
    {
        
          set { ViewState["_hd_caminho_filtros"] = value;
         // carregaCamposFiltro();
          
          }
        get
        {
            if (ViewState["_hd_caminho_filtros"] == null)
                ViewState["_hd_caminho_filtros"] = String.Empty;

           return  ViewState["_hd_caminho_filtros"].ToString();
        }

    }

    public override bool TemFiltro
    {
        get
        {
            return temFiltro;
        }
        set
        {
            temFiltro = value;
        }
    }

    public override bool bloquear
    {
        set
        {
            if (value)
            {
                dd_glob_cadastro.Enabled = false;
                g_glob_tipo_filtro.Enabled = false;
                imgMostraFiltroAvancado.Visible = false;
                txt_glob_search.Enabled = false;
                bt_glob_search.Enabled = false;
                imgExportaExcel.Visible = false;

            }
        }

    }
    
    private bool temFiltro = false;

    private string applicaAlias(string alias, string coluna)
    {
        if (coluna.IndexOf(".").Equals(-1) )
            return alias + coluna;

        return coluna;
    }

    public override string getSqlFiltro()
    {

        string MsgFiltro = string.Empty;
        string filtro = string.Empty;
        string tipo_valor_filtro_simples = "System.String";
        string label_valor_filtro_simples = "";

        DataView dw = null;

        try
        {
            dw = getCampos();
        }
        catch { }

        if (dw == null)
            dw = getCamposByExcelLibrary();

        bool filtroAvancado = false;

        if (dw == null)
            return String.Empty;


        string regra_simples = "";

        for (int i = 0; i < dw.Count; i++)
        {
            string nome = dw[i]["nome"].ToString();
            string tipo = dw[i]["tipo"].ToString();
            string label = dw[i]["label"].ToString();

            string regra = string.Empty;

            if (dw.Table.Columns.Contains("regra"))
            {
                regra = dw[i]["regra"].ToString();

            }

            if (nome.Equals(getRequest(dd_glob_cadastro.ID)) && dw.Table.Columns.Contains("regra") )
            {

                regra_simples = dw[i]["regra"].ToString();
            }

           string lista = string.Empty;

            if ( dw.Table.Columns.Contains("lista") && dw[i]["lista"] != DBNull.Value 
                 && dw[i]["lista"].ToString() != String.Empty ){
                lista = dw[i]["lista"].ToString();
            }
                string req = "g_txt_filtro_" + nome;

                if (getRequest(dd_glob_cadastro.ID, true) == nome)
                {
                tipo_valor_filtro_simples =  tipo;
                label_valor_filtro_simples = label;
            }

            string valorReq = getRequest(req, true);

           

            if (tipo == "System.Double" || tipo == "System.Decimal")
            {

                string req2 = "g_txt_filtro_" + nome+"2";

                if ( valorReq != String.Empty)
                {
                    filtro += " and " + applicaAlias(Alias, nome) + " >= " + valorReq;               
                   
                }
                if (getRequest(req2, true) != String.Empty)
                {
                    filtro += " and " + applicaAlias(Alias, nome) + " <=  " + getRequest(req2, true);
                 
                }
                if (valorReq != String.Empty || (getRequest(req2) != String.Empty))
                {
                    if (MsgFiltro != "")
                        MsgFiltro += " ";
                    
                    MsgFiltro += "<b>" + label + "</b>:";
                    
                    if ( valorReq != String.Empty)
                        MsgFiltro += " a partir de " + valorReq;
                    if (getRequest(req2, true) != String.Empty)
                        MsgFiltro += " até " + getRequest(req2, true);
                }
            }
            else if (tipo == "System.Int32" || tipo == "System.Int16" || tipo == "System.Int64" || tipo == "System.Byte")
            {
                if (getRequest(req) != String.Empty)
                {
                    if (regra == String.Empty)
                        filtro += " and " + applicaAlias(Alias, nome) + " = " + getRequest(req, true);
                    else
                        filtro += " " + regra.Replace("{0}", getRequest(req, true));

                    if (lista.Trim().Length > 1) //Se for um combo, pelo o text vindo do select dele..
                    {
                        Control cr = Utilities.Format.localizaControl(req, this.div_filtro_avancado);
                        if (cr is DropDownList)
                        {
                            if ( ((DropDownList)cr).SelectedItem != null )
                                valorReq = ((DropDownList)cr).SelectedItem.Text;
                        }
                    }
                  
                    if (MsgFiltro != "")
                        MsgFiltro += " ";

                    MsgFiltro += "<b>" + label + "</b>: " + valorReq + "";
                   
                }
            }
            else if (tipo == "System.DateTime")
            {
                string dt_inicio = getRequest(req + "$" + "txtDtInicio");

                string hora_inicio = base.Nvl( getRequest(req + "$" + "txtDtInicioHora"),"00:00:00").ToString();

                string dt_fim = getRequest(req + "$" + "txtDtFim");

                string hora_fim = base.Nvl(getRequest(req + "$" + "txtDtFimHora"), "23:59:59").ToString();

                ///Se não tem filtro, porém devemos ter um valor padrão.. vamos saber aqui.
                if (valoresPadrao != null && valoresPadrao[nome] != null && dt_inicio == String.Empty && dt_fim == String.Empty)
                {
                    string[] subreq = valoresPadrao[nome].ToString().Split('|');

                    dt_inicio = subreq[0];
                    dt_fim = subreq[1];

                    Control crData = Utilities.Format.localizaControl("g_txt_filtro_" + nome, this.div_filtro_avancado);

                    if (crData != null && crData is IField)
                    {
                        ((IField)crData).Value = valoresPadrao[nome].ToString();
                    }

                }

                if (dt_inicio != String.Empty)
                {

                    if ( regra == String.Empty )
                         filtro += " and " + applicaAlias(Alias, nome) + " >= " + toMySqlDate(dt_inicio, 0, " " + hora_inicio);
                     else
                         filtro += " " + regra.Replace("{0}"," >= " + toMySqlDate(dt_inicio, 0, " " + hora_inicio));
                  
                }
                if (dt_fim != String.Empty)
                {

                    if (regra == String.Empty)
                        filtro += " and " + applicaAlias(Alias, nome) + " <= " + toMySqlDate(dt_fim, 0, " " + hora_fim);
                    else
                        filtro += " " + regra.Replace("{0}", " <= " + toMySqlDate(dt_fim, 0, " " + hora_fim));

                    //filtro += " and " + applicaAlias(Alias, nome) + " <= " + toMySqlDate(dt_fim, 0, " "+ hora_fim);
                   
                }

                if (dt_inicio != String.Empty || dt_fim != String.Empty)
                {

                    if (MsgFiltro != "")
                        MsgFiltro += " ";

                    MsgFiltro += "<b>" + label + "</b>:";

                    if (dt_inicio != String.Empty)
                        MsgFiltro += " a partir de " + dt_inicio+ " " + hora_inicio;
                    if (dt_fim != String.Empty)
                        MsgFiltro += " até " + dt_fim + " " + hora_fim;
                }


            }
            else
            {
                if (getRequest(req) != String.Empty)
                {


                    if (regra == String.Empty)
                        filtro += " and " + applicaAlias(Alias, nome) + " like  '%" + getRequest(req) + "%'";
                    else
                        filtro += " " + regra.Replace("{0}", getRequest(req, true));


                    if (MsgFiltro != "")
                        MsgFiltro += " , ";

                    MsgFiltro += "<b>" + label + "</b>: \"" +valorReq+"\"";

                }
            }
        }
        //Estamos usando o filtro avançado.
        if (filtro != String.Empty)
           filtroAvancado = true;

        if (filtro == String.Empty && getRequest(txt_glob_search.ID) != String.Empty )
        {
            if (EhInteiro(getRequest(txt_glob_search.ID)) && (tipo_valor_filtro_simples == "System.Int32" || tipo_valor_filtro_simples == "System.Int16" || tipo_valor_filtro_simples == "System.Int64"
                || tipo_valor_filtro_simples == "System.Byte"))
            {
                filtro += " and " + Alias + getRequest(dd_glob_cadastro.ID) + " = " + getRequest(txt_glob_search.ID);
                MsgFiltro += "<b>" + label_valor_filtro_simples + "</b>: " + getRequest(txt_glob_search.ID) + "";
            }
            else
            {

                if (regra_simples == String.Empty)
                {



                    if (getRequest(txt_glob_search.ID).Length > 2)
                    {
                        filtro += " and " + applicaAlias(Alias, getRequest(dd_glob_cadastro.ID)) + " like '%" + getRequest(txt_glob_search.ID) + "%' ";
                        MsgFiltro += "<b>" + label_valor_filtro_simples + "</b>: " + getRequest(txt_glob_search.ID) + "";
                    }
                    else
                    {

                        filtro += " and " + applicaAlias(Alias, getRequest(dd_glob_cadastro.ID)) + " = '" + getRequest(txt_glob_search.ID) + "' ";
                        MsgFiltro += "<b>" + label_valor_filtro_simples + "</b>: " + getRequest(txt_glob_search.ID) + "";

                    }
                }
                else
                {
                    filtro += " " + regra_simples.Replace("{0}", getRequest(txt_glob_search.ID, true));
                    MsgFiltro += "<b>" + label_valor_filtro_simples + "</b>: " + getRequest(txt_glob_search.ID) + "";
                }
            }
        }



        if (filtro != String.Empty)
            TemFiltro = true;

        string ordem = getRequest("g_glb_cmb_ordem");
        string ordem_tipo = getRequest("g_glb_cmb_ordenacao");

        if (ordem == String.Empty && valoresPadrao != null && valoresPadrao["order"] != null)
        {


            ordem = valoresPadrao["order"].Split(' ')[0];
            ordem_tipo = valoresPadrao["order"].Split(' ')[1];

            Control cmb_ordem = Utilities.Format.localizaControl("g_glb_cmb_ordem", div_filtro_avancado);
            Control cmb_ordenacao = Utilities.Format.localizaControl("g_glb_cmb_ordenacao", div_filtro_avancado);


              if ( cmb_ordem != null && cmb_ordenacao != null ){
                     ((DropDownList)cmb_ordem).SelectedValue = ordem;
                     ((DropDownList)cmb_ordenacao).SelectedValue = ordem_tipo;
                 }
            

        }

        if (ordem != String.Empty)
        {

            filtro += " order by " + Alias + ordem + " " +
                  ordem_tipo;
            if ( filtroAvancado ){
            Control cmb_ordem = Utilities.Format.localizaControl("g_glb_cmb_ordem", div_filtro_avancado);
            Control cmb_ordenacao = Utilities.Format.localizaControl("g_glb_cmb_ordenacao",div_filtro_avancado);

            if ( cmb_ordem != null && cmb_ordenacao != null ){
                MsgFiltro += " / <span class='ordenacao'>Ordenação</span> &gt;&gt; " +
                     ((DropDownList)cmb_ordem).SelectedItem.Text + " - " +
                     ((DropDownList)cmb_ordenacao).SelectedItem.Text;
                 }
            }
        }

        Session["_divExplicaFiltro"] = MsgFiltro;
        return filtro;
    }


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

    public string toMySqlDate(string data, int dia, string hora)
    {
        if (data == string.Empty)
            return string.Empty;

        DateTime dt = DateTime.Parse(data).AddDays(Convert.ToDouble(dia));

        return "'" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + hora + "'";
    }

    public DataView getCamposByExcelLibrary()
    {

       DataTable dt =   ExcelLibrary.DataSetHelper.CreateDataTable(this.CaminhoExcel, this.Tabela );


       DataView dw = new DataView(dt);
       dw.RowFilter = " visivel = 'True' ";
       dw.Sort = "tipo desc, ordem asc, label asc";


       return dw;
    }

  
    public DataView getCampos()
    {



        string str_driver = PageCadastroBase.getExcelDriver();

        if (str_driver == "")
            str_driver = "Microsoft Excel Driver (*.xls)";

        string conexao = "Driver={" + str_driver + "};DriverId=790;DBQ=" +
             this.CaminhoExcel + ";ReadOnly= true ; ";

        //conn.ConnectionString = conexao;


        ConnODBC OdbcConn = null;

        try
        {
            IDbPersist oTmp = FactoryConn.getConn("odbc", conexao, "odbc");
            OdbcConn = (ConnODBC)oTmp;
            //conn.Open();
        }
        catch (Exception exp)
        {
            Trace.Write("Erro ao tentar conectar o banco de dados de filtro via ODBC: " +
                 exp.Message + " - Arquivo em formato inválido! - String de Conexão:" + conexao);

            //Alert("Erro ao tentar conectar o banco de dados de filtro via ODBC: " +
               //  exp.Message + " - Arquivo em formato inválido! ");


            return null;
        }

        OdbcConnection conn = (OdbcConnection)OdbcConn.getConn();

        string[] restrictions = new string[4];
        restrictions[3] = "Table";
        
        DataTable userTables = conn.GetSchema("Tables");
        
        string my_tabela = this.Tabela;
        
        for (int i = 0; i < userTables.Rows.Count; i++)
        {
            string tab_col = userTables.Rows[i][2].ToString();

            if (userTables.Rows[i][3].ToString().IndexOf("TABLE") > -1 && tab_col == this.Tabela+"$" )
            {
                my_tabela = userTables.Rows[i][2].ToString();
                break;
            }

        }


        DataTable dt_campos =
                  ConnAccess.fetchData(OdbcConn,  "select * from [" +
                    my_tabela + "] where visivel = 'True' order by tipo desc, ordem asc, label asc ");
        //primaryKey desc, label,
        conn.Close();
        conn.Dispose();

        string add_filtro = string.Empty;

        if (dt_campos.Columns.Contains("filtro"))
            add_filtro += " and filtro = '1' ";

        DataView dw = dt_campos.DefaultView;

        return dw;

    }
    public override void carregaCamposFiltro2()
    {
      //  carregaCamposFiltro();
    }

    public override void loadFields()
    {

        if (!System.IO.File.Exists(CaminhoExcel))
            return;

        if (Tabela.Trim() == String.Empty)
            return;



        DataView dw = null;

        try
        {
            dw = getCampos();

        }
        catch { }
        if (dw == null)
        {
            dw = getCamposByExcelLibrary();

        }

        carregaCampos(dw);

        string add_filtro = string.Empty;

        if (dw.Table.Columns.Contains("filtro"))
            add_filtro += " and filtro = '1' ";

        if (dw.Table.Columns.Contains("lista"))
            add_filtro += " and lista is null  ";

        dw.RowFilter = " tipo <> 'System.DateTime' and tipo <> 'System.Decimal' and tipo <> 'System.Double' " + add_filtro;



        dd_glob_cadastro.DataSource = dw;
        dd_glob_cadastro.DataTextField = "label";
        dd_glob_cadastro.DataValueField = "nome";
        dd_glob_cadastro.DataBind();

        //dw.RowFilter = " tipo = 'System.DateTime' " + add_filtro;

        //ddl_campo_periodo.DataSource = dw;
        //ddl_campo_periodo.DataTextField = "label";
        //ddl_campo_periodo.DataValueField = "nome";
        //ddl_campo_periodo.DataBind();

        //tdFiltroData.Visible = !dw.Count.Equals(0);

        garantePostBack();
      

    }

    public override void carregaCamposFiltro()
    {
        if (!System.IO.File.Exists(CaminhoExcel))
            return;
        
        if (Tabela.Trim() == String.Empty)
            return;

        DataView dw = null;
        try
        {
                   dw = getCampos();

        }
        catch { }

            if (dw == null)
                dw = getCamposByExcelLibrary();

            carregaCampos(dw);

            string add_filtro = string.Empty;

            if (dw.Table.Columns.Contains("filtro"))
                add_filtro += " and filtro = '1' ";

            if (dw.Table.Columns.Contains("lista"))
                add_filtro += " and ( lista is null or lista = '' )   ";

            dw.RowFilter = " tipo <> 'System.DateTime' and tipo <> 'System.Decimal' and tipo <> 'System.Double' " + add_filtro;



            dd_glob_cadastro.DataSource = dw;
            dd_glob_cadastro.DataTextField = "label";
            dd_glob_cadastro.DataValueField = "nome";
            dd_glob_cadastro.DataBind();

            //dw.RowFilter = " tipo = 'System.DateTime' " + add_filtro;

            //ddl_campo_periodo.DataSource = dw;
            //ddl_campo_periodo.DataTextField = "label";
            //ddl_campo_periodo.DataValueField = "nome";
            //ddl_campo_periodo.DataBind();

            //tdFiltroData.Visible = !dw.Count.Equals(0);

            garantePostBack();
      

        
        
    }

    /// <summary>
    /// Garante que os dados estejam lá, mesmo após o postback.
    /// </summary>
    public override void garantePostBack()
    {
        preecheControles(this);

        if (getRequest(dd_glob_cadastro.ID) != String.Empty)
            setValor(dd_glob_cadastro, getRequest(dd_glob_cadastro.ID));


        if (getRequest(txt_glob_search.ID) != String.Empty)
            setValor(txt_glob_search, getRequest(txt_glob_search.ID));

        if (ControlesPreenchidos(div_filtro_avancado) && g_glob_tipo_filtro.SelectedValue == "A")
        {
            setaTipoFiltro();

            imgMostraFiltroAvancado.ImageUrl = "~/images/show_icon2.png";
        }
        else
        {
            //Se tiver preenchido nos avançados, então setamos para avançado..
            if (ControlesPreenchidos(div_filtro_avancado) && g_glob_tipo_filtro.SelectedValue == "R")
            {
                g_glob_tipo_filtro.Items[1].Selected = true;
                g_glob_tipo_filtro.SelectedValue = "A";
                setaTipoFiltro();
                imgMostraFiltroAvancado.ImageUrl = "~/images/show_icon2.png";
            }
        }

    }
    System.Collections.Specialized.NameValueCollection valoresPadrao;

    public override void setFiltroPadrao(System.Collections.Specialized.NameValueCollection valoresPadrao)
    {
        this.valoresPadrao = valoresPadrao;

    }
    private void carregaCampos(DataView dw)
    {
        if (dw == null)
            return;


        g_tbFiltro.Rows.Clear();
        
        for (int i = 0; i < dw.Count; i++)
        {
            HtmlTableRow tr = new HtmlTableRow();

            if ((i % 2) <=  0)
                tr.Attributes.Add("class", "tr_g_alt_filtro");

            HtmlTableCell td = new HtmlTableCell();


            td.Attributes.Add("class", "td_g_filtro");
            td.InnerHtml = dw[i]["label"].ToString();
            
            //célulca com os valores..

            HtmlTableCell td2 = new HtmlTableCell();
            td2.Attributes.Add("class", "td_g_filtro");

            string[] st_lista = new String[]{};

            if ( dw.Table.Columns.Contains("lista") && dw[i]["lista"] != DBNull.Value
                && dw[i]["lista"].ToString() != String.Empty ){
                st_lista = new String[] { dw[i]["lista"].ToString() };
            }

            insereCampoFiltro(td2, dw[i]["nome"].ToString(),
                 dw[i]["tipo"].ToString(), String.Empty, st_lista, dw[i]);

            tr.Cells.Add(td);
            tr.Cells.Add(td2);

            g_tbFiltro.Rows.Add(tr);
        }

        
        HtmlTableRow mtr = new HtmlTableRow();
        HtmlTableCell mtd = new HtmlTableCell("th");

        mtd.InnerHtml = "<label>Ordenar por</label>";


        DropDownList cmbOrdem = new DropDownList();
        cmbOrdem.ID = "g_glb_cmb_ordem";

        string sort_anterior = dw.Sort;

        dw.Sort = "primaryKey desc, label asc";

        carregaCombo(cmbOrdem, dw, "nome", "label", String.Empty);

        dw.Sort = sort_anterior;

        HtmlTableCell mtd2 = new HtmlTableCell("th");
        mtd2.Controls.Add(cmbOrdem);


        DropDownList cmbOrdenacao = new DropDownList();
        cmbOrdenacao.ID = "g_glb_cmb_ordenacao";
        System.Collections.Generic.IList<Entities.SimplesCodigoNome> lst = new System.Collections.Generic.List<Entities.SimplesCodigoNome>();

        lst.Add(new Entities.SimplesCodigoNome("ASC", "Ascendente"));
        lst.Add(new Entities.SimplesCodigoNome("DESC", "Descendente"));

        carregaCombo(cmbOrdenacao, lst, "Codigo", "Nome", String.Empty);
        mtd2.Controls.Add(cmbOrdenacao);


        mtr.Cells.Add(mtd);
        mtr.Cells.Add(mtd2);

        g_tbFiltro.Rows.Add(mtr);

        Control cr_bt =  Utilities.Format.localizaControl("btPesquisar", this.Page.Form);

        if (cr_bt != null)
        {
            if (NoClearAction == String.Empty)
            {
                this.bt_glob_search.OnClientClick = "document.forms[0].action='" + Request.ServerVariables["URL"].ToString() + "'; " + this.Page.GetPostBackEventReference(cr_bt) + "; return false";
            }
            else
            {

                this.bt_glob_search.OnClientClick = this.Page.GetPostBackEventReference(cr_bt) + "; return false";
            }
        }
        else
        {
            this.bt_glob_search.OnClientClick = "document.forms[0].action = 'listar" + PageCadastroBase.SplitCamelCase(this.Tabela) + ".aspx';document.forms[0].__VIEWSTATE.value=''; document.forms[0].submit(); return false";
            
        }

        Control cr_bt2 = Utilities.Format.localizaControl("btExportar", this.Page.Form);

        if (cr_bt2 != null)
        {
            this.imgExportaExcel.OnClientClick = this.Page.GetPostBackEventReference(cr_bt2) + "; return false";

            this.imgExportaExcel.Visible = true;
        }
        else
        {
            this.imgExportaExcel.Visible = false;
        }


        //getRequest(Button1.ID)
        if (getRequest("__EVENTTARGET") != String.Empty && getRequest("__EVENTTARGET").IndexOf(Button1.ID) > -1 && 
             getRequest("__EVENTTARGET").IndexOf(  this.ID) > -1)
        {
            Utilities.JavaScript.ExecuteScript(this.Page, "uc_filtro_avancado", "document.getElementById('" +
                   div_filtro_avancado.ClientID + "').style.display='block';", true);
        }

    }

    private void insereCampoFiltro(HtmlTableCell td,
         string nome, string tipo, string regra, string[] parametros, DataRowView drv)
    {

        if (parametros.Length > 0 && parametros[0] != String.Empty)
        {
            DropDownList cmb = new DropDownList();

            cmb.ID = "g_txt_filtro_" + nome;
            string lista = parametros[0];


            if (lista.IndexOf("lista:") > -1)
            {

                string[] qu0 = lista.Split(':');
                string[] qu = qu0[1].Split('|');

                System.Collections.Generic.IList<Entities.SimplesCodigoNome> ls_combo = new
                    System.Collections.Generic.List<Entities.SimplesCodigoNome>();

                for (int i = 0; i < qu.Length; i++)
                {
                    string item = qu[i];

                    string[]  it = item.Split(',');

                    ls_combo.Add(new Entities.SimplesCodigoNome(it[0], it[1]));
                }
                carregaCombo(cmb, ls_combo, "Codigo", "Nome", String.Empty, true);

            }

            if (lista.IndexOf("querie:") > -1)
            {
                string[] qu = lista.Split(':');

                //----------- Filtro por dependência ------------------
                    
                    if ( drv.DataView.Table.Columns.Contains("dependencia")){

                        if (drv["dependencia"].ToString() != String.Empty)
                        {
                            string[] ar_ttmp = drv["dependencia"].ToString().Split('$');

                            for (int sy = 0; sy < ar_ttmp.Length; sy++)
                            {
                                if (ar_ttmp[sy].Trim() == String.Empty)
                                    continue;

                                string[] lipmid = ar_ttmp[sy].Split(new string[] { "->" }, System.StringSplitOptions.None);

                                if (getRequest("g_txt_filtro_"+lipmid[0]) != "")
                                {
                                    qu[1] = qu[1].Replace("($)", lipmid[1].Replace("{0}", getRequest("g_txt_filtro_" + lipmid[0]))); 

                                }

                            }

                        }
                        //Control cr_btPesq = Utilities.Format.localizaControl("btPesquisar", this.Page.Form);
                        //&& cr_btPesq != null && cr_btPesq is Button
                        for (int sy = 0; sy < drv.DataView.Count; sy++)
                        {
                            if (drv.DataView[sy]["dependencia"] != DBNull.Value &&
                                drv.DataView[sy]["dependencia"].ToString().IndexOf(nome + "->") > -1 )
                            {

                                cmb.Attributes.Remove("onchange");
                                cmb.Attributes.Add("onchange", this.Page.GetPostBackClientEvent(Button1, String.Empty));
                            }

                        }
                    }
                     qu[1] = qu[1].Replace("($)","");
                    // -------------------------------------------

                try
                {
                    IDbPersist oConn = ConnAccess.getConn();

                    DataTable dtaB = ConnAccess.fetchData(oConn,  qu[1]);

                    carregaCombo(cmb, dtaB, dtaB.Columns[0].ColumnName, dtaB.Columns[1].ColumnName,
                        String.Empty, true);

                    dtaB.Dispose();
                }
                catch (Exception exp)
                {
                    //problema ao montar o componente!..

                }

            }

            td.Controls.Add(cmb);
            return;
        }

        if (tipo == "System.Double" || tipo == "System.Decimal")
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = " de ";

            td.Controls.Add(span);

            TextBox txt = new TextBox();
            txt.ID = "g_txt_filtro_" + nome;
            txt.Width = Unit.Parse("70px");
            txt.CssClass = "c_txt_filtro";
            txt.MaxLength = 18;
            setSoDecimal(txt);
            td.Controls.Add(txt);

            HtmlGenericControl span2 = new HtmlGenericControl("span");
            span2.InnerHtml = " até ";
            td.Controls.Add(span2);


            TextBox txt2 = new TextBox();
            txt2.ID = "g_txt_filtro_" + nome + "2";
            txt2.Width = Unit.Parse("70px");
            txt2.CssClass = "c_txt_filtro";
            txt2.MaxLength = 18;
            setSoDecimal(txt2);
            td.Controls.Add(txt2);


            return;
        } else if (tipo == "System.DateTime" )
        {

            System.Web.UI.UserControl uc =
            (System.Web.UI.UserControl)this.Page.LoadControl("~/controles/UcFaseDatas.ascx");

            td.Controls.Add(uc);
            ((IField)uc).Auxiliar = "N";
            uc.ID = "g_txt_filtro_" + nome;

            if (uc is IFieldDataHora)
            {
                ((IFieldDataHora)uc).mostraHora = true;
            }

            return;
        }
        else
        {
            TextBox txt = new TextBox();
            txt.ID = "g_txt_filtro_" + nome;
            txt.Width = Unit.Parse("150px");
            txt.CssClass = "c_txt_filtro";

            txt.MaxLength = 250;
            if (tipo == "System.Int32" || tipo == "System.Int16" || tipo == "System.Int64" || tipo == "System.Byte")
            {
                txt.MaxLength = 20;
                setSoNumero(txt);
            }
            else
            {
                txt.Width = Unit.Parse( "95%" );
            }
            

            td.Controls.Add(txt);

        }

    }
    /// <summary>
    /// Esconde o componente com os filtros.. ImageClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        string js = " document.getElementById('" +
              div_filtro_avancado.ClientID + "').style.display='none'; ";

        string img = "show_icon.png";
        try
        {
            if (ControlesPreenchidos(div_filtro_avancado))
            {
                img = "show_icon2.png";

                g_glob_tipo_filtro.SelectedValue = "A";
            }
            else
            {
                g_glob_tipo_filtro.SelectedValue = "R";
            }

            setaTipoFiltro();
        }
        catch {

            g_glob_tipo_filtro.SelectedValue = "R";
        }

        Utilities.JavaScript.ExecuteScript(this.Page, js, true);

        imgMostraFiltroAvancado.ImageUrl = "~/images/" + img;
    }

     /// <summary>
    /// Limpa o filtro avançado..     ImageClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton2_Click(object sender, EventArgs e)
    {

        ClearControl(div_filtro_avancado);
       
    }


    #region "parâmetros extras"

    private void ClearControl(Control controlP)
    {

        foreach (Control ctl in controlP.Controls)
        {
            if (ctl == null)
                continue;

            if (ctl is TextBox)
            {
                if (((TextBox)ctl).Visible == true)
                {
                    ((TextBox)ctl).Text = string.Empty;
                }
            }
            if (ctl is DropDownList)
            {
                if (((DropDownList)ctl).Visible == true)
                {
                    if (((DropDownList)ctl).SelectedIndex > 0)
                    {
                        ((DropDownList)ctl).SelectedIndex = 0;
                    }
                }
            }
            if (ctl is CheckBox)
            {
                if (((CheckBox)ctl).Visible == true)
                {
                    ((CheckBox)ctl).Checked = false;
                }
            }
            if (ctl is HiddenField)
            {

                ((HiddenField)ctl).Value = "";

            }
            if (ctl is IField)
            {
                ((IField)ctl).Value = String.Empty;
            }
            if (ctl.Controls.Count > 0)
            {
                ClearControl(ctl);
            }
        }
    }


    private void preecheControles(Control controlP)
    {

        foreach (Control ctl in controlP.Controls)
        {
            //if (ctl.ID == "g_glob_tipo_filtro")
            //    continue;

            if (ctl is TextBox || ctl is DropDownList
                || ctl is CheckBox || ctl is HiddenField || ctl is RadioButtonList)
            {
                string str = getRequest(ctl.ID);

                if (str.Trim() != String.Empty)
                    setValor(ctl, str);
            }

          
            if (ctl is CheckBox)
            {
                //string str = getValoresCheckList(((CheckBoxList)ctl), ",");

                //if (str.Trim() != String.Empty)
                //    return true;

            }
            if (ctl.Controls.Count > 0)
            {
                preecheControles(ctl);
                    
            }
        }

    }

    private bool ControlesPreenchidos(Control controlP)
    {

        foreach (Control ctl in controlP.Controls)
        {
            if (ctl.ID == "g_glb_cmb_ordem" || ctl.ID == "g_glb_cmb_ordenacao")
                continue;

            if (ctl is TextBox || ctl is DropDownList
                || ctl is CheckBox )
            {
                string str = getValor(ctl);

                if (str.Trim() != String.Empty)
                    return true;
            }

            if (ctl is IField)
            {
                //((IField)ctl).Value = String.Empty;
                string str = ((IField)ctl).Value;

                if (str.Trim() != String.Empty)
                    return true;

            }
            if (ctl is CheckBoxList)
            {
                string str = getValoresCheckList(((CheckBoxList)ctl),",");

                if (str.Trim() != String.Empty)
                    return true;

            }
            if (ctl.Controls.Count > 0)
            {
                if (ControlesPreenchidos(ctl))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Traz os valores selecionados em um CheckBoxList
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="sep"></param>
    /// <returns></returns>
    private string getValoresCheckList(CheckBoxList lst, string sep)
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




    #endregion
    protected void g_glob_tipo_filtro_SelectedIndexChanged(object sender, EventArgs e)
    {
        g_glob_tipo_filtro.SelectedValue = getRequest(g_glob_tipo_filtro.ID);

        if (g_glob_tipo_filtro.SelectedValue == "A")
        {
            setaExibicaoFiltroAvancado("block");

        }
        if (g_glob_tipo_filtro.SelectedValue == "R")
        {
            ClearControl(this.div_filtro_avancado);
            ImageButton1_Click(ImageButton1, null);
            //setaExibicaoFiltroAvancado("none");
        }

        setaTipoFiltro();
    }
    protected void setaTipoFiltro()
    {

        if (g_glob_tipo_filtro.SelectedValue == "A")
        {
            dd_glob_cadastro.Enabled = false;
            txt_glob_search.Text = String.Empty;
            txt_glob_search.Enabled = false;
        }
        else
        {

            dd_glob_cadastro.Enabled = true;
            txt_glob_search.Enabled = true;
        }

    }

    protected void setaExibicaoFiltroAvancado(string disp)
    {
        string js = " document.getElementById('" +
              div_filtro_avancado.ClientID + "').style.display='" + disp + "'; ";

        Utilities.JavaScript.ExecuteScript(this.Page, js, true);
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        //garantePostBack();

        //Utilities.JavaScript.ExecuteScript(this.Page,"uc_filtro_avancado", "document.getElementById('" +
        //       div_filtro_avancado.ClientID + "').style.display='block'; return false;", true);

    }
}
