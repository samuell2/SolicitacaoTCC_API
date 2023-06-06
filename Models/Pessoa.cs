using System.Data;

namespace SolicitacaoTCC.API.pessoa
{
    public class Pessoa
    {
        public int PESSOA_ID {get; set;}
        public string NOME {get; set;}
        public int TIPOPESSOA_ID {get; set;}
        public string EMAIL {get; set;}
        public string SENHA {get; set;}
        public DateTime DT_CADASTRO {get; set;}
        public bool STATUS {get; set;} 
    }

    public class CreatePessoa
    {
        public Pessoa Cadastro(int id, string nome, int tp_id, string email, string senha, DateTime dtcad, bool status)
        {
            
        }

    }
}