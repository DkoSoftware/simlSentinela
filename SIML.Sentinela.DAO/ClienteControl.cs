using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using SIML.Sentnela;
using System.Data;

namespace SIML.Sentinela
{


    /// <summary> 
    /// Tabela: Cliente  
    /// Autor: DAL Creator .net 
    /// Data de criação: 23/06/2013 15:53:57 
    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "ClienteFields". 
    /// </summary> 
    public class ClienteControl : IDisposable 
    {

        #region String de conexão 
        private string StrConnetionDB = ConfigurationManager .ConnectionStrings["StringConn"].ToString();
        #endregion


        #region Propriedade que armazena erros de execução 
        private string _ErrorMessage;
        public string ErrorMessage { get { return _ErrorMessage; } }
        #endregion


        #region Objetos de conexão 
        SqlConnection Conn;
        SqlCommand Cmd;
        SqlTransaction Tran;
        #endregion


        #region Funcões que retornam Conexões e Transações 

        public SqlTransaction GetNewTransaction(SqlConnection connIn)
        {
            if (connIn.State != ConnectionState.Open)
                connIn.Open();
            SqlTransaction TranOut = connIn.BeginTransaction();
            return TranOut;
        }

        public SqlConnection GetNewConnection()
        {
            return GetNewConnection(this.StrConnetionDB);
        }

        public SqlConnection GetNewConnection(string StringConnection)
        {
            SqlConnection connOut = new SqlConnection(StringConnection);
            return connOut;
        }

        #endregion


        #region enum SQLMode 
        /// <summary>   
        /// Representa o procedimento que está sendo executado na tabela.
        /// </summary>
        public enum SQLMode
        {                     
            /// <summary>
            /// Adiciona registro na tabela.
            /// </summary>
            Add,
            /// <summary>
            /// Atualiza registro na tabela.
            /// </summary>
            Update,
            /// <summary>
            /// Excluir registro na tabela
            /// </summary>
            Delete,
            /// <summary>
            /// Exclui TODOS os registros da tabela.
            /// </summary>
            DeleteAll,
            /// <summary>
            /// Seleciona um registro na tabela.
            /// </summary>
            Select,
            /// <summary>
            /// Seleciona TODOS os registros da tabela.
            /// </summary>
            SelectAll,
            /// <summary>
            /// Excluir ou seleciona um registro na tabela.
            /// </summary>
            SelectORDelete
        }
        #endregion 


        public ClienteControl() {}


        #region Inserindo dados na tabela 

        /// <summary> 
        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Add( ref ClienteFields FieldInfo )
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_Cliente_Add", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
                this.Tran.Commit();
                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
                return true;

            }
            catch (SqlException e)
            {
                this.Tran.Rollback();
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this.Tran.Rollback();
                this._ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                if (this.Conn != null)
                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
                if (this.Cmd != null)
                  this.Cmd.Dispose();
            }
        }

        #endregion


        #region Inserindo dados na tabela utilizando conexão e transação externa (compartilhada) 

        /// <summary> 
        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref ClienteFields FieldInfo )
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_Cliente_Add", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
                return true;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return false;
            }
        }

        #endregion


        #region Editando dados na tabela 

        /// <summary> 
        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Update( ClienteFields FieldInfo )
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_Cliente_Update", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
                this.Tran.Commit();
                return true;
            }
            catch (SqlException e)
            {
                this.Tran.Rollback();
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this.Tran.Rollback();
                this._ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                if (this.Conn != null)
                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
                if (this.Cmd != null)
                  this.Cmd.Dispose();
            }
        }

        #endregion


        #region Editando dados na tabela utilizando conexão e transação externa (compartilhada) 

        /// <summary> 
        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_Cliente_Update", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
                return true;
            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return false;
            }
        }

        #endregion


        #region Excluindo todos os dados da tabela 

        /// <summary> 
        /// Exclui todos os registros da tabela
        /// </summary>
        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool DeleteAll()
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                this.Tran.Commit();
                return true;
            }
            catch (SqlException e)
            {
                this.Tran.Rollback();
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this.Tran.Rollback();
                this._ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                if (this.Conn != null)
                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
                if (this.Cmd != null)
                  this.Cmd.Dispose();
            }
        }

        #endregion


        #region Excluindo todos os dados da tabela utilizando conexão e transação externa (compartilhada)

        /// <summary> 
        /// Exclui todos os registros da tabela
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                return true;
            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return false;
            }
        }

        #endregion


        #region Excluindo dados da tabela 

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( ClienteFields FieldInfo )
        {
            return Delete(FieldInfo.idCliente);
        }

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="Param_idCliente">int</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete(
                                     int Param_idCliente)
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_Cliente_Delete", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                this.Tran.Commit();
                return true;
            }
            catch (SqlException e)
            {
                this.Tran.Rollback();
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this.Tran.Rollback();
                this._ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                if (this.Conn != null)
                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
                if (this.Cmd != null)
                  this.Cmd.Dispose();
            }
        }

        #endregion


        #region Excluindo dados da tabela utilizando conexão e transação externa (compartilhada)

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
        {
            return Delete(ConnIn, TranIn, FieldInfo.idCliente);
        }

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="Param_idCliente">int</param>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
                                     int Param_idCliente)
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_Cliente_Delete", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                return true;
            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
                return false;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return false;
            }
        }

        #endregion


        #region Selecionando um item da tabela 

        /// <summary> 
        /// Retorna um objeto ClienteFields através da chave primária passada como parâmetro
        /// </summary>
        /// <param name="Param_idCliente">int</param>
        /// <returns>Objeto ClienteFields</returns> 
        public ClienteFields GetItem(
                                     int Param_idCliente)
        {
            ClienteFields infoFields = new ClienteFields();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_Select", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            if (dr.Read())
                            {
                               infoFields = GetDataFromReader( dr );
                            }
                        }
                    }
                 }

                 return infoFields;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion


        #region Selecionando todos os dados da tabela 

        /// <summary> 
        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto ClienteFields.
        /// </summary>
        /// <returns>List de objetos ClienteFields</returns> 
        public List<ClienteFields> GetAll()
        {
            List<ClienteFields> arrayInfo = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_GetAll", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                           if (!dr.HasRows) return null;
                           while (dr.Read())
                           {
                              arrayInfo.Add(GetDataFromReader( dr ));
                           }
                        }
                    }
                }

                return arrayInfo;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion


        #region Contando os dados da tabela 

        /// <summary> 
        /// Retorna o total de registros contidos na tabela
        /// </summary>
        /// <returns>int</returns> 
        public int CountAll()
        {
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_CountAll", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Connection.Open();
                        object CountRegs = this.Cmd.ExecuteScalar();
                        if (CountRegs == null)
                        { return 0; }
                        else
                        { return (int)CountRegs; }
                    }
                }
            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
                return 0;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return 0;
            }
            finally
            {
                if (this.Conn != null)
                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion


        #region Função GetDataFromReader

        /// <summary> 
        /// Retorna um objeto ClienteFields preenchido com os valores dos campos do SqlDataReader
        /// </summary>
        /// <param name="dr">SqlDataReader - Preenche o objeto ClienteFields </param>
        /// <returns>ClienteFields</returns>
        private ClienteFields GetDataFromReader( SqlDataReader dr )
        {
            ClienteFields infoFields = new ClienteFields();

            if (!dr.IsDBNull(0))
            { infoFields.idCliente = dr.GetInt32(0); }
            else
            { infoFields.idCliente = 0; }



            if (!dr.IsDBNull(1))
            { infoFields.nomeCliente = dr.GetString(1); }
            else
            { infoFields.nomeCliente = string.Empty; }



            if (!dr.IsDBNull(2))
            { infoFields.enderecoClienteA = dr.GetString(2); }
            else
            { infoFields.enderecoClienteA = string.Empty; }



            if (!dr.IsDBNull(3))
            { infoFields.enderecoClienteB = dr.GetString(3); }
            else
            { infoFields.enderecoClienteB = string.Empty; }



            if (!dr.IsDBNull(4))
            { infoFields.enderecoClienteC = dr.GetString(4); }
            else
            { infoFields.enderecoClienteC = string.Empty; }



            if (!dr.IsDBNull(5))
            { infoFields.bairroClienteA = dr.GetString(5); }
            else
            { infoFields.bairroClienteA = string.Empty; }



            if (!dr.IsDBNull(6))
            { infoFields.bairroClienteB = dr.GetString(6); }
            else
            { infoFields.bairroClienteB = string.Empty; }



            if (!dr.IsDBNull(7))
            { infoFields.bairroClientec = dr.GetString(7); }
            else
            { infoFields.bairroClientec = string.Empty; }



            if (!dr.IsDBNull(8))
            { infoFields.cidadeClienteA = dr.GetString(8); }
            else
            { infoFields.cidadeClienteA = string.Empty; }



            if (!dr.IsDBNull(9))
            { infoFields.cidadeClienteB = dr.GetString(9); }
            else
            { infoFields.cidadeClienteB = string.Empty; }



            if (!dr.IsDBNull(10))
            { infoFields.cidadeClienteC = dr.GetString(10); }
            else
            { infoFields.cidadeClienteC = string.Empty; }



            if (!dr.IsDBNull(11))
            { infoFields.estadoClienteA = dr.GetString(11); }
            else
            { infoFields.estadoClienteA = string.Empty; }



            if (!dr.IsDBNull(12))
            { infoFields.estadoClienteB = dr.GetString(12); }
            else
            { infoFields.estadoClienteB = string.Empty; }



            if (!dr.IsDBNull(13))
            { infoFields.estadoClienteC = dr.GetString(13); }
            else
            { infoFields.estadoClienteC = string.Empty; }



            if (!dr.IsDBNull(14))
            { infoFields.cepClienteA = dr.GetString(14); }
            else
            { infoFields.cepClienteA = string.Empty; }



            if (!dr.IsDBNull(15))
            { infoFields.cepClienteB = dr.GetString(15); }
            else
            { infoFields.cepClienteB = string.Empty; }



            if (!dr.IsDBNull(16))
            { infoFields.cepClienteC = dr.GetString(16); }
            else
            { infoFields.cepClienteC = string.Empty; }



            if (!dr.IsDBNull(17))
            { infoFields.telefoneClienteA = dr.GetString(17); }
            else
            { infoFields.telefoneClienteA = string.Empty; }



            if (!dr.IsDBNull(18))
            { infoFields.telefoneClienteB = dr.GetString(18); }
            else
            { infoFields.telefoneClienteB = string.Empty; }



            if (!dr.IsDBNull(19))
            { infoFields.telefoneClienteC = dr.GetString(19); }
            else
            { infoFields.telefoneClienteC = string.Empty; }



            if (!dr.IsDBNull(20))
            { infoFields.telefoneClienteD = dr.GetString(20); }
            else
            { infoFields.telefoneClienteD = string.Empty; }



            if (!dr.IsDBNull(21))
            { infoFields.celularClienteA = dr.GetString(21); }
            else
            { infoFields.celularClienteA = string.Empty; }



            if (!dr.IsDBNull(22))
            { infoFields.celularClienteB = dr.GetString(22); }
            else
            { infoFields.celularClienteB = string.Empty; }



            if (!dr.IsDBNull(23))
            { infoFields.celularClienteC = dr.GetString(23); }
            else
            { infoFields.celularClienteC = string.Empty; }



            if (!dr.IsDBNull(24))
            { infoFields.complementoCliente = dr.GetString(24); }
            else
            { infoFields.complementoCliente = string.Empty; }



            if (!dr.IsDBNull(25))
            { infoFields.dataNascimentoCliente = dr.GetDateTime(25); }
            else
            { infoFields.dataNascimentoCliente = DateTime.MinValue; }



            if (!dr.IsDBNull(26))
            { infoFields.emailClienteA = dr.GetString(26); }
            else
            { infoFields.emailClienteA = string.Empty; }



            if (!dr.IsDBNull(27))
            { infoFields.emailClienteB = dr.GetString(27); }
            else
            { infoFields.emailClienteB = string.Empty; }



            if (!dr.IsDBNull(28))
            { infoFields.contatoClienteA = dr.GetString(28); }
            else
            { infoFields.contatoClienteA = string.Empty; }



            if (!dr.IsDBNull(29))
            { infoFields.contatoClienteB = dr.GetString(29); }
            else
            { infoFields.contatoClienteB = string.Empty; }



            if (!dr.IsDBNull(30))
            { infoFields.contatoClienteC = dr.GetString(30); }
            else
            { infoFields.contatoClienteC = string.Empty; }



            if (!dr.IsDBNull(31))
            { infoFields.cnpjCliente = dr.GetString(31); }
            else
            { infoFields.cnpjCliente = string.Empty; }



            if (!dr.IsDBNull(32))
            { infoFields.cpfCliente = dr.GetString(32); }
            else
            { infoFields.cpfCliente = string.Empty; }



            if (!dr.IsDBNull(33))
            { infoFields.rgCliente = dr.GetString(33); }
            else
            { infoFields.rgCliente = string.Empty; }



            if (!dr.IsDBNull(34))
            { infoFields.inscEstadualCliente = dr.GetString(34); }
            else
            { infoFields.inscEstadualCliente = string.Empty; }



            if (!dr.IsDBNull(35))
            { infoFields.observacoesCliente = dr.GetString(35); }
            else
            { infoFields.observacoesCliente = string.Empty; }



            if (!dr.IsDBNull(36))
            { infoFields.dataCadastroCliente = dr.GetDateTime(36); }
            else
            { infoFields.dataCadastroCliente = DateTime.MinValue; }



            if (!dr.IsDBNull(37))
            { infoFields.tipoCliente = dr.GetString(37); }
            else
            { infoFields.tipoCliente = string.Empty; }



            if (!dr.IsDBNull(38))
            { infoFields.statusCliente = dr.GetString(38); }
            else
            { infoFields.statusCliente = string.Empty; }



            if (!dr.IsDBNull(39))
            { infoFields.fkSubGrupoCliente = dr.GetInt32(39); }
            else
            { infoFields.fkSubGrupoCliente = 0; }



            if (!dr.IsDBNull(40))
            { infoFields.dataUltimaCompraCliente = dr.GetDateTime(40); }
            else
            { infoFields.dataUltimaCompraCliente = DateTime.MinValue; }



            if (!dr.IsDBNull(41))
            { infoFields.numeroCasaCliente = dr.GetString(41); }
            else
            { infoFields.numeroCasaCliente = string.Empty; }



            if (!dr.IsDBNull(42))
            { infoFields.faxCliente = dr.GetString(42); }
            else
            { infoFields.faxCliente = string.Empty; }



            if (!dr.IsDBNull(43))
            { infoFields.dataNascimentoClienteA = dr.GetDateTime(43); }
            else
            { infoFields.dataNascimentoClienteA = DateTime.MinValue; }



            if (!dr.IsDBNull(44))
            { infoFields.dataNascimentoClienteB = dr.GetDateTime(44); }
            else
            { infoFields.dataNascimentoClienteB = DateTime.MinValue; }



            if (!dr.IsDBNull(45))
            { infoFields.dataNascimentoClienteC = dr.GetDateTime(45); }
            else
            { infoFields.dataNascimentoClienteC = DateTime.MinValue; }



            if (!dr.IsDBNull(46))
            { infoFields.emailPrincipalCliente = dr.GetString(46); }
            else
            { infoFields.emailPrincipalCliente = string.Empty; }


            return infoFields;
        }
        #endregion


        #region Selecionando dados da tabela através do campo "cnpjCliente"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo cnpjCliente.
        /// </summary>
        /// <param name="Param_cnpjCliente">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindBycnpjCliente(
                               string Param_cnpjCliente)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBycnpjCliente", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_cnpjCliente", SqlDbType.VarChar, 50)).Value = Param_cnpjCliente;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "cpfCliente"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo cpfCliente.
        /// </summary>
        /// <param name="Param_cpfCliente">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindBycpfCliente(
                               string Param_cpfCliente)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBycpfCliente", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_cpfCliente", SqlDbType.VarChar, 50)).Value = Param_cpfCliente;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "rgCliente"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo rgCliente.
        /// </summary>
        /// <param name="Param_rgCliente">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindByrgCliente(
                               string Param_rgCliente)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindByrgCliente", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_rgCliente", SqlDbType.VarChar, 50)).Value = Param_rgCliente;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "statusCliente"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo statusCliente.
        /// </summary>
        /// <param name="Param_statusCliente">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindBystatusCliente(
                               string Param_statusCliente)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBystatusCliente", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_statusCliente", SqlDbType.VarChar, 2)).Value = Param_statusCliente;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "fkSubGrupoCliente"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkSubGrupoCliente.
        /// </summary>
        /// <param name="Param_fkSubGrupoCliente">int</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindByfkSubGrupoCliente(
                               int Param_fkSubGrupoCliente)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindByfkSubGrupoCliente", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkSubGrupoCliente", SqlDbType.Int)).Value = Param_fkSubGrupoCliente;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "telefoneClienteA"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteA.
        /// </summary>
        /// <param name="Param_telefoneClienteA">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindBytelefoneClienteA(
                               string Param_telefoneClienteA)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteA", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteA", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteA;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "telefoneClienteB"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteB.
        /// </summary>
        /// <param name="Param_telefoneClienteB">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindBytelefoneClienteB(
                               string Param_telefoneClienteB)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteB", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteB", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteB;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "telefoneClienteC"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteB.
        /// </summary>
        /// <param name="Param_telefoneClienteB">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindBytelefoneClienteC(
                               string Param_telefoneClienteC)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteC", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteC", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteC;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados da tabela através do campo "telefoneClienteD"

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteD.
        /// </summary>
        /// <param name="Param_telefoneClienteD">string</param>
        /// <returns>List ClienteFields</returns> 
        public List<ClienteFields> FindBytelefoneClienteD(
                               string Param_telefoneClienteD)
        {
            List<ClienteFields> arrayList = new List<ClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteD", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteD", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteD;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                                arrayList.Add(GetDataFromReader(dr));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        #endregion

        #region Selecionando dados MainCliente

        public DataTable GetMainCliente(string nomeCliente, string nomeContato, string statusCliente, DateTime dtCadastroInicio, DateTime dtCadastroFim, string cidadeCliente, string tipoCliente, List<int> idSubGrupoCliente, string telefone, string codigoCliente, string enderecoCliente)
        {
            DataSet dsMainCliente = new DataSet();
            try
            {
                SqlConnection Conn = new SqlConnection(this.StrConnetionDB);

                string query = GetQuery(nomeCliente, nomeContato, statusCliente, dtCadastroInicio, dtCadastroFim, cidadeCliente, tipoCliente, idSubGrupoCliente, telefone, codigoCliente, enderecoCliente);

                Conn.Open();
                DataTable dt = new DataTable();
                SqlCommand Cmd = new SqlCommand(query.ToString(), Conn);
                Cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dsMainCliente, "MainCliente");

                return dsMainCliente.Tables[0];

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
                return null;
            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return null;
            }
            finally
            {
                if (this.Conn != null)
                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
            }
        }

        private string GetQuery(string nomeCliente, string nomeContato, string statusCliente, DateTime dtCadastroInicio, DateTime dtCadastroFim, string cidadeCliente, string tipoCliente, List<int> idSubGrupoCliente, string telefone, string codigoCliente, string enderecoCliente)
        {
            StringBuilder query = new StringBuilder();
            query.Append(@"  SELECT c.*, (SELECT gc.descricaoGrupoCliente 
                                           FROM GrupoCliente gc 
                                          WHERE gc.idGrupoCliente = (SELECT sg.fkGrupoCliente  
                                                                       FROM SubGrupoCliente sg 
                                                                       WHERE sg.idSubGrupoCliente = c.fkSubGrupoCliente)) as descricaoGrupoCliente
                                                                    FROM Cliente c,SubGrupoCliente	 sgc 
                                                                    WHERE c.fkSubGrupoCliente = sgc.idSubGrupoCliente ");

            if (!string.IsNullOrEmpty(telefone))
            {
                query.Append(GetStringByTelefone(telefone));
            }

            if (!string.IsNullOrEmpty(enderecoCliente))
            {
                query.AppendFormat(@" And (c.enderecoClienteA LIKE '%{0}%' 
                                             OR c.enderecoClienteB LIKE '%{1}%'  
                                             OR c.enderecoClienteC LIKE '%{2}%' )  ", enderecoCliente, enderecoCliente, enderecoCliente);
            }

            if (!string.IsNullOrEmpty(nomeCliente))
                query.AppendFormat(" AND c.nomeCliente LIKE '%{0}%' ", nomeCliente);

            if (!string.IsNullOrEmpty(codigoCliente))
                query.AppendFormat(" AND c.idCliente = '{0}' ", codigoCliente);

            if (!string.IsNullOrEmpty(nomeContato))
                query.AppendFormat(" AND c.contatoClienteA LIKE '%{0}%' ", nomeContato);

            if (statusCliente != "Todas")
                query.AppendFormat("AND c.statusCliente = '{0}' ", statusCliente);

            if (idSubGrupoCliente.Count > 0)
            {
                string ids = string.Empty;
                foreach (var item in idSubGrupoCliente)
                {
                    ids += item.ToString() + ",";
                }

                query.AppendFormat("AND c.fkSubGrupoCliente in ({0}) ", ids.Substring(0, ids.Length - 1));
            }

            // query.AppendFormat(" AND c.dataCadastroCliente >= '{0}' AND c.dataCadastroCliente <= '{1}' ", dtCadastroInicio.ToString("MM/dd/yyyy 00:00:00"), dtCadastroFim.ToString("MM/dd/yyyy 23:59:59"));//TODO descomentar para bd em ingles
            // query.AppendFormat(" AND c.dataCadastroCliente >= '{0}' AND c.dataCadastroCliente <= '{1}' ", dtCadastroInicio.ToString(), dtCadastroFim.ToString());

            if (!string.IsNullOrEmpty(cidadeCliente))
                query.AppendFormat(" AND c.cidadeClienteA LIKE '%{0}%' ", cidadeCliente);

            if (tipoCliente != "Todos")
                query.AppendFormat(" AND c.tipoCliente = {0} ", tipoCliente);

            query.Append(" ORDER BY c.nomeCliente ");

            return query.ToString();
        }

        private string GetStringByTelefone(string telefone)
        {
            string query = string.Empty;
            ClienteControl cliDal = new ClienteControl();

            if (cliDal.GetAll().Find(x => x.telefoneClienteA.Equals(telefone)) != null)
                query = " AND c.telefoneClienteA = '" + telefone + "' ";
            else if (cliDal.GetAll().Find(x => x.telefoneClienteB.Equals(telefone)) != null)
                query = " AND c.telefoneClienteB = '" + telefone + "' ";
            else if (cliDal.GetAll().Find(x => x.telefoneClienteC.Equals(telefone)) != null)
                query = " AND c.telefoneClienteC = '" + telefone + "' ";
            else if (cliDal.GetAll().Find(x => x.telefoneClienteD.Equals(telefone)) != null)
                query = " AND c.telefoneClienteD = '" + telefone + "' ";
            else
                return " AND c.telefoneClienteA = 'X'";

            return query;
        }

        #endregion

      






























































































        #region Função GetAllParameters

        /// <summary> 
        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto ClienteFields</param>
        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
        private SqlParameter[] GetAllParameters( ClienteFields FieldInfo, SQLMode Modo )
        {
            SqlParameter[] Parameters;

            switch (Modo)
            {
                case SQLMode.Add:
                    Parameters = new SqlParameter[47];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idCliente
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].Direction = ParameterDirection.Output;
                    Parameters[0].ParameterName = "@Param_idCliente";
                    Parameters[0].Value = DBNull.Value;

                    break;

                case SQLMode.Update:
                    Parameters = new SqlParameter[47];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idCliente
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].ParameterName = "@Param_idCliente";
                    Parameters[0].Value = FieldInfo.idCliente;

                    break;

                case SQLMode.SelectORDelete:
                    Parameters = new SqlParameter[1];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idCliente
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].ParameterName = "@Param_idCliente";
                    Parameters[0].Value = FieldInfo.idCliente;

                    return Parameters;

                default:
                    Parameters = new SqlParameter[47];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    break;
            }

            //Field nomeCliente
            Parameters[1].SqlDbType = SqlDbType.VarChar;
            Parameters[1].ParameterName = "@Param_nomeCliente";
            if (( FieldInfo.nomeCliente == null ) || ( FieldInfo.nomeCliente == string.Empty ))
            { Parameters[1].Value = DBNull.Value; }
            else
            { Parameters[1].Value = FieldInfo.nomeCliente; }
            Parameters[1].Size = 255;

            //Field enderecoClienteA
            Parameters[2].SqlDbType = SqlDbType.VarChar;
            Parameters[2].ParameterName = "@Param_enderecoClienteA";
            if (( FieldInfo.enderecoClienteA == null ) || ( FieldInfo.enderecoClienteA == string.Empty ))
            { Parameters[2].Value = DBNull.Value; }
            else
            { Parameters[2].Value = FieldInfo.enderecoClienteA; }
            Parameters[2].Size = 255;

            //Field enderecoClienteB
            Parameters[3].SqlDbType = SqlDbType.VarChar;
            Parameters[3].ParameterName = "@Param_enderecoClienteB";
            if (( FieldInfo.enderecoClienteB == null ) || ( FieldInfo.enderecoClienteB == string.Empty ))
            { Parameters[3].Value = DBNull.Value; }
            else
            { Parameters[3].Value = FieldInfo.enderecoClienteB; }
            Parameters[3].Size = 255;

            //Field enderecoClienteC
            Parameters[4].SqlDbType = SqlDbType.VarChar;
            Parameters[4].ParameterName = "@Param_enderecoClienteC";
            if (( FieldInfo.enderecoClienteC == null ) || ( FieldInfo.enderecoClienteC == string.Empty ))
            { Parameters[4].Value = DBNull.Value; }
            else
            { Parameters[4].Value = FieldInfo.enderecoClienteC; }
            Parameters[4].Size = 255;

            //Field bairroClienteA
            Parameters[5].SqlDbType = SqlDbType.VarChar;
            Parameters[5].ParameterName = "@Param_bairroClienteA";
            if (( FieldInfo.bairroClienteA == null ) || ( FieldInfo.bairroClienteA == string.Empty ))
            { Parameters[5].Value = DBNull.Value; }
            else
            { Parameters[5].Value = FieldInfo.bairroClienteA; }
            Parameters[5].Size = 255;

            //Field bairroClienteB
            Parameters[6].SqlDbType = SqlDbType.VarChar;
            Parameters[6].ParameterName = "@Param_bairroClienteB";
            if (( FieldInfo.bairroClienteB == null ) || ( FieldInfo.bairroClienteB == string.Empty ))
            { Parameters[6].Value = DBNull.Value; }
            else
            { Parameters[6].Value = FieldInfo.bairroClienteB; }
            Parameters[6].Size = 255;

            //Field bairroClientec
            Parameters[7].SqlDbType = SqlDbType.VarChar;
            Parameters[7].ParameterName = "@Param_bairroClientec";
            if (( FieldInfo.bairroClientec == null ) || ( FieldInfo.bairroClientec == string.Empty ))
            { Parameters[7].Value = DBNull.Value; }
            else
            { Parameters[7].Value = FieldInfo.bairroClientec; }
            Parameters[7].Size = 255;

            //Field cidadeClienteA
            Parameters[8].SqlDbType = SqlDbType.VarChar;
            Parameters[8].ParameterName = "@Param_cidadeClienteA";
            if (( FieldInfo.cidadeClienteA == null ) || ( FieldInfo.cidadeClienteA == string.Empty ))
            { Parameters[8].Value = DBNull.Value; }
            else
            { Parameters[8].Value = FieldInfo.cidadeClienteA; }
            Parameters[8].Size = 255;

            //Field cidadeClienteB
            Parameters[9].SqlDbType = SqlDbType.VarChar;
            Parameters[9].ParameterName = "@Param_cidadeClienteB";
            if (( FieldInfo.cidadeClienteB == null ) || ( FieldInfo.cidadeClienteB == string.Empty ))
            { Parameters[9].Value = DBNull.Value; }
            else
            { Parameters[9].Value = FieldInfo.cidadeClienteB; }
            Parameters[9].Size = 255;

            //Field cidadeClienteC
            Parameters[10].SqlDbType = SqlDbType.VarChar;
            Parameters[10].ParameterName = "@Param_cidadeClienteC";
            if (( FieldInfo.cidadeClienteC == null ) || ( FieldInfo.cidadeClienteC == string.Empty ))
            { Parameters[10].Value = DBNull.Value; }
            else
            { Parameters[10].Value = FieldInfo.cidadeClienteC; }
            Parameters[10].Size = 255;

            //Field estadoClienteA
            Parameters[11].SqlDbType = SqlDbType.VarChar;
            Parameters[11].ParameterName = "@Param_estadoClienteA";
            if (( FieldInfo.estadoClienteA == null ) || ( FieldInfo.estadoClienteA == string.Empty ))
            { Parameters[11].Value = DBNull.Value; }
            else
            { Parameters[11].Value = FieldInfo.estadoClienteA; }
            Parameters[11].Size = 2;

            //Field estadoClienteB
            Parameters[12].SqlDbType = SqlDbType.VarChar;
            Parameters[12].ParameterName = "@Param_estadoClienteB";
            if (( FieldInfo.estadoClienteB == null ) || ( FieldInfo.estadoClienteB == string.Empty ))
            { Parameters[12].Value = DBNull.Value; }
            else
            { Parameters[12].Value = FieldInfo.estadoClienteB; }
            Parameters[12].Size = 2;

            //Field estadoClienteC
            Parameters[13].SqlDbType = SqlDbType.VarChar;
            Parameters[13].ParameterName = "@Param_estadoClienteC";
            if (( FieldInfo.estadoClienteC == null ) || ( FieldInfo.estadoClienteC == string.Empty ))
            { Parameters[13].Value = DBNull.Value; }
            else
            { Parameters[13].Value = FieldInfo.estadoClienteC; }
            Parameters[13].Size = 2;

            //Field cepClienteA
            Parameters[14].SqlDbType = SqlDbType.VarChar;
            Parameters[14].ParameterName = "@Param_cepClienteA";
            if (( FieldInfo.cepClienteA == null ) || ( FieldInfo.cepClienteA == string.Empty ))
            { Parameters[14].Value = DBNull.Value; }
            else
            { Parameters[14].Value = FieldInfo.cepClienteA; }
            Parameters[14].Size = 9;

            //Field cepClienteB
            Parameters[15].SqlDbType = SqlDbType.VarChar;
            Parameters[15].ParameterName = "@Param_cepClienteB";
            if (( FieldInfo.cepClienteB == null ) || ( FieldInfo.cepClienteB == string.Empty ))
            { Parameters[15].Value = DBNull.Value; }
            else
            { Parameters[15].Value = FieldInfo.cepClienteB; }
            Parameters[15].Size = 9;

            //Field cepClienteC
            Parameters[16].SqlDbType = SqlDbType.VarChar;
            Parameters[16].ParameterName = "@Param_cepClienteC";
            if (( FieldInfo.cepClienteC == null ) || ( FieldInfo.cepClienteC == string.Empty ))
            { Parameters[16].Value = DBNull.Value; }
            else
            { Parameters[16].Value = FieldInfo.cepClienteC; }
            Parameters[16].Size = 9;

            //Field telefoneClienteA
            Parameters[17].SqlDbType = SqlDbType.VarChar;
            Parameters[17].ParameterName = "@Param_telefoneClienteA";
            if (( FieldInfo.telefoneClienteA == null ) || ( FieldInfo.telefoneClienteA == string.Empty ))
            { Parameters[17].Value = DBNull.Value; }
            else
            { Parameters[17].Value = FieldInfo.telefoneClienteA; }
            Parameters[17].Size = 50;

            //Field telefoneClienteB
            Parameters[18].SqlDbType = SqlDbType.VarChar;
            Parameters[18].ParameterName = "@Param_telefoneClienteB";
            if (( FieldInfo.telefoneClienteB == null ) || ( FieldInfo.telefoneClienteB == string.Empty ))
            { Parameters[18].Value = DBNull.Value; }
            else
            { Parameters[18].Value = FieldInfo.telefoneClienteB; }
            Parameters[18].Size = 50;

            //Field telefoneClienteC
            Parameters[19].SqlDbType = SqlDbType.VarChar;
            Parameters[19].ParameterName = "@Param_telefoneClienteC";
            if (( FieldInfo.telefoneClienteC == null ) || ( FieldInfo.telefoneClienteC == string.Empty ))
            { Parameters[19].Value = DBNull.Value; }
            else
            { Parameters[19].Value = FieldInfo.telefoneClienteC; }
            Parameters[19].Size = 50;

            //Field telefoneClienteD
            Parameters[20].SqlDbType = SqlDbType.VarChar;
            Parameters[20].ParameterName = "@Param_telefoneClienteD";
            if (( FieldInfo.telefoneClienteD == null ) || ( FieldInfo.telefoneClienteD == string.Empty ))
            { Parameters[20].Value = DBNull.Value; }
            else
            { Parameters[20].Value = FieldInfo.telefoneClienteD; }
            Parameters[20].Size = 50;

            //Field celularClienteA
            Parameters[21].SqlDbType = SqlDbType.VarChar;
            Parameters[21].ParameterName = "@Param_celularClienteA";
            if (( FieldInfo.celularClienteA == null ) || ( FieldInfo.celularClienteA == string.Empty ))
            { Parameters[21].Value = DBNull.Value; }
            else
            { Parameters[21].Value = FieldInfo.celularClienteA; }
            Parameters[21].Size = 50;

            //Field celularClienteB
            Parameters[22].SqlDbType = SqlDbType.VarChar;
            Parameters[22].ParameterName = "@Param_celularClienteB";
            if (( FieldInfo.celularClienteB == null ) || ( FieldInfo.celularClienteB == string.Empty ))
            { Parameters[22].Value = DBNull.Value; }
            else
            { Parameters[22].Value = FieldInfo.celularClienteB; }
            Parameters[22].Size = 50;

            //Field celularClienteC
            Parameters[23].SqlDbType = SqlDbType.VarChar;
            Parameters[23].ParameterName = "@Param_celularClienteC";
            if (( FieldInfo.celularClienteC == null ) || ( FieldInfo.celularClienteC == string.Empty ))
            { Parameters[23].Value = DBNull.Value; }
            else
            { Parameters[23].Value = FieldInfo.celularClienteC; }
            Parameters[23].Size = 50;

            //Field complementoCliente
            Parameters[24].SqlDbType = SqlDbType.VarChar;
            Parameters[24].ParameterName = "@Param_complementoCliente";
            if (( FieldInfo.complementoCliente == null ) || ( FieldInfo.complementoCliente == string.Empty ))
            { Parameters[24].Value = DBNull.Value; }
            else
            { Parameters[24].Value = FieldInfo.complementoCliente; }
            Parameters[24].Size = 100;

            //Field dataNascimentoCliente
            Parameters[25].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[25].ParameterName = "@Param_dataNascimentoCliente";
            if ( FieldInfo.dataNascimentoCliente == DateTime.MinValue )
            { Parameters[25].Value = DBNull.Value; }
            else
            { Parameters[25].Value = FieldInfo.dataNascimentoCliente; }

            //Field emailClienteA
            Parameters[26].SqlDbType = SqlDbType.VarChar;
            Parameters[26].ParameterName = "@Param_emailClienteA";
            if (( FieldInfo.emailClienteA == null ) || ( FieldInfo.emailClienteA == string.Empty ))
            { Parameters[26].Value = DBNull.Value; }
            else
            { Parameters[26].Value = FieldInfo.emailClienteA; }
            Parameters[26].Size = 255;

            //Field emailClienteB
            Parameters[27].SqlDbType = SqlDbType.VarChar;
            Parameters[27].ParameterName = "@Param_emailClienteB";
            if (( FieldInfo.emailClienteB == null ) || ( FieldInfo.emailClienteB == string.Empty ))
            { Parameters[27].Value = DBNull.Value; }
            else
            { Parameters[27].Value = FieldInfo.emailClienteB; }
            Parameters[27].Size = 255;

            //Field contatoClienteA
            Parameters[28].SqlDbType = SqlDbType.VarChar;
            Parameters[28].ParameterName = "@Param_contatoClienteA";
            if (( FieldInfo.contatoClienteA == null ) || ( FieldInfo.contatoClienteA == string.Empty ))
            { Parameters[28].Value = DBNull.Value; }
            else
            { Parameters[28].Value = FieldInfo.contatoClienteA; }
            Parameters[28].Size = 255;

            //Field contatoClienteB
            Parameters[29].SqlDbType = SqlDbType.VarChar;
            Parameters[29].ParameterName = "@Param_contatoClienteB";
            if (( FieldInfo.contatoClienteB == null ) || ( FieldInfo.contatoClienteB == string.Empty ))
            { Parameters[29].Value = DBNull.Value; }
            else
            { Parameters[29].Value = FieldInfo.contatoClienteB; }
            Parameters[29].Size = 255;

            //Field contatoClienteC
            Parameters[30].SqlDbType = SqlDbType.VarChar;
            Parameters[30].ParameterName = "@Param_contatoClienteC";
            if (( FieldInfo.contatoClienteC == null ) || ( FieldInfo.contatoClienteC == string.Empty ))
            { Parameters[30].Value = DBNull.Value; }
            else
            { Parameters[30].Value = FieldInfo.contatoClienteC; }
            Parameters[30].Size = 255;

            //Field cnpjCliente
            Parameters[31].SqlDbType = SqlDbType.VarChar;
            Parameters[31].ParameterName = "@Param_cnpjCliente";
            if (( FieldInfo.cnpjCliente == null ) || ( FieldInfo.cnpjCliente == string.Empty ))
            { Parameters[31].Value = DBNull.Value; }
            else
            { Parameters[31].Value = FieldInfo.cnpjCliente; }
            Parameters[31].Size = 50;

            //Field cpfCliente
            Parameters[32].SqlDbType = SqlDbType.VarChar;
            Parameters[32].ParameterName = "@Param_cpfCliente";
            if (( FieldInfo.cpfCliente == null ) || ( FieldInfo.cpfCliente == string.Empty ))
            { Parameters[32].Value = DBNull.Value; }
            else
            { Parameters[32].Value = FieldInfo.cpfCliente; }
            Parameters[32].Size = 50;

            //Field rgCliente
            Parameters[33].SqlDbType = SqlDbType.VarChar;
            Parameters[33].ParameterName = "@Param_rgCliente";
            if (( FieldInfo.rgCliente == null ) || ( FieldInfo.rgCliente == string.Empty ))
            { Parameters[33].Value = DBNull.Value; }
            else
            { Parameters[33].Value = FieldInfo.rgCliente; }
            Parameters[33].Size = 50;

            //Field inscEstadualCliente
            Parameters[34].SqlDbType = SqlDbType.VarChar;
            Parameters[34].ParameterName = "@Param_inscEstadualCliente";
            if (( FieldInfo.inscEstadualCliente == null ) || ( FieldInfo.inscEstadualCliente == string.Empty ))
            { Parameters[34].Value = DBNull.Value; }
            else
            { Parameters[34].Value = FieldInfo.inscEstadualCliente; }
            Parameters[34].Size = 50;

            //Field observacoesCliente
            Parameters[35].SqlDbType = SqlDbType.VarChar;
            Parameters[35].ParameterName = "@Param_observacoesCliente";
            if (( FieldInfo.observacoesCliente == null ) || ( FieldInfo.observacoesCliente == string.Empty ))
            { Parameters[35].Value = DBNull.Value; }
            else
            { Parameters[35].Value = FieldInfo.observacoesCliente; }
            Parameters[35].Size = 300;

            //Field dataCadastroCliente
            Parameters[36].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[36].ParameterName = "@Param_dataCadastroCliente";
            if ( FieldInfo.dataCadastroCliente == DateTime.MinValue )
            { Parameters[36].Value = DBNull.Value; }
            else
            { Parameters[36].Value = FieldInfo.dataCadastroCliente; }

            //Field tipoCliente
            Parameters[37].SqlDbType = SqlDbType.VarChar;
            Parameters[37].ParameterName = "@Param_tipoCliente";
            if (( FieldInfo.tipoCliente == null ) || ( FieldInfo.tipoCliente == string.Empty ))
            { Parameters[37].Value = DBNull.Value; }
            else
            { Parameters[37].Value = FieldInfo.tipoCliente; }
            Parameters[37].Size = 20;

            //Field statusCliente
            Parameters[38].SqlDbType = SqlDbType.VarChar;
            Parameters[38].ParameterName = "@Param_statusCliente";
            if (( FieldInfo.statusCliente == null ) || ( FieldInfo.statusCliente == string.Empty ))
            { Parameters[38].Value = DBNull.Value; }
            else
            { Parameters[38].Value = FieldInfo.statusCliente; }
            Parameters[38].Size = 2;

            //Field fkSubGrupoCliente
            Parameters[39].SqlDbType = SqlDbType.Int;
            Parameters[39].ParameterName = "@Param_fkSubGrupoCliente";
            Parameters[39].Value = FieldInfo.fkSubGrupoCliente;

            //Field dataUltimaCompraCliente
            Parameters[40].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[40].ParameterName = "@Param_dataUltimaCompraCliente";
            if ( FieldInfo.dataUltimaCompraCliente == DateTime.MinValue )
            { Parameters[40].Value = DBNull.Value; }
            else
            { Parameters[40].Value = FieldInfo.dataUltimaCompraCliente; }

            //Field numeroCasaCliente
            Parameters[41].SqlDbType = SqlDbType.VarChar;
            Parameters[41].ParameterName = "@Param_numeroCasaCliente";
            if (( FieldInfo.numeroCasaCliente == null ) || ( FieldInfo.numeroCasaCliente == string.Empty ))
            { Parameters[41].Value = DBNull.Value; }
            else
            { Parameters[41].Value = FieldInfo.numeroCasaCliente; }
            Parameters[41].Size = 30;

            //Field faxCliente
            Parameters[42].SqlDbType = SqlDbType.VarChar;
            Parameters[42].ParameterName = "@Param_faxCliente";
            if (( FieldInfo.faxCliente == null ) || ( FieldInfo.faxCliente == string.Empty ))
            { Parameters[42].Value = DBNull.Value; }
            else
            { Parameters[42].Value = FieldInfo.faxCliente; }
            Parameters[42].Size = 50;

            //Field dataNascimentoClienteA
            Parameters[43].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[43].ParameterName = "@Param_dataNascimentoClienteA";
            if ( FieldInfo.dataNascimentoClienteA == DateTime.MinValue )
            { Parameters[43].Value = DBNull.Value; }
            else
            { Parameters[43].Value = FieldInfo.dataNascimentoClienteA; }

            //Field dataNascimentoClienteB
            Parameters[44].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[44].ParameterName = "@Param_dataNascimentoClienteB";
            if ( FieldInfo.dataNascimentoClienteB == DateTime.MinValue )
            { Parameters[44].Value = DBNull.Value; }
            else
            { Parameters[44].Value = FieldInfo.dataNascimentoClienteB; }

            //Field dataNascimentoClienteC
            Parameters[45].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[45].ParameterName = "@Param_dataNascimentoClienteC";
            if ( FieldInfo.dataNascimentoClienteC == DateTime.MinValue )
            { Parameters[45].Value = DBNull.Value; }
            else
            { Parameters[45].Value = FieldInfo.dataNascimentoClienteC; }

            //Field emailPrincipalCliente
            Parameters[46].SqlDbType = SqlDbType.VarChar;
            Parameters[46].ParameterName = "@Param_emailPrincipalCliente";
            if (( FieldInfo.emailPrincipalCliente == null ) || ( FieldInfo.emailPrincipalCliente == string.Empty ))
            { Parameters[46].Value = DBNull.Value; }
            else
            { Parameters[46].Value = FieldInfo.emailPrincipalCliente; }
            Parameters[46].Size = 150;

            return Parameters;
        }
        #endregion





        #region IDisposable Members 

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ClienteControl() 
        { 
            Dispose(false); 
        }

        private void Dispose(bool disposing) 
        {
            if (!this.disposed)
            {
                if (disposing) 
                {
                    if (this.Conn != null)
                        if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
                }
            }

        }
        #endregion 



    }

}





