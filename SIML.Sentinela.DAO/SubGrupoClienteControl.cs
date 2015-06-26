using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using SIML.Sentnela;

namespace SIML.Sentinela
{


    /// <summary> 
    /// Tabela: SubGrupoCliente  
    /// Autor: DAL Creator .net 
    /// Data de cria��o: 02/05/2013 19:05:56 
    /// Descri��o: Classe respons�vel pela perssit�ncia de dados. Utiliza a classe "SubGrupoClienteFields". 
    /// </summary> 
    public class SubGrupoClienteControl : IDisposable 
    {

        #region String de conex�o 
        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
        #endregion


        #region Propriedade que armazena erros de execu��o 
        private string _ErrorMessage;
        public string ErrorMessage { get { return _ErrorMessage; } }
        #endregion


        #region Objetos de conex�o 
        SqlConnection Conn;
        SqlCommand Cmd;
        SqlTransaction Tran;
        #endregion


        #region Func�es que retornam Conex�es e Transa��es 

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
        /// Representa o procedimento que est� sendo executado na tabela.
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


        public SubGrupoClienteControl() {}


        #region Inserindo dados na tabela 

        /// <summary> 
        /// Grava/Persiste um novo objeto SubGrupoClienteFields no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser gravado.Caso o par�metro solicite a express�o "ref", ser� adicionado um novo valor a algum campo auto incremento.</param>
        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Add( ref SubGrupoClienteFields FieldInfo )
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Add", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
                this.Tran.Commit();
                FieldInfo.idSubGrupoCliente = (int)this.Cmd.Parameters["@Param_idSubGrupoCliente"].Value;
                return true;

            }
            catch (SqlException e)
            {
                this.Tran.Rollback();
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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


        #region Inserindo dados na tabela utilizando conex�o e transa��o externa (compartilhada) 

        /// <summary> 
        /// Grava/Persiste um novo objeto SubGrupoClienteFields no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser gravado.Caso o par�metro solicite a express�o "ref", ser� adicionado um novo valor a algum campo auto incremento.</param>
        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref SubGrupoClienteFields FieldInfo )
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Add", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
                FieldInfo.idSubGrupoCliente = (int)this.Cmd.Parameters["@Param_idSubGrupoCliente"].Value;
                return true;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
        /// Grava/Persiste as altera��es em um objeto SubGrupoClienteFields no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser alterado.</param>
        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Update( SubGrupoClienteFields FieldInfo )
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Update", this.Conn, this.Tran);
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
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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


        #region Editando dados na tabela utilizando conex�o e transa��o externa (compartilhada) 

