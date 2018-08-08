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
    /// Summary description for Format
    /// </summary>
    public static class Format
    {
        private const int invalid = 0;
        private const int valid = 1;
        private static string[,] entry = { { "'", Environment.NewLine}, { "\\'", "\\n"} };

		public static string Message(string message)
        {
            for (int i = 0; i <= entry.GetUpperBound(1); i++)
            {
                message = message.Replace(entry[invalid, i], entry[valid, i]);
            }

            return message;
        }
		public static string CpfMask(string cpf)
        {
			if (cpf != null)
			{
				if (cpf.Length.Equals(11))
				{
					string cpfMasked = CpfNoMask(cpf);

					return string.Concat(
							cpfMasked.Substring(0, 3), ".",
							cpfMasked.Substring(3, 3), ".",
							cpfMasked.Substring(6, 3), "-",
							cpfMasked.Substring(9, 2));
				}
			}
			
			return cpf;
        }

        /// <summary>
        /// Remove a formatação do CPF passado.
        /// </summary>
        /// <param name="cpf">Número de CPF com formatação</param>
        /// <returns>CPF sem formatação</returns>
		public static string CpfNoMask(string cpf)
        {
            cpf = cpf.ToString().Replace("-", string.Empty);
            cpf = cpf.ToString().Replace(".", string.Empty);
            cpf = cpf.ToString().Replace(".", string.Empty);
            return cpf.ToString();
        }

        /// <summary>
        /// Formata o CNPJ passado com a mascara padrão.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ sem formatação</param>
        /// <returns>CNPJ com formatação</returns>
		public static string CnpjMask(string cnpj)
        {
            if (cnpj!=null)
			{
				if (cnpj.Length.Equals(14))
				{
					string cnpjMasked = CnpjNoMask(cnpj);

					return string.Concat(
							cnpj.Substring(0, 2), ".",
							cnpj.Substring(2, 3), ".",
							cnpj.Substring(5, 3), "/",
							cnpj.Substring(8, 4), "-",
							cnpj.Substring(12, 2));
				}
            }
            return cnpj;
        }

        /// <summary>
        /// Remove a formatação do CNPJ passado.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ com formatação</param>
        /// <returns>CNPJ sem formatação</returns>
		public static string CnpjNoMask(string cnpj)
        {
            cnpj = cnpj.ToString().Replace("-", string.Empty);
            cnpj = cnpj.ToString().Replace(".", string.Empty);
            cnpj = cnpj.ToString().Replace(".", string.Empty);
            cnpj = cnpj.ToString().Replace("/", string.Empty);
            return cnpj.ToString();
        }
		public static string CepMask(string cep)
		{
			if( cep!=null)
			{
				if (cep.Length == 8)
				{
					string cepMasked = CepNoMask(cep);
					return string.Concat(
							cepMasked.Substring(0, 5), "-",
							cep.Substring(5, 3));
				}
			}
			return cep;
		}
		public static string CepNoMask(string cep)
		{
			return cep.ToString().Replace("-", string.Empty);
		}

        /// <summary>
        /// Repete uma string N vezes
        /// </summary>
        /// <param name="str">String que será repetida</param>
        /// <param name="n">Número de vezes que a string será repetida</param>
        /// <returns>String repetida N vezes</returns>
		public static string RepeatString(string str, int n)
        {
            string st = string.Empty;
            for (int ind = 1; ind <= n; ind++)
            {
                st += str;
            }
            return st;
        }

        public static string RemoveCaracteresEspeciais(string texto)
        {
            texto = texto.Replace(" ", String.Empty);
            texto = texto.Normalize( System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char c in texto.ToCharArray())
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sb.Append(c);


            return sb.ToString();

        }
        public static Control localizaControl(string id, GridViewRow row)
        {
            for (int i = 0; i < row.Cells.Count; i++)
            {
                Control cr = localizaControl(id, row.Cells[i]);

                if (cr != null)
                    return cr;
            }
            return null;
        }
        public static Control localizaControl(string id, Control controlP)
        {

            if (controlP.ID == id)
                return controlP;


            foreach (Control ctl in controlP.Controls)
            {
                if (ctl.ID == id)
                    return ctl;

                if (ctl.Controls.Count > 0)
                {
                    Control cra = localizaControl(id, ctl);
                    if (cra != null)
                    {
                        return cra;
                    }
                } 
            }
            return null;
        }


        public static void EnableControl(Control controlP, bool enable)
        {

            foreach (Control ctl in controlP.Controls)
            {
                if (ctl is TextBox)
                {
                    if (((TextBox)ctl).Visible == true)
                    {
                        ((TextBox)ctl).Enabled = enable;
                    }
                }
                if (ctl is DropDownList)
                {
                    if (((DropDownList)ctl).Visible == true)
                    {
                        ((DropDownList)ctl).Enabled = enable;
                    }
                }
                if (ctl is CheckBox)
                {
                    if (((CheckBox)ctl).Visible == true)
                    {
                        ((CheckBox)ctl).Enabled = enable;
                    }
                }
                if (ctl is HiddenField)
                {


                }
                if (ctl.Controls.Count > 0)
                {
                    EnableControl(ctl, enable);
                }
            }
        }

		public static void ClearControl(Control controlP) 
        { 
        
            foreach ( Control ctl  in controlP.Controls) { 
                if (ctl is TextBox) { 
                    if (((TextBox)ctl).Visible == true) { 
                        ((TextBox)ctl).Text = string.Empty; 
                    } 
                } 
                if (ctl is DropDownList) { 
                    if (((DropDownList)ctl).Visible == true) 
                    {
						if (((DropDownList) ctl).SelectedIndex>0)
						{
                        ((DropDownList)ctl).SelectedIndex = 0; 
                        }
                    } 
                } 
                if (ctl is CheckBox) { 
                    if (((CheckBox)ctl).Visible == true) { 
                        ((CheckBox)ctl).Checked = false; 
                    } 
                }
                if (ctl is HiddenField)
                {

                    ((HiddenField)ctl).Value = "";
                   
                } 
                if (ctl.Controls.Count > 0) { 
                    ClearControl(ctl); 
                } 
            } 
        }

		public static string ConcatEnd(string log, string num, string comp, 
									   string bai, string cid, string est,
									   string cp)
		{
			string separador1 = ", ";
			string separador2 = "/";
			string separador3 = "-";

			string logra = log.Trim();
			string numero = string.Concat(separador1,num.Trim());
			string complemento = string.Concat(separador2,comp.Trim());
			string bairro = string.Concat(separador1, bai.Trim());
			string cidade = string.Concat(separador1, cid.Trim());
			string estado = string.Concat(separador3, est.Trim());
			string cep = CepMask(cp);
			cep = string.Concat(separador1, cep.Trim());

			if (numero.Equals(separador1))
				numero = string.Empty;
			if (complemento.Equals(separador2))
				complemento = string.Empty;
			if (bairro.Equals(separador1))
				bairro = string.Empty;
			if (cidade.Equals(separador1))
				cidade = string.Empty;
			if (estado.Equals(separador3))
				estado = string.Empty;
			if (cep.Equals(separador1))
				cep = string.Empty;

			string endereco = string.Concat(logra, numero, complemento,
											bairro, cidade, estado, cep);

			return endereco.Trim();
		}
		public static string ConcatContato(string dd1, string tel1, string ram1,
									string dd2, string tel2, string ram2,
									string dd3, string tel3, string ram3,
									string sn, string st, string en, string et)
		{
			string contato = String.Concat("{0}",dd1,"{1}",tel1,"{2}",ram1,
										   "{3}",dd2,"{4}",tel2,"{5}",ram2,
										   "{6}",dd3,"{7}",tel3,"{8}",ram3,
										   "{9}",sn,"{10}",st,
										   "{11}",en,"{12}",et);

			contato = string.Format(contato,
				((dd1.Equals(string.Empty) && (dd1!=null)) ? string.Empty : "("),
				((dd1.Equals(string.Empty) && (dd1 != null)) ? string.Empty : ") "),
				((ram1.Equals(string.Empty) && (ram1 != null)) ? string.Empty : " r-"),
				((dd2.Equals(string.Empty) && (dd2 != null)) ? string.Empty : "("),
				((dd2.Equals(string.Empty) && (dd2 != null)) ? string.Empty : ") "),
				((ram2.Equals(string.Empty) && (ram2 != null)) ? string.Empty : " r-"),
				((dd3.Equals(string.Empty) && (dd3 != null)) ? string.Empty : " fax ("),
				((dd3.Equals(string.Empty) && (dd3 != null)) ? string.Empty : ") "),
				((ram3.Equals(string.Empty) && (ram3 != null)) ? string.Empty : " r-"),
				((sn.Equals(string.Empty) && (sn != null)) ? string.Empty : ", Sindico:"),
				((st.Equals(string.Empty) && (st != null)) ? string.Empty : " "),
				((en.Equals(string.Empty) && (en != null)) ? string.Empty : ", Encarregado:"),
				((et.Equals(string.Empty) && (et != null)) ? string.Empty : " "));

			return contato.Trim();
		}

		public static string InvertChar(string str)
		{
			Char[] characters = str.ToCharArray();

			Array.Reverse(characters);

			String reversed = new String(characters, 0, characters.Length);

			return reversed;
		}
    }
}