//Projeto substituído ------------------------
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Text;
//
//namespace SIMLgen
//{
//
//
//    /// <summary> 
//    /// Tabela: Cliente  
//    /// Autor: DAL Creator .net 
//    /// Data de criação: 23/06/2013 15:09:16 
//    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "ClienteFields". 
//    /// </summary> 
//    public class ClienteControl : IDisposable 
//    {
//
//        #region String de conexão 
//        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
//        #endregion
//
//
//        #region Propriedade que armazena erros de execução 
//        private string _ErrorMessage;
//        public string ErrorMessage { get { return _ErrorMessage; } }
//        #endregion
//
//
//        #region Objetos de conexão 
//        SqlConnection Conn;
//        SqlCommand Cmd;
//        SqlTransaction Tran;
//        #endregion
//
//
//        #region Funcões que retornam Conexões e Transações 
//
//        public SqlTransaction GetNewTransaction(SqlConnection connIn)
//        {
//            if (connIn.State != ConnectionState.Open)
//                connIn.Open();
//            SqlTransaction TranOut = connIn.BeginTransaction();
//            return TranOut;
//        }
//
//        public SqlConnection GetNewConnection()
//        {
//            return GetNewConnection(this.StrConnetionDB);
//        }
//
//        public SqlConnection GetNewConnection(string StringConnection)
//        {
//            SqlConnection connOut = new SqlConnection(StringConnection);
//            return connOut;
//        }
//
//        #endregion
//
//
//        #region enum SQLMode 
//        /// <summary>   
//        /// Representa o procedimento que está sendo executado na tabela.
//        /// </summary>
//        public enum SQLMode
//        {                     
//            /// <summary>
//            /// Adiciona registro na tabela.
//            /// </summary>
//            Add,
//            /// <summary>
//            /// Atualiza registro na tabela.
//            /// </summary>
//            Update,
//            /// <summary>
//            /// Excluir registro na tabela
//            /// </summary>
//            Delete,
//            /// <summary>
//            /// Exclui TODOS os registros da tabela.
//            /// </summary>
//            DeleteAll,
//            /// <summary>
//            /// Seleciona um registro na tabela.
//            /// </summary>
//            Select,
//            /// <summary>
//            /// Seleciona TODOS os registros da tabela.
//            /// </summary>
//            SelectAll,
//            /// <summary>
//            /// Excluir ou seleciona um registro na tabela.
//            /// </summary>
//            SelectORDelete
//        }
//        #endregion 
//
//
//        public ClienteControl() {}
//
//
//        #region Inserindo dados na tabela 
//
//        /// <summary> 
//        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Add( ref ClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_Cliente_Add", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//                this.Tran.Commit();
//                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
//                return true;
//
//            }
//            catch (SqlException e)
//            {
//                this.Tran.Rollback();
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this.Tran.Rollback();
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//                if (this.Cmd != null)
//                  this.Cmd.Dispose();
//            }
//        }
//
//        #endregion
//
//
//        #region Inserindo dados na tabela utilizando conexão e transação externa (compartilhada) 
//
//        /// <summary> 
//        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref ClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_Cliente_Add", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
//                return true;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//        }
//
//        #endregion
//
//
//        #region Editando dados na tabela 
//
//        /// <summary> 
//        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
//        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Update( ClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_Cliente_Update", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
//                this.Tran.Commit();
//                return true;
//            }
//            catch (SqlException e)
//            {
//                this.Tran.Rollback();
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this.Tran.Rollback();
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//                if (this.Cmd != null)
//                  this.Cmd.Dispose();
//            }
//        }
//
//        #endregion
//
//
//        #region Editando dados na tabela utilizando conexão e transação externa (compartilhada) 
//
//        /// <summary> 
//        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
//        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_Cliente_Update", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
//                return true;
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//        }
//
//        #endregion
//
//
//        #region Excluindo todos os dados da tabela 
//
//        /// <summary> 
//        /// Exclui todos os registros da tabela
//        /// </summary>
//        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool DeleteAll()
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                this.Tran.Commit();
//                return true;
//            }
//            catch (SqlException e)
//            {
//                this.Tran.Rollback();
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this.Tran.Rollback();
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//                if (this.Cmd != null)
//                  this.Cmd.Dispose();
//            }
//        }
//
//        #endregion
//
//
//        #region Excluindo todos os dados da tabela utilizando conexão e transação externa (compartilhada)
//
//        /// <summary> 
//        /// Exclui todos os registros da tabela
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                return true;
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//        }
//
//        #endregion
//
//
//        #region Excluindo dados da tabela 
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( ClienteFields FieldInfo )
//        {
//            return Delete(FieldInfo.idCliente);
//        }
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="Param_idCliente">int</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete(
//                                     int Param_idCliente)
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_Cliente_Delete", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                this.Tran.Commit();
//                return true;
//            }
//            catch (SqlException e)
//            {
//                this.Tran.Rollback();
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this.Tran.Rollback();
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//                if (this.Cmd != null)
//                  this.Cmd.Dispose();
//            }
//        }
//
//        #endregion
//
//
//        #region Excluindo dados da tabela utilizando conexão e transação externa (compartilhada)
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
//        {
//            return Delete(ConnIn, TranIn, FieldInfo.idCliente);
//        }
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="Param_idCliente">int</param>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
//                                     int Param_idCliente)
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_Cliente_Delete", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                return true;
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//                return false;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//        }
//
//        #endregion
//
//
//        #region Selecionando um item da tabela 
//
//        /// <summary> 
//        /// Retorna um objeto ClienteFields através da chave primária passada como parâmetro
//        /// </summary>
//        /// <param name="Param_idCliente">int</param>
//        /// <returns>Objeto ClienteFields</returns> 
//        public ClienteFields GetItem(
//                                     int Param_idCliente)
//        {
//            ClienteFields infoFields = new ClienteFields();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_Select", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            if (dr.Read())
//                            {
//                               infoFields = GetDataFromReader( dr );
//                            }
//                        }
//                    }
//                 }
//
//                 return infoFields;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//        #region Selecionando todos os dados da tabela 
//
//        /// <summary> 
//        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto ClienteFields.
//        /// </summary>
//        /// <returns>List de objetos ClienteFields</returns> 
//        public List<ClienteFields> GetAll()
//        {
//            List<ClienteFields> arrayInfo = new List<ClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_GetAll", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                           if (!dr.HasRows) return null;
//                           while (dr.Read())
//                           {
//                              arrayInfo.Add(GetDataFromReader( dr ));
//                           }
//                        }
//                    }
//                }
//
//                return arrayInfo;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//        #region Contando os dados da tabela 
//
//        /// <summary> 
//        /// Retorna o total de registros contidos na tabela
//        /// </summary>
//        /// <returns>int</returns> 
//        public int CountAll()
//        {
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_CountAll", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Connection.Open();
//                        object CountRegs = this.Cmd.ExecuteScalar();
//                        if (CountRegs == null)
//                        { return 0; }
//                        else
//                        { return (int)CountRegs; }
//                    }
//                }
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return 0;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return 0;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//        #region Selecionando dados da tabela através do campo "cnpjCliente" 
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo cnpjCliente.
//        /// </summary>
//        /// <param name="Param_cnpjCliente">string</param>
//        /// <returns>ClienteFields</returns> 
//        public ClienteFields FindBycnpjCliente(
//                               string Param_cnpjCliente )
//        {
//            ClienteFields infoFields = new ClienteFields();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBycnpjCliente", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_cnpjCliente", SqlDbType.VarChar, 50)).Value = Param_cnpjCliente;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            if (dr.Read())
//                            {
//                               infoFields = GetDataFromReader( dr );
//                            }
//                        }
//                    }
//                }
//
//                return infoFields;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//
//        #region Selecionando dados da tabela através do campo "rgCliente" 
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo rgCliente.
//        /// </summary>
//        /// <param name="Param_rgCliente">string</param>
//        /// <returns>ClienteFields</returns> 
//        public ClienteFields FindByrgCliente(
//                               string Param_rgCliente )
//        {
//            ClienteFields infoFields = new ClienteFields();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindByrgCliente", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_rgCliente", SqlDbType.VarChar, 50)).Value = Param_rgCliente;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            if (dr.Read())
//                            {
//                               infoFields = GetDataFromReader( dr );
//                            }
//                        }
//                    }
//                }
//
//                return infoFields;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//
//        #region Selecionando dados da tabela através do campo "statusCliente" 
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo statusCliente.
//        /// </summary>
//        /// <param name="Param_statusCliente">string</param>
//        /// <returns>List ClienteFields</returns> 
//        public List<ClienteFields> FindBystatusCliente(
//                               string Param_statusCliente )
//        {
//            List<ClienteFields> arrayList = new List<ClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBystatusCliente", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_statusCliente", SqlDbType.VarChar, 2)).Value = Param_statusCliente;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            while (dr.Read())
//                            {
//                               arrayList.Add(GetDataFromReader( dr ));
//                            }
//                        }
//                    }
//                }
//
//                return arrayList;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//
//        #region Selecionando dados da tabela através do campo "fkSubGrupoCliente" 
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkSubGrupoCliente.
//        /// </summary>
//        /// <param name="Param_fkSubGrupoCliente">int</param>
//        /// <returns>List ClienteFields</returns> 
//        public List<ClienteFields> FindByfkSubGrupoCliente(
//                               int Param_fkSubGrupoCliente )
//        {
//            List<ClienteFields> arrayList = new List<ClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindByfkSubGrupoCliente", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkSubGrupoCliente", SqlDbType.Int)).Value = Param_fkSubGrupoCliente;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            while (dr.Read())
//                            {
//                               arrayList.Add(GetDataFromReader( dr ));
//                            }
//                        }
//                    }
//                }
//
//                return arrayList;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//        #region Selecionando dados da tabela através do campo "telefoneClienteA"
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteA.
//        /// </summary>
//        /// <param name="Param_telefoneClienteA">string</param>
//        /// <returns>List ClienteFields</returns> 
//        public List<ClienteFields> FindBytelefoneClienteA(
//                               string Param_telefoneClienteA)
//        {
//            List<ClienteFields> arrayList = new List<ClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteA", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteA", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteA;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            while (dr.Read())
//                            {
//                                arrayList.Add(GetDataFromReader(dr));
//                            }
//                        }
//                    }
//                }
//
//                return arrayList;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//
//        #region Selecionando dados da tabela através do campo "telefoneClienteB"
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteB.
//        /// </summary>
//        /// <param name="Param_telefoneClienteB">string</param>
//        /// <returns>List ClienteFields</returns> 
//        public List<ClienteFields> FindBytelefoneClienteB(
//                               string Param_telefoneClienteB)
//        {
//            List<ClienteFields> arrayList = new List<ClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteB", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteB", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteB;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            while (dr.Read())
//                            {
//                                arrayList.Add(GetDataFromReader(dr));
//                            }
//                        }
//                    }
//                }
//
//                return arrayList;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//        #region Selecionando dados da tabela através do campo "telefoneClienteC"
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteB.
//        /// </summary>
//        /// <param name="Param_telefoneClienteB">string</param>
//        /// <returns>List ClienteFields</returns> 
//        public List<ClienteFields> FindBytelefoneClienteC(
//                               string Param_telefoneClienteC)
//        {
//            List<ClienteFields> arrayList = new List<ClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteC", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteC", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteC;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            while (dr.Read())
//                            {
//                                arrayList.Add(GetDataFromReader(dr));
//                            }
//                        }
//                    }
//                }
//
//                return arrayList;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//
//        #region Selecionando dados da tabela através do campo "telefoneClienteD"
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo telefoneClienteD.
//        /// </summary>
//        /// <param name="Param_telefoneClienteD">string</param>
//        /// <returns>List ClienteFields</returns> 
//        public List<ClienteFields> FindBytelefoneClienteD(
//                               string Param_telefoneClienteD)
//        {
//            List<ClienteFields> arrayList = new List<ClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBytelefoneClienteD", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_telefoneClienteD", SqlDbType.VarChar, 50)).Value = Param_telefoneClienteD;
//                        this.Cmd.Connection.Open();
//                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//                        {
//                            if (!dr.HasRows) return null;
//                            while (dr.Read())
//                            {
//                                arrayList.Add(GetDataFromReader(dr));
//                            }
//                        }
//                    }
//                }
//
//                return arrayList;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        #endregion
//
//        #region Função GetDataFromReader
//
//        /// <summary> 
//        /// Retorna um objeto ClienteFields preenchido com os valores dos campos do SqlDataReader
//        /// </summary>
//        /// <param name="dr">SqlDataReader - Preenche o objeto ClienteFields </param>
//        /// <returns>ClienteFields</returns>
//        private ClienteFields GetDataFromReader( SqlDataReader dr )
//        {
//            ClienteFields infoFields = new ClienteFields();
//
//            if (!dr.IsDBNull(0))
//            { infoFields.idCliente = dr.GetInt32(0); }
//            else
//            { infoFields.idCliente = 0; }
//
//
//
//            if (!dr.IsDBNull(1))
//            { infoFields.nomeCliente = dr.GetString(1); }
//            else
//            { infoFields.nomeCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(2))
//            { infoFields.enderecoClienteA = dr.GetString(2); }
//            else
//            { infoFields.enderecoClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(3))
//            { infoFields.enderecoClienteB = dr.GetString(3); }
//            else
//            { infoFields.enderecoClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(4))
//            { infoFields.enderecoClienteC = dr.GetString(4); }
//            else
//            { infoFields.enderecoClienteC = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(5))
//            { infoFields.bairroClienteA = dr.GetString(5); }
//            else
//            { infoFields.bairroClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(6))
//            { infoFields.bairroClienteB = dr.GetString(6); }
//            else
//            { infoFields.bairroClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(7))
//            { infoFields.bairroClientec = dr.GetString(7); }
//            else
//            { infoFields.bairroClientec = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(8))
//            { infoFields.cidadeClienteA = dr.GetString(8); }
//            else
//            { infoFields.cidadeClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(9))
//            { infoFields.cidadeClienteB = dr.GetString(9); }
//            else
//            { infoFields.cidadeClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(10))
//            { infoFields.cidadeClienteC = dr.GetString(10); }
//            else
//            { infoFields.cidadeClienteC = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(11))
//            { infoFields.estadoClienteA = dr.GetString(11); }
//            else
//            { infoFields.estadoClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(12))
//            { infoFields.estadoClienteB = dr.GetString(12); }
//            else
//            { infoFields.estadoClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(13))
//            { infoFields.estadoClienteC = dr.GetString(13); }
//            else
//            { infoFields.estadoClienteC = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(14))
//            { infoFields.cepClienteA = dr.GetString(14); }
//            else
//            { infoFields.cepClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(15))
//            { infoFields.cepClienteB = dr.GetString(15); }
//            else
//            { infoFields.cepClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(16))
//            { infoFields.cepClienteC = dr.GetString(16); }
//            else
//            { infoFields.cepClienteC = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(17))
//            { infoFields.telefoneClienteA = dr.GetString(17); }
//            else
//            { infoFields.telefoneClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(18))
//            { infoFields.telefoneClienteB = dr.GetString(18); }
//            else
//            { infoFields.telefoneClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(19))
//            { infoFields.telefoneClienteC = dr.GetString(19); }
//            else
//            { infoFields.telefoneClienteC = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(20))
//            { infoFields.telefoneClienteD = dr.GetString(20); }
//            else
//            { infoFields.telefoneClienteD = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(21))
//            { infoFields.celularClienteA = dr.GetString(21); }
//            else
//            { infoFields.celularClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(22))
//            { infoFields.celularClienteB = dr.GetString(22); }
//            else
//            { infoFields.celularClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(23))
//            { infoFields.celularClienteC = dr.GetString(23); }
//            else
//            { infoFields.celularClienteC = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(24))
//            { infoFields.complementoCliente = dr.GetString(24); }
//            else
//            { infoFields.complementoCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(25))
//            { infoFields.dataNascimentoCliente = dr.GetDateTime(25); }
//            else
//            { infoFields.dataNascimentoCliente = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(26))
//            { infoFields.emailClienteA = dr.GetString(26); }
//            else
//            { infoFields.emailClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(27))
//            { infoFields.emailClienteB = dr.GetString(27); }
//            else
//            { infoFields.emailClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(28))
//            { infoFields.contatoClienteA = dr.GetString(28); }
//            else
//            { infoFields.contatoClienteA = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(29))
//            { infoFields.contatoClienteB = dr.GetString(29); }
//            else
//            { infoFields.contatoClienteB = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(30))
//            { infoFields.contatoClienteC = dr.GetString(30); }
//            else
//            { infoFields.contatoClienteC = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(31))
//            { infoFields.cnpjCliente = dr.GetString(31); }
//            else
//            { infoFields.cnpjCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(32))
//            { infoFields.cpfCliente = dr.GetString(32); }
//            else
//            { infoFields.cpfCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(33))
//            { infoFields.rgCliente = dr.GetString(33); }
//            else
//            { infoFields.rgCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(34))
//            { infoFields.inscEstadualCliente = dr.GetString(34); }
//            else
//            { infoFields.inscEstadualCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(35))
//            { infoFields.observacoesCliente = dr.GetString(35); }
//            else
//            { infoFields.observacoesCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(36))
//            { infoFields.dataCadastroCliente = dr.GetDateTime(36); }
//            else
//            { infoFields.dataCadastroCliente = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(37))
//            { infoFields.tipoCliente = dr.GetString(37); }
//            else
//            { infoFields.tipoCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(38))
//            { infoFields.statusCliente = dr.GetString(38); }
//            else
//            { infoFields.statusCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(39))
//            { infoFields.fkSubGrupoCliente = dr.GetInt32(39); }
//            else
//            { infoFields.fkSubGrupoCliente = 0; }
//
//
//
//            if (!dr.IsDBNull(40))
//            { infoFields.dataUltimaCompraCliente = dr.GetDateTime(40); }
//            else
//            { infoFields.dataUltimaCompraCliente = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(41))
//            { infoFields.numeroCasaCliente = dr.GetString(41); }
//            else
//            { infoFields.numeroCasaCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(42))
//            { infoFields.faxCliente = dr.GetString(42); }
//            else
//            { infoFields.faxCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(43))
//            { infoFields.dataNascimentoClienteA = dr.GetDateTime(43); }
//            else
//            { infoFields.dataNascimentoClienteA = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(44))
//            { infoFields.dataNascimentoClienteB = dr.GetDateTime(44); }
//            else
//            { infoFields.dataNascimentoClienteB = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(45))
//            { infoFields.dataNascimentoClienteC = dr.GetDateTime(45); }
//            else
//            { infoFields.dataNascimentoClienteC = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(46))
//            { infoFields.emailPrincipalCliente = dr.GetString(46); }
//            else
//            { infoFields.emailPrincipalCliente = string.Empty; }
//
//
//            return infoFields;
//        }
//        #endregion
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//        #region Função GetAllParameters
//
//        /// <summary> 
//        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto ClienteFields</param>
//        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
//        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
//        private SqlParameter[] GetAllParameters( ClienteFields FieldInfo, SQLMode Modo )
//        {
//            SqlParameter[] Parameters;
//
//            switch (Modo)
//            {
//                case SQLMode.Add:
//                    Parameters = new SqlParameter[47];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idCliente
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].Direction = ParameterDirection.Output;
//                    Parameters[0].ParameterName = "@Param_idCliente";
//                    Parameters[0].Value = DBNull.Value;
//
//                    break;
//
//                case SQLMode.Update:
//                    Parameters = new SqlParameter[47];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idCliente
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].ParameterName = "@Param_idCliente";
//                    Parameters[0].Value = FieldInfo.idCliente;
//
//                    break;
//
//                case SQLMode.SelectORDelete:
//                    Parameters = new SqlParameter[1];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idCliente
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].ParameterName = "@Param_idCliente";
//                    Parameters[0].Value = FieldInfo.idCliente;
//
//                    return Parameters;
//
//                default:
//                    Parameters = new SqlParameter[47];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    break;
//            }
//
//            //Field nomeCliente
//            Parameters[1].SqlDbType = SqlDbType.VarChar;
//            Parameters[1].ParameterName = "@Param_nomeCliente";
//            if (( FieldInfo.nomeCliente == null ) || ( FieldInfo.nomeCliente == string.Empty ))
//            { Parameters[1].Value = DBNull.Value; }
//            else
//            { Parameters[1].Value = FieldInfo.nomeCliente; }
//            Parameters[1].Size = 255;
//
//            //Field enderecoClienteA
//            Parameters[2].SqlDbType = SqlDbType.VarChar;
//            Parameters[2].ParameterName = "@Param_enderecoClienteA";
//            if (( FieldInfo.enderecoClienteA == null ) || ( FieldInfo.enderecoClienteA == string.Empty ))
//            { Parameters[2].Value = DBNull.Value; }
//            else
//            { Parameters[2].Value = FieldInfo.enderecoClienteA; }
//            Parameters[2].Size = 255;
//
//            //Field enderecoClienteB
//            Parameters[3].SqlDbType = SqlDbType.VarChar;
//            Parameters[3].ParameterName = "@Param_enderecoClienteB";
//            if (( FieldInfo.enderecoClienteB == null ) || ( FieldInfo.enderecoClienteB == string.Empty ))
//            { Parameters[3].Value = DBNull.Value; }
//            else
//            { Parameters[3].Value = FieldInfo.enderecoClienteB; }
//            Parameters[3].Size = 255;
//
//            //Field enderecoClienteC
//            Parameters[4].SqlDbType = SqlDbType.VarChar;
//            Parameters[4].ParameterName = "@Param_enderecoClienteC";
//            if (( FieldInfo.enderecoClienteC == null ) || ( FieldInfo.enderecoClienteC == string.Empty ))
//            { Parameters[4].Value = DBNull.Value; }
//            else
//            { Parameters[4].Value = FieldInfo.enderecoClienteC; }
//            Parameters[4].Size = 255;
//
//            //Field bairroClienteA
//            Parameters[5].SqlDbType = SqlDbType.VarChar;
//            Parameters[5].ParameterName = "@Param_bairroClienteA";
//            if (( FieldInfo.bairroClienteA == null ) || ( FieldInfo.bairroClienteA == string.Empty ))
//            { Parameters[5].Value = DBNull.Value; }
//            else
//            { Parameters[5].Value = FieldInfo.bairroClienteA; }
//            Parameters[5].Size = 255;
//
//            //Field bairroClienteB
//            Parameters[6].SqlDbType = SqlDbType.VarChar;
//            Parameters[6].ParameterName = "@Param_bairroClienteB";
//            if (( FieldInfo.bairroClienteB == null ) || ( FieldInfo.bairroClienteB == string.Empty ))
//            { Parameters[6].Value = DBNull.Value; }
//            else
//            { Parameters[6].Value = FieldInfo.bairroClienteB; }
//            Parameters[6].Size = 255;
//
//            //Field bairroClientec
//            Parameters[7].SqlDbType = SqlDbType.VarChar;
//            Parameters[7].ParameterName = "@Param_bairroClientec";
//            if (( FieldInfo.bairroClientec == null ) || ( FieldInfo.bairroClientec == string.Empty ))
//            { Parameters[7].Value = DBNull.Value; }
//            else
//            { Parameters[7].Value = FieldInfo.bairroClientec; }
//            Parameters[7].Size = 255;
//
//            //Field cidadeClienteA
//            Parameters[8].SqlDbType = SqlDbType.VarChar;
//            Parameters[8].ParameterName = "@Param_cidadeClienteA";
//            if (( FieldInfo.cidadeClienteA == null ) || ( FieldInfo.cidadeClienteA == string.Empty ))
//            { Parameters[8].Value = DBNull.Value; }
//            else
//            { Parameters[8].Value = FieldInfo.cidadeClienteA; }
//            Parameters[8].Size = 255;
//
//            //Field cidadeClienteB
//            Parameters[9].SqlDbType = SqlDbType.VarChar;
//            Parameters[9].ParameterName = "@Param_cidadeClienteB";
//            if (( FieldInfo.cidadeClienteB == null ) || ( FieldInfo.cidadeClienteB == string.Empty ))
//            { Parameters[9].Value = DBNull.Value; }
//            else
//            { Parameters[9].Value = FieldInfo.cidadeClienteB; }
//            Parameters[9].Size = 255;
//
//            //Field cidadeClienteC
//            Parameters[10].SqlDbType = SqlDbType.VarChar;
//            Parameters[10].ParameterName = "@Param_cidadeClienteC";
//            if (( FieldInfo.cidadeClienteC == null ) || ( FieldInfo.cidadeClienteC == string.Empty ))
//            { Parameters[10].Value = DBNull.Value; }
//            else
//            { Parameters[10].Value = FieldInfo.cidadeClienteC; }
//            Parameters[10].Size = 255;
//
//            //Field estadoClienteA
//            Parameters[11].SqlDbType = SqlDbType.VarChar;
//            Parameters[11].ParameterName = "@Param_estadoClienteA";
//            if (( FieldInfo.estadoClienteA == null ) || ( FieldInfo.estadoClienteA == string.Empty ))
//            { Parameters[11].Value = DBNull.Value; }
//            else
//            { Parameters[11].Value = FieldInfo.estadoClienteA; }
//            Parameters[11].Size = 2;
//
//            //Field estadoClienteB
//            Parameters[12].SqlDbType = SqlDbType.VarChar;
//            Parameters[12].ParameterName = "@Param_estadoClienteB";
//            if (( FieldInfo.estadoClienteB == null ) || ( FieldInfo.estadoClienteB == string.Empty ))
//            { Parameters[12].Value = DBNull.Value; }
//            else
//            { Parameters[12].Value = FieldInfo.estadoClienteB; }
//            Parameters[12].Size = 2;
//
//            //Field estadoClienteC
//            Parameters[13].SqlDbType = SqlDbType.VarChar;
//            Parameters[13].ParameterName = "@Param_estadoClienteC";
//            if (( FieldInfo.estadoClienteC == null ) || ( FieldInfo.estadoClienteC == string.Empty ))
//            { Parameters[13].Value = DBNull.Value; }
//            else
//            { Parameters[13].Value = FieldInfo.estadoClienteC; }
//            Parameters[13].Size = 2;
//
//            //Field cepClienteA
//            Parameters[14].SqlDbType = SqlDbType.VarChar;
//            Parameters[14].ParameterName = "@Param_cepClienteA";
//            if (( FieldInfo.cepClienteA == null ) || ( FieldInfo.cepClienteA == string.Empty ))
//            { Parameters[14].Value = DBNull.Value; }
//            else
//            { Parameters[14].Value = FieldInfo.cepClienteA; }
//            Parameters[14].Size = 9;
//
//            //Field cepClienteB
//            Parameters[15].SqlDbType = SqlDbType.VarChar;
//            Parameters[15].ParameterName = "@Param_cepClienteB";
//            if (( FieldInfo.cepClienteB == null ) || ( FieldInfo.cepClienteB == string.Empty ))
//            { Parameters[15].Value = DBNull.Value; }
//            else
//            { Parameters[15].Value = FieldInfo.cepClienteB; }
//            Parameters[15].Size = 9;
//
//            //Field cepClienteC
//            Parameters[16].SqlDbType = SqlDbType.VarChar;
//            Parameters[16].ParameterName = "@Param_cepClienteC";
//            if (( FieldInfo.cepClienteC == null ) || ( FieldInfo.cepClienteC == string.Empty ))
//            { Parameters[16].Value = DBNull.Value; }
//            else
//            { Parameters[16].Value = FieldInfo.cepClienteC; }
//            Parameters[16].Size = 9;
//
//            //Field telefoneClienteA
//            Parameters[17].SqlDbType = SqlDbType.VarChar;
//            Parameters[17].ParameterName = "@Param_telefoneClienteA";
//            if (( FieldInfo.telefoneClienteA == null ) || ( FieldInfo.telefoneClienteA == string.Empty ))
//            { Parameters[17].Value = DBNull.Value; }
//            else
//            { Parameters[17].Value = FieldInfo.telefoneClienteA; }
//            Parameters[17].Size = 50;
//
//            //Field telefoneClienteB
//            Parameters[18].SqlDbType = SqlDbType.VarChar;
//            Parameters[18].ParameterName = "@Param_telefoneClienteB";
//            if (( FieldInfo.telefoneClienteB == null ) || ( FieldInfo.telefoneClienteB == string.Empty ))
//            { Parameters[18].Value = DBNull.Value; }
//            else
//            { Parameters[18].Value = FieldInfo.telefoneClienteB; }
//            Parameters[18].Size = 50;
//
//            //Field telefoneClienteC
//            Parameters[19].SqlDbType = SqlDbType.VarChar;
//            Parameters[19].ParameterName = "@Param_telefoneClienteC";
//            if (( FieldInfo.telefoneClienteC == null ) || ( FieldInfo.telefoneClienteC == string.Empty ))
//            { Parameters[19].Value = DBNull.Value; }
//            else
//            { Parameters[19].Value = FieldInfo.telefoneClienteC; }
//            Parameters[19].Size = 50;
//
//            //Field telefoneClienteD
//            Parameters[20].SqlDbType = SqlDbType.VarChar;
//            Parameters[20].ParameterName = "@Param_telefoneClienteD";
//            if (( FieldInfo.telefoneClienteD == null ) || ( FieldInfo.telefoneClienteD == string.Empty ))
//            { Parameters[20].Value = DBNull.Value; }
//            else
//            { Parameters[20].Value = FieldInfo.telefoneClienteD; }
//            Parameters[20].Size = 50;
//
//            //Field celularClienteA
//            Parameters[21].SqlDbType = SqlDbType.VarChar;
//            Parameters[21].ParameterName = "@Param_celularClienteA";
//            if (( FieldInfo.celularClienteA == null ) || ( FieldInfo.celularClienteA == string.Empty ))
//            { Parameters[21].Value = DBNull.Value; }
//            else
//            { Parameters[21].Value = FieldInfo.celularClienteA; }
//            Parameters[21].Size = 50;
//
//            //Field celularClienteB
//            Parameters[22].SqlDbType = SqlDbType.VarChar;
//            Parameters[22].ParameterName = "@Param_celularClienteB";
//            if (( FieldInfo.celularClienteB == null ) || ( FieldInfo.celularClienteB == string.Empty ))
//            { Parameters[22].Value = DBNull.Value; }
//            else
//            { Parameters[22].Value = FieldInfo.celularClienteB; }
//            Parameters[22].Size = 50;
//
//            //Field celularClienteC
//            Parameters[23].SqlDbType = SqlDbType.VarChar;
//            Parameters[23].ParameterName = "@Param_celularClienteC";
//            if (( FieldInfo.celularClienteC == null ) || ( FieldInfo.celularClienteC == string.Empty ))
//            { Parameters[23].Value = DBNull.Value; }
//            else
//            { Parameters[23].Value = FieldInfo.celularClienteC; }
//            Parameters[23].Size = 50;
//
//            //Field complementoCliente
//            Parameters[24].SqlDbType = SqlDbType.VarChar;
//            Parameters[24].ParameterName = "@Param_complementoCliente";
//            if (( FieldInfo.complementoCliente == null ) || ( FieldInfo.complementoCliente == string.Empty ))
//            { Parameters[24].Value = DBNull.Value; }
//            else
//            { Parameters[24].Value = FieldInfo.complementoCliente; }
//            Parameters[24].Size = 100;
//
//            //Field dataNascimentoCliente
//            Parameters[25].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[25].ParameterName = "@Param_dataNascimentoCliente";
//            if ( FieldInfo.dataNascimentoCliente == DateTime.MinValue )
//            { Parameters[25].Value = DBNull.Value; }
//            else
//            { Parameters[25].Value = FieldInfo.dataNascimentoCliente; }
//
//            //Field emailClienteA
//            Parameters[26].SqlDbType = SqlDbType.VarChar;
//            Parameters[26].ParameterName = "@Param_emailClienteA";
//            if (( FieldInfo.emailClienteA == null ) || ( FieldInfo.emailClienteA == string.Empty ))
//            { Parameters[26].Value = DBNull.Value; }
//            else
//            { Parameters[26].Value = FieldInfo.emailClienteA; }
//            Parameters[26].Size = 255;
//
//            //Field emailClienteB
//            Parameters[27].SqlDbType = SqlDbType.VarChar;
//            Parameters[27].ParameterName = "@Param_emailClienteB";
//            if (( FieldInfo.emailClienteB == null ) || ( FieldInfo.emailClienteB == string.Empty ))
//            { Parameters[27].Value = DBNull.Value; }
//            else
//            { Parameters[27].Value = FieldInfo.emailClienteB; }
//            Parameters[27].Size = 255;
//
//            //Field contatoClienteA
//            Parameters[28].SqlDbType = SqlDbType.VarChar;
//            Parameters[28].ParameterName = "@Param_contatoClienteA";
//            if (( FieldInfo.contatoClienteA == null ) || ( FieldInfo.contatoClienteA == string.Empty ))
//            { Parameters[28].Value = DBNull.Value; }
//            else
//            { Parameters[28].Value = FieldInfo.contatoClienteA; }
//            Parameters[28].Size = 255;
//
//            //Field contatoClienteB
//            Parameters[29].SqlDbType = SqlDbType.VarChar;
//            Parameters[29].ParameterName = "@Param_contatoClienteB";
//            if (( FieldInfo.contatoClienteB == null ) || ( FieldInfo.contatoClienteB == string.Empty ))
//            { Parameters[29].Value = DBNull.Value; }
//            else
//            { Parameters[29].Value = FieldInfo.contatoClienteB; }
//            Parameters[29].Size = 255;
//
//            //Field contatoClienteC
//            Parameters[30].SqlDbType = SqlDbType.VarChar;
//            Parameters[30].ParameterName = "@Param_contatoClienteC";
//            if (( FieldInfo.contatoClienteC == null ) || ( FieldInfo.contatoClienteC == string.Empty ))
//            { Parameters[30].Value = DBNull.Value; }
//            else
//            { Parameters[30].Value = FieldInfo.contatoClienteC; }
//            Parameters[30].Size = 255;
//
//            //Field cnpjCliente
//            Parameters[31].SqlDbType = SqlDbType.VarChar;
//            Parameters[31].ParameterName = "@Param_cnpjCliente";
//            if (( FieldInfo.cnpjCliente == null ) || ( FieldInfo.cnpjCliente == string.Empty ))
//            { Parameters[31].Value = DBNull.Value; }
//            else
//            { Parameters[31].Value = FieldInfo.cnpjCliente; }
//            Parameters[31].Size = 50;
//
//            //Field cpfCliente
//            Parameters[32].SqlDbType = SqlDbType.VarChar;
//            Parameters[32].ParameterName = "@Param_cpfCliente";
//            if (( FieldInfo.cpfCliente == null ) || ( FieldInfo.cpfCliente == string.Empty ))
//            { Parameters[32].Value = DBNull.Value; }
//            else
//            { Parameters[32].Value = FieldInfo.cpfCliente; }
//            Parameters[32].Size = 50;
//
//            //Field rgCliente
//            Parameters[33].SqlDbType = SqlDbType.VarChar;
//            Parameters[33].ParameterName = "@Param_rgCliente";
//            if (( FieldInfo.rgCliente == null ) || ( FieldInfo.rgCliente == string.Empty ))
//            { Parameters[33].Value = DBNull.Value; }
//            else
//            { Parameters[33].Value = FieldInfo.rgCliente; }
//            Parameters[33].Size = 50;
//
//            //Field inscEstadualCliente
//            Parameters[34].SqlDbType = SqlDbType.VarChar;
//            Parameters[34].ParameterName = "@Param_inscEstadualCliente";
//            if (( FieldInfo.inscEstadualCliente == null ) || ( FieldInfo.inscEstadualCliente == string.Empty ))
//            { Parameters[34].Value = DBNull.Value; }
//            else
//            { Parameters[34].Value = FieldInfo.inscEstadualCliente; }
//            Parameters[34].Size = 50;
//
//            //Field observacoesCliente
//            Parameters[35].SqlDbType = SqlDbType.VarChar;
//            Parameters[35].ParameterName = "@Param_observacoesCliente";
//            if (( FieldInfo.observacoesCliente == null ) || ( FieldInfo.observacoesCliente == string.Empty ))
//            { Parameters[35].Value = DBNull.Value; }
//            else
//            { Parameters[35].Value = FieldInfo.observacoesCliente; }
//            Parameters[35].Size = 300;
//
//            //Field dataCadastroCliente
//            Parameters[36].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[36].ParameterName = "@Param_dataCadastroCliente";
//            if ( FieldInfo.dataCadastroCliente == DateTime.MinValue )
//            { Parameters[36].Value = DBNull.Value; }
//            else
//            { Parameters[36].Value = FieldInfo.dataCadastroCliente; }
//
//            //Field tipoCliente
//            Parameters[37].SqlDbType = SqlDbType.VarChar;
//            Parameters[37].ParameterName = "@Param_tipoCliente";
//            if (( FieldInfo.tipoCliente == null ) || ( FieldInfo.tipoCliente == string.Empty ))
//            { Parameters[37].Value = DBNull.Value; }
//            else
//            { Parameters[37].Value = FieldInfo.tipoCliente; }
//            Parameters[37].Size = 20;
//
//            //Field statusCliente
//            Parameters[38].SqlDbType = SqlDbType.VarChar;
//            Parameters[38].ParameterName = "@Param_statusCliente";
//            if (( FieldInfo.statusCliente == null ) || ( FieldInfo.statusCliente == string.Empty ))
//            { Parameters[38].Value = DBNull.Value; }
//            else
//            { Parameters[38].Value = FieldInfo.statusCliente; }
//            Parameters[38].Size = 2;
//
//            //Field fkSubGrupoCliente
//            Parameters[39].SqlDbType = SqlDbType.Int;
//            Parameters[39].ParameterName = "@Param_fkSubGrupoCliente";
//            Parameters[39].Value = FieldInfo.fkSubGrupoCliente;
//
//            //Field dataUltimaCompraCliente
//            Parameters[40].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[40].ParameterName = "@Param_dataUltimaCompraCliente";
//            if ( FieldInfo.dataUltimaCompraCliente == DateTime.MinValue )
//            { Parameters[40].Value = DBNull.Value; }
//            else
//            { Parameters[40].Value = FieldInfo.dataUltimaCompraCliente; }
//
//            //Field numeroCasaCliente
//            Parameters[41].SqlDbType = SqlDbType.VarChar;
//            Parameters[41].ParameterName = "@Param_numeroCasaCliente";
//            if (( FieldInfo.numeroCasaCliente == null ) || ( FieldInfo.numeroCasaCliente == string.Empty ))
//            { Parameters[41].Value = DBNull.Value; }
//            else
//            { Parameters[41].Value = FieldInfo.numeroCasaCliente; }
//            Parameters[41].Size = 30;
//
//            //Field faxCliente
//            Parameters[42].SqlDbType = SqlDbType.VarChar;
//            Parameters[42].ParameterName = "@Param_faxCliente";
//            if (( FieldInfo.faxCliente == null ) || ( FieldInfo.faxCliente == string.Empty ))
//            { Parameters[42].Value = DBNull.Value; }
//            else
//            { Parameters[42].Value = FieldInfo.faxCliente; }
//            Parameters[42].Size = 50;
//
//            //Field dataNascimentoClienteA
//            Parameters[43].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[43].ParameterName = "@Param_dataNascimentoClienteA";
//            if ( FieldInfo.dataNascimentoClienteA == DateTime.MinValue )
//            { Parameters[43].Value = DBNull.Value; }
//            else
//            { Parameters[43].Value = FieldInfo.dataNascimentoClienteA; }
//
//            //Field dataNascimentoClienteB
//            Parameters[44].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[44].ParameterName = "@Param_dataNascimentoClienteB";
//            if ( FieldInfo.dataNascimentoClienteB == DateTime.MinValue )
//            { Parameters[44].Value = DBNull.Value; }
//            else
//            { Parameters[44].Value = FieldInfo.dataNascimentoClienteB; }
//
//            //Field dataNascimentoClienteC
//            Parameters[45].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[45].ParameterName = "@Param_dataNascimentoClienteC";
//            if ( FieldInfo.dataNascimentoClienteC == DateTime.MinValue )
//            { Parameters[45].Value = DBNull.Value; }
//            else
//            { Parameters[45].Value = FieldInfo.dataNascimentoClienteC; }
//
//            //Field emailPrincipalCliente
//            Parameters[46].SqlDbType = SqlDbType.VarChar;
//            Parameters[46].ParameterName = "@Param_emailPrincipalCliente";
//            if (( FieldInfo.emailPrincipalCliente == null ) || ( FieldInfo.emailPrincipalCliente == string.Empty ))
//            { Parameters[46].Value = DBNull.Value; }
//            else
//            { Parameters[46].Value = FieldInfo.emailPrincipalCliente; }
//            Parameters[46].Size = 1;
//
//            return Parameters;
//        }
//        #endregion
//
//
//
//
//
//        #region IDisposable Members 
//
//        bool disposed = false;
//
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//
//        ~ClienteControl() 
//        { 
//            Dispose(false); 
//        }
//
//        private void Dispose(bool disposing) 
//        {
//            if (!this.disposed)
//            {
//                if (disposing) 
//                {
//                    if (this.Conn != null)
//                        if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//                }
//            }
//
//        }
//        #endregion 
//
//#region Selecionando dados MainCliente
//
//        public DataTable GetMainCliente(string nomeCliente, string nomeContato, string statusCliente, DateTime dtCadastroInicio, DateTime dtCadastroFim, string cidadeCliente, string tipoCliente, List<int> idSubGrupoCliente, string telefone,string codigoCliente,string enderecoCliente)
//        {
//            DataSet dsMainCliente = new DataSet();
//            try
//            {
//                SqlConnection Conn = new SqlConnection(this.StrConnetionDB);
//
//                string query = GetQuery(nomeCliente, nomeContato, statusCliente, dtCadastroInicio, dtCadastroFim, cidadeCliente, tipoCliente, idSubGrupoCliente, telefone, codigoCliente, enderecoCliente);
//
//                Conn.Open();
//                DataTable dt = new DataTable();
//                SqlCommand Cmd = new SqlCommand(query.ToString(), Conn);
//                Cmd.CommandType = CommandType.Text;
//                SqlDataAdapter da = new SqlDataAdapter(Cmd);
//                da.Fill(dsMainCliente, "MainCliente");
//
//                return dsMainCliente.Tables[0];
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.", e.Message);
//                return null;
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return null;
//            }
//            finally
//            {
//                if (this.Conn != null)
//                    if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//            }
//        }
//
//        private string GetQuery(string nomeCliente, string nomeContato, string statusCliente, DateTime dtCadastroInicio, DateTime dtCadastroFim, string cidadeCliente, string tipoCliente, List<int> idSubGrupoCliente, string telefone, string codigoCliente, string enderecoCliente)
//        {
//            StringBuilder query = new StringBuilder();
//            query.Append(@"  SELECT c.*, (SELECT gc.descricaoGrupoCliente 
//                                           FROM GrupoCliente gc 
//                                          WHERE gc.idGrupoCliente = (SELECT sg.fkGrupoCliente  
//                                                                       FROM SubGrupoCliente sg 
//                                                                       WHERE sg.idSubGrupoCliente = c.fkSubGrupoCliente)) as descricaoGrupoCliente
//                                                                    FROM Cliente c,SubGrupoCliente	 sgc 
//                                                                    WHERE c.fkSubGrupoCliente = sgc.idSubGrupoCliente ");
//
//            if (!string.IsNullOrEmpty(telefone))
//            {
//                query.Append(GetStringByTelefone(telefone));
//            }
//
//            if (!string.IsNullOrEmpty(enderecoCliente))
//            {
//                query.AppendFormat(@" And (c.enderecoClienteA LIKE '%{0}%' 
//                                             OR c.enderecoClienteB LIKE '%{1}%'  
//                                             OR c.enderecoClienteC LIKE '%{2}%' )  ", enderecoCliente, enderecoCliente, enderecoCliente);
//            }
//
//            if (!string.IsNullOrEmpty(nomeCliente))
//                query.AppendFormat(" AND c.nomeCliente LIKE '%{0}%' ", nomeCliente);
//
//            if (!string.IsNullOrEmpty(codigoCliente))
//                query.AppendFormat(" AND c.idCliente = '{0}' ", codigoCliente);
//
//            if (!string.IsNullOrEmpty(nomeContato))
//                query.AppendFormat(" AND c.contatoClienteA LIKE '%{0}%' ", nomeContato);
//
//            if (statusCliente != "Todas")
//                query.AppendFormat("AND c.statusCliente = '{0}' ", statusCliente);
//
//            if (idSubGrupoCliente.Count > 0)
//            {
//                string ids = string.Empty;
//                foreach (var item in idSubGrupoCliente)
//                {
//                    ids += item.ToString() + ",";
//                }
//
//                query.AppendFormat("AND c.fkSubGrupoCliente in ({0}) ", ids.Substring(0,ids.Length -1));
//            }
//
//            // query.AppendFormat(" AND c.dataCadastroCliente >= '{0}' AND c.dataCadastroCliente <= '{1}' ", dtCadastroInicio.ToString("MM/dd/yyyy 00:00:00"), dtCadastroFim.ToString("MM/dd/yyyy 23:59:59"));//TODO descomentar para bd em ingles
//            // query.AppendFormat(" AND c.dataCadastroCliente >= '{0}' AND c.dataCadastroCliente <= '{1}' ", dtCadastroInicio.ToString(), dtCadastroFim.ToString());
//
//            if (!string.IsNullOrEmpty(cidadeCliente))
//                query.AppendFormat(" AND c.cidadeClienteA LIKE '%{0}%' ", cidadeCliente);
//
//            if (tipoCliente != "Todos")
//                query.AppendFormat(" AND c.tipoCliente = {0} ", tipoCliente);
//
//            query.Append(" ORDER BY c.nomeCliente ");
//
//            return query.ToString();
//        }
//
//        private string GetStringByTelefone(string telefone)
//        {
//            string query = string.Empty;
//            ClienteControl cliDal = new ClienteControl();
//
//            if (cliDal.GetAll().Find(x => x.telefoneClienteA.Equals(telefone)) != null)
//                query = " AND c.telefoneClienteA = '" + telefone + "' ";
//            else if (cliDal.GetAll().Find(x => x.telefoneClienteB.Equals(telefone)) != null)
//                query = " AND c.telefoneClienteB = '" + telefone + "' ";
//            else if (cliDal.GetAll().Find(x => x.telefoneClienteC.Equals(telefone)) != null)
//                query = " AND c.telefoneClienteC = '" + telefone + "' ";
//            else if (cliDal.GetAll().Find(x => x.telefoneClienteD.Equals(telefone)) != null)
//                query = " AND c.telefoneClienteD = '" + telefone + "' ";
//            else
//                return " AND c.telefoneClienteA = 'X'" ;
//
//            return query;
//        }
//
//        #endregion
//
//    }
//
//}
//
//
//
//
//
////Projeto substituído ------------------------
////using System;
////using System.Collections;
////using System.Collections.Generic;
////using System.Data;
////using System.Data.SqlClient;
////using System.Configuration;
////using System.Text;
////
////namespace SIMLgen
////{
////
////
////    /// <summary> 
////    /// Tabela: Cliente  
////    /// Autor: DAL Creator .net 
////    /// Data de criação: 22/05/2013 09:16:07 
////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "ClienteFields". 
////    /// </summary> 
////    public class ClienteControl : IDisposable 
////    {
////
////        #region String de conexão 
////        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
////        #endregion
////
////
////        #region Propriedade que armazena erros de execução 
////        private string _ErrorMessage;
////        public string ErrorMessage { get { return _ErrorMessage; } }
////        #endregion
////
////
////        #region Objetos de conexão 
////        SqlConnection Conn;
////        SqlCommand Cmd;
////        SqlTransaction Tran;
////        #endregion
////
////
////        #region Funcões que retornam Conexões e Transações 
////
////        public SqlTransaction GetNewTransaction(SqlConnection connIn)
////        {
////            if (connIn.State != ConnectionState.Open)
////                connIn.Open();
////            SqlTransaction TranOut = connIn.BeginTransaction();
////            return TranOut;
////        }
////
////        public SqlConnection GetNewConnection()
////        {
////            return GetNewConnection(this.StrConnetionDB);
////        }
////
////        public SqlConnection GetNewConnection(string StringConnection)
////        {
////            SqlConnection connOut = new SqlConnection(StringConnection);
////            return connOut;
////        }
////
////        #endregion
////
////
////        #region enum SQLMode 
////        /// <summary>   
////        /// Representa o procedimento que está sendo executado na tabela.
////        /// </summary>
////        public enum SQLMode
////        {                     
////            /// <summary>
////            /// Adiciona registro na tabela.
////            /// </summary>
////            Add,
////            /// <summary>
////            /// Atualiza registro na tabela.
////            /// </summary>
////            Update,
////            /// <summary>
////            /// Excluir registro na tabela
////            /// </summary>
////            Delete,
////            /// <summary>
////            /// Exclui TODOS os registros da tabela.
////            /// </summary>
////            DeleteAll,
////            /// <summary>
////            /// Seleciona um registro na tabela.
////            /// </summary>
////            Select,
////            /// <summary>
////            /// Seleciona TODOS os registros da tabela.
////            /// </summary>
////            SelectAll,
////            /// <summary>
////            /// Excluir ou seleciona um registro na tabela.
////            /// </summary>
////            SelectORDelete
////        }
////        #endregion 
////
////
////        public ClienteControl() {}
////
////
////        #region Inserindo dados na tabela 
////
////        /// <summary> 
////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
////        /// </summary>
////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Add( ref ClienteFields FieldInfo )
////        {
////            try
////            {
////                this.Conn = new SqlConnection(this.StrConnetionDB);
////                this.Conn.Open();
////                this.Tran = this.Conn.BeginTransaction();
////                this.Cmd = new SqlCommand("Proc_Cliente_Add", this.Conn, this.Tran);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////                this.Tran.Commit();
////                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
////                return true;
////
////            }
////            catch (SqlException e)
////            {
////                this.Tran.Rollback();
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this.Tran.Rollback();
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////            finally
////            {
////                if (this.Conn != null)
////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////                if (this.Cmd != null)
////                  this.Cmd.Dispose();
////            }
////        }
////
////        #endregion
////
////
////        #region Inserindo dados na tabela utilizando conexão e transação externa (compartilhada) 
////
////        /// <summary> 
////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
////        /// </summary>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref ClienteFields FieldInfo )
////        {
////            try
////            {
////                this.Cmd = new SqlCommand("Proc_Cliente_Add", ConnIn, TranIn);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
////                return true;
////
////            }
////            catch (SqlException e)
////            {
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////        }
////
////        #endregion
////
////
////        #region Editando dados na tabela 
////
////        /// <summary> 
////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
////        /// </summary>
////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Update( ClienteFields FieldInfo )
////        {
////            try
////            {
////                this.Conn = new SqlConnection(this.StrConnetionDB);
////                this.Conn.Open();
////                this.Tran = this.Conn.BeginTransaction();
////                this.Cmd = new SqlCommand("Proc_Cliente_Update", this.Conn, this.Tran);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
////                this.Tran.Commit();
////                return true;
////            }
////            catch (SqlException e)
////            {
////                this.Tran.Rollback();
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this.Tran.Rollback();
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////            finally
////            {
////                if (this.Conn != null)
////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////                if (this.Cmd != null)
////                  this.Cmd.Dispose();
////            }
////        }
////
////        #endregion
////
////
////        #region Editando dados na tabela utilizando conexão e transação externa (compartilhada) 
////
////        /// <summary> 
////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
////        /// </summary>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
////        {
////            try
////            {
////                this.Cmd = new SqlCommand("Proc_Cliente_Update", ConnIn, TranIn);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
////                return true;
////            }
////            catch (SqlException e)
////            {
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////        }
////
////        #endregion
////
////
////        #region Excluindo todos os dados da tabela 
////
////        /// <summary> 
////        /// Exclui todos os registros da tabela
////        /// </summary>
////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool DeleteAll()
////        {
////            try
////            {
////                this.Conn = new SqlConnection(this.StrConnetionDB);
////                this.Conn.Open();
////                this.Tran = this.Conn.BeginTransaction();
////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", this.Conn, this.Tran);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////                this.Tran.Commit();
////                return true;
////            }
////            catch (SqlException e)
////            {
////                this.Tran.Rollback();
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this.Tran.Rollback();
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////            finally
////            {
////                if (this.Conn != null)
////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////                if (this.Cmd != null)
////                  this.Cmd.Dispose();
////            }
////        }
////
////        #endregion
////
////
////        #region Excluindo todos os dados da tabela utilizando conexão e transação externa (compartilhada)
////
////        /// <summary> 
////        /// Exclui todos os registros da tabela
////        /// </summary>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
////        {
////            try
////            {
////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", ConnIn, TranIn);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////                return true;
////            }
////            catch (SqlException e)
////            {
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////        }
////
////        #endregion
////
////
////        #region Excluindo dados da tabela 
////
////        /// <summary> 
////        /// Exclui um registro da tabela no banco de dados
////        /// </summary>
////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete( ClienteFields FieldInfo )
////        {
////            return Delete(FieldInfo.idCliente);
////        }
////
////        /// <summary> 
////        /// Exclui um registro da tabela no banco de dados
////        /// </summary>
////        /// <param name="Param_idCliente">int</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete(
////                                     int Param_idCliente)
////        {
////            try
////            {
////                this.Conn = new SqlConnection(this.StrConnetionDB);
////                this.Conn.Open();
////                this.Tran = this.Conn.BeginTransaction();
////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", this.Conn, this.Tran);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////                this.Tran.Commit();
////                return true;
////            }
////            catch (SqlException e)
////            {
////                this.Tran.Rollback();
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this.Tran.Rollback();
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////            finally
////            {
////                if (this.Conn != null)
////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////                if (this.Cmd != null)
////                  this.Cmd.Dispose();
////            }
////        }
////
////        #endregion
////
////
////        #region Excluindo dados da tabela utilizando conexão e transação externa (compartilhada)
////
////        /// <summary> 
////        /// Exclui um registro da tabela no banco de dados
////        /// </summary>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
////        {
////            return Delete(ConnIn, TranIn, FieldInfo.idCliente);
////        }
////
////        /// <summary> 
////        /// Exclui um registro da tabela no banco de dados
////        /// </summary>
////        /// <param name="Param_idCliente">int</param>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
////                                     int Param_idCliente)
////        {
////            try
////            {
////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", ConnIn, TranIn);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////                return true;
////            }
////            catch (SqlException e)
////            {
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////                return false;
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////        }
////
////        #endregion
////
////
////        #region Selecionando um item da tabela 
////
////        /// <summary> 
////        /// Retorna um objeto ClienteFields através da chave primária passada como parâmetro
////        /// </summary>
////        /// <param name="Param_idCliente">int</param>
////        /// <returns>Objeto ClienteFields</returns> 
////        public ClienteFields GetItem(
////                                     int Param_idCliente)
////        {
////            ClienteFields infoFields = new ClienteFields();
////            try
////            {
////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////                {
////                    using (this.Cmd = new SqlCommand("Proc_Cliente_Select", this.Conn))
////                    {
////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////                        this.Cmd.Parameters.Clear();
////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
////                        this.Cmd.Connection.Open();
////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
////                        {
////                            if (!dr.HasRows) return null;
////                            if (dr.Read())
////                            {
////                               infoFields = GetDataFromReader( dr );
////                            }
////                        }
////                    }
////                 }
////
////                 return infoFields;
////
////            }
////            catch (SqlException e)
////            {
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
////                return null;
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return null;
////            }
////            finally
////            {
////                if (this.Conn != null)
////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////            }
////        }
////
////        #endregion
////
////
////        #region Selecionando todos os dados da tabela 
////
////        /// <summary> 
////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto ClienteFields.
////        /// </summary>
////        /// <returns>List de objetos ClienteFields</returns> 
////        public List<ClienteFields> GetAll()
////        {
////            List<ClienteFields> arrayInfo = new List<ClienteFields>();
////            try
////            {
////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////                {
////                    using (this.Cmd = new SqlCommand("Proc_Cliente_GetAll", this.Conn))
////                    {
////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////                        this.Cmd.Parameters.Clear();
////                        this.Cmd.Connection.Open();
////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
////                        {
////                           if (!dr.HasRows) return null;
////                           while (dr.Read())
////                           {
////                              arrayInfo.Add(GetDataFromReader( dr ));
////                           }
////                        }
////                    }
////                }
////
////                return arrayInfo;
////
////            }
////            catch (SqlException e)
////            {
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
////                return null;
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return null;
////            }
////            finally
////            {
////                if (this.Conn != null)
////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////            }
////        }
////
////        #endregion
////
////
////        #region Contando os dados da tabela 
////
////        /// <summary> 
////        /// Retorna o total de registros contidos na tabela
////        /// </summary>
////        /// <returns>int</returns> 
////        public int CountAll()
////        {
////            try
////            {
////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////                {
////                    using (this.Cmd = new SqlCommand("Proc_Cliente_CountAll", this.Conn))
////                    {
////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////                        this.Cmd.Parameters.Clear();
////                        this.Cmd.Connection.Open();
////                        object CountRegs = this.Cmd.ExecuteScalar();
////                        if (CountRegs == null)
////                        { return 0; }
////                        else
////                        { return (int)CountRegs; }
////                    }
////                }
////            }
////            catch (SqlException e)
////            {
////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
////                return 0;
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return 0;
////            }
////            finally
////            {
////                if (this.Conn != null)
////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////            }
////        }
////
////        #endregion
////
////
////        #region Função GetDataFromReader
////
////        /// <summary> 
////        /// Retorna um objeto ClienteFields preenchido com os valores dos campos do SqlDataReader
////        /// </summary>
////        /// <param name="dr">SqlDataReader - Preenche o objeto ClienteFields </param>
////        /// <returns>ClienteFields</returns>
////        private ClienteFields GetDataFromReader( SqlDataReader dr )
////        {
////            ClienteFields infoFields = new ClienteFields();
////
////            if (!dr.IsDBNull(0))
////            { infoFields.idCliente = dr.GetInt32(0); }
////            else
////            { infoFields.idCliente = 0; }
////
////
////
////            if (!dr.IsDBNull(1))
////            { infoFields.nomeCliente = dr.GetString(1); }
////            else
////            { infoFields.nomeCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(2))
////            { infoFields.enderecoClienteA = dr.GetString(2); }
////            else
////            { infoFields.enderecoClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(3))
////            { infoFields.enderecoClienteB = dr.GetString(3); }
////            else
////            { infoFields.enderecoClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(4))
////            { infoFields.enderecoClienteC = dr.GetString(4); }
////            else
////            { infoFields.enderecoClienteC = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(5))
////            { infoFields.bairroClienteA = dr.GetString(5); }
////            else
////            { infoFields.bairroClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(6))
////            { infoFields.bairroClienteB = dr.GetString(6); }
////            else
////            { infoFields.bairroClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(7))
////            { infoFields.bairroClientec = dr.GetString(7); }
////            else
////            { infoFields.bairroClientec = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(8))
////            { infoFields.cidadeClienteA = dr.GetString(8); }
////            else
////            { infoFields.cidadeClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(9))
////            { infoFields.cidadeClienteB = dr.GetString(9); }
////            else
////            { infoFields.cidadeClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(10))
////            { infoFields.cidadeClienteC = dr.GetString(10); }
////            else
////            { infoFields.cidadeClienteC = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(11))
////            { infoFields.estadoClienteA = dr.GetString(11); }
////            else
////            { infoFields.estadoClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(12))
////            { infoFields.estadoClienteB = dr.GetString(12); }
////            else
////            { infoFields.estadoClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(13))
////            { infoFields.estadoClienteC = dr.GetString(13); }
////            else
////            { infoFields.estadoClienteC = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(14))
////            { infoFields.cepClienteA = dr.GetString(14); }
////            else
////            { infoFields.cepClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(15))
////            { infoFields.cepClienteB = dr.GetString(15); }
////            else
////            { infoFields.cepClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(16))
////            { infoFields.cepClienteC = dr.GetString(16); }
////            else
////            { infoFields.cepClienteC = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(17))
////            { infoFields.telefoneClienteA = dr.GetString(17); }
////            else
////            { infoFields.telefoneClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(18))
////            { infoFields.telefoneClienteB = dr.GetString(18); }
////            else
////            { infoFields.telefoneClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(19))
////            { infoFields.telefoneClienteC = dr.GetString(19); }
////            else
////            { infoFields.telefoneClienteC = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(20))
////            { infoFields.telefoneClienteD = dr.GetString(20); }
////            else
////            { infoFields.telefoneClienteD = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(21))
////            { infoFields.celularClienteA = dr.GetString(21); }
////            else
////            { infoFields.celularClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(22))
////            { infoFields.celularClienteB = dr.GetString(22); }
////            else
////            { infoFields.celularClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(23))
////            { infoFields.celularClienteC = dr.GetString(23); }
////            else
////            { infoFields.celularClienteC = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(24))
////            { infoFields.complementoCliente = dr.GetString(24); }
////            else
////            { infoFields.complementoCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(25))
////            { infoFields.dataNascimentoCliente = dr.GetDateTime(25); }
////            else
////            { infoFields.dataNascimentoCliente = DateTime.MinValue; }
////
////
////
////            if (!dr.IsDBNull(26))
////            { infoFields.emailClienteA = dr.GetString(26); }
////            else
////            { infoFields.emailClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(27))
////            { infoFields.emailClienteB = dr.GetString(27); }
////            else
////            { infoFields.emailClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(28))
////            { infoFields.contatoClienteA = dr.GetString(28); }
////            else
////            { infoFields.contatoClienteA = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(29))
////            { infoFields.contatoClienteB = dr.GetString(29); }
////            else
////            { infoFields.contatoClienteB = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(30))
////            { infoFields.contatoClienteC = dr.GetString(30); }
////            else
////            { infoFields.contatoClienteC = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(31))
////            { infoFields.cnpjCliente = dr.GetString(31); }
////            else
////            { infoFields.cnpjCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(32))
////            { infoFields.cpfCliente = dr.GetString(32); }
////            else
////            { infoFields.cpfCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(33))
////            { infoFields.rgCliente = dr.GetString(33); }
////            else
////            { infoFields.rgCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(34))
////            { infoFields.inscEstadualCliente = dr.GetString(34); }
////            else
////            { infoFields.inscEstadualCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(35))
////            { infoFields.observacoesCliente = dr.GetString(35); }
////            else
////            { infoFields.observacoesCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(36))
////            { infoFields.dataCadastroCliente = dr.GetDateTime(36); }
////            else
////            { infoFields.dataCadastroCliente = DateTime.MinValue; }
////
////
////
////            if (!dr.IsDBNull(37))
////            { infoFields.tipoCliente = dr.GetString(37); }
////            else
////            { infoFields.tipoCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(38))
////            { infoFields.statusCliente = dr.GetString(38); }
////            else
////            { infoFields.statusCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(39))
////            { infoFields.fkSubGrupoCliente = dr.GetInt32(39); }
////            else
////            { infoFields.fkSubGrupoCliente = 0; }
////
////
////
////            if (!dr.IsDBNull(40))
////            { infoFields.dataUltimaCompraCliente = dr.GetDateTime(40); }
////            else
////            { infoFields.dataUltimaCompraCliente = DateTime.MinValue; }
////
////
////
////            if (!dr.IsDBNull(41))
////            { infoFields.numeroCasaCliente = dr.GetString(41); }
////            else
////            { infoFields.numeroCasaCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(42))
////            { infoFields.faxCliente = dr.GetString(42); }
////            else
////            { infoFields.faxCliente = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(43))
////            { infoFields.dataNascimentoClienteA = dr.GetDateTime(43); }
////            else
////            { infoFields.dataNascimentoClienteA = DateTime.MinValue; }
////
////
////
////            if (!dr.IsDBNull(44))
////            { infoFields.dataNascimentoClienteB = dr.GetDateTime(44); }
////            else
////            { infoFields.dataNascimentoClienteB = DateTime.MinValue; }
////
////
////
////            if (!dr.IsDBNull(45))
////            { infoFields.dataNascimentoClienteC = dr.GetDateTime(45); }
////            else
////            { infoFields.dataNascimentoClienteC = DateTime.MinValue; }
////
////
////            return infoFields;
////        }
////        #endregion
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////
////        #region Função GetAllParameters
////
////        /// <summary> 
////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
////        /// </summary>
////        /// <param name="FieldInfo">Objeto ClienteFields</param>
////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
////        private SqlParameter[] GetAllParameters( ClienteFields FieldInfo, SQLMode Modo )
////        {
////            SqlParameter[] Parameters;
////
////            switch (Modo)
////            {
////                case SQLMode.Add:
////                    Parameters = new SqlParameter[46];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    //Field idCliente
////                    Parameters[0].SqlDbType = SqlDbType.Int;
////                    Parameters[0].Direction = ParameterDirection.Output;
////                    Parameters[0].ParameterName = "@Param_idCliente";
////                    Parameters[0].Value = DBNull.Value;
////
////                    break;
////
////                case SQLMode.Update:
////                    Parameters = new SqlParameter[46];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    //Field idCliente
////                    Parameters[0].SqlDbType = SqlDbType.Int;
////                    Parameters[0].ParameterName = "@Param_idCliente";
////                    Parameters[0].Value = FieldInfo.idCliente;
////
////                    break;
////
////                case SQLMode.SelectORDelete:
////                    Parameters = new SqlParameter[1];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    //Field idCliente
////                    Parameters[0].SqlDbType = SqlDbType.Int;
////                    Parameters[0].ParameterName = "@Param_idCliente";
////                    Parameters[0].Value = FieldInfo.idCliente;
////
////                    return Parameters;
////
////                default:
////                    Parameters = new SqlParameter[46];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    break;
////            }
////
////            //Field nomeCliente
////            Parameters[1].SqlDbType = SqlDbType.VarChar;
////            Parameters[1].ParameterName = "@Param_nomeCliente";
////            if (( FieldInfo.nomeCliente == null ) || ( FieldInfo.nomeCliente == string.Empty ))
////            { Parameters[1].Value = DBNull.Value; }
////            else
////            { Parameters[1].Value = FieldInfo.nomeCliente; }
////            Parameters[1].Size = 255;
////
////            //Field enderecoClienteA
////            Parameters[2].SqlDbType = SqlDbType.VarChar;
////            Parameters[2].ParameterName = "@Param_enderecoClienteA";
////            if (( FieldInfo.enderecoClienteA == null ) || ( FieldInfo.enderecoClienteA == string.Empty ))
////            { Parameters[2].Value = DBNull.Value; }
////            else
////            { Parameters[2].Value = FieldInfo.enderecoClienteA; }
////            Parameters[2].Size = 255;
////
////            //Field enderecoClienteB
////            Parameters[3].SqlDbType = SqlDbType.VarChar;
////            Parameters[3].ParameterName = "@Param_enderecoClienteB";
////            if (( FieldInfo.enderecoClienteB == null ) || ( FieldInfo.enderecoClienteB == string.Empty ))
////            { Parameters[3].Value = DBNull.Value; }
////            else
////            { Parameters[3].Value = FieldInfo.enderecoClienteB; }
////            Parameters[3].Size = 255;
////
////            //Field enderecoClienteC
////            Parameters[4].SqlDbType = SqlDbType.VarChar;
////            Parameters[4].ParameterName = "@Param_enderecoClienteC";
////            if (( FieldInfo.enderecoClienteC == null ) || ( FieldInfo.enderecoClienteC == string.Empty ))
////            { Parameters[4].Value = DBNull.Value; }
////            else
////            { Parameters[4].Value = FieldInfo.enderecoClienteC; }
////            Parameters[4].Size = 255;
////
////            //Field bairroClienteA
////            Parameters[5].SqlDbType = SqlDbType.VarChar;
////            Parameters[5].ParameterName = "@Param_bairroClienteA";
////            if (( FieldInfo.bairroClienteA == null ) || ( FieldInfo.bairroClienteA == string.Empty ))
////            { Parameters[5].Value = DBNull.Value; }
////            else
////            { Parameters[5].Value = FieldInfo.bairroClienteA; }
////            Parameters[5].Size = 255;
////
////            //Field bairroClienteB
////            Parameters[6].SqlDbType = SqlDbType.VarChar;
////            Parameters[6].ParameterName = "@Param_bairroClienteB";
////            if (( FieldInfo.bairroClienteB == null ) || ( FieldInfo.bairroClienteB == string.Empty ))
////            { Parameters[6].Value = DBNull.Value; }
////            else
////            { Parameters[6].Value = FieldInfo.bairroClienteB; }
////            Parameters[6].Size = 255;
////
////            //Field bairroClientec
////            Parameters[7].SqlDbType = SqlDbType.VarChar;
////            Parameters[7].ParameterName = "@Param_bairroClientec";
////            if (( FieldInfo.bairroClientec == null ) || ( FieldInfo.bairroClientec == string.Empty ))
////            { Parameters[7].Value = DBNull.Value; }
////            else
////            { Parameters[7].Value = FieldInfo.bairroClientec; }
////            Parameters[7].Size = 255;
////
////            //Field cidadeClienteA
////            Parameters[8].SqlDbType = SqlDbType.VarChar;
////            Parameters[8].ParameterName = "@Param_cidadeClienteA";
////            if (( FieldInfo.cidadeClienteA == null ) || ( FieldInfo.cidadeClienteA == string.Empty ))
////            { Parameters[8].Value = DBNull.Value; }
////            else
////            { Parameters[8].Value = FieldInfo.cidadeClienteA; }
////            Parameters[8].Size = 255;
////
////            //Field cidadeClienteB
////            Parameters[9].SqlDbType = SqlDbType.VarChar;
////            Parameters[9].ParameterName = "@Param_cidadeClienteB";
////            if (( FieldInfo.cidadeClienteB == null ) || ( FieldInfo.cidadeClienteB == string.Empty ))
////            { Parameters[9].Value = DBNull.Value; }
////            else
////            { Parameters[9].Value = FieldInfo.cidadeClienteB; }
////            Parameters[9].Size = 255;
////
////            //Field cidadeClienteC
////            Parameters[10].SqlDbType = SqlDbType.VarChar;
////            Parameters[10].ParameterName = "@Param_cidadeClienteC";
////            if (( FieldInfo.cidadeClienteC == null ) || ( FieldInfo.cidadeClienteC == string.Empty ))
////            { Parameters[10].Value = DBNull.Value; }
////            else
////            { Parameters[10].Value = FieldInfo.cidadeClienteC; }
////            Parameters[10].Size = 255;
////
////            //Field estadoClienteA
////            Parameters[11].SqlDbType = SqlDbType.VarChar;
////            Parameters[11].ParameterName = "@Param_estadoClienteA";
////            if (( FieldInfo.estadoClienteA == null ) || ( FieldInfo.estadoClienteA == string.Empty ))
////            { Parameters[11].Value = DBNull.Value; }
////            else
////            { Parameters[11].Value = FieldInfo.estadoClienteA; }
////            Parameters[11].Size = 2;
////
////            //Field estadoClienteB
////            Parameters[12].SqlDbType = SqlDbType.VarChar;
////            Parameters[12].ParameterName = "@Param_estadoClienteB";
////            if (( FieldInfo.estadoClienteB == null ) || ( FieldInfo.estadoClienteB == string.Empty ))
////            { Parameters[12].Value = DBNull.Value; }
////            else
////            { Parameters[12].Value = FieldInfo.estadoClienteB; }
////            Parameters[12].Size = 2;
////
////            //Field estadoClienteC
////            Parameters[13].SqlDbType = SqlDbType.VarChar;
////            Parameters[13].ParameterName = "@Param_estadoClienteC";
////            if (( FieldInfo.estadoClienteC == null ) || ( FieldInfo.estadoClienteC == string.Empty ))
////            { Parameters[13].Value = DBNull.Value; }
////            else
////            { Parameters[13].Value = FieldInfo.estadoClienteC; }
////            Parameters[13].Size = 2;
////
////            //Field cepClienteA
////            Parameters[14].SqlDbType = SqlDbType.VarChar;
////            Parameters[14].ParameterName = "@Param_cepClienteA";
////            if (( FieldInfo.cepClienteA == null ) || ( FieldInfo.cepClienteA == string.Empty ))
////            { Parameters[14].Value = DBNull.Value; }
////            else
////            { Parameters[14].Value = FieldInfo.cepClienteA; }
////            Parameters[14].Size = 9;
////
////            //Field cepClienteB
////            Parameters[15].SqlDbType = SqlDbType.VarChar;
////            Parameters[15].ParameterName = "@Param_cepClienteB";
////            if (( FieldInfo.cepClienteB == null ) || ( FieldInfo.cepClienteB == string.Empty ))
////            { Parameters[15].Value = DBNull.Value; }
////            else
////            { Parameters[15].Value = FieldInfo.cepClienteB; }
////            Parameters[15].Size = 9;
////
////            //Field cepClienteC
////            Parameters[16].SqlDbType = SqlDbType.VarChar;
////            Parameters[16].ParameterName = "@Param_cepClienteC";
////            if (( FieldInfo.cepClienteC == null ) || ( FieldInfo.cepClienteC == string.Empty ))
////            { Parameters[16].Value = DBNull.Value; }
////            else
////            { Parameters[16].Value = FieldInfo.cepClienteC; }
////            Parameters[16].Size = 9;
////
////            //Field telefoneClienteA
////            Parameters[17].SqlDbType = SqlDbType.VarChar;
////            Parameters[17].ParameterName = "@Param_telefoneClienteA";
////            if (( FieldInfo.telefoneClienteA == null ) || ( FieldInfo.telefoneClienteA == string.Empty ))
////            { Parameters[17].Value = DBNull.Value; }
////            else
////            { Parameters[17].Value = FieldInfo.telefoneClienteA; }
////            Parameters[17].Size = 50;
////
////            //Field telefoneClienteB
////            Parameters[18].SqlDbType = SqlDbType.VarChar;
////            Parameters[18].ParameterName = "@Param_telefoneClienteB";
////            if (( FieldInfo.telefoneClienteB == null ) || ( FieldInfo.telefoneClienteB == string.Empty ))
////            { Parameters[18].Value = DBNull.Value; }
////            else
////            { Parameters[18].Value = FieldInfo.telefoneClienteB; }
////            Parameters[18].Size = 50;
////
////            //Field telefoneClienteC
////            Parameters[19].SqlDbType = SqlDbType.VarChar;
////            Parameters[19].ParameterName = "@Param_telefoneClienteC";
////            if (( FieldInfo.telefoneClienteC == null ) || ( FieldInfo.telefoneClienteC == string.Empty ))
////            { Parameters[19].Value = DBNull.Value; }
////            else
////            { Parameters[19].Value = FieldInfo.telefoneClienteC; }
////            Parameters[19].Size = 50;
////
////            //Field telefoneClienteD
////            Parameters[20].SqlDbType = SqlDbType.VarChar;
////            Parameters[20].ParameterName = "@Param_telefoneClienteD";
////            if (( FieldInfo.telefoneClienteD == null ) || ( FieldInfo.telefoneClienteD == string.Empty ))
////            { Parameters[20].Value = DBNull.Value; }
////            else
////            { Parameters[20].Value = FieldInfo.telefoneClienteD; }
////            Parameters[20].Size = 50;
////
////            //Field celularClienteA
////            Parameters[21].SqlDbType = SqlDbType.VarChar;
////            Parameters[21].ParameterName = "@Param_celularClienteA";
////            if (( FieldInfo.celularClienteA == null ) || ( FieldInfo.celularClienteA == string.Empty ))
////            { Parameters[21].Value = DBNull.Value; }
////            else
////            { Parameters[21].Value = FieldInfo.celularClienteA; }
////            Parameters[21].Size = 50;
////
////            //Field celularClienteB
////            Parameters[22].SqlDbType = SqlDbType.VarChar;
////            Parameters[22].ParameterName = "@Param_celularClienteB";
////            if (( FieldInfo.celularClienteB == null ) || ( FieldInfo.celularClienteB == string.Empty ))
////            { Parameters[22].Value = DBNull.Value; }
////            else
////            { Parameters[22].Value = FieldInfo.celularClienteB; }
////            Parameters[22].Size = 50;
////
////            //Field celularClienteC
////            Parameters[23].SqlDbType = SqlDbType.VarChar;
////            Parameters[23].ParameterName = "@Param_celularClienteC";
////            if (( FieldInfo.celularClienteC == null ) || ( FieldInfo.celularClienteC == string.Empty ))
////            { Parameters[23].Value = DBNull.Value; }
////            else
////            { Parameters[23].Value = FieldInfo.celularClienteC; }
////            Parameters[23].Size = 50;
////
////            //Field complementoCliente
////            Parameters[24].SqlDbType = SqlDbType.VarChar;
////            Parameters[24].ParameterName = "@Param_complementoCliente";
////            if (( FieldInfo.complementoCliente == null ) || ( FieldInfo.complementoCliente == string.Empty ))
////            { Parameters[24].Value = DBNull.Value; }
////            else
////            { Parameters[24].Value = FieldInfo.complementoCliente; }
////            Parameters[24].Size = 100;
////
////            //Field dataNascimentoCliente
////            Parameters[25].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[25].ParameterName = "@Param_dataNascimentoCliente";
////            if ( FieldInfo.dataNascimentoCliente == DateTime.MinValue )
////            { Parameters[25].Value = DBNull.Value; }
////            else
////            { Parameters[25].Value = FieldInfo.dataNascimentoCliente; }
////
////            //Field emailClienteA
////            Parameters[26].SqlDbType = SqlDbType.VarChar;
////            Parameters[26].ParameterName = "@Param_emailClienteA";
////            if (( FieldInfo.emailClienteA == null ) || ( FieldInfo.emailClienteA == string.Empty ))
////            { Parameters[26].Value = DBNull.Value; }
////            else
////            { Parameters[26].Value = FieldInfo.emailClienteA; }
////            Parameters[26].Size = 255;
////
////            //Field emailClienteB
////            Parameters[27].SqlDbType = SqlDbType.VarChar;
////            Parameters[27].ParameterName = "@Param_emailClienteB";
////            if (( FieldInfo.emailClienteB == null ) || ( FieldInfo.emailClienteB == string.Empty ))
////            { Parameters[27].Value = DBNull.Value; }
////            else
////            { Parameters[27].Value = FieldInfo.emailClienteB; }
////            Parameters[27].Size = 255;
////
////            //Field contatoClienteA
////            Parameters[28].SqlDbType = SqlDbType.VarChar;
////            Parameters[28].ParameterName = "@Param_contatoClienteA";
////            if (( FieldInfo.contatoClienteA == null ) || ( FieldInfo.contatoClienteA == string.Empty ))
////            { Parameters[28].Value = DBNull.Value; }
////            else
////            { Parameters[28].Value = FieldInfo.contatoClienteA; }
////            Parameters[28].Size = 255;
////
////            //Field contatoClienteB
////            Parameters[29].SqlDbType = SqlDbType.VarChar;
////            Parameters[29].ParameterName = "@Param_contatoClienteB";
////            if (( FieldInfo.contatoClienteB == null ) || ( FieldInfo.contatoClienteB == string.Empty ))
////            { Parameters[29].Value = DBNull.Value; }
////            else
////            { Parameters[29].Value = FieldInfo.contatoClienteB; }
////            Parameters[29].Size = 255;
////
////            //Field contatoClienteC
////            Parameters[30].SqlDbType = SqlDbType.VarChar;
////            Parameters[30].ParameterName = "@Param_contatoClienteC";
////            if (( FieldInfo.contatoClienteC == null ) || ( FieldInfo.contatoClienteC == string.Empty ))
////            { Parameters[30].Value = DBNull.Value; }
////            else
////            { Parameters[30].Value = FieldInfo.contatoClienteC; }
////            Parameters[30].Size = 255;
////
////            //Field cnpjCliente
////            Parameters[31].SqlDbType = SqlDbType.VarChar;
////            Parameters[31].ParameterName = "@Param_cnpjCliente";
////            if (( FieldInfo.cnpjCliente == null ) || ( FieldInfo.cnpjCliente == string.Empty ))
////            { Parameters[31].Value = DBNull.Value; }
////            else
////            { Parameters[31].Value = FieldInfo.cnpjCliente; }
////            Parameters[31].Size = 50;
////
////            //Field cpfCliente
////            Parameters[32].SqlDbType = SqlDbType.VarChar;
////            Parameters[32].ParameterName = "@Param_cpfCliente";
////            if (( FieldInfo.cpfCliente == null ) || ( FieldInfo.cpfCliente == string.Empty ))
////            { Parameters[32].Value = DBNull.Value; }
////            else
////            { Parameters[32].Value = FieldInfo.cpfCliente; }
////            Parameters[32].Size = 50;
////
////            //Field rgCliente
////            Parameters[33].SqlDbType = SqlDbType.VarChar;
////            Parameters[33].ParameterName = "@Param_rgCliente";
////            if (( FieldInfo.rgCliente == null ) || ( FieldInfo.rgCliente == string.Empty ))
////            { Parameters[33].Value = DBNull.Value; }
////            else
////            { Parameters[33].Value = FieldInfo.rgCliente; }
////            Parameters[33].Size = 50;
////
////            //Field inscEstadualCliente
////            Parameters[34].SqlDbType = SqlDbType.VarChar;
////            Parameters[34].ParameterName = "@Param_inscEstadualCliente";
////            if (( FieldInfo.inscEstadualCliente == null ) || ( FieldInfo.inscEstadualCliente == string.Empty ))
////            { Parameters[34].Value = DBNull.Value; }
////            else
////            { Parameters[34].Value = FieldInfo.inscEstadualCliente; }
////            Parameters[34].Size = 50;
////
////            //Field observacoesCliente
////            Parameters[35].SqlDbType = SqlDbType.VarChar;
////            Parameters[35].ParameterName = "@Param_observacoesCliente";
////            if (( FieldInfo.observacoesCliente == null ) || ( FieldInfo.observacoesCliente == string.Empty ))
////            { Parameters[35].Value = DBNull.Value; }
////            else
////            { Parameters[35].Value = FieldInfo.observacoesCliente; }
////            Parameters[35].Size = 300;
////
////            //Field dataCadastroCliente
////            Parameters[36].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[36].ParameterName = "@Param_dataCadastroCliente";
////            if ( FieldInfo.dataCadastroCliente == DateTime.MinValue )
////            { Parameters[36].Value = DBNull.Value; }
////            else
////            { Parameters[36].Value = FieldInfo.dataCadastroCliente; }
////
////            //Field tipoCliente
////            Parameters[37].SqlDbType = SqlDbType.VarChar;
////            Parameters[37].ParameterName = "@Param_tipoCliente";
////            if (( FieldInfo.tipoCliente == null ) || ( FieldInfo.tipoCliente == string.Empty ))
////            { Parameters[37].Value = DBNull.Value; }
////            else
////            { Parameters[37].Value = FieldInfo.tipoCliente; }
////            Parameters[37].Size = 20;
////
////            //Field statusCliente
////            Parameters[38].SqlDbType = SqlDbType.VarChar;
////            Parameters[38].ParameterName = "@Param_statusCliente";
////            if (( FieldInfo.statusCliente == null ) || ( FieldInfo.statusCliente == string.Empty ))
////            { Parameters[38].Value = DBNull.Value; }
////            else
////            { Parameters[38].Value = FieldInfo.statusCliente; }
////            Parameters[38].Size = 2;
////
////            //Field fkSubGrupoCliente
////            Parameters[39].SqlDbType = SqlDbType.Int;
////            Parameters[39].ParameterName = "@Param_fkSubGrupoCliente";
////            Parameters[39].Value = FieldInfo.fkSubGrupoCliente;
////
////            //Field dataUltimaCompraCliente
////            Parameters[40].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[40].ParameterName = "@Param_dataUltimaCompraCliente";
////            if ( FieldInfo.dataUltimaCompraCliente == DateTime.MinValue )
////            { Parameters[40].Value = DBNull.Value; }
////            else
////            { Parameters[40].Value = FieldInfo.dataUltimaCompraCliente; }
////
////            //Field numeroCasaCliente
////            Parameters[41].SqlDbType = SqlDbType.VarChar;
////            Parameters[41].ParameterName = "@Param_numeroCasaCliente";
////            if (( FieldInfo.numeroCasaCliente == null ) || ( FieldInfo.numeroCasaCliente == string.Empty ))
////            { Parameters[41].Value = DBNull.Value; }
////            else
////            { Parameters[41].Value = FieldInfo.numeroCasaCliente; }
////            Parameters[41].Size = 30;
////
////            //Field faxCliente
////            Parameters[42].SqlDbType = SqlDbType.VarChar;
////            Parameters[42].ParameterName = "@Param_faxCliente";
////            if (( FieldInfo.faxCliente == null ) || ( FieldInfo.faxCliente == string.Empty ))
////            { Parameters[42].Value = DBNull.Value; }
////            else
////            { Parameters[42].Value = FieldInfo.faxCliente; }
////            Parameters[42].Size = 50;
////
////            //Field dataNascimentoClienteA
////            Parameters[43].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[43].ParameterName = "@Param_dataNascimentoClienteA";
////            if ( FieldInfo.dataNascimentoClienteA == DateTime.MinValue )
////            { Parameters[43].Value = DBNull.Value; }
////            else
////            { Parameters[43].Value = FieldInfo.dataNascimentoClienteA; }
////
////            //Field dataNascimentoClienteB
////            Parameters[44].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[44].ParameterName = "@Param_dataNascimentoClienteB";
////            if ( FieldInfo.dataNascimentoClienteB == DateTime.MinValue )
////            { Parameters[44].Value = DBNull.Value; }
////            else
////            { Parameters[44].Value = FieldInfo.dataNascimentoClienteB; }
////
////            //Field dataNascimentoClienteC
////            Parameters[45].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[45].ParameterName = "@Param_dataNascimentoClienteC";
////            if ( FieldInfo.dataNascimentoClienteC == DateTime.MinValue )
////            { Parameters[45].Value = DBNull.Value; }
////            else
////            { Parameters[45].Value = FieldInfo.dataNascimentoClienteC; }
////
////            return Parameters;
////        }
////        #endregion
////
////
////
////
////
////        #region IDisposable Members 
////
////        bool disposed = false;
////
////        public void Dispose()
////        {
////            Dispose(true);
////            GC.SuppressFinalize(this);
////        }
////
////        ~ClienteControl() 
////        { 
////            Dispose(false); 
////        }
////
////        private void Dispose(bool disposing) 
////        {
////            if (!this.disposed)
////            {
////                if (disposing) 
////                {
////                    if (this.Conn != null)
////                        if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////                }
////            }
////
////        }
////        #endregion 
////
////
////
////    }
////
////}
////
////
////
////
////
//////Projeto substituído ------------------------
//////using System;
//////using System.Collections;
//////using System.Collections.Generic;
//////using System.Data;
//////using System.Data.SqlClient;
//////using System.Configuration;
//////using System.Text;
//////
//////namespace SIMLgen
//////{
//////
//////
//////    /// <summary> 
//////    /// Tabela: Cliente  
//////    /// Autor: DAL Creator .net 
//////    /// Data de criação: 22/05/2013 09:08:26 
//////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "ClienteFields". 
//////    /// </summary> 
//////    public class ClienteControl : IDisposable 
//////    {
//////
//////        #region String de conexão 
//////        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
//////        #endregion
//////
//////
//////        #region Propriedade que armazena erros de execução 
//////        private string _ErrorMessage;
//////        public string ErrorMessage { get { return _ErrorMessage; } }
//////        #endregion
//////
//////
//////        #region Objetos de conexão 
//////        SqlConnection Conn;
//////        SqlCommand Cmd;
//////        SqlTransaction Tran;
//////        #endregion
//////
//////
//////        #region Funcões que retornam Conexões e Transações 
//////
//////        public SqlTransaction GetNewTransaction(SqlConnection connIn)
//////        {
//////            if (connIn.State != ConnectionState.Open)
//////                connIn.Open();
//////            SqlTransaction TranOut = connIn.BeginTransaction();
//////            return TranOut;
//////        }
//////
//////        public SqlConnection GetNewConnection()
//////        {
//////            return GetNewConnection(this.StrConnetionDB);
//////        }
//////
//////        public SqlConnection GetNewConnection(string StringConnection)
//////        {
//////            SqlConnection connOut = new SqlConnection(StringConnection);
//////            return connOut;
//////        }
//////
//////        #endregion
//////
//////
//////        #region enum SQLMode 
//////        /// <summary>   
//////        /// Representa o procedimento que está sendo executado na tabela.
//////        /// </summary>
//////        public enum SQLMode
//////        {                     
//////            /// <summary>
//////            /// Adiciona registro na tabela.
//////            /// </summary>
//////            Add,
//////            /// <summary>
//////            /// Atualiza registro na tabela.
//////            /// </summary>
//////            Update,
//////            /// <summary>
//////            /// Excluir registro na tabela
//////            /// </summary>
//////            Delete,
//////            /// <summary>
//////            /// Exclui TODOS os registros da tabela.
//////            /// </summary>
//////            DeleteAll,
//////            /// <summary>
//////            /// Seleciona um registro na tabela.
//////            /// </summary>
//////            Select,
//////            /// <summary>
//////            /// Seleciona TODOS os registros da tabela.
//////            /// </summary>
//////            SelectAll,
//////            /// <summary>
//////            /// Excluir ou seleciona um registro na tabela.
//////            /// </summary>
//////            SelectORDelete
//////        }
//////        #endregion 
//////
//////
//////        public ClienteControl() {}
//////
//////
//////        #region Inserindo dados na tabela 
//////
//////        /// <summary> 
//////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
//////        /// </summary>
//////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Add( ref ClienteFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////                this.Conn.Open();
//////                this.Tran = this.Conn.BeginTransaction();
//////                this.Cmd = new SqlCommand("Proc_Cliente_Add", this.Conn, this.Tran);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////                this.Tran.Commit();
//////                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
//////                return true;
//////
//////            }
//////            catch (SqlException e)
//////            {
//////                this.Tran.Rollback();
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this.Tran.Rollback();
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////            finally
//////            {
//////                if (this.Conn != null)
//////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////                if (this.Cmd != null)
//////                  this.Cmd.Dispose();
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Inserindo dados na tabela utilizando conexão e transação externa (compartilhada) 
//////
//////        /// <summary> 
//////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
//////        /// </summary>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref ClienteFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Cmd = new SqlCommand("Proc_Cliente_Add", ConnIn, TranIn);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
//////                return true;
//////
//////            }
//////            catch (SqlException e)
//////            {
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Editando dados na tabela 
//////
//////        /// <summary> 
//////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
//////        /// </summary>
//////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
//////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Update( ClienteFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////                this.Conn.Open();
//////                this.Tran = this.Conn.BeginTransaction();
//////                this.Cmd = new SqlCommand("Proc_Cliente_Update", this.Conn, this.Tran);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
//////                this.Tran.Commit();
//////                return true;
//////            }
//////            catch (SqlException e)
//////            {
//////                this.Tran.Rollback();
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this.Tran.Rollback();
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////            finally
//////            {
//////                if (this.Conn != null)
//////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////                if (this.Cmd != null)
//////                  this.Cmd.Dispose();
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Editando dados na tabela utilizando conexão e transação externa (compartilhada) 
//////
//////        /// <summary> 
//////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
//////        /// </summary>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
//////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Cmd = new SqlCommand("Proc_Cliente_Update", ConnIn, TranIn);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
//////                return true;
//////            }
//////            catch (SqlException e)
//////            {
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Excluindo todos os dados da tabela 
//////
//////        /// <summary> 
//////        /// Exclui todos os registros da tabela
//////        /// </summary>
//////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool DeleteAll()
//////        {
//////            try
//////            {
//////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////                this.Conn.Open();
//////                this.Tran = this.Conn.BeginTransaction();
//////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", this.Conn, this.Tran);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////                this.Tran.Commit();
//////                return true;
//////            }
//////            catch (SqlException e)
//////            {
//////                this.Tran.Rollback();
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this.Tran.Rollback();
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////            finally
//////            {
//////                if (this.Conn != null)
//////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////                if (this.Cmd != null)
//////                  this.Cmd.Dispose();
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Excluindo todos os dados da tabela utilizando conexão e transação externa (compartilhada)
//////
//////        /// <summary> 
//////        /// Exclui todos os registros da tabela
//////        /// </summary>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
//////        {
//////            try
//////            {
//////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", ConnIn, TranIn);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////                return true;
//////            }
//////            catch (SqlException e)
//////            {
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Excluindo dados da tabela 
//////
//////        /// <summary> 
//////        /// Exclui um registro da tabela no banco de dados
//////        /// </summary>
//////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete( ClienteFields FieldInfo )
//////        {
//////            return Delete(FieldInfo.idCliente);
//////        }
//////
//////        /// <summary> 
//////        /// Exclui um registro da tabela no banco de dados
//////        /// </summary>
//////        /// <param name="Param_idCliente">int</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete(
//////                                     int Param_idCliente)
//////        {
//////            try
//////            {
//////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////                this.Conn.Open();
//////                this.Tran = this.Conn.BeginTransaction();
//////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", this.Conn, this.Tran);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////                this.Tran.Commit();
//////                return true;
//////            }
//////            catch (SqlException e)
//////            {
//////                this.Tran.Rollback();
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this.Tran.Rollback();
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////            finally
//////            {
//////                if (this.Conn != null)
//////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////                if (this.Cmd != null)
//////                  this.Cmd.Dispose();
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Excluindo dados da tabela utilizando conexão e transação externa (compartilhada)
//////
//////        /// <summary> 
//////        /// Exclui um registro da tabela no banco de dados
//////        /// </summary>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
//////        {
//////            return Delete(ConnIn, TranIn, FieldInfo.idCliente);
//////        }
//////
//////        /// <summary> 
//////        /// Exclui um registro da tabela no banco de dados
//////        /// </summary>
//////        /// <param name="Param_idCliente">int</param>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
//////                                     int Param_idCliente)
//////        {
//////            try
//////            {
//////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", ConnIn, TranIn);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////                return true;
//////            }
//////            catch (SqlException e)
//////            {
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return false;
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Selecionando um item da tabela 
//////
//////        /// <summary> 
//////        /// Retorna um objeto ClienteFields através da chave primária passada como parâmetro
//////        /// </summary>
//////        /// <param name="Param_idCliente">int</param>
//////        /// <returns>Objeto ClienteFields</returns> 
//////        public ClienteFields GetItem(
//////                                     int Param_idCliente)
//////        {
//////            ClienteFields infoFields = new ClienteFields();
//////            try
//////            {
//////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////                {
//////                    using (this.Cmd = new SqlCommand("Proc_Cliente_Select", this.Conn))
//////                    {
//////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////                        this.Cmd.Parameters.Clear();
//////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//////                        this.Cmd.Connection.Open();
//////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////                        {
//////                            if (!dr.HasRows) return null;
//////                            if (dr.Read())
//////                            {
//////                               infoFields = GetDataFromReader( dr );
//////                            }
//////                        }
//////                    }
//////                 }
//////
//////                 return infoFields;
//////
//////            }
//////            catch (SqlException e)
//////            {
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return null;
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return null;
//////            }
//////            finally
//////            {
//////                if (this.Conn != null)
//////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Selecionando todos os dados da tabela 
//////
//////        /// <summary> 
//////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto ClienteFields.
//////        /// </summary>
//////        /// <returns>List de objetos ClienteFields</returns> 
//////        public List<ClienteFields> GetAll()
//////        {
//////            List<ClienteFields> arrayInfo = new List<ClienteFields>();
//////            try
//////            {
//////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////                {
//////                    using (this.Cmd = new SqlCommand("Proc_Cliente_GetAll", this.Conn))
//////                    {
//////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////                        this.Cmd.Parameters.Clear();
//////                        this.Cmd.Connection.Open();
//////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////                        {
//////                           if (!dr.HasRows) return null;
//////                           while (dr.Read())
//////                           {
//////                              arrayInfo.Add(GetDataFromReader( dr ));
//////                           }
//////                        }
//////                    }
//////                }
//////
//////                return arrayInfo;
//////
//////            }
//////            catch (SqlException e)
//////            {
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return null;
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return null;
//////            }
//////            finally
//////            {
//////                if (this.Conn != null)
//////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Contando os dados da tabela 
//////
//////        /// <summary> 
//////        /// Retorna o total de registros contidos na tabela
//////        /// </summary>
//////        /// <returns>int</returns> 
//////        public int CountAll()
//////        {
//////            try
//////            {
//////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////                {
//////                    using (this.Cmd = new SqlCommand("Proc_Cliente_CountAll", this.Conn))
//////                    {
//////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////                        this.Cmd.Parameters.Clear();
//////                        this.Cmd.Connection.Open();
//////                        object CountRegs = this.Cmd.ExecuteScalar();
//////                        if (CountRegs == null)
//////                        { return 0; }
//////                        else
//////                        { return (int)CountRegs; }
//////                    }
//////                }
//////            }
//////            catch (SqlException e)
//////            {
//////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////                return 0;
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return 0;
//////            }
//////            finally
//////            {
//////                if (this.Conn != null)
//////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////            }
//////        }
//////
//////        #endregion
//////
//////
//////        #region Função GetDataFromReader
//////
//////        /// <summary> 
//////        /// Retorna um objeto ClienteFields preenchido com os valores dos campos do SqlDataReader
//////        /// </summary>
//////        /// <param name="dr">SqlDataReader - Preenche o objeto ClienteFields </param>
//////        /// <returns>ClienteFields</returns>
//////        private ClienteFields GetDataFromReader( SqlDataReader dr )
//////        {
//////            ClienteFields infoFields = new ClienteFields();
//////
//////            if (!dr.IsDBNull(0))
//////            { infoFields.idCliente = dr.GetInt32(0); }
//////            else
//////            { infoFields.idCliente = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(1))
//////            { infoFields.nomeCliente = dr.GetString(1); }
//////            else
//////            { infoFields.nomeCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(2))
//////            { infoFields.enderecoClienteA = dr.GetString(2); }
//////            else
//////            { infoFields.enderecoClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(3))
//////            { infoFields.enderecoClienteB = dr.GetString(3); }
//////            else
//////            { infoFields.enderecoClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(4))
//////            { infoFields.enderecoClienteC = dr.GetString(4); }
//////            else
//////            { infoFields.enderecoClienteC = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(5))
//////            { infoFields.bairroClienteA = dr.GetString(5); }
//////            else
//////            { infoFields.bairroClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(6))
//////            { infoFields.bairroClienteB = dr.GetString(6); }
//////            else
//////            { infoFields.bairroClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(7))
//////            { infoFields.bairroClientec = dr.GetString(7); }
//////            else
//////            { infoFields.bairroClientec = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(8))
//////            { infoFields.cidadeClienteA = dr.GetString(8); }
//////            else
//////            { infoFields.cidadeClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(9))
//////            { infoFields.cidadeClienteB = dr.GetString(9); }
//////            else
//////            { infoFields.cidadeClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(10))
//////            { infoFields.cidadeClienteC = dr.GetString(10); }
//////            else
//////            { infoFields.cidadeClienteC = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(11))
//////            { infoFields.estadoClienteA = dr.GetString(11); }
//////            else
//////            { infoFields.estadoClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(12))
//////            { infoFields.estadoClienteB = dr.GetString(12); }
//////            else
//////            { infoFields.estadoClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(13))
//////            { infoFields.estadoClienteC = dr.GetString(13); }
//////            else
//////            { infoFields.estadoClienteC = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(14))
//////            { infoFields.cepClienteA = dr.GetString(14); }
//////            else
//////            { infoFields.cepClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(15))
//////            { infoFields.cepClienteB = dr.GetString(15); }
//////            else
//////            { infoFields.cepClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(16))
//////            { infoFields.cepClienteC = dr.GetString(16); }
//////            else
//////            { infoFields.cepClienteC = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(17))
//////            { infoFields.telefoneClienteA = dr.GetString(17); }
//////            else
//////            { infoFields.telefoneClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(18))
//////            { infoFields.telefoneClienteB = dr.GetString(18); }
//////            else
//////            { infoFields.telefoneClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(19))
//////            { infoFields.telefoneClienteC = dr.GetString(19); }
//////            else
//////            { infoFields.telefoneClienteC = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(20))
//////            { infoFields.telefoneClienteD = dr.GetString(20); }
//////            else
//////            { infoFields.telefoneClienteD = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(21))
//////            { infoFields.celularClienteA = dr.GetString(21); }
//////            else
//////            { infoFields.celularClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(22))
//////            { infoFields.celularClienteB = dr.GetString(22); }
//////            else
//////            { infoFields.celularClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(23))
//////            { infoFields.celularClienteC = dr.GetString(23); }
//////            else
//////            { infoFields.celularClienteC = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(24))
//////            { infoFields.complementoCliente = dr.GetString(24); }
//////            else
//////            { infoFields.complementoCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(25))
//////            { infoFields.dataNascimentoCliente = dr.GetDateTime(25); }
//////            else
//////            { infoFields.dataNascimentoCliente = DateTime.MinValue; }
//////
//////
//////
//////            if (!dr.IsDBNull(26))
//////            { infoFields.emailClienteA = dr.GetString(26); }
//////            else
//////            { infoFields.emailClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(27))
//////            { infoFields.emailClienteB = dr.GetString(27); }
//////            else
//////            { infoFields.emailClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(28))
//////            { infoFields.contatoClienteA = dr.GetString(28); }
//////            else
//////            { infoFields.contatoClienteA = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(29))
//////            { infoFields.contatoClienteB = dr.GetString(29); }
//////            else
//////            { infoFields.contatoClienteB = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(30))
//////            { infoFields.contatoClienteC = dr.GetString(30); }
//////            else
//////            { infoFields.contatoClienteC = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(31))
//////            { infoFields.cnpjCliente = dr.GetString(31); }
//////            else
//////            { infoFields.cnpjCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(32))
//////            { infoFields.cpfCliente = dr.GetString(32); }
//////            else
//////            { infoFields.cpfCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(33))
//////            { infoFields.rgCliente = dr.GetString(33); }
//////            else
//////            { infoFields.rgCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(34))
//////            { infoFields.inscEstadualCliente = dr.GetString(34); }
//////            else
//////            { infoFields.inscEstadualCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(35))
//////            { infoFields.observacoesCliente = dr.GetString(35); }
//////            else
//////            { infoFields.observacoesCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(36))
//////            { infoFields.dataCadastroCliente = dr.GetDateTime(36); }
//////            else
//////            { infoFields.dataCadastroCliente = DateTime.MinValue; }
//////
//////
//////
//////            if (!dr.IsDBNull(37))
//////            { infoFields.tipoCliente = dr.GetString(37); }
//////            else
//////            { infoFields.tipoCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(38))
//////            { infoFields.statusCliente = dr.GetString(38); }
//////            else
//////            { infoFields.statusCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(39))
//////            { infoFields.fkSubGrupoCliente = dr.GetInt32(39); }
//////            else
//////            { infoFields.fkSubGrupoCliente = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(40))
//////            { infoFields.dataUltimaCompraCliente = dr.GetDateTime(40); }
//////            else
//////            { infoFields.dataUltimaCompraCliente = DateTime.MinValue; }
//////
//////
//////
//////            if (!dr.IsDBNull(41))
//////            { infoFields.numeroCasaCliente = dr.GetString(41); }
//////            else
//////            { infoFields.numeroCasaCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(42))
//////            { infoFields.faxCliente = dr.GetString(42); }
//////            else
//////            { infoFields.faxCliente = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(43))
//////            { infoFields.dataNascimentoClienteA = dr.GetDateTime(43); }
//////            else
//////            { infoFields.dataNascimentoClienteA = DateTime.MinValue; }
//////
//////
//////
//////            if (!dr.IsDBNull(44))
//////            { infoFields.dataNascimentoClienteB = dr.GetDateTime(44); }
//////            else
//////            { infoFields.dataNascimentoClienteB = DateTime.MinValue; }
//////
//////
//////
//////            if (!dr.IsDBNull(45))
//////            { infoFields.dataNascimentoClienteC = dr.GetDateTime(45); }
//////            else
//////            { infoFields.dataNascimentoClienteC = DateTime.MinValue; }
//////
//////
//////            return infoFields;
//////        }
//////        #endregion
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////
//////        #region Função GetAllParameters
//////
//////        /// <summary> 
//////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
//////        /// </summary>
//////        /// <param name="FieldInfo">Objeto ClienteFields</param>
//////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
//////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
//////        private SqlParameter[] GetAllParameters( ClienteFields FieldInfo, SQLMode Modo )
//////        {
//////            SqlParameter[] Parameters;
//////
//////            switch (Modo)
//////            {
//////                case SQLMode.Add:
//////                    Parameters = new SqlParameter[46];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    //Field idCliente
//////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////                    Parameters[0].Direction = ParameterDirection.Output;
//////                    Parameters[0].ParameterName = "@Param_idCliente";
//////                    Parameters[0].Value = DBNull.Value;
//////
//////                    break;
//////
//////                case SQLMode.Update:
//////                    Parameters = new SqlParameter[46];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    //Field idCliente
//////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////                    Parameters[0].ParameterName = "@Param_idCliente";
//////                    Parameters[0].Value = FieldInfo.idCliente;
//////
//////                    break;
//////
//////                case SQLMode.SelectORDelete:
//////                    Parameters = new SqlParameter[1];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    //Field idCliente
//////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////                    Parameters[0].ParameterName = "@Param_idCliente";
//////                    Parameters[0].Value = FieldInfo.idCliente;
//////
//////                    return Parameters;
//////
//////                default:
//////                    Parameters = new SqlParameter[46];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    break;
//////            }
//////
//////            //Field nomeCliente
//////            Parameters[1].SqlDbType = SqlDbType.VarChar;
//////            Parameters[1].ParameterName = "@Param_nomeCliente";
//////            if (( FieldInfo.nomeCliente == null ) || ( FieldInfo.nomeCliente == string.Empty ))
//////            { Parameters[1].Value = DBNull.Value; }
//////            else
//////            { Parameters[1].Value = FieldInfo.nomeCliente; }
//////            Parameters[1].Size = 255;
//////
//////            //Field enderecoClienteA
//////            Parameters[2].SqlDbType = SqlDbType.VarChar;
//////            Parameters[2].ParameterName = "@Param_enderecoClienteA";
//////            if (( FieldInfo.enderecoClienteA == null ) || ( FieldInfo.enderecoClienteA == string.Empty ))
//////            { Parameters[2].Value = DBNull.Value; }
//////            else
//////            { Parameters[2].Value = FieldInfo.enderecoClienteA; }
//////            Parameters[2].Size = 255;
//////
//////            //Field enderecoClienteB
//////            Parameters[3].SqlDbType = SqlDbType.VarChar;
//////            Parameters[3].ParameterName = "@Param_enderecoClienteB";
//////            if (( FieldInfo.enderecoClienteB == null ) || ( FieldInfo.enderecoClienteB == string.Empty ))
//////            { Parameters[3].Value = DBNull.Value; }
//////            else
//////            { Parameters[3].Value = FieldInfo.enderecoClienteB; }
//////            Parameters[3].Size = 255;
//////
//////            //Field enderecoClienteC
//////            Parameters[4].SqlDbType = SqlDbType.VarChar;
//////            Parameters[4].ParameterName = "@Param_enderecoClienteC";
//////            if (( FieldInfo.enderecoClienteC == null ) || ( FieldInfo.enderecoClienteC == string.Empty ))
//////            { Parameters[4].Value = DBNull.Value; }
//////            else
//////            { Parameters[4].Value = FieldInfo.enderecoClienteC; }
//////            Parameters[4].Size = 255;
//////
//////            //Field bairroClienteA
//////            Parameters[5].SqlDbType = SqlDbType.VarChar;
//////            Parameters[5].ParameterName = "@Param_bairroClienteA";
//////            if (( FieldInfo.bairroClienteA == null ) || ( FieldInfo.bairroClienteA == string.Empty ))
//////            { Parameters[5].Value = DBNull.Value; }
//////            else
//////            { Parameters[5].Value = FieldInfo.bairroClienteA; }
//////            Parameters[5].Size = 255;
//////
//////            //Field bairroClienteB
//////            Parameters[6].SqlDbType = SqlDbType.VarChar;
//////            Parameters[6].ParameterName = "@Param_bairroClienteB";
//////            if (( FieldInfo.bairroClienteB == null ) || ( FieldInfo.bairroClienteB == string.Empty ))
//////            { Parameters[6].Value = DBNull.Value; }
//////            else
//////            { Parameters[6].Value = FieldInfo.bairroClienteB; }
//////            Parameters[6].Size = 255;
//////
//////            //Field bairroClientec
//////            Parameters[7].SqlDbType = SqlDbType.VarChar;
//////            Parameters[7].ParameterName = "@Param_bairroClientec";
//////            if (( FieldInfo.bairroClientec == null ) || ( FieldInfo.bairroClientec == string.Empty ))
//////            { Parameters[7].Value = DBNull.Value; }
//////            else
//////            { Parameters[7].Value = FieldInfo.bairroClientec; }
//////            Parameters[7].Size = 255;
//////
//////            //Field cidadeClienteA
//////            Parameters[8].SqlDbType = SqlDbType.VarChar;
//////            Parameters[8].ParameterName = "@Param_cidadeClienteA";
//////            if (( FieldInfo.cidadeClienteA == null ) || ( FieldInfo.cidadeClienteA == string.Empty ))
//////            { Parameters[8].Value = DBNull.Value; }
//////            else
//////            { Parameters[8].Value = FieldInfo.cidadeClienteA; }
//////            Parameters[8].Size = 255;
//////
//////            //Field cidadeClienteB
//////            Parameters[9].SqlDbType = SqlDbType.VarChar;
//////            Parameters[9].ParameterName = "@Param_cidadeClienteB";
//////            if (( FieldInfo.cidadeClienteB == null ) || ( FieldInfo.cidadeClienteB == string.Empty ))
//////            { Parameters[9].Value = DBNull.Value; }
//////            else
//////            { Parameters[9].Value = FieldInfo.cidadeClienteB; }
//////            Parameters[9].Size = 255;
//////
//////            //Field cidadeClienteC
//////            Parameters[10].SqlDbType = SqlDbType.VarChar;
//////            Parameters[10].ParameterName = "@Param_cidadeClienteC";
//////            if (( FieldInfo.cidadeClienteC == null ) || ( FieldInfo.cidadeClienteC == string.Empty ))
//////            { Parameters[10].Value = DBNull.Value; }
//////            else
//////            { Parameters[10].Value = FieldInfo.cidadeClienteC; }
//////            Parameters[10].Size = 255;
//////
//////            //Field estadoClienteA
//////            Parameters[11].SqlDbType = SqlDbType.VarChar;
//////            Parameters[11].ParameterName = "@Param_estadoClienteA";
//////            if (( FieldInfo.estadoClienteA == null ) || ( FieldInfo.estadoClienteA == string.Empty ))
//////            { Parameters[11].Value = DBNull.Value; }
//////            else
//////            { Parameters[11].Value = FieldInfo.estadoClienteA; }
//////            Parameters[11].Size = 2;
//////
//////            //Field estadoClienteB
//////            Parameters[12].SqlDbType = SqlDbType.VarChar;
//////            Parameters[12].ParameterName = "@Param_estadoClienteB";
//////            if (( FieldInfo.estadoClienteB == null ) || ( FieldInfo.estadoClienteB == string.Empty ))
//////            { Parameters[12].Value = DBNull.Value; }
//////            else
//////            { Parameters[12].Value = FieldInfo.estadoClienteB; }
//////            Parameters[12].Size = 2;
//////
//////            //Field estadoClienteC
//////            Parameters[13].SqlDbType = SqlDbType.VarChar;
//////            Parameters[13].ParameterName = "@Param_estadoClienteC";
//////            if (( FieldInfo.estadoClienteC == null ) || ( FieldInfo.estadoClienteC == string.Empty ))
//////            { Parameters[13].Value = DBNull.Value; }
//////            else
//////            { Parameters[13].Value = FieldInfo.estadoClienteC; }
//////            Parameters[13].Size = 2;
//////
//////            //Field cepClienteA
//////            Parameters[14].SqlDbType = SqlDbType.VarChar;
//////            Parameters[14].ParameterName = "@Param_cepClienteA";
//////            if (( FieldInfo.cepClienteA == null ) || ( FieldInfo.cepClienteA == string.Empty ))
//////            { Parameters[14].Value = DBNull.Value; }
//////            else
//////            { Parameters[14].Value = FieldInfo.cepClienteA; }
//////            Parameters[14].Size = 9;
//////
//////            //Field cepClienteB
//////            Parameters[15].SqlDbType = SqlDbType.VarChar;
//////            Parameters[15].ParameterName = "@Param_cepClienteB";
//////            if (( FieldInfo.cepClienteB == null ) || ( FieldInfo.cepClienteB == string.Empty ))
//////            { Parameters[15].Value = DBNull.Value; }
//////            else
//////            { Parameters[15].Value = FieldInfo.cepClienteB; }
//////            Parameters[15].Size = 9;
//////
//////            //Field cepClienteC
//////            Parameters[16].SqlDbType = SqlDbType.VarChar;
//////            Parameters[16].ParameterName = "@Param_cepClienteC";
//////            if (( FieldInfo.cepClienteC == null ) || ( FieldInfo.cepClienteC == string.Empty ))
//////            { Parameters[16].Value = DBNull.Value; }
//////            else
//////            { Parameters[16].Value = FieldInfo.cepClienteC; }
//////            Parameters[16].Size = 9;
//////
//////            //Field telefoneClienteA
//////            Parameters[17].SqlDbType = SqlDbType.VarChar;
//////            Parameters[17].ParameterName = "@Param_telefoneClienteA";
//////            if (( FieldInfo.telefoneClienteA == null ) || ( FieldInfo.telefoneClienteA == string.Empty ))
//////            { Parameters[17].Value = DBNull.Value; }
//////            else
//////            { Parameters[17].Value = FieldInfo.telefoneClienteA; }
//////            Parameters[17].Size = 50;
//////
//////            //Field telefoneClienteB
//////            Parameters[18].SqlDbType = SqlDbType.VarChar;
//////            Parameters[18].ParameterName = "@Param_telefoneClienteB";
//////            if (( FieldInfo.telefoneClienteB == null ) || ( FieldInfo.telefoneClienteB == string.Empty ))
//////            { Parameters[18].Value = DBNull.Value; }
//////            else
//////            { Parameters[18].Value = FieldInfo.telefoneClienteB; }
//////            Parameters[18].Size = 50;
//////
//////            //Field telefoneClienteC
//////            Parameters[19].SqlDbType = SqlDbType.VarChar;
//////            Parameters[19].ParameterName = "@Param_telefoneClienteC";
//////            if (( FieldInfo.telefoneClienteC == null ) || ( FieldInfo.telefoneClienteC == string.Empty ))
//////            { Parameters[19].Value = DBNull.Value; }
//////            else
//////            { Parameters[19].Value = FieldInfo.telefoneClienteC; }
//////            Parameters[19].Size = 50;
//////
//////            //Field telefoneClienteD
//////            Parameters[20].SqlDbType = SqlDbType.VarChar;
//////            Parameters[20].ParameterName = "@Param_telefoneClienteD";
//////            if (( FieldInfo.telefoneClienteD == null ) || ( FieldInfo.telefoneClienteD == string.Empty ))
//////            { Parameters[20].Value = DBNull.Value; }
//////            else
//////            { Parameters[20].Value = FieldInfo.telefoneClienteD; }
//////            Parameters[20].Size = 50;
//////
//////            //Field celularClienteA
//////            Parameters[21].SqlDbType = SqlDbType.VarChar;
//////            Parameters[21].ParameterName = "@Param_celularClienteA";
//////            if (( FieldInfo.celularClienteA == null ) || ( FieldInfo.celularClienteA == string.Empty ))
//////            { Parameters[21].Value = DBNull.Value; }
//////            else
//////            { Parameters[21].Value = FieldInfo.celularClienteA; }
//////            Parameters[21].Size = 50;
//////
//////            //Field celularClienteB
//////            Parameters[22].SqlDbType = SqlDbType.VarChar;
//////            Parameters[22].ParameterName = "@Param_celularClienteB";
//////            if (( FieldInfo.celularClienteB == null ) || ( FieldInfo.celularClienteB == string.Empty ))
//////            { Parameters[22].Value = DBNull.Value; }
//////            else
//////            { Parameters[22].Value = FieldInfo.celularClienteB; }
//////            Parameters[22].Size = 50;
//////
//////            //Field celularClienteC
//////            Parameters[23].SqlDbType = SqlDbType.VarChar;
//////            Parameters[23].ParameterName = "@Param_celularClienteC";
//////            if (( FieldInfo.celularClienteC == null ) || ( FieldInfo.celularClienteC == string.Empty ))
//////            { Parameters[23].Value = DBNull.Value; }
//////            else
//////            { Parameters[23].Value = FieldInfo.celularClienteC; }
//////            Parameters[23].Size = 50;
//////
//////            //Field complementoCliente
//////            Parameters[24].SqlDbType = SqlDbType.VarChar;
//////            Parameters[24].ParameterName = "@Param_complementoCliente";
//////            if (( FieldInfo.complementoCliente == null ) || ( FieldInfo.complementoCliente == string.Empty ))
//////            { Parameters[24].Value = DBNull.Value; }
//////            else
//////            { Parameters[24].Value = FieldInfo.complementoCliente; }
//////            Parameters[24].Size = 100;
//////
//////            //Field dataNascimentoCliente
//////            Parameters[25].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[25].ParameterName = "@Param_dataNascimentoCliente";
//////            if ( FieldInfo.dataNascimentoCliente == DateTime.MinValue )
//////            { Parameters[25].Value = DBNull.Value; }
//////            else
//////            { Parameters[25].Value = FieldInfo.dataNascimentoCliente; }
//////
//////            //Field emailClienteA
//////            Parameters[26].SqlDbType = SqlDbType.VarChar;
//////            Parameters[26].ParameterName = "@Param_emailClienteA";
//////            if (( FieldInfo.emailClienteA == null ) || ( FieldInfo.emailClienteA == string.Empty ))
//////            { Parameters[26].Value = DBNull.Value; }
//////            else
//////            { Parameters[26].Value = FieldInfo.emailClienteA; }
//////            Parameters[26].Size = 255;
//////
//////            //Field emailClienteB
//////            Parameters[27].SqlDbType = SqlDbType.VarChar;
//////            Parameters[27].ParameterName = "@Param_emailClienteB";
//////            if (( FieldInfo.emailClienteB == null ) || ( FieldInfo.emailClienteB == string.Empty ))
//////            { Parameters[27].Value = DBNull.Value; }
//////            else
//////            { Parameters[27].Value = FieldInfo.emailClienteB; }
//////            Parameters[27].Size = 255;
//////
//////            //Field contatoClienteA
//////            Parameters[28].SqlDbType = SqlDbType.VarChar;
//////            Parameters[28].ParameterName = "@Param_contatoClienteA";
//////            if (( FieldInfo.contatoClienteA == null ) || ( FieldInfo.contatoClienteA == string.Empty ))
//////            { Parameters[28].Value = DBNull.Value; }
//////            else
//////            { Parameters[28].Value = FieldInfo.contatoClienteA; }
//////            Parameters[28].Size = 255;
//////
//////            //Field contatoClienteB
//////            Parameters[29].SqlDbType = SqlDbType.VarChar;
//////            Parameters[29].ParameterName = "@Param_contatoClienteB";
//////            if (( FieldInfo.contatoClienteB == null ) || ( FieldInfo.contatoClienteB == string.Empty ))
//////            { Parameters[29].Value = DBNull.Value; }
//////            else
//////            { Parameters[29].Value = FieldInfo.contatoClienteB; }
//////            Parameters[29].Size = 255;
//////
//////            //Field contatoClienteC
//////            Parameters[30].SqlDbType = SqlDbType.VarChar;
//////            Parameters[30].ParameterName = "@Param_contatoClienteC";
//////            if (( FieldInfo.contatoClienteC == null ) || ( FieldInfo.contatoClienteC == string.Empty ))
//////            { Parameters[30].Value = DBNull.Value; }
//////            else
//////            { Parameters[30].Value = FieldInfo.contatoClienteC; }
//////            Parameters[30].Size = 255;
//////
//////            //Field cnpjCliente
//////            Parameters[31].SqlDbType = SqlDbType.VarChar;
//////            Parameters[31].ParameterName = "@Param_cnpjCliente";
//////            if (( FieldInfo.cnpjCliente == null ) || ( FieldInfo.cnpjCliente == string.Empty ))
//////            { Parameters[31].Value = DBNull.Value; }
//////            else
//////            { Parameters[31].Value = FieldInfo.cnpjCliente; }
//////            Parameters[31].Size = 50;
//////
//////            //Field cpfCliente
//////            Parameters[32].SqlDbType = SqlDbType.VarChar;
//////            Parameters[32].ParameterName = "@Param_cpfCliente";
//////            if (( FieldInfo.cpfCliente == null ) || ( FieldInfo.cpfCliente == string.Empty ))
//////            { Parameters[32].Value = DBNull.Value; }
//////            else
//////            { Parameters[32].Value = FieldInfo.cpfCliente; }
//////            Parameters[32].Size = 50;
//////
//////            //Field rgCliente
//////            Parameters[33].SqlDbType = SqlDbType.VarChar;
//////            Parameters[33].ParameterName = "@Param_rgCliente";
//////            if (( FieldInfo.rgCliente == null ) || ( FieldInfo.rgCliente == string.Empty ))
//////            { Parameters[33].Value = DBNull.Value; }
//////            else
//////            { Parameters[33].Value = FieldInfo.rgCliente; }
//////            Parameters[33].Size = 50;
//////
//////            //Field inscEstadualCliente
//////            Parameters[34].SqlDbType = SqlDbType.VarChar;
//////            Parameters[34].ParameterName = "@Param_inscEstadualCliente";
//////            if (( FieldInfo.inscEstadualCliente == null ) || ( FieldInfo.inscEstadualCliente == string.Empty ))
//////            { Parameters[34].Value = DBNull.Value; }
//////            else
//////            { Parameters[34].Value = FieldInfo.inscEstadualCliente; }
//////            Parameters[34].Size = 50;
//////
//////            //Field observacoesCliente
//////            Parameters[35].SqlDbType = SqlDbType.VarChar;
//////            Parameters[35].ParameterName = "@Param_observacoesCliente";
//////            if (( FieldInfo.observacoesCliente == null ) || ( FieldInfo.observacoesCliente == string.Empty ))
//////            { Parameters[35].Value = DBNull.Value; }
//////            else
//////            { Parameters[35].Value = FieldInfo.observacoesCliente; }
//////            Parameters[35].Size = 300;
//////
//////            //Field dataCadastroCliente
//////            Parameters[36].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[36].ParameterName = "@Param_dataCadastroCliente";
//////            if ( FieldInfo.dataCadastroCliente == DateTime.MinValue )
//////            { Parameters[36].Value = DBNull.Value; }
//////            else
//////            { Parameters[36].Value = FieldInfo.dataCadastroCliente; }
//////
//////            //Field tipoCliente
//////            Parameters[37].SqlDbType = SqlDbType.VarChar;
//////            Parameters[37].ParameterName = "@Param_tipoCliente";
//////            if (( FieldInfo.tipoCliente == null ) || ( FieldInfo.tipoCliente == string.Empty ))
//////            { Parameters[37].Value = DBNull.Value; }
//////            else
//////            { Parameters[37].Value = FieldInfo.tipoCliente; }
//////            Parameters[37].Size = 20;
//////
//////            //Field statusCliente
//////            Parameters[38].SqlDbType = SqlDbType.VarChar;
//////            Parameters[38].ParameterName = "@Param_statusCliente";
//////            if (( FieldInfo.statusCliente == null ) || ( FieldInfo.statusCliente == string.Empty ))
//////            { Parameters[38].Value = DBNull.Value; }
//////            else
//////            { Parameters[38].Value = FieldInfo.statusCliente; }
//////            Parameters[38].Size = 2;
//////
//////            //Field fkSubGrupoCliente
//////            Parameters[39].SqlDbType = SqlDbType.Int;
//////            Parameters[39].ParameterName = "@Param_fkSubGrupoCliente";
//////            Parameters[39].Value = FieldInfo.fkSubGrupoCliente;
//////
//////            //Field dataUltimaCompraCliente
//////            Parameters[40].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[40].ParameterName = "@Param_dataUltimaCompraCliente";
//////            if ( FieldInfo.dataUltimaCompraCliente == DateTime.MinValue )
//////            { Parameters[40].Value = DBNull.Value; }
//////            else
//////            { Parameters[40].Value = FieldInfo.dataUltimaCompraCliente; }
//////
//////            //Field numeroCasaCliente
//////            Parameters[41].SqlDbType = SqlDbType.VarChar;
//////            Parameters[41].ParameterName = "@Param_numeroCasaCliente";
//////            if (( FieldInfo.numeroCasaCliente == null ) || ( FieldInfo.numeroCasaCliente == string.Empty ))
//////            { Parameters[41].Value = DBNull.Value; }
//////            else
//////            { Parameters[41].Value = FieldInfo.numeroCasaCliente; }
//////            Parameters[41].Size = 30;
//////
//////            //Field faxCliente
//////            Parameters[42].SqlDbType = SqlDbType.VarChar;
//////            Parameters[42].ParameterName = "@Param_faxCliente";
//////            if (( FieldInfo.faxCliente == null ) || ( FieldInfo.faxCliente == string.Empty ))
//////            { Parameters[42].Value = DBNull.Value; }
//////            else
//////            { Parameters[42].Value = FieldInfo.faxCliente; }
//////            Parameters[42].Size = 50;
//////
//////            //Field dataNascimentoClienteA
//////            Parameters[43].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[43].ParameterName = "@Param_dataNascimentoClienteA";
//////            if ( FieldInfo.dataNascimentoClienteA == DateTime.MinValue )
//////            { Parameters[43].Value = DBNull.Value; }
//////            else
//////            { Parameters[43].Value = FieldInfo.dataNascimentoClienteA; }
//////
//////            //Field dataNascimentoClienteB
//////            Parameters[44].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[44].ParameterName = "@Param_dataNascimentoClienteB";
//////            if ( FieldInfo.dataNascimentoClienteB == DateTime.MinValue )
//////            { Parameters[44].Value = DBNull.Value; }
//////            else
//////            { Parameters[44].Value = FieldInfo.dataNascimentoClienteB; }
//////
//////            //Field dataNascimentoClienteC
//////            Parameters[45].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[45].ParameterName = "@Param_dataNascimentoClienteC";
//////            if ( FieldInfo.dataNascimentoClienteC == DateTime.MinValue )
//////            { Parameters[45].Value = DBNull.Value; }
//////            else
//////            { Parameters[45].Value = FieldInfo.dataNascimentoClienteC; }
//////
//////            return Parameters;
//////        }
//////        #endregion
//////
//////
//////
//////
//////
//////        #region IDisposable Members 
//////
//////        bool disposed = false;
//////
//////        public void Dispose()
//////        {
//////            Dispose(true);
//////            GC.SuppressFinalize(this);
//////        }
//////
//////        ~ClienteControl() 
//////        { 
//////            Dispose(false); 
//////        }
//////
//////        private void Dispose(bool disposing) 
//////        {
//////            if (!this.disposed)
//////            {
//////                if (disposing) 
//////                {
//////                    if (this.Conn != null)
//////                        if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////                }
//////            }
//////
//////        }
//////        #endregion 
//////
//////
//////
//////    }
//////
//////}
//////
//////
//////
//////
//////
////////Projeto substituído ------------------------
////////using System;
////////using System.Collections;
////////using System.Collections.Generic;
////////using System.Data;
////////using System.Data.SqlClient;
////////using System.Configuration;
////////using System.Text;
////////
////////namespace SIMLgen
////////{
////////
////////
////////    /// <summary> 
////////    /// Tabela: Cliente  
////////    /// Autor: DAL Creator .net 
////////    /// Data de criação: 29/04/2013 12:36:36 
////////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "ClienteFields". 
////////    /// </summary> 
////////    public class ClienteControl : IDisposable 
////////    {
////////
////////        #region String de conexão 
////////        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
////////        #endregion
////////
////////
////////        #region Propriedade que armazena erros de execução 
////////        private string _ErrorMessage;
////////        public string ErrorMessage { get { return _ErrorMessage; } }
////////        #endregion
////////
////////
////////        #region Objetos de conexão 
////////        SqlConnection Conn;
////////        SqlCommand Cmd;
////////        SqlTransaction Tran;
////////        #endregion
////////
////////
////////        #region Funcões que retornam Conexões e Transações 
////////
////////        public SqlTransaction GetNewTransaction(SqlConnection connIn)
////////        {
////////            if (connIn.State != ConnectionState.Open)
////////                connIn.Open();
////////            SqlTransaction TranOut = connIn.BeginTransaction();
////////            return TranOut;
////////        }
////////
////////        public SqlConnection GetNewConnection()
////////        {
////////            return GetNewConnection(this.StrConnetionDB);
////////        }
////////
////////        public SqlConnection GetNewConnection(string StringConnection)
////////        {
////////            SqlConnection connOut = new SqlConnection(StringConnection);
////////            return connOut;
////////        }
////////
////////        #endregion
////////
////////
////////        #region enum SQLMode 
////////        /// <summary>   
////////        /// Representa o procedimento que está sendo executado na tabela.
////////        /// </summary>
////////        public enum SQLMode
////////        {                     
////////            /// <summary>
////////            /// Adiciona registro na tabela.
////////            /// </summary>
////////            Add,
////////            /// <summary>
////////            /// Atualiza registro na tabela.
////////            /// </summary>
////////            Update,
////////            /// <summary>
////////            /// Excluir registro na tabela
////////            /// </summary>
////////            Delete,
////////            /// <summary>
////////            /// Exclui TODOS os registros da tabela.
////////            /// </summary>
////////            DeleteAll,
////////            /// <summary>
////////            /// Seleciona um registro na tabela.
////////            /// </summary>
////////            Select,
////////            /// <summary>
////////            /// Seleciona TODOS os registros da tabela.
////////            /// </summary>
////////            SelectAll,
////////            /// <summary>
////////            /// Excluir ou seleciona um registro na tabela.
////////            /// </summary>
////////            SelectORDelete
////////        }
////////        #endregion 
////////
////////
////////        public ClienteControl() {}
////////
////////
////////        #region Inserindo dados na tabela 
////////
////////        /// <summary> 
////////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
////////        /// </summary>
////////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Add( ref ClienteFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Conn = new SqlConnection(this.StrConnetionDB);
////////                this.Conn.Open();
////////                this.Tran = this.Conn.BeginTransaction();
////////                this.Cmd = new SqlCommand("Proc_Cliente_Add", this.Conn, this.Tran);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////////                this.Tran.Commit();
////////                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
////////                return true;
////////
////////            }
////////            catch (SqlException e)
////////            {
////////                this.Tran.Rollback();
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this.Tran.Rollback();
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////            finally
////////            {
////////                if (this.Conn != null)
////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////                if (this.Cmd != null)
////////                  this.Cmd.Dispose();
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Inserindo dados na tabela utilizando conexão e transação externa (compartilhada) 
////////
////////        /// <summary> 
////////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
////////        /// </summary>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref ClienteFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Cmd = new SqlCommand("Proc_Cliente_Add", ConnIn, TranIn);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////////                FieldInfo.idCliente = (int)this.Cmd.Parameters["@Param_idCliente"].Value;
////////                return true;
////////
////////            }
////////            catch (SqlException e)
////////            {
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Editando dados na tabela 
////////
////////        /// <summary> 
////////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
////////        /// </summary>
////////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Update( ClienteFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Conn = new SqlConnection(this.StrConnetionDB);
////////                this.Conn.Open();
////////                this.Tran = this.Conn.BeginTransaction();
////////                this.Cmd = new SqlCommand("Proc_Cliente_Update", this.Conn, this.Tran);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
////////                this.Tran.Commit();
////////                return true;
////////            }
////////            catch (SqlException e)
////////            {
////////                this.Tran.Rollback();
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this.Tran.Rollback();
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////            finally
////////            {
////////                if (this.Conn != null)
////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////                if (this.Cmd != null)
////////                  this.Cmd.Dispose();
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Editando dados na tabela utilizando conexão e transação externa (compartilhada) 
////////
////////        /// <summary> 
////////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
////////        /// </summary>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Cmd = new SqlCommand("Proc_Cliente_Update", ConnIn, TranIn);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
////////                return true;
////////            }
////////            catch (SqlException e)
////////            {
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Excluindo todos os dados da tabela 
////////
////////        /// <summary> 
////////        /// Exclui todos os registros da tabela
////////        /// </summary>
////////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool DeleteAll()
////////        {
////////            try
////////            {
////////                this.Conn = new SqlConnection(this.StrConnetionDB);
////////                this.Conn.Open();
////////                this.Tran = this.Conn.BeginTransaction();
////////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", this.Conn, this.Tran);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////////                this.Tran.Commit();
////////                return true;
////////            }
////////            catch (SqlException e)
////////            {
////////                this.Tran.Rollback();
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this.Tran.Rollback();
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////            finally
////////            {
////////                if (this.Conn != null)
////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////                if (this.Cmd != null)
////////                  this.Cmd.Dispose();
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Excluindo todos os dados da tabela utilizando conexão e transação externa (compartilhada)
////////
////////        /// <summary> 
////////        /// Exclui todos os registros da tabela
////////        /// </summary>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
////////        {
////////            try
////////            {
////////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", ConnIn, TranIn);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////////                return true;
////////            }
////////            catch (SqlException e)
////////            {
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Excluindo dados da tabela 
////////
////////        /// <summary> 
////////        /// Exclui um registro da tabela no banco de dados
////////        /// </summary>
////////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete( ClienteFields FieldInfo )
////////        {
////////            return Delete(FieldInfo.idCliente);
////////        }
////////
////////        /// <summary> 
////////        /// Exclui um registro da tabela no banco de dados
////////        /// </summary>
////////        /// <param name="Param_idCliente">int</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete(
////////                                     int Param_idCliente)
////////        {
////////            try
////////            {
////////                this.Conn = new SqlConnection(this.StrConnetionDB);
////////                this.Conn.Open();
////////                this.Tran = this.Conn.BeginTransaction();
////////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", this.Conn, this.Tran);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////////                this.Tran.Commit();
////////                return true;
////////            }
////////            catch (SqlException e)
////////            {
////////                this.Tran.Rollback();
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this.Tran.Rollback();
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////            finally
////////            {
////////                if (this.Conn != null)
////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////                if (this.Cmd != null)
////////                  this.Cmd.Dispose();
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Excluindo dados da tabela utilizando conexão e transação externa (compartilhada)
////////
////////        /// <summary> 
////////        /// Exclui um registro da tabela no banco de dados
////////        /// </summary>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
////////        {
////////            return Delete(ConnIn, TranIn, FieldInfo.idCliente);
////////        }
////////
////////        /// <summary> 
////////        /// Exclui um registro da tabela no banco de dados
////////        /// </summary>
////////        /// <param name="Param_idCliente">int</param>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
////////                                     int Param_idCliente)
////////        {
////////            try
////////            {
////////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", ConnIn, TranIn);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
////////                return true;
////////            }
////////            catch (SqlException e)
////////            {
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return false;
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Selecionando um item da tabela 
////////
////////        /// <summary> 
////////        /// Retorna um objeto ClienteFields através da chave primária passada como parâmetro
////////        /// </summary>
////////        /// <param name="Param_idCliente">int</param>
////////        /// <returns>Objeto ClienteFields</returns> 
////////        public ClienteFields GetItem(
////////                                     int Param_idCliente)
////////        {
////////            ClienteFields infoFields = new ClienteFields();
////////            try
////////            {
////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////////                {
////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_Select", this.Conn))
////////                    {
////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////////                        this.Cmd.Parameters.Clear();
////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
////////                        this.Cmd.Connection.Open();
////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
////////                        {
////////                            if (!dr.HasRows) return null;
////////                            if (dr.Read())
////////                            {
////////                               infoFields = GetDataFromReader( dr );
////////                            }
////////                        }
////////                    }
////////                 }
////////
////////                 return infoFields;
////////
////////            }
////////            catch (SqlException e)
////////            {
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return null;
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return null;
////////            }
////////            finally
////////            {
////////                if (this.Conn != null)
////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Selecionando todos os dados da tabela 
////////
////////        /// <summary> 
////////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto ClienteFields.
////////        /// </summary>
////////        /// <returns>List de objetos ClienteFields</returns> 
////////        public List<ClienteFields> GetAll()
////////        {
////////            List<ClienteFields> arrayInfo = new List<ClienteFields>();
////////            try
////////            {
////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////////                {
////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_GetAll", this.Conn))
////////                    {
////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////////                        this.Cmd.Parameters.Clear();
////////                        this.Cmd.Connection.Open();
////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
////////                        {
////////                           if (!dr.HasRows) return null;
////////                           while (dr.Read())
////////                           {
////////                              arrayInfo.Add(GetDataFromReader( dr ));
////////                           }
////////                        }
////////                    }
////////                }
////////
////////                return arrayInfo;
////////
////////            }
////////            catch (SqlException e)
////////            {
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return null;
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return null;
////////            }
////////            finally
////////            {
////////                if (this.Conn != null)
////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Contando os dados da tabela 
////////
////////        /// <summary> 
////////        /// Retorna o total de registros contidos na tabela
////////        /// </summary>
////////        /// <returns>int</returns> 
////////        public int CountAll()
////////        {
////////            try
////////            {
////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////////                {
////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_CountAll", this.Conn))
////////                    {
////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////////                        this.Cmd.Parameters.Clear();
////////                        this.Cmd.Connection.Open();
////////                        object CountRegs = this.Cmd.ExecuteScalar();
////////                        if (CountRegs == null)
////////                        { return 0; }
////////                        else
////////                        { return (int)CountRegs; }
////////                    }
////////                }
////////            }
////////            catch (SqlException e)
////////            {
////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
////////                return 0;
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return 0;
////////            }
////////            finally
////////            {
////////                if (this.Conn != null)
////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////            }
////////        }
////////
////////        #endregion
////////
////////
////////        #region Função GetDataFromReader
////////
////////        /// <summary> 
////////        /// Retorna um objeto ClienteFields preenchido com os valores dos campos do SqlDataReader
////////        /// </summary>
////////        /// <param name="dr">SqlDataReader - Preenche o objeto ClienteFields </param>
////////        /// <returns>ClienteFields</returns>
////////        private ClienteFields GetDataFromReader( SqlDataReader dr )
////////        {
////////            ClienteFields infoFields = new ClienteFields();
////////
////////            if (!dr.IsDBNull(0))
////////            { infoFields.idCliente = dr.GetInt32(0); }
////////            else
////////            { infoFields.idCliente = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(1))
////////            { infoFields.nomeCliente = dr.GetString(1); }
////////            else
////////            { infoFields.nomeCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(2))
////////            { infoFields.enderecoClienteA = dr.GetString(2); }
////////            else
////////            { infoFields.enderecoClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(3))
////////            { infoFields.enderecoClienteB = dr.GetString(3); }
////////            else
////////            { infoFields.enderecoClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(4))
////////            { infoFields.enderecoClienteC = dr.GetString(4); }
////////            else
////////            { infoFields.enderecoClienteC = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(5))
////////            { infoFields.bairroClienteA = dr.GetString(5); }
////////            else
////////            { infoFields.bairroClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(6))
////////            { infoFields.bairroClienteB = dr.GetString(6); }
////////            else
////////            { infoFields.bairroClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(7))
////////            { infoFields.bairroClientec = dr.GetString(7); }
////////            else
////////            { infoFields.bairroClientec = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(8))
////////            { infoFields.cidadeClienteA = dr.GetString(8); }
////////            else
////////            { infoFields.cidadeClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(9))
////////            { infoFields.cidadeClienteB = dr.GetString(9); }
////////            else
////////            { infoFields.cidadeClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(10))
////////            { infoFields.cidadeClienteC = dr.GetString(10); }
////////            else
////////            { infoFields.cidadeClienteC = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(11))
////////            { infoFields.estadoClienteA = dr.GetString(11); }
////////            else
////////            { infoFields.estadoClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(12))
////////            { infoFields.estadoClienteB = dr.GetString(12); }
////////            else
////////            { infoFields.estadoClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(13))
////////            { infoFields.estadoClienteC = dr.GetString(13); }
////////            else
////////            { infoFields.estadoClienteC = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(14))
////////            { infoFields.cepClienteA = dr.GetString(14); }
////////            else
////////            { infoFields.cepClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(15))
////////            { infoFields.cepClienteB = dr.GetString(15); }
////////            else
////////            { infoFields.cepClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(16))
////////            { infoFields.cepClienteC = dr.GetString(16); }
////////            else
////////            { infoFields.cepClienteC = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(17))
////////            { infoFields.telefoneClienteA = dr.GetString(17); }
////////            else
////////            { infoFields.telefoneClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(18))
////////            { infoFields.telefoneClienteB = dr.GetString(18); }
////////            else
////////            { infoFields.telefoneClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(19))
////////            { infoFields.telefoneClienteC = dr.GetString(19); }
////////            else
////////            { infoFields.telefoneClienteC = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(20))
////////            { infoFields.telefoneClienteD = dr.GetString(20); }
////////            else
////////            { infoFields.telefoneClienteD = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(21))
////////            { infoFields.celularClienteA = dr.GetString(21); }
////////            else
////////            { infoFields.celularClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(22))
////////            { infoFields.celularClienteB = dr.GetString(22); }
////////            else
////////            { infoFields.celularClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(23))
////////            { infoFields.celularClienteC = dr.GetString(23); }
////////            else
////////            { infoFields.celularClienteC = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(24))
////////            { infoFields.complementoCliente = dr.GetString(24); }
////////            else
////////            { infoFields.complementoCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(25))
////////            { infoFields.dataNascimentoCliente = dr.GetDateTime(25); }
////////            else
////////            { infoFields.dataNascimentoCliente = DateTime.MinValue; }
////////
////////
////////
////////            if (!dr.IsDBNull(26))
////////            { infoFields.emailClienteA = dr.GetString(26); }
////////            else
////////            { infoFields.emailClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(27))
////////            { infoFields.emailClienteB = dr.GetString(27); }
////////            else
////////            { infoFields.emailClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(28))
////////            { infoFields.contatoClienteA = dr.GetString(28); }
////////            else
////////            { infoFields.contatoClienteA = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(29))
////////            { infoFields.contatoClienteB = dr.GetString(29); }
////////            else
////////            { infoFields.contatoClienteB = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(30))
////////            { infoFields.contatoClienteC = dr.GetString(30); }
////////            else
////////            { infoFields.contatoClienteC = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(31))
////////            { infoFields.cnpjCliente = dr.GetString(31); }
////////            else
////////            { infoFields.cnpjCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(32))
////////            { infoFields.cpfCliente = dr.GetString(32); }
////////            else
////////            { infoFields.cpfCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(33))
////////            { infoFields.rgCliente = dr.GetString(33); }
////////            else
////////            { infoFields.rgCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(34))
////////            { infoFields.inscEstadualCliente = dr.GetString(34); }
////////            else
////////            { infoFields.inscEstadualCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(35))
////////            { infoFields.observacoesCliente = dr.GetString(35); }
////////            else
////////            { infoFields.observacoesCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(36))
////////            { infoFields.dataCadastroCliente = dr.GetDateTime(36); }
////////            else
////////            { infoFields.dataCadastroCliente = DateTime.MinValue; }
////////
////////
////////
////////            if (!dr.IsDBNull(37))
////////            { infoFields.tipoCliente = dr.GetString(37); }
////////            else
////////            { infoFields.tipoCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(38))
////////            { infoFields.statusCliente = dr.GetString(38); }
////////            else
////////            { infoFields.statusCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(39))
////////            { infoFields.fkSubGrupoCliente = dr.GetInt32(39); }
////////            else
////////            { infoFields.fkSubGrupoCliente = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(40))
////////            { infoFields.dataUltimaCompraCliente = dr.GetDateTime(40); }
////////            else
////////            { infoFields.dataUltimaCompraCliente = DateTime.MinValue; }
////////
////////
////////
////////            if (!dr.IsDBNull(41))
////////            { infoFields.numeroCasaCliente = dr.GetString(41); }
////////            else
////////            { infoFields.numeroCasaCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(42))
////////            { infoFields.faxCliente = dr.GetString(42); }
////////            else
////////            { infoFields.faxCliente = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(43))
////////            { infoFields.dataNascimentoClienteA = dr.GetDateTime(43); }
////////            else
////////            { infoFields.dataNascimentoClienteA = DateTime.MinValue; }
////////
////////
////////
////////            if (!dr.IsDBNull(44))
////////            { infoFields.dataNascimentoClienteB = dr.GetDateTime(44); }
////////            else
////////            { infoFields.dataNascimentoClienteB = DateTime.MinValue; }
////////
////////
////////
////////            if (!dr.IsDBNull(45))
////////            { infoFields.dataNascimentoClienteC = dr.GetDateTime(45); }
////////            else
////////            { infoFields.dataNascimentoClienteC = DateTime.MinValue; }
////////
////////
////////            return infoFields;
////////        }
////////        #endregion
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////
////////        #region Função GetAllParameters
////////
////////        /// <summary> 
////////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
////////        /// </summary>
////////        /// <param name="FieldInfo">Objeto ClienteFields</param>
////////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
////////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
////////        private SqlParameter[] GetAllParameters( ClienteFields FieldInfo, SQLMode Modo )
////////        {
////////            SqlParameter[] Parameters;
////////
////////            switch (Modo)
////////            {
////////                case SQLMode.Add:
////////                    Parameters = new SqlParameter[46];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    //Field idCliente
////////                    Parameters[0].SqlDbType = SqlDbType.Int;
////////                    Parameters[0].Direction = ParameterDirection.Output;
////////                    Parameters[0].ParameterName = "@Param_idCliente";
////////                    Parameters[0].Value = DBNull.Value;
////////
////////                    break;
////////
////////                case SQLMode.Update:
////////                    Parameters = new SqlParameter[46];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    //Field idCliente
////////                    Parameters[0].SqlDbType = SqlDbType.Int;
////////                    Parameters[0].ParameterName = "@Param_idCliente";
////////                    Parameters[0].Value = FieldInfo.idCliente;
////////
////////                    break;
////////
////////                case SQLMode.SelectORDelete:
////////                    Parameters = new SqlParameter[1];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    //Field idCliente
////////                    Parameters[0].SqlDbType = SqlDbType.Int;
////////                    Parameters[0].ParameterName = "@Param_idCliente";
////////                    Parameters[0].Value = FieldInfo.idCliente;
////////
////////                    return Parameters;
////////
////////                default:
////////                    Parameters = new SqlParameter[46];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    break;
////////            }
////////
////////            //Field nomeCliente
////////            Parameters[1].SqlDbType = SqlDbType.VarChar;
////////            Parameters[1].ParameterName = "@Param_nomeCliente";
////////            if (( FieldInfo.nomeCliente == null ) || ( FieldInfo.nomeCliente == string.Empty ))
////////            { Parameters[1].Value = DBNull.Value; }
////////            else
////////            { Parameters[1].Value = FieldInfo.nomeCliente; }
////////            Parameters[1].Size = 255;
////////
////////            //Field enderecoClienteA
////////            Parameters[2].SqlDbType = SqlDbType.VarChar;
////////            Parameters[2].ParameterName = "@Param_enderecoClienteA";
////////            if (( FieldInfo.enderecoClienteA == null ) || ( FieldInfo.enderecoClienteA == string.Empty ))
////////            { Parameters[2].Value = DBNull.Value; }
////////            else
////////            { Parameters[2].Value = FieldInfo.enderecoClienteA; }
////////            Parameters[2].Size = 255;
////////
////////            //Field enderecoClienteB
////////            Parameters[3].SqlDbType = SqlDbType.VarChar;
////////            Parameters[3].ParameterName = "@Param_enderecoClienteB";
////////            if (( FieldInfo.enderecoClienteB == null ) || ( FieldInfo.enderecoClienteB == string.Empty ))
////////            { Parameters[3].Value = DBNull.Value; }
////////            else
////////            { Parameters[3].Value = FieldInfo.enderecoClienteB; }
////////            Parameters[3].Size = 255;
////////
////////            //Field enderecoClienteC
////////            Parameters[4].SqlDbType = SqlDbType.VarChar;
////////            Parameters[4].ParameterName = "@Param_enderecoClienteC";
////////            if (( FieldInfo.enderecoClienteC == null ) || ( FieldInfo.enderecoClienteC == string.Empty ))
////////            { Parameters[4].Value = DBNull.Value; }
////////            else
////////            { Parameters[4].Value = FieldInfo.enderecoClienteC; }
////////            Parameters[4].Size = 255;
////////
////////            //Field bairroClienteA
////////            Parameters[5].SqlDbType = SqlDbType.VarChar;
////////            Parameters[5].ParameterName = "@Param_bairroClienteA";
////////            if (( FieldInfo.bairroClienteA == null ) || ( FieldInfo.bairroClienteA == string.Empty ))
////////            { Parameters[5].Value = DBNull.Value; }
////////            else
////////            { Parameters[5].Value = FieldInfo.bairroClienteA; }
////////            Parameters[5].Size = 255;
////////
////////            //Field bairroClienteB
////////            Parameters[6].SqlDbType = SqlDbType.VarChar;
////////            Parameters[6].ParameterName = "@Param_bairroClienteB";
////////            if (( FieldInfo.bairroClienteB == null ) || ( FieldInfo.bairroClienteB == string.Empty ))
////////            { Parameters[6].Value = DBNull.Value; }
////////            else
////////            { Parameters[6].Value = FieldInfo.bairroClienteB; }
////////            Parameters[6].Size = 255;
////////
////////            //Field bairroClientec
////////            Parameters[7].SqlDbType = SqlDbType.VarChar;
////////            Parameters[7].ParameterName = "@Param_bairroClientec";
////////            if (( FieldInfo.bairroClientec == null ) || ( FieldInfo.bairroClientec == string.Empty ))
////////            { Parameters[7].Value = DBNull.Value; }
////////            else
////////            { Parameters[7].Value = FieldInfo.bairroClientec; }
////////            Parameters[7].Size = 255;
////////
////////            //Field cidadeClienteA
////////            Parameters[8].SqlDbType = SqlDbType.VarChar;
////////            Parameters[8].ParameterName = "@Param_cidadeClienteA";
////////            if (( FieldInfo.cidadeClienteA == null ) || ( FieldInfo.cidadeClienteA == string.Empty ))
////////            { Parameters[8].Value = DBNull.Value; }
////////            else
////////            { Parameters[8].Value = FieldInfo.cidadeClienteA; }
////////            Parameters[8].Size = 255;
////////
////////            //Field cidadeClienteB
////////            Parameters[9].SqlDbType = SqlDbType.VarChar;
////////            Parameters[9].ParameterName = "@Param_cidadeClienteB";
////////            if (( FieldInfo.cidadeClienteB == null ) || ( FieldInfo.cidadeClienteB == string.Empty ))
////////            { Parameters[9].Value = DBNull.Value; }
////////            else
////////            { Parameters[9].Value = FieldInfo.cidadeClienteB; }
////////            Parameters[9].Size = 255;
////////
////////            //Field cidadeClienteC
////////            Parameters[10].SqlDbType = SqlDbType.VarChar;
////////            Parameters[10].ParameterName = "@Param_cidadeClienteC";
////////            if (( FieldInfo.cidadeClienteC == null ) || ( FieldInfo.cidadeClienteC == string.Empty ))
////////            { Parameters[10].Value = DBNull.Value; }
////////            else
////////            { Parameters[10].Value = FieldInfo.cidadeClienteC; }
////////            Parameters[10].Size = 255;
////////
////////            //Field estadoClienteA
////////            Parameters[11].SqlDbType = SqlDbType.VarChar;
////////            Parameters[11].ParameterName = "@Param_estadoClienteA";
////////            if (( FieldInfo.estadoClienteA == null ) || ( FieldInfo.estadoClienteA == string.Empty ))
////////            { Parameters[11].Value = DBNull.Value; }
////////            else
////////            { Parameters[11].Value = FieldInfo.estadoClienteA; }
////////            Parameters[11].Size = 2;
////////
////////            //Field estadoClienteB
////////            Parameters[12].SqlDbType = SqlDbType.VarChar;
////////            Parameters[12].ParameterName = "@Param_estadoClienteB";
////////            if (( FieldInfo.estadoClienteB == null ) || ( FieldInfo.estadoClienteB == string.Empty ))
////////            { Parameters[12].Value = DBNull.Value; }
////////            else
////////            { Parameters[12].Value = FieldInfo.estadoClienteB; }
////////            Parameters[12].Size = 2;
////////
////////            //Field estadoClienteC
////////            Parameters[13].SqlDbType = SqlDbType.VarChar;
////////            Parameters[13].ParameterName = "@Param_estadoClienteC";
////////            if (( FieldInfo.estadoClienteC == null ) || ( FieldInfo.estadoClienteC == string.Empty ))
////////            { Parameters[13].Value = DBNull.Value; }
////////            else
////////            { Parameters[13].Value = FieldInfo.estadoClienteC; }
////////            Parameters[13].Size = 2;
////////
////////            //Field cepClienteA
////////            Parameters[14].SqlDbType = SqlDbType.VarChar;
////////            Parameters[14].ParameterName = "@Param_cepClienteA";
////////            if (( FieldInfo.cepClienteA == null ) || ( FieldInfo.cepClienteA == string.Empty ))
////////            { Parameters[14].Value = DBNull.Value; }
////////            else
////////            { Parameters[14].Value = FieldInfo.cepClienteA; }
////////            Parameters[14].Size = 9;
////////
////////            //Field cepClienteB
////////            Parameters[15].SqlDbType = SqlDbType.VarChar;
////////            Parameters[15].ParameterName = "@Param_cepClienteB";
////////            if (( FieldInfo.cepClienteB == null ) || ( FieldInfo.cepClienteB == string.Empty ))
////////            { Parameters[15].Value = DBNull.Value; }
////////            else
////////            { Parameters[15].Value = FieldInfo.cepClienteB; }
////////            Parameters[15].Size = 9;
////////
////////            //Field cepClienteC
////////            Parameters[16].SqlDbType = SqlDbType.VarChar;
////////            Parameters[16].ParameterName = "@Param_cepClienteC";
////////            if (( FieldInfo.cepClienteC == null ) || ( FieldInfo.cepClienteC == string.Empty ))
////////            { Parameters[16].Value = DBNull.Value; }
////////            else
////////            { Parameters[16].Value = FieldInfo.cepClienteC; }
////////            Parameters[16].Size = 9;
////////
////////            //Field telefoneClienteA
////////            Parameters[17].SqlDbType = SqlDbType.VarChar;
////////            Parameters[17].ParameterName = "@Param_telefoneClienteA";
////////            if (( FieldInfo.telefoneClienteA == null ) || ( FieldInfo.telefoneClienteA == string.Empty ))
////////            { Parameters[17].Value = DBNull.Value; }
////////            else
////////            { Parameters[17].Value = FieldInfo.telefoneClienteA; }
////////            Parameters[17].Size = 50;
////////
////////            //Field telefoneClienteB
////////            Parameters[18].SqlDbType = SqlDbType.VarChar;
////////            Parameters[18].ParameterName = "@Param_telefoneClienteB";
////////            if (( FieldInfo.telefoneClienteB == null ) || ( FieldInfo.telefoneClienteB == string.Empty ))
////////            { Parameters[18].Value = DBNull.Value; }
////////            else
////////            { Parameters[18].Value = FieldInfo.telefoneClienteB; }
////////            Parameters[18].Size = 50;
////////
////////            //Field telefoneClienteC
////////            Parameters[19].SqlDbType = SqlDbType.VarChar;
////////            Parameters[19].ParameterName = "@Param_telefoneClienteC";
////////            if (( FieldInfo.telefoneClienteC == null ) || ( FieldInfo.telefoneClienteC == string.Empty ))
////////            { Parameters[19].Value = DBNull.Value; }
////////            else
////////            { Parameters[19].Value = FieldInfo.telefoneClienteC; }
////////            Parameters[19].Size = 50;
////////
////////            //Field telefoneClienteD
////////            Parameters[20].SqlDbType = SqlDbType.VarChar;
////////            Parameters[20].ParameterName = "@Param_telefoneClienteD";
////////            if (( FieldInfo.telefoneClienteD == null ) || ( FieldInfo.telefoneClienteD == string.Empty ))
////////            { Parameters[20].Value = DBNull.Value; }
////////            else
////////            { Parameters[20].Value = FieldInfo.telefoneClienteD; }
////////            Parameters[20].Size = 50;
////////
////////            //Field celularClienteA
////////            Parameters[21].SqlDbType = SqlDbType.VarChar;
////////            Parameters[21].ParameterName = "@Param_celularClienteA";
////////            if (( FieldInfo.celularClienteA == null ) || ( FieldInfo.celularClienteA == string.Empty ))
////////            { Parameters[21].Value = DBNull.Value; }
////////            else
////////            { Parameters[21].Value = FieldInfo.celularClienteA; }
////////            Parameters[21].Size = 50;
////////
////////            //Field celularClienteB
////////            Parameters[22].SqlDbType = SqlDbType.VarChar;
////////            Parameters[22].ParameterName = "@Param_celularClienteB";
////////            if (( FieldInfo.celularClienteB == null ) || ( FieldInfo.celularClienteB == string.Empty ))
////////            { Parameters[22].Value = DBNull.Value; }
////////            else
////////            { Parameters[22].Value = FieldInfo.celularClienteB; }
////////            Parameters[22].Size = 50;
////////
////////            //Field celularClienteC
////////            Parameters[23].SqlDbType = SqlDbType.VarChar;
////////            Parameters[23].ParameterName = "@Param_celularClienteC";
////////            if (( FieldInfo.celularClienteC == null ) || ( FieldInfo.celularClienteC == string.Empty ))
////////            { Parameters[23].Value = DBNull.Value; }
////////            else
////////            { Parameters[23].Value = FieldInfo.celularClienteC; }
////////            Parameters[23].Size = 50;
////////
////////            //Field complementoCliente
////////            Parameters[24].SqlDbType = SqlDbType.VarChar;
////////            Parameters[24].ParameterName = "@Param_complementoCliente";
////////            if (( FieldInfo.complementoCliente == null ) || ( FieldInfo.complementoCliente == string.Empty ))
////////            { Parameters[24].Value = DBNull.Value; }
////////            else
////////            { Parameters[24].Value = FieldInfo.complementoCliente; }
////////            Parameters[24].Size = 100;
////////
////////            //Field dataNascimentoCliente
////////            Parameters[25].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[25].ParameterName = "@Param_dataNascimentoCliente";
////////            if ( FieldInfo.dataNascimentoCliente == DateTime.MinValue )
////////            { Parameters[25].Value = DBNull.Value; }
////////            else
////////            { Parameters[25].Value = FieldInfo.dataNascimentoCliente; }
////////
////////            //Field emailClienteA
////////            Parameters[26].SqlDbType = SqlDbType.VarChar;
////////            Parameters[26].ParameterName = "@Param_emailClienteA";
////////            if (( FieldInfo.emailClienteA == null ) || ( FieldInfo.emailClienteA == string.Empty ))
////////            { Parameters[26].Value = DBNull.Value; }
////////            else
////////            { Parameters[26].Value = FieldInfo.emailClienteA; }
////////            Parameters[26].Size = 255;
////////
////////            //Field emailClienteB
////////            Parameters[27].SqlDbType = SqlDbType.VarChar;
////////            Parameters[27].ParameterName = "@Param_emailClienteB";
////////            if (( FieldInfo.emailClienteB == null ) || ( FieldInfo.emailClienteB == string.Empty ))
////////            { Parameters[27].Value = DBNull.Value; }
////////            else
////////            { Parameters[27].Value = FieldInfo.emailClienteB; }
////////            Parameters[27].Size = 255;
////////
////////            //Field contatoClienteA
////////            Parameters[28].SqlDbType = SqlDbType.VarChar;
////////            Parameters[28].ParameterName = "@Param_contatoClienteA";
////////            if (( FieldInfo.contatoClienteA == null ) || ( FieldInfo.contatoClienteA == string.Empty ))
////////            { Parameters[28].Value = DBNull.Value; }
////////            else
////////            { Parameters[28].Value = FieldInfo.contatoClienteA; }
////////            Parameters[28].Size = 255;
////////
////////            //Field contatoClienteB
////////            Parameters[29].SqlDbType = SqlDbType.VarChar;
////////            Parameters[29].ParameterName = "@Param_contatoClienteB";
////////            if (( FieldInfo.contatoClienteB == null ) || ( FieldInfo.contatoClienteB == string.Empty ))
////////            { Parameters[29].Value = DBNull.Value; }
////////            else
////////            { Parameters[29].Value = FieldInfo.contatoClienteB; }
////////            Parameters[29].Size = 255;
////////
////////            //Field contatoClienteC
////////            Parameters[30].SqlDbType = SqlDbType.VarChar;
////////            Parameters[30].ParameterName = "@Param_contatoClienteC";
////////            if (( FieldInfo.contatoClienteC == null ) || ( FieldInfo.contatoClienteC == string.Empty ))
////////            { Parameters[30].Value = DBNull.Value; }
////////            else
////////            { Parameters[30].Value = FieldInfo.contatoClienteC; }
////////            Parameters[30].Size = 255;
////////
////////            //Field cnpjCliente
////////            Parameters[31].SqlDbType = SqlDbType.VarChar;
////////            Parameters[31].ParameterName = "@Param_cnpjCliente";
////////            if (( FieldInfo.cnpjCliente == null ) || ( FieldInfo.cnpjCliente == string.Empty ))
////////            { Parameters[31].Value = DBNull.Value; }
////////            else
////////            { Parameters[31].Value = FieldInfo.cnpjCliente; }
////////            Parameters[31].Size = 50;
////////
////////            //Field cpfCliente
////////            Parameters[32].SqlDbType = SqlDbType.VarChar;
////////            Parameters[32].ParameterName = "@Param_cpfCliente";
////////            if (( FieldInfo.cpfCliente == null ) || ( FieldInfo.cpfCliente == string.Empty ))
////////            { Parameters[32].Value = DBNull.Value; }
////////            else
////////            { Parameters[32].Value = FieldInfo.cpfCliente; }
////////            Parameters[32].Size = 50;
////////
////////            //Field rgCliente
////////            Parameters[33].SqlDbType = SqlDbType.VarChar;
////////            Parameters[33].ParameterName = "@Param_rgCliente";
////////            if (( FieldInfo.rgCliente == null ) || ( FieldInfo.rgCliente == string.Empty ))
////////            { Parameters[33].Value = DBNull.Value; }
////////            else
////////            { Parameters[33].Value = FieldInfo.rgCliente; }
////////            Parameters[33].Size = 50;
////////
////////            //Field inscEstadualCliente
////////            Parameters[34].SqlDbType = SqlDbType.VarChar;
////////            Parameters[34].ParameterName = "@Param_inscEstadualCliente";
////////            if (( FieldInfo.inscEstadualCliente == null ) || ( FieldInfo.inscEstadualCliente == string.Empty ))
////////            { Parameters[34].Value = DBNull.Value; }
////////            else
////////            { Parameters[34].Value = FieldInfo.inscEstadualCliente; }
////////            Parameters[34].Size = 50;
////////
////////            //Field observacoesCliente
////////            Parameters[35].SqlDbType = SqlDbType.VarChar;
////////            Parameters[35].ParameterName = "@Param_observacoesCliente";
////////            if (( FieldInfo.observacoesCliente == null ) || ( FieldInfo.observacoesCliente == string.Empty ))
////////            { Parameters[35].Value = DBNull.Value; }
////////            else
////////            { Parameters[35].Value = FieldInfo.observacoesCliente; }
////////            Parameters[35].Size = 300;
////////
////////            //Field dataCadastroCliente
////////            Parameters[36].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[36].ParameterName = "@Param_dataCadastroCliente";
////////            if ( FieldInfo.dataCadastroCliente == DateTime.MinValue )
////////            { Parameters[36].Value = DBNull.Value; }
////////            else
////////            { Parameters[36].Value = FieldInfo.dataCadastroCliente; }
////////
////////            //Field tipoCliente
////////            Parameters[37].SqlDbType = SqlDbType.VarChar;
////////            Parameters[37].ParameterName = "@Param_tipoCliente";
////////            if (( FieldInfo.tipoCliente == null ) || ( FieldInfo.tipoCliente == string.Empty ))
////////            { Parameters[37].Value = DBNull.Value; }
////////            else
////////            { Parameters[37].Value = FieldInfo.tipoCliente; }
////////            Parameters[37].Size = 20;
////////
////////            //Field statusCliente
////////            Parameters[38].SqlDbType = SqlDbType.VarChar;
////////            Parameters[38].ParameterName = "@Param_statusCliente";
////////            if (( FieldInfo.statusCliente == null ) || ( FieldInfo.statusCliente == string.Empty ))
////////            { Parameters[38].Value = DBNull.Value; }
////////            else
////////            { Parameters[38].Value = FieldInfo.statusCliente; }
////////            Parameters[38].Size = 2;
////////
////////            //Field fkSubGrupoCliente
////////            Parameters[39].SqlDbType = SqlDbType.Int;
////////            Parameters[39].ParameterName = "@Param_fkSubGrupoCliente";
////////            Parameters[39].Value = FieldInfo.fkSubGrupoCliente;
////////
////////            //Field dataUltimaCompraCliente
////////            Parameters[40].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[40].ParameterName = "@Param_dataUltimaCompraCliente";
////////            if ( FieldInfo.dataUltimaCompraCliente == DateTime.MinValue )
////////            { Parameters[40].Value = DBNull.Value; }
////////            else
////////            { Parameters[40].Value = FieldInfo.dataUltimaCompraCliente; }
////////
////////            //Field numeroCasaCliente
////////            Parameters[41].SqlDbType = SqlDbType.VarChar;
////////            Parameters[41].ParameterName = "@Param_numeroCasaCliente";
////////            if (( FieldInfo.numeroCasaCliente == null ) || ( FieldInfo.numeroCasaCliente == string.Empty ))
////////            { Parameters[41].Value = DBNull.Value; }
////////            else
////////            { Parameters[41].Value = FieldInfo.numeroCasaCliente; }
////////            Parameters[41].Size = 30;
////////
////////            //Field faxCliente
////////            Parameters[42].SqlDbType = SqlDbType.VarChar;
////////            Parameters[42].ParameterName = "@Param_faxCliente";
////////            if (( FieldInfo.faxCliente == null ) || ( FieldInfo.faxCliente == string.Empty ))
////////            { Parameters[42].Value = DBNull.Value; }
////////            else
////////            { Parameters[42].Value = FieldInfo.faxCliente; }
////////            Parameters[42].Size = 50;
////////
////////            //Field dataNascimentoClienteA
////////            Parameters[43].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[43].ParameterName = "@Param_dataNascimentoClienteA";
////////            if ( FieldInfo.dataNascimentoClienteA == DateTime.MinValue )
////////            { Parameters[43].Value = DBNull.Value; }
////////            else
////////            { Parameters[43].Value = FieldInfo.dataNascimentoClienteA; }
////////
////////            //Field dataNascimentoClienteB
////////            Parameters[44].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[44].ParameterName = "@Param_dataNascimentoClienteB";
////////            if ( FieldInfo.dataNascimentoClienteB == DateTime.MinValue )
////////            { Parameters[44].Value = DBNull.Value; }
////////            else
////////            { Parameters[44].Value = FieldInfo.dataNascimentoClienteB; }
////////
////////            //Field dataNascimentoClienteC
////////            Parameters[45].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[45].ParameterName = "@Param_dataNascimentoClienteC";
////////            if ( FieldInfo.dataNascimentoClienteC == DateTime.MinValue )
////////            { Parameters[45].Value = DBNull.Value; }
////////            else
////////            { Parameters[45].Value = FieldInfo.dataNascimentoClienteC; }
////////
////////            return Parameters;
////////        }
////////        #endregion
////////
////////
////////
////////
////////
////////        #region IDisposable Members 
////////
////////        bool disposed = false;
////////
////////        public void Dispose()
////////        {
////////            Dispose(true);
////////            GC.SuppressFinalize(this);
////////        }
////////
////////        ~ClienteControl() 
////////        { 
////////            Dispose(false); 
////////        }
////////
////////        private void Dispose(bool disposing) 
////////        {
////////            if (!this.disposed)
////////            {
////////                if (disposing) 
////////                {
////////                    if (this.Conn != null)
////////                        if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
////////                }
////////            }
////////
////////        }
////////        #endregion 
////////
////////
////////
////////    }
////////
////////}
////////
////////
////////
////////
////////
//////////Projeto substituído ------------------------
//////////using System;
//////////using System.Collections;
//////////using System.Collections.Generic;
//////////using System.Data;
//////////using System.Data.SqlClient;
//////////using System.Configuration;
//////////using System.Text;
//////////
//////////namespace SIMLgen
//////////{
//////////
//////////
//////////    /// <summary> 
//////////    /// Tabela: Cliente  
//////////    /// Autor: DAL Creator .net 
//////////    /// Data de criação: 25/04/2013 15:38:03 
//////////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "ClienteFields". 
//////////    /// </summary> 
//////////    public class ClienteControl : IDisposable 
//////////    {
//////////
//////////        #region String de conexão 
//////////        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
//////////        #endregion
//////////
//////////
//////////        #region Propriedade que armazena erros de execução 
//////////        private string _ErrorMessage;
//////////        public string ErrorMessage { get { return _ErrorMessage; } }
//////////        #endregion
//////////
//////////
//////////        #region Objetos de conexão 
//////////        SqlConnection Conn;
//////////        SqlCommand Cmd;
//////////        SqlTransaction Tran;
//////////        #endregion
//////////
//////////
//////////        #region Funcões que retornam Conexões e Transações 
//////////
//////////        public SqlTransaction GetNewTransaction(SqlConnection connIn)
//////////        {
//////////            if (connIn.State != ConnectionState.Open)
//////////                connIn.Open();
//////////            SqlTransaction TranOut = connIn.BeginTransaction();
//////////            return TranOut;
//////////        }
//////////
//////////        public SqlConnection GetNewConnection()
//////////        {
//////////            return GetNewConnection(this.StrConnetionDB);
//////////        }
//////////
//////////        public SqlConnection GetNewConnection(string StringConnection)
//////////        {
//////////            SqlConnection connOut = new SqlConnection(StringConnection);
//////////            return connOut;
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region enum SQLMode 
//////////        /// <summary>   
//////////        /// Representa o procedimento que está sendo executado na tabela.
//////////        /// </summary>
//////////        public enum SQLMode
//////////        {                     
//////////            /// <summary>
//////////            /// Adiciona registro na tabela.
//////////            /// </summary>
//////////            Add,
//////////            /// <summary>
//////////            /// Atualiza registro na tabela.
//////////            /// </summary>
//////////            Update,
//////////            /// <summary>
//////////            /// Excluir registro na tabela
//////////            /// </summary>
//////////            Delete,
//////////            /// <summary>
//////////            /// Exclui TODOS os registros da tabela.
//////////            /// </summary>
//////////            DeleteAll,
//////////            /// <summary>
//////////            /// Seleciona um registro na tabela.
//////////            /// </summary>
//////////            Select,
//////////            /// <summary>
//////////            /// Seleciona TODOS os registros da tabela.
//////////            /// </summary>
//////////            SelectAll,
//////////            /// <summary>
//////////            /// Excluir ou seleciona um registro na tabela.
//////////            /// </summary>
//////////            SelectORDelete
//////////        }
//////////        #endregion 
//////////
//////////
//////////        public ClienteControl() {}
//////////
//////////
//////////        #region Inserindo dados na tabela 
//////////
//////////        /// <summary> 
//////////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Add(  ClienteFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////////                this.Conn.Open();
//////////                this.Tran = this.Conn.BeginTransaction();
//////////                this.Cmd = new SqlCommand("Proc_Cliente_Add", this.Conn, this.Tran);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////////                this.Tran.Commit();
//////////                return true;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                this.Tran.Rollback();
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this.Tran.Rollback();
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////                if (this.Cmd != null)
//////////                  this.Cmd.Dispose();
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Inserindo dados na tabela utilizando conexão e transação externa (compartilhada) 
//////////
//////////        /// <summary> 
//////////        /// Grava/Persiste um novo objeto ClienteFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <param name="FieldInfo">Objeto ClienteFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn,  ClienteFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Cmd = new SqlCommand("Proc_Cliente_Add", ConnIn, TranIn);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////////                return true;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Editando dados na tabela 
//////////
//////////        /// <summary> 
//////////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
//////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Update( ClienteFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////////                this.Conn.Open();
//////////                this.Tran = this.Conn.BeginTransaction();
//////////                this.Cmd = new SqlCommand("Proc_Cliente_Update", this.Conn, this.Tran);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
//////////                this.Tran.Commit();
//////////                return true;
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                this.Tran.Rollback();
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this.Tran.Rollback();
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////                if (this.Cmd != null)
//////////                  this.Cmd.Dispose();
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Editando dados na tabela utilizando conexão e transação externa (compartilhada) 
//////////
//////////        /// <summary> 
//////////        /// Grava/Persiste as alterações em um objeto ClienteFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <param name="FieldInfo">Objeto ClienteFields a ser alterado.</param>
//////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Cmd = new SqlCommand("Proc_Cliente_Update", ConnIn, TranIn);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
//////////                return true;
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Excluindo todos os dados da tabela 
//////////
//////////        /// <summary> 
//////////        /// Exclui todos os registros da tabela
//////////        /// </summary>
//////////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool DeleteAll()
//////////        {
//////////            try
//////////            {
//////////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////////                this.Conn.Open();
//////////                this.Tran = this.Conn.BeginTransaction();
//////////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", this.Conn, this.Tran);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////////                this.Tran.Commit();
//////////                return true;
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                this.Tran.Rollback();
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this.Tran.Rollback();
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////                if (this.Cmd != null)
//////////                  this.Cmd.Dispose();
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Excluindo todos os dados da tabela utilizando conexão e transação externa (compartilhada)
//////////
//////////        /// <summary> 
//////////        /// Exclui todos os registros da tabela
//////////        /// </summary>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
//////////        {
//////////            try
//////////            {
//////////                this.Cmd = new SqlCommand("Proc_Cliente_DeleteAll", ConnIn, TranIn);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////////                return true;
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Excluindo dados da tabela 
//////////
//////////        /// <summary> 
//////////        /// Exclui um registro da tabela no banco de dados
//////////        /// </summary>
//////////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete( ClienteFields FieldInfo )
//////////        {
//////////            return Delete(FieldInfo.idCliente);
//////////        }
//////////
//////////        /// <summary> 
//////////        /// Exclui um registro da tabela no banco de dados
//////////        /// </summary>
//////////        /// <param name="Param_idCliente">int</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete(
//////////                                     int Param_idCliente)
//////////        {
//////////            try
//////////            {
//////////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////////                this.Conn.Open();
//////////                this.Tran = this.Conn.BeginTransaction();
//////////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", this.Conn, this.Tran);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////////                this.Tran.Commit();
//////////                return true;
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                this.Tran.Rollback();
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this.Tran.Rollback();
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////                if (this.Cmd != null)
//////////                  this.Cmd.Dispose();
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Excluindo dados da tabela utilizando conexão e transação externa (compartilhada)
//////////
//////////        /// <summary> 
//////////        /// Exclui um registro da tabela no banco de dados
//////////        /// </summary>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <param name="FieldInfo">Objeto ClienteFields a ser excluído.</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, ClienteFields FieldInfo )
//////////        {
//////////            return Delete(ConnIn, TranIn, FieldInfo.idCliente);
//////////        }
//////////
//////////        /// <summary> 
//////////        /// Exclui um registro da tabela no banco de dados
//////////        /// </summary>
//////////        /// <param name="Param_idCliente">int</param>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
//////////                                     int Param_idCliente)
//////////        {
//////////            try
//////////            {
//////////                this.Cmd = new SqlCommand("Proc_Cliente_Delete", ConnIn, TranIn);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//////////                return true;
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return false;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Selecionando um item da tabela 
//////////
//////////        /// <summary> 
//////////        /// Retorna um objeto ClienteFields através da chave primária passada como parâmetro
//////////        /// </summary>
//////////        /// <param name="Param_idCliente">int</param>
//////////        /// <returns>Objeto ClienteFields</returns> 
//////////        public ClienteFields GetItem(
//////////                                     int Param_idCliente)
//////////        {
//////////            ClienteFields infoFields = new ClienteFields();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_Select", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idCliente", SqlDbType.Int)).Value = Param_idCliente;
//////////                        this.Cmd.Connection.Open();
//////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////////                        {
//////////                            if (!dr.HasRows) return null;
//////////                            if (dr.Read())
//////////                            {
//////////                               infoFields = GetDataFromReader( dr );
//////////                            }
//////////                        }
//////////                    }
//////////                 }
//////////
//////////                 return infoFields;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return null;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return null;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Selecionando todos os dados da tabela 
//////////
//////////        /// <summary> 
//////////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto ClienteFields.
//////////        /// </summary>
//////////        /// <returns>List de objetos ClienteFields</returns> 
//////////        public List<ClienteFields> GetAll()
//////////        {
//////////            List<ClienteFields> arrayInfo = new List<ClienteFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_GetAll", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Connection.Open();
//////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////////                        {
//////////                           if (!dr.HasRows) return null;
//////////                           while (dr.Read())
//////////                           {
//////////                              arrayInfo.Add(GetDataFromReader( dr ));
//////////                           }
//////////                        }
//////////                    }
//////////                }
//////////
//////////                return arrayInfo;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return null;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return null;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Contando os dados da tabela 
//////////
//////////        /// <summary> 
//////////        /// Retorna o total de registros contidos na tabela
//////////        /// </summary>
//////////        /// <returns>int</returns> 
//////////        public int CountAll()
//////////        {
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_CountAll", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Connection.Open();
//////////                        object CountRegs = this.Cmd.ExecuteScalar();
//////////                        if (CountRegs == null)
//////////                        { return 0; }
//////////                        else
//////////                        { return (int)CountRegs; }
//////////                    }
//////////                }
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return 0;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return 0;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////        #region Selecionando dados da tabela através do campo "cnpjCliente" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo cnpjCliente.
//////////        /// </summary>
//////////        /// <param name="Param_cnpjCliente">string</param>
//////////        /// <returns>List ClienteFields</returns> 
//////////        public List<ClienteFields> FindBycnpjCliente(
//////////                               string Param_cnpjCliente )
//////////        {
//////////            List<ClienteFields> arrayList = new List<ClienteFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBycnpjCliente", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_cnpjCliente", SqlDbType.VarChar, 50)).Value = Param_cnpjCliente;
//////////                        this.Cmd.Connection.Open();
//////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////////                        {
//////////                            if (!dr.HasRows) return null;
//////////                            while (dr.Read())
//////////                            {
//////////                               arrayList.Add(GetDataFromReader( dr ));
//////////                            }
//////////                        }
//////////                    }
//////////                }
//////////
//////////                return arrayList;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return null;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return null;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////
//////////        #region Selecionando dados da tabela através do campo "cpfCliente" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo cpfCliente.
//////////        /// </summary>
//////////        /// <param name="Param_cpfCliente">string</param>
//////////        /// <returns>List ClienteFields</returns> 
//////////        public List<ClienteFields> FindBycpfCliente(
//////////                               string Param_cpfCliente )
//////////        {
//////////            List<ClienteFields> arrayList = new List<ClienteFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBycpfCliente", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_cpfCliente", SqlDbType.VarChar, 50)).Value = Param_cpfCliente;
//////////                        this.Cmd.Connection.Open();
//////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////////                        {
//////////                            if (!dr.HasRows) return null;
//////////                            while (dr.Read())
//////////                            {
//////////                               arrayList.Add(GetDataFromReader( dr ));
//////////                            }
//////////                        }
//////////                    }
//////////                }
//////////
//////////                return arrayList;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return null;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return null;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////
//////////        #region Selecionando dados da tabela através do campo "rgCliente" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo rgCliente.
//////////        /// </summary>
//////////        /// <param name="Param_rgCliente">string</param>
//////////        /// <returns>List ClienteFields</returns> 
//////////        public List<ClienteFields> FindByrgCliente(
//////////                               string Param_rgCliente )
//////////        {
//////////            List<ClienteFields> arrayList = new List<ClienteFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindByrgCliente", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_rgCliente", SqlDbType.VarChar, 50)).Value = Param_rgCliente;
//////////                        this.Cmd.Connection.Open();
//////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////////                        {
//////////                            if (!dr.HasRows) return null;
//////////                            while (dr.Read())
//////////                            {
//////////                               arrayList.Add(GetDataFromReader( dr ));
//////////                            }
//////////                        }
//////////                    }
//////////                }
//////////
//////////                return arrayList;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return null;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return null;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////
//////////        #region Selecionando dados da tabela através do campo "statusCliente" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo statusCliente.
//////////        /// </summary>
//////////        /// <param name="Param_statusCliente">string</param>
//////////        /// <returns>List ClienteFields</returns> 
//////////        public List<ClienteFields> FindBystatusCliente(
//////////                               string Param_statusCliente )
//////////        {
//////////            List<ClienteFields> arrayList = new List<ClienteFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindBystatusCliente", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_statusCliente", SqlDbType.VarChar, 2)).Value = Param_statusCliente;
//////////                        this.Cmd.Connection.Open();
//////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////////                        {
//////////                            if (!dr.HasRows) return null;
//////////                            while (dr.Read())
//////////                            {
//////////                               arrayList.Add(GetDataFromReader( dr ));
//////////                            }
//////////                        }
//////////                    }
//////////                }
//////////
//////////                return arrayList;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return null;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return null;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////
//////////        #region Selecionando dados da tabela através do campo "fkSubGrupoCliente" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkSubGrupoCliente.
//////////        /// </summary>
//////////        /// <param name="Param_fkSubGrupoCliente">int</param>
//////////        /// <returns>List ClienteFields</returns> 
//////////        public List<ClienteFields> FindByfkSubGrupoCliente(
//////////                               int Param_fkSubGrupoCliente )
//////////        {
//////////            List<ClienteFields> arrayList = new List<ClienteFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Cliente_FindByfkSubGrupoCliente", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkSubGrupoCliente", SqlDbType.Int)).Value = Param_fkSubGrupoCliente;
//////////                        this.Cmd.Connection.Open();
//////////                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
//////////                        {
//////////                            if (!dr.HasRows) return null;
//////////                            while (dr.Read())
//////////                            {
//////////                               arrayList.Add(GetDataFromReader( dr ));
//////////                            }
//////////                        }
//////////                    }
//////////                }
//////////
//////////                return arrayList;
//////////
//////////            }
//////////            catch (SqlException e)
//////////            {
//////////                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: Código do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
//////////                this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: {0}.",  e.Message);
//////////                return null;
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return null;
//////////            }
//////////            finally
//////////            {
//////////                if (this.Conn != null)
//////////                  if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////            }
//////////        }
//////////
//////////        #endregion
//////////
//////////
//////////
//////////        #region Função GetDataFromReader
//////////
//////////        /// <summary> 
//////////        /// Retorna um objeto ClienteFields preenchido com os valores dos campos do SqlDataReader
//////////        /// </summary>
//////////        /// <param name="dr">SqlDataReader - Preenche o objeto ClienteFields </param>
//////////        /// <returns>ClienteFields</returns>
//////////        private ClienteFields GetDataFromReader( SqlDataReader dr )
//////////        {
//////////            ClienteFields infoFields = new ClienteFields();
//////////
//////////            if (!dr.IsDBNull(0))
//////////            { infoFields.idCliente = dr.GetInt32(0); }
//////////            else
//////////            { infoFields.idCliente = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(1))
//////////            { infoFields.nomeCliente = dr.GetString(1); }
//////////            else
//////////            { infoFields.nomeCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(2))
//////////            { infoFields.enderecoClienteA = dr.GetString(2); }
//////////            else
//////////            { infoFields.enderecoClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(3))
//////////            { infoFields.enderecoClienteB = dr.GetString(3); }
//////////            else
//////////            { infoFields.enderecoClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(4))
//////////            { infoFields.enderecoClienteC = dr.GetString(4); }
//////////            else
//////////            { infoFields.enderecoClienteC = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(5))
//////////            { infoFields.bairroClienteA = dr.GetString(5); }
//////////            else
//////////            { infoFields.bairroClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(6))
//////////            { infoFields.bairroClienteB = dr.GetString(6); }
//////////            else
//////////            { infoFields.bairroClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(7))
//////////            { infoFields.bairroClientec = dr.GetString(7); }
//////////            else
//////////            { infoFields.bairroClientec = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(8))
//////////            { infoFields.cidadeClienteA = dr.GetString(8); }
//////////            else
//////////            { infoFields.cidadeClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(9))
//////////            { infoFields.cidadeClienteB = dr.GetString(9); }
//////////            else
//////////            { infoFields.cidadeClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(10))
//////////            { infoFields.cidadeClienteC = dr.GetString(10); }
//////////            else
//////////            { infoFields.cidadeClienteC = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(11))
//////////            { infoFields.estadoClienteA = dr.GetString(11); }
//////////            else
//////////            { infoFields.estadoClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(12))
//////////            { infoFields.estadoClienteB = dr.GetString(12); }
//////////            else
//////////            { infoFields.estadoClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(13))
//////////            { infoFields.estadoClienteC = dr.GetString(13); }
//////////            else
//////////            { infoFields.estadoClienteC = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(14))
//////////            { infoFields.cepClienteA = dr.GetString(14); }
//////////            else
//////////            { infoFields.cepClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(15))
//////////            { infoFields.cepClienteB = dr.GetString(15); }
//////////            else
//////////            { infoFields.cepClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(16))
//////////            { infoFields.cepClienteC = dr.GetString(16); }
//////////            else
//////////            { infoFields.cepClienteC = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(17))
//////////            { infoFields.telefoneClienteA = dr.GetString(17); }
//////////            else
//////////            { infoFields.telefoneClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(18))
//////////            { infoFields.telefoneClienteB = dr.GetString(18); }
//////////            else
//////////            { infoFields.telefoneClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(19))
//////////            { infoFields.telefoneClienteC = dr.GetString(19); }
//////////            else
//////////            { infoFields.telefoneClienteC = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(20))
//////////            { infoFields.telefoneClienteD = dr.GetString(20); }
//////////            else
//////////            { infoFields.telefoneClienteD = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(21))
//////////            { infoFields.celularClienteA = dr.GetString(21); }
//////////            else
//////////            { infoFields.celularClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(22))
//////////            { infoFields.celularClienteB = dr.GetString(22); }
//////////            else
//////////            { infoFields.celularClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(23))
//////////            { infoFields.celularClienteC = dr.GetString(23); }
//////////            else
//////////            { infoFields.celularClienteC = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(24))
//////////            { infoFields.complementoCliente = dr.GetString(24); }
//////////            else
//////////            { infoFields.complementoCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(25))
//////////            { infoFields.dataNascimentoCliente = dr.GetDateTime(25); }
//////////            else
//////////            { infoFields.dataNascimentoCliente = DateTime.MinValue; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(26))
//////////            { infoFields.emailClienteA = dr.GetString(26); }
//////////            else
//////////            { infoFields.emailClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(27))
//////////            { infoFields.emailClienteB = dr.GetString(27); }
//////////            else
//////////            { infoFields.emailClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(28))
//////////            { infoFields.contatoClienteA = dr.GetString(28); }
//////////            else
//////////            { infoFields.contatoClienteA = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(29))
//////////            { infoFields.contatoClienteB = dr.GetString(29); }
//////////            else
//////////            { infoFields.contatoClienteB = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(30))
//////////            { infoFields.contatoClienteC = dr.GetString(30); }
//////////            else
//////////            { infoFields.contatoClienteC = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(31))
//////////            { infoFields.cnpjCliente = dr.GetString(31); }
//////////            else
//////////            { infoFields.cnpjCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(32))
//////////            { infoFields.cpfCliente = dr.GetString(32); }
//////////            else
//////////            { infoFields.cpfCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(33))
//////////            { infoFields.rgCliente = dr.GetString(33); }
//////////            else
//////////            { infoFields.rgCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(34))
//////////            { infoFields.inscEstadualCliente = dr.GetString(34); }
//////////            else
//////////            { infoFields.inscEstadualCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(35))
//////////            { infoFields.observacoesCliente = dr.GetString(35); }
//////////            else
//////////            { infoFields.observacoesCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(36))
//////////            { infoFields.dataCadastroCliente = dr.GetDateTime(36); }
//////////            else
//////////            { infoFields.dataCadastroCliente = DateTime.MinValue; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(37))
//////////            { infoFields.tipoCliente = dr.GetString(37); }
//////////            else
//////////            { infoFields.tipoCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(38))
//////////            { infoFields.statusCliente = dr.GetString(38); }
//////////            else
//////////            { infoFields.statusCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(39))
//////////            { infoFields.fkSubGrupoCliente = dr.GetInt32(39); }
//////////            else
//////////            { infoFields.fkSubGrupoCliente = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(40))
//////////            { infoFields.dataUltimaCompraCliente = dr.GetDateTime(40); }
//////////            else
//////////            { infoFields.dataUltimaCompraCliente = DateTime.MinValue; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(41))
//////////            { infoFields.numeroCasaCliente = dr.GetString(41); }
//////////            else
//////////            { infoFields.numeroCasaCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(42))
//////////            { infoFields.faxCliente = dr.GetString(42); }
//////////            else
//////////            { infoFields.faxCliente = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(43))
//////////            { infoFields.dataNascimentoClienteA = dr.GetDateTime(43); }
//////////            else
//////////            { infoFields.dataNascimentoClienteA = DateTime.MinValue; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(44))
//////////            { infoFields.dataNascimentoClienteB = dr.GetDateTime(44); }
//////////            else
//////////            { infoFields.dataNascimentoClienteB = DateTime.MinValue; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(45))
//////////            { infoFields.dataNascimentoClienteC = dr.GetDateTime(45); }
//////////            else
//////////            { infoFields.dataNascimentoClienteC = DateTime.MinValue; }
//////////
//////////
//////////            return infoFields;
//////////        }
//////////        #endregion
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////
//////////        #region Função GetAllParameters
//////////
//////////        /// <summary> 
//////////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
//////////        /// </summary>
//////////        /// <param name="FieldInfo">Objeto ClienteFields</param>
//////////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
//////////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
//////////        private SqlParameter[] GetAllParameters( ClienteFields FieldInfo, SQLMode Modo )
//////////        {
//////////            SqlParameter[] Parameters;
//////////
//////////            switch (Modo)
//////////            {
//////////                case SQLMode.SelectORDelete:
//////////                    Parameters = new SqlParameter[1];
//////////                    for (int I = 0; I < Parameters.Length; I++)
//////////                       Parameters[I] = new SqlParameter();
//////////                    //Field idCliente
//////////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////////                    Parameters[0].ParameterName = "@Param_idCliente";
//////////                    Parameters[0].Value = FieldInfo.idCliente;
//////////
//////////                    return Parameters;
//////////
//////////                default:
//////////                    Parameters = new SqlParameter[46];
//////////                    for (int I = 0; I < Parameters.Length; I++)
//////////                       Parameters[I] = new SqlParameter();
//////////                    break;
//////////            }
//////////
//////////            //Field idCliente
//////////            Parameters[0].SqlDbType = SqlDbType.Int;
//////////            Parameters[0].ParameterName = "@Param_idCliente";
//////////            Parameters[0].Value = FieldInfo.idCliente;
//////////
//////////            //Field nomeCliente
//////////            Parameters[1].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[1].ParameterName = "@Param_nomeCliente";
//////////            if (( FieldInfo.nomeCliente == null ) || ( FieldInfo.nomeCliente == string.Empty ))
//////////            { Parameters[1].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[1].Value = FieldInfo.nomeCliente; }
//////////            Parameters[1].Size = 255;
//////////
//////////            //Field enderecoClienteA
//////////            Parameters[2].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[2].ParameterName = "@Param_enderecoClienteA";
//////////            if (( FieldInfo.enderecoClienteA == null ) || ( FieldInfo.enderecoClienteA == string.Empty ))
//////////            { Parameters[2].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[2].Value = FieldInfo.enderecoClienteA; }
//////////            Parameters[2].Size = 255;
//////////
//////////            //Field enderecoClienteB
//////////            Parameters[3].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[3].ParameterName = "@Param_enderecoClienteB";
//////////            if (( FieldInfo.enderecoClienteB == null ) || ( FieldInfo.enderecoClienteB == string.Empty ))
//////////            { Parameters[3].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[3].Value = FieldInfo.enderecoClienteB; }
//////////            Parameters[3].Size = 255;
//////////
//////////            //Field enderecoClienteC
//////////            Parameters[4].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[4].ParameterName = "@Param_enderecoClienteC";
//////////            if (( FieldInfo.enderecoClienteC == null ) || ( FieldInfo.enderecoClienteC == string.Empty ))
//////////            { Parameters[4].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[4].Value = FieldInfo.enderecoClienteC; }
//////////            Parameters[4].Size = 255;
//////////
//////////            //Field bairroClienteA
//////////            Parameters[5].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[5].ParameterName = "@Param_bairroClienteA";
//////////            if (( FieldInfo.bairroClienteA == null ) || ( FieldInfo.bairroClienteA == string.Empty ))
//////////            { Parameters[5].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[5].Value = FieldInfo.bairroClienteA; }
//////////            Parameters[5].Size = 255;
//////////
//////////            //Field bairroClienteB
//////////            Parameters[6].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[6].ParameterName = "@Param_bairroClienteB";
//////////            if (( FieldInfo.bairroClienteB == null ) || ( FieldInfo.bairroClienteB == string.Empty ))
//////////            { Parameters[6].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[6].Value = FieldInfo.bairroClienteB; }
//////////            Parameters[6].Size = 255;
//////////
//////////            //Field bairroClientec
//////////            Parameters[7].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[7].ParameterName = "@Param_bairroClientec";
//////////            if (( FieldInfo.bairroClientec == null ) || ( FieldInfo.bairroClientec == string.Empty ))
//////////            { Parameters[7].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[7].Value = FieldInfo.bairroClientec; }
//////////            Parameters[7].Size = 255;
//////////
//////////            //Field cidadeClienteA
//////////            Parameters[8].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[8].ParameterName = "@Param_cidadeClienteA";
//////////            if (( FieldInfo.cidadeClienteA == null ) || ( FieldInfo.cidadeClienteA == string.Empty ))
//////////            { Parameters[8].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[8].Value = FieldInfo.cidadeClienteA; }
//////////            Parameters[8].Size = 255;
//////////
//////////            //Field cidadeClienteB
//////////            Parameters[9].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[9].ParameterName = "@Param_cidadeClienteB";
//////////            if (( FieldInfo.cidadeClienteB == null ) || ( FieldInfo.cidadeClienteB == string.Empty ))
//////////            { Parameters[9].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[9].Value = FieldInfo.cidadeClienteB; }
//////////            Parameters[9].Size = 255;
//////////
//////////            //Field cidadeClienteC
//////////            Parameters[10].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[10].ParameterName = "@Param_cidadeClienteC";
//////////            if (( FieldInfo.cidadeClienteC == null ) || ( FieldInfo.cidadeClienteC == string.Empty ))
//////////            { Parameters[10].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[10].Value = FieldInfo.cidadeClienteC; }
//////////            Parameters[10].Size = 255;
//////////
//////////            //Field estadoClienteA
//////////            Parameters[11].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[11].ParameterName = "@Param_estadoClienteA";
//////////            if (( FieldInfo.estadoClienteA == null ) || ( FieldInfo.estadoClienteA == string.Empty ))
//////////            { Parameters[11].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[11].Value = FieldInfo.estadoClienteA; }
//////////            Parameters[11].Size = 2;
//////////
//////////            //Field estadoClienteB
//////////            Parameters[12].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[12].ParameterName = "@Param_estadoClienteB";
//////////            if (( FieldInfo.estadoClienteB == null ) || ( FieldInfo.estadoClienteB == string.Empty ))
//////////            { Parameters[12].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[12].Value = FieldInfo.estadoClienteB; }
//////////            Parameters[12].Size = 2;
//////////
//////////            //Field estadoClienteC
//////////            Parameters[13].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[13].ParameterName = "@Param_estadoClienteC";
//////////            if (( FieldInfo.estadoClienteC == null ) || ( FieldInfo.estadoClienteC == string.Empty ))
//////////            { Parameters[13].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[13].Value = FieldInfo.estadoClienteC; }
//////////            Parameters[13].Size = 2;
//////////
//////////            //Field cepClienteA
//////////            Parameters[14].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[14].ParameterName = "@Param_cepClienteA";
//////////            if (( FieldInfo.cepClienteA == null ) || ( FieldInfo.cepClienteA == string.Empty ))
//////////            { Parameters[14].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[14].Value = FieldInfo.cepClienteA; }
//////////            Parameters[14].Size = 9;
//////////
//////////            //Field cepClienteB
//////////            Parameters[15].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[15].ParameterName = "@Param_cepClienteB";
//////////            if (( FieldInfo.cepClienteB == null ) || ( FieldInfo.cepClienteB == string.Empty ))
//////////            { Parameters[15].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[15].Value = FieldInfo.cepClienteB; }
//////////            Parameters[15].Size = 9;
//////////
//////////            //Field cepClienteC
//////////            Parameters[16].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[16].ParameterName = "@Param_cepClienteC";
//////////            if (( FieldInfo.cepClienteC == null ) || ( FieldInfo.cepClienteC == string.Empty ))
//////////            { Parameters[16].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[16].Value = FieldInfo.cepClienteC; }
//////////            Parameters[16].Size = 9;
//////////
//////////            //Field telefoneClienteA
//////////            Parameters[17].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[17].ParameterName = "@Param_telefoneClienteA";
//////////            if (( FieldInfo.telefoneClienteA == null ) || ( FieldInfo.telefoneClienteA == string.Empty ))
//////////            { Parameters[17].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[17].Value = FieldInfo.telefoneClienteA; }
//////////            Parameters[17].Size = 50;
//////////
//////////            //Field telefoneClienteB
//////////            Parameters[18].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[18].ParameterName = "@Param_telefoneClienteB";
//////////            if (( FieldInfo.telefoneClienteB == null ) || ( FieldInfo.telefoneClienteB == string.Empty ))
//////////            { Parameters[18].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[18].Value = FieldInfo.telefoneClienteB; }
//////////            Parameters[18].Size = 50;
//////////
//////////            //Field telefoneClienteC
//////////            Parameters[19].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[19].ParameterName = "@Param_telefoneClienteC";
//////////            if (( FieldInfo.telefoneClienteC == null ) || ( FieldInfo.telefoneClienteC == string.Empty ))
//////////            { Parameters[19].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[19].Value = FieldInfo.telefoneClienteC; }
//////////            Parameters[19].Size = 50;
//////////
//////////            //Field telefoneClienteD
//////////            Parameters[20].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[20].ParameterName = "@Param_telefoneClienteD";
//////////            if (( FieldInfo.telefoneClienteD == null ) || ( FieldInfo.telefoneClienteD == string.Empty ))
//////////            { Parameters[20].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[20].Value = FieldInfo.telefoneClienteD; }
//////////            Parameters[20].Size = 50;
//////////
//////////            //Field celularClienteA
//////////            Parameters[21].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[21].ParameterName = "@Param_celularClienteA";
//////////            if (( FieldInfo.celularClienteA == null ) || ( FieldInfo.celularClienteA == string.Empty ))
//////////            { Parameters[21].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[21].Value = FieldInfo.celularClienteA; }
//////////            Parameters[21].Size = 50;
//////////
//////////            //Field celularClienteB
//////////            Parameters[22].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[22].ParameterName = "@Param_celularClienteB";
//////////            if (( FieldInfo.celularClienteB == null ) || ( FieldInfo.celularClienteB == string.Empty ))
//////////            { Parameters[22].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[22].Value = FieldInfo.celularClienteB; }
//////////            Parameters[22].Size = 50;
//////////
//////////            //Field celularClienteC
//////////            Parameters[23].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[23].ParameterName = "@Param_celularClienteC";
//////////            if (( FieldInfo.celularClienteC == null ) || ( FieldInfo.celularClienteC == string.Empty ))
//////////            { Parameters[23].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[23].Value = FieldInfo.celularClienteC; }
//////////            Parameters[23].Size = 50;
//////////
//////////            //Field complementoCliente
//////////            Parameters[24].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[24].ParameterName = "@Param_complementoCliente";
//////////            if (( FieldInfo.complementoCliente == null ) || ( FieldInfo.complementoCliente == string.Empty ))
//////////            { Parameters[24].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[24].Value = FieldInfo.complementoCliente; }
//////////            Parameters[24].Size = 100;
//////////
//////////            //Field dataNascimentoCliente
//////////            Parameters[25].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[25].ParameterName = "@Param_dataNascimentoCliente";
//////////            if ( FieldInfo.dataNascimentoCliente == DateTime.MinValue )
//////////            { Parameters[25].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[25].Value = FieldInfo.dataNascimentoCliente; }
//////////
//////////            //Field emailClienteA
//////////            Parameters[26].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[26].ParameterName = "@Param_emailClienteA";
//////////            if (( FieldInfo.emailClienteA == null ) || ( FieldInfo.emailClienteA == string.Empty ))
//////////            { Parameters[26].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[26].Value = FieldInfo.emailClienteA; }
//////////            Parameters[26].Size = 255;
//////////
//////////            //Field emailClienteB
//////////            Parameters[27].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[27].ParameterName = "@Param_emailClienteB";
//////////            if (( FieldInfo.emailClienteB == null ) || ( FieldInfo.emailClienteB == string.Empty ))
//////////            { Parameters[27].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[27].Value = FieldInfo.emailClienteB; }
//////////            Parameters[27].Size = 255;
//////////
//////////            //Field contatoClienteA
//////////            Parameters[28].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[28].ParameterName = "@Param_contatoClienteA";
//////////            if (( FieldInfo.contatoClienteA == null ) || ( FieldInfo.contatoClienteA == string.Empty ))
//////////            { Parameters[28].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[28].Value = FieldInfo.contatoClienteA; }
//////////            Parameters[28].Size = 255;
//////////
//////////            //Field contatoClienteB
//////////            Parameters[29].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[29].ParameterName = "@Param_contatoClienteB";
//////////            if (( FieldInfo.contatoClienteB == null ) || ( FieldInfo.contatoClienteB == string.Empty ))
//////////            { Parameters[29].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[29].Value = FieldInfo.contatoClienteB; }
//////////            Parameters[29].Size = 255;
//////////
//////////            //Field contatoClienteC
//////////            Parameters[30].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[30].ParameterName = "@Param_contatoClienteC";
//////////            if (( FieldInfo.contatoClienteC == null ) || ( FieldInfo.contatoClienteC == string.Empty ))
//////////            { Parameters[30].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[30].Value = FieldInfo.contatoClienteC; }
//////////            Parameters[30].Size = 255;
//////////
//////////            //Field cnpjCliente
//////////            Parameters[31].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[31].ParameterName = "@Param_cnpjCliente";
//////////            if (( FieldInfo.cnpjCliente == null ) || ( FieldInfo.cnpjCliente == string.Empty ))
//////////            { Parameters[31].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[31].Value = FieldInfo.cnpjCliente; }
//////////            Parameters[31].Size = 50;
//////////
//////////            //Field cpfCliente
//////////            Parameters[32].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[32].ParameterName = "@Param_cpfCliente";
//////////            if (( FieldInfo.cpfCliente == null ) || ( FieldInfo.cpfCliente == string.Empty ))
//////////            { Parameters[32].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[32].Value = FieldInfo.cpfCliente; }
//////////            Parameters[32].Size = 50;
//////////
//////////            //Field rgCliente
//////////            Parameters[33].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[33].ParameterName = "@Param_rgCliente";
//////////            if (( FieldInfo.rgCliente == null ) || ( FieldInfo.rgCliente == string.Empty ))
//////////            { Parameters[33].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[33].Value = FieldInfo.rgCliente; }
//////////            Parameters[33].Size = 50;
//////////
//////////            //Field inscEstadualCliente
//////////            Parameters[34].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[34].ParameterName = "@Param_inscEstadualCliente";
//////////            if (( FieldInfo.inscEstadualCliente == null ) || ( FieldInfo.inscEstadualCliente == string.Empty ))
//////////            { Parameters[34].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[34].Value = FieldInfo.inscEstadualCliente; }
//////////            Parameters[34].Size = 50;
//////////
//////////            //Field observacoesCliente
//////////            Parameters[35].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[35].ParameterName = "@Param_observacoesCliente";
//////////            if (( FieldInfo.observacoesCliente == null ) || ( FieldInfo.observacoesCliente == string.Empty ))
//////////            { Parameters[35].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[35].Value = FieldInfo.observacoesCliente; }
//////////            Parameters[35].Size = 300;
//////////
//////////            //Field dataCadastroCliente
//////////            Parameters[36].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[36].ParameterName = "@Param_dataCadastroCliente";
//////////            if ( FieldInfo.dataCadastroCliente == DateTime.MinValue )
//////////            { Parameters[36].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[36].Value = FieldInfo.dataCadastroCliente; }
//////////
//////////            //Field tipoCliente
//////////            Parameters[37].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[37].ParameterName = "@Param_tipoCliente";
//////////            if (( FieldInfo.tipoCliente == null ) || ( FieldInfo.tipoCliente == string.Empty ))
//////////            { Parameters[37].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[37].Value = FieldInfo.tipoCliente; }
//////////            Parameters[37].Size = 20;
//////////
//////////            //Field statusCliente
//////////            Parameters[38].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[38].ParameterName = "@Param_statusCliente";
//////////            if (( FieldInfo.statusCliente == null ) || ( FieldInfo.statusCliente == string.Empty ))
//////////            { Parameters[38].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[38].Value = FieldInfo.statusCliente; }
//////////            Parameters[38].Size = 2;
//////////
//////////            //Field fkSubGrupoCliente
//////////            Parameters[39].SqlDbType = SqlDbType.Int;
//////////            Parameters[39].ParameterName = "@Param_fkSubGrupoCliente";
//////////            Parameters[39].Value = FieldInfo.fkSubGrupoCliente;
//////////
//////////            //Field dataUltimaCompraCliente
//////////            Parameters[40].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[40].ParameterName = "@Param_dataUltimaCompraCliente";
//////////            if ( FieldInfo.dataUltimaCompraCliente == DateTime.MinValue )
//////////            { Parameters[40].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[40].Value = FieldInfo.dataUltimaCompraCliente; }
//////////
//////////            //Field numeroCasaCliente
//////////            Parameters[41].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[41].ParameterName = "@Param_numeroCasaCliente";
//////////            if (( FieldInfo.numeroCasaCliente == null ) || ( FieldInfo.numeroCasaCliente == string.Empty ))
//////////            { Parameters[41].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[41].Value = FieldInfo.numeroCasaCliente; }
//////////            Parameters[41].Size = 30;
//////////
//////////            //Field faxCliente
//////////            Parameters[42].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[42].ParameterName = "@Param_faxCliente";
//////////            if (( FieldInfo.faxCliente == null ) || ( FieldInfo.faxCliente == string.Empty ))
//////////            { Parameters[42].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[42].Value = FieldInfo.faxCliente; }
//////////            Parameters[42].Size = 50;
//////////
//////////            //Field dataNascimentoClienteA
//////////            Parameters[43].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[43].ParameterName = "@Param_dataNascimentoClienteA";
//////////            if ( FieldInfo.dataNascimentoClienteA == DateTime.MinValue )
//////////            { Parameters[43].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[43].Value = FieldInfo.dataNascimentoClienteA; }
//////////
//////////            //Field dataNascimentoClienteB
//////////            Parameters[44].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[44].ParameterName = "@Param_dataNascimentoClienteB";
//////////            if ( FieldInfo.dataNascimentoClienteB == DateTime.MinValue )
//////////            { Parameters[44].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[44].Value = FieldInfo.dataNascimentoClienteB; }
//////////
//////////            //Field dataNascimentoClienteC
//////////            Parameters[45].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[45].ParameterName = "@Param_dataNascimentoClienteC";
//////////            if ( FieldInfo.dataNascimentoClienteC == DateTime.MinValue )
//////////            { Parameters[45].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[45].Value = FieldInfo.dataNascimentoClienteC; }
//////////
//////////            return Parameters;
//////////        }
//////////        #endregion
//////////
//////////
//////////
//////////
//////////
//////////        #region IDisposable Members 
//////////
//////////        bool disposed = false;
//////////
//////////        public void Dispose()
//////////        {
//////////            Dispose(true);
//////////            GC.SuppressFinalize(this);
//////////        }
//////////
//////////        ~ClienteControl() 
//////////        { 
//////////            Dispose(false); 
//////////        }
//////////
//////////        private void Dispose(bool disposing) 
//////////        {
//////////            if (!this.disposed)
//////////            {
//////////                if (disposing) 
//////////                {
//////////                    if (this.Conn != null)
//////////                        if (this.Conn.State == ConnectionState.Open) { this.Conn.Dispose(); }
//////////                }
//////////            }
//////////
//////////        }
//////////        #endregion 
//////////
//////////
//////////
//////////    }
//////////
//////////}
