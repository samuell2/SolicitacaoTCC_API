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
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_PROFESSOR @PESSOA_ID", conn))
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

        public bool CancelRequest(cancelRequestsWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_REPROVA_SOLICITACAO @PROF_ORIENT_ID, @SOLICITACAO_ID, @JUSTIFICATIVA", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@SOLICITACAO_ID", Convert.ToInt32(data.SOLICITACAO_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROF_ORIENT_ID", Convert.ToInt32(data.PROF_ORIENT_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@JUSTIFICATIVA", data.JUSTIFICATIVA));


                    adp.Fill(dt1);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public bool CreateProject(createProjectWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_CRIACAO_PROJETO @PROF_ORIENT_ID, @ALUNO_SOLIC_ID, @SOLICITACAO_ID", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@SOLICITACAO_ID", Convert.ToInt32(data.SOLICITACAO_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROF_ORIENT_ID", Convert.ToInt32(data.PROF_ORIENT_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ALUNO_SOLIC_ID", Convert.ToInt32(data.ALUNO_SOLIC_ID)));


                    adp.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public List<ProjectWorkers> ListProject(getRequestsWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_PROJETO @PROF_ORIENT_ID, @ALUNO_SOLIC_ID", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ALUNO_SOLIC_ID", Convert.ToInt32(data.ALUNO_SOLIC_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROF_ORIENT_ID", Convert.ToInt32(data.PROF_ORIENT_ID)));


                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {

                        List<ProjectWorkers> response = new List<ProjectWorkers>();
                        LoginService loginService = new LoginService();

                        foreach (DataRow row in dt1.Rows)
                        {
                            ProjectWorkers request = new ProjectWorkers();

                            request.SOLICITACAO_ID = Convert.ToInt32(row["SOLICITACAO_ID"]);
                            request.PROJETO_ID = Convert.ToInt32(row["PROJETO_ID"]);
                            request.ALUNO = loginService.GetPeople(new Login(row["ALUNO_SOLIC_ID"].ToString()));
                            request.PROFESSOR = loginService.GetPeople(new Login(row["PROF_ORIENT_ID"].ToString()));
                            request.SITUACAO_ID = Convert.ToInt32(row["SITUACAO_ID"]);
                            request.SITUACAO = row["SITUACAO"].ToString();
                            request.DT_INICIO = row["DT_INICIO"].ToString();
                            request.DT_FINAL = row["DT_FINAL"].ToString();
                            request.NOME_PROJ = row["NOME_PROJ"].ToString();
                            request.DESCRICAO = row["DESCRICAO"].ToString();
                            request.DT_APROVACAO = row["DT_APROVACAO"].ToString();
                            request.DT_REPROVACAO = row["DT_REPROVACAO"].ToString();
                            request.JUSTIFICATIVA = row["JUSTIFICATIVA"].ToString();
                            request.PESSOA_CANCELAMENTO = row["PESSOA_CANC_ID"].ToString() != "" ? loginService.GetPeople(new Login(row["PESSOA_CANC_ID"].ToString())) : null;
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

        public bool updateSituationProject(updtProjectWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_ATUALIZA_STATUS_PROJETO @PROJETO_ID, @SITUACAO_ID, @PESSOA_ID, @JUSTIFICATIVA", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROJETO_ID", Convert.ToInt32(data.PROJETO_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@SITUACAO_ID", Convert.ToInt32(data.ID_STATUS)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PESSOA_ID", Convert.ToInt32(data.PESSOA_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@JUSTIFICATIVA", data.JUSTIFICATIVA));


                    adp.Fill(dt1);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<getStageTask> getStageTask()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_ETAPA_TAREFA", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;

                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {

                        List<getStageTask> response = new List<getStageTask>();

                        foreach (DataRow row in dt1.Rows)
                        {
                            getStageTask request = new getStageTask();

                            request.ETAPA_ID = Convert.ToInt32(row["ETAPA_ID"]);
                            request.DESCRICAO = row["DESCRICAO"].ToString();

                            response.Add(request);
                        }

                        return response;
                    }
                    else
                    {
                        throw new Exception("Nenhum estagio para esses parametros!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool createTask(createTask data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_CRIACAO_TAREFA @PROJETO_ID	,@ETAPA_ID ,@TITULO ,@DESCRICAO	,@DT_INICIO", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROJETO_ID", Convert.ToInt32(data.PROJETO_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ETAPA_ID", Convert.ToInt32(data.ETAPA_ID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@TITULO", data.TITULO));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@DESCRICAO", data.DESCRICAO));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@DT_INICIO", data.DT_INICIO));


                    adp.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public List<getTask> getTask(getTaskWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_TAREFA @PROJETO_ID", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PROJETO_ID", Convert.ToInt32(data.PROJETO_ID)));

                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {

                        List<getTask> response = new List<getTask>();

                        foreach (DataRow row in dt1.Rows)
                        {
                            getTask request = new getTask();

                            request.TAREFA_ID = Convert.ToInt32(row["TAREFA_ID"]);
                            request.PROJETO_ID = Convert.ToInt32(row["PROJETO_ID"]);
                            request.ETAPA_ID = Convert.ToInt32(row["ETAPA_ID"]);
                            request.ETAPA = row["ETAPA"].ToString();
                            request.TITULO = row["TITULO"].ToString();
                            request.DESCRICAO = row["DESCRICAO"].ToString();
                            request.DT_INICIO = row["DT_INICIO"].ToString();
                            request.STATUS = Convert.ToInt32(row["STATUS"]);
                            request.DT_FINALIZADA = row["DT_FINALIZADA"].ToString();
                            request.DT_CADASTRO = row["DT_CADASTRO"].ToString();
                            request.STATUS = Convert.ToInt32(row["STATUS"]);


                            response.Add(request);
                        }

                        return response;
                    }
                    else
                    {
                        throw new Exception("Nenhuma tarefa para esses parametros!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool concludedTask(concludedTask data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_CONCLUI_TAREFA @TAREFA_ID", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@TAREFA_ID", Convert.ToInt32(data.TAREFA_ID)));
                   

                    adp.Fill(dt1);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