        /// <summary> 
        /// Grava/Persiste as altera��es em um objeto SubGrupoClienteFields no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser alterado.</param>
        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, SubGrupoClienteFields FieldInfo )
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Update", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
                return true;
            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_DeleteAll", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                this.Tran.Commit();
                return true;
            }
            catch (SqlException e)
            {
                this.Tran.Rollback();
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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


        #region Excluindo todos os dados da tabela utilizando conex�o e transa��o externa (compartilhada)

        /// <summary> 
        /// Exclui todos os registros da tabela
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_DeleteAll", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                return true;
            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser exclu�do.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( SubGrupoClienteFields FieldInfo )
        {
            return Delete(FieldInfo.idSubGrupoCliente);
        }

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="Param_idSubGrupoCliente">int</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete(
                                     int Param_idSubGrupoCliente)
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Delete", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.Add(new SqlParameter("@Param_idSubGrupoCliente", SqlDbType.Int)).Value = Param_idSubGrupoCliente;
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                this.Tran.Commit();
                return true;
            }
            catch (SqlException e)
            {
                this.Tran.Rollback();
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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


        #region Excluindo dados da tabela utilizando conex�o e transa��o externa (compartilhada)

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser exclu�do.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, SubGrupoClienteFields FieldInfo )
        {
            return Delete(ConnIn, TranIn, FieldInfo.idSubGrupoCliente);
        }

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="Param_idSubGrupoCliente">int</param>
        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
                                     int Param_idSubGrupoCliente)
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Delete", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.Add(new SqlParameter("@Param_idSubGrupoCliente", SqlDbType.Int)).Value = Param_idSubGrupoCliente;
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
                return true;
            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
        /// Retorna um objeto SubGrupoClienteFields atrav�s da chave prim�ria passada como par�metro
        /// </summary>
        /// <param name="Param_idSubGrupoCliente">int</param>
        /// <returns>Objeto SubGrupoClienteFields</returns> 
        public SubGrupoClienteFields GetItem(
                                     int Param_idSubGrupoCliente)
        {
            SubGrupoClienteFields infoFields = new SubGrupoClienteFields();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Select", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idSubGrupoCliente", SqlDbType.Int)).Value = Param_idSubGrupoCliente;
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
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto SubGrupoClienteFields.
        /// </summary>
        /// <returns>List de objetos SubGrupoClienteFields</returns> 
        public List<SubGrupoClienteFields> GetAll()
        {
            List<SubGrupoClienteFields> arrayInfo = new List<SubGrupoClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_GetAll", this.Conn))
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
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_CountAll", this.Conn))
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
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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


        #region Selecionando dados da tabela atrav�s do campo "fkGrupoCliente" 

        /// <summary> 
        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkGrupoCliente.
        /// </summary>
        /// <param name="Param_fkGrupoCliente">int</param>
        /// <returns>List SubGrupoClienteFields</returns> 
        public List<SubGrupoClienteFields> FindByfkGrupoCliente(
                               int Param_fkGrupoCliente )
        {
            List<SubGrupoClienteFields> arrayList = new List<SubGrupoClienteFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_FindByfkGrupoCliente", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkGrupoCliente", SqlDbType.Int)).Value = Param_fkGrupoCliente;
                        this.Cmd.Connection.Open();
                        using (SqlDataReader dr = this.Cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (!dr.HasRows) return null;
                            while (dr.Read())
                            {
                               arrayList.Add(GetDataFromReader( dr ));
                            }
                        }
                    }
                }

                return arrayList;

            }
            catch (SqlException e)
            {
                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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



        #region Fun��o GetDataFromReader

        /// <summary> 
        /// Retorna um objeto SubGrupoClienteFields preenchido com os valores dos campos do SqlDataReader
        /// </summary>
        /// <param name="dr">SqlDataReader - Preenche o objeto SubGrupoClienteFields </param>
        /// <returns>SubGrupoClienteFields</returns>
        private SubGrupoClienteFields GetDataFromReader( SqlDataReader dr )
        {
            SubGrupoClienteFields infoFields = new SubGrupoClienteFields();

            if (!dr.IsDBNull(0))
            { infoFields.idSubGrupoCliente = dr.GetInt32(0); }
            else
            { infoFields.idSubGrupoCliente = 0; }



            if (!dr.IsDBNull(1))
            { infoFields.descricaoSubGrupoCliente = dr.GetString(1); }
            else
            { infoFields.descricaoSubGrupoCliente = string.Empty; }



            if (!dr.IsDBNull(2))
            { infoFields.fkGrupoCliente = dr.GetInt32(2); }
            else
            { infoFields.fkGrupoCliente = 0; }



            if (!dr.IsDBNull(3))
            { infoFields.valorIndiceInicial = dr.GetDecimal(3); }
            else
            { infoFields.valorIndiceInicial = 0; }



            if (!dr.IsDBNull(4))
            { infoFields.valorIndiceFinal = dr.GetDecimal(4); }
            else
            { infoFields.valorIndiceFinal = 0; }


            return infoFields;
        }
        #endregion
















        #region Fun��o GetAllParameters

        /// <summary> 
        /// Retorna um array de par�metros com campos para atualiza��o, sele��o e inser��o no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto SubGrupoClienteFields</param>
        /// <param name="Modo">Tipo de oepra��o a ser executada no banco de dados</param>
        /// <returns>SqlParameter[] - Array de par�metros</returns> 
        private SqlParameter[] GetAllParameters( SubGrupoClienteFields FieldInfo, SQLMode Modo )
        {
            SqlParameter[] Parameters;

            switch (Modo)
            {
                case SQLMode.Add:
                    Parameters = new SqlParameter[5];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idSubGrupoCliente
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].Direction = ParameterDirection.Output;
                    Parameters[0].ParameterName = "@Param_idSubGrupoCliente";
                    Parameters[0].Value = DBNull.Value;

                    break;

                case SQLMode.Update:
                    Parameters = new SqlParameter[5];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idSubGrupoCliente
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].ParameterName = "@Param_idSubGrupoCliente";
                    Parameters[0].Value = FieldInfo.idSubGrupoCliente;

                    break;

                case SQLMode.SelectORDelete:
                    Parameters = new SqlParameter[1];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idSubGrupoCliente
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].ParameterName = "@Param_idSubGrupoCliente";
                    Parameters[0].Value = FieldInfo.idSubGrupoCliente;

                    return Parameters;

                default:
                    Parameters = new SqlParameter[5];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    break;
            }

            //Field descricaoSubGrupoCliente
            Parameters[1].SqlDbType = SqlDbType.VarChar;
            Parameters[1].ParameterName = "@Param_descricaoSubGrupoCliente";
            if (( FieldInfo.descricaoSubGrupoCliente == null ) || ( FieldInfo.descricaoSubGrupoCliente == string.Empty ))
            { Parameters[1].Value = DBNull.Value; }
            else
            { Parameters[1].Value = FieldInfo.descricaoSubGrupoCliente; }
            Parameters[1].Size = 150;

            //Field fkGrupoCliente
            Parameters[2].SqlDbType = SqlDbType.Int;
            Parameters[2].ParameterName = "@Param_fkGrupoCliente";
            Parameters[2].Value = FieldInfo.fkGrupoCliente;

            //Field valorIndiceInicial
            Parameters[3].SqlDbType = SqlDbType.Decimal;
            Parameters[3].ParameterName = "@Param_valorIndiceInicial";
            Parameters[3].Value = FieldInfo.valorIndiceInicial;

            //Field valorIndiceFinal
            Parameters[4].SqlDbType = SqlDbType.Decimal;
            Parameters[4].ParameterName = "@Param_valorIndiceFinal";
            Parameters[4].Value = FieldInfo.valorIndiceFinal;

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

        ~SubGrupoClienteControl() 
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





