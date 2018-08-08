using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Acoes usadas no cadastro
/// </summary>
public struct FormAcao
{
    public static string INSERIR = "Inserir";
    public static string EDITAR = "Editar";
    public static string EXCLUIR = "Excluir";
    public static string PESQUISAR = "Pesquisar";
}


/// <summary>
/// Nome: Session Facade
/// Classe responsável por gerenciar informações do usuário na sessão.
/// </summary>
public class SessionFacade
{
    public static void limpaAcao()
    {
        Acao = FormAcao.INSERIR;
    }


    public static string Acao
    {
        get
        {
            //string str = FormAcao.INSERIR;
            if (HttpContext.Current.Cache["acao"] == null)
            {
                createCache("acao", FormAcao.INSERIR);

            }
            return HttpContext.Current.Cache["acao"].ToString();
        }
        set
        {
            createCache("acao", value);
        }
    }


    public static String Nome
    {
        get
        {
            if (HttpContext.Current.Session["nome"] == null)
            {
                HttpContext.Current.Session["nome"] = string.Empty;

                if (SessionFacade.getCookie("nome") != String.Empty)
                {
                    HttpContext.Current.Session["nome"] = SessionFacade.getCookie("nome");
                }

            }


            return HttpContext.Current.Session["nome"].ToString();
        }
        set
        {
            HttpContext.Current.Session["nome"] = value;
            SessionFacade.setCookie("nome", value);
        }
    }

    public static void setCookie(string nome, string valor)
    {

        HttpContext.Current.Response.Cookies.Remove(nome);
        HttpCookie cook = new HttpCookie(nome);
        cook.Expires = DateTime.Now.AddHours(6);
        cook.Value = valor;
        HttpContext.Current.Request.Cookies.Add(cook);
    }
    public static string getCookie(string nome)
    {
        if (HttpContext.Current.Request.Cookies[nome] != null)
        {
            if (HttpContext.Current.Request.Cookies[nome].Expires > DateTime.Now)
            {
                return HttpContext.Current.Request.Cookies[nome].Value;
            }


        }
        return string.Empty;
    }

    public static String Contrato
    {
        get
        {

            HttpContext.Current.Session.Timeout = 999;

            if (HttpContext.Current.Session["_contrato"] == null)
                HttpContext.Current.Session["_contrato"] = string.Empty;

            return HttpContext.Current.Session["_contrato"].ToString();
        }
        set
        {
            HttpContext.Current.Session["_contrato"] = value;


            HttpContext.Current.Response.Cookies.Remove("_contrato");

            HttpCookie cookie = new HttpCookie("_contrato");
            cookie.Expires = DateTime.Now.AddHours(2);
            cookie.Value = value;
            HttpContext.Current.Response.Cookies.Add(cookie);

        }
    }


    public static Boolean Admin
    {
        get
        {
            return SessionFacade.TipoId.Equals(1);
        }
    }
    public static Boolean ContratoAtivo
    {
        get
        {

            HttpContext.Current.Session.Timeout = 999;

            if (HttpContext.Current.Session["_contratoAtivo"] == null)
                HttpContext.Current.Session["_contratoAtivo"] = false;

            return Convert.ToBoolean(HttpContext.Current.Session["_contratoAtivo"].ToString());
        }
        set { HttpContext.Current.Session["_contratoAtivo"] = value; }
    }

    public static String Controle
    {
        get
        {

            HttpContext.Current.Session.Timeout = 999;

            if (HttpContext.Current.Session["_controle"] == null)
                HttpContext.Current.Session["_controle"] = string.Empty;

            return HttpContext.Current.Session["_controle"].ToString();
        }
        set
        {
            HttpContext.Current.Session["_controle"] = value;



            HttpContext.Current.Response.Cookies.Remove("_controle");

            HttpCookie cookie = new HttpCookie("_controle");
            cookie.Expires = DateTime.Now.AddHours(2);
            cookie.Value = value;
            HttpContext.Current.Response.Cookies.Add(cookie);



        }
    }


