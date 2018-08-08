using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class SimplesCodigoNome
    {
        string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public SimplesCodigoNome(string cd, string nm)
        {
            this.Codigo = cd;
            this.Nome = nm;
        }

        public SimplesCodigoNome()
        {
        }


       /// <summary>
        /// Gera uma lista deste objeto a partir de uma string informada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static IList<Entities.SimplesCodigoNome> generateByString(string str)
        {
            IList<Entities.SimplesCodigoNome> saida = new List<Entities.SimplesCodigoNome>();

            string[] arp = str.Split(',');

            for (int i = 0; i < arp.Length; i++)
            {
                Entities.SimplesCodigoNome item = new Entities.SimplesCodigoNome();

                string[] row = arp[i].Split(new string[] { "||" }, StringSplitOptions.None);

                item.Codigo = row[0];
                try
                {

                    item.Nome = row[1];
                }
                catch
                {
                    item.Nome = row[0];

                }


                saida.Add(item);

            }


            return saida;
        }
		
		/// <summary>
        /// Obtém a descrição pelo código, faz a busca numa lista.
        /// </summary>
		    public static string getDescricaoByCodigo(string cod, IList<Entities.SimplesCodigoNome>  lst)
			{
				for (int i = 0; i < lst.Count; i++)
				{
					if (lst[i].Codigo.Equals(cod))
						return lst[i].Nome;
				}

				return string.Empty;
			}


    }
}
