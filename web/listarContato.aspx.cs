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
using DataAccess;

public partial class listarContato : PageCadastroBase
{

    //Tela de lista dados para Contato
    string G_table = "contato";


    protected void Page_Init(object sender, EventArgs e)
    {


        Control sm_scr = Utilities.Format.localizaControl("sm", this.Form);

        if (sm_scr != null && sm_scr is ScriptManager)
        {

            ((ScriptManager)sm_scr).EnablePartialRendering = false;
            ///EnablePartialRendering = "false";

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        base.setaPermissao("contato","lista");



        if (!Page.IsPostBack || request("__VIEWSTATE").Length.Equals(0))
        {  


            try
            {
                if (request("page") != String.Empty)
                    gvwDados.PageIndex = Convert.ToInt32(request("page"));
            }
            catch { }
            carregaGrid();


            //setValor(txtDescricao, getRequest(this.txtDescricao.ID));

            btPesquisar.Visible = true;
            btPesquisar.Click += btPesquisar_Click;
        }
       
        

    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        guardaSessao(String.Empty,this.G_table);
        setaTitulo("Contato");
    }


        /// <summary>
        /// Método principal, executa consulta no sistema e popula GridView
        /// Main method, get querie result and bind GridView
        /// </summary>
        /// <returns></returns>
    public override void carregaGrid()
    {
        
        string filtro = " where 1 = 1 ";
        //string nomecoluna = "nome";

        //if (getRequest(txtDescricao.ID) != String.Empty)
        //{
        //    filtro += " and " + nomecoluna + " like '%" + getRequest(txtDescricao.ID) + "%'";
       // }


        IFiltro opFiltro = getFiltroPrincipal();
      
        filtro += opFiltro.getSqlFiltro();


        string sql = " select * from  "+ this.G_table +" "+ filtro;

        DataTable ds = ConnAccess.fetchData( ConnAccess.getConn(), sql);

        btExportar.Visible = (ds.Rows.Count > 0 );


        gvwDados.DataSource = ds;
        gvwDados.DataBind();
        //Processaremos alguma coisa, de acordo ao tipo de botão usado.  
        base.processaConsulta(this.obj_botao,ds,gvwDados);


        dv_qtde_registros.InnerHtml = " <b> Qtde de Registros: </b> &nbsp;" + ds.Rows.Count.ToString();


     }

    private object obj_botao;

    protected void btPesquisar_Click(object sender, EventArgs e)
    {
        this.obj_botao = sender;
        carregaGrid();
    }
    protected void gvwDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwDados.PageIndex = e.NewPageIndex;
        carregaGrid();
    }
    protected void gvwDados_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if ( e.NewSelectedIndex < 0 )
             return;


    }
    protected void gvwDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
   

}