    public static String Login
    {
        get
        {
            if (HttpContext.Current.Session["login"] == null)
            {
                HttpContext.Current.Session["login"] = string.Empty;

                if (SessionFacade.getCookie("login") != String.Empty)
                {
                    HttpContext.Current.Session["login"] = SessionFacade.getCookie("login");
                }

            }


            return HttpContext.Current.Session["login"].ToString();
        }
        set
        {
            HttpContext.Current.Session["login"] = value;
            setCookie("login", value);
        }
    }


    public static String listaProcessos
    {
        get
        {
            if (HttpContext.Current.Session["listaProcessos"] == null)
                HttpContext.Current.Session["listaProcessos"] = string.Empty;

            return HttpContext.Current.Session["listaProcessos"].ToString();
        }
        set { HttpContext.Current.Session["listaProcessos"] = value; }
    }

    public static DateTime? ultimoAcessoLista
    {
        get
        {
            if (HttpContext.Current.Session["ultimoAcessoLista"] == null)
                return null;

            return (DateTime)HttpContext.Current.Session["ultimoAcessoLista"];
        }
        set { HttpContext.Current.Session["ultimoAcessoLista"] = value; }

    }

    public static String listaPaginas
    {
        get
        {
            if (HttpContext.Current.Session["listaPaginas"] == null)
                HttpContext.Current.Session["listaPaginas"] = string.Empty;

            return HttpContext.Current.Session["listaPaginas"].ToString();
        }
        set { HttpContext.Current.Session["listaPaginas"] = value; }
    }

    public static String listaModulos
    {
        get
        {
            if (HttpContext.Current.Session["listaModulos"] == null)
                HttpContext.Current.Session["listaModulos"] = string.Empty;

            return HttpContext.Current.Session["listaModulos"].ToString().Replace(" ", "");
        }
        set { HttpContext.Current.Session["listaModulos"] = value; }
    }


    public static String getPropriedade(string nome)
    {

        if (HttpContext.Current.Session[nome] == null)
        {
            HttpContext.Current.Session[nome] = String.Empty;
            if (SessionFacade.getCookie(nome) != String.Empty)
            {
                HttpContext.Current.Session[nome] = SessionFacade.getCookie(nome);
            }


        }
        return HttpContext.Current.Session[nome].ToString();

    }
    public static void setPropriedade(string nome, string valor)
    {

        HttpContext.Current.Session[nome] = valor;

    }

    public static bool temPagina(string nm_pagina)
    {
        string paginas = SessionFacade.getPropriedade("_paginas");

        string[] pags = paginas.Split(',');
        for (int i = 0; i < pags.Length; i++)
        {
            if (pags[i].Trim().Equals(nm_pagina))
            {
                return true;
            }
        }
        return false;

    }

    public static String TelaAtual
    {
        get
        {
            if (HttpContext.Current.Session["TelaAtual"] == null)
                HttpContext.Current.Session["TelaAtual"] = "Principal";

            return HttpContext.Current.Session["TelaAtual"].ToString();
        }
        set { HttpContext.Current.Session["TelaAtual"] = value; }
    }

    public static int Id
    {
        get
        {
            if (HttpContext.Current.Session["idUsuario"] == null)
            {
                HttpContext.Current.Session["idUsuario"] = 0; //0
                if (SessionFacade.getCookie("idUsuario") != String.Empty)
                {
                    HttpContext.Current.Session["idUsuario"] = SessionFacade.getCookie("idUsuario");
                }


            }

            return int.Parse(HttpContext.Current.Session["idUsuario"].ToString());
        }
        set
        {
            HttpContext.Current.Session["idUsuario"] = value;
            setCookie("idUsuario", value.ToString());
        }
    }

