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

/// <summary>
/// Enable select multiple values using 2 ListBoxes on Form.
/// </summary>
public partial class controles_ucMyDoubleListBox : UserControlCadastroBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.listOptions1.Attributes.Remove("ondblclick");
        this.listOptions1.Attributes.Add("ondblclick", Page.GetPostBackClientEvent(bt_right1,"direita"));


        this.listOptions2.Attributes.Remove("ondblclick");
        this.listOptions2.Attributes.Add("ondblclick", Page.GetPostBackClientEvent(bt_left1,"esquerda"));
    }

    private bool permiteDuplicar = false;
    public bool PermiteDuplicar
    {
        set
        {
            this.permiteDuplicar = value;
        }
        get
        {
            return this.permiteDuplicar;
        }

    }

    /// <summary>
    /// Return all values selected separated by comma (,)
    /// </summary>
    /// <returns></returns>
    public string getValuesSelecteds()
    {
        //On postback, listbox lost all values if was not modified by asp.net. 
        //Therefore we use hiddenfields, because that keep changed values out of asp.net
        return hd_values.Value;
    }

    /// <summary>
    /// Return all text selected separated by comma (,)
    /// </summary>
    /// <returns></returns>
    public string getTextsSelecteds()
    {
        //On postback, listbox lost all values if was not modified by asp.net. 
        //Therefore we use hiddenfields, because that keep changed values out of asp.net
        return hd_text.Value;
    }

    /// <summary>
    /// Set usercontrol width
    /// </summary>
    public string Width
    {
        set
        {
            tb_general.Attributes.Remove("style");
            tb_general.Attributes.Add("style", "width:" + value);
        }

    }

    /// <summary>
    /// Set prefer css class to all buttons.
    /// </summary>
    public string ButtonCssClass
    {
        set
        {
            bt_left1.Attributes.Remove("class");
            bt_left1.Attributes.Add("class", value);


            bt_left2.Attributes.Remove("class");
            bt_left2.Attributes.Add("class", value);


            bt_right1.Attributes.Remove("class");
            bt_right1.Attributes.Add("class", value);


            bt_right2.Attributes.Remove("class");
            bt_right2.Attributes.Add("class", value);

        }
    }


    /// <summary>
    /// Get and Set listbox's height
    /// </summary>
    public string Height
    {
        set
        {
            listOptions1.Height = Unit.Parse(value);
            listOptions2.Height = Unit.Parse(value);
        }
        get
        {
            return listOptions1.Height.Value.ToString();
        }
    }

    public event EventHandler selectedItem;


    public void setMyViewState(string key, string value)
    {
        ViewState[key] = value;
    }

    public string getMyViewState(string key)
    {
        if (ViewState[key] == null)
            return String.Empty;

        return ViewState[key].ToString();

    }

    private string getJsEle(string id)
    {
        return "document.getElementById('" + id + "')";

    }
    public string getSelectedItens()
    {
        string saida = string.Empty;
        for (int i = 0; i < listOptions2.Items.Count; i++)
        {
            if (i.Equals(0))
                saida += listOptions2.Items[i].Value;
            else
                saida +=","+ listOptions2.Items[i].Value;

        }
        return saida;

    }

    /// <summary>
    /// Transfere um registro de um combo para o outro..
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="all"></param>
    protected void transferItem(ListBox from, ListBox to, bool all)
    {
        transferItem(from, to, all, true);
    }

    /// <summary>
    /// Transfere um registro de um combo para o outro..
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="all"></param>
    protected void transferItem(ListBox from, ListBox to, bool all, bool habilitarEvento)
    {
        ArrayList arr = new ArrayList();
        for (int i = 0; i < from.Items.Count; i++)
        {
            if (from.Items[i].Selected || all)
            {

                //Se posso duplicar, e meu listbox é o segundo, não preciso adicionar novamente no primeiro.
                if (permiteDuplicar && from.ID == listOptions2.ID)
                {
                }
                else
                {
                    //É uma situação comum, que não precisa duplicar.
                    to.Items.Add(new ListItem(from.Items[i].Text, from.Items[i].Value));
                    
                }
                arr.Add(i);

                ArrayList arr2 = new ArrayList();
                arr2.Add(from.Items[i].Value);
                arr2.Add(i.ToString()); //Index
                //O sentido..
                if (from.ID == listOptions1.ID)
                {
                    arr2.Add("add");
                }
                else
                {
                    arr2.Add("remove");
                }
                if (selectedItem != null && habilitarEvento)
                {                    
                    selectedItem(arr2, EventArgs.Empty);  
                }
            }
        }
        
        
       
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                if (permiteDuplicar && from.ID == listOptions1.ID) //Se posso duplicar, e meu listbox é o primeiro, não preciso remover meus itens.
                {
                    from.Items[Convert.ToInt32(arr[i])].Selected = false;
                }
                else
                {   //É uma situação comum ou eu sou o listbox 2, vou remover meu item pois já dei um item para o outro listbox..
                    from.Items.RemoveAt(Convert.ToInt32(arr[i]));
                }
            }
        

    }



    public void removerIds(string ids)
    {
        string[] arr = ids.Split(',');

        for (int i = 0; i < arr.Length; i++)
        {
            bool encontrou = false;
            string id = arr[i].ToString();

            if (id == String.Empty)
                continue;

            for (int y = 0; y < this.listOptions2.Items.Count; y++)
            {
                if (this.listOptions2.Items[y].Value.Equals(id))
                {
                    this.listOptions2.Items[y].Selected = true;
                    transferItem(this.listOptions2, this.listOptions1, false, false);
                    encontrou = true;
                }
                if (encontrou)
                {
                    break;
                }
            }

            for (int zz = 0; zz < VsLista.Count; zz++)
            {
                if (VsLista[zz].Equals(id))
                {
                    VsLista.RemoveAt(zz);
                    break;
                }
            }
        }

    }

    /// <summary>
    /// Ao adicionar ids, é transferido dados de um combo para o outro.
    /// </summary>
    /// <param name="ids"></param>
    public void adicionarIds(string ids, bool habilitarEvento)
    {
        string[] arr = ids.Split(',');
        
        for (int i = 0; i < arr.Length; i++)
        {
            bool encontrou = false;
            string id = arr[i].ToString();

            if (id == String.Empty)
                continue;

            for (int y = 0; y < this.listOptions1.Items.Count; y++)
            {
                if (this.listOptions1.Items[y].Value.Equals(id))
                {
                    this.listOptions1.Items[y].Selected = true;
                    transferItem(this.listOptions1, this.listOptions2, false, habilitarEvento);
                    encontrou = true;
                }
                if (encontrou)
                {
                    break;
                }
            }

            if (!encontrou)
            {
                VsLista.Add(id);
            }
        }

        if (VsLista.Count > 0)
        {

            string sql = this.getMyViewState("sqlSelect");

            string campo = " id ";

            if (sql.IndexOf("e.") > -1)
                campo = " e.id ";

            sql = sql.Replace("order", " and "+campo+" in ( " + strVsLista(",") + ") order ");

            DataTable dt_l = DataAccess.ConnAccess.fetchData(  DataAccess.ConnAccess.getConn(), sql);

            for (int i = 0; i < dt_l.Rows.Count; i++)
            {
                DataRow dr = dt_l.Rows[i];

                ListItem lsi = new ListItem(dr[this.getMyViewState("textfield")].ToString(),
                       dr[this.getMyViewState("valuefield")].ToString());

                this.listOptions2.Items.Add(lsi);
            }

        }
    }
    public void adicionarIds(string ids)
    {
        adicionarIds(ids, true);
    }

    protected void bt_right1_ServerClick(object sender, EventArgs e)
    {
        transferItem(this.listOptions1, this.listOptions2, false);
    }


    protected void bt_right2_ServerClick(object sender, EventArgs e)
    {

        transferItem(this.listOptions1, this.listOptions2, true);
    }
    protected void bt_left1_ServerClick(object sender, EventArgs e)
    {

        transferItem(this.listOptions2, this.listOptions1, false);
    }
    protected void bt_left2_ServerClick(object sender, EventArgs e)
    {

        transferItem(this.listOptions2, this.listOptions1, true);
    }


    /// <summary>
    /// Set SQL to change results from database. I use sql directly to evict keep results on viewstate or session
    /// But, if you prefer, change to use dataView Directly.
    /// </summary>
    /// <param name="sqlSelect"> Sql Select command</param>
    /// <param name="valuefield">value field</param>
    /// <param name="textfield">text field</param>
    /// <param name="selectedValue">selected(s) value(s) separated(s) by comma (ex: 1,2,3) ,  if Empty type String.Empty</param>
    public void setSQLSource(string sqlSelect, string valuefield, string textfield, string selectedValue)
    {
        DataView dw = new DataView();

        DataAccess.IDbPersist oConn = (DataAccess.IDbPersist)DataAccess.ConnAccess.getConn();


        //Implement bellow, using your database connection, to retrieve a DataView from your select command.
        DataTable dt = DataAccess.ConnAccess.fetchData(oConn, sqlSelect);
        dw = new DataView(dt);
        //Change two lines above to use your own method connection


        //This method assume that value field is number.. edit to your own preference.

        if (selectedValue != String.Empty && permiteDuplicar == false)
        {
            dw.RowFilter = valuefield + " not in (" + selectedValue + ") ";
        }
        string filtro_esquerda = "";

        if (dw.Table.Columns.Contains("stat"))
        {
            filtro_esquerda = " stat = 1 ";
        }

        if (filtro_esquerda != String.Empty && getMyViewState("semfiltro") == String.Empty)
        {

            string subfiltro = filtro_esquerda;

            if ( dw.RowFilter != String.Empty )
                subfiltro = " and " + filtro_esquerda;
            else
                dw.RowFilter = subfiltro;
        }

        listOptions1.DataSource = dw;
        listOptions1.DataValueField = valuefield;
        listOptions1.DataTextField = textfield;
        listOptions1.DataBind();

        if (permiteDuplicar == false)
        {
            dw.RowFilter = valuefield + " in (" + nvl(selectedValue, "0") + ") ";

            listOptions2.DataSource = dw;
            listOptions2.DataValueField = valuefield;
            listOptions2.DataTextField = textfield;
            listOptions2.DataBind();

        }
        else
        {
            if (selectedValue.Trim() == String.Empty)
            {
                listOptions2.DataSource = null; listOptions2.DataBind();
            }
            else
            {
                string[] arr = selectedValue.Split(',');
                System.Collections.Generic.List<Entities.SimplesCodigoNome> lst = new System.Collections.Generic.List<Entities.SimplesCodigoNome>();

                for (int i = 0; i < arr.Length; i++)
                {
                    dw.RowFilter = valuefield + " in (" + nvl(arr[i], "0") + ") ";
                    if (dw.Count > 0)
                    {
                        lst.Add(new Entities.SimplesCodigoNome(arr[i], dw[0][textfield].ToString()));
                    }
                }
                listOptions2.DataSource = lst;
                listOptions2.DataValueField = "Codigo";
                listOptions2.DataTextField = "Nome";
                listOptions2.DataBind();
            }
        }

        this.setMyViewState("sqlSelect", sqlSelect);
        this.setMyViewState("valuefield", valuefield);
        this.setMyViewState("textfield", textfield);


        //if (!Page.IsPostBack)
        //{   //Keep texts on text hidden field
        //    hd_values.Value = selectedValue;

        //    string text_values = string.Empty;
        //    for (int i = 0; i < dw.Count; i++)
        //    {
        //        if (i.Equals(0))
        //            text_values += dw[i][textfield].ToString();
        //        else
        //            text_values += "," + dw[i][textfield].ToString();

        //    }
        //    hd_text.Value = text_values;
        //}
    }

    private string nvl(string val, string valIfNull)
    {
        if (val == null || val == string.Empty)
            return valIfNull;

        return val;
    }


}
