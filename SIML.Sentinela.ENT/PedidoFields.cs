using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIML.Sentnela
{


    /// <summary> 
    /// Tabela: Pedido  
    /// Autor: DAL Creator .net  
    /// Data de criação: 24/05/2013 17:57:16 
    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Pedido 
    /// </summary> 
    public class PedidoFields 
    {

        private int _idPedido = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): Int 
        /// Somente Leitura/Auto Incremental
        /// </summary> 
        public int idPedido
        {
            get { return _idPedido; }
            set { _idPedido = value; }
        }

        private DateTime _dtPedido = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dtPedido
        {
            get { return _dtPedido; }
            set { _dtPedido = value; }
        }

        private DateTime _dtSaidaPedido = DateTime.MinValue;


        /// <summary>  
        /// Tipo de dados (DataBase): datetime 
        /// Preenchimento obrigatório:  Não 
        /// </summary> 
        public DateTime dtSaidaPedido
        {
            get { return _dtSaidaPedido; }
            set { _dtSaidaPedido = value; }
        }

        private string _tipoPedido = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string tipoPedido
        {
            get { return _tipoPedido.Trim(); }
            set { _tipoPedido = value.Trim(); }
        }

        private string _situacaoPedido = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string situacaoPedido
        {
            get { return _situacaoPedido.Trim(); }
            set { _situacaoPedido = value.Trim(); }
        }

        private string _tipoEntregaPedido = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 50 
        /// </summary> 
        public string tipoEntregaPedido
        {
            get { return _tipoEntregaPedido.Trim(); }
            set { _tipoEntregaPedido = value.Trim(); }
        }

        private int _fkCliente = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int fkCliente
        {
            get { return _fkCliente; }
            set { _fkCliente = value; }
        }

        private int _fkUsuario = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int fkUsuario
        {
            get { return _fkUsuario; }
            set { _fkUsuario = value; }
        }

        private int _fkTipoPagamento = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int fkTipoPagamento
        {
            get { return _fkTipoPagamento; }
            set { _fkTipoPagamento = value; }
        }

        private int _fkFormaPagamento = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int fkFormaPagamento
        {
            get { return _fkFormaPagamento; }
            set { _fkFormaPagamento = value; }
        }

        private decimal _valorTotalPedido = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): decimal 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public decimal valorTotalPedido
        {
            get { return _valorTotalPedido; }
            set { _valorTotalPedido = value; }
        }

        private int _numeroPedido = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int numeroPedido
        {
            get { return _numeroPedido; }
            set { _numeroPedido = value; }
        }

        private int _fkLoja = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int fkLoja
        {
            get { return _fkLoja; }
            set { _fkLoja = value; }
        }

        private string _numeroNF = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 200 
        /// </summary> 
        public string numeroNF
        {
            get { return _numeroNF.Trim(); }
            set { _numeroNF = value.Trim(); }
        }

        private string _Observacao = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Não 
        /// Estilo: Normal  
        /// Tamanho Máximo: 2000 
        /// </summary> 
        public string Observacao
        {
            get { return _Observacao.Trim(); }
            set { _Observacao = value.Trim(); }
        }

        private decimal _ValorDesconto = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): decimal 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public decimal ValorDesconto
        {
            get { return _ValorDesconto; }
            set { _ValorDesconto = value; }
        }

        private decimal _ValorFrete = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): decimal 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public decimal ValorFrete
        {
            get { return _ValorFrete; }
            set { _ValorFrete = value; }
        }



        public PedidoFields() {}

        public PedidoFields(
                        DateTime Param_dtPedido, 
                        DateTime Param_dtSaidaPedido, 
                        string Param_tipoPedido, 
                        string Param_situacaoPedido, 
                        string Param_tipoEntregaPedido, 
                        int Param_fkCliente, 
                        int Param_fkUsuario, 
                        int Param_fkTipoPagamento, 
                        int Param_fkFormaPagamento, 
                        decimal Param_valorTotalPedido, 
                        int Param_numeroPedido, 
                        int Param_fkLoja, 
                        string Param_numeroNF, 
                        string Param_Observacao, 
                        decimal Param_ValorDesconto, 
                        decimal Param_ValorFrete)
        {
               this._dtPedido = Param_dtPedido;
               this._dtSaidaPedido = Param_dtSaidaPedido;
               this._tipoPedido = Param_tipoPedido;
               this._situacaoPedido = Param_situacaoPedido;
               this._tipoEntregaPedido = Param_tipoEntregaPedido;
               this._fkCliente = Param_fkCliente;
               this._fkUsuario = Param_fkUsuario;
               this._fkTipoPagamento = Param_fkTipoPagamento;
               this._fkFormaPagamento = Param_fkFormaPagamento;
               this._valorTotalPedido = Param_valorTotalPedido;
               this._numeroPedido = Param_numeroPedido;
               this._fkLoja = Param_fkLoja;
               this._numeroNF = Param_numeroNF;
               this._Observacao = Param_Observacao;
               this._ValorDesconto = Param_ValorDesconto;
               this._ValorFrete = Param_ValorFrete;
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
//    /// Tabela: Pedido  
//    /// Autor: DAL Creator .net  
//    /// Data de criação: 24/05/2013 16:44:13 
//    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Pedido 
//    /// </summary> 
//    public class PedidoFields 
//    {
//
//        private int _idPedido = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): Int 
//        /// Somente Leitura/Auto Incremental
//        /// </summary> 
//        public int idPedido
//        {
//            get { return _idPedido; }
//            set { _idPedido = value; }
//        }
//
//        private DateTime _dtPedido = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dtPedido
//        {
//            get { return _dtPedido; }
//            set { _dtPedido = value; }
//        }
//
//        private DateTime _dtSaidaPedido = DateTime.MinValue;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): datetime 
//        /// Preenchimento obrigatório:  Não 
//        /// </summary> 
//        public DateTime dtSaidaPedido
//        {
//            get { return _dtSaidaPedido; }
//            set { _dtSaidaPedido = value; }
//        }
//
//        private string _tipoPedido = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string tipoPedido
//        {
//            get { return _tipoPedido.Trim(); }
//            set { _tipoPedido = value.Trim(); }
//        }
//
//        private string _situacaoPedido = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string situacaoPedido
//        {
//            get { return _situacaoPedido.Trim(); }
//            set { _situacaoPedido = value.Trim(); }
//        }
//
//        private string _tipoEntregaPedido = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 50 
//        /// </summary> 
//        public string tipoEntregaPedido
//        {
//            get { return _tipoEntregaPedido.Trim(); }
//            set { _tipoEntregaPedido = value.Trim(); }
//        }
//
//        private int _fkCliente = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int fkCliente
//        {
//            get { return _fkCliente; }
//            set { _fkCliente = value; }
//        }
//
//        private int _fkUsuario = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int fkUsuario
//        {
//            get { return _fkUsuario; }
//            set { _fkUsuario = value; }
//        }
//
//        private int _fkTipoPagamento = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int fkTipoPagamento
//        {
//            get { return _fkTipoPagamento; }
//            set { _fkTipoPagamento = value; }
//        }
//
//        private int _fkFormaPagamento = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int fkFormaPagamento
//        {
//            get { return _fkFormaPagamento; }
//            set { _fkFormaPagamento = value; }
//        }
//
//        private decimal _valorTotalPedido = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): decimal 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public decimal valorTotalPedido
//        {
//            get { return _valorTotalPedido; }
//            set { _valorTotalPedido = value; }
//        }
//
//        private int _numeroPedido = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int numeroPedido
//        {
//            get { return _numeroPedido; }
//            set { _numeroPedido = value; }
//        }
//
//        private int _fkLoja = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int fkLoja
//        {
//            get { return _fkLoja; }
//            set { _fkLoja = value; }
//        }
//
//        private string _numeroNF = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 200 
//        /// </summary> 
//        public string numeroNF
//        {
//            get { return _numeroNF.Trim(); }
//            set { _numeroNF = value.Trim(); }
//        }
//
//        private string _Observacao = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Não 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 0 
//        /// </summary> 
//        public string Observacao
//        {
//            get { return _Observacao.Trim(); }
//            set { _Observacao = value.Trim(); }
//        }
//
//        private decimal _ValorDesconto = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): decimal 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public decimal ValorDesconto
//        {
//            get { return _ValorDesconto; }
//            set { _ValorDesconto = value; }
//        }
//
//        private decimal _ValorFrete = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): decimal 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public decimal ValorFrete
//        {
//            get { return _ValorFrete; }
//            set { _ValorFrete = value; }
//        }
//
//
//
//        public PedidoFields() {}
//
//        public PedidoFields(
//                        DateTime Param_dtPedido, 
//                        DateTime Param_dtSaidaPedido, 
//                        string Param_tipoPedido, 
//                        string Param_situacaoPedido, 
//                        string Param_tipoEntregaPedido, 
//                        int Param_fkCliente, 
//                        int Param_fkUsuario, 
//                        int Param_fkTipoPagamento, 
//                        int Param_fkFormaPagamento, 
//                        decimal Param_valorTotalPedido, 
//                        int Param_numeroPedido, 
//                        int Param_fkLoja, 
//                        string Param_numeroNF, 
//                        string Param_Observacao, 
//                        decimal Param_ValorDesconto, 
//                        decimal Param_ValorFrete)
//        {
//               this._dtPedido = Param_dtPedido;
//               this._dtSaidaPedido = Param_dtSaidaPedido;
//               this._tipoPedido = Param_tipoPedido;
//               this._situacaoPedido = Param_situacaoPedido;
//               this._tipoEntregaPedido = Param_tipoEntregaPedido;
//               this._fkCliente = Param_fkCliente;
//               this._fkUsuario = Param_fkUsuario;
//               this._fkTipoPagamento = Param_fkTipoPagamento;
//               this._fkFormaPagamento = Param_fkFormaPagamento;
//               this._valorTotalPedido = Param_valorTotalPedido;
//               this._numeroPedido = Param_numeroPedido;
//               this._fkLoja = Param_fkLoja;
//               this._numeroNF = Param_numeroNF;
//               this._Observacao = Param_Observacao;
//               this._ValorDesconto = Param_ValorDesconto;
//               this._ValorFrete = Param_ValorFrete;
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
////    /// Tabela: Pedido  
////    /// Autor: DAL Creator .net  
////    /// Data de criação: 24/05/2013 16:35:44 
////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Pedido 
////    /// </summary> 
////    public class PedidoFields 
////    {
////
////        private int _idPedido = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): Int 
////        /// Somente Leitura/Auto Incremental
////        /// </summary> 
////        public int idPedido
////        {
////            get { return _idPedido; }
////            set { _idPedido = value; }
////        }
////
////        private DateTime _dtPedido = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dtPedido
////        {
////            get { return _dtPedido; }
////            set { _dtPedido = value; }
////        }
////
////        private DateTime _dtSaidaPedido = DateTime.MinValue;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): datetime 
////        /// Preenchimento obrigatório:  Não 
////        /// </summary> 
////        public DateTime dtSaidaPedido
////        {
////            get { return _dtSaidaPedido; }
////            set { _dtSaidaPedido = value; }
////        }
////
////        private string _tipoPedido = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string tipoPedido
////        {
////            get { return _tipoPedido.Trim(); }
////            set { _tipoPedido = value.Trim(); }
////        }
////
////        private string _situacaoPedido = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string situacaoPedido
////        {
////            get { return _situacaoPedido.Trim(); }
////            set { _situacaoPedido = value.Trim(); }
////        }
////
////        private string _tipoEntregaPedido = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 50 
////        /// </summary> 
////        public string tipoEntregaPedido
////        {
////            get { return _tipoEntregaPedido.Trim(); }
////            set { _tipoEntregaPedido = value.Trim(); }
////        }
////
////        private int _fkCliente = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): int 
////        /// Preenchimento obrigatório:  Não 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public int fkCliente
////        {
////            get { return _fkCliente; }
////            set { _fkCliente = value; }
////        }
////
////        private int _fkUsuario = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): int 
////        /// Preenchimento obrigatório:  Não 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public int fkUsuario
////        {
////            get { return _fkUsuario; }
////            set { _fkUsuario = value; }
////        }
////
////        private int _fkTipoPagamento = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): int 
////        /// Preenchimento obrigatório:  Não 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public int fkTipoPagamento
////        {
////            get { return _fkTipoPagamento; }
////            set { _fkTipoPagamento = value; }
////        }
////
////        private int _fkFormaPagamento = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): int 
////        /// Preenchimento obrigatório:  Não 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public int fkFormaPagamento
////        {
////            get { return _fkFormaPagamento; }
////            set { _fkFormaPagamento = value; }
////        }
////
////        private decimal _valorTotalPedido = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): decimal 
////        /// Preenchimento obrigatório:  Não 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public decimal valorTotalPedido
////        {
////            get { return _valorTotalPedido; }
////            set { _valorTotalPedido = value; }
////        }
////
////        private int _numeroPedido = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): int 
////        /// Preenchimento obrigatório:  Não 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public int numeroPedido
////        {
////            get { return _numeroPedido; }
////            set { _numeroPedido = value; }
////        }
////
////        private int _fkLoja = 0;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): int 
////        /// Preenchimento obrigatório:  Não 
////        /// Permitido:  Maior que zero 
////        /// </summary> 
////        public int fkLoja
////        {
////            get { return _fkLoja; }
////            set { _fkLoja = value; }
////        }
////
////        private string _numeroNF = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 200 
////        /// </summary> 
////        public string numeroNF
////        {
////            get { return _numeroNF.Trim(); }
////            set { _numeroNF = value.Trim(); }
////        }
////
////        private string _Observacao = string.Empty;
////
////
////        /// <summary>  
////        /// Tipo de dados (DataBase): nvarchar 
////        /// Preenchimento obrigatório:  Não 
////        /// Estilo: Normal  
////        /// Tamanho Máximo: 0 
////        /// </summary> 
////        public string Observacao
////        {
////            get { return _Observacao.Trim(); }
////            set { _Observacao = value.Trim(); }
////        }
////
////
////
////        public PedidoFields() {}
////
////        public PedidoFields(
////                        DateTime Param_dtPedido, 
////                        DateTime Param_dtSaidaPedido, 
////                        string Param_tipoPedido, 
////                        string Param_situacaoPedido, 
////                        string Param_tipoEntregaPedido, 
////                        int Param_fkCliente, 
////                        int Param_fkUsuario, 
////                        int Param_fkTipoPagamento, 
////                        int Param_fkFormaPagamento, 
////                        decimal Param_valorTotalPedido, 
////                        int Param_numeroPedido, 
////                        int Param_fkLoja, 
////                        string Param_numeroNF, 
////                        string Param_Observacao)
////        {
////               this._dtPedido = Param_dtPedido;
////               this._dtSaidaPedido = Param_dtSaidaPedido;
////               this._tipoPedido = Param_tipoPedido;
////               this._situacaoPedido = Param_situacaoPedido;
////               this._tipoEntregaPedido = Param_tipoEntregaPedido;
////               this._fkCliente = Param_fkCliente;
////               this._fkUsuario = Param_fkUsuario;
////               this._fkTipoPagamento = Param_fkTipoPagamento;
////               this._fkFormaPagamento = Param_fkFormaPagamento;
////               this._valorTotalPedido = Param_valorTotalPedido;
////               this._numeroPedido = Param_numeroPedido;
////               this._fkLoja = Param_fkLoja;
////               this._numeroNF = Param_numeroNF;
////               this._Observacao = Param_Observacao;
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
//////    /// Tabela: Pedido  
//////    /// Autor: DAL Creator .net  
//////    /// Data de criação: 24/05/2013 15:25:16 
//////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Pedido 
//////    /// </summary> 
//////    public class PedidoFields 
//////    {
//////
//////        private int _idPedido = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): Int 
//////        /// Somente Leitura/Auto Incremental
//////        /// </summary> 
//////        public int idPedido
//////        {
//////            get { return _idPedido; }
//////            set { _idPedido = value; }
//////        }
//////
//////        private DateTime _dtPedido = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dtPedido
//////        {
//////            get { return _dtPedido; }
//////            set { _dtPedido = value; }
//////        }
//////
//////        private DateTime _dtSaidaPedido = DateTime.MinValue;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): datetime 
//////        /// Preenchimento obrigatório:  Não 
//////        /// </summary> 
//////        public DateTime dtSaidaPedido
//////        {
//////            get { return _dtSaidaPedido; }
//////            set { _dtSaidaPedido = value; }
//////        }
//////
//////        private string _tipoPedido = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string tipoPedido
//////        {
//////            get { return _tipoPedido.Trim(); }
//////            set { _tipoPedido = value.Trim(); }
//////        }
//////
//////        private string _situacaoPedido = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string situacaoPedido
//////        {
//////            get { return _situacaoPedido.Trim(); }
//////            set { _situacaoPedido = value.Trim(); }
//////        }
//////
//////        private string _tipoEntregaPedido = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 50 
//////        /// </summary> 
//////        public string tipoEntregaPedido
//////        {
//////            get { return _tipoEntregaPedido.Trim(); }
//////            set { _tipoEntregaPedido = value.Trim(); }
//////        }
//////
//////        private int _fkCliente = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): int 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public int fkCliente
//////        {
//////            get { return _fkCliente; }
//////            set { _fkCliente = value; }
//////        }
//////
//////        private int _fkUsuario = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): int 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public int fkUsuario
//////        {
//////            get { return _fkUsuario; }
//////            set { _fkUsuario = value; }
//////        }
//////
//////        private int _fkTipoPagamento = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): int 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public int fkTipoPagamento
//////        {
//////            get { return _fkTipoPagamento; }
//////            set { _fkTipoPagamento = value; }
//////        }
//////
//////        private int _fkFormaPagamento = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): int 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public int fkFormaPagamento
//////        {
//////            get { return _fkFormaPagamento; }
//////            set { _fkFormaPagamento = value; }
//////        }
//////
//////        private decimal _valorTotalPedido = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): decimal 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public decimal valorTotalPedido
//////        {
//////            get { return _valorTotalPedido; }
//////            set { _valorTotalPedido = value; }
//////        }
//////
//////        private int _numeroPedido = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): int 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public int numeroPedido
//////        {
//////            get { return _numeroPedido; }
//////            set { _numeroPedido = value; }
//////        }
//////
//////        private int _fkLoja = 0;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): int 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Permitido:  Maior que zero 
//////        /// </summary> 
//////        public int fkLoja
//////        {
//////            get { return _fkLoja; }
//////            set { _fkLoja = value; }
//////        }
//////
//////        private string _numeroNF = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 200 
//////        /// </summary> 
//////        public string numeroNF
//////        {
//////            get { return _numeroNF.Trim(); }
//////            set { _numeroNF = value.Trim(); }
//////        }
//////
//////        private string _Observacao = string.Empty;
//////
//////
//////        /// <summary>  
//////        /// Tipo de dados (DataBase): nvarchar 
//////        /// Preenchimento obrigatório:  Não 
//////        /// Estilo: Normal  
//////        /// Tamanho Máximo: 0 
//////        /// </summary> 
//////        public string Observacao
//////        {
//////            get { return _Observacao.Trim(); }
//////            set { _Observacao = value.Trim(); }
//////        }
//////
//////
//////
//////        public PedidoFields() {}
//////
//////        public PedidoFields(
//////                        DateTime Param_dtPedido, 
//////                        DateTime Param_dtSaidaPedido, 
//////                        string Param_tipoPedido, 
//////                        string Param_situacaoPedido, 
//////                        string Param_tipoEntregaPedido, 
//////                        int Param_fkCliente, 
//////                        int Param_fkUsuario, 
//////                        int Param_fkTipoPagamento, 
//////                        int Param_fkFormaPagamento, 
//////                        decimal Param_valorTotalPedido, 
//////                        int Param_numeroPedido, 
//////                        int Param_fkLoja, 
//////                        string Param_numeroNF, 
//////                        string Param_Observacao)
//////        {
//////               this._dtPedido = Param_dtPedido;
//////               this._dtSaidaPedido = Param_dtSaidaPedido;
//////               this._tipoPedido = Param_tipoPedido;
//////               this._situacaoPedido = Param_situacaoPedido;
//////               this._tipoEntregaPedido = Param_tipoEntregaPedido;
//////               this._fkCliente = Param_fkCliente;
//////               this._fkUsuario = Param_fkUsuario;
//////               this._fkTipoPagamento = Param_fkTipoPagamento;
//////               this._fkFormaPagamento = Param_fkFormaPagamento;
//////               this._valorTotalPedido = Param_valorTotalPedido;
//////               this._numeroPedido = Param_numeroPedido;
//////               this._fkLoja = Param_fkLoja;
//////               this._numeroNF = Param_numeroNF;
//////               this._Observacao = Param_Observacao;
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
////////    /// Tabela: Pedido  
////////    /// Autor: DAL Creator .net  
////////    /// Data de criação: 24/05/2013 15:18:26 
////////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Pedido 
////////    /// </summary> 
////////    public class PedidoFields 
////////    {
////////
////////        private int _idPedido = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): Int 
////////        /// Somente Leitura/Auto Incremental
////////        /// </summary> 
////////        public int idPedido
////////        {
////////            get { return _idPedido; }
////////            set { _idPedido = value; }
////////        }
////////
////////        private DateTime _dtPedido = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dtPedido
////////        {
////////            get { return _dtPedido; }
////////            set { _dtPedido = value; }
////////        }
////////
////////        private DateTime _dtSaidaPedido = DateTime.MinValue;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): datetime 
////////        /// Preenchimento obrigatório:  Não 
////////        /// </summary> 
////////        public DateTime dtSaidaPedido
////////        {
////////            get { return _dtSaidaPedido; }
////////            set { _dtSaidaPedido = value; }
////////        }
////////
////////        private string _tipoPedido = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string tipoPedido
////////        {
////////            get { return _tipoPedido.Trim(); }
////////            set { _tipoPedido = value.Trim(); }
////////        }
////////
////////        private string _situacaoPedido = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string situacaoPedido
////////        {
////////            get { return _situacaoPedido.Trim(); }
////////            set { _situacaoPedido = value.Trim(); }
////////        }
////////
////////        private string _tipoEntregaPedido = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 50 
////////        /// </summary> 
////////        public string tipoEntregaPedido
////////        {
////////            get { return _tipoEntregaPedido.Trim(); }
////////            set { _tipoEntregaPedido = value.Trim(); }
////////        }
////////
////////        private int _fkCliente = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): int 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public int fkCliente
////////        {
////////            get { return _fkCliente; }
////////            set { _fkCliente = value; }
////////        }
////////
////////        private int _fkUsuario = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): int 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public int fkUsuario
////////        {
////////            get { return _fkUsuario; }
////////            set { _fkUsuario = value; }
////////        }
////////
////////        private int _fkTipoPagamento = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): int 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public int fkTipoPagamento
////////        {
////////            get { return _fkTipoPagamento; }
////////            set { _fkTipoPagamento = value; }
////////        }
////////
////////        private int _fkFormaPagamento = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): int 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public int fkFormaPagamento
////////        {
////////            get { return _fkFormaPagamento; }
////////            set { _fkFormaPagamento = value; }
////////        }
////////
////////        private decimal _valorTotalPedido = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): decimal 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public decimal valorTotalPedido
////////        {
////////            get { return _valorTotalPedido; }
////////            set { _valorTotalPedido = value; }
////////        }
////////
////////        private int _numeroPedido = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): int 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public int numeroPedido
////////        {
////////            get { return _numeroPedido; }
////////            set { _numeroPedido = value; }
////////        }
////////
////////        private int _fkLoja = 0;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): int 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Permitido:  Maior que zero 
////////        /// </summary> 
////////        public int fkLoja
////////        {
////////            get { return _fkLoja; }
////////            set { _fkLoja = value; }
////////        }
////////
////////        private string _numeroNF = string.Empty;
////////
////////
////////        /// <summary>  
////////        /// Tipo de dados (DataBase): nvarchar 
////////        /// Preenchimento obrigatório:  Não 
////////        /// Estilo: Normal  
////////        /// Tamanho Máximo: 200 
////////        /// </summary> 
////////        public string numeroNF
////////        {
////////            get { return _numeroNF.Trim(); }
////////            set { _numeroNF = value.Trim(); }
////////        }
////////
////////
////////
////////        public PedidoFields() {}
////////
////////        public PedidoFields(
////////                        DateTime Param_dtPedido, 
////////                        DateTime Param_dtSaidaPedido, 
////////                        string Param_tipoPedido, 
////////                        string Param_situacaoPedido, 
////////                        string Param_tipoEntregaPedido, 
////////                        int Param_fkCliente, 
////////                        int Param_fkUsuario, 
////////                        int Param_fkTipoPagamento, 
////////                        int Param_fkFormaPagamento, 
////////                        decimal Param_valorTotalPedido, 
////////                        int Param_numeroPedido, 
////////                        int Param_fkLoja, 
////////                        string Param_numeroNF)
////////        {
////////               this._dtPedido = Param_dtPedido;
////////               this._dtSaidaPedido = Param_dtSaidaPedido;
////////               this._tipoPedido = Param_tipoPedido;
////////               this._situacaoPedido = Param_situacaoPedido;
////////               this._tipoEntregaPedido = Param_tipoEntregaPedido;
////////               this._fkCliente = Param_fkCliente;
////////               this._fkUsuario = Param_fkUsuario;
////////               this._fkTipoPagamento = Param_fkTipoPagamento;
////////               this._fkFormaPagamento = Param_fkFormaPagamento;
////////               this._valorTotalPedido = Param_valorTotalPedido;
////////               this._numeroPedido = Param_numeroPedido;
////////               this._fkLoja = Param_fkLoja;
////////               this._numeroNF = Param_numeroNF;
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
//////////    /// Tabela: Pedido  
//////////    /// Autor: DAL Creator .net  
//////////    /// Data de criação: 28/04/2013 19:05:15 
//////////    /// Descrição: Classe que retorna todos os campos/propriedades da tabela Pedido 
//////////    /// </summary> 
//////////    public class PedidoFields 
//////////    {
//////////
//////////        private int _idPedido = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): Int 
//////////        /// Somente Leitura/Auto Incremental
//////////        /// </summary> 
//////////        public int idPedido
//////////        {
//////////            get { return _idPedido; }
//////////            set { _idPedido = value; }
//////////        }
//////////
//////////        private DateTime _dtPedido = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dtPedido
//////////        {
//////////            get { return _dtPedido; }
//////////            set { _dtPedido = value; }
//////////        }
//////////
//////////        private DateTime _dtSaidaPedido = DateTime.MinValue;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): datetime 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// </summary> 
//////////        public DateTime dtSaidaPedido
//////////        {
//////////            get { return _dtSaidaPedido; }
//////////            set { _dtSaidaPedido = value; }
//////////        }
//////////
//////////        private string _tipoPedido = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string tipoPedido
//////////        {
//////////            get { return _tipoPedido.Trim(); }
//////////            set { _tipoPedido = value.Trim(); }
//////////        }
//////////
//////////        private string _situacaoPedido = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string situacaoPedido
//////////        {
//////////            get { return _situacaoPedido.Trim(); }
//////////            set { _situacaoPedido = value.Trim(); }
//////////        }
//////////
//////////        private string _tipoEntregaPedido = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 50 
//////////        /// </summary> 
//////////        public string tipoEntregaPedido
//////////        {
//////////            get { return _tipoEntregaPedido.Trim(); }
//////////            set { _tipoEntregaPedido = value.Trim(); }
//////////        }
//////////
//////////        private int _fkCliente = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int fkCliente
//////////        {
//////////            get { return _fkCliente; }
//////////            set { _fkCliente = value; }
//////////        }
//////////
//////////        private int _fkUsuario = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int fkUsuario
//////////        {
//////////            get { return _fkUsuario; }
//////////            set { _fkUsuario = value; }
//////////        }
//////////
//////////        private int _fkTipoPagamento = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int fkTipoPagamento
//////////        {
//////////            get { return _fkTipoPagamento; }
//////////            set { _fkTipoPagamento = value; }
//////////        }
//////////
//////////        private int _fkFormaPagamento = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int fkFormaPagamento
//////////        {
//////////            get { return _fkFormaPagamento; }
//////////            set { _fkFormaPagamento = value; }
//////////        }
//////////
//////////        private decimal _valorTotalPedido = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): decimal 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public decimal valorTotalPedido
//////////        {
//////////            get { return _valorTotalPedido; }
//////////            set { _valorTotalPedido = value; }
//////////        }
//////////
//////////        private int _numeroPedido = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int numeroPedido
//////////        {
//////////            get { return _numeroPedido; }
//////////            set { _numeroPedido = value; }
//////////        }
//////////
//////////        private int _fkLoja = 0;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): int 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Permitido:  Maior que zero 
//////////        /// </summary> 
//////////        public int fkLoja
//////////        {
//////////            get { return _fkLoja; }
//////////            set { _fkLoja = value; }
//////////        }
//////////
//////////        private string _numeroNF = string.Empty;
//////////
//////////
//////////        /// <summary>  
//////////        /// Tipo de dados (DataBase): nvarchar 
//////////        /// Preenchimento obrigatório:  Não 
//////////        /// Estilo: Normal  
//////////        /// Tamanho Máximo: 200 
//////////        /// </summary> 
//////////        public string numeroNF
//////////        {
//////////            get { return _numeroNF.Trim(); }
//////////            set { _numeroNF = value.Trim(); }
//////////        }
//////////
//////////
//////////
//////////        public PedidoFields() {}
//////////
//////////        public PedidoFields(
//////////                        DateTime Param_dtPedido, 
//////////                        DateTime Param_dtSaidaPedido, 
//////////                        string Param_tipoPedido, 
//////////                        string Param_situacaoPedido, 
//////////                        string Param_tipoEntregaPedido, 
//////////                        int Param_fkCliente, 
//////////                        int Param_fkUsuario, 
//////////                        int Param_fkTipoPagamento, 
//////////                        int Param_fkFormaPagamento, 
//////////                        decimal Param_valorTotalPedido, 
//////////                        int Param_numeroPedido, 
//////////                        int Param_fkLoja, 
//////////                        string Param_numeroNF)
//////////        {
//////////               this._dtPedido = Param_dtPedido;
//////////               this._dtSaidaPedido = Param_dtSaidaPedido;
//////////               this._tipoPedido = Param_tipoPedido;
//////////               this._situacaoPedido = Param_situacaoPedido;
//////////               this._tipoEntregaPedido = Param_tipoEntregaPedido;
//////////               this._fkCliente = Param_fkCliente;
//////////               this._fkUsuario = Param_fkUsuario;
//////////               this._fkTipoPagamento = Param_fkTipoPagamento;
//////////               this._fkFormaPagamento = Param_fkFormaPagamento;
//////////               this._valorTotalPedido = Param_valorTotalPedido;
//////////               this._numeroPedido = Param_numeroPedido;
//////////               this._fkLoja = Param_fkLoja;
//////////               this._numeroNF = Param_numeroNF;
//////////        }
//////////    }
//////////
//////////}
