using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class GeneralMasterPage : BaseMasterPage
{
    #region "Atributos etc"
        
       
        
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
    }

    void Page_PreRender(object sender, EventArgs e)
    {
     
    }

    /// <summary>
    /// Seta Recurso Speech do HTML5 em campos do tipo input=text
    /// </summary>
    /// <param name="txt"></param>
    void setInputSpeech(TextBox txt)
    {
        if ( txt.TextMode == TextBoxMode.SingleLine ){

            txt.Attributes.Remove("x-webkit-speech");
            txt.Attributes.Add("x-webkit-speech","x-webkit-speech");
            txt.Attributes.Remove("lang");
            txt.Attributes.Add("lang", "pt-BR");
            //lang="es-MX"
        }
    }
    /// <summary>
    /// Faz uma varredura na página e adiciona o speech do HTML em todos os input=text
    /// </summary>
    /// <param name="form"></param>
    void setInputSpeech_control(Control controlP)
    {

        foreach (Control ctl in controlP.Controls)
        {
            if (ctl is TextBox)
            {
                if (((TextBox)ctl).Visible == true)
                {
                    TextBox tx = (TextBox)ctl;
                    setInputSpeech(tx);
                }
            }
          
            if (ctl.Controls.Count > 0)
            {
                setInputSpeech_control(ctl);
            }
        }
    }


    #region "Objetos gerais"


    /// <summary>
    /// Tentaremos liberar memória de sessão..
    /// </summary>
    private void limpaMemoria()
    {
        string url = Request.ServerVariables["URL"].ToString();

        if (url.IndexOf("mostraRelatorio") <= -1)
        {

            if (Session["_relatorio_data"] != null)
            {
                if (Session["_relatorio_data"] is DataSet)
                {
                    DataSet ds = (DataSet)Session["_relatorio_data"];
                    ds.Dispose();

                }
                if (Session["_relatorio_data"] is DataTable)
                {
                    DataTable ds = (DataTable)Session["_relatorio_data"];
                    ds.Dispose();

                }
            }
            Session.Remove("_relatorio_data");

        }
      

    }


    /// <summary>
    /// Obtem valor de uma coluna de um datatable
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    public string getValorDataTable(DataTable dt, string prop)
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

        return propriedade;

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionFacade.Id <= 0)
        {
            if (Request.Cookies["logado"] != null)
            {
                HttpCookie cookie = Request.Cookies["logado"];
                if (cookie.Value != String.Empty)
                {
                    //
                  //  string ids = SessionFacade.Id.ToString() + "||" + SessionFacade.Nome + "||" + SessionFacade.listaProcessos +
                    //      "||" + SessionFacade.listaModulos + "||" + SessionFacade.TextoChamada + "||" + SessionFacade.Email;
                    string[] ar = cookie.Value.Split(new string[] { "||" }, System.StringSplitOptions.None);

                    SessionFacade.Id = Convert.ToInt32(ar[0]);

                    SessionFacade.Login = String.Empty;
                    SessionFacade.Nome = ar[1];

                    try
                    {

                       // if (ar[2] != String.Empty)
                          //  SessionFacade.TipoId = Convert.ToInt32(ar[2]);

                        SessionFacade.listaProcessos = ar[2];
                        SessionFacade.listaModulos = ar[3];
                        SessionFacade.TextoChamada = ar[4];
                        //SessionFacade.Email = ar[5];
                    }
                    catch { }


                   
      
                }

            }
        }

        if (SessionFacade.Id > 0)
        {

            string filtroDestinatario = " and (  exists ( select ss.id_mensagem from mensagem_destino ss where ss.id_destinatario = " +
            SessionFacade.Id.ToString() + " and ss.id_mensagem = m.id and ifNull(ss.arquivada,0) = 0 ) or m.todos = 1  ) ";

            string sqlcont = " select count(*) from mensagem m where 1 = 1 " + filtroDestinatario + " and m.id not in ( select id_mensagem from mensagem_destino where id_destinatario =" +
            SessionFacade.Id.ToString() + " and data_lida is not null ) ";


            int qtdeMensagensNaoLidas = Convert.ToInt32(DataAccess.ConnAccess.fetchData( DataAccess.ConnAccess.getConn(), sqlcont ));

            a_msg.InnerHtml = "- Mensagens (" + qtdeMensagensNaoLidas.ToString() + ") ";

            if (qtdeMensagensNaoLidas > 0)
            {
                a_msg.Attributes.Remove("style");
                a_msg.Attributes.Add("style", "color: orange");
            }
            else
            {
                a_msg.Attributes.Remove("style");
                a_msg.Attributes.Add("style", "color: blue");

            }

            //a_msg

        }

        if (SessionFacade.getApp("NaoConfereAcesso") == "1")
        {


        }
        else
        {
            if (SessionFacade.Id <= 0)
            {
                //Response.Redirect("login.aspx");
            }
        }
       
      
        string urlAtual = Request.ServerVariables["URL"].ToString();


        SessionFacade.TelaAtual = urlAtual;

        

        Control txPesquisar = encontraControles(this.Page.Form, "txtPesquisar");
        Control imgPesq = encontraControles(this.Page.Form, "imgRefreshFiltro");

        if (txPesquisar != null && imgPesq != null)
        {
            //this.setEnter((TextBox)txPesquisar, (ImageButton)imgPesq);

        }
        //Informa em que módulo o sistema se encontra.
        string mod = "Entretenimento";

        if (Request.QueryString["modulo"] != null && Request.QueryString["modulo"].ToString() != String.Empty)
            SessionFacade.Modulo = Request.QueryString["modulo"].ToString();


        //setInputSpeech_control(this.form1);
      
       //carregaMenuSistema();

        lb_usuario.Text = SessionFacade.Nome;
        lb_perfil.Text = "(" + SessionFacade.TextoChamada + ") ";

        if (!Page.IsPostBack)
        {
          //  UcFiltroBasico1.CaminhoExcel =
            //    Server.MapPath("estrutura_banco.xls");
        }
       
    }
    #endregion

    private void carregaMenuSistema()
    {

        string filtro = string.Empty;

        //if (!SessionFacade.Admin && SessionFacade.getApp("NaoConfereAcesso") != "1" )
        //{
        //    filtro += " and pagina in ('" +
        //         SessionFacade.getPropriedade("_paginas").Replace(",", "','") + "') ";
        //}
        if (SessionFacade.listaProcessos == String.Empty)
            SessionFacade.listaProcessos = "0";

        if (!SessionFacade.Admin)
        {
            filtro += " and id in (select id_processo from perfil_processos where id_perfil in ( " +
                  SessionFacade.listaProcessos + ") and acao is not null  ) ";
        }
           



        UcSubMenu1.carregaMenu();

        DataTable dt_menus = DataAccess.ConnAccess.fetchData( DataAccess.ConnAccess.getConn(), " select * from menu where id_item_pai is null " +
             " and nivel like '%MO%' " + filtro + "  order by nivel ");

        string menu_itens = string.Empty;
        for (int i = 0; i < dt_menus.Rows.Count; i++)
        {
            menu_itens += "<li";
            if (Convert.ToString(i + 1) == SessionFacade.Modulo)
            {
                menu_itens += " class='current' ";
            }
            string url = "#";
            if ( dt_menus.Rows[i]["pagina"] != DBNull.Value && dt_menus.Rows[i]["pagina"].ToString() != String.Empty ){
                url = dt_menus.Rows[i]["pagina"].ToString() + "?modulo=" + Convert.ToString(i + 1);
            }

            string continua = "";

            if (url.IndexOf("calendario.aspx") > -1)
            {
                continua = " target='_blank' ";
            }

            menu_itens += "><a " + continua + " href=\"" + url + "\">" +
                 dt_menus.Rows[i]["funcionalidade"].ToString() + "</a></li>"; 
        }
        current.InnerHtml = menu_itens;
    }

    private string urlDestino()
    {
        string urlDestino = Request.ServerVariables["URL"].ToString();
        if (Request.QueryString["tipo"] != null)
            urlDestino += "?tipo=" + Request.QueryString["tipo"].ToString();

        if (urlDestino.IndexOf("cadSgoFuncao") > -1)
        {
            urlDestino = Request.ServerVariables["URL"].ToString();

            string str = string.Empty;
            string tt = "?";
            for (int i = 0; i < Request.QueryString.Count; i++)
            {
                if (i > 0) { tt = "&"; }

                str += tt + Request.QueryString.Keys[i] + "=" + Request.QueryString[i];
            }

            return urlDestino + str;
        }

        return urlDestino;
    }




}
