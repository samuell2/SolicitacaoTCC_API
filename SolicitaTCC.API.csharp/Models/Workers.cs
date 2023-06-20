using System.Data;

namespace SolicitaTCC.API.csharp.Models
{
    public class getWorker
    {
        public int PESSOA_ID { get; set; }

    }

    public class Workers
    {
        public int PESSOA_ID { get; set; }
        public string NOME_PROJ { get; set; }
        public string AREA_ATUACAO { get; set; }
        public string ACEITA_TRABALHO { get; set; }
    }

    public class RequestWorkers
    {
        public int SOLICITACAO_ID { get; set; }
        public Pessoa ALUNO { get; set; }
        public Pessoa PROFESSOR { get; set; }
        public int SITUACAO_ID { get; set; }
        public string SITUACAO { get; set; }
        public string NOME_PROJ { get; set; }
        public string DESCRICAO { get; set; }
        public string DT_APROVACAO { get; set; }
        public string DT_REPROVACAO { get; set; }
        public string JUSTIFICATIVA { get; set; }
        public Pessoa PESSOA_RECUSOU_ID { get; set; }
        public string DT_CADASTRO { get; set; }
        public int STATUS { get; set; }

    }

    public class ProjectWorkers
    {
        public int PROJETO_ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public Pessoa ALUNO { get; set; }
        public Pessoa PROFESSOR { get; set; }
        public int SITUACAO_ID { get; set; }
        public string SITUACAO_PROJETO { get; set; }
        public string DT_INICIO { get; set; }
        public string DT_FINAL { get; set; }
        public string NOME_PROJ { get; set; }
        public string DESCRICAO { get; set; }
        public string DT_APROVACAO { get; set; }
        public string DT_REPROVACAO { get; set; }
        public string JUSTIFICATIVA { get; set; }
        public Pessoa PESSOA_CANCELAMENTO { get; set; }
        public string DT_CADASTRO { get; set; }
        public int STATUS { get; set; }

    }

    public class postRequestWorker
    {
        public int ALUNO_SOLIC_ID { get; set; }
        public int PROF_ORIENT_ID { get; set; }
        public string NOME_PROJ { get; set; }
        public string DESCRICAO { get; set; }

    }

    public class getRequestsWorker
    {
        public int ALUNO_SOLIC_ID { get; set; }
        public int PROF_ORIENT_ID { get; set; }

    }
    public class getStageTask
    {
        public int ETAPA_ID { get; set; }
        public string DESCRICAO { get; set; }

    }
    public class createTask
    {
        public int PROJETO_ID { get; set; }
        public int ETAPA_ID { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public string DT_CADASTRO { get; set; }
     
    }
    public class getTaskWorker
    {
        public int PROJETO_ID { get; set; }
    }

    public class concludedTask
    {
        public int TAREFA_ID { get; set; }
    }

    public class getTask
    {
        public int TAREFA_ID { get; set; }
        public int PROJETO_ID { get; set; }
        public int ETAPA_ID { get; set; }
        public string ETAPA { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public string DT_INICIO { get; set; }
        public string DT_PREVISTA { get; set; }
        public int FL_FINALIZADA { get; set; }
        public string DT_FINALIZADA { get; set; }
        public string DT_CADASTRO { get; set; }
        public int STATUS { get; set; }

    }
    public class updtProjectWorker
    {
        public int PROJETO_ID { get; set; }
        public int STATUS { get; set; }
        public int PESSOA_ID { get; set; }
        public string JUSTIFICATIVA { get; set; }

    }
    public class cancelRequestsWorker
    {
        public int PROF_ORIENT_ID { get; set; }
        public string JUSTIFICATIVA { get; set; }
        public int SOLICITACAO_ID { get; set; }

    }

    public class createProjectWorker
    {
        public int ALUNO_SOLIC_ID { get; set; }
        public int PROF_ORIENT_ID { get; set; }
        public int SOLICITACAO_ID { get; set; }

    }



    public class WorkerFunctions
    {
        

        public List<Workers> DataTableListWorkers(DataTable dt)
        {
            List<Workers> professores = new List<Workers>();

            foreach (DataRow row in dt.Rows)
            {
                Workers professor = new Workers();

                professor.PESSOA_ID = Convert.ToInt32(row["PESSOA_ID"]);
                professor.AREA_ATUACAO = row["AREA_ATUACAO"].ToString();
                professor.NOME_PROJ = row["NOME_PROJ"].ToString();
                professor.ACEITA_TRABALHO = row["ACEITA_TRABALHO"].ToString();
                professores.Add(professor);
            }

            return professores;
        }

        
    }


}
