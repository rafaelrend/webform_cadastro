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

public partial class cadContato : PageCadastroBase
{


    /// <summary>
    /// Executa page init, seta máscaras ou outras coisas do ajax
    /// Execute page init, with input maks or ajax things
    /// </summary>
    /// <returns></returns>
    protected void Page_Init(object sender, EventArgs e)
    {
	        //Escconde PKS
             tr_id.Visible = false;


             Control sm_scr = Utilities.Format.localizaControl("sm", this.Form);

             if (sm_scr != null && sm_scr is ScriptManager)
             {

                // ((ScriptManager)sm_scr).EnablePartialRendering = false;
                 ///EnablePartialRendering = "false";

             }
	


			//Seta máscaras, se tiver..
            
 
    }





    /// <summary>
    /// Popula combos na tela, se tiver
    /// Populate lists
    /// </summary>
    /// <returns></returns>
    public void ini_carregaCombos()
    {
                
    }
    
    //Tabela utilizada neste cadastro - table used at this file.
    string G_Table = "contato";


    protected void Page_Load(object sender, EventArgs e)
    {
        
        string acao = request("acao");
        //Obtém o request, baseado nas chaves primárias.
        
         string id = request("id");
      


        if (!Page.IsPostBack)
        {

            Session["_dt_reg_anterior"] = null;
            ini_carregaCombos();

            if ( id != String.Empty && acao != String.Empty)
            {
                DataRow dr = ConnAccess.getRow(ConnAccess.getConn(), G_Table, 
                         " and id = "+id
                      );

                carregaForm(dr);
                if ( dr != null ){
               //registra o que temos na tela este momento..
                     Session["_dt_reg_anterior"] = base.getRegistroTela(this.Page.Form, dr.Table);
                }
            }

            base.DataCarregamentoDaTela = DateTime.Now;

        }

        botaoVoltar(G_Table);
        
        base.setaPermissao(G_Table,"cadastro");
        base.setaEstiloTable(tbCadastro);
    }



    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        //setaTitulo(txtIdTipo.SelectedItem.Text);

