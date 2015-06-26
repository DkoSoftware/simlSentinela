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
    /// Tabela: Pedido  
    /// Autor: DAL Creator .net 
    /// Data de criação: 24/05/2013 17:57:17 
    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "PedidoFields". 
    /// </summary> 
    public class PedidoControl : IDisposable 
    {

        #region String de conexão 
        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["StringConn"].ToString();
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


        public PedidoControl() {}


        #region Inserindo dados na tabela 

        /// <summary> 
        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Add( ref PedidoFields FieldInfo )
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_Pedido_Add", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
                this.Tran.Commit();
                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref PedidoFields FieldInfo )
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_Pedido_Add", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Update( PedidoFields FieldInfo )
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_Pedido_Update", this.Conn, this.Tran);
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
        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
        /// </summary>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_Pedido_Update", ConnIn, TranIn);
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
                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", this.Conn, this.Tran);
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
                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", ConnIn, TranIn);
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
        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( PedidoFields FieldInfo )
        {
            return Delete(FieldInfo.idPedido);
        }

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="Param_idPedido">int</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete(
                                     int Param_idPedido)
        {
            try
            {
                this.Conn = new SqlConnection(this.StrConnetionDB);
                this.Conn.Open();
                this.Tran = this.Conn.BeginTransaction();
                this.Cmd = new SqlCommand("Proc_Pedido_Delete", this.Conn, this.Tran);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
        {
            return Delete(ConnIn, TranIn, FieldInfo.idPedido);
        }

        /// <summary> 
        /// Exclui um registro da tabela no banco de dados
        /// </summary>
        /// <param name="Param_idPedido">int</param>
        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
                                     int Param_idPedido)
        {
            try
            {
                this.Cmd = new SqlCommand("Proc_Pedido_Delete", ConnIn, TranIn);
                this.Cmd.CommandType = CommandType.StoredProcedure;
                this.Cmd.Parameters.Clear();
                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
        /// Retorna um objeto PedidoFields através da chave primária passada como parâmetro
        /// </summary>
        /// <param name="Param_idPedido">int</param>
        /// <returns>Objeto PedidoFields</returns> 
        public PedidoFields GetItem(
                                     int Param_idPedido)
        {
            PedidoFields infoFields = new PedidoFields();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Pedido_Select", this.Conn))
                    {
                        this.Cmd.CommandType = CommandType.StoredProcedure;
                        this.Cmd.Parameters.Clear();
                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto PedidoFields.
        /// </summary>
        /// <returns>List de objetos PedidoFields</returns> 
        public List<PedidoFields> GetAll()
        {
            List<PedidoFields> arrayInfo = new List<PedidoFields>();
            try
            {
                using (this.Conn = new SqlConnection(this.StrConnetionDB))
                {
                    using (this.Cmd = new SqlCommand("Proc_Pedido_GetAll", this.Conn))
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
                    using (this.Cmd = new SqlCommand("Proc_Pedido_CountAll", this.Conn))
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
        /// Retorna um objeto PedidoFields preenchido com os valores dos campos do SqlDataReader
        /// </summary>
        /// <param name="dr">SqlDataReader - Preenche o objeto PedidoFields </param>
        /// <returns>PedidoFields</returns>
        private PedidoFields GetDataFromReader( SqlDataReader dr )
        {
            PedidoFields infoFields = new PedidoFields();

            if (!dr.IsDBNull(0))
            { infoFields.idPedido = dr.GetInt32(0); }
            else
            { infoFields.idPedido = 0; }



            if (!dr.IsDBNull(1))
            { infoFields.dtPedido = dr.GetDateTime(1); }
            else
            { infoFields.dtPedido = DateTime.MinValue; }



            if (!dr.IsDBNull(2))
            { infoFields.dtSaidaPedido = dr.GetDateTime(2); }
            else
            { infoFields.dtSaidaPedido = DateTime.MinValue; }



            if (!dr.IsDBNull(3))
            { infoFields.tipoPedido = dr.GetString(3); }
            else
            { infoFields.tipoPedido = string.Empty; }



            if (!dr.IsDBNull(4))
            { infoFields.situacaoPedido = dr.GetString(4); }
            else
            { infoFields.situacaoPedido = string.Empty; }



            if (!dr.IsDBNull(5))
            { infoFields.tipoEntregaPedido = dr.GetString(5); }
            else
            { infoFields.tipoEntregaPedido = string.Empty; }



            if (!dr.IsDBNull(6))
            { infoFields.fkCliente = dr.GetInt32(6); }
            else
            { infoFields.fkCliente = 0; }



            if (!dr.IsDBNull(7))
            { infoFields.fkUsuario = dr.GetInt32(7); }
            else
            { infoFields.fkUsuario = 0; }



            if (!dr.IsDBNull(8))
            { infoFields.fkTipoPagamento = dr.GetInt32(8); }
            else
            { infoFields.fkTipoPagamento = 0; }



            if (!dr.IsDBNull(9))
            { infoFields.fkFormaPagamento = dr.GetInt32(9); }
            else
            { infoFields.fkFormaPagamento = 0; }



            if (!dr.IsDBNull(10))
            { infoFields.valorTotalPedido = dr.GetDecimal(10); }
            else
            { infoFields.valorTotalPedido = 0; }



            if (!dr.IsDBNull(11))
            { infoFields.numeroPedido = dr.GetInt32(11); }
            else
            { infoFields.numeroPedido = 0; }



            if (!dr.IsDBNull(12))
            { infoFields.fkLoja = dr.GetInt32(12); }
            else
            { infoFields.fkLoja = 0; }



            if (!dr.IsDBNull(13))
            { infoFields.numeroNF = dr.GetString(13); }
            else
            { infoFields.numeroNF = string.Empty; }



            if (!dr.IsDBNull(14))
            { infoFields.Observacao = dr.GetString(14); }
            else
            { infoFields.Observacao = string.Empty; }



            if (!dr.IsDBNull(15))
            { infoFields.ValorDesconto = dr.GetDecimal(15); }
            else
            { infoFields.ValorDesconto = 0; }



            if (!dr.IsDBNull(16))
            { infoFields.ValorFrete = dr.GetDecimal(16); }
            else
            { infoFields.ValorFrete = 0; }


            return infoFields;
        }
        #endregion

        #region [GetMaionOS]

        public DataTable GetMainPedido(string nomeCliente, int codPedido, DateTime dtInicioCadastro, DateTime dtFimCadastro, string situacaoPedido, string TipoPedido, int idLoja, string numSerieEntrada, string numSerieSaida, string numNF)
        {
            DataSet dsMainOS = new DataSet();
            try
            {
                SqlConnection Conn = new SqlConnection(this.StrConnetionDB);

                string query = GetQueryMainPedido(nomeCliente, codPedido, dtInicioCadastro, dtFimCadastro, situacaoPedido, TipoPedido, idLoja, numSerieEntrada, numSerieSaida, numNF);

                Conn.Open();
                DataTable dt = new DataTable();
                SqlCommand Cmd = new SqlCommand(query.ToString(), Conn);
                Cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dsMainOS, "MainPedido");

                return dsMainOS.Tables[0];
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

        private string GetQueryMainPedido(string nomeCliente, int codPedido, DateTime dtInicioCadastro, DateTime dtFimCadastro, string situacaoPedido, string TipoPedido, int idLoja, string numSerieEntrada, string numSerieSaida, string numNF)
        {
            StringBuilder query = new StringBuilder();
            if (!string.IsNullOrEmpty(numSerieEntrada) || !string.IsNullOrEmpty(numSerieSaida))
            {
                query.Append(@" SELECT distinct p.idPedido
                                                ,P.dtPedido
                                                ,P.dtSaidaPedido
                                                ,c.nomeCliente 
                                                ,P.valorTotalPedido
                                                ,P.valorDesconto
                                                ,P.situacaoPedido
                                                ,P.numeroPedido
                                                ,P.numeroNF
                                                ,P.tipoPedido
                                                ,P.fkLoja
                                                ,IP.numeorSerieEntrada
                                                ,IP.numeroSerieSaida
                                                ,P.observacao
                                                FROM Pedido P LEFT JOIN ItemPedido IP ON P.idPedido = IP.fkPedido INNER JOIN Cliente c ON c.idCliente = p.fkCliente ");

                if (!string.IsNullOrEmpty(numSerieEntrada))
                    query.AppendFormat(" AND IP.numeorSerieEntrada LIKE '%{0}%' ", numSerieEntrada);

                if (!string.IsNullOrEmpty(numSerieSaida))
                    query.AppendFormat(" AND IP.numeroSerieSaida LIKE '%{0}%' ", numSerieSaida);
            }
            else
            {
                query.Append(@"SELECT p.idPedido
                                    ,P.dtPedido
                                    ,P.dtSaidaPedido
                                    ,c.nomeCliente 
                                    ,p.valorTotalPedido
                                    ,p.valorDesconto
                                    ,p.situacaoPedido
                                    ,p.numeroPedido
                                    ,p.numeroNF
                                    ,P.tipoPedido
                                    ,P.fkLoja
                                    ,P.observacao
                            FROM (SELECT distinct p.* FROM Pedido p) as p

                            INNER JOIN (SELECT c.nomeCliente, c.idCliente FROM Cliente c) as c
                                ON c.idCliente = p.fkCliente");
            }
            
            if (!string.IsNullOrEmpty(nomeCliente))
                query.AppendFormat(" AND  c.nomeCliente  LIKE '%{0}%' ", nomeCliente);

            if (idLoja > 0)
                query.AppendFormat("  AND P.fkLoja =  {0} ", idLoja);

            if (codPedido > 0)
                query.AppendFormat("  AND P.numeroPedido =  {0} ", codPedido);

            if (!string.IsNullOrEmpty(situacaoPedido))
                query.AppendFormat(" AND P.situacaoPedido =  '{0}' ", situacaoPedido);

            if (!string.IsNullOrEmpty(TipoPedido))
                query.AppendFormat(" AND P.tipoPedido = '{0}' ", TipoPedido);

            if (!string.IsNullOrEmpty(numNF))
                query.AppendFormat(" AND P.numeroNF = '{0}' ", numNF);

            query.AppendFormat(" AND P.dtPedido >= '{0}' AND P.dtPedido < '{1}' ", 
                                 dtInicioCadastro.ToString("MM/dd/yyyy 00:00:00"), 
                                 dtFimCadastro.ToString("MM/dd/yyyy 23:59:59"));

            query.Append(" ORDER BY p.numeroPedido DESC ");

            return query.ToString();
        }

        #endregion







































        #region Função GetAllParameters

        /// <summary> 
        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
        /// </summary>
        /// <param name="FieldInfo">Objeto PedidoFields</param>
        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
        private SqlParameter[] GetAllParameters( PedidoFields FieldInfo, SQLMode Modo )
        {
            SqlParameter[] Parameters;

            switch (Modo)
            {
                case SQLMode.Add:
                    Parameters = new SqlParameter[17];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idPedido
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].Direction = ParameterDirection.Output;
                    Parameters[0].ParameterName = "@Param_idPedido";
                    Parameters[0].Value = DBNull.Value;

                    break;

                case SQLMode.Update:
                    Parameters = new SqlParameter[17];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idPedido
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].ParameterName = "@Param_idPedido";
                    Parameters[0].Value = FieldInfo.idPedido;

                    break;

                case SQLMode.SelectORDelete:
                    Parameters = new SqlParameter[1];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    //Field idPedido
                    Parameters[0].SqlDbType = SqlDbType.Int;
                    Parameters[0].ParameterName = "@Param_idPedido";
                    Parameters[0].Value = FieldInfo.idPedido;

                    return Parameters;

                default:
                    Parameters = new SqlParameter[17];
                    for (int I = 0; I < Parameters.Length; I++)
                       Parameters[I] = new SqlParameter();
                    break;
            }

            //Field dtPedido
            Parameters[1].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[1].ParameterName = "@Param_dtPedido";
            if ( FieldInfo.dtPedido == DateTime.MinValue )
            { Parameters[1].Value = DBNull.Value; }
            else
            { Parameters[1].Value = FieldInfo.dtPedido; }

            //Field dtSaidaPedido
            Parameters[2].SqlDbType = SqlDbType.SmallDateTime;
            Parameters[2].ParameterName = "@Param_dtSaidaPedido";
            if ( FieldInfo.dtSaidaPedido == DateTime.MinValue )
            { Parameters[2].Value = DBNull.Value; }
            else
            { Parameters[2].Value = FieldInfo.dtSaidaPedido; }

            //Field tipoPedido
            Parameters[3].SqlDbType = SqlDbType.VarChar;
            Parameters[3].ParameterName = "@Param_tipoPedido";
            if (( FieldInfo.tipoPedido == null ) || ( FieldInfo.tipoPedido == string.Empty ))
            { Parameters[3].Value = DBNull.Value; }
            else
            { Parameters[3].Value = FieldInfo.tipoPedido; }
            Parameters[3].Size = 50;

            //Field situacaoPedido
            Parameters[4].SqlDbType = SqlDbType.VarChar;
            Parameters[4].ParameterName = "@Param_situacaoPedido";
            if (( FieldInfo.situacaoPedido == null ) || ( FieldInfo.situacaoPedido == string.Empty ))
            { Parameters[4].Value = DBNull.Value; }
            else
            { Parameters[4].Value = FieldInfo.situacaoPedido; }
            Parameters[4].Size = 50;

            //Field tipoEntregaPedido
            Parameters[5].SqlDbType = SqlDbType.VarChar;
            Parameters[5].ParameterName = "@Param_tipoEntregaPedido";
            if (( FieldInfo.tipoEntregaPedido == null ) || ( FieldInfo.tipoEntregaPedido == string.Empty ))
            { Parameters[5].Value = DBNull.Value; }
            else
            { Parameters[5].Value = FieldInfo.tipoEntregaPedido; }
            Parameters[5].Size = 50;

            //Field fkCliente
            Parameters[6].SqlDbType = SqlDbType.Int;
            Parameters[6].ParameterName = "@Param_fkCliente";
            Parameters[6].Value = FieldInfo.fkCliente;

            //Field fkUsuario
            Parameters[7].SqlDbType = SqlDbType.Int;
            Parameters[7].ParameterName = "@Param_fkUsuario";
            Parameters[7].Value = FieldInfo.fkUsuario;

            //Field fkTipoPagamento
            Parameters[8].SqlDbType = SqlDbType.Int;
            Parameters[8].ParameterName = "@Param_fkTipoPagamento";
            Parameters[8].Value = FieldInfo.fkTipoPagamento;

            //Field fkFormaPagamento
            Parameters[9].SqlDbType = SqlDbType.Int;
            Parameters[9].ParameterName = "@Param_fkFormaPagamento";
            Parameters[9].Value = FieldInfo.fkFormaPagamento;

            //Field valorTotalPedido
            Parameters[10].SqlDbType = SqlDbType.Decimal;
            Parameters[10].ParameterName = "@Param_valorTotalPedido";
            Parameters[10].Value = FieldInfo.valorTotalPedido;

            //Field numeroPedido
            Parameters[11].SqlDbType = SqlDbType.Int;
            Parameters[11].ParameterName = "@Param_numeroPedido";
            Parameters[11].Value = FieldInfo.numeroPedido;

            //Field fkLoja
            Parameters[12].SqlDbType = SqlDbType.Int;
            Parameters[12].ParameterName = "@Param_fkLoja";
            Parameters[12].Value = FieldInfo.fkLoja;

            //Field numeroNF
            Parameters[13].SqlDbType = SqlDbType.VarChar;
            Parameters[13].ParameterName = "@Param_numeroNF";
            if (( FieldInfo.numeroNF == null ) || ( FieldInfo.numeroNF == string.Empty ))
            { Parameters[13].Value = DBNull.Value; }
            else
            { Parameters[13].Value = FieldInfo.numeroNF; }
            Parameters[13].Size = 200;

            //Field Observacao
            Parameters[14].SqlDbType = SqlDbType.VarChar;
            Parameters[14].ParameterName = "@Param_Observacao";
            if (( FieldInfo.Observacao == null ) || ( FieldInfo.Observacao == string.Empty ))
            { Parameters[14].Value = DBNull.Value; }
            else
            { Parameters[14].Value = FieldInfo.Observacao; }
            Parameters[14].Size = 2000;

            //Field ValorDesconto
            Parameters[15].SqlDbType = SqlDbType.Decimal;
            Parameters[15].ParameterName = "@Param_ValorDesconto";
            Parameters[15].Value = FieldInfo.ValorDesconto;

            //Field ValorFrete
            Parameters[16].SqlDbType = SqlDbType.Decimal;
            Parameters[16].ParameterName = "@Param_ValorFrete";
            Parameters[16].Value = FieldInfo.ValorFrete;

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

        ~PedidoControl() 
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
//    /// Tabela: Pedido  
//    /// Autor: DAL Creator .net 
//    /// Data de criação: 24/05/2013 16:44:13 
//    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "PedidoFields". 
//    /// </summary> 
//    public class PedidoControl : IDisposable 
//    {
//
//        #region String de conexão 
//        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["Data Source=(local);Initial Catalog=simlteste;Integrated Security=SSPI;"].ToString();
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
//        public PedidoControl() {}
//
//
//        #region Inserindo dados na tabela 
//
//        /// <summary> 
//        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Add( ref PedidoFields FieldInfo )
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_Pedido_Add", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//                this.Tran.Commit();
//                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
//        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref PedidoFields FieldInfo )
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_Pedido_Add", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
//        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
//        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Update( PedidoFields FieldInfo )
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_Pedido_Update", this.Conn, this.Tran);
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
//        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
//        /// </summary>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
//        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_Pedido_Update", ConnIn, TranIn);
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
//                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", this.Conn, this.Tran);
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
//                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", ConnIn, TranIn);
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
//        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( PedidoFields FieldInfo )
//        {
//            return Delete(FieldInfo.idPedido);
//        }
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="Param_idPedido">int</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete(
//                                     int Param_idPedido)
//        {
//            try
//            {
//                this.Conn = new SqlConnection(this.StrConnetionDB);
//                this.Conn.Open();
//                this.Tran = this.Conn.BeginTransaction();
//                this.Cmd = new SqlCommand("Proc_Pedido_Delete", this.Conn, this.Tran);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
//        {
//            return Delete(ConnIn, TranIn, FieldInfo.idPedido);
//        }
//
//        /// <summary> 
//        /// Exclui um registro da tabela no banco de dados
//        /// </summary>
//        /// <param name="Param_idPedido">int</param>
//        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
//                                     int Param_idPedido)
//        {
//            try
//            {
//                this.Cmd = new SqlCommand("Proc_Pedido_Delete", ConnIn, TranIn);
//                this.Cmd.CommandType = CommandType.StoredProcedure;
//                this.Cmd.Parameters.Clear();
//                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//        /// Retorna um objeto PedidoFields através da chave primária passada como parâmetro
//        /// </summary>
//        /// <param name="Param_idPedido">int</param>
//        /// <returns>Objeto PedidoFields</returns> 
//        public PedidoFields GetItem(
//                                     int Param_idPedido)
//        {
//            PedidoFields infoFields = new PedidoFields();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Pedido_Select", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto PedidoFields.
//        /// </summary>
//        /// <returns>List de objetos PedidoFields</returns> 
//        public List<PedidoFields> GetAll()
//        {
//            List<PedidoFields> arrayInfo = new List<PedidoFields>();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Pedido_GetAll", this.Conn))
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
//                    using (this.Cmd = new SqlCommand("Proc_Pedido_CountAll", this.Conn))
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
//        #region Selecionando dados da tabela através do campo "fkCliente" 
//
//        /// <summary> 
//        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkCliente.
//        /// </summary>
//        /// <param name="Param_fkCliente">int</param>
//        /// <returns>PedidoFields</returns> 
//        public PedidoFields FindByfkCliente(
//                               int Param_fkCliente )
//        {
//            PedidoFields infoFields = new PedidoFields();
//            try
//            {
//                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//                {
//                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindByfkCliente", this.Conn))
//                    {
//                        this.Cmd.CommandType = CommandType.StoredProcedure;
//                        this.Cmd.Parameters.Clear();
//                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkCliente", SqlDbType.Int)).Value = Param_fkCliente;
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
//        #region Função GetDataFromReader
//
//        /// <summary> 
//        /// Retorna um objeto PedidoFields preenchido com os valores dos campos do SqlDataReader
//        /// </summary>
//        /// <param name="dr">SqlDataReader - Preenche o objeto PedidoFields </param>
//        /// <returns>PedidoFields</returns>
//        private PedidoFields GetDataFromReader( SqlDataReader dr )
//        {
//            PedidoFields infoFields = new PedidoFields();
//
//            if (!dr.IsDBNull(0))
//            { infoFields.idPedido = dr.GetInt32(0); }
//            else
//            { infoFields.idPedido = 0; }
//
//
//
//            if (!dr.IsDBNull(1))
//            { infoFields.dtPedido = dr.GetDateTime(1); }
//            else
//            { infoFields.dtPedido = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(2))
//            { infoFields.dtSaidaPedido = dr.GetDateTime(2); }
//            else
//            { infoFields.dtSaidaPedido = DateTime.MinValue; }
//
//
//
//            if (!dr.IsDBNull(3))
//            { infoFields.tipoPedido = dr.GetString(3); }
//            else
//            { infoFields.tipoPedido = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(4))
//            { infoFields.situacaoPedido = dr.GetString(4); }
//            else
//            { infoFields.situacaoPedido = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(5))
//            { infoFields.tipoEntregaPedido = dr.GetString(5); }
//            else
//            { infoFields.tipoEntregaPedido = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(6))
//            { infoFields.fkCliente = dr.GetInt32(6); }
//            else
//            { infoFields.fkCliente = 0; }
//
//
//
//            if (!dr.IsDBNull(7))
//            { infoFields.fkUsuario = dr.GetInt32(7); }
//            else
//            { infoFields.fkUsuario = 0; }
//
//
//
//            if (!dr.IsDBNull(8))
//            { infoFields.fkTipoPagamento = dr.GetInt32(8); }
//            else
//            { infoFields.fkTipoPagamento = 0; }
//
//
//
//            if (!dr.IsDBNull(9))
//            { infoFields.fkFormaPagamento = dr.GetInt32(9); }
//            else
//            { infoFields.fkFormaPagamento = 0; }
//
//
//
//            if (!dr.IsDBNull(10))
//            { infoFields.valorTotalPedido = dr.GetDecimal(10); }
//            else
//            { infoFields.valorTotalPedido = 0; }
//
//
//
//            if (!dr.IsDBNull(11))
//            { infoFields.numeroPedido = dr.GetInt32(11); }
//            else
//            { infoFields.numeroPedido = 0; }
//
//
//
//            if (!dr.IsDBNull(12))
//            { infoFields.fkLoja = dr.GetInt32(12); }
//            else
//            { infoFields.fkLoja = 0; }
//
//
//
//            if (!dr.IsDBNull(13))
//            { infoFields.numeroNF = dr.GetString(13); }
//            else
//            { infoFields.numeroNF = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(14))
//            { infoFields.Observacao = dr.GetString(14); }
//            else
//            { infoFields.Observacao = string.Empty; }
//
//
//
//            if (!dr.IsDBNull(15))
//            { infoFields.ValorDesconto = dr.GetDecimal(15); }
//            else
//            { infoFields.ValorDesconto = 0; }
//
//
//
//            if (!dr.IsDBNull(16))
//            { infoFields.ValorFrete = dr.GetDecimal(16); }
//            else
//            { infoFields.ValorFrete = 0; }
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
//        #region Função GetAllParameters
//
//        /// <summary> 
//        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
//        /// </summary>
//        /// <param name="FieldInfo">Objeto PedidoFields</param>
//        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
//        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
//        private SqlParameter[] GetAllParameters( PedidoFields FieldInfo, SQLMode Modo )
//        {
//            SqlParameter[] Parameters;
//
//            switch (Modo)
//            {
//                case SQLMode.Add:
//                    Parameters = new SqlParameter[17];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idPedido
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].Direction = ParameterDirection.Output;
//                    Parameters[0].ParameterName = "@Param_idPedido";
//                    Parameters[0].Value = DBNull.Value;
//
//                    break;
//
//                case SQLMode.Update:
//                    Parameters = new SqlParameter[17];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idPedido
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].ParameterName = "@Param_idPedido";
//                    Parameters[0].Value = FieldInfo.idPedido;
//
//                    break;
//
//                case SQLMode.SelectORDelete:
//                    Parameters = new SqlParameter[1];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    //Field idPedido
//                    Parameters[0].SqlDbType = SqlDbType.Int;
//                    Parameters[0].ParameterName = "@Param_idPedido";
//                    Parameters[0].Value = FieldInfo.idPedido;
//
//                    return Parameters;
//
//                default:
//                    Parameters = new SqlParameter[17];
//                    for (int I = 0; I < Parameters.Length; I++)
//                       Parameters[I] = new SqlParameter();
//                    break;
//            }
//
//            //Field dtPedido
//            Parameters[1].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[1].ParameterName = "@Param_dtPedido";
//            if ( FieldInfo.dtPedido == DateTime.MinValue )
//            { Parameters[1].Value = DBNull.Value; }
//            else
//            { Parameters[1].Value = FieldInfo.dtPedido; }
//
//            //Field dtSaidaPedido
//            Parameters[2].SqlDbType = SqlDbType.SmallDateTime;
//            Parameters[2].ParameterName = "@Param_dtSaidaPedido";
//            if ( FieldInfo.dtSaidaPedido == DateTime.MinValue )
//            { Parameters[2].Value = DBNull.Value; }
//            else
//            { Parameters[2].Value = FieldInfo.dtSaidaPedido; }
//
//            //Field tipoPedido
//            Parameters[3].SqlDbType = SqlDbType.VarChar;
//            Parameters[3].ParameterName = "@Param_tipoPedido";
//            if (( FieldInfo.tipoPedido == null ) || ( FieldInfo.tipoPedido == string.Empty ))
//            { Parameters[3].Value = DBNull.Value; }
//            else
//            { Parameters[3].Value = FieldInfo.tipoPedido; }
//            Parameters[3].Size = 50;
//
//            //Field situacaoPedido
//            Parameters[4].SqlDbType = SqlDbType.VarChar;
//            Parameters[4].ParameterName = "@Param_situacaoPedido";
//            if (( FieldInfo.situacaoPedido == null ) || ( FieldInfo.situacaoPedido == string.Empty ))
//            { Parameters[4].Value = DBNull.Value; }
//            else
//            { Parameters[4].Value = FieldInfo.situacaoPedido; }
//            Parameters[4].Size = 50;
//
//            //Field tipoEntregaPedido
//            Parameters[5].SqlDbType = SqlDbType.VarChar;
//            Parameters[5].ParameterName = "@Param_tipoEntregaPedido";
//            if (( FieldInfo.tipoEntregaPedido == null ) || ( FieldInfo.tipoEntregaPedido == string.Empty ))
//            { Parameters[5].Value = DBNull.Value; }
//            else
//            { Parameters[5].Value = FieldInfo.tipoEntregaPedido; }
//            Parameters[5].Size = 50;
//
//            //Field fkCliente
//            Parameters[6].SqlDbType = SqlDbType.Int;
//            Parameters[6].ParameterName = "@Param_fkCliente";
//            Parameters[6].Value = FieldInfo.fkCliente;
//
//            //Field fkUsuario
//            Parameters[7].SqlDbType = SqlDbType.Int;
//            Parameters[7].ParameterName = "@Param_fkUsuario";
//            Parameters[7].Value = FieldInfo.fkUsuario;
//
//            //Field fkTipoPagamento
//            Parameters[8].SqlDbType = SqlDbType.Int;
//            Parameters[8].ParameterName = "@Param_fkTipoPagamento";
//            Parameters[8].Value = FieldInfo.fkTipoPagamento;
//
//            //Field fkFormaPagamento
//            Parameters[9].SqlDbType = SqlDbType.Int;
//            Parameters[9].ParameterName = "@Param_fkFormaPagamento";
//            Parameters[9].Value = FieldInfo.fkFormaPagamento;
//
//            //Field valorTotalPedido
//            Parameters[10].SqlDbType = SqlDbType.Decimal;
//            Parameters[10].ParameterName = "@Param_valorTotalPedido";
//            Parameters[10].Value = FieldInfo.valorTotalPedido;
//
//            //Field numeroPedido
//            Parameters[11].SqlDbType = SqlDbType.Int;
//            Parameters[11].ParameterName = "@Param_numeroPedido";
//            Parameters[11].Value = FieldInfo.numeroPedido;
//
//            //Field fkLoja
//            Parameters[12].SqlDbType = SqlDbType.Int;
//            Parameters[12].ParameterName = "@Param_fkLoja";
//            Parameters[12].Value = FieldInfo.fkLoja;
//
//            //Field numeroNF
//            Parameters[13].SqlDbType = SqlDbType.VarChar;
//            Parameters[13].ParameterName = "@Param_numeroNF";
//            if (( FieldInfo.numeroNF == null ) || ( FieldInfo.numeroNF == string.Empty ))
//            { Parameters[13].Value = DBNull.Value; }
//            else
//            { Parameters[13].Value = FieldInfo.numeroNF; }
//            Parameters[13].Size = 200;
//
//            //Field Observacao
//            Parameters[14].SqlDbType = SqlDbType.NText;
//            Parameters[14].ParameterName = "@Param_Observacao";
//            if (( FieldInfo.Observacao == null ) || ( FieldInfo.Observacao == string.Empty ))
//            { Parameters[14].Value = DBNull.Value; }
//            else
//            { Parameters[14].Value = FieldInfo.Observacao; }
//            Parameters[14].Size = 0;
//
//            //Field ValorDesconto
//            Parameters[15].SqlDbType = SqlDbType.Decimal;
//            Parameters[15].ParameterName = "@Param_ValorDesconto";
//            Parameters[15].Value = FieldInfo.ValorDesconto;
//
//            //Field ValorFrete
//            Parameters[16].SqlDbType = SqlDbType.Decimal;
//            Parameters[16].ParameterName = "@Param_ValorFrete";
//            Parameters[16].Value = FieldInfo.ValorFrete;
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
//        ~PedidoControl() 
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
////    /// Tabela: Pedido  
////    /// Autor: DAL Creator .net 
////    /// Data de criação: 24/05/2013 16:35:44 
////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "PedidoFields". 
////    /// </summary> 
////    public class PedidoControl : IDisposable 
////    {
////
////        #region String de conexão 
////        private string StrConnetionDB = ConfigurationManager.ConnectionStrings["Data Source=(local);Initial Catalog=simlteste;Integrated Security=SSPI;"].ToString();
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
////        public PedidoControl() {}
////
////
////        #region Inserindo dados na tabela 
////
////        /// <summary> 
////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
////        /// </summary>
////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Add( ref PedidoFields FieldInfo )
////        {
////            try
////            {
////                this.Conn = new SqlConnection(this.StrConnetionDB);
////                this.Conn.Open();
////                this.Tran = this.Conn.BeginTransaction();
////                this.Cmd = new SqlCommand("Proc_Pedido_Add", this.Conn, this.Tran);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////                this.Tran.Commit();
////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
////        /// </summary>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref PedidoFields FieldInfo )
////        {
////            try
////            {
////                this.Cmd = new SqlCommand("Proc_Pedido_Add", ConnIn, TranIn);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
////        /// </summary>
////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Update( PedidoFields FieldInfo )
////        {
////            try
////            {
////                this.Conn = new SqlConnection(this.StrConnetionDB);
////                this.Conn.Open();
////                this.Tran = this.Conn.BeginTransaction();
////                this.Cmd = new SqlCommand("Proc_Pedido_Update", this.Conn, this.Tran);
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
////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
////        /// </summary>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
////        {
////            try
////            {
////                this.Cmd = new SqlCommand("Proc_Pedido_Update", ConnIn, TranIn);
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
////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", this.Conn, this.Tran);
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
////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", ConnIn, TranIn);
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
////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete( PedidoFields FieldInfo )
////        {
////            return Delete(FieldInfo.idPedido);
////        }
////
////        /// <summary> 
////        /// Exclui um registro da tabela no banco de dados
////        /// </summary>
////        /// <param name="Param_idPedido">int</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete(
////                                     int Param_idPedido)
////        {
////            try
////            {
////                this.Conn = new SqlConnection(this.StrConnetionDB);
////                this.Conn.Open();
////                this.Tran = this.Conn.BeginTransaction();
////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", this.Conn, this.Tran);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
////        {
////            return Delete(ConnIn, TranIn, FieldInfo.idPedido);
////        }
////
////        /// <summary> 
////        /// Exclui um registro da tabela no banco de dados
////        /// </summary>
////        /// <param name="Param_idPedido">int</param>
////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
////                                     int Param_idPedido)
////        {
////            try
////            {
////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", ConnIn, TranIn);
////                this.Cmd.CommandType = CommandType.StoredProcedure;
////                this.Cmd.Parameters.Clear();
////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
////        /// Retorna um objeto PedidoFields através da chave primária passada como parâmetro
////        /// </summary>
////        /// <param name="Param_idPedido">int</param>
////        /// <returns>Objeto PedidoFields</returns> 
////        public PedidoFields GetItem(
////                                     int Param_idPedido)
////        {
////            PedidoFields infoFields = new PedidoFields();
////            try
////            {
////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////                {
////                    using (this.Cmd = new SqlCommand("Proc_Pedido_Select", this.Conn))
////                    {
////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////                        this.Cmd.Parameters.Clear();
////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto PedidoFields.
////        /// </summary>
////        /// <returns>List de objetos PedidoFields</returns> 
////        public List<PedidoFields> GetAll()
////        {
////            List<PedidoFields> arrayInfo = new List<PedidoFields>();
////            try
////            {
////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////                {
////                    using (this.Cmd = new SqlCommand("Proc_Pedido_GetAll", this.Conn))
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
////                    using (this.Cmd = new SqlCommand("Proc_Pedido_CountAll", this.Conn))
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
////        /// Retorna um objeto PedidoFields preenchido com os valores dos campos do SqlDataReader
////        /// </summary>
////        /// <param name="dr">SqlDataReader - Preenche o objeto PedidoFields </param>
////        /// <returns>PedidoFields</returns>
////        private PedidoFields GetDataFromReader( SqlDataReader dr )
////        {
////            PedidoFields infoFields = new PedidoFields();
////
////            if (!dr.IsDBNull(0))
////            { infoFields.idPedido = dr.GetInt32(0); }
////            else
////            { infoFields.idPedido = 0; }
////
////
////
////            if (!dr.IsDBNull(1))
////            { infoFields.dtPedido = dr.GetDateTime(1); }
////            else
////            { infoFields.dtPedido = DateTime.MinValue; }
////
////
////
////            if (!dr.IsDBNull(2))
////            { infoFields.dtSaidaPedido = dr.GetDateTime(2); }
////            else
////            { infoFields.dtSaidaPedido = DateTime.MinValue; }
////
////
////
////            if (!dr.IsDBNull(3))
////            { infoFields.tipoPedido = dr.GetString(3); }
////            else
////            { infoFields.tipoPedido = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(4))
////            { infoFields.situacaoPedido = dr.GetString(4); }
////            else
////            { infoFields.situacaoPedido = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(5))
////            { infoFields.tipoEntregaPedido = dr.GetString(5); }
////            else
////            { infoFields.tipoEntregaPedido = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(6))
////            { infoFields.fkCliente = dr.GetInt32(6); }
////            else
////            { infoFields.fkCliente = 0; }
////
////
////
////            if (!dr.IsDBNull(7))
////            { infoFields.fkUsuario = dr.GetInt32(7); }
////            else
////            { infoFields.fkUsuario = 0; }
////
////
////
////            if (!dr.IsDBNull(8))
////            { infoFields.fkTipoPagamento = dr.GetInt32(8); }
////            else
////            { infoFields.fkTipoPagamento = 0; }
////
////
////
////            if (!dr.IsDBNull(9))
////            { infoFields.fkFormaPagamento = dr.GetInt32(9); }
////            else
////            { infoFields.fkFormaPagamento = 0; }
////
////
////
////            if (!dr.IsDBNull(10))
////            { infoFields.valorTotalPedido = dr.GetDecimal(10); }
////            else
////            { infoFields.valorTotalPedido = 0; }
////
////
////
////            if (!dr.IsDBNull(11))
////            { infoFields.numeroPedido = dr.GetInt32(11); }
////            else
////            { infoFields.numeroPedido = 0; }
////
////
////
////            if (!dr.IsDBNull(12))
////            { infoFields.fkLoja = dr.GetInt32(12); }
////            else
////            { infoFields.fkLoja = 0; }
////
////
////
////            if (!dr.IsDBNull(13))
////            { infoFields.numeroNF = dr.GetString(13); }
////            else
////            { infoFields.numeroNF = string.Empty; }
////
////
////
////            if (!dr.IsDBNull(14))
////            { infoFields.Observacao = dr.GetString(14); }
////            else
////            { infoFields.Observacao = string.Empty; }
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
////        #region Função GetAllParameters
////
////        /// <summary> 
////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
////        /// </summary>
////        /// <param name="FieldInfo">Objeto PedidoFields</param>
////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
////        private SqlParameter[] GetAllParameters( PedidoFields FieldInfo, SQLMode Modo )
////        {
////            SqlParameter[] Parameters;
////
////            switch (Modo)
////            {
////                case SQLMode.Add:
////                    Parameters = new SqlParameter[15];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    //Field idPedido
////                    Parameters[0].SqlDbType = SqlDbType.Int;
////                    Parameters[0].Direction = ParameterDirection.Output;
////                    Parameters[0].ParameterName = "@Param_idPedido";
////                    Parameters[0].Value = DBNull.Value;
////
////                    break;
////
////                case SQLMode.Update:
////                    Parameters = new SqlParameter[15];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    //Field idPedido
////                    Parameters[0].SqlDbType = SqlDbType.Int;
////                    Parameters[0].ParameterName = "@Param_idPedido";
////                    Parameters[0].Value = FieldInfo.idPedido;
////
////                    break;
////
////                case SQLMode.SelectORDelete:
////                    Parameters = new SqlParameter[1];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    //Field idPedido
////                    Parameters[0].SqlDbType = SqlDbType.Int;
////                    Parameters[0].ParameterName = "@Param_idPedido";
////                    Parameters[0].Value = FieldInfo.idPedido;
////
////                    return Parameters;
////
////                default:
////                    Parameters = new SqlParameter[15];
////                    for (int I = 0; I < Parameters.Length; I++)
////                       Parameters[I] = new SqlParameter();
////                    break;
////            }
////
////            //Field dtPedido
////            Parameters[1].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[1].ParameterName = "@Param_dtPedido";
////            if ( FieldInfo.dtPedido == DateTime.MinValue )
////            { Parameters[1].Value = DBNull.Value; }
////            else
////            { Parameters[1].Value = FieldInfo.dtPedido; }
////
////            //Field dtSaidaPedido
////            Parameters[2].SqlDbType = SqlDbType.SmallDateTime;
////            Parameters[2].ParameterName = "@Param_dtSaidaPedido";
////            if ( FieldInfo.dtSaidaPedido == DateTime.MinValue )
////            { Parameters[2].Value = DBNull.Value; }
////            else
////            { Parameters[2].Value = FieldInfo.dtSaidaPedido; }
////
////            //Field tipoPedido
////            Parameters[3].SqlDbType = SqlDbType.VarChar;
////            Parameters[3].ParameterName = "@Param_tipoPedido";
////            if (( FieldInfo.tipoPedido == null ) || ( FieldInfo.tipoPedido == string.Empty ))
////            { Parameters[3].Value = DBNull.Value; }
////            else
////            { Parameters[3].Value = FieldInfo.tipoPedido; }
////            Parameters[3].Size = 50;
////
////            //Field situacaoPedido
////            Parameters[4].SqlDbType = SqlDbType.VarChar;
////            Parameters[4].ParameterName = "@Param_situacaoPedido";
////            if (( FieldInfo.situacaoPedido == null ) || ( FieldInfo.situacaoPedido == string.Empty ))
////            { Parameters[4].Value = DBNull.Value; }
////            else
////            { Parameters[4].Value = FieldInfo.situacaoPedido; }
////            Parameters[4].Size = 50;
////
////            //Field tipoEntregaPedido
////            Parameters[5].SqlDbType = SqlDbType.VarChar;
////            Parameters[5].ParameterName = "@Param_tipoEntregaPedido";
////            if (( FieldInfo.tipoEntregaPedido == null ) || ( FieldInfo.tipoEntregaPedido == string.Empty ))
////            { Parameters[5].Value = DBNull.Value; }
////            else
////            { Parameters[5].Value = FieldInfo.tipoEntregaPedido; }
////            Parameters[5].Size = 50;
////
////            //Field fkCliente
////            Parameters[6].SqlDbType = SqlDbType.Int;
////            Parameters[6].ParameterName = "@Param_fkCliente";
////            Parameters[6].Value = FieldInfo.fkCliente;
////
////            //Field fkUsuario
////            Parameters[7].SqlDbType = SqlDbType.Int;
////            Parameters[7].ParameterName = "@Param_fkUsuario";
////            Parameters[7].Value = FieldInfo.fkUsuario;
////
////            //Field fkTipoPagamento
////            Parameters[8].SqlDbType = SqlDbType.Int;
////            Parameters[8].ParameterName = "@Param_fkTipoPagamento";
////            Parameters[8].Value = FieldInfo.fkTipoPagamento;
////
////            //Field fkFormaPagamento
////            Parameters[9].SqlDbType = SqlDbType.Int;
////            Parameters[9].ParameterName = "@Param_fkFormaPagamento";
////            Parameters[9].Value = FieldInfo.fkFormaPagamento;
////
////            //Field valorTotalPedido
////            Parameters[10].SqlDbType = SqlDbType.Decimal;
////            Parameters[10].ParameterName = "@Param_valorTotalPedido";
////            Parameters[10].Value = FieldInfo.valorTotalPedido;
////
////            //Field numeroPedido
////            Parameters[11].SqlDbType = SqlDbType.Int;
////            Parameters[11].ParameterName = "@Param_numeroPedido";
////            Parameters[11].Value = FieldInfo.numeroPedido;
////
////            //Field fkLoja
////            Parameters[12].SqlDbType = SqlDbType.Int;
////            Parameters[12].ParameterName = "@Param_fkLoja";
////            Parameters[12].Value = FieldInfo.fkLoja;
////
////            //Field numeroNF
////            Parameters[13].SqlDbType = SqlDbType.VarChar;
////            Parameters[13].ParameterName = "@Param_numeroNF";
////            if (( FieldInfo.numeroNF == null ) || ( FieldInfo.numeroNF == string.Empty ))
////            { Parameters[13].Value = DBNull.Value; }
////            else
////            { Parameters[13].Value = FieldInfo.numeroNF; }
////            Parameters[13].Size = 200;
////
////            //Field Observacao
////            Parameters[14].SqlDbType = SqlDbType.NText;
////            Parameters[14].ParameterName = "@Param_Observacao";
////            if (( FieldInfo.Observacao == null ) || ( FieldInfo.Observacao == string.Empty ))
////            { Parameters[14].Value = DBNull.Value; }
////            else
////            { Parameters[14].Value = FieldInfo.Observacao; }
////            Parameters[14].Size = 0;
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
////        ~PedidoControl() 
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
//////    /// Tabela: Pedido  
//////    /// Autor: DAL Creator .net 
//////    /// Data de criação: 24/05/2013 15:25:16 
//////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "PedidoFields". 
//////    /// </summary> 
//////    public class PedidoControl : IDisposable 
//////    {
//////
//////        #region String de conexão 
//////        private string StrConnetionDB = ConfigurationSettings.AppSettings["StringConn"].ToString();
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
//////        public PedidoControl() {}
//////
//////
//////        #region Inserindo dados na tabela 
//////
//////        /// <summary> 
//////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
//////        /// </summary>
//////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Add( ref PedidoFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////                this.Conn.Open();
//////                this.Tran = this.Conn.BeginTransaction();
//////                this.Cmd = new SqlCommand("Proc_Pedido_Add", this.Conn, this.Tran);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////                this.Tran.Commit();
//////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
//////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
//////        /// </summary>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref PedidoFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Cmd = new SqlCommand("Proc_Pedido_Add", ConnIn, TranIn);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
//////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
//////        /// </summary>
//////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
//////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Update( PedidoFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////                this.Conn.Open();
//////                this.Tran = this.Conn.BeginTransaction();
//////                this.Cmd = new SqlCommand("Proc_Pedido_Update", this.Conn, this.Tran);
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
//////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
//////        /// </summary>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
//////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
//////        {
//////            try
//////            {
//////                this.Cmd = new SqlCommand("Proc_Pedido_Update", ConnIn, TranIn);
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
//////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", this.Conn, this.Tran);
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
//////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", ConnIn, TranIn);
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
//////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete( PedidoFields FieldInfo )
//////        {
//////            return Delete(FieldInfo.idPedido);
//////        }
//////
//////        /// <summary> 
//////        /// Exclui um registro da tabela no banco de dados
//////        /// </summary>
//////        /// <param name="Param_idPedido">int</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete(
//////                                     int Param_idPedido)
//////        {
//////            try
//////            {
//////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////                this.Conn.Open();
//////                this.Tran = this.Conn.BeginTransaction();
//////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", this.Conn, this.Tran);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
//////        {
//////            return Delete(ConnIn, TranIn, FieldInfo.idPedido);
//////        }
//////
//////        /// <summary> 
//////        /// Exclui um registro da tabela no banco de dados
//////        /// </summary>
//////        /// <param name="Param_idPedido">int</param>
//////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
//////                                     int Param_idPedido)
//////        {
//////            try
//////            {
//////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", ConnIn, TranIn);
//////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////                this.Cmd.Parameters.Clear();
//////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//////        /// Retorna um objeto PedidoFields através da chave primária passada como parâmetro
//////        /// </summary>
//////        /// <param name="Param_idPedido">int</param>
//////        /// <returns>Objeto PedidoFields</returns> 
//////        public PedidoFields GetItem(
//////                                     int Param_idPedido)
//////        {
//////            PedidoFields infoFields = new PedidoFields();
//////            try
//////            {
//////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////                {
//////                    using (this.Cmd = new SqlCommand("Proc_Pedido_Select", this.Conn))
//////                    {
//////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////                        this.Cmd.Parameters.Clear();
//////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto PedidoFields.
//////        /// </summary>
//////        /// <returns>List de objetos PedidoFields</returns> 
//////        public List<PedidoFields> GetAll()
//////        {
//////            List<PedidoFields> arrayInfo = new List<PedidoFields>();
//////            try
//////            {
//////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////                {
//////                    using (this.Cmd = new SqlCommand("Proc_Pedido_GetAll", this.Conn))
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
//////                    using (this.Cmd = new SqlCommand("Proc_Pedido_CountAll", this.Conn))
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
//////        /// Retorna um objeto PedidoFields preenchido com os valores dos campos do SqlDataReader
//////        /// </summary>
//////        /// <param name="dr">SqlDataReader - Preenche o objeto PedidoFields </param>
//////        /// <returns>PedidoFields</returns>
//////        private PedidoFields GetDataFromReader( SqlDataReader dr )
//////        {
//////            PedidoFields infoFields = new PedidoFields();
//////
//////            if (!dr.IsDBNull(0))
//////            { infoFields.idPedido = dr.GetInt32(0); }
//////            else
//////            { infoFields.idPedido = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(1))
//////            { infoFields.dtPedido = dr.GetDateTime(1); }
//////            else
//////            { infoFields.dtPedido = DateTime.MinValue; }
//////
//////
//////
//////            if (!dr.IsDBNull(2))
//////            { infoFields.dtSaidaPedido = dr.GetDateTime(2); }
//////            else
//////            { infoFields.dtSaidaPedido = DateTime.MinValue; }
//////
//////
//////
//////            if (!dr.IsDBNull(3))
//////            { infoFields.tipoPedido = dr.GetString(3); }
//////            else
//////            { infoFields.tipoPedido = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(4))
//////            { infoFields.situacaoPedido = dr.GetString(4); }
//////            else
//////            { infoFields.situacaoPedido = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(5))
//////            { infoFields.tipoEntregaPedido = dr.GetString(5); }
//////            else
//////            { infoFields.tipoEntregaPedido = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(6))
//////            { infoFields.fkCliente = dr.GetInt32(6); }
//////            else
//////            { infoFields.fkCliente = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(7))
//////            { infoFields.fkUsuario = dr.GetInt32(7); }
//////            else
//////            { infoFields.fkUsuario = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(8))
//////            { infoFields.fkTipoPagamento = dr.GetInt32(8); }
//////            else
//////            { infoFields.fkTipoPagamento = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(9))
//////            { infoFields.fkFormaPagamento = dr.GetInt32(9); }
//////            else
//////            { infoFields.fkFormaPagamento = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(10))
//////            { infoFields.valorTotalPedido = dr.GetDecimal(10); }
//////            else
//////            { infoFields.valorTotalPedido = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(11))
//////            { infoFields.numeroPedido = dr.GetInt32(11); }
//////            else
//////            { infoFields.numeroPedido = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(12))
//////            { infoFields.fkLoja = dr.GetInt32(12); }
//////            else
//////            { infoFields.fkLoja = 0; }
//////
//////
//////
//////            if (!dr.IsDBNull(13))
//////            { infoFields.numeroNF = dr.GetString(13); }
//////            else
//////            { infoFields.numeroNF = string.Empty; }
//////
//////
//////
//////            if (!dr.IsDBNull(14))
//////            { infoFields.Observacao = dr.GetString(14); }
//////            else
//////            { infoFields.Observacao = string.Empty; }
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
//////        #region Função GetAllParameters
//////
//////        /// <summary> 
//////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
//////        /// </summary>
//////        /// <param name="FieldInfo">Objeto PedidoFields</param>
//////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
//////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
//////        private SqlParameter[] GetAllParameters( PedidoFields FieldInfo, SQLMode Modo )
//////        {
//////            SqlParameter[] Parameters;
//////
//////            switch (Modo)
//////            {
//////                case SQLMode.Add:
//////                    Parameters = new SqlParameter[15];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    //Field idPedido
//////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////                    Parameters[0].Direction = ParameterDirection.Output;
//////                    Parameters[0].ParameterName = "@Param_idPedido";
//////                    Parameters[0].Value = DBNull.Value;
//////
//////                    break;
//////
//////                case SQLMode.Update:
//////                    Parameters = new SqlParameter[15];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    //Field idPedido
//////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////                    Parameters[0].ParameterName = "@Param_idPedido";
//////                    Parameters[0].Value = FieldInfo.idPedido;
//////
//////                    break;
//////
//////                case SQLMode.SelectORDelete:
//////                    Parameters = new SqlParameter[1];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    //Field idPedido
//////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////                    Parameters[0].ParameterName = "@Param_idPedido";
//////                    Parameters[0].Value = FieldInfo.idPedido;
//////
//////                    return Parameters;
//////
//////                default:
//////                    Parameters = new SqlParameter[15];
//////                    for (int I = 0; I < Parameters.Length; I++)
//////                       Parameters[I] = new SqlParameter();
//////                    break;
//////            }
//////
//////            //Field dtPedido
//////            Parameters[1].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[1].ParameterName = "@Param_dtPedido";
//////            if ( FieldInfo.dtPedido == DateTime.MinValue )
//////            { Parameters[1].Value = DBNull.Value; }
//////            else
//////            { Parameters[1].Value = FieldInfo.dtPedido; }
//////
//////            //Field dtSaidaPedido
//////            Parameters[2].SqlDbType = SqlDbType.SmallDateTime;
//////            Parameters[2].ParameterName = "@Param_dtSaidaPedido";
//////            if ( FieldInfo.dtSaidaPedido == DateTime.MinValue )
//////            { Parameters[2].Value = DBNull.Value; }
//////            else
//////            { Parameters[2].Value = FieldInfo.dtSaidaPedido; }
//////
//////            //Field tipoPedido
//////            Parameters[3].SqlDbType = SqlDbType.VarChar;
//////            Parameters[3].ParameterName = "@Param_tipoPedido";
//////            if (( FieldInfo.tipoPedido == null ) || ( FieldInfo.tipoPedido == string.Empty ))
//////            { Parameters[3].Value = DBNull.Value; }
//////            else
//////            { Parameters[3].Value = FieldInfo.tipoPedido; }
//////            Parameters[3].Size = 50;
//////
//////            //Field situacaoPedido
//////            Parameters[4].SqlDbType = SqlDbType.VarChar;
//////            Parameters[4].ParameterName = "@Param_situacaoPedido";
//////            if (( FieldInfo.situacaoPedido == null ) || ( FieldInfo.situacaoPedido == string.Empty ))
//////            { Parameters[4].Value = DBNull.Value; }
//////            else
//////            { Parameters[4].Value = FieldInfo.situacaoPedido; }
//////            Parameters[4].Size = 50;
//////
//////            //Field tipoEntregaPedido
//////            Parameters[5].SqlDbType = SqlDbType.VarChar;
//////            Parameters[5].ParameterName = "@Param_tipoEntregaPedido";
//////            if (( FieldInfo.tipoEntregaPedido == null ) || ( FieldInfo.tipoEntregaPedido == string.Empty ))
//////            { Parameters[5].Value = DBNull.Value; }
//////            else
//////            { Parameters[5].Value = FieldInfo.tipoEntregaPedido; }
//////            Parameters[5].Size = 50;
//////
//////            //Field fkCliente
//////            Parameters[6].SqlDbType = SqlDbType.Int;
//////            Parameters[6].ParameterName = "@Param_fkCliente";
//////            Parameters[6].Value = FieldInfo.fkCliente;
//////
//////            //Field fkUsuario
//////            Parameters[7].SqlDbType = SqlDbType.Int;
//////            Parameters[7].ParameterName = "@Param_fkUsuario";
//////            Parameters[7].Value = FieldInfo.fkUsuario;
//////
//////            //Field fkTipoPagamento
//////            Parameters[8].SqlDbType = SqlDbType.Int;
//////            Parameters[8].ParameterName = "@Param_fkTipoPagamento";
//////            Parameters[8].Value = FieldInfo.fkTipoPagamento;
//////
//////            //Field fkFormaPagamento
//////            Parameters[9].SqlDbType = SqlDbType.Int;
//////            Parameters[9].ParameterName = "@Param_fkFormaPagamento";
//////            Parameters[9].Value = FieldInfo.fkFormaPagamento;
//////
//////            //Field valorTotalPedido
//////            Parameters[10].SqlDbType = SqlDbType.Decimal;
//////            Parameters[10].ParameterName = "@Param_valorTotalPedido";
//////            Parameters[10].Value = FieldInfo.valorTotalPedido;
//////
//////            //Field numeroPedido
//////            Parameters[11].SqlDbType = SqlDbType.Int;
//////            Parameters[11].ParameterName = "@Param_numeroPedido";
//////            Parameters[11].Value = FieldInfo.numeroPedido;
//////
//////            //Field fkLoja
//////            Parameters[12].SqlDbType = SqlDbType.Int;
//////            Parameters[12].ParameterName = "@Param_fkLoja";
//////            Parameters[12].Value = FieldInfo.fkLoja;
//////
//////            //Field numeroNF
//////            Parameters[13].SqlDbType = SqlDbType.VarChar;
//////            Parameters[13].ParameterName = "@Param_numeroNF";
//////            if (( FieldInfo.numeroNF == null ) || ( FieldInfo.numeroNF == string.Empty ))
//////            { Parameters[13].Value = DBNull.Value; }
//////            else
//////            { Parameters[13].Value = FieldInfo.numeroNF; }
//////            Parameters[13].Size = 200;
//////
//////            //Field Observacao
//////            Parameters[14].SqlDbType = SqlDbType.NText;
//////            Parameters[14].ParameterName = "@Param_Observacao";
//////            if (( FieldInfo.Observacao == null ) || ( FieldInfo.Observacao == string.Empty ))
//////            { Parameters[14].Value = DBNull.Value; }
//////            else
//////            { Parameters[14].Value = FieldInfo.Observacao; }
//////            Parameters[14].Size = 0;
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
//////        ~PedidoControl() 
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
////////    /// Tabela: Pedido  
////////    /// Autor: DAL Creator .net 
////////    /// Data de criação: 24/05/2013 15:18:26 
////////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "PedidoFields". 
////////    /// </summary> 
////////    public class PedidoControl : IDisposable 
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
////////        public PedidoControl() {}
////////
////////
////////        #region Inserindo dados na tabela 
////////
////////        /// <summary> 
////////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
////////        /// </summary>
////////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Add( ref PedidoFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Conn = new SqlConnection(this.StrConnetionDB);
////////                this.Conn.Open();
////////                this.Tran = this.Conn.BeginTransaction();
////////                this.Cmd = new SqlCommand("Proc_Pedido_Add", this.Conn, this.Tran);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////////                this.Tran.Commit();
////////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
////////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
////////        /// </summary>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref PedidoFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Cmd = new SqlCommand("Proc_Pedido_Add", ConnIn, TranIn);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
////////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
////////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
////////        /// </summary>
////////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Update( PedidoFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Conn = new SqlConnection(this.StrConnetionDB);
////////                this.Conn.Open();
////////                this.Tran = this.Conn.BeginTransaction();
////////                this.Cmd = new SqlCommand("Proc_Pedido_Update", this.Conn, this.Tran);
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
////////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
////////        /// </summary>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
////////        {
////////            try
////////            {
////////                this.Cmd = new SqlCommand("Proc_Pedido_Update", ConnIn, TranIn);
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
////////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", this.Conn, this.Tran);
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
////////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", ConnIn, TranIn);
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
////////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete( PedidoFields FieldInfo )
////////        {
////////            return Delete(FieldInfo.idPedido);
////////        }
////////
////////        /// <summary> 
////////        /// Exclui um registro da tabela no banco de dados
////////        /// </summary>
////////        /// <param name="Param_idPedido">int</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete(
////////                                     int Param_idPedido)
////////        {
////////            try
////////            {
////////                this.Conn = new SqlConnection(this.StrConnetionDB);
////////                this.Conn.Open();
////////                this.Tran = this.Conn.BeginTransaction();
////////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", this.Conn, this.Tran);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
////////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
////////        {
////////            return Delete(ConnIn, TranIn, FieldInfo.idPedido);
////////        }
////////
////////        /// <summary> 
////////        /// Exclui um registro da tabela no banco de dados
////////        /// </summary>
////////        /// <param name="Param_idPedido">int</param>
////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
////////                                     int Param_idPedido)
////////        {
////////            try
////////            {
////////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", ConnIn, TranIn);
////////                this.Cmd.CommandType = CommandType.StoredProcedure;
////////                this.Cmd.Parameters.Clear();
////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
////////        /// Retorna um objeto PedidoFields através da chave primária passada como parâmetro
////////        /// </summary>
////////        /// <param name="Param_idPedido">int</param>
////////        /// <returns>Objeto PedidoFields</returns> 
////////        public PedidoFields GetItem(
////////                                     int Param_idPedido)
////////        {
////////            PedidoFields infoFields = new PedidoFields();
////////            try
////////            {
////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////////                {
////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_Select", this.Conn))
////////                    {
////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
////////                        this.Cmd.Parameters.Clear();
////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
////////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto PedidoFields.
////////        /// </summary>
////////        /// <returns>List de objetos PedidoFields</returns> 
////////        public List<PedidoFields> GetAll()
////////        {
////////            List<PedidoFields> arrayInfo = new List<PedidoFields>();
////////            try
////////            {
////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
////////                {
////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_GetAll", this.Conn))
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
////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_CountAll", this.Conn))
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
////////        /// Retorna um objeto PedidoFields preenchido com os valores dos campos do SqlDataReader
////////        /// </summary>
////////        /// <param name="dr">SqlDataReader - Preenche o objeto PedidoFields </param>
////////        /// <returns>PedidoFields</returns>
////////        private PedidoFields GetDataFromReader( SqlDataReader dr )
////////        {
////////            PedidoFields infoFields = new PedidoFields();
////////
////////            if (!dr.IsDBNull(0))
////////            { infoFields.idPedido = dr.GetInt32(0); }
////////            else
////////            { infoFields.idPedido = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(1))
////////            { infoFields.dtPedido = dr.GetDateTime(1); }
////////            else
////////            { infoFields.dtPedido = DateTime.MinValue; }
////////
////////
////////
////////            if (!dr.IsDBNull(2))
////////            { infoFields.dtSaidaPedido = dr.GetDateTime(2); }
////////            else
////////            { infoFields.dtSaidaPedido = DateTime.MinValue; }
////////
////////
////////
////////            if (!dr.IsDBNull(3))
////////            { infoFields.tipoPedido = dr.GetString(3); }
////////            else
////////            { infoFields.tipoPedido = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(4))
////////            { infoFields.situacaoPedido = dr.GetString(4); }
////////            else
////////            { infoFields.situacaoPedido = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(5))
////////            { infoFields.tipoEntregaPedido = dr.GetString(5); }
////////            else
////////            { infoFields.tipoEntregaPedido = string.Empty; }
////////
////////
////////
////////            if (!dr.IsDBNull(6))
////////            { infoFields.fkCliente = dr.GetInt32(6); }
////////            else
////////            { infoFields.fkCliente = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(7))
////////            { infoFields.fkUsuario = dr.GetInt32(7); }
////////            else
////////            { infoFields.fkUsuario = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(8))
////////            { infoFields.fkTipoPagamento = dr.GetInt32(8); }
////////            else
////////            { infoFields.fkTipoPagamento = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(9))
////////            { infoFields.fkFormaPagamento = dr.GetInt32(9); }
////////            else
////////            { infoFields.fkFormaPagamento = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(10))
////////            { infoFields.valorTotalPedido = dr.GetDecimal(10); }
////////            else
////////            { infoFields.valorTotalPedido = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(11))
////////            { infoFields.numeroPedido = dr.GetInt32(11); }
////////            else
////////            { infoFields.numeroPedido = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(12))
////////            { infoFields.fkLoja = dr.GetInt32(12); }
////////            else
////////            { infoFields.fkLoja = 0; }
////////
////////
////////
////////            if (!dr.IsDBNull(13))
////////            { infoFields.numeroNF = dr.GetString(13); }
////////            else
////////            { infoFields.numeroNF = string.Empty; }
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
////////        #region Função GetAllParameters
////////
////////        /// <summary> 
////////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
////////        /// </summary>
////////        /// <param name="FieldInfo">Objeto PedidoFields</param>
////////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
////////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
////////        private SqlParameter[] GetAllParameters( PedidoFields FieldInfo, SQLMode Modo )
////////        {
////////            SqlParameter[] Parameters;
////////
////////            switch (Modo)
////////            {
////////                case SQLMode.Add:
////////                    Parameters = new SqlParameter[14];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    //Field idPedido
////////                    Parameters[0].SqlDbType = SqlDbType.Int;
////////                    Parameters[0].Direction = ParameterDirection.Output;
////////                    Parameters[0].ParameterName = "@Param_idPedido";
////////                    Parameters[0].Value = DBNull.Value;
////////
////////                    break;
////////
////////                case SQLMode.Update:
////////                    Parameters = new SqlParameter[14];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    //Field idPedido
////////                    Parameters[0].SqlDbType = SqlDbType.Int;
////////                    Parameters[0].ParameterName = "@Param_idPedido";
////////                    Parameters[0].Value = FieldInfo.idPedido;
////////
////////                    break;
////////
////////                case SQLMode.SelectORDelete:
////////                    Parameters = new SqlParameter[1];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    //Field idPedido
////////                    Parameters[0].SqlDbType = SqlDbType.Int;
////////                    Parameters[0].ParameterName = "@Param_idPedido";
////////                    Parameters[0].Value = FieldInfo.idPedido;
////////
////////                    return Parameters;
////////
////////                default:
////////                    Parameters = new SqlParameter[14];
////////                    for (int I = 0; I < Parameters.Length; I++)
////////                       Parameters[I] = new SqlParameter();
////////                    break;
////////            }
////////
////////            //Field dtPedido
////////            Parameters[1].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[1].ParameterName = "@Param_dtPedido";
////////            if ( FieldInfo.dtPedido == DateTime.MinValue )
////////            { Parameters[1].Value = DBNull.Value; }
////////            else
////////            { Parameters[1].Value = FieldInfo.dtPedido; }
////////
////////            //Field dtSaidaPedido
////////            Parameters[2].SqlDbType = SqlDbType.SmallDateTime;
////////            Parameters[2].ParameterName = "@Param_dtSaidaPedido";
////////            if ( FieldInfo.dtSaidaPedido == DateTime.MinValue )
////////            { Parameters[2].Value = DBNull.Value; }
////////            else
////////            { Parameters[2].Value = FieldInfo.dtSaidaPedido; }
////////
////////            //Field tipoPedido
////////            Parameters[3].SqlDbType = SqlDbType.VarChar;
////////            Parameters[3].ParameterName = "@Param_tipoPedido";
////////            if (( FieldInfo.tipoPedido == null ) || ( FieldInfo.tipoPedido == string.Empty ))
////////            { Parameters[3].Value = DBNull.Value; }
////////            else
////////            { Parameters[3].Value = FieldInfo.tipoPedido; }
////////            Parameters[3].Size = 50;
////////
////////            //Field situacaoPedido
////////            Parameters[4].SqlDbType = SqlDbType.VarChar;
////////            Parameters[4].ParameterName = "@Param_situacaoPedido";
////////            if (( FieldInfo.situacaoPedido == null ) || ( FieldInfo.situacaoPedido == string.Empty ))
////////            { Parameters[4].Value = DBNull.Value; }
////////            else
////////            { Parameters[4].Value = FieldInfo.situacaoPedido; }
////////            Parameters[4].Size = 50;
////////
////////            //Field tipoEntregaPedido
////////            Parameters[5].SqlDbType = SqlDbType.VarChar;
////////            Parameters[5].ParameterName = "@Param_tipoEntregaPedido";
////////            if (( FieldInfo.tipoEntregaPedido == null ) || ( FieldInfo.tipoEntregaPedido == string.Empty ))
////////            { Parameters[5].Value = DBNull.Value; }
////////            else
////////            { Parameters[5].Value = FieldInfo.tipoEntregaPedido; }
////////            Parameters[5].Size = 50;
////////
////////            //Field fkCliente
////////            Parameters[6].SqlDbType = SqlDbType.Int;
////////            Parameters[6].ParameterName = "@Param_fkCliente";
////////            Parameters[6].Value = FieldInfo.fkCliente;
////////
////////            //Field fkUsuario
////////            Parameters[7].SqlDbType = SqlDbType.Int;
////////            Parameters[7].ParameterName = "@Param_fkUsuario";
////////            Parameters[7].Value = FieldInfo.fkUsuario;
////////
////////            //Field fkTipoPagamento
////////            Parameters[8].SqlDbType = SqlDbType.Int;
////////            Parameters[8].ParameterName = "@Param_fkTipoPagamento";
////////            Parameters[8].Value = FieldInfo.fkTipoPagamento;
////////
////////            //Field fkFormaPagamento
////////            Parameters[9].SqlDbType = SqlDbType.Int;
////////            Parameters[9].ParameterName = "@Param_fkFormaPagamento";
////////            Parameters[9].Value = FieldInfo.fkFormaPagamento;
////////
////////            //Field valorTotalPedido
////////            Parameters[10].SqlDbType = SqlDbType.Decimal;
////////            Parameters[10].ParameterName = "@Param_valorTotalPedido";
////////            Parameters[10].Value = FieldInfo.valorTotalPedido;
////////
////////            //Field numeroPedido
////////            Parameters[11].SqlDbType = SqlDbType.Int;
////////            Parameters[11].ParameterName = "@Param_numeroPedido";
////////            Parameters[11].Value = FieldInfo.numeroPedido;
////////
////////            //Field fkLoja
////////            Parameters[12].SqlDbType = SqlDbType.Int;
////////            Parameters[12].ParameterName = "@Param_fkLoja";
////////            Parameters[12].Value = FieldInfo.fkLoja;
////////
////////            //Field numeroNF
////////            Parameters[13].SqlDbType = SqlDbType.VarChar;
////////            Parameters[13].ParameterName = "@Param_numeroNF";
////////            if (( FieldInfo.numeroNF == null ) || ( FieldInfo.numeroNF == string.Empty ))
////////            { Parameters[13].Value = DBNull.Value; }
////////            else
////////            { Parameters[13].Value = FieldInfo.numeroNF; }
////////            Parameters[13].Size = 200;
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
////////        ~PedidoControl() 
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
//////////    /// Tabela: Pedido  
//////////    /// Autor: DAL Creator .net 
//////////    /// Data de criação: 28/04/2013 19:05:15 
//////////    /// Descrição: Classe responsável pela perssitência de dados. Utiliza a classe "PedidoFields". 
//////////    /// </summary> 
//////////    public class PedidoControl : IDisposable 
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
//////////        public PedidoControl() {}
//////////
//////////
//////////        #region Inserindo dados na tabela 
//////////
//////////        /// <summary> 
//////////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Add( ref PedidoFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////////                this.Conn.Open();
//////////                this.Tran = this.Conn.BeginTransaction();
//////////                this.Cmd = new SqlCommand("Proc_Pedido_Add", this.Conn, this.Tran);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////////                this.Tran.Commit();
//////////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
//////////        /// Grava/Persiste um novo objeto PedidoFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <param name="FieldInfo">Objeto PedidoFields a ser gravado.Caso o parâmetro solicite a expressão "ref", será adicionado um novo valor a algum campo auto incremento.</param>
//////////        /// <returns>"true" = registro gravado com sucesso, "false" = erro ao gravar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Add( SqlConnection ConnIn, SqlTransaction TranIn, ref PedidoFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Cmd = new SqlCommand("Proc_Pedido_Add", ConnIn, TranIn);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.AddRange(GetAllParameters(FieldInfo, SQLMode.Add));
//////////                if (!(this.Cmd.ExecuteNonQuery() > 0)) throw new Exception("Erro ao tentar inserir registro!!");
//////////                FieldInfo.idPedido = (int)this.Cmd.Parameters["@Param_idPedido"].Value;
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
//////////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
//////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Update( PedidoFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////////                this.Conn.Open();
//////////                this.Tran = this.Conn.BeginTransaction();
//////////                this.Cmd = new SqlCommand("Proc_Pedido_Update", this.Conn, this.Tran);
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
//////////        /// Grava/Persiste as alterações em um objeto PedidoFields no banco de dados
//////////        /// </summary>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <param name="FieldInfo">Objeto PedidoFields a ser alterado.</param>
//////////        /// <returns>"true" = registro alterado com sucesso, "false" = erro ao tentar alterar registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Update( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
//////////        {
//////////            try
//////////            {
//////////                this.Cmd = new SqlCommand("Proc_Pedido_Update", ConnIn, TranIn);
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
//////////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", this.Conn, this.Tran);
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
//////////                this.Cmd = new SqlCommand("Proc_Pedido_DeleteAll", ConnIn, TranIn);
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
//////////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete( PedidoFields FieldInfo )
//////////        {
//////////            return Delete(FieldInfo.idPedido);
//////////        }
//////////
//////////        /// <summary> 
//////////        /// Exclui um registro da tabela no banco de dados
//////////        /// </summary>
//////////        /// <param name="Param_idPedido">int</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete(
//////////                                     int Param_idPedido)
//////////        {
//////////            try
//////////            {
//////////                this.Conn = new SqlConnection(this.StrConnetionDB);
//////////                this.Conn.Open();
//////////                this.Tran = this.Conn.BeginTransaction();
//////////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", this.Conn, this.Tran);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//////////        /// <param name="FieldInfo">Objeto PedidoFields a ser excluído.</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, PedidoFields FieldInfo )
//////////        {
//////////            return Delete(ConnIn, TranIn, FieldInfo.idPedido);
//////////        }
//////////
//////////        /// <summary> 
//////////        /// Exclui um registro da tabela no banco de dados
//////////        /// </summary>
//////////        /// <param name="Param_idPedido">int</param>
//////////        /// <param name="ConnIn">Objeto SqlConnection responsável pela conexão com o banco de dados.</param>
//////////        /// <param name="TranIn">Objeto SqlTransaction responsável pela transação iniciada no banco de dados.</param>
//////////        /// <returns>"true" = registro excluido com sucesso, "false" = erro ao tentar excluir registro (consulte a propriedade ErrorMessage para detalhes)</returns> 
//////////        public bool Delete( SqlConnection ConnIn, SqlTransaction TranIn, 
//////////                                     int Param_idPedido)
//////////        {
//////////            try
//////////            {
//////////                this.Cmd = new SqlCommand("Proc_Pedido_Delete", ConnIn, TranIn);
//////////                this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                this.Cmd.Parameters.Clear();
//////////                this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//////////        /// Retorna um objeto PedidoFields através da chave primária passada como parâmetro
//////////        /// </summary>
//////////        /// <param name="Param_idPedido">int</param>
//////////        /// <returns>Objeto PedidoFields</returns> 
//////////        public PedidoFields GetItem(
//////////                                     int Param_idPedido)
//////////        {
//////////            PedidoFields infoFields = new PedidoFields();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_Select", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_idPedido", SqlDbType.Int)).Value = Param_idPedido;
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
//////////        /// Seleciona todos os registro da tabela e preenche um ArrayList com o objeto PedidoFields.
//////////        /// </summary>
//////////        /// <returns>List de objetos PedidoFields</returns> 
//////////        public List<PedidoFields> GetAll()
//////////        {
//////////            List<PedidoFields> arrayInfo = new List<PedidoFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_GetAll", this.Conn))
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
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_CountAll", this.Conn))
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
//////////        #region Selecionando dados da tabela através do campo "tipoPedido" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo tipoPedido.
//////////        /// </summary>
//////////        /// <param name="Param_tipoPedido">string</param>
//////////        /// <returns>List PedidoFields</returns> 
//////////        public List<PedidoFields> FindBytipoPedido(
//////////                               string Param_tipoPedido )
//////////        {
//////////            List<PedidoFields> arrayList = new List<PedidoFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindBytipoPedido", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_tipoPedido", SqlDbType.VarChar, 50)).Value = Param_tipoPedido;
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
//////////        #region Selecionando dados da tabela através do campo "situacaoPedido" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo situacaoPedido.
//////////        /// </summary>
//////////        /// <param name="Param_situacaoPedido">string</param>
//////////        /// <returns>List PedidoFields</returns> 
//////////        public List<PedidoFields> FindBysituacaoPedido(
//////////                               string Param_situacaoPedido )
//////////        {
//////////            List<PedidoFields> arrayList = new List<PedidoFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindBysituacaoPedido", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_situacaoPedido", SqlDbType.VarChar, 50)).Value = Param_situacaoPedido;
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
//////////        #region Selecionando dados da tabela através do campo "fkCliente" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkCliente.
//////////        /// </summary>
//////////        /// <param name="Param_fkCliente">int</param>
//////////        /// <returns>List PedidoFields</returns> 
//////////        public List<PedidoFields> FindByfkCliente(
//////////                               int Param_fkCliente )
//////////        {
//////////            List<PedidoFields> arrayList = new List<PedidoFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindByfkCliente", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkCliente", SqlDbType.Int)).Value = Param_fkCliente;
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
//////////        #region Selecionando dados da tabela através do campo "fkTipoPagamento" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkTipoPagamento.
//////////        /// </summary>
//////////        /// <param name="Param_fkTipoPagamento">int</param>
//////////        /// <returns>List PedidoFields</returns> 
//////////        public List<PedidoFields> FindByfkTipoPagamento(
//////////                               int Param_fkTipoPagamento )
//////////        {
//////////            List<PedidoFields> arrayList = new List<PedidoFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindByfkTipoPagamento", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkTipoPagamento", SqlDbType.Int)).Value = Param_fkTipoPagamento;
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
//////////        #region Selecionando dados da tabela através do campo "numeroPedido" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo numeroPedido.
//////////        /// </summary>
//////////        /// <param name="Param_numeroPedido">int</param>
//////////        /// <returns>PedidoFields</returns> 
//////////        public PedidoFields FindBynumeroPedido(
//////////                               int Param_numeroPedido )
//////////        {
//////////            PedidoFields infoFields = new PedidoFields();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindBynumeroPedido", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_numeroPedido", SqlDbType.Int)).Value = Param_numeroPedido;
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
//////////                }
//////////
//////////                return infoFields;
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
//////////        #region Selecionando dados da tabela através do campo "fkLoja" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo fkLoja.
//////////        /// </summary>
//////////        /// <param name="Param_fkLoja">int</param>
//////////        /// <returns>List PedidoFields</returns> 
//////////        public List<PedidoFields> FindByfkLoja(
//////////                               int Param_fkLoja )
//////////        {
//////////            List<PedidoFields> arrayList = new List<PedidoFields>();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindByfkLoja", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_fkLoja", SqlDbType.Int)).Value = Param_fkLoja;
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
//////////        #region Selecionando dados da tabela através do campo "numeroNF" 
//////////
//////////        /// <summary> 
//////////        /// Retorna um ou mais registros da tabela no banco de dados, filtrados pelo campo numeroNF.
//////////        /// </summary>
//////////        /// <param name="Param_numeroNF">string</param>
//////////        /// <returns>PedidoFields</returns> 
//////////        public PedidoFields FindBynumeroNF(
//////////                               string Param_numeroNF )
//////////        {
//////////            PedidoFields infoFields = new PedidoFields();
//////////            try
//////////            {
//////////                using (this.Conn = new SqlConnection(this.StrConnetionDB))
//////////                {
//////////                    using (this.Cmd = new SqlCommand("Proc_Pedido_FindBynumeroNF", this.Conn))
//////////                    {
//////////                        this.Cmd.CommandType = CommandType.StoredProcedure;
//////////                        this.Cmd.Parameters.Clear();
//////////                        this.Cmd.Parameters.Add(new SqlParameter("@Param_numeroNF", SqlDbType.VarChar, 200)).Value = Param_numeroNF;
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
//////////                }
//////////
//////////                return infoFields;
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
//////////        /// Retorna um objeto PedidoFields preenchido com os valores dos campos do SqlDataReader
//////////        /// </summary>
//////////        /// <param name="dr">SqlDataReader - Preenche o objeto PedidoFields </param>
//////////        /// <returns>PedidoFields</returns>
//////////        private PedidoFields GetDataFromReader( SqlDataReader dr )
//////////        {
//////////            PedidoFields infoFields = new PedidoFields();
//////////
//////////            if (!dr.IsDBNull(0))
//////////            { infoFields.idPedido = dr.GetInt32(0); }
//////////            else
//////////            { infoFields.idPedido = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(1))
//////////            { infoFields.dtPedido = dr.GetDateTime(1); }
//////////            else
//////////            { infoFields.dtPedido = DateTime.MinValue; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(2))
//////////            { infoFields.dtSaidaPedido = dr.GetDateTime(2); }
//////////            else
//////////            { infoFields.dtSaidaPedido = DateTime.MinValue; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(3))
//////////            { infoFields.tipoPedido = dr.GetString(3); }
//////////            else
//////////            { infoFields.tipoPedido = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(4))
//////////            { infoFields.situacaoPedido = dr.GetString(4); }
//////////            else
//////////            { infoFields.situacaoPedido = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(5))
//////////            { infoFields.tipoEntregaPedido = dr.GetString(5); }
//////////            else
//////////            { infoFields.tipoEntregaPedido = string.Empty; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(6))
//////////            { infoFields.fkCliente = dr.GetInt32(6); }
//////////            else
//////////            { infoFields.fkCliente = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(7))
//////////            { infoFields.fkUsuario = dr.GetInt32(7); }
//////////            else
//////////            { infoFields.fkUsuario = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(8))
//////////            { infoFields.fkTipoPagamento = dr.GetInt32(8); }
//////////            else
//////////            { infoFields.fkTipoPagamento = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(9))
//////////            { infoFields.fkFormaPagamento = dr.GetInt32(9); }
//////////            else
//////////            { infoFields.fkFormaPagamento = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(10))
//////////            { infoFields.valorTotalPedido = dr.GetDecimal(10); }
//////////            else
//////////            { infoFields.valorTotalPedido = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(11))
//////////            { infoFields.numeroPedido = dr.GetInt32(11); }
//////////            else
//////////            { infoFields.numeroPedido = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(12))
//////////            { infoFields.fkLoja = dr.GetInt32(12); }
//////////            else
//////////            { infoFields.fkLoja = 0; }
//////////
//////////
//////////
//////////            if (!dr.IsDBNull(13))
//////////            { infoFields.numeroNF = dr.GetString(13); }
//////////            else
//////////            { infoFields.numeroNF = string.Empty; }
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
//////////        #region Função GetAllParameters
//////////
//////////        /// <summary> 
//////////        /// Retorna um array de parâmetros com campos para atualização, seleção e inserção no banco de dados
//////////        /// </summary>
//////////        /// <param name="FieldInfo">Objeto PedidoFields</param>
//////////        /// <param name="Modo">Tipo de oepração a ser executada no banco de dados</param>
//////////        /// <returns>SqlParameter[] - Array de parâmetros</returns> 
//////////        private SqlParameter[] GetAllParameters( PedidoFields FieldInfo, SQLMode Modo )
//////////        {
//////////            SqlParameter[] Parameters;
//////////
//////////            switch (Modo)
//////////            {
//////////                case SQLMode.Add:
//////////                    Parameters = new SqlParameter[14];
//////////                    for (int I = 0; I < Parameters.Length; I++)
//////////                       Parameters[I] = new SqlParameter();
//////////                    //Field idPedido
//////////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////////                    Parameters[0].Direction = ParameterDirection.Output;
//////////                    Parameters[0].ParameterName = "@Param_idPedido";
//////////                    Parameters[0].Value = DBNull.Value;
//////////
//////////                    break;
//////////
//////////                case SQLMode.Update:
//////////                    Parameters = new SqlParameter[14];
//////////                    for (int I = 0; I < Parameters.Length; I++)
//////////                       Parameters[I] = new SqlParameter();
//////////                    //Field idPedido
//////////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////////                    Parameters[0].ParameterName = "@Param_idPedido";
//////////                    Parameters[0].Value = FieldInfo.idPedido;
//////////
//////////                    break;
//////////
//////////                case SQLMode.SelectORDelete:
//////////                    Parameters = new SqlParameter[1];
//////////                    for (int I = 0; I < Parameters.Length; I++)
//////////                       Parameters[I] = new SqlParameter();
//////////                    //Field idPedido
//////////                    Parameters[0].SqlDbType = SqlDbType.Int;
//////////                    Parameters[0].ParameterName = "@Param_idPedido";
//////////                    Parameters[0].Value = FieldInfo.idPedido;
//////////
//////////                    return Parameters;
//////////
//////////                default:
//////////                    Parameters = new SqlParameter[14];
//////////                    for (int I = 0; I < Parameters.Length; I++)
//////////                       Parameters[I] = new SqlParameter();
//////////                    break;
//////////            }
//////////
//////////            //Field dtPedido
//////////            Parameters[1].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[1].ParameterName = "@Param_dtPedido";
//////////            if ( FieldInfo.dtPedido == DateTime.MinValue )
//////////            { Parameters[1].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[1].Value = FieldInfo.dtPedido; }
//////////
//////////            //Field dtSaidaPedido
//////////            Parameters[2].SqlDbType = SqlDbType.SmallDateTime;
//////////            Parameters[2].ParameterName = "@Param_dtSaidaPedido";
//////////            if ( FieldInfo.dtSaidaPedido == DateTime.MinValue )
//////////            { Parameters[2].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[2].Value = FieldInfo.dtSaidaPedido; }
//////////
//////////            //Field tipoPedido
//////////            Parameters[3].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[3].ParameterName = "@Param_tipoPedido";
//////////            if (( FieldInfo.tipoPedido == null ) || ( FieldInfo.tipoPedido == string.Empty ))
//////////            { Parameters[3].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[3].Value = FieldInfo.tipoPedido; }
//////////            Parameters[3].Size = 50;
//////////
//////////            //Field situacaoPedido
//////////            Parameters[4].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[4].ParameterName = "@Param_situacaoPedido";
//////////            if (( FieldInfo.situacaoPedido == null ) || ( FieldInfo.situacaoPedido == string.Empty ))
//////////            { Parameters[4].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[4].Value = FieldInfo.situacaoPedido; }
//////////            Parameters[4].Size = 50;
//////////
//////////            //Field tipoEntregaPedido
//////////            Parameters[5].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[5].ParameterName = "@Param_tipoEntregaPedido";
//////////            if (( FieldInfo.tipoEntregaPedido == null ) || ( FieldInfo.tipoEntregaPedido == string.Empty ))
//////////            { Parameters[5].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[5].Value = FieldInfo.tipoEntregaPedido; }
//////////            Parameters[5].Size = 50;
//////////
//////////            //Field fkCliente
//////////            Parameters[6].SqlDbType = SqlDbType.Int;
//////////            Parameters[6].ParameterName = "@Param_fkCliente";
//////////            Parameters[6].Value = FieldInfo.fkCliente;
//////////
//////////            //Field fkUsuario
//////////            Parameters[7].SqlDbType = SqlDbType.Int;
//////////            Parameters[7].ParameterName = "@Param_fkUsuario";
//////////            Parameters[7].Value = FieldInfo.fkUsuario;
//////////
//////////            //Field fkTipoPagamento
//////////            Parameters[8].SqlDbType = SqlDbType.Int;
//////////            Parameters[8].ParameterName = "@Param_fkTipoPagamento";
//////////            Parameters[8].Value = FieldInfo.fkTipoPagamento;
//////////
//////////            //Field fkFormaPagamento
//////////            Parameters[9].SqlDbType = SqlDbType.Int;
//////////            Parameters[9].ParameterName = "@Param_fkFormaPagamento";
//////////            Parameters[9].Value = FieldInfo.fkFormaPagamento;
//////////
//////////            //Field valorTotalPedido
//////////            Parameters[10].SqlDbType = SqlDbType.Decimal;
//////////            Parameters[10].ParameterName = "@Param_valorTotalPedido";
//////////            Parameters[10].Value = FieldInfo.valorTotalPedido;
//////////
//////////            //Field numeroPedido
//////////            Parameters[11].SqlDbType = SqlDbType.Int;
//////////            Parameters[11].ParameterName = "@Param_numeroPedido";
//////////            Parameters[11].Value = FieldInfo.numeroPedido;
//////////
//////////            //Field fkLoja
//////////            Parameters[12].SqlDbType = SqlDbType.Int;
//////////            Parameters[12].ParameterName = "@Param_fkLoja";
//////////            Parameters[12].Value = FieldInfo.fkLoja;
//////////
//////////            //Field numeroNF
//////////            Parameters[13].SqlDbType = SqlDbType.VarChar;
//////////            Parameters[13].ParameterName = "@Param_numeroNF";
//////////            if (( FieldInfo.numeroNF == null ) || ( FieldInfo.numeroNF == string.Empty ))
//////////            { Parameters[13].Value = DBNull.Value; }
//////////            else
//////////            { Parameters[13].Value = FieldInfo.numeroNF; }
//////////            Parameters[13].Size = 200;
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
//////////        ~PedidoControl() 
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
