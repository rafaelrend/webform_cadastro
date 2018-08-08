using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
/// <summary>
/// Summary description for BaseMasterPage
/// </summary>
public class BaseMasterPage : System.Web.UI.MasterPage
{
    public bool validaAcesso = false;
    public bool isPopup = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GC.Collect(); GC.WaitForPendingFinalizers();

        }
    }

    protected void testaChamadosNaoAvaliados()
    {
        Session.Timeout = 9999;
       

    }


    public Control encontraControles(Control controlP, string id)
    {

        foreach (Control ctl in controlP.Controls)
        {


            if (ctl.ID == id)
                return ctl;

            if (ctl.Controls.Count > 0)
            {
                Control ctt = encontraControles(ctl, id);
                if (ctt != null)
                    return ctt;
            }
        }

        return null;
    }

    public System.Collections.ArrayList geraArrList(string paginas)
    {
        string[] ar = paginas.Split(',');

        System.Collections.ArrayList ret = new System.Collections.ArrayList();
        for (int i = 0; i < ar.Length; i++)
        {

            ret.Add(ar[i].Trim());
        }
        return ret;
    }

    public bool temInArray(System.Collections.ArrayList ar, string pag)
    {
        for (int i = 0; i < ar.Count; i++)
        {

            if (ar[i].ToString().IndexOf(pag.Replace("~/","")) > -1)
            {
                return true;
            }
        }
        return false;
    }

    public void defineMenu(MenuItemCollection menu, System.Collections.ArrayList arrTodos)
    {
        if (SessionFacade.TipoId.Equals(1))
            return;

        //arrTodos.Add("ImportacaoDadosDesktop.aspx");

        System.Collections.ArrayList arr = this.geraArrList(SessionFacade.listaPaginas);

        inicio:

        
        //if (!Page.IsPostBack)
        //{
            for (int z = 0; z < menu.Count; z++)
            {
                MenuItem item = menu[z];

                if (item.NavigateUrl != String.Empty &&
                    temInArray(arrTodos, item.NavigateUrl) &&
                    !temInArray(arr, item.NavigateUrl) )
                {
                    menu.RemoveAt( z ); // = false;
                    goto inicio;
                }
                if (item.NavigateUrl == String.Empty && item.ChildItems.Count > 0 )
                {
                    defineMenu(item.ChildItems, arrTodos);

                }



            }
        meio:
        for (int z = menu.Count-1; z >= 0 ; z--)
        {
            if (menu[z].NavigateUrl == String.Empty &&
                   menu[z].ChildItems.Count.Equals(0))
            {
                menu.RemoveAt(z);
                goto meio;
            }

        }

    //}
    }



    public void exlcluiItem(Menu mn, string pagina)
    {
        for (int i = 0; i < mn.Items.Count; i++)
        {
            if (mn.Items[i].NavigateUrl == pagina)
            {
                mn.Items.RemoveAt(i);
                return;
            }
            else if (mn.Items[i].ChildItems.Count > 0)
            {
                for (int y = 0; y < mn.Items[i].ChildItems.Count; y++)
                {
                    if (mn.Items[i].ChildItems[y].NavigateUrl == pagina)
                    {
                        mn.Items[i].ChildItems.RemoveAt(y);
                        return;
                    }
                }

            }
        }
    }

  

    public void verificaAcesso()
    {
        if (validaAcesso)
        {
            Session.Timeout = 9999;
            if (SessionFacade.Id.Equals(0) && false)
            {
                SessionFacade.Id = 2;
                SessionFacade.Nome = "Cláudio";
                SessionFacade.Contrato = "ABC12345";
                SessionFacade.Controle = "O";
                SessionFacade.TipoId = 1;
                //SessionFacade.listaModulos = "ABC12345,TAG548";
                
                //SessionFacade.
            }
            if (SessionFacade.Id.Equals(0))
            {
                if (SessionFacade.getApp("usaCookie") != null && SessionFacade.getApp("usaCookie") != String.Empty)
                {

                    if (Request.Cookies["logado"] != null)
                    {
                        HttpCookie cookie = Request.Cookies["logado"];
                        if (cookie.Value != String.Empty)
                        {

                            string[] ar = cookie.Value.Split("||".ToCharArray());

                            SessionFacade.Id = Convert.ToInt32(ar[0]);

                            SessionFacade.Login = ar[1];
                            SessionFacade.Nome = ar[2];
                            SessionFacade.listaProcessos = ar[3];
                            SessionFacade.listaModulos = ar[4];

                            if (Request.Cookies["_contrato"] != null && Request.Cookies["_contrato"].Value != String.Empty)
                            {
                                Session["_contrato"] = Request.Cookies["_contrato"].Value;
                            }

                            if (Request.Cookies["_controle"] != null && Request.Cookies["_controle"].Value != String.Empty)
                            {
                                Session["_controle"] = Request.Cookies["_controle"].Value;
                            }

                            if (Request.Cookies["TipoId"] != null && Request.Cookies["TipoId"].Value != String.Empty)
                            {
                                Session["TipoId"] = Request.Cookies["TipoId"].Value;
                            }

                            return;

                        }
                    }
                }
            }


            if (SessionFacade.Id.Equals(0))
            {

               



                //Request.Q
                if (!isPopup)
                    Response.Redirect("~/login.aspx");
                else
                {
                    Utilities.JavaScript.Alert("Sessão expirada!", this.Page);
                    Utilities.JavaScript.ExecuteScript(this.Page, "opener.location.href='login.aspx'; window.close();", true);
                }

            }
         

        }
    }
}
