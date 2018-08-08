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

public partial class controles_ucSubMenu : UserControlCadastroBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void carregaMenu()
    {

        string filtro = string.Empty;

        //if (!SessionFacade.Admin)
        //{
        //    filtro += " and pagina in ('"+
        //         SessionFacade.getPropriedade("_paginas").Replace(",","','")+"') ";
        //}
        if (!SessionFacade.Admin)
        {
            
                filtro += " and id in (select id_processo from perfil_processos where id_perfil in ( " +
                      SessionFacade.listaProcessos + ") and acao is not null  ) ";
         

        }

        ////if (!SessionFacade.Admin)
        ////{
        ////    filtro += " and id in ( " +
        ////          SessionFacade.listaProcessos + ")  ";
        ////}

        IDbPersist oConn = ConnAccess.getConn();

        DataTable dt = ConnAccess.fetchData( oConn, " select * from menu where nivel not like 'MO%' " +
            " and id_item_pai is null " + 
             filtro + " order by  nivel asc ");


        string menu = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            DataTable dt_itens = ConnAccess.fetchData(oConn, " select * from menu where id_item_pai = " +
                   dr["id"].ToString() + filtro + " order by funcionalidade asc ");

            if (base.Nvl(dr["pagina"], "#").ToString() == "#"
                 && dt_itens.Rows.Count <= 0)
            {
                //É um menu, sem filhos, então não precisa aparecer...

            }
            else
            {
                menu += System.Environment.NewLine + "<li class=\"pureCssMenui0\"><a class=\"pureCssMenui0\" " +
                  " href=\"" + base.Nvl(dr["pagina"], "#").ToString() + "\"><span>" +
                  dr["funcionalidade"].ToString() +
                  "</span><![if gt IE 6]></a><![endif]><!--[if lte IE 6]><table><tr><td><![endif]-->";


            }

            if ( dt_itens.Rows.Count > 0 ){
			     menu += System.Environment.NewLine + 
                       "<ul class=\"pureCssMenum\">";
                        for (int z = 0; z < dt_itens.Rows.Count; z++)
			            {
                            menu += System.Environment.NewLine +  "<li class=\"pureCssMenui\"><a class=\"pureCssMenui\" href=\""+
                                dt_itens.Rows[z]["pagina"].ToString() + "\">" +
                                dt_itens.Rows[z]["funcionalidade"].ToString()+"</a></li>";
			            }
                menu += "</ul>";
            }

             menu += "<!--[if lte IE 6]></td></tr></table></a><![endif]--></li>";

        }
        ul_menu.InnerHtml = menu;
    }
}
