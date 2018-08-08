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


/// <summary>
/// Class's name: ucDoubleCombobox
/// 
/// Created by: Rafael Rend - rafaelrend@gmail.com
/// Creation's Date: 10/01/2012
/// 
/// Short Description: Double ListBox or Double Combobox.. the name can be whatever you think better.
/// 
/// Long Description: This UserControl consists use 2 ListBoxes to select multiple values. With left's ListBox 
/// you select options you want, after, using buttons or double listbox click, transfer to right's ListBox. 
/// Right's ListBox will save your selected list. 
///
/// This uses javascript functions and works fine inside page with masterpages and/or ajaxExtensions. You can usually
/// repeat this usercontrol in same .aspx page all times you need.
///
/// The reason I decide create this user control is select multiple values between listboxes wihtout dependes of PostBack.
/// In specific web application I was develloping, my pages were very heavly and constantly used and sometimes across the internet. 
/// Therefore every postback I could evict was good to employer productivity.
/// 
/// If I had decided use asp.net and c# functions to transfer values between listbox, every button click whould be a new postback to server.
/// 
/// Ps: Don't forget to include selectbox.js (From: Matt Kruse) script on your .aspx page. 
/// This File Contains javascript functions that will operate with html multiple select options.
/// example: <script src='selectbox.js' type='text/javascript></script>
/// 
/// 
/// </summary>
public partial class ucDoubleListBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            setJavascriptEvents();

        }
        else
        {
            //If PostBack, our listBox losted all changes made by javascript.. so, unfortunally, we will 
            //load this changes.
            if (getMyViewState("sqlSelect") != String.Empty && getMyViewState("valuefield") != String.Empty)
            {

                setSQLSource(getMyViewState("sqlSelect"),
                        getMyViewState("valuefield"), getMyViewState("textfield"),
                        hd_values.Value);
            }

        }

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

        //Implement bellow, using your database connection, to retrieve a DataView from your select command.
        DataTable dt = ConnAccess.fetchData(ConnAccess.getConn(),  sqlSelect );
        dw = new DataView(dt);
        //Change two lines above to use your own method connection


       //This method assume that value field is number.. edit to your own preference.

        if (selectedValue != String.Empty)
        {
            dw.RowFilter = valuefield + " not in (" + selectedValue + ") ";
        }

        listOptions1.DataSource = dw;
        listOptions1.DataValueField = valuefield;
        listOptions1.DataTextField = textfield;
        listOptions1.DataBind();


        dw.RowFilter = valuefield + " in ("+nvl(selectedValue,"0")+") ";

        listOptions2.DataSource = dw;
        listOptions2.DataValueField = valuefield;
        listOptions2.DataTextField = textfield;
        listOptions2.DataBind();


        this.setMyViewState("sqlSelect", sqlSelect);
        this.setMyViewState("valuefield", valuefield);
        this.setMyViewState("textfield", textfield);


        if (!Page.IsPostBack)
        {   //Keep texts on text hidden field
            hd_values.Value = selectedValue;

            string text_values = string.Empty;
            for (int i = 0; i < dw.Count; i++)
            {
                if (i.Equals(0))
                    text_values += dw[i][textfield].ToString();
                else
                    text_values += "," + dw[i][textfield].ToString();

            }
            hd_text.Value = text_values;
        }
    }

    private string nvl(string val, string valIfNull)
    {
        if (val == null || val == string.Empty)
            return valIfNull;

        return val;
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

    /// <summary>
    /// Set onclick events in every button in this user control.
    /// </summary>
    private void setJavascriptEvents()
    {
         string listbox1_clientid = listOptions1.ClientID;
         string listbox2_clientid = listOptions2.ClientID;
         string tmp_hidden_clientid  = hdTemp.ClientID;

         //These hiddens fields will save and return our both values and text's kept on right listbox.
         string hd_values_clientid = hd_values.ClientID;
         string hd_text_clientid = hd_text.ClientID;

        //Seting calls to javascript function to send values from left's listbox to right.
         bt_right1.Attributes.Remove("onclick");
         bt_right1.Attributes.Add("onclick", "moveSelectedOptions(" + getJsEle(listbox1_clientid) + "," +
             getJsEle(listbox2_clientid) + ",true," + getJsEle(tmp_hidden_clientid) + ".value,"+
             getJsEle(hd_values_clientid) + "," + getJsEle(hd_text_clientid) + ");");
         
        bt_right2.Attributes.Remove("onclick");
        bt_right2.Attributes.Add("onclick", "moveAllOptions(" + getJsEle(listbox1_clientid) + "," +
             getJsEle(listbox2_clientid) + ",true," + getJsEle(tmp_hidden_clientid) + ".value," +
             getJsEle(hd_values_clientid) + "," + getJsEle(hd_text_clientid) + ");");
        

        //Seting calls to javascript function to send values back from right's listbox to left.   
         bt_left1.Attributes.Remove("onclick");
         bt_left1.Attributes.Add("onclick", "moveSelectedOptions(" + getJsEle(listbox2_clientid ) + "," +
             getJsEle(listbox1_clientid) + ",true," + getJsEle(tmp_hidden_clientid) + ".value," +
             getJsEle(hd_values_clientid) + "," + getJsEle(hd_text_clientid) + ");");
         
        bt_left2.Attributes.Remove("onclick");
        bt_left2.Attributes.Add("onclick", "moveAllOptions(" + getJsEle(listbox2_clientid) + "," +
             getJsEle(listbox1_clientid) + ",true," + getJsEle(tmp_hidden_clientid) + ".value," +
             getJsEle(hd_values_clientid) + "," + getJsEle(hd_text_clientid) + ");");
     
        //Seting double click events on every listBox.
        listOptions1.Attributes.Remove("onclick");
        listOptions1.Attributes.Add("onclick","return false"); 
        listOptions1.Attributes.Remove("onDblClick");
        listOptions1.Attributes.Add("onDblClick",bt_right1.Attributes["onclick"]); //Double click is equal to call in button from left to right.
       
           //Seting double click event on right list box.
        listOptions2.Attributes.Remove("onclick");
        listOptions2.Attributes.Add("onclick","return false"); 
        listOptions2.Attributes.Remove("onDblClick");
        listOptions2.Attributes.Add("onDblClick",bt_right2.Attributes["onclick"]); //Double click is equal to call in button from right to left.
       
    }

    protected void setMyViewState(string key, string value)
    {
        ViewState[key] = value;
    }

    protected string getMyViewState(string key)
    {
        if (ViewState[key] == null)
            return String.Empty;

        return ViewState[key].ToString();

    }

    private string getJsEle(string id)
    {
        return "document.getElementById('" + id + "')";

    }
  

}