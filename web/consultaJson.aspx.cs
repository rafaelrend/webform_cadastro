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

public partial class consultaJson : PageCadastroBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string tabela = request("tabela");
        string campoid = request("campoid");
        string campodesc = request("campodesc");
        string campocod = request("campocod");
        string term = request("term").Replace("'", "");
        string comp = request("comp");


        if (tabela != String.Empty && term != String.Empty )
        {
            string sql = " select concat(" + campodesc + " , ' -COD:' ,  cast(" + campocod + " as char) , ' -ID:' , cast(" + campocod + " as char)) as descricao ";

            if ( campoid == String.Empty )
                sql = " select concat(" + campodesc + " , ' -COD:' , cast(" + campocod + " as char)) as descricao  ";

            sql += " from " + tabela + " where ( " + campodesc + " like '%" + term + "%' or cast(" + campocod + " as char) like '%" + term + "%' ) " + comp + " order by  " + campodesc;

            //Response.Write(sql); Response.End();
            DataTable dt = ConnAccess.fetchData(ConnAccess.getConn(),  sql );

            Response.Write(converteDataTableToJson(dt)); Response.End();
        }


    }



    protected string converteDataTableToJson(DataTable dt)
    {
        string str = "[";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i > 0)
                str += ",";

            str += System.Environment.NewLine +  "\"" + dt.Rows[i][0].ToString().Replace('"',' ' )+"\"";
            
        }

        str += "]";

        return str;
    }
}