        botaoVoltar(G_Table);

    }

    /// <summary>
    /// Carrega valores do banco de dados na tela
    /// Load values from database to screen
    /// </summary>
    /// <returns></returns>
    public override void carregaForm(DataRow entidade)
    {
        if (entidade == null)
            return;

              tr_id.Visible = true; 
                  
			
             base.PkId = Convert.ToInt32(entidade["id"]);

              
         setValor(txtId, ConnAccess.DBNullToNull(entidade["id"]));
      
 
         setValor(txtNome, ConnAccess.DBNullToNull(entidade["nome"]));
      
 
         setValor(txtEmpresa, ConnAccess.DBNullToNull(entidade["empresa"]));
      
 
         setValor(txtTelefonePessoal, ConnAccess.DBNullToNull(entidade["telefone_pessoal"]));
      
 
         setValor(txtTelefoneComercial, ConnAccess.DBNullToNull(entidade["telefone_comercial"]));
      
 
         setValor(txtEmails, ConnAccess.DBNullToNull(entidade["emails"]));
      
 
         setValor(txtDataCadastro, ConnAccess.DBNullToNull(entidade["data_cadastro"]));


         UcListarContatoEmail1.PkId = base.PkId;
         UcListarContatoEmail1.carregaGrid();

    
    }

    /// <summary>
    /// Testa campos obrigatórios
    /// Test required fields
    /// </summary>
    /// <returns></returns>
    protected override bool validar()
    {

                 
      if (! this.campoPreenchido(this.txtNome, "Informe Nome!")){
      return false;
      } 

          
        return base.validar();
    }


    /// <summary>
    /// Obtém dados que estão nos campos da tela.
    /// Get field values, inside a datarow structure, ready to save
    /// </summary>
    /// <returns>DataRow contendo estrutura da tabela + dados da tela.</returns>
    public override DataRow obtemForm()
    {

        DataTable dtModelo = ConnAccess.fetchData(ConnAccess.getConn(), " select * from "+G_Table+" where 1= 0 ");

        DataRow dr = null;
    
        base.AcaoID = 1;
        if ( base.PkId > 0 ) {

            dr = ConnAccess.getRow(ConnAccess.getConn(), G_Table, "id", base.PkId.ToString());

            base.AcaoID = 2;
        }
        if (dr == null)
        {
            dr = dtModelo.NewRow();
        }
		
		         dr["nome"] = ConnAccess.NullToDBNull(  getValor(txtNome)  ); 
         dr["empresa"] = ConnAccess.NullToDBNull(  getValor(txtEmpresa)  ); 
         dr["telefone_pessoal"] = ConnAccess.NullToDBNull(  getValor(txtTelefonePessoal)  ); 
         dr["telefone_comercial"] = ConnAccess.NullToDBNull(  getValor(txtTelefoneComercial)  ); 
         dr["emails"] = ConnAccess.NullToDBNull(  getValor(txtEmails)  ); 
         dr["data_cadastro"] = ConnAccess.NullToDBNull(  getDate(txtDataCadastro)  ); 
        
        dr.Table.TableName = G_Table;

        try
        {
            dr.Table.PrimaryKey = new DataColumn[]{ 
                
            dr.Table.Columns["id"]
      
        };
        }
        catch { }


        //Garantindo ultima formatação para o nosso data row que será salvo..
        base.formataRowAntesSalvar(ref dr);

        return dr;

    }
    protected void btNovo_Click(object sender, EventArgs e)
    {
        limpaForm();
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {

        if (!validar())
        {
            return;
        }


        DataRow dr = obtemForm();
        salvar(dr, false, String.Empty);

        base.PkId = Convert.ToInt32(dr["id"]);

        UcListarContatoEmail1.PkId = base.PkId;
        UcListarContatoEmail1.salvarGrid();


        dr["emails"] = base.getValorDataTable(UcListarContatoEmail1.gDt, "email", ", ");

        ConnAccess.Update(ConnAccess.getConn(), dr, "id");

        Session["st_Mensagem"] = "Contato salvo com sucesso!";

        //Garantindo o log de alteração..
        base.DataSalvamentoDaTela = DateTime.Now;
        
        DataTable regLogAtual = base.getRegistroTela(this.Page.Form, dr.Table);
        regLogAtual.TableName = dr.Table.TableName;

       //----------------- Salvamos o log aqui..   

      //  base.registraLog("Contato", base.DataCarregamentoDaTela, base.DataSalvamentoDaTela,
        //     Session["_dt_reg_anterior"], regLogAtual, base.AcaoID, dr["id"] );
       
        //Recarregamos esta tela para poder limpar o cache do navegador e do asp.net 
        Response.Redirect(Request.ServerVariables["URL"].ToString()+"?id="+dr["id"].ToString()+"&acao=LOAD");
        
        //carregaForm(dr);


    }
    protected void btExcluir_Click(object sender, EventArgs e)
    {
        if (base.PkId.Equals(0))
        {
            Alert("Selecione um registro antes de excluir!");
            return;
        }

        base.AcaoID = 3;

        DataRow dr = obtemForm();

        excluir(dr, false, String.Empty);
        
        //Salvando o log.
        base.DataSalvamentoDaTela = DateTime.Now;
        DataTable regLogAtual = base.getRegistroTela(this.Page.Form, dr.Table);
        regLogAtual.TableName = dr.Table.TableName;

        //base.registraLog("Contato", base.DataCarregamentoDaTela, base.DataSalvamentoDaTela,
        //    null, regLogAtual, base.AcaoID, dr["id"] );
        //------------------

        Session["st_Mensagem"] = " Contato exclu&iacute;do com sucesso!";
       // limpaForm();
	   
        Response.Redirect(Request.ServerVariables["URL"].ToString());



        if (base.HouveErro)
            return;
    }


    protected override void limpaForm()
    {
        Utilities.Format.ClearControl(tbCadastro);
        base.PkId = 0;
        tr_id.Visible = false;
        
         btSalvar.Enabled = true;

         btExcluir.Enabled = true;
   
         Session["_dt_reg_anterior"] = null;
         
    }
   
}
