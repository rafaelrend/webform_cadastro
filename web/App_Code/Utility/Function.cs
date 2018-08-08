using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

namespace Utilities
{
	/// <summary>
	/// Summary description for Functions
	/// </summary>
	public static class Function
	{
		/// <summary> 
		/// Retorna o id dos itens com o chekbox selecionado em uma grid 
		/// </summary> 
		/// <param name="grdView">Grid onde esta os checkbox</param> 
		/// <param name="chkBox">Nome do checkBox</param> 
		/// <param name="index">Indice da coluna onde esta o checkbox</param> 
		/// <returns>String com ids separado por virgula</returns> 
		public static string GetChecked(GridView grdView, string chkBox, int index)
		{
			//cria e instancia uma variável do tipo StringBuilder para concatenar os registros 
			StringBuilder str = new StringBuilder();
			int i = 0;

			//Laço que percorre todas as linhas do grid 
			while (i < grdView.Rows.Count)
			{
				//Variável do tipo GridViewRow recebe a linha específica do GridView 
				GridViewRow row = grdView.Rows[i];
				//A variável booleana recebe true se o checkbox desse registro estiver marcado 
				bool isChecked = ((CheckBox) row.FindControl(chkBox)).Checked;
				if (isChecked)
				{
					str.Append(grdView.Rows[i].Cells[index].Text);
					str.Append(",");
				}
				i += 1;
			}
			if (str.Length > 0)
			{
				return str.ToString().Remove(str.Length - 1, 1);
			}
			else
			{
                return String.Empty;
			}
		} 
		public static void RolesTranslate(string[] str)
		{

		}

        public static string EmailHost = ConfigurationSettings.AppSettings["EmailHost"].ToString();
        public static string EmailUserName = ConfigurationSettings.AppSettings["EmailUserName"].ToString();
        public static string EmailPwd = ConfigurationSettings.AppSettings["EmailPwd"].ToString();
        public static string EmailAdm = ConfigurationSettings.AppSettings["EmailAdm"].ToString();
 


        public static string EnviarEmail(string from, string to, string to2, string subject, string body)
        {
            try
            {

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(to);
                
                if ( to2 != String.Empty)
                    mailMessage.To.Add(to2);

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient(EmailHost);
                System.Net.NetworkCredential SMTPUserInfo =
                    new System.Net.NetworkCredential(EmailUserName, EmailPwd);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = SMTPUserInfo;

                smtpClient.Send(mailMessage);

                return string.Empty;
            }
            catch (Exception ex)
            {
                //return ex.Message;
                return string.Empty;
            }
        }
	}
}