//Projeto substitu�do ------------------------
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
//    /// Tabela: SubGrupoCliente  
//    /// Autor: DAL Creator .net 
//    /// Data de cria��o: 25/04/2013 16:44:06 
//    /// Descri��o: Classe respons�vel pela perssit�ncia de dados. Utiliza a classe "SubGrupoClienteFields". 
//    /// </summary> 
//    public class SubGrupoClienteControl : IDisposable 
//    {
//
//        #region String de conex�o 
//        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
//        #endregion
//
//
//        #region Propriedade que armazena erros de execu��o 
//        private string _ErrorMessage;
//        public string ErrorMessage { get { return _ErrorMessage; } }
//        #endregion
//
//
//        #region Objetos de conex�o 
//        SqlConnection Conn;
//        SqlCommand Cmd;
//        SqlTransaction Tran;
//        #endregion
//
//
//        #region Func�es que retornam Conex�es e Transa��es 
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
//        /// Representa o procedimento que est� sendo executado na tabela.
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
//        public SubGrupoClienteControl() {}
//
//
//        #region Inserindo dados na tabela 
//
//        /// <summary> 
//        /// Grava/Persiste um novo objeto SubGrupoClienteFields no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser gravado.Caso o par�metro solicite a express�o "ref", ser� adicionado um novo valor a algum campo auto incremento.</param>
//        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Add( ref SubGrupoClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Add", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//                this.Tran.Commit();
//                FieldInfo.idSubGrupoCliente = (int)this.Cmd.Parameters["@Param_idSubGrupoCliente"].Value;
//                return true;
//
//            }
//            catch (SqlException e)
//            {
//                this.Tran.Rollback();
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        #region Inserindo dados na tabela utilizando conex�o e transa��o externa (compartilhada) 
//
//        /// <summary> 
//        /// Grava/Persiste um novo objeto SubGrupoClienteFields no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser gravado.Caso o par�metro solicite a express�o "ref", ser� adicionado um novo valor a algum campo auto incremento.</param>
//        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref SubGrupoClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Add", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//                FieldInfo.idSubGrupoCliente = (int)this.Cmd.Parameters["@Param_idSubGrupoCliente"].Value;
//                return true;
//
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar inserir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        /// Grava/Persiste as altera��es em um objeto SubGrupoClienteFields no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser alterado.</param>
//        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Update( SubGrupoClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Update", this.Conn, this.Tran);
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
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        #region Editando dados na tabela utilizando conex�o e transa��o externa (compartilhada) 
//
//        /// <summary> 
//        /// Grava/Persiste as altera��es em um objeto SubGrupoClienteFields no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser alterado.</param>
//        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, SubGrupoClienteFields FieldInfo )
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Update", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Update));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar atualizar registro!!");
//                return true;
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar atualizar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_DeleteAll", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                this.Tran.Commit();
//                return true;
//            }
//            catch (SqlException e)
//            {
//                this.Tran.Rollback();
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        #region Excluindo todos os dados da tabela utilizando conex�o e transa��o externa (compartilhada)
//
//        /// <summary> 
//        /// Exclui todos os registros da tabela
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
//        /// <returns>"true" = registros excluidos com sucesso, "false" = erro ao tentar excluir registros (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool DeleteAll(SqlConnection ConnIn, SqlTransaction TranIn)
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_DeleteAll", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                return true;
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser exclu�do.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( SubGrupoClienteFields FieldInfo )
//        {
//            return Delete(FieldInfo.idSubGrupoCliente);
//        }
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="Param_idSubGrupoCliente">int</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete(
//                                     int Param_idSubGrupoCliente)
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Delete", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.Add(new SqlParameter("@Param_idSubGrupoCliente", SqlDbType.Int)).Value = Param_idSubGrupoCliente;
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                this.Tran.Commit();
//                return true;
//            }
//            catch (SqlException e)
//            {
//                this.Tran.Rollback();
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        #region Excluindo dados da tabela utilizando conex�o e transa��o externa (compartilhada)
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto SubGrupoClienteFields a ser exclu�do.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, SubGrupoClienteFields FieldInfo )
//        {
//            return Delete(ConnIn, TranIn, FieldInfo.idSubGrupoCliente);
//        }
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="Param_idSubGrupoCliente">int</param>
//        /// <param name="ConnIn">Objeto SqlConnection respons�vel pela conex�o com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction respons�vel pela transa��o iniciada no banco de dados.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
//                                     int Param_idSubGrupoCliente)
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Delete", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.Add(new SqlParameter("@Param_idSubGrupoCliente", SqlDbType.Int)).Value = Param_idSubGrupoCliente;
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar excluir registro!!");
//                return true;
//            }
//            catch (SqlException e)
//            {
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar excluir o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        /// Retorna um objeto SubGrupoClienteFields atrav�s da chave prim�ria passada como par�metro
//        /// </summary>
//        /// <param name="Param_idSubGrupoCliente">int</param>
//        /// <returns>Objeto SubGrupoClienteFields</returns> 
//        public SubGrupoClienteFields GetItem(
//                                     int Param_idSubGrupoCliente)
//        {
//            SubGrupoClienteFields infoFields = new SubGrupoClienteFields();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_Select", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idSubGrupoCliente", SqlDbType.Int)).Value = Param_idSubGrupoCliente;
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
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto SubGrupoClienteFields.
//        /// </summary>
//        /// <returns>List de objetos SubGrupoClienteFields</returns> 
//        public List<SubGrupoClienteFields> GetAll()
//        {
//            List<SubGrupoClienteFields> arrayInfo = new List<SubGrupoClienteFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_GetAll", this.Conn))
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
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_CountAll", this.Conn))
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
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        #region Selecionando dados da tabela atrav�s do campo "fkGrupoCliente" 
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkGrupoCliente.
//        /// </summary>
//        /// <param name="Param_fkGrupoCliente">int</param>
//        /// <returns>SubGrupoClienteFields</returns> 
//        public SubGrupoClienteFields FindByfkGrupoCliente(
//                               int Param_fkGrupoCliente )
//        {
//            SubGrupoClienteFields infoFields = new SubGrupoClienteFields();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_SubGrupoCliente_FindByfkGrupoCliente", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkGrupoCliente", SqlDbType.Int)).Value = Param_fkGrupoCliente;
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
//                //this._ErrorMessage = string.Format(@"Houve um erro imprevisto ao tentar selecionar o(s) registro(s) solicitados: C�digo do erro: {0}, Mensagem: {1}, Procedimento: {2}, Linha do erro {3}.", e.ErrorCode, e.Message, e.Procedure, e.LineNumber);
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
//        #region Fun��o GetDataFromReader
//
//        /// <summary> 
//        /// Retorna um objeto SubGrupoClienteFields preenchido com os valores dos campos do SqlDataReader
//        /// </summary>
//        /// <param name="dr">SqlDataReader - Preenche o objeto SubGrupoClienteFields </param>
//        /// <returns>SubGrupoClienteFields</returns>
//        private SubGrupoClienteFields GetDataFromReader( SqlDataReader dr )
//        {
//            SubGrupoClienteFields infoFields = new SubGrupoClienteFields();
//
//            if (!dr.IsDBNull(0))
//            { infoFields.idSubGrupoCliente = dr.GetInt32(0); }
//            else
//            { infoFields.idSubGrupoCliente = 0; }
//
//
//
//            if (!dr.IsDBNull(1))
//            { infoFields.descricaoSubGrupoCliente = dr.GetString(1); }
//            else
//            { infoFields.descricaoSubGrupoCliente = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(2))
//            { infoFields.fkGrupoCliente = dr.GetInt32(2); }
//            else
//            { infoFields.fkGrupoCliente = 0; }
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
//        #region Fun��o GetAllParameters
//
//        /// <summary> 
//        /// Retorna um array de par�metros com campos para atualiza��o, sele��o e inser��o no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto SubGrupoClienteFields</param>
//        /// <param name="Modo">Tipo de oepra��o a ser executada no banco de dados</param>
//        /// <returns>SqlParameter[] - Array de par�metros</returns> 
//        private SqlParameter[] GetAllParameters( SubGrupoClienteFields FieldInfo, SQLMode Modo )
//        {
//            SqlParameter[] Parameters;
//
//            switch (Modo)
//            {
//                case SQLMode.Add:
//                    Parameters = new SqlParameter[3];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idSubGrupoCliente
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].Direction = ParameterDirection.Output;
//                    Parameters[0].ParameterName = "@Param_idSubGrupoCliente";
//                    Parameters[0].Value = DBNull.Value;
//
//                    break;
//
//                case SQLMode.Update:
//                    Parameters = new SqlParameter[3];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idSubGrupoCliente
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].ParameterName = "@Param_idSubGrupoCliente";
//                    Parameters[0].Value = FieldInfo.idSubGrupoCliente;
//
//                    break;
//
//                case SQLMode.SelectORDelete:
//                    Parameters = new SqlParameter[1];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idSubGrupoCliente
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].ParameterName = "@Param_idSubGrupoCliente";
//                    Parameters[0].Value = FieldInfo.idSubGrupoCliente;
//
//                    return Parameters;
//
//                default:
//                    Parameters = new SqlParameter[3];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    break;
//            }
//
//            //Field descricaoSubGrupoCliente
//            Parameters[1].SqlDbType = SqlDbType.VarChar;
//            Parameters[1].ParameterName = "@Param_descricaoSubGrupoCliente";
//            if (( FieldInfo.descricaoSubGrupoCliente == null ) || ( FieldInfo.descricaoSubGrupoCliente == string.Empty ))
//            { Parameters[1].Value = DBNull.Value; }
//            else
//            { Parameters[1].Value = FieldInfo.descricaoSubGrupoCliente; }
//            Parameters[1].Size = 150;
//
//            //Field fkGrupoCliente
//            Parameters[2].SqlDbType = SqlDbType.Int;
//            Parameters[2].ParameterName = "@Param_fkGrupoCliente";
//            Parameters[2].Value = FieldInfo.fkGrupoCliente;
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
//        ~SubGrupoClienteControl() 
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
//
//
//    }
//
//}
