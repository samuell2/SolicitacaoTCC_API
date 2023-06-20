using System.Data;

using System.Data.SqlClient;
using System;
using SolicitaTCC.API.csharp.Models;

namespace SolicitaTCC.API.csharp.Services
{
    public class WorkerAdivisor
    {
        string connectionStringLocalhost = @"Data Source=CARLOSRODRIGUES\SQLEXPRESS;Initial Catalog=DB_AVANCADO;User ID=USERCSHARP;Password=USERCSHARP;Integrated Security=SSPI;TrustServerCertificate=True";
        string connectionString = @"Data Source=201.62.57.93;Initial Catalog=BD042700;User ID=RA042700;Password=042700;TrustServerCertificate=True";

        public List<Workers> getAdvisor(getWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_BUSCA_USUARIO @PESSOA_ID", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PESSOA_ID", Convert.ToInt32(data.PESSOA_ID)));
                    

                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        List<Workers> respo = new WorkerFunctions().DataTableListWorkers(dt1);

                        return respo;
                    }
                    else
                    {
                        throw new Exception("Nenhum professor foi encontrado para esse usuario!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool sendRequest(postRequestWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_SOLICITA_TRABALHO @ALUNO_SOLIC_ID, @PROF_ORIENT_ID, @NOME_PROJ, @DESCRICAO", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ALUNO_SOLIC_ID", Convert.ToInt32(data.ALUNO_SOLIC_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROF_ORIENT_ID", Convert.ToInt32(data.PROF_ORIENT_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@NOME_PROJ", data.NOME_PROJ));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@DESCRICAO", data.DESCRICAO));


                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Não foi possivel criar a solicitação!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RequestWorkers> ListRequest(getRequestsWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_SOLICITACAO @ALUNO_SOLIC_ID, @PROF_ORIENT_ID", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ALUNO_SOLIC_ID", Convert.ToInt32(data.ALUNO_SOLIC_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROF_ORIENT_ID", Convert.ToInt32(data.PROF_ORIENT_ID)));


                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {

                        List<RequestWorkers> response = new List<RequestWorkers>();
                        LoginService loginService = new LoginService();

                        foreach (DataRow row in dt1.Rows)
                        {
                            RequestWorkers request = new RequestWorkers();

                            request.SOLICITACAO_ID = Convert.ToInt32(row["SOLICITACAO_ID"]);
                            request.ALUNO = loginService.GetPeople(new Login(row["ALUNO_SOLIC_ID"].ToString()));
                            request.PROFESSOR = loginService.GetPeople(new Login(row["PROF_ORIENT_ID"].ToString()));
                            request.SITUACAO_ID = Convert.ToInt32(row["SITUACAO_ID"]);
                            request.SITUACAO = row["SITUACAO"].ToString();
                            request.NOME_PROJ = row["NOME_PROJ"].ToString();
                            request.DESCRICAO = row["DESCRICAO"].ToString();
                            request.DT_APROVACAO = row["DT_APROVACAO"].ToString();
                            request.DT_REPROVACAO = row["DT_REPROVACAO"].ToString();
                            request.JUSTIFICATIVA = row["JUSTIFICATIVA"].ToString();
                            request.PESSOA_REPROVACAO = row["PESSOA_RECUSOU_ID"].ToString() != "" ? loginService.GetPeople(new Login(row["PESSOA_RECUSOU_ID"].ToString())) : null;
                            request.DT_CADASTRO = row["DT_CADASTRO"].ToString();
                            request.STATUS = Convert.ToInt32(row["STATUS"]);

                            response.Add(request);
                        }

                        return response;
                    }
                    else
                    {
                        throw new Exception("Nenhuma solcitação para esses parametros!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
