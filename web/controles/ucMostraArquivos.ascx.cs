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
using System.IO;

public partial class controles_ucMostraArquivos : UserControlCadastroBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (request("hd_uc_acao") == "excluir")
        {
            string arquivo = request("hd_uc_arquivo");

            string arquivocompleto = Server.MapPath(this.Pasta) + "\\" +
                     arquivo;

            if (File.Exists(arquivocompleto))
            {
                File.Delete(arquivocompleto);

                if (this.hd_uc_tabela.Value != String.Empty && this.hd_ic_id_origem.Value != String.Empty)
                {
                    //string sq_delete = " delete from arquivos where id_origem = " + this.hd_ic_id_origem.Value +
                      //   " and tabela='" + this.hd_uc_tabela.Value + "' and nome='" + arquivo + "' ";

                    /*
                    Entities.Log.registraExclusao("arquivos", " id_origem = " + this.hd_ic_id_origem.Value +
                         " and tabela='" + this.hd_uc_tabela.Value + "' and nome='" + arquivo + "' ",
                         SessionFacade.Id, SessionFacade.Nome);

                    */
                    //ConnAccess.Execute(sq_delete);


                }


                Response.Write("<script>alert('Arquivo " + arquivo + " excluído com sucesso!');</script>");
            }

        }


        carregaArquivos();
    }

    public void setaDadosParaPersistir(string tabela, string id_origem)
    {
        this.hd_uc_tabela.Value = tabela;
        this.hd_ic_id_origem.Value = id_origem;

    }

    public string Pasta
    {
        set
        {
            hd_pasta.Value = value;
        }
        get
        {
            return hd_pasta.Value;
        }
    }

    public void carregaArquivos()
    {

        string pasta = "";
        string[] arquivos = new string[] { };
        try
        {

            pasta = Server.MapPath(this.Pasta);
            arquivos = Directory.GetFiles(pasta);

            System.Array.Sort(arquivos);
        }
        catch
        {
            Alert("Pasta " + this.Pasta + " incorreta!");
            return;
        }

        DataTable dt = this.transformaTextoEmDataTable(arquivos);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        dt.Dispose();
        sp_nome_pasta.InnerHtml = pasta;

    }

    public DataTable transformaTextoEmDataTable(string[] arquivos)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("nome", typeof(string)));
        dt.Columns.Add(new DataColumn("tamanho", typeof(string)));
        dt.Columns.Add(new DataColumn("extensao", typeof(string)));
        dt.Columns.Add(new DataColumn("url", typeof(string)));
        dt.Columns.Add(new DataColumn("caminho", typeof(string)));


        for (int i = 0; i < arquivos.Length; i++)
        {
            DataRow dr = dt.NewRow();
            string arquivo = arquivos[i];

            FileInfo fil = new FileInfo( arquivo);


            dr["nome"] = fil.Name;
            dr["extensao"] = fil.Extension;
            dr["url"] = "http://"+Request.ServerVariables["SERVER_NAME"] + SessionFacade.getApp("subpasta") +"/"+ this.Pasta + "/" + fil.Name;
            dr["caminho"] =  arquivo;
            dr["tamanho"] = fil.Length / 1024;

            dt.Rows.Add(dr);

            

        }

        return dt;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0)
            return;

        Control cr1 = e.Row.Cells[4].Controls[0];
        Control cr2 = e.Row.Cells[5].Controls[0];

        HyperLink hpk = (HyperLink)cr1;
        HyperLink hpk2 = (HyperLink)cr2;

        hpk.NavigateUrl = "../"+this.Pasta + "/" + e.Row.Cells[0].Text;
        hpk.Attributes.Add("target", "_blank");

        hpk2.NavigateUrl = "javascript: excluir_uc_arquivo('" + e.Row.Cells[0].Text + "','" + this.Pasta + "')";
     

    }
}
