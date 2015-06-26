using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIML.Sentnela
{


    /// <summary> 
    /// Tabela: Cliente  
    /// Autor: DAL Creator .net  
    /// Data de criação: 23/06/2013 15:53:57 
    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Cliente 
    /// </summary> 
    public class ClienteFields 
    {

        private int _idCliente = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): Int 
        /// Somente Leitura/Auto Incremental
        /// </summary> 
        public int idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private string _nomeCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Sim 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string nomeCliente
        {
            get { return _nomeCliente.Trim(); }
            set { _nomeCliente = value.Trim(); }
        }

        private string _enderecoClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string enderecoClienteA
        {
            get { return _enderecoClienteA.Trim(); }
            set { _enderecoClienteA = value.Trim(); }
        }

        private string _enderecoClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string enderecoClienteB
        {
            get { return _enderecoClienteB.Trim(); }
            set { _enderecoClienteB = value.Trim(); }
        }

        private string _enderecoClienteC = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string enderecoClienteC
        {
            get { return _enderecoClienteC.Trim(); }
            set { _enderecoClienteC = value.Trim(); }
        }

        private string _bairroClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string bairroClienteA
        {
            get { return _bairroClienteA.Trim(); }
            set { _bairroClienteA = value.Trim(); }
        }

        private string _bairroClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string bairroClienteB
        {
            get { return _bairroClienteB.Trim(); }
            set { _bairroClienteB = value.Trim(); }
        }

        private string _bairroClientec = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string bairroClientec
        {
            get { return _bairroClientec.Trim(); }
            set { _bairroClientec = value.Trim(); }
        }

        private string _cidadeClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string cidadeClienteA
        {
            get { return _cidadeClienteA.Trim(); }
            set { _cidadeClienteA = value.Trim(); }
        }

        private string _cidadeClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string cidadeClienteB
        {
            get { return _cidadeClienteB.Trim(); }
            set { _cidadeClienteB = value.Trim(); }
        }

        private string _cidadeClienteC = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string cidadeClienteC
        {
            get { return _cidadeClienteC.Trim(); }
            set { _cidadeClienteC = value.Trim(); }
        }

        private string _estadoClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 2 
        /// </summary> 
        public string estadoClienteA
        {
            get { return _estadoClienteA.Trim(); }
            set { _estadoClienteA = value.Trim(); }
        }

        private string _estadoClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 2 
        /// </summary> 
        public string estadoClienteB
        {
            get { return _estadoClienteB.Trim(); }
            set { _estadoClienteB = value.Trim(); }
        }

        private string _estadoClienteC = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 2 
        /// </summary> 
        public string estadoClienteC
        {
            get { return _estadoClienteC.Trim(); }
            set { _estadoClienteC = value.Trim(); }
        }

        private string _cepClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 9 
        /// </summary> 
        public string cepClienteA
        {
            get { return _cepClienteA.Trim(); }
            set { _cepClienteA = value.Trim(); }
        }

        private string _cepClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 9 
        /// </summary> 
        public string cepClienteB
        {
            get { return _cepClienteB.Trim(); }
            set { _cepClienteB = value.Trim(); }
        }

        private string _cepClienteC = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 9 
        /// </summary> 
        public string cepClienteC
        {
            get { return _cepClienteC.Trim(); }
            set { _cepClienteC = value.Trim(); }
        }

        private string _telefoneClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string telefoneClienteA
        {
            get { return _telefoneClienteA.Trim(); }
            set { _telefoneClienteA = value.Trim(); }
        }

        private string _telefoneClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string telefoneClienteB
        {
            get { return _telefoneClienteB.Trim(); }
            set { _telefoneClienteB = value.Trim(); }
        }

        private string _telefoneClienteC = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string telefoneClienteC
        {
            get { return _telefoneClienteC.Trim(); }
            set { _telefoneClienteC = value.Trim(); }
        }

        private string _telefoneClienteD = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string telefoneClienteD
        {
            get { return _telefoneClienteD.Trim(); }
            set { _telefoneClienteD = value.Trim(); }
        }

        private string _celularClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string celularClienteA
        {
            get { return _celularClienteA.Trim(); }
            set { _celularClienteA = value.Trim(); }
        }

        private string _celularClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string celularClienteB
        {
            get { return _celularClienteB.Trim(); }
            set { _celularClienteB = value.Trim(); }
        }

        private string _celularClienteC = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string celularClienteC
        {
            get { return _celularClienteC.Trim(); }
            set { _celularClienteC = value.Trim(); }
        }

        private string _complementoCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 100 
        /// </summary> 
        public string complementoCliente
        {
            get { return _complementoCliente.Trim(); }
            set { _complementoCliente = value.Trim(); }
        }

        private DateTime _dataNascimentoCliente = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dataNascimentoCliente
        {
            get { return _dataNascimentoCliente; }
            set { _dataNascimentoCliente = value; }
        }

        private string _emailClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string emailClienteA
        {
            get { return _emailClienteA.Trim(); }
            set { _emailClienteA = value.Trim(); }
        }

        private string _emailClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string emailClienteB
        {
            get { return _emailClienteB.Trim(); }
            set { _emailClienteB = value.Trim(); }
        }

        private string _contatoClienteA = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string contatoClienteA
        {
            get { return _contatoClienteA.Trim(); }
            set { _contatoClienteA = value.Trim(); }
        }

        private string _contatoClienteB = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string contatoClienteB
        {
            get { return _contatoClienteB.Trim(); }
            set { _contatoClienteB = value.Trim(); }
        }

        private string _contatoClienteC = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 255 
        /// </summary> 
        public string contatoClienteC
        {
            get { return _contatoClienteC.Trim(); }
            set { _contatoClienteC = value.Trim(); }
        }

        private string _cnpjCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string cnpjCliente
        {
            get { return _cnpjCliente.Trim(); }
            set { _cnpjCliente = value.Trim(); }
        }

        private string _cpfCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string cpfCliente
        {
            get { return _cpfCliente.Trim(); }
            set { _cpfCliente = value.Trim(); }
        }

        private string _rgCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string rgCliente
        {
            get { return _rgCliente.Trim(); }
            set { _rgCliente = value.Trim(); }
        }

        private string _inscEstadualCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string inscEstadualCliente
        {
            get { return _inscEstadualCliente.Trim(); }
            set { _inscEstadualCliente = value.Trim(); }
        }

        private string _observacoesCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 300 
        /// </summary> 
        public string observacoesCliente
        {
            get { return _observacoesCliente.Trim(); }
            set { _observacoesCliente = value.Trim(); }
        }

        private DateTime _dataCadastroCliente = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dataCadastroCliente
        {
            get { return _dataCadastroCliente; }
            set { _dataCadastroCliente = value; }
        }

        private string _tipoCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 20 
        /// </summary> 
        public string tipoCliente
        {
            get { return _tipoCliente.Trim(); }
            set { _tipoCliente = value.Trim(); }
        }

        private string _statusCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Sim 
        /// Estilo: Normal  
        /// Tamanho Máximo: 2 
        /// </summary> 
        public string statusCliente
        {
            get { return _statusCliente.Trim(); }
            set { _statusCliente = value.Trim(); }
        }

        private int _fkSubGrupoCliente = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Sim 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int fkSubGrupoCliente
        {
            get { return _fkSubGrupoCliente; }
            set { _fkSubGrupoCliente = value; }
        }

        private DateTime _dataUltimaCompraCliente = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dataUltimaCompraCliente
        {
            get { return _dataUltimaCompraCliente; }
            set { _dataUltimaCompraCliente = value; }
        }

        private string _numeroCasaCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): varchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 30 
        /// </summary> 
        public string numeroCasaCliente
        {
            get { return _numeroCasaCliente.Trim(); }
            set { _numeroCasaCliente = value.Trim(); }
        }

        private string _faxCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): varchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string faxCliente
        {
            get { return _faxCliente.Trim(); }
            set { _faxCliente = value.Trim(); }
        }

        private DateTime _dataNascimentoClienteA = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dataNascimentoClienteA
        {
            get { return _dataNascimentoClienteA; }
            set { _dataNascimentoClienteA = value; }
        }

        private DateTime _dataNascimentoClienteB = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dataNascimentoClienteB
        {
            get { return _dataNascimentoClienteB; }
            set { _dataNascimentoClienteB = value; }
        }

        private DateTime _dataNascimentoClienteC = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dataNascimentoClienteC
        {
            get { return _dataNascimentoClienteC; }
            set { _dataNascimentoClienteC = value; }
        }

        private string _emailPrincipalCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): varchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 150 
        /// </summary> 
        public string emailPrincipalCliente
        {
            get { return _emailPrincipalCliente.Trim(); }
            set { _emailPrincipalCliente = value.Trim(); }
        }



        public ClienteFields() {}

        public ClienteFields(
                        string Param_nomeCliente, 
                        string Param_enderecoClienteA, 
                        string Param_enderecoClienteB, 
                        string Param_enderecoClienteC, 
                        string Param_bairroClienteA, 
                        string Param_bairroClienteB, 
                        string Param_bairroClientec, 
                        string Param_cidadeClienteA, 
                        string Param_cidadeClienteB, 
                        string Param_cidadeClienteC, 
                        string Param_estadoClienteA, 
                        string Param_estadoClienteB, 
                        string Param_estadoClienteC, 
                        string Param_cepClienteA, 
                        string Param_cepClienteB, 
                        string Param_cepClienteC, 
                        string Param_telefoneClienteA, 
                        string Param_telefoneClienteB, 
                        string Param_telefoneClienteC, 
                        string Param_telefoneClienteD, 
                        string Param_celularClienteA, 
                        string Param_celularClienteB, 
                        string Param_celularClienteC, 
                        string Param_complementoCliente, 
                        DateTime Param_dataNascimentoCliente, 
                        string Param_emailClienteA, 
                        string Param_emailClienteB, 
                        string Param_contatoClienteA, 
                        string Param_contatoClienteB, 
                        string Param_contatoClienteC, 
                        string Param_cnpjCliente, 
                        string Param_cpfCliente, 
                        string Param_rgCliente, 
                        string Param_inscEstadualCliente, 
                        string Param_observacoesCliente, 
                        DateTime Param_dataCadastroCliente, 
                        string Param_tipoCliente, 
                        string Param_statusCliente, 
                        int Param_fkSubGrupoCliente, 
                        DateTime Param_dataUltimaCompraCliente, 
                        string Param_numeroCasaCliente, 
                        string Param_faxCliente, 
                        DateTime Param_dataNascimentoClienteA, 
                        DateTime Param_dataNascimentoClienteB, 
                        DateTime Param_dataNascimentoClienteC, 
                        string Param_emailPrincipalCliente)
        {
               this._nomeCliente = Param_nomeCliente;
               this._enderecoClienteA = Param_enderecoClienteA;
               this._enderecoClienteB = Param_enderecoClienteB;
               this._enderecoClienteC = Param_enderecoClienteC;
               this._bairroClienteA = Param_bairroClienteA;
               this._bairroClienteB = Param_bairroClienteB;
               this._bairroClientec = Param_bairroClientec;
               this._cidadeClienteA = Param_cidadeClienteA;
               this._cidadeClienteB = Param_cidadeClienteB;
               this._cidadeClienteC = Param_cidadeClienteC;
               this._estadoClienteA = Param_estadoClienteA;
               this._estadoClienteB = Param_estadoClienteB;
               this._estadoClienteC = Param_estadoClienteC;
               this._cepClienteA = Param_cepClienteA;
               this._cepClienteB = Param_cepClienteB;
               this._cepClienteC = Param_cepClienteC;
               this._telefoneClienteA = Param_telefoneClienteA;
               this._telefoneClienteB = Param_telefoneClienteB;
               this._telefoneClienteC = Param_telefoneClienteC;
               this._telefoneClienteD = Param_telefoneClienteD;
               this._celularClienteA = Param_celularClienteA;
               this._celularClienteB = Param_celularClienteB;
               this._celularClienteC = Param_celularClienteC;
               this._complementoCliente = Param_complementoCliente;
               this._dataNascimentoCliente = Param_dataNascimentoCliente;
               this._emailClienteA = Param_emailClienteA;
               this._emailClienteB = Param_emailClienteB;
               this._contatoClienteA = Param_contatoClienteA;
               this._contatoClienteB = Param_contatoClienteB;
               this._contatoClienteC = Param_contatoClienteC;
               this._cnpjCliente = Param_cnpjCliente;
               this._cpfCliente = Param_cpfCliente;
               this._rgCliente = Param_rgCliente;
               this._inscEstadualCliente = Param_inscEstadualCliente;
               this._observacoesCliente = Param_observacoesCliente;
               this._dataCadastroCliente = Param_dataCadastroCliente;
               this._tipoCliente = Param_tipoCliente;
               this._statusCliente = Param_statusCliente;
               this._fkSubGrupoCliente = Param_fkSubGrupoCliente;
               this._dataUltimaCompraCliente = Param_dataUltimaCompraCliente;
               this._numeroCasaCliente = Param_numeroCasaCliente;
               this._faxCliente = Param_faxCliente;
               this._dataNascimentoClienteA = Param_dataNascimentoClienteA;
               this._dataNascimentoClienteB = Param_dataNascimentoClienteB;
               this._dataNascimentoClienteC = Param_dataNascimentoClienteC;
               this._emailPrincipalCliente = Param_emailPrincipalCliente;
        }
    }

}