    /// <summary>
    /// Perfil do usuário logado. 1 - Administrador, 2 - Cliente, 0 - Não logado.
    /// </summary>
    public static int TipoId
    {
        get
        {
            if (HttpContext.Current.Session["TipoId"] == null)
            {
                HttpContext.Current.Session["TipoId"] = 0;

                if (SessionFacade.getCookie("TipoId") != String.Empty)
                {
                    HttpContext.Current.Session["TipoId"] = SessionFacade.getCookie("TipoId");
                }

            }

            return int.Parse(HttpContext.Current.Session["TipoId"].ToString());
        }
        set
        {
            HttpContext.Current.Session["TipoId"] = value;

            SessionFacade.setCookie("TipoId", value.ToString());

        }
    }

    public static bool temPerfilTecnico()
    {
        bool ret = false;

        try
        {
            if (listaProcessos.ToLower().IndexOf("admi") > -1 ||
                 listaProcessos.ToLower().IndexOf("técni") > -1)
                ret = true;
        }
        catch { }

        return ret;

    }



    /// <summary>
    /// Remove todas as variáveis cache que estão na memória
    /// </summary>
    public static void clearAllCache()
    {
        Object SessionListCache = HttpContext.Current.Session["list_Cache"];
        if (SessionListCache == null)
            return;

        ArrayList arr = (ArrayList)SessionListCache;
        for (int i = 0; i < arr.Count; i++)
        {
            HttpContext.Current.Cache.Remove(arr[i].ToString());
        }
        arr.Clear();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    /// <summary>
    /// Remove apenas uma varável cache 
    /// </summary>
    /// <param name="Nome">Nome da varável</param>
    public static void removeCache(String Nome)
    {

        Object SessionListCache = HttpContext.Current.Session["list_Cache"];
        ArrayList arr = (ArrayList)SessionListCache;
        int index = -1;
        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i].ToString() == Nome)
            {
                HttpContext.Current.Cache.Remove(arr[i].ToString());
                index = i;
                break;
            }
        }
        if (index > -1)
        {
            arr.RemoveAt(index);
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    /// <summary>
    /// Cria uma nova variável Cache.
    /// </summary>
    /// <param name="Nome"></param>
    /// <param name="value"></param>
    /// 
    public static void createCache(String Nome, object value)
    {
        HttpContext.Current.Cache.Remove(Nome);
        int minutos = HttpContext.Current.Session.Timeout;
        HttpContext.Current.Cache.Add(Nome, value, null, DateTime.Now.AddMinutes(minutos), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

        Object SessionListCache = HttpContext.Current.Session["list_Cache"];
        ArrayList arr;
        if (SessionListCache != null)
            arr = (ArrayList)SessionListCache;
        else
        {
            arr = new ArrayList();
            HttpContext.Current.Session["list_Cache"] = arr;
        }

        arr.Add(Nome);

    }

    public static int Servidor
    {
        get
        {
            int saida = 0;
            if (getApp("servidor") != String.Empty)
                saida = Convert.ToInt32(getApp("servidor"));

            return saida;
        }
    }

    public static string getApp(string propriedade)
    {
        if (System.Configuration.ConfigurationManager.AppSettings[propriedade] != null)
            return System.Configuration.ConfigurationManager.AppSettings[propriedade].ToString();

        return String.Empty;

    }
    public static String ResolucaoWidth
    {
        get
        {
            if (HttpContext.Current.Session["ResolucaoWidth"] == null)
            {
                HttpContext.Current.Session["ResolucaoWidth"] = string.Empty;

                if (SessionFacade.getCookie("ResolucaoWidth") != String.Empty)
                {
                    HttpContext.Current.Session["ResolucaoWidth"] = SessionFacade.getCookie("ResolucaoWidth");
                }

            }


            return HttpContext.Current.Session["ResolucaoWidth"].ToString();
        }
        set
        {
            HttpContext.Current.Session["ResolucaoWidth"] = value;

        }
    }

    public static String ResolucaoHeight
    {
        get
        {
            if (HttpContext.Current.Session["ResolucaoHeight"] == null)
            {
                HttpContext.Current.Session["ResolucaoHeight"] = string.Empty;

                if (SessionFacade.getCookie("ResolucaoHeight") != String.Empty)
                {
                    HttpContext.Current.Session["ResolucaoHeight"] = SessionFacade.getCookie("ResolucaoHeight");
                }

            }


            return HttpContext.Current.Session["ResolucaoHeight"].ToString();
        }
        set
        {
            HttpContext.Current.Session["ResolucaoHeight"] = value;

        }
    }

    public static string TextoChamada
    {
        get
        {
            if (HttpContext.Current.Session["msgChamada"] == null ||
                HttpContext.Current.Session["msgChamada"].ToString() == String.Empty)
            {

                HttpContext.Current.Session["msgChamada"] = string.Empty;

                if (SessionFacade.getCookie("msgChamada") != String.Empty)
                {
                    HttpContext.Current.Session["msgChamada"] = SessionFacade.getCookie("msgChamada");
                }


            }
            return HttpContext.Current.Session["msgChamada"].ToString();
        }
        set
        {
            HttpContext.Current.Session["msgChamada"] = value;
            setCookie("msgChamada", value);
        }
    }





    //public static string PathPictureFolder = HttpContext.Current.Server.MapPath("../productImages");
    //public static string VirtualPictureFolder = "productImages";
    //public static string PathUrl =
    //   System.Web.VirtualPathUtility.GetDirectory(
    //    System.Configuration.ConfigurationManager.AppSettings["PicturePath"].ToString());

    public static string UniqueName = DateTime.Now.ToShortDateString().Replace(" ", "").Replace("/", "").Replace(":", "");



    /// <summary>
    /// Path para onde os arquivos do sistema são salvos..
    /// </summary>
    public static string PathArquivo
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["PastaAnexos"].ToString();
        }
    }

    /// <summary>
    /// Path para onde os arquivos do sistema são salvos..
    /// </summary>
    public static string VirtualArquivo
    {
        get
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings["VirtualAnexos"].ToString();
            }
            catch { }

            return "Anexos";
        }

    }


    /// <summary>
    /// Indica o módulo do sistema em que o usuário se encontra no momento..
    /// </summary>
    public static string Modulo
    {
        get
        {
            string str = SessionFacade.getPropriedade("Modulo");
            if (str == String.Empty)
                str = "1";

            return str;
        }
        set
        {
            SessionFacade.setPropriedade("Modulo", value);


            HttpContext.Current.Response.Cookies.Remove("_modulo");

            HttpCookie cookie = new HttpCookie("_modulo");
            cookie.Expires = DateTime.Now.AddHours(2);
            cookie.Value = value;
            HttpContext.Current.Response.Cookies.Add(cookie);

        }
    }

    /// <summary>
    /// Pede para o sistema tentar restaurar a sessão a partir dos cookies guardados..
    /// </summary>
    public static void tentaRestaurarSessao()
    {


        if (HttpContext.Current.Request.Cookies["logado"] != null)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["logado"];
            if (cookie.Value != String.Empty)
            {

                string[] ar = cookie.Value.Split(new string[] { "||" }, System.StringSplitOptions.None);

                SessionFacade.Id = Convert.ToInt32(ar[0]);

                SessionFacade.Login = String.Empty;
                SessionFacade.Nome = ar[1];
                SessionFacade.TipoId = Convert.ToInt32(ar[2]);

               // SessionFacade.listaProcessos = ar[3];
                SessionFacade.TextoChamada = ar[3];
                SessionFacade.setPropriedade("_paginas", ar[4]);


                SessionFacade.ResolucaoWidth = ar[5];
                SessionFacade.ResolucaoHeight = ar[6];
                SessionFacade.listaProcessos = ar[7];
                SessionFacade.Login = ar[8];



                //   cookie.Value = SessionFacade.Id.ToString() + "||" + SessionFacade.Nome + "||" + SessionFacade.TipoId.ToString()+ "||" + SessionFacade.listaProcessos +"||"+
                //SessionFacade.TextoChamada + "||" + SessionFacade.getPropriedade("_paginas").Trim();
            }

            HttpCookie cookie2 = HttpContext.Current.Request.Cookies["_modulo"];
            if (cookie2 != null && cookie2.Value != String.Empty)
                SessionFacade.Modulo = cookie2.Value;

        }

    }



}
