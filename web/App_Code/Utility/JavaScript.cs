using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Utilities
{
    /// <summary>
    /// Summary description for Script
    /// </summary>
	public static class JavaScript
    {
        /// <summary>
        ///  Testa se um textbox está vazio e exibe um alert.. retorna false se
        ///  o textbox está vazio e true se está preenchido
        /// </summary>
        /// <param name="pag"></param>
        /// <param name="txt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool TextInserted(Page pag, TextBox txt, string msg)
        {
            if (txt.Text.Trim() == String.Empty)
            {
                Alert(msg, pag);
                txt.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Executa alert no JavaScript (Sobrecarga)
        /// </summary>
        /// <param name="pag">Pagina onde será executado o JavaScript</param>
        /// <param name="msg">Mensagem que será exibida</param>
		public static void Alert(string msg, Page pag)
        {
			ExecuteScript(pag, "alert", "alert('" + Format.Message(msg) + "');", true);
        }

        /// <summary>
        /// Executa script generico do JavaScript
        /// </summary>
        /// <param name="pag">Pagina onde será registrado o JavaScript</param>
        /// <param name="scr">Script que será executado</param>
		public static void ExecuteScript(Page pag, string scr)
        {
            ExecuteScript(pag, "scrpt", scr);
        }

        /// <summary>
        /// Executa script generico do JavaScript (Sobrecarga)
        /// </summary>
        /// <param name="pag">Pagina onde será registrado o JavaScript</param>
        /// <param name="scr">Script que será executado</param>
        /// <param name="addTags">Define se será adicionada a tag javascript</param>
		public static void ExecuteScript(Page pag, string scr, bool addTags)
        {
            string key = DateTime.Now.Year.ToString() + DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            ExecuteScript(pag, key, scr, addTags);
        }

        /// <summary>
        /// Executa script generico do JavaScript (Sobrecarga)
        /// </summary>
        /// <param name="pag">Pagina onde será registrado o JavaScript</param>
        /// <param name="key">Chave de identificação do script</param>
        /// <param name="scr">Script que será executado</param>
		public static void ExecuteScript(Page pag, string key, string scr)
        {
            ExecuteScript(pag, key, scr, false);
        }

        /// <summary>
        /// Executa script generico do JavaScript (Sobrecarga)
        /// </summary>
        /// <param name="pag">Pagina onde será registrado o JavaScript</param>
        /// <param name="key">Chave de identificação do script</param>
        /// <param name="scr">Script que será executado</param>
        /// <param name="addTags">Define se será adicionada a tag javascript</param>
		public static void ExecuteScript(Page pag, string key, string scr, bool addTags)
        {
            if ((!(ScriptManager.GetCurrent(pag).Page.IsStartupScriptRegistered(key))))
            {
                if (scr.IndexOf("window.close") < 0)
                {

                    ScriptManager.RegisterStartupScript(pag, typeof(Page), key, scr, addTags);
                }
                else
                {
                   // scr = scr.Replace("window.close", "self.close");
                    ScriptManager.RegisterClientScriptBlock(pag, typeof(Page), key, scr, addTags);
                    
                }
                }
        }

        /// <summary>
        /// Executa script de mensagem para uma div na pagina
        /// </summary>
        /// <param name="msg">Mensagem que será exibida</param>
        /// <param name="pag">Pagina onde será exibido a mensagem</param>
		public static void ShowMessage(Page pag, string msg)
        {
			msg = Format.Message(msg);
            string scr = "showMessage('" + msg + "');";
            ScriptManager.RegisterStartupScript(pag, typeof(Page), "Script", scr, true);
        }

        /// <summary>
        /// Inclui url numa pagina
        /// </summary>
        /// <param name="ctr">Objeto onde será incluido a url (ex: this)</param>
        /// <param name="key">Chave de identificação do script</param>
        /// <param name="url">url que será incluída</param>
		public static void IncludeUrl(Page pag, string key, string url)
        {
            if ((!(ScriptManager.GetCurrent(pag).Page.IsClientScriptBlockRegistered(key))))
            {
                ScriptManager.RegisterClientScriptInclude(pag, typeof(Page), key, url);
            }
        }

        /// <summary>
        /// Coloca o foco no controle especificado
        /// </summary>
        /// <param name="pag">Pagina onde esta o controle</param>
        /// <param name="ctr">Controle que receberá o foco</param>
		public static void SetFocus(Page pag, Control ctr)
        {
            ScriptManager.GetCurrent(pag).SetFocus(ctr);
        }
    }

}