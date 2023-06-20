using System.Data;

using System.Data.SqlClient;
using System;
using SolicitaTCC.API.csharp.Models;

namespace SolicitaTCC.API.csharp.Services
{
    public class LoginService
    {
        string connectionStringLocalhost = @"Data Source=CARLOSRODRIGUES\SQLEXPRESS;Initial Catalog=DB_AVANCADO;User ID=USERCSHARP;Password=USERCSHARP;Integrated Security=SSPI;TrustServerCertificate=True";
        string connectionString = @"Data Source=201.62.57.93;Initial Catalog=BD042700;User ID=RA042700;Password=042700;TrustServerCertificate=True";

        public Login Login(userLogin userLogin)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_SR_LOGIN @USER, @PSSW", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@USER", userLogin.EMAIL));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PSSW", userLogin.SENHA));
                    adp.Fill(dt1);

                    if (dt1.Rows.Count <= 0)
                    {
                        throw new Exception("E-mail ou senha incorretos!");
                    }
                    else
                    {
                        return new Login(dt1.Rows[0]["PESSOA_ID"].ToString());
                    }
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Login LoginCreate(CreateLogin user)
        {
            try
            {
                if (user.TIPOPESSOA_ID == 1 && user.USUARIO == null)
                {
                    throw new Exception("USUARIO é obrigatorio para professores");
                }
                if (user.TIPOPESSOA_ID == 2 && user.RA == null)
                {
                    throw new Exception("RA é obrigatorio para alunos");
                }

                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_CADASTRAR_USUARIO @NOME, @TIPOPESSOA_ID, @EMAIL, @PSSW, @RA, @USUARIO, @AREA_ATUACAO", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@NOME", user.NOME));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@TIPOPESSOA_ID", user.TIPOPESSOA_ID));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@EMAIL", user.EMAIL));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PSSW", user.SENHA));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@RA", user.TIPOPESSOA_ID == 1 ? "NULL" : user.RA));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@USUARIO", user.TIPOPESSOA_ID == 2 ? "NULL" : user.USUARIO));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@AREA_ATUACAO", user.AREA_ATUACAO));
                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        try
                        {
                            return new Login(dt1.Rows[0]["PESSOA_ID"].ToString());

                        }
                        catch (Exception ex)
                        {

                            throw new Exception("E-mail já utilizado!");

                        }
                    }
                    else
                    {
                        throw new Exception("Erro ao gerar cadastro!");
                    }


                    

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Pessoa GetPeople(Login user)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_BUSCA_USER, @PESSOA_ID", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@PESSOA_ID", Convert.ToInt32(user.PESSOA_ID)));

                    adp.Fill(dt1);

                    if (dt1.Rows.Count == 1)
                    {
                        List<Pessoa> response = new Functions().DataTableToPessoaList(dt1);
                        return response[0];
                    }
                    else
                    {
                        throw new Exception("Nenhum usuario foi encontrado!");
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
