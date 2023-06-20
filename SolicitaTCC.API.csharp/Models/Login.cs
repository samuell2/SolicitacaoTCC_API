using System.Data;

namespace SolicitaTCC.API.csharp.Models
{
    public class userLogin
    {
        public string EMAIL { get; set; }
        public string SENHA { get; set; }
    }

    public class Login
    {
        public string PESSOA_ID { get; set; }

        public Login(string pessoa_id)
        {
            this.PESSOA_ID = pessoa_id;
        }
    }

    public class CreateLogin
    {
        public string NOME { get; set; }
        public int TIPOPESSOA_ID { get; set; }
        public string EMAIL { get; set; }
        public string SENHA { get; set; }
        public string? RA { get; set; }
        public string AREA_ATUACAO {get; set;}
        public string? USUARIO { get; set; }
    }

    public class Pessoa
    {
        public int PESSOA_ID { get; set; }
        public string NOME { get; set; }
        public int TIPOPESSOA_ID { get; set; }
        public string EMAIL { get; set; }
        public string RA { get; set; }
        public string USUARIO { get; set; }
        public string IMG { get; set; }
        public int STATUS { get; set; }
        public string AREA_ATUACAO {get; set;}

    }

    public class Functions
    {
        public List<Pessoa> DataTableToPessoaList(DataTable dt)
        {
            List<Pessoa> pessoas = new List<Pessoa>();

            foreach (DataRow row in dt.Rows)
            {
                Pessoa pessoa = new Pessoa();

                pessoa.PESSOA_ID = Convert.ToInt32(row["PESSOA_ID"]);
                pessoa.NOME = row["NOME"].ToString();
                pessoa.TIPOPESSOA_ID = Convert.ToInt32(row["TIPOPESSOA_ID"]);
                pessoa.EMAIL = row["EMAIL"].ToString();
                pessoa.RA = row["RA"].ToString();
                pessoa.AREA_ATUACAO = row["AREA_ATUACAO"].ToString();
                pessoa.USUARIO = row["USUARIO"].ToString();
                pessoa.STATUS = Convert.ToInt32(row["STATUS"]);

                pessoas.Add(pessoa);
            }

            return pessoas;
        }
    }

    


}
