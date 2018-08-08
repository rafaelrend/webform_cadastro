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
using DataAccess;

public partial class UcListarContatoEmail : UserControlCadastroBase
{
    protected void Page_Load(object sender, EventArgs e)
    {



        if (!Page.IsPostBack)
        {



            try
            {
                if (request("page") != String.Empty)
                    gvwDados.PageIndex = Convert.ToInt32(request("page"));
            }
            catch { }
            carregaGrid();



        }

       

    }
	
	  public bool Consulta
    {
        set
        {
            ViewState["_consulta"] = value;
        }
        get
        {
            if (ViewState["_consulta"] == null)
                ViewState["_consulta"] = false;

            return Convert.ToBoolean(ViewState["_consulta"]);
        }

    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    public DataTable gDt
    {
        get
        {
 //HttpContext.Current.Session

            if (ViewState[base.sPkId.ToString() +"_gDt"] == null)
            {

                return null;
            }
            else
            {

                return (DataTable)ViewState[base.sPkId.ToString() +"_gDt"];
            }


        }
        set
        {

            ViewState[base.sPkId.ToString() +"_gDt"] = value;
        }

    }

    string G_Table ="contato_email";

    public DataTable getDataTable()
    {

        string sql = " select * from  "+ G_Table + " where id_contato = " + base.PkId.ToString() + " order by id ";
        DataTable ds = ConnAccess.fetchData(ConnAccess.getConn(), sql);

        return ds;
    }

    public override void carregaGrid()
    {

        this.gDt = getDataTable();

        gvwDados.DataSource = this.gDt;
        gvwDados.DataBind();
		
        if (!this.Consulta)
        {

            this.ImageButton1.Visible = true;
            gvwDados.Columns[gvwDados.Columns.Count - 1].Visible = true;
        }
        else
        {

            this.ImageButton1.Visible = false;
            gvwDados.Columns[gvwDados.Columns.Count - 1].Visible = false;

        }
		

    }

    protected void btPesquisar_Click(object sender, EventArgs e)
    {
        carregaGrid();
    }
    protected void gvwDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwDados.PageIndex = e.NewPageIndex;
        carregaGrid();
    }
    /// <summary>
    /// Esta função irá excluir os dados..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvwDados_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex < 0)
            return;

         atualizaTabela( this.gDt);

        string cod = gvwDados.Rows[e.NewSelectedIndex].Cells[0].Text;

        if (cod == String.Empty || cod == "&nbsp;")
        {
            try
            {
                this.gDt.Rows.RemoveAt(e.NewSelectedIndex);
            }
            catch { }
                gvwDados.DataSource = this.gDt;
                gvwDados.DataBind();
           
            this.my_dvMensagem.InnerHtml = "Exclus&atilde;o realizada com sucesso!";
            return;

        }

        

        this.gDt.Rows.RemoveAt(e.NewSelectedIndex);
         
        ConnAccess.executeCommand(ConnAccess.getConn(), " delete from " + G_Table +" where id = " + cod);

        gvwDados.DataSource = this.gDt;
        gvwDados.DataBind();


        //Utilities.JavaScript.ExecuteScript(this.Page, "calculaTotal(null,'')", true);

        this.my_dvMensagem.InnerHtml = "Exclus&atilde;o realizada com sucesso!";

    }

    protected void carregarow_init(GridViewRow grw)
    {
	
	    
         base.setSoNumero( Utilities.Format.localizaControl("txtOrdemCadastro", grw) );   
       
		     
    }

    protected void gvwDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0)
            return;

        GridViewRow grw = e.Row;
        carregarow_init(grw);

        DataRow entidade = this.gDt.Rows[e.Row.RowIndex];

          
          //id
          setValor(Utilities.Format.localizaControl("txtId", grw) , ConnAccess.DBNullToNull(entidade["id"]));  
          setValor(Utilities.Format.localizaControl("lb_txtId", grw) , ConnAccess.DBNullToNull(entidade["id"]));  
      
      
 
          //email
          setValor(Utilities.Format.localizaControl("txtEmail", grw) , ConnAccess.DBNullToNull(entidade["email"]));  
          setValor(Utilities.Format.localizaControl("lb_txtEmail", grw) , ConnAccess.DBNullToNull(entidade["email"]));  
      
      
		
		 

        
    }

    public void salvarGrid()
    {
        if (gDt == null)
        {
            Alert("Sessão expirada!");
            return;
        }

        


        gDt.TableName = G_Table;
		
        atualizaTabela( this.gDt);

        try
        {
            gDt.PrimaryKey = new DataColumn[]{ 
                
            gDt.Columns["id"]
      
        };
        }
        catch { }

        for (int i = 0; i < gDt.Rows.Count; i++)
        {
            DataRow dr = gDt.Rows[i];

            GridViewRow grw = gvwDados.Rows[i];


            //if (!this.campoPreenchido(Utilities.Format.localizaControl("txtEmail", grw), "Informe Email!"))
            //{
            //    return false;
            //} 
	
            int pk = Convert.ToInt32(Nvl(dr["id"], 0));

            int max =ConnAccess.getMax( ConnAccess.getConn(),  "id",G_Table ,"") ;

            if (pk <= 0 )
            {
                ConnAccess.Insert(ConnAccess.getConn(), dr,"id", true);
                dr["id"] = ConnAccess.getMax(ConnAccess.getConn(), "id", G_Table, "");
            }
            else
            {
                ConnAccess.Update(ConnAccess.getConn(), dr, "id" );
            }
        }
       this.my_dvMensagem.InnerHtml = "Dados salvos com sucesso!";
        //Alert("Dados salvos com sucesso!");
    }

    protected void btNovo_Click(object sender, EventArgs e)
    {

    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        salvarGrid();
        carregaGrid();
    }
	
	 public void atualizaTabela( DataTable gDt)
    {
        for (int i = 0; i < gDt.Rows.Count; i++)
        {
            DataRow dr = gDt.Rows[i];

            GridViewRow grw = gvwDados.Rows[i];
			
			if (grw.Cells[0].Text != String.Empty && grw.Cells[0].Text != "&nbsp;")
            {
                dr["id"] = Convert.ToInt32(grw.Cells[0].Text);
            }

            dr["id_contato"] = base.PkId; // ConnAccess.NullToDBNull(getValor(Utilities.Format.localizaControl("txtIdContato", grw))); 
             dr["email"] = ConnAccess.NullToDBNull(  getValor(Utilities.Format.localizaControl("txtEmail", grw))  );
             dr["ordem_cadastro"] = i;                // ConnAccess.NullToDBNull(  getValor(Utilities.Format.localizaControl("txtOrdemCadastro", grw))  ); 
			
            setaEstilo(grw);

		 }
	}	 	
	
	    public void setaEstilo(GridViewRow row)
    {
        for (int i = 0; i < row.Cells.Count; i++)
        {

            row.Cells[i].Attributes.Remove("style");
            row.Cells[i].Attributes.Add("style", "text-align:center");
        }


    }
	
	
    /// <summary>
    /// Adiciona uma nova linha..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (this.gDt == null)
        {
            Alert("Sessao expirou!");
            return;

        }

       atualizaTabela( this.gDt);

        DataTable dtt = this.gDt;

        dtt.Rows.Add(dtt.NewRow());


        this.gDt = dtt;
        gvwDados.DataSource = this.gDt;
        gvwDados.DataBind();
    }
}
