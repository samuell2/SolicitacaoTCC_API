using System.Data;

namespace SolicitaTCC.API.csharp.Models
{
    public class getWorker
    {
        public int PESSOA_ID { get; set; }

    }
    public class Worker
    {
        public int SOLICITACAO_ID { get; set; }

    }
    public class WorkerData
    {
        public int SOLICITACAO_ID { get; set; }
        public string NOME_PROJ { get; set; }
        public string DESCRICAO { get; set; }
    }

    public class Workers
    {
        public int PESSOA_ID { get; set; }
        public string AREA_ATUACAO { get; set; }
    }

    public class RequestWorkers
    {
        public int SOLICITACAO_ID { get; set; }
        public int ALUNO_SOLIC_ID { get; set; }
        public int PROF_ORIENT_ID { get; set; }
        public string NOME_ALUNO { get; set; }
        public int SITUACAO_ID { get; set; }
        public string SITUACAO { get; set; }
        public string NOME_PROJ { get; set; }
        public string DESCRICAO { get; set; }
        public string DT_APROVACAO { get; set; }
        public string DT_REPROVACAO { get; set; }
        public string JUSTIFICATIVA { get; set; }
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
                professores.Add(professor);
            }

            return professores;
        }

        public List<WorkerData> DataTableListSolic(DataTable dt)
        {
            List<WorkerData> solicList = new List<WorkerData>();

            foreach (DataRow row in dt.Rows)
            {
                WorkerData solic = new WorkerData();

                solic.SOLICITACAO_ID = Convert.ToInt32(row["SOLICITACAO_ID"]);
                solic.NOME_PROJ = row["NOME_PROJ"].ToString();
                solic.DESCRICAO = row["DESCRICAO"].ToString();
                solicList.Add(solic);
            }

            return solicList;
        }


    }


}
