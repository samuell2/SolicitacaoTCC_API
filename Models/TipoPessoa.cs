using System.Data;

namespace SolicitacaoTCC.API.TipoPessoa
{
    public class TipoPessoa
    {
        public int TP_PESSOA_ID {get; set;}
        public string DESCRICAO {get; set;}
        public DateTime DT_CADASTRO {get; set;}
        public bool STATUS {get; set;}
    }
}