//Projeto substituído ------------------------
//using System;
//using System.Collections;
//using System.Collections.Generic;
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
//    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Cliente 
//    /// </summary> 
//    public class ClienteFields 
//    {
//
//        private int _idCliente = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): Int 
//        /// Somente Leitura/Auto Incremental
//        /// </summary> 
//        public int idCliente
//        {
//            get { return _idCliente; }
//            set { _idCliente = value; }
//        }
//
//        private string _nomeCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Sim 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string nomeCliente
//        {
//            get { return _nomeCliente.Trim(); }
//            set { _nomeCliente = value.Trim(); }
//        }
//
//        private string _enderecoClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string enderecoClienteA
//        {
//            get { return _enderecoClienteA.Trim(); }
//            set { _enderecoClienteA = value.Trim(); }
//        }
//
//        private string _enderecoClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string enderecoClienteB
//        {
//            get { return _enderecoClienteB.Trim(); }
//            set { _enderecoClienteB = value.Trim(); }
//        }
//
//        private string _enderecoClienteC = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string enderecoClienteC
//        {
//            get { return _enderecoClienteC.Trim(); }
//            set { _enderecoClienteC = value.Trim(); }
//        }
//
//        private string _bairroClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string bairroClienteA
//        {
//            get { return _bairroClienteA.Trim(); }
//            set { _bairroClienteA = value.Trim(); }
//        }
//
//        private string _bairroClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string bairroClienteB
//        {
//            get { return _bairroClienteB.Trim(); }
//            set { _bairroClienteB = value.Trim(); }
//        }
//
//        private string _bairroClientec = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string bairroClientec
//        {
//            get { return _bairroClientec.Trim(); }
//            set { _bairroClientec = value.Trim(); }
//        }
//
//        private string _cidadeClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string cidadeClienteA
//        {
//            get { return _cidadeClienteA.Trim(); }
//            set { _cidadeClienteA = value.Trim(); }
//        }
//
//        private string _cidadeClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string cidadeClienteB
//        {
//            get { return _cidadeClienteB.Trim(); }
//            set { _cidadeClienteB = value.Trim(); }
//        }
//
//        private string _cidadeClienteC = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string cidadeClienteC
//        {
//            get { return _cidadeClienteC.Trim(); }
//            set { _cidadeClienteC = value.Trim(); }
//        }
//
//        private string _estadoClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 2 
//        /// </summary> 
//        public string estadoClienteA
//        {
//            get { return _estadoClienteA.Trim(); }
//            set { _estadoClienteA = value.Trim(); }
//        }
//
//        private string _estadoClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 2 
//        /// </summary> 
//        public string estadoClienteB
//        {
//            get { return _estadoClienteB.Trim(); }
//            set { _estadoClienteB = value.Trim(); }
//        }
//
//        private string _estadoClienteC = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 2 
//        /// </summary> 
//        public string estadoClienteC
//        {
//            get { return _estadoClienteC.Trim(); }
//            set { _estadoClienteC = value.Trim(); }
//        }
//
//        private string _cepClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 9 
//        /// </summary> 
//        public string cepClienteA
//        {
//            get { return _cepClienteA.Trim(); }
//            set { _cepClienteA = value.Trim(); }
//        }
//
//        private string _cepClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 9 
//        /// </summary> 
//        public string cepClienteB
//        {
//            get { return _cepClienteB.Trim(); }
//            set { _cepClienteB = value.Trim(); }
//        }
//
//        private string _cepClienteC = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 9 
//        /// </summary> 
//        public string cepClienteC
//        {
//            get { return _cepClienteC.Trim(); }
//            set { _cepClienteC = value.Trim(); }
//        }
//
//        private string _telefoneClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string telefoneClienteA
//        {
//            get { return _telefoneClienteA.Trim(); }
//            set { _telefoneClienteA = value.Trim(); }
//        }
//
//        private string _telefoneClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string telefoneClienteB
//        {
//            get { return _telefoneClienteB.Trim(); }
//            set { _telefoneClienteB = value.Trim(); }
//        }
//
//        private string _telefoneClienteC = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string telefoneClienteC
//        {
//            get { return _telefoneClienteC.Trim(); }
//            set { _telefoneClienteC = value.Trim(); }
//        }
//
//        private string _telefoneClienteD = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string telefoneClienteD
//        {
//            get { return _telefoneClienteD.Trim(); }
//            set { _telefoneClienteD = value.Trim(); }
//        }
//
//        private string _celularClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string celularClienteA
//        {
//            get { return _celularClienteA.Trim(); }
//            set { _celularClienteA = value.Trim(); }
//        }
//
//        private string _celularClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string celularClienteB
//        {
//            get { return _celularClienteB.Trim(); }
//            set { _celularClienteB = value.Trim(); }
//        }
//
//        private string _celularClienteC = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string celularClienteC
//        {
//            get { return _celularClienteC.Trim(); }
//            set { _celularClienteC = value.Trim(); }
//        }
//
//        private string _complementoCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 100 
//        /// </summary> 
//        public string complementoCliente
//        {
//            get { return _complementoCliente.Trim(); }
//            set { _complementoCliente = value.Trim(); }
//        }
//
//        private DateTime _dataNascimentoCliente = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dataNascimentoCliente
//        {
//            get { return _dataNascimentoCliente; }
//            set { _dataNascimentoCliente = value; }
//        }
//
//        private string _emailClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string emailClienteA
//        {
//            get { return _emailClienteA.Trim(); }
//            set { _emailClienteA = value.Trim(); }
//        }
//
//        private string _emailClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string emailClienteB
//        {
//            get { return _emailClienteB.Trim(); }
//            set { _emailClienteB = value.Trim(); }
//        }
//
//        private string _contatoClienteA = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string contatoClienteA
//        {
//            get { return _contatoClienteA.Trim(); }
//            set { _contatoClienteA = value.Trim(); }
//        }
//
//        private string _contatoClienteB = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string contatoClienteB
//        {
//            get { return _contatoClienteB.Trim(); }
//            set { _contatoClienteB = value.Trim(); }
//        }
//
//        private string _contatoClienteC = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 255 
//        /// </summary> 
//        public string contatoClienteC
//        {
//            get { return _contatoClienteC.Trim(); }
//            set { _contatoClienteC = value.Trim(); }
//        }
//
//        private string _cnpjCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string cnpjCliente
//        {
//            get { return _cnpjCliente.Trim(); }
//            set { _cnpjCliente = value.Trim(); }
//        }
//
//        private string _cpfCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string cpfCliente
//        {
//            get { return _cpfCliente.Trim(); }
//            set { _cpfCliente = value.Trim(); }
//        }
//
//        private string _rgCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string rgCliente
//        {
//            get { return _rgCliente.Trim(); }
//            set { _rgCliente = value.Trim(); }
//        }
//
//        private string _inscEstadualCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string inscEstadualCliente
//        {
//            get { return _inscEstadualCliente.Trim(); }
//            set { _inscEstadualCliente = value.Trim(); }
//        }
//
//        private string _observacoesCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 300 
//        /// </summary> 
//        public string observacoesCliente
//        {
//            get { return _observacoesCliente.Trim(); }
//            set { _observacoesCliente = value.Trim(); }
//        }
//
//        private DateTime _dataCadastroCliente = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dataCadastroCliente
//        {
//            get { return _dataCadastroCliente; }
//            set { _dataCadastroCliente = value; }
//        }
//
//        private string _tipoCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 20 
//        /// </summary> 
//        public string tipoCliente
//        {
//            get { return _tipoCliente.Trim(); }
//            set { _tipoCliente = value.Trim(); }
//        }
//
//        private string _statusCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Sim 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 2 
//        /// </summary> 
//        public string statusCliente
//        {
//            get { return _statusCliente.Trim(); }
//            set { _statusCliente = value.Trim(); }
//        }
//
//        private int _fkSubGrupoCliente = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Sim 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int fkSubGrupoCliente
//        {
//            get { return _fkSubGrupoCliente; }
//            set { _fkSubGrupoCliente = value; }
//        }
//
//        private DateTime _dataUltimaCompraCliente = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dataUltimaCompraCliente
//        {
//            get { return _dataUltimaCompraCliente; }
//            set { _dataUltimaCompraCliente = value; }
//        }
//
//        private string _numeroCasaCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): varchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 30 
//        /// </summary> 
//        public string numeroCasaCliente
//        {
//            get { return _numeroCasaCliente.Trim(); }
//            set { _numeroCasaCliente = value.Trim(); }
//        }
//
//        private string _faxCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): varchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string faxCliente
//        {
//            get { return _faxCliente.Trim(); }
//            set { _faxCliente = value.Trim(); }
//        }
//
//        private DateTime _dataNascimentoClienteA = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dataNascimentoClienteA
//        {
//            get { return _dataNascimentoClienteA; }
//            set { _dataNascimentoClienteA = value; }
//        }
//
//        private DateTime _dataNascimentoClienteB = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dataNascimentoClienteB
//        {
//            get { return _dataNascimentoClienteB; }
//            set { _dataNascimentoClienteB = value; }
//        }
//
//        private DateTime _dataNascimentoClienteC = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dataNascimentoClienteC
//        {
//            get { return _dataNascimentoClienteC; }
//            set { _dataNascimentoClienteC = value; }
//        }
//
//        private string _emailPrincipalCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): varchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 1 
//        /// </summary> 
//        public string emailPrincipalCliente
//        {
//            get { return _emailPrincipalCliente.Trim(); }
//            set { _emailPrincipalCliente = value.Trim(); }
//        }
//
//
//
//        public ClienteFields() {}
//
//        public ClienteFields(
//                        string Param_nomeCliente, 
//                        string Param_enderecoClienteA, 
//                        string Param_enderecoClienteB, 
//                        string Param_enderecoClienteC, 
//                        string Param_bairroClienteA, 
//                        string Param_bairroClienteB, 
//                        string Param_bairroClientec, 
//                        string Param_cidadeClienteA, 
//                        string Param_cidadeClienteB, 
//                        string Param_cidadeClienteC, 
//                        string Param_estadoClienteA, 
//                        string Param_estadoClienteB, 
//                        string Param_estadoClienteC, 
//                        string Param_cepClienteA, 
//                        string Param_cepClienteB, 
//                        string Param_cepClienteC, 
//                        string Param_telefoneClienteA, 
//                        string Param_telefoneClienteB, 
//                        string Param_telefoneClienteC, 
//                        string Param_telefoneClienteD, 
//                        string Param_celularClienteA, 
//                        string Param_celularClienteB, 
//                        string Param_celularClienteC, 
//                        string Param_complementoCliente, 
//                        DateTime Param_dataNascimentoCliente, 
//                        string Param_emailClienteA, 
//                        string Param_emailClienteB, 
//                        string Param_contatoClienteA, 
//                        string Param_contatoClienteB, 
//                        string Param_contatoClienteC, 
//                        string Param_cnpjCliente, 
//                        string Param_cpfCliente, 
//                        string Param_rgCliente, 
//                        string Param_inscEstadualCliente, 
//                        string Param_observacoesCliente, 
//                        DateTime Param_dataCadastroCliente, 
//                        string Param_tipoCliente, 
//                        string Param_statusCliente, 
//                        int Param_fkSubGrupoCliente, 
//                        DateTime Param_dataUltimaCompraCliente, 
//                        string Param_numeroCasaCliente, 
//                        string Param_faxCliente, 
//                        DateTime Param_dataNascimentoClienteA, 
//                        DateTime Param_dataNascimentoClienteB, 
//                        DateTime Param_dataNascimentoClienteC, 
//                        string Param_emailPrincipalCliente)
//        {
//               this._nomeCliente = Param_nomeCliente;
//               this._enderecoClienteA = Param_enderecoClienteA;
//               this._enderecoClienteB = Param_enderecoClienteB;
//               this._enderecoClienteC = Param_enderecoClienteC;
//               this._bairroClienteA = Param_bairroClienteA;
//               this._bairroClienteB = Param_bairroClienteB;
//               this._bairroClientec = Param_bairroClientec;
//               this._cidadeClienteA = Param_cidadeClienteA;
//               this._cidadeClienteB = Param_cidadeClienteB;
//               this._cidadeClienteC = Param_cidadeClienteC;
//               this._estadoClienteA = Param_estadoClienteA;
//               this._estadoClienteB = Param_estadoClienteB;
//               this._estadoClienteC = Param_estadoClienteC;
//               this._cepClienteA = Param_cepClienteA;
//               this._cepClienteB = Param_cepClienteB;
//               this._cepClienteC = Param_cepClienteC;
//               this._telefoneClienteA = Param_telefoneClienteA;
//               this._telefoneClienteB = Param_telefoneClienteB;
//               this._telefoneClienteC = Param_telefoneClienteC;
//               this._telefoneClienteD = Param_telefoneClienteD;
//               this._celularClienteA = Param_celularClienteA;
//               this._celularClienteB = Param_celularClienteB;
//               this._celularClienteC = Param_celularClienteC;
//               this._complementoCliente = Param_complementoCliente;
//               this._dataNascimentoCliente = Param_dataNascimentoCliente;
//               this._emailClienteA = Param_emailClienteA;
//               this._emailClienteB = Param_emailClienteB;
//               this._contatoClienteA = Param_contatoClienteA;
//               this._contatoClienteB = Param_contatoClienteB;
//               this._contatoClienteC = Param_contatoClienteC;
//               this._cnpjCliente = Param_cnpjCliente;
//               this._cpfCliente = Param_cpfCliente;
//               this._rgCliente = Param_rgCliente;
//               this._inscEstadualCliente = Param_inscEstadualCliente;
//               this._observacoesCliente = Param_observacoesCliente;
//               this._dataCadastroCliente = Param_dataCadastroCliente;
//               this._tipoCliente = Param_tipoCliente;
//               this._statusCliente = Param_statusCliente;
//               this._fkSubGrupoCliente = Param_fkSubGrupoCliente;
//               this._dataUltimaCompraCliente = Param_dataUltimaCompraCliente;
//               this._numeroCasaCliente = Param_numeroCasaCliente;
//               this._faxCliente = Param_faxCliente;
//               this._dataNascimentoClienteA = Param_dataNascimentoClienteA;
//               this._dataNascimentoClienteB = Param_dataNascimentoClienteB;
//               this._dataNascimentoClienteC = Param_dataNascimentoClienteC;
//               this._emailPrincipalCliente = Param_emailPrincipalCliente;
//        }
//    }
//
//}
//
//
//
//
////Projeto substituído ------------------------
////using System;
////using System.Collections;
////using System.Collections.Generic;
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
////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Cliente 
////    /// </summary> 
////    public class ClienteFields 
////    {
////
////        private int _idCliente = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): Int 
////        /// Somente Leitura/Auto Incremental
////        /// </summary> 
////        public int idCliente
////        {
////            get { return _idCliente; }
////            set { _idCliente = value; }
////        }
////
////        private string _nomeCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Sim 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string nomeCliente
////        {
////            get { return _nomeCliente.Trim(); }
////            set { _nomeCliente = value.Trim(); }
////        }
////
////        private string _enderecoClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string enderecoClienteA
////        {
////            get { return _enderecoClienteA.Trim(); }
////            set { _enderecoClienteA = value.Trim(); }
////        }
////
////        private string _enderecoClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string enderecoClienteB
////        {
////            get { return _enderecoClienteB.Trim(); }
////            set { _enderecoClienteB = value.Trim(); }
////        }
////
////        private string _enderecoClienteC = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string enderecoClienteC
////        {
////            get { return _enderecoClienteC.Trim(); }
////            set { _enderecoClienteC = value.Trim(); }
////        }
////
////        private string _bairroClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string bairroClienteA
////        {
////            get { return _bairroClienteA.Trim(); }
////            set { _bairroClienteA = value.Trim(); }
////        }
////
////        private string _bairroClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string bairroClienteB
////        {
////            get { return _bairroClienteB.Trim(); }
////            set { _bairroClienteB = value.Trim(); }
////        }
////
////        private string _bairroClientec = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string bairroClientec
////        {
////            get { return _bairroClientec.Trim(); }
////            set { _bairroClientec = value.Trim(); }
////        }
////
////        private string _cidadeClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string cidadeClienteA
////        {
////            get { return _cidadeClienteA.Trim(); }
////            set { _cidadeClienteA = value.Trim(); }
////        }
////
////        private string _cidadeClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string cidadeClienteB
////        {
////            get { return _cidadeClienteB.Trim(); }
////            set { _cidadeClienteB = value.Trim(); }
////        }
////
////        private string _cidadeClienteC = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string cidadeClienteC
////        {
////            get { return _cidadeClienteC.Trim(); }
////            set { _cidadeClienteC = value.Trim(); }
////        }
////
////        private string _estadoClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 2 
////        /// </summary> 
////        public string estadoClienteA
////        {
////            get { return _estadoClienteA.Trim(); }
////            set { _estadoClienteA = value.Trim(); }
////        }
////
////        private string _estadoClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 2 
////        /// </summary> 
////        public string estadoClienteB
////        {
////            get { return _estadoClienteB.Trim(); }
////            set { _estadoClienteB = value.Trim(); }
////        }
////
////        private string _estadoClienteC = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 2 
////        /// </summary> 
////        public string estadoClienteC
////        {
////            get { return _estadoClienteC.Trim(); }
////            set { _estadoClienteC = value.Trim(); }
////        }
////
////        private string _cepClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 9 
////        /// </summary> 
////        public string cepClienteA
////        {
////            get { return _cepClienteA.Trim(); }
////            set { _cepClienteA = value.Trim(); }
////        }
////
////        private string _cepClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 9 
////        /// </summary> 
////        public string cepClienteB
////        {
////            get { return _cepClienteB.Trim(); }
////            set { _cepClienteB = value.Trim(); }
////        }
////
////        private string _cepClienteC = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 9 
////        /// </summary> 
////        public string cepClienteC
////        {
////            get { return _cepClienteC.Trim(); }
////            set { _cepClienteC = value.Trim(); }
////        }
////
////        private string _telefoneClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string telefoneClienteA
////        {
////            get { return _telefoneClienteA.Trim(); }
////            set { _telefoneClienteA = value.Trim(); }
////        }
////
////        private string _telefoneClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string telefoneClienteB
////        {
////            get { return _telefoneClienteB.Trim(); }
////            set { _telefoneClienteB = value.Trim(); }
////        }
////
////        private string _telefoneClienteC = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string telefoneClienteC
////        {
////            get { return _telefoneClienteC.Trim(); }
////            set { _telefoneClienteC = value.Trim(); }
////        }
////
////        private string _telefoneClienteD = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string telefoneClienteD
////        {
////            get { return _telefoneClienteD.Trim(); }
////            set { _telefoneClienteD = value.Trim(); }
////        }
////
////        private string _celularClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string celularClienteA
////        {
////            get { return _celularClienteA.Trim(); }
////            set { _celularClienteA = value.Trim(); }
////        }
////
////        private string _celularClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string celularClienteB
////        {
////            get { return _celularClienteB.Trim(); }
////            set { _celularClienteB = value.Trim(); }
////        }
////
////        private string _celularClienteC = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string celularClienteC
////        {
////            get { return _celularClienteC.Trim(); }
////            set { _celularClienteC = value.Trim(); }
////        }
////
////        private string _complementoCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 100 
////        /// </summary> 
////        public string complementoCliente
////        {
////            get { return _complementoCliente.Trim(); }
////            set { _complementoCliente = value.Trim(); }
////        }
////
////        private DateTime _dataNascimentoCliente = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dataNascimentoCliente
////        {
////            get { return _dataNascimentoCliente; }
////            set { _dataNascimentoCliente = value; }
////        }
////
////        private string _emailClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string emailClienteA
////        {
////            get { return _emailClienteA.Trim(); }
////            set { _emailClienteA = value.Trim(); }
////        }
////
////        private string _emailClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string emailClienteB
////        {
////            get { return _emailClienteB.Trim(); }
////            set { _emailClienteB = value.Trim(); }
////        }
////
////        private string _contatoClienteA = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string contatoClienteA
////        {
////            get { return _contatoClienteA.Trim(); }
////            set { _contatoClienteA = value.Trim(); }
////        }
////
////        private string _contatoClienteB = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string contatoClienteB
////        {
////            get { return _contatoClienteB.Trim(); }
////            set { _contatoClienteB = value.Trim(); }
////        }
////
////        private string _contatoClienteC = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 255 
////        /// </summary> 
////        public string contatoClienteC
////        {
////            get { return _contatoClienteC.Trim(); }
////            set { _contatoClienteC = value.Trim(); }
////        }
////
////        private string _cnpjCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string cnpjCliente
////        {
////            get { return _cnpjCliente.Trim(); }
////            set { _cnpjCliente = value.Trim(); }
////        }
////
////        private string _cpfCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string cpfCliente
////        {
////            get { return _cpfCliente.Trim(); }
////            set { _cpfCliente = value.Trim(); }
////        }
////
////        private string _rgCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string rgCliente
////        {
////            get { return _rgCliente.Trim(); }
////            set { _rgCliente = value.Trim(); }
////        }
////
////        private string _inscEstadualCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string inscEstadualCliente
////        {
////            get { return _inscEstadualCliente.Trim(); }
////            set { _inscEstadualCliente = value.Trim(); }
////        }
////
////        private string _observacoesCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 300 
////        /// </summary> 
////        public string observacoesCliente
////        {
////            get { return _observacoesCliente.Trim(); }
////            set { _observacoesCliente = value.Trim(); }
////        }
////
////        private DateTime _dataCadastroCliente = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dataCadastroCliente
////        {
////            get { return _dataCadastroCliente; }
////            set { _dataCadastroCliente = value; }
////        }
////
////        private string _tipoCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 20 
////        /// </summary> 
////        public string tipoCliente
////        {
////            get { return _tipoCliente.Trim(); }
////            set { _tipoCliente = value.Trim(); }
////        }
////
////        private string _statusCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Sim 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 2 
////        /// </summary> 
////        public string statusCliente
////        {
////            get { return _statusCliente.Trim(); }
////            set { _statusCliente = value.Trim(); }
////        }
////
////        private int _fkSubGrupoCliente = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): int 
////        /// Preenchimento obrigatório:  Sim 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public int fkSubGrupoCliente
////        {
////            get { return _fkSubGrupoCliente; }
////            set { _fkSubGrupoCliente = value; }
////        }
////
////        private DateTime _dataUltimaCompraCliente = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dataUltimaCompraCliente
////        {
////            get { return _dataUltimaCompraCliente; }
////            set { _dataUltimaCompraCliente = value; }
////        }
////
////        private string _numeroCasaCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): varchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 30 
////        /// </summary> 
////        public string numeroCasaCliente
////        {
////            get { return _numeroCasaCliente.Trim(); }
////            set { _numeroCasaCliente = value.Trim(); }
////        }
////
////        private string _faxCliente = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): varchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string faxCliente
////        {
////            get { return _faxCliente.Trim(); }
////            set { _faxCliente = value.Trim(); }
////        }
////
////        private DateTime _dataNascimentoClienteA = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dataNascimentoClienteA
////        {
////            get { return _dataNascimentoClienteA; }
////            set { _dataNascimentoClienteA = value; }
////        }
////
////        private DateTime _dataNascimentoClienteB = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dataNascimentoClienteB
////        {
////            get { return _dataNascimentoClienteB; }
////            set { _dataNascimentoClienteB = value; }
////        }
////
////        private DateTime _dataNascimentoClienteC = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dataNascimentoClienteC
////        {
////            get { return _dataNascimentoClienteC; }
////            set { _dataNascimentoClienteC = value; }
////        }
////
////
////
////        public ClienteFields() {}
////
////        public ClienteFields(
////                        string Param_nomeCliente, 
////                        string Param_enderecoClienteA, 
////                        string Param_enderecoClienteB, 
////                        string Param_enderecoClienteC, 
////                        string Param_bairroClienteA, 
////                        string Param_bairroClienteB, 
////                        string Param_bairroClientec, 
////                        string Param_cidadeClienteA, 
////                        string Param_cidadeClienteB, 
////                        string Param_cidadeClienteC, 
////                        string Param_estadoClienteA, 
////                        string Param_estadoClienteB, 
////                        string Param_estadoClienteC, 
////                        string Param_cepClienteA, 
////                        string Param_cepClienteB, 
////                        string Param_cepClienteC, 
////                        string Param_telefoneClienteA, 
////                        string Param_telefoneClienteB, 
////                        string Param_telefoneClienteC, 
////                        string Param_telefoneClienteD, 
////                        string Param_celularClienteA, 
////                        string Param_celularClienteB, 
////                        string Param_celularClienteC, 
////                        string Param_complementoCliente, 
////                        DateTime Param_dataNascimentoCliente, 
////                        string Param_emailClienteA, 
////                        string Param_emailClienteB, 
////                        string Param_contatoClienteA, 
////                        string Param_contatoClienteB, 
////                        string Param_contatoClienteC, 
////                        string Param_cnpjCliente, 
////                        string Param_cpfCliente, 
////                        string Param_rgCliente, 
////                        string Param_inscEstadualCliente, 
////                        string Param_observacoesCliente, 
////                        DateTime Param_dataCadastroCliente, 
////                        string Param_tipoCliente, 
////                        string Param_statusCliente, 
////                        int Param_fkSubGrupoCliente, 
////                        DateTime Param_dataUltimaCompraCliente, 
////                        string Param_numeroCasaCliente, 
////                        string Param_faxCliente, 
////                        DateTime Param_dataNascimentoClienteA, 
////                        DateTime Param_dataNascimentoClienteB, 
////                        DateTime Param_dataNascimentoClienteC)
////        {
////               this._nomeCliente = Param_nomeCliente;
////               this._enderecoClienteA = Param_enderecoClienteA;
////               this._enderecoClienteB = Param_enderecoClienteB;
////               this._enderecoClienteC = Param_enderecoClienteC;
////               this._bairroClienteA = Param_bairroClienteA;
////               this._bairroClienteB = Param_bairroClienteB;
////               this._bairroClientec = Param_bairroClientec;
////               this._cidadeClienteA = Param_cidadeClienteA;
////               this._cidadeClienteB = Param_cidadeClienteB;
////               this._cidadeClienteC = Param_cidadeClienteC;
////               this._estadoClienteA = Param_estadoClienteA;
////               this._estadoClienteB = Param_estadoClienteB;
////               this._estadoClienteC = Param_estadoClienteC;
////               this._cepClienteA = Param_cepClienteA;
////               this._cepClienteB = Param_cepClienteB;
////               this._cepClienteC = Param_cepClienteC;
////               this._telefoneClienteA = Param_telefoneClienteA;
////               this._telefoneClienteB = Param_telefoneClienteB;
////               this._telefoneClienteC = Param_telefoneClienteC;
////               this._telefoneClienteD = Param_telefoneClienteD;
////               this._celularClienteA = Param_celularClienteA;
////               this._celularClienteB = Param_celularClienteB;
////               this._celularClienteC = Param_celularClienteC;
////               this._complementoCliente = Param_complementoCliente;
////               this._dataNascimentoCliente = Param_dataNascimentoCliente;
////               this._emailClienteA = Param_emailClienteA;
////               this._emailClienteB = Param_emailClienteB;
////               this._contatoClienteA = Param_contatoClienteA;
////               this._contatoClienteB = Param_contatoClienteB;
////               this._contatoClienteC = Param_contatoClienteC;
////               this._cnpjCliente = Param_cnpjCliente;
////               this._cpfCliente = Param_cpfCliente;
////               this._rgCliente = Param_rgCliente;
////               this._inscEstadualCliente = Param_inscEstadualCliente;
////               this._observacoesCliente = Param_observacoesCliente;
////               this._dataCadastroCliente = Param_dataCadastroCliente;
////               this._tipoCliente = Param_tipoCliente;
////               this._statusCliente = Param_statusCliente;
////               this._fkSubGrupoCliente = Param_fkSubGrupoCliente;
////               this._dataUltimaCompraCliente = Param_dataUltimaCompraCliente;
////               this._numeroCasaCliente = Param_numeroCasaCliente;
////               this._faxCliente = Param_faxCliente;
////               this._dataNascimentoClienteA = Param_dataNascimentoClienteA;
////               this._dataNascimentoClienteB = Param_dataNascimentoClienteB;
////               this._dataNascimentoClienteC = Param_dataNascimentoClienteC;
////        }
////    }
////
////}
////
////
////
////
//////Projeto substituído ------------------------
//////using System;
//////using System.Collections;
//////using System.Collections.Generic;
//////using System.Text;
//////
//////namespace SIMLgen
//////{
//////
//////
//////    /// <summary> 
//////    /// Tabela: Cliente  
//////    /// Autor: DAL Creator .net  
//////    /// Data de criação: 22/05/2013 09:08:25 
//////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Cliente 
//////    /// </summary> 
//////    public class ClienteFields 
//////    {
//////
//////        private int _idCliente = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): Int 
//////        /// Somente Leitura/Auto Incremental
//////        /// </summary> 
//////        public int idCliente
//////        {
//////            get { return _idCliente; }
//////            set { _idCliente = value; }
//////        }
//////
//////        private string _nomeCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Sim 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string nomeCliente
//////        {
//////            get { return _nomeCliente.Trim(); }
//////            set { _nomeCliente = value.Trim(); }
//////        }
//////
//////        private string _enderecoClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string enderecoClienteA
//////        {
//////            get { return _enderecoClienteA.Trim(); }
//////            set { _enderecoClienteA = value.Trim(); }
//////        }
//////
//////        private string _enderecoClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string enderecoClienteB
//////        {
//////            get { return _enderecoClienteB.Trim(); }
//////            set { _enderecoClienteB = value.Trim(); }
//////        }
//////
//////        private string _enderecoClienteC = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string enderecoClienteC
//////        {
//////            get { return _enderecoClienteC.Trim(); }
//////            set { _enderecoClienteC = value.Trim(); }
//////        }
//////
//////        private string _bairroClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string bairroClienteA
//////        {
//////            get { return _bairroClienteA.Trim(); }
//////            set { _bairroClienteA = value.Trim(); }
//////        }
//////
//////        private string _bairroClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string bairroClienteB
//////        {
//////            get { return _bairroClienteB.Trim(); }
//////            set { _bairroClienteB = value.Trim(); }
//////        }
//////
//////        private string _bairroClientec = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string bairroClientec
//////        {
//////            get { return _bairroClientec.Trim(); }
//////            set { _bairroClientec = value.Trim(); }
//////        }
//////
//////        private string _cidadeClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string cidadeClienteA
//////        {
//////            get { return _cidadeClienteA.Trim(); }
//////            set { _cidadeClienteA = value.Trim(); }
//////        }
//////
//////        private string _cidadeClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string cidadeClienteB
//////        {
//////            get { return _cidadeClienteB.Trim(); }
//////            set { _cidadeClienteB = value.Trim(); }
//////        }
//////
//////        private string _cidadeClienteC = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string cidadeClienteC
//////        {
//////            get { return _cidadeClienteC.Trim(); }
//////            set { _cidadeClienteC = value.Trim(); }
//////        }
//////
//////        private string _estadoClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 2 
//////        /// </summary> 
//////        public string estadoClienteA
//////        {
//////            get { return _estadoClienteA.Trim(); }
//////            set { _estadoClienteA = value.Trim(); }
//////        }
//////
//////        private string _estadoClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 2 
//////        /// </summary> 
//////        public string estadoClienteB
//////        {
//////            get { return _estadoClienteB.Trim(); }
//////            set { _estadoClienteB = value.Trim(); }
//////        }
//////
//////        private string _estadoClienteC = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 2 
//////        /// </summary> 
//////        public string estadoClienteC
//////        {
//////            get { return _estadoClienteC.Trim(); }
//////            set { _estadoClienteC = value.Trim(); }
//////        }
//////
//////        private string _cepClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 9 
//////        /// </summary> 
//////        public string cepClienteA
//////        {
//////            get { return _cepClienteA.Trim(); }
//////            set { _cepClienteA = value.Trim(); }
//////        }
//////
//////        private string _cepClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 9 
//////        /// </summary> 
//////        public string cepClienteB
//////        {
//////            get { return _cepClienteB.Trim(); }
//////            set { _cepClienteB = value.Trim(); }
//////        }
//////
//////        private string _cepClienteC = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 9 
//////        /// </summary> 
//////        public string cepClienteC
//////        {
//////            get { return _cepClienteC.Trim(); }
//////            set { _cepClienteC = value.Trim(); }
//////        }
//////
//////        private string _telefoneClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string telefoneClienteA
//////        {
//////            get { return _telefoneClienteA.Trim(); }
//////            set { _telefoneClienteA = value.Trim(); }
//////        }
//////
//////        private string _telefoneClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string telefoneClienteB
//////        {
//////            get { return _telefoneClienteB.Trim(); }
//////            set { _telefoneClienteB = value.Trim(); }
//////        }
//////
//////        private string _telefoneClienteC = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string telefoneClienteC
//////        {
//////            get { return _telefoneClienteC.Trim(); }
//////            set { _telefoneClienteC = value.Trim(); }
//////        }
//////
//////        private string _telefoneClienteD = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string telefoneClienteD
//////        {
//////            get { return _telefoneClienteD.Trim(); }
//////            set { _telefoneClienteD = value.Trim(); }
//////        }
//////
//////        private string _celularClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string celularClienteA
//////        {
//////            get { return _celularClienteA.Trim(); }
//////            set { _celularClienteA = value.Trim(); }
//////        }
//////
//////        private string _celularClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string celularClienteB
//////        {
//////            get { return _celularClienteB.Trim(); }
//////            set { _celularClienteB = value.Trim(); }
//////        }
//////
//////        private string _celularClienteC = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string celularClienteC
//////        {
//////            get { return _celularClienteC.Trim(); }
//////            set { _celularClienteC = value.Trim(); }
//////        }
//////
//////        private string _complementoCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 100 
//////        /// </summary> 
//////        public string complementoCliente
//////        {
//////            get { return _complementoCliente.Trim(); }
//////            set { _complementoCliente = value.Trim(); }
//////        }
//////
//////        private DateTime _dataNascimentoCliente = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dataNascimentoCliente
//////        {
//////            get { return _dataNascimentoCliente; }
//////            set { _dataNascimentoCliente = value; }
//////        }
//////
//////        private string _emailClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string emailClienteA
//////        {
//////            get { return _emailClienteA.Trim(); }
//////            set { _emailClienteA = value.Trim(); }
//////        }
//////
//////        private string _emailClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string emailClienteB
//////        {
//////            get { return _emailClienteB.Trim(); }
//////            set { _emailClienteB = value.Trim(); }
//////        }
//////
//////        private string _contatoClienteA = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string contatoClienteA
//////        {
//////            get { return _contatoClienteA.Trim(); }
//////            set { _contatoClienteA = value.Trim(); }
//////        }
//////
//////        private string _contatoClienteB = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string contatoClienteB
//////        {
//////            get { return _contatoClienteB.Trim(); }
//////            set { _contatoClienteB = value.Trim(); }
//////        }
//////
//////        private string _contatoClienteC = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 255 
//////        /// </summary> 
//////        public string contatoClienteC
//////        {
//////            get { return _contatoClienteC.Trim(); }
//////            set { _contatoClienteC = value.Trim(); }
//////        }
//////
//////        private string _cnpjCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string cnpjCliente
//////        {
//////            get { return _cnpjCliente.Trim(); }
//////            set { _cnpjCliente = value.Trim(); }
//////        }
//////
//////        private string _cpfCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string cpfCliente
//////        {
//////            get { return _cpfCliente.Trim(); }
//////            set { _cpfCliente = value.Trim(); }
//////        }
//////
//////        private string _rgCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string rgCliente
//////        {
//////            get { return _rgCliente.Trim(); }
//////            set { _rgCliente = value.Trim(); }
//////        }
//////
//////        private string _inscEstadualCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string inscEstadualCliente
//////        {
//////            get { return _inscEstadualCliente.Trim(); }
//////            set { _inscEstadualCliente = value.Trim(); }
//////        }
//////
//////        private string _observacoesCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 300 
//////        /// </summary> 
//////        public string observacoesCliente
//////        {
//////            get { return _observacoesCliente.Trim(); }
//////            set { _observacoesCliente = value.Trim(); }
//////        }
//////
//////        private DateTime _dataCadastroCliente = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dataCadastroCliente
//////        {
//////            get { return _dataCadastroCliente; }
//////            set { _dataCadastroCliente = value; }
//////        }
//////
//////        private string _tipoCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 20 
//////        /// </summary> 
//////        public string tipoCliente
//////        {
//////            get { return _tipoCliente.Trim(); }
//////            set { _tipoCliente = value.Trim(); }
//////        }
//////
//////        private string _statusCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Sim 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 2 
//////        /// </summary> 
//////        public string statusCliente
//////        {
//////            get { return _statusCliente.Trim(); }
//////            set { _statusCliente = value.Trim(); }
//////        }
//////
//////        private int _fkSubGrupoCliente = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): int 
//////        /// Preenchimento obrigatório:  Sim 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public int fkSubGrupoCliente
//////        {
//////            get { return _fkSubGrupoCliente; }
//////            set { _fkSubGrupoCliente = value; }
//////        }
//////
//////        private DateTime _dataUltimaCompraCliente = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dataUltimaCompraCliente
//////        {
//////            get { return _dataUltimaCompraCliente; }
//////            set { _dataUltimaCompraCliente = value; }
//////        }
//////
//////        private string _numeroCasaCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): varchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 30 
//////        /// </summary> 
//////        public string numeroCasaCliente
//////        {
//////            get { return _numeroCasaCliente.Trim(); }
//////            set { _numeroCasaCliente = value.Trim(); }
//////        }
//////
//////        private string _faxCliente = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): varchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string faxCliente
//////        {
//////            get { return _faxCliente.Trim(); }
//////            set { _faxCliente = value.Trim(); }
//////        }
//////
//////        private DateTime _dataNascimentoClienteA = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dataNascimentoClienteA
//////        {
//////            get { return _dataNascimentoClienteA; }
//////            set { _dataNascimentoClienteA = value; }
//////        }
//////
//////        private DateTime _dataNascimentoClienteB = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dataNascimentoClienteB
//////        {
//////            get { return _dataNascimentoClienteB; }
//////            set { _dataNascimentoClienteB = value; }
//////        }
//////
//////        private DateTime _dataNascimentoClienteC = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dataNascimentoClienteC
//////        {
//////            get { return _dataNascimentoClienteC; }
//////            set { _dataNascimentoClienteC = value; }
//////        }
//////
//////
//////
//////        public ClienteFields() {}
//////
//////        public ClienteFields(
//////                        string Param_nomeCliente, 
//////                        string Param_enderecoClienteA, 
//////                        string Param_enderecoClienteB, 
//////                        string Param_enderecoClienteC, 
//////                        string Param_bairroClienteA, 
//////                        string Param_bairroClienteB, 
//////                        string Param_bairroClientec, 
//////                        string Param_cidadeClienteA, 
//////                        string Param_cidadeClienteB, 
//////                        string Param_cidadeClienteC, 
//////                        string Param_estadoClienteA, 
//////                        string Param_estadoClienteB, 
//////                        string Param_estadoClienteC, 
//////                        string Param_cepClienteA, 
//////                        string Param_cepClienteB, 
//////                        string Param_cepClienteC, 
//////                        string Param_telefoneClienteA, 
//////                        string Param_telefoneClienteB, 
//////                        string Param_telefoneClienteC, 
//////                        string Param_telefoneClienteD, 
//////                        string Param_celularClienteA, 
//////                        string Param_celularClienteB, 
//////                        string Param_celularClienteC, 
//////                        string Param_complementoCliente, 
//////                        DateTime Param_dataNascimentoCliente, 
//////                        string Param_emailClienteA, 
//////                        string Param_emailClienteB, 
//////                        string Param_contatoClienteA, 
//////                        string Param_contatoClienteB, 
//////                        string Param_contatoClienteC, 
//////                        string Param_cnpjCliente, 
//////                        string Param_cpfCliente, 
//////                        string Param_rgCliente, 
//////                        string Param_inscEstadualCliente, 
//////                        string Param_observacoesCliente, 
//////                        DateTime Param_dataCadastroCliente, 
//////                        string Param_tipoCliente, 
//////                        string Param_statusCliente, 
//////                        int Param_fkSubGrupoCliente, 
//////                        DateTime Param_dataUltimaCompraCliente, 
//////                        string Param_numeroCasaCliente, 
//////                        string Param_faxCliente, 
//////                        DateTime Param_dataNascimentoClienteA, 
//////                        DateTime Param_dataNascimentoClienteB, 
//////                        DateTime Param_dataNascimentoClienteC)
//////        {
//////               this._nomeCliente = Param_nomeCliente;
//////               this._enderecoClienteA = Param_enderecoClienteA;
//////               this._enderecoClienteB = Param_enderecoClienteB;
//////               this._enderecoClienteC = Param_enderecoClienteC;
//////               this._bairroClienteA = Param_bairroClienteA;
//////               this._bairroClienteB = Param_bairroClienteB;
//////               this._bairroClientec = Param_bairroClientec;
//////               this._cidadeClienteA = Param_cidadeClienteA;
//////               this._cidadeClienteB = Param_cidadeClienteB;
//////               this._cidadeClienteC = Param_cidadeClienteC;
//////               this._estadoClienteA = Param_estadoClienteA;
//////               this._estadoClienteB = Param_estadoClienteB;
//////               this._estadoClienteC = Param_estadoClienteC;
//////               this._cepClienteA = Param_cepClienteA;
//////               this._cepClienteB = Param_cepClienteB;
//////               this._cepClienteC = Param_cepClienteC;
//////               this._telefoneClienteA = Param_telefoneClienteA;
//////               this._telefoneClienteB = Param_telefoneClienteB;
//////               this._telefoneClienteC = Param_telefoneClienteC;
//////               this._telefoneClienteD = Param_telefoneClienteD;
//////               this._celularClienteA = Param_celularClienteA;
//////               this._celularClienteB = Param_celularClienteB;
//////               this._celularClienteC = Param_celularClienteC;
//////               this._complementoCliente = Param_complementoCliente;
//////               this._dataNascimentoCliente = Param_dataNascimentoCliente;
//////               this._emailClienteA = Param_emailClienteA;
//////               this._emailClienteB = Param_emailClienteB;
//////               this._contatoClienteA = Param_contatoClienteA;
//////               this._contatoClienteB = Param_contatoClienteB;
//////               this._contatoClienteC = Param_contatoClienteC;
//////               this._cnpjCliente = Param_cnpjCliente;
//////               this._cpfCliente = Param_cpfCliente;
//////               this._rgCliente = Param_rgCliente;
//////               this._inscEstadualCliente = Param_inscEstadualCliente;
//////               this._observacoesCliente = Param_observacoesCliente;
//////               this._dataCadastroCliente = Param_dataCadastroCliente;
//////               this._tipoCliente = Param_tipoCliente;
//////               this._statusCliente = Param_statusCliente;
//////               this._fkSubGrupoCliente = Param_fkSubGrupoCliente;
//////               this._dataUltimaCompraCliente = Param_dataUltimaCompraCliente;
//////               this._numeroCasaCliente = Param_numeroCasaCliente;
//////               this._faxCliente = Param_faxCliente;
//////               this._dataNascimentoClienteA = Param_dataNascimentoClienteA;
//////               this._dataNascimentoClienteB = Param_dataNascimentoClienteB;
//////               this._dataNascimentoClienteC = Param_dataNascimentoClienteC;
//////        }
//////    }
//////
//////}
//////
//////
//////
//////
////////Projeto substituído ------------------------
////////using System;
////////using System.Collections;
////////using System.Collections.Generic;
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
////////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Cliente 
////////    /// </summary> 
////////    public class ClienteFields 
////////    {
////////
////////        private int _idCliente = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): Int 
////////        /// Somente Leitura/Auto Incremental
////////        /// </summary> 
////////        public int idCliente
////////        {
////////            get { return _idCliente; }
////////            set { _idCliente = value; }
////////        }
////////
////////        private string _nomeCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Sim 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string nomeCliente
////////        {
////////            get { return _nomeCliente.Trim(); }
////////            set { _nomeCliente = value.Trim(); }
////////        }
////////
////////        private string _enderecoClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string enderecoClienteA
////////        {
////////            get { return _enderecoClienteA.Trim(); }
////////            set { _enderecoClienteA = value.Trim(); }
////////        }
////////
////////        private string _enderecoClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string enderecoClienteB
////////        {
////////            get { return _enderecoClienteB.Trim(); }
////////            set { _enderecoClienteB = value.Trim(); }
////////        }
////////
////////        private string _enderecoClienteC = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string enderecoClienteC
////////        {
////////            get { return _enderecoClienteC.Trim(); }
////////            set { _enderecoClienteC = value.Trim(); }
////////        }
////////
////////        private string _bairroClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string bairroClienteA
////////        {
////////            get { return _bairroClienteA.Trim(); }
////////            set { _bairroClienteA = value.Trim(); }
////////        }
////////
////////        private string _bairroClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string bairroClienteB
////////        {
////////            get { return _bairroClienteB.Trim(); }
////////            set { _bairroClienteB = value.Trim(); }
////////        }
////////
////////        private string _bairroClientec = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string bairroClientec
////////        {
////////            get { return _bairroClientec.Trim(); }
////////            set { _bairroClientec = value.Trim(); }
////////        }
////////
////////        private string _cidadeClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string cidadeClienteA
////////        {
////////            get { return _cidadeClienteA.Trim(); }
////////            set { _cidadeClienteA = value.Trim(); }
////////        }
////////
////////        private string _cidadeClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string cidadeClienteB
////////        {
////////            get { return _cidadeClienteB.Trim(); }
////////            set { _cidadeClienteB = value.Trim(); }
////////        }
////////
////////        private string _cidadeClienteC = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string cidadeClienteC
////////        {
////////            get { return _cidadeClienteC.Trim(); }
////////            set { _cidadeClienteC = value.Trim(); }
////////        }
////////
////////        private string _estadoClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 2 
////////        /// </summary> 
////////        public string estadoClienteA
////////        {
////////            get { return _estadoClienteA.Trim(); }
////////            set { _estadoClienteA = value.Trim(); }
////////        }
////////
////////        private string _estadoClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 2 
////////        /// </summary> 
////////        public string estadoClienteB
////////        {
////////            get { return _estadoClienteB.Trim(); }
////////            set { _estadoClienteB = value.Trim(); }
////////        }
////////
////////        private string _estadoClienteC = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 2 
////////        /// </summary> 
////////        public string estadoClienteC
////////        {
////////            get { return _estadoClienteC.Trim(); }
////////            set { _estadoClienteC = value.Trim(); }
////////        }
////////
////////        private string _cepClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 9 
////////        /// </summary> 
////////        public string cepClienteA
////////        {
////////            get { return _cepClienteA.Trim(); }
////////            set { _cepClienteA = value.Trim(); }
////////        }
////////
////////        private string _cepClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 9 
////////        /// </summary> 
////////        public string cepClienteB
////////        {
////////            get { return _cepClienteB.Trim(); }
////////            set { _cepClienteB = value.Trim(); }
////////        }
////////
////////        private string _cepClienteC = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 9 
////////        /// </summary> 
////////        public string cepClienteC
////////        {
////////            get { return _cepClienteC.Trim(); }
////////            set { _cepClienteC = value.Trim(); }
////////        }
////////
////////        private string _telefoneClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string telefoneClienteA
////////        {
////////            get { return _telefoneClienteA.Trim(); }
////////            set { _telefoneClienteA = value.Trim(); }
////////        }
////////
////////        private string _telefoneClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string telefoneClienteB
////////        {
////////            get { return _telefoneClienteB.Trim(); }
////////            set { _telefoneClienteB = value.Trim(); }
////////        }
////////
////////        private string _telefoneClienteC = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string telefoneClienteC
////////        {
////////            get { return _telefoneClienteC.Trim(); }
////////            set { _telefoneClienteC = value.Trim(); }
////////        }
////////
////////        private string _telefoneClienteD = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string telefoneClienteD
////////        {
////////            get { return _telefoneClienteD.Trim(); }
////////            set { _telefoneClienteD = value.Trim(); }
////////        }
////////
////////        private string _celularClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string celularClienteA
////////        {
////////            get { return _celularClienteA.Trim(); }
////////            set { _celularClienteA = value.Trim(); }
////////        }
////////
////////        private string _celularClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string celularClienteB
////////        {
////////            get { return _celularClienteB.Trim(); }
////////            set { _celularClienteB = value.Trim(); }
////////        }
////////
////////        private string _celularClienteC = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string celularClienteC
////////        {
////////            get { return _celularClienteC.Trim(); }
////////            set { _celularClienteC = value.Trim(); }
////////        }
////////
////////        private string _complementoCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 100 
////////        /// </summary> 
////////        public string complementoCliente
////////        {
////////            get { return _complementoCliente.Trim(); }
////////            set { _complementoCliente = value.Trim(); }
////////        }
////////
////////        private DateTime _dataNascimentoCliente = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dataNascimentoCliente
////////        {
////////            get { return _dataNascimentoCliente; }
////////            set { _dataNascimentoCliente = value; }
////////        }
////////
////////        private string _emailClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string emailClienteA
////////        {
////////            get { return _emailClienteA.Trim(); }
////////            set { _emailClienteA = value.Trim(); }
////////        }
////////
////////        private string _emailClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string emailClienteB
////////        {
////////            get { return _emailClienteB.Trim(); }
////////            set { _emailClienteB = value.Trim(); }
////////        }
////////
////////        private string _contatoClienteA = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string contatoClienteA
////////        {
////////            get { return _contatoClienteA.Trim(); }
////////            set { _contatoClienteA = value.Trim(); }
////////        }
////////
////////        private string _contatoClienteB = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string contatoClienteB
////////        {
////////            get { return _contatoClienteB.Trim(); }
////////            set { _contatoClienteB = value.Trim(); }
////////        }
////////
////////        private string _contatoClienteC = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 255 
////////        /// </summary> 
////////        public string contatoClienteC
////////        {
////////            get { return _contatoClienteC.Trim(); }
////////            set { _contatoClienteC = value.Trim(); }
////////        }
////////
////////        private string _cnpjCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string cnpjCliente
////////        {
////////            get { return _cnpjCliente.Trim(); }
////////            set { _cnpjCliente = value.Trim(); }
////////        }
////////
////////        private string _cpfCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string cpfCliente
////////        {
////////            get { return _cpfCliente.Trim(); }
////////            set { _cpfCliente = value.Trim(); }
////////        }
////////
////////        private string _rgCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string rgCliente
////////        {
////////            get { return _rgCliente.Trim(); }
////////            set { _rgCliente = value.Trim(); }
////////        }
////////
////////        private string _inscEstadualCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string inscEstadualCliente
////////        {
////////            get { return _inscEstadualCliente.Trim(); }
////////            set { _inscEstadualCliente = value.Trim(); }
////////        }
////////
////////        private string _observacoesCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 300 
////////        /// </summary> 
////////        public string observacoesCliente
////////        {
////////            get { return _observacoesCliente.Trim(); }
////////            set { _observacoesCliente = value.Trim(); }
////////        }
////////
////////        private DateTime _dataCadastroCliente = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dataCadastroCliente
////////        {
////////            get { return _dataCadastroCliente; }
////////            set { _dataCadastroCliente = value; }
////////        }
////////
////////        private string _tipoCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 20 
////////        /// </summary> 
////////        public string tipoCliente
////////        {
////////            get { return _tipoCliente.Trim(); }
////////            set { _tipoCliente = value.Trim(); }
////////        }
////////
////////        private string _statusCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Sim 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 2 
////////        /// </summary> 
////////        public string statusCliente
////////        {
////////            get { return _statusCliente.Trim(); }
////////            set { _statusCliente = value.Trim(); }
////////        }
////////
////////        private int _fkSubGrupoCliente = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): int 
////////        /// Preenchimento obrigatório:  Sim 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public int fkSubGrupoCliente
////////        {
////////            get { return _fkSubGrupoCliente; }
////////            set { _fkSubGrupoCliente = value; }
////////        }
////////
////////        private DateTime _dataUltimaCompraCliente = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dataUltimaCompraCliente
////////        {
////////            get { return _dataUltimaCompraCliente; }
////////            set { _dataUltimaCompraCliente = value; }
////////        }
////////
////////        private string _numeroCasaCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): varchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 30 
////////        /// </summary> 
////////        public string numeroCasaCliente
////////        {
////////            get { return _numeroCasaCliente.Trim(); }
////////            set { _numeroCasaCliente = value.Trim(); }
////////        }
////////
////////        private string _faxCliente = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): varchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string faxCliente
////////        {
////////            get { return _faxCliente.Trim(); }
////////            set { _faxCliente = value.Trim(); }
////////        }
////////
////////        private DateTime _dataNascimentoClienteA = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dataNascimentoClienteA
////////        {
////////            get { return _dataNascimentoClienteA; }
////////            set { _dataNascimentoClienteA = value; }
////////        }
////////
////////        private DateTime _dataNascimentoClienteB = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dataNascimentoClienteB
////////        {
////////            get { return _dataNascimentoClienteB; }
////////            set { _dataNascimentoClienteB = value; }
////////        }
////////
////////        private DateTime _dataNascimentoClienteC = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dataNascimentoClienteC
////////        {
////////            get { return _dataNascimentoClienteC; }
////////            set { _dataNascimentoClienteC = value; }
////////        }
////////
////////
////////
////////        public ClienteFields() {}
////////
////////        public ClienteFields(
////////                        string Param_nomeCliente, 
////////                        string Param_enderecoClienteA, 
////////                        string Param_enderecoClienteB, 
////////                        string Param_enderecoClienteC, 
////////                        string Param_bairroClienteA, 
////////                        string Param_bairroClienteB, 
////////                        string Param_bairroClientec, 
////////                        string Param_cidadeClienteA, 
////////                        string Param_cidadeClienteB, 
////////                        string Param_cidadeClienteC, 
////////                        string Param_estadoClienteA, 
////////                        string Param_estadoClienteB, 
////////                        string Param_estadoClienteC, 
////////                        string Param_cepClienteA, 
////////                        string Param_cepClienteB, 
////////                        string Param_cepClienteC, 
////////                        string Param_telefoneClienteA, 
////////                        string Param_telefoneClienteB, 
////////                        string Param_telefoneClienteC, 
////////                        string Param_telefoneClienteD, 
////////                        string Param_celularClienteA, 
////////                        string Param_celularClienteB, 
////////                        string Param_celularClienteC, 
////////                        string Param_complementoCliente, 
////////                        DateTime Param_dataNascimentoCliente, 
////////                        string Param_emailClienteA, 
////////                        string Param_emailClienteB, 
////////                        string Param_contatoClienteA, 
////////                        string Param_contatoClienteB, 
////////                        string Param_contatoClienteC, 
////////                        string Param_cnpjCliente, 
////////                        string Param_cpfCliente, 
////////                        string Param_rgCliente, 
////////                        string Param_inscEstadualCliente, 
////////                        string Param_observacoesCliente, 
////////                        DateTime Param_dataCadastroCliente, 
////////                        string Param_tipoCliente, 
////////                        string Param_statusCliente, 
////////                        int Param_fkSubGrupoCliente, 
////////                        DateTime Param_dataUltimaCompraCliente, 
////////                        string Param_numeroCasaCliente, 
////////                        string Param_faxCliente, 
////////                        DateTime Param_dataNascimentoClienteA, 
////////                        DateTime Param_dataNascimentoClienteB, 
////////                        DateTime Param_dataNascimentoClienteC)
////////        {
////////               this._nomeCliente = Param_nomeCliente;
////////               this._enderecoClienteA = Param_enderecoClienteA;
////////               this._enderecoClienteB = Param_enderecoClienteB;
////////               this._enderecoClienteC = Param_enderecoClienteC;
////////               this._bairroClienteA = Param_bairroClienteA;
////////               this._bairroClienteB = Param_bairroClienteB;
////////               this._bairroClientec = Param_bairroClientec;
////////               this._cidadeClienteA = Param_cidadeClienteA;
////////               this._cidadeClienteB = Param_cidadeClienteB;
////////               this._cidadeClienteC = Param_cidadeClienteC;
////////               this._estadoClienteA = Param_estadoClienteA;
////////               this._estadoClienteB = Param_estadoClienteB;
////////               this._estadoClienteC = Param_estadoClienteC;
////////               this._cepClienteA = Param_cepClienteA;
////////               this._cepClienteB = Param_cepClienteB;
////////               this._cepClienteC = Param_cepClienteC;
////////               this._telefoneClienteA = Param_telefoneClienteA;
////////               this._telefoneClienteB = Param_telefoneClienteB;
////////               this._telefoneClienteC = Param_telefoneClienteC;
////////               this._telefoneClienteD = Param_telefoneClienteD;
////////               this._celularClienteA = Param_celularClienteA;
////////               this._celularClienteB = Param_celularClienteB;
////////               this._celularClienteC = Param_celularClienteC;
////////               this._complementoCliente = Param_complementoCliente;
////////               this._dataNascimentoCliente = Param_dataNascimentoCliente;
////////               this._emailClienteA = Param_emailClienteA;
////////               this._emailClienteB = Param_emailClienteB;
////////               this._contatoClienteA = Param_contatoClienteA;
////////               this._contatoClienteB = Param_contatoClienteB;
////////               this._contatoClienteC = Param_contatoClienteC;
////////               this._cnpjCliente = Param_cnpjCliente;
////////               this._cpfCliente = Param_cpfCliente;
////////               this._rgCliente = Param_rgCliente;
////////               this._inscEstadualCliente = Param_inscEstadualCliente;
////////               this._observacoesCliente = Param_observacoesCliente;
////////               this._dataCadastroCliente = Param_dataCadastroCliente;
////////               this._tipoCliente = Param_tipoCliente;
////////               this._statusCliente = Param_statusCliente;
////////               this._fkSubGrupoCliente = Param_fkSubGrupoCliente;
////////               this._dataUltimaCompraCliente = Param_dataUltimaCompraCliente;
////////               this._numeroCasaCliente = Param_numeroCasaCliente;
////////               this._faxCliente = Param_faxCliente;
////////               this._dataNascimentoClienteA = Param_dataNascimentoClienteA;
////////               this._dataNascimentoClienteB = Param_dataNascimentoClienteB;
////////               this._dataNascimentoClienteC = Param_dataNascimentoClienteC;
////////        }
////////    }
////////
////////}
////////
////////
////////
////////
//////////Projeto substituído ------------------------
//////////using System;
//////////using System.Collections;
//////////using System.Collections.Generic;
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
//////////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Cliente 
//////////    /// </summary> 
//////////    public class ClienteFields 
//////////    {
//////////
//////////        private int _idCliente = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Sim 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int idCliente
//////////        {
//////////            get { return _idCliente; }
//////////            set { _idCliente = value; }
//////////        }
//////////
//////////        private string _nomeCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Sim 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string nomeCliente
//////////        {
//////////            get { return _nomeCliente.Trim(); }
//////////            set { _nomeCliente = value.Trim(); }
//////////        }
//////////
//////////        private string _enderecoClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string enderecoClienteA
//////////        {
//////////            get { return _enderecoClienteA.Trim(); }
//////////            set { _enderecoClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _enderecoClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string enderecoClienteB
//////////        {
//////////            get { return _enderecoClienteB.Trim(); }
//////////            set { _enderecoClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _enderecoClienteC = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string enderecoClienteC
//////////        {
//////////            get { return _enderecoClienteC.Trim(); }
//////////            set { _enderecoClienteC = value.Trim(); }
//////////        }
//////////
//////////        private string _bairroClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string bairroClienteA
//////////        {
//////////            get { return _bairroClienteA.Trim(); }
//////////            set { _bairroClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _bairroClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string bairroClienteB
//////////        {
//////////            get { return _bairroClienteB.Trim(); }
//////////            set { _bairroClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _bairroClientec = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string bairroClientec
//////////        {
//////////            get { return _bairroClientec.Trim(); }
//////////            set { _bairroClientec = value.Trim(); }
//////////        }
//////////
//////////        private string _cidadeClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string cidadeClienteA
//////////        {
//////////            get { return _cidadeClienteA.Trim(); }
//////////            set { _cidadeClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _cidadeClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string cidadeClienteB
//////////        {
//////////            get { return _cidadeClienteB.Trim(); }
//////////            set { _cidadeClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _cidadeClienteC = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string cidadeClienteC
//////////        {
//////////            get { return _cidadeClienteC.Trim(); }
//////////            set { _cidadeClienteC = value.Trim(); }
//////////        }
//////////
//////////        private string _estadoClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 2 
//////////        /// </summary> 
//////////        public string estadoClienteA
//////////        {
//////////            get { return _estadoClienteA.Trim(); }
//////////            set { _estadoClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _estadoClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 2 
//////////        /// </summary> 
//////////        public string estadoClienteB
//////////        {
//////////            get { return _estadoClienteB.Trim(); }
//////////            set { _estadoClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _estadoClienteC = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 2 
//////////        /// </summary> 
//////////        public string estadoClienteC
//////////        {
//////////            get { return _estadoClienteC.Trim(); }
//////////            set { _estadoClienteC = value.Trim(); }
//////////        }
//////////
//////////        private string _cepClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 9 
//////////        /// </summary> 
//////////        public string cepClienteA
//////////        {
//////////            get { return _cepClienteA.Trim(); }
//////////            set { _cepClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _cepClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 9 
//////////        /// </summary> 
//////////        public string cepClienteB
//////////        {
//////////            get { return _cepClienteB.Trim(); }
//////////            set { _cepClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _cepClienteC = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 9 
//////////        /// </summary> 
//////////        public string cepClienteC
//////////        {
//////////            get { return _cepClienteC.Trim(); }
//////////            set { _cepClienteC = value.Trim(); }
//////////        }
//////////
//////////        private string _telefoneClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string telefoneClienteA
//////////        {
//////////            get { return _telefoneClienteA.Trim(); }
//////////            set { _telefoneClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _telefoneClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string telefoneClienteB
//////////        {
//////////            get { return _telefoneClienteB.Trim(); }
//////////            set { _telefoneClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _telefoneClienteC = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string telefoneClienteC
//////////        {
//////////            get { return _telefoneClienteC.Trim(); }
//////////            set { _telefoneClienteC = value.Trim(); }
//////////        }
//////////
//////////        private string _telefoneClienteD = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string telefoneClienteD
//////////        {
//////////            get { return _telefoneClienteD.Trim(); }
//////////            set { _telefoneClienteD = value.Trim(); }
//////////        }
//////////
//////////        private string _celularClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string celularClienteA
//////////        {
//////////            get { return _celularClienteA.Trim(); }
//////////            set { _celularClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _celularClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string celularClienteB
//////////        {
//////////            get { return _celularClienteB.Trim(); }
//////////            set { _celularClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _celularClienteC = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string celularClienteC
//////////        {
//////////            get { return _celularClienteC.Trim(); }
//////////            set { _celularClienteC = value.Trim(); }
//////////        }
//////////
//////////        private string _complementoCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 100 
//////////        /// </summary> 
//////////        public string complementoCliente
//////////        {
//////////            get { return _complementoCliente.Trim(); }
//////////            set { _complementoCliente = value.Trim(); }
//////////        }
//////////
//////////        private DateTime _dataNascimentoCliente = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dataNascimentoCliente
//////////        {
//////////            get { return _dataNascimentoCliente; }
//////////            set { _dataNascimentoCliente = value; }
//////////        }
//////////
//////////        private string _emailClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string emailClienteA
//////////        {
//////////            get { return _emailClienteA.Trim(); }
//////////            set { _emailClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _emailClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string emailClienteB
//////////        {
//////////            get { return _emailClienteB.Trim(); }
//////////            set { _emailClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _contatoClienteA = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string contatoClienteA
//////////        {
//////////            get { return _contatoClienteA.Trim(); }
//////////            set { _contatoClienteA = value.Trim(); }
//////////        }
//////////
//////////        private string _contatoClienteB = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string contatoClienteB
//////////        {
//////////            get { return _contatoClienteB.Trim(); }
//////////            set { _contatoClienteB = value.Trim(); }
//////////        }
//////////
//////////        private string _contatoClienteC = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 255 
//////////        /// </summary> 
//////////        public string contatoClienteC
//////////        {
//////////            get { return _contatoClienteC.Trim(); }
//////////            set { _contatoClienteC = value.Trim(); }
//////////        }
//////////
//////////        private string _cnpjCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string cnpjCliente
//////////        {
//////////            get { return _cnpjCliente.Trim(); }
//////////            set { _cnpjCliente = value.Trim(); }
//////////        }
//////////
//////////        private string _cpfCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string cpfCliente
//////////        {
//////////            get { return _cpfCliente.Trim(); }
//////////            set { _cpfCliente = value.Trim(); }
//////////        }
//////////
//////////        private string _rgCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string rgCliente
//////////        {
//////////            get { return _rgCliente.Trim(); }
//////////            set { _rgCliente = value.Trim(); }
//////////        }
//////////
//////////        private string _inscEstadualCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string inscEstadualCliente
//////////        {
//////////            get { return _inscEstadualCliente.Trim(); }
//////////            set { _inscEstadualCliente = value.Trim(); }
//////////        }
//////////
//////////        private string _observacoesCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 300 
//////////        /// </summary> 
//////////        public string observacoesCliente
//////////        {
//////////            get { return _observacoesCliente.Trim(); }
//////////            set { _observacoesCliente = value.Trim(); }
//////////        }
//////////
//////////        private DateTime _dataCadastroCliente = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dataCadastroCliente
//////////        {
//////////            get { return _dataCadastroCliente; }
//////////            set { _dataCadastroCliente = value; }
//////////        }
//////////
//////////        private string _tipoCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 20 
//////////        /// </summary> 
//////////        public string tipoCliente
//////////        {
//////////            get { return _tipoCliente.Trim(); }
//////////            set { _tipoCliente = value.Trim(); }
//////////        }
//////////
//////////        private string _statusCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Sim 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 2 
//////////        /// </summary> 
//////////        public string statusCliente
//////////        {
//////////            get { return _statusCliente.Trim(); }
//////////            set { _statusCliente = value.Trim(); }
//////////        }
//////////
//////////        private int _fkSubGrupoCliente = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Sim 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int fkSubGrupoCliente
//////////        {
//////////            get { return _fkSubGrupoCliente; }
//////////            set { _fkSubGrupoCliente = value; }
//////////        }
//////////
//////////        private DateTime _dataUltimaCompraCliente = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dataUltimaCompraCliente
//////////        {
//////////            get { return _dataUltimaCompraCliente; }
//////////            set { _dataUltimaCompraCliente = value; }
//////////        }
//////////
//////////        private string _numeroCasaCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): varchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 30 
//////////        /// </summary> 
//////////        public string numeroCasaCliente
//////////        {
//////////            get { return _numeroCasaCliente.Trim(); }
//////////            set { _numeroCasaCliente = value.Trim(); }
//////////        }
//////////
//////////        private string _faxCliente = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): varchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string faxCliente
//////////        {
//////////            get { return _faxCliente.Trim(); }
//////////            set { _faxCliente = value.Trim(); }
//////////        }
//////////
//////////        private DateTime _dataNascimentoClienteA = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dataNascimentoClienteA
//////////        {
//////////            get { return _dataNascimentoClienteA; }
//////////            set { _dataNascimentoClienteA = value; }
//////////        }
//////////
//////////        private DateTime _dataNascimentoClienteB = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dataNascimentoClienteB
//////////        {
//////////            get { return _dataNascimentoClienteB; }
//////////            set { _dataNascimentoClienteB = value; }
//////////        }
//////////
//////////        private DateTime _dataNascimentoClienteC = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dataNascimentoClienteC
//////////        {
//////////            get { return _dataNascimentoClienteC; }
//////////            set { _dataNascimentoClienteC = value; }
//////////        }
//////////
//////////
//////////
//////////        public ClienteFields() {}
//////////
//////////        public ClienteFields(
//////////                        int Param_idCliente, 
//////////                        string Param_nomeCliente, 
//////////                        string Param_enderecoClienteA, 
//////////                        string Param_enderecoClienteB, 
//////////                        string Param_enderecoClienteC, 
//////////                        string Param_bairroClienteA, 
//////////                        string Param_bairroClienteB, 
//////////                        string Param_bairroClientec, 
//////////                        string Param_cidadeClienteA, 
//////////                        string Param_cidadeClienteB, 
//////////                        string Param_cidadeClienteC, 
//////////                        string Param_estadoClienteA, 
//////////                        string Param_estadoClienteB, 
//////////                        string Param_estadoClienteC, 
//////////                        string Param_cepClienteA, 
//////////                        string Param_cepClienteB, 
//////////                        string Param_cepClienteC, 
//////////                        string Param_telefoneClienteA, 
//////////                        string Param_telefoneClienteB, 
//////////                        string Param_telefoneClienteC, 
//////////                        string Param_telefoneClienteD, 
//////////                        string Param_celularClienteA, 
//////////                        string Param_celularClienteB, 
//////////                        string Param_celularClienteC, 
//////////                        string Param_complementoCliente, 
//////////                        DateTime Param_dataNascimentoCliente, 
//////////                        string Param_emailClienteA, 
//////////                        string Param_emailClienteB, 
//////////                        string Param_contatoClienteA, 
//////////                        string Param_contatoClienteB, 
//////////                        string Param_contatoClienteC, 
//////////                        string Param_cnpjCliente, 
//////////                        string Param_cpfCliente, 
//////////                        string Param_rgCliente, 
//////////                        string Param_inscEstadualCliente, 
//////////                        string Param_observacoesCliente, 
//////////                        DateTime Param_dataCadastroCliente, 
//////////                        string Param_tipoCliente, 
//////////                        string Param_statusCliente, 
//////////                        int Param_fkSubGrupoCliente, 
//////////                        DateTime Param_dataUltimaCompraCliente, 
//////////                        string Param_numeroCasaCliente, 
//////////                        string Param_faxCliente, 
//////////                        DateTime Param_dataNascimentoClienteA, 
//////////                        DateTime Param_dataNascimentoClienteB, 
//////////                        DateTime Param_dataNascimentoClienteC)
//////////        {
//////////               this._idCliente = Param_idCliente;
//////////               this._nomeCliente = Param_nomeCliente;
//////////               this._enderecoClienteA = Param_enderecoClienteA;
//////////               this._enderecoClienteB = Param_enderecoClienteB;
//////////               this._enderecoClienteC = Param_enderecoClienteC;
//////////               this._bairroClienteA = Param_bairroClienteA;
//////////               this._bairroClienteB = Param_bairroClienteB;
//////////               this._bairroClientec = Param_bairroClientec;
//////////               this._cidadeClienteA = Param_cidadeClienteA;
//////////               this._cidadeClienteB = Param_cidadeClienteB;
//////////               this._cidadeClienteC = Param_cidadeClienteC;
//////////               this._estadoClienteA = Param_estadoClienteA;
//////////               this._estadoClienteB = Param_estadoClienteB;
//////////               this._estadoClienteC = Param_estadoClienteC;
//////////               this._cepClienteA = Param_cepClienteA;
//////////               this._cepClienteB = Param_cepClienteB;
//////////               this._cepClienteC = Param_cepClienteC;
//////////               this._telefoneClienteA = Param_telefoneClienteA;
//////////               this._telefoneClienteB = Param_telefoneClienteB;
//////////               this._telefoneClienteC = Param_telefoneClienteC;
//////////               this._telefoneClienteD = Param_telefoneClienteD;
//////////               this._celularClienteA = Param_celularClienteA;
//////////               this._celularClienteB = Param_celularClienteB;
//////////               this._celularClienteC = Param_celularClienteC;
//////////               this._complementoCliente = Param_complementoCliente;
//////////               this._dataNascimentoCliente = Param_dataNascimentoCliente;
//////////               this._emailClienteA = Param_emailClienteA;
//////////               this._emailClienteB = Param_emailClienteB;
//////////               this._contatoClienteA = Param_contatoClienteA;
//////////               this._contatoClienteB = Param_contatoClienteB;
//////////               this._contatoClienteC = Param_contatoClienteC;
//////////               this._cnpjCliente = Param_cnpjCliente;
//////////               this._cpfCliente = Param_cpfCliente;
//////////               this._rgCliente = Param_rgCliente;
//////////               this._inscEstadualCliente = Param_inscEstadualCliente;
//////////               this._observacoesCliente = Param_observacoesCliente;
//////////               this._dataCadastroCliente = Param_dataCadastroCliente;
//////////               this._tipoCliente = Param_tipoCliente;
//////////               this._statusCliente = Param_statusCliente;
//////////               this._fkSubGrupoCliente = Param_fkSubGrupoCliente;
//////////               this._dataUltimaCompraCliente = Param_dataUltimaCompraCliente;
//////////               this._numeroCasaCliente = Param_numeroCasaCliente;
//////////               this._faxCliente = Param_faxCliente;
//////////               this._dataNascimentoClienteA = Param_dataNascimentoClienteA;
//////////               this._dataNascimentoClienteB = Param_dataNascimentoClienteB;
//////////               this._dataNascimentoClienteC = Param_dataNascimentoClienteC;
//////////        }
//////////    }
//////////
//////////}
