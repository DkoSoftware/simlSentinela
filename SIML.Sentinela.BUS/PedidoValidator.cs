using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SIML.Sentnela
{


    /// <summary> 
    /// Autor: DAL Creator .net  
    /// Data de criação: 24/05/2013 16:44:13 
    /// Descrição: Classe que valida o objeto "PedidoFields". 
    /// </summary> 
    public class PedidoValidator
    {


        #region Propriedade que armazena erros de execução
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage { get { return _ErrorMessage; } }
        #endregion


        public PedidoValidator() { }


        public bool isValid(PedidoFields fieldInfo)
        {
            try
            {


                //Field tipoPedido
                if (fieldInfo.tipoPedido != string.Empty)
                    if (fieldInfo.tipoPedido.Trim().Length > 50)
                        throw new Exception("O campo \"tipoPedido\" deve ter comprimento máximo de 50 caracter(es).");


                //Field situacaoPedido
                if (fieldInfo.situacaoPedido != string.Empty)
                    if (fieldInfo.situacaoPedido.Trim().Length > 50)
                        throw new Exception("O campo \"situacaoPedido\" deve ter comprimento máximo de 50 caracter(es).");


                //Field tipoEntregaPedido
                if (fieldInfo.tipoEntregaPedido != string.Empty)
                    if (fieldInfo.tipoEntregaPedido.Trim().Length > 50)
                        throw new Exception("O campo \"tipoEntregaPedido\" deve ter comprimento máximo de 50 caracter(es).");


                //Field fkCliente
                if (!(fieldInfo.fkCliente > 0))
                    throw new Exception("O campo \"fkCliente\" deve ser maior que zero.");


                //Field fkUsuario
                if (!(fieldInfo.fkUsuario > 0))
                    throw new Exception("O campo \"fkUsuario\" deve ser maior que zero.");


                //Field fkTipoPagamento
                //if ( !( fieldInfo.fkTipoPagamento > 0 ) )
                //   throw new Exception("O campo \"fkTipoPagamento\" deve ser maior que zero.");


                //Field fkFormaPagamento
                //if ( !( fieldInfo.fkFormaPagamento > 0 ) )
                //   throw new Exception("O campo \"fkFormaPagamento\" deve ser maior que zero.");


                //Field valorTotalPedido
                //if (!(fieldInfo.valorTotalPedido > 0))
                //    throw new Exception("O campo \"valorTotalPedido\" deve ser maior que zero.");


                //Field numeroPedido
                //if (!(fieldInfo.numeroPedido > 0))
                //    throw new Exception("O campo \"numeroPedido\" deve ser maior que zero.");


                //Field fkLoja
                if (!(fieldInfo.fkLoja > 0))
                    throw new Exception("O campo \"fkLoja\" deve ser maior que zero.");


                //Field numeroNF
                if (fieldInfo.numeroNF != string.Empty)
                    if (fieldInfo.numeroNF.Trim().Length > 200)
                        throw new Exception("O campo \"numeroNF\" deve ter comprimento máximo de 200 caracter(es).");


                //Field Observacao
                //if (  fieldInfo.Observacao != string.Empty ) 
                //   if ( fieldInfo.Observacao.Trim().Length > 0  )
                //      throw new Exception("O campo \"Observacao\" deve ter comprimento máximo de 0 caracter(es).");


                ////Field ValorDesconto
                //if ( !( fieldInfo.ValorDesconto > 0 ) )
                //   throw new Exception("O campo \"ValorDesconto\" deve ser maior que zero.");


                ////Field ValorFrete
                //if ( !( fieldInfo.ValorFrete > 0 ) )
                //   throw new Exception("O campo \"ValorFrete\" deve ser maior que zero.");

                return true;

            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return false;
            }

        }
    }

}






//Projeto substituído ------------------------
//using System;
//using System.Text;
//using System.Text.RegularExpressions;
//
//namespace SIMLgen
//{
//
//
//    /// <summary> 
//    /// Autor: DAL Creator .net  
//    /// Data de criação: 24/05/2013 16:35:44 
//    /// Descrição: Classe que valida o objeto "PedidoFields". 
//    /// </summary> 
//    public class PedidoValidator 
//    {
//
//
//        #region Propriedade que armazena erros de execução 
//        private string _ErrorMessage = string.Empty;
//        public string ErrorMessage { get { return _ErrorMessage; } }
//        #endregion
//
//
//        public PedidoValidator() {}
//
//
//        public bool isValid( PedidoFields fieldInfo )
//        {
//            try
//            {
//
//
//                //Field tipoPedido
//                if (  fieldInfo.tipoPedido != string.Empty ) 
//                   if ( fieldInfo.tipoPedido.Trim().Length > 50  )
//                      throw new Exception("O campo \"tipoPedido\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field situacaoPedido
//                if (  fieldInfo.situacaoPedido != string.Empty ) 
//                   if ( fieldInfo.situacaoPedido.Trim().Length > 50  )
//                      throw new Exception("O campo \"situacaoPedido\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field tipoEntregaPedido
//                if (  fieldInfo.tipoEntregaPedido != string.Empty ) 
//                   if ( fieldInfo.tipoEntregaPedido.Trim().Length > 50  )
//                      throw new Exception("O campo \"tipoEntregaPedido\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field fkCliente
//                if ( !( fieldInfo.fkCliente > 0 ) )
//                   throw new Exception("O campo \"fkCliente\" deve ser maior que zero.");
//
//
//                //Field fkUsuario
//                if ( !( fieldInfo.fkUsuario > 0 ) )
//                   throw new Exception("O campo \"fkUsuario\" deve ser maior que zero.");
//
//
//                //Field fkTipoPagamento
//                if ( !( fieldInfo.fkTipoPagamento > 0 ) )
//                   throw new Exception("O campo \"fkTipoPagamento\" deve ser maior que zero.");
//
//
//                //Field fkFormaPagamento
//                if ( !( fieldInfo.fkFormaPagamento > 0 ) )
//                   throw new Exception("O campo \"fkFormaPagamento\" deve ser maior que zero.");
//
//
//                //Field valorTotalPedido
//                if ( !( fieldInfo.valorTotalPedido > 0 ) )
//                   throw new Exception("O campo \"valorTotalPedido\" deve ser maior que zero.");
//
//
//                //Field numeroPedido
//                if ( !( fieldInfo.numeroPedido > 0 ) )
//                   throw new Exception("O campo \"numeroPedido\" deve ser maior que zero.");
//
//
//                //Field fkLoja
//                if ( !( fieldInfo.fkLoja > 0 ) )
//                   throw new Exception("O campo \"fkLoja\" deve ser maior que zero.");
//
//
//                //Field numeroNF
//                if (  fieldInfo.numeroNF != string.Empty ) 
//                   if ( fieldInfo.numeroNF.Trim().Length > 200  )
//                      throw new Exception("O campo \"numeroNF\" deve ter comprimento máximo de 200 caracter(es).");
//
//
//                //Field Observacao
//                if (  fieldInfo.Observacao != string.Empty ) 
//                   if ( fieldInfo.Observacao.Trim().Length > 0  )
//                      throw new Exception("O campo \"Observacao\" deve ter comprimento máximo de 0 caracter(es).");
//
//                return true;
//
//            }
//            catch (Exception e)
//            {
//                this._ErrorMessage = e.Message;
//                return false;
//            }
//
//        }
//    }
//
//}
//
//
//
//
//
//
////Projeto substituído ------------------------
////using System;
////using System.Text;
////using System.Text.RegularExpressions;
////
////namespace SIMLgen
////{
////
////
////    /// <summary> 
////    /// Autor: DAL Creator .net  
////    /// Data de criação: 24/05/2013 15:25:16 
////    /// Descrição: Classe que valida o objeto "PedidoFields". 
////    /// </summary> 
////    public class PedidoValidator 
////    {
////
////
////        #region Propriedade que armazena erros de execução 
////        private string _ErrorMessage = string.Empty;
////        public string ErrorMessage { get { return _ErrorMessage; } }
////        #endregion
////
////
////        public PedidoValidator() {}
////
////
////        public bool isValid( PedidoFields fieldInfo )
////        {
////            try
////            {
////
////
////                //Field tipoPedido
////                if (  fieldInfo.tipoPedido != string.Empty ) 
////                   if ( fieldInfo.tipoPedido.Trim().Length > 50  )
////                      throw new Exception("O campo \"tipoPedido\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field situacaoPedido
////                if (  fieldInfo.situacaoPedido != string.Empty ) 
////                   if ( fieldInfo.situacaoPedido.Trim().Length > 50  )
////                      throw new Exception("O campo \"situacaoPedido\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field tipoEntregaPedido
////                if (  fieldInfo.tipoEntregaPedido != string.Empty ) 
////                   if ( fieldInfo.tipoEntregaPedido.Trim().Length > 50  )
////                      throw new Exception("O campo \"tipoEntregaPedido\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field fkCliente
////                if ( !( fieldInfo.fkCliente > 0 ) )
////                   throw new Exception("O campo \"fkCliente\" deve ser maior que zero.");
////
////
////                //Field fkUsuario
////                if ( !( fieldInfo.fkUsuario > 0 ) )
////                   throw new Exception("O campo \"fkUsuario\" deve ser maior que zero.");
////
////
////                //Field fkTipoPagamento
////                if ( !( fieldInfo.fkTipoPagamento > 0 ) )
////                   throw new Exception("O campo \"fkTipoPagamento\" deve ser maior que zero.");
////
////
////                //Field fkFormaPagamento
////                if ( !( fieldInfo.fkFormaPagamento > 0 ) )
////                   throw new Exception("O campo \"fkFormaPagamento\" deve ser maior que zero.");
////
////
////                //Field valorTotalPedido
////                if ( !( fieldInfo.valorTotalPedido > 0 ) )
////                   throw new Exception("O campo \"valorTotalPedido\" deve ser maior que zero.");
////
////
////                //Field numeroPedido
////                if ( !( fieldInfo.numeroPedido > 0 ) )
////                   throw new Exception("O campo \"numeroPedido\" deve ser maior que zero.");
////
////
////                //Field fkLoja
////                if ( !( fieldInfo.fkLoja > 0 ) )
////                   throw new Exception("O campo \"fkLoja\" deve ser maior que zero.");
////
////
////                //Field numeroNF
////                if (  fieldInfo.numeroNF != string.Empty ) 
////                   if ( fieldInfo.numeroNF.Trim().Length > 200  )
////                      throw new Exception("O campo \"numeroNF\" deve ter comprimento máximo de 200 caracter(es).");
////
////
////                //Field Observacao
////                if (  fieldInfo.Observacao != string.Empty ) 
////                   if ( fieldInfo.Observacao.Trim().Length > 0  )
////                      throw new Exception("O campo \"Observacao\" deve ter comprimento máximo de 0 caracter(es).");
////
////                return true;
////
////            }
////            catch (Exception e)
////            {
////                this._ErrorMessage = e.Message;
////                return false;
////            }
////
////        }
////    }
////
////}
////
////
////
////
////
////
//////Projeto substituído ------------------------
//////using System;
//////using System.Text;
//////using System.Text.RegularExpressions;
//////
//////namespace SIMLgen
//////{
//////
//////
//////    /// <summary> 
//////    /// Autor: DAL Creator .net  
//////    /// Data de criação: 24/05/2013 15:18:26 
//////    /// Descrição: Classe que valida o objeto "PedidoFields". 
//////    /// </summary> 
//////    public class PedidoValidator 
//////    {
//////
//////
//////        #region Propriedade que armazena erros de execução 
//////        private string _ErrorMessage = string.Empty;
//////        public string ErrorMessage { get { return _ErrorMessage; } }
//////        #endregion
//////
//////
//////        public PedidoValidator() {}
//////
//////
//////        public bool isValid( PedidoFields fieldInfo )
//////        {
//////            try
//////            {
//////
//////
//////                //Field tipoPedido
//////                if (  fieldInfo.tipoPedido != string.Empty ) 
//////                   if ( fieldInfo.tipoPedido.Trim().Length > 50  )
//////                      throw new Exception("O campo \"tipoPedido\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field situacaoPedido
//////                if (  fieldInfo.situacaoPedido != string.Empty ) 
//////                   if ( fieldInfo.situacaoPedido.Trim().Length > 50  )
//////                      throw new Exception("O campo \"situacaoPedido\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field tipoEntregaPedido
//////                if (  fieldInfo.tipoEntregaPedido != string.Empty ) 
//////                   if ( fieldInfo.tipoEntregaPedido.Trim().Length > 50  )
//////                      throw new Exception("O campo \"tipoEntregaPedido\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field fkCliente
//////                if ( !( fieldInfo.fkCliente > 0 ) )
//////                   throw new Exception("O campo \"fkCliente\" deve ser maior que zero.");
//////
//////
//////                //Field fkUsuario
//////                if ( !( fieldInfo.fkUsuario > 0 ) )
//////                   throw new Exception("O campo \"fkUsuario\" deve ser maior que zero.");
//////
//////
//////                //Field fkTipoPagamento
//////                if ( !( fieldInfo.fkTipoPagamento > 0 ) )
//////                   throw new Exception("O campo \"fkTipoPagamento\" deve ser maior que zero.");
//////
//////
//////                //Field fkFormaPagamento
//////                if ( !( fieldInfo.fkFormaPagamento > 0 ) )
//////                   throw new Exception("O campo \"fkFormaPagamento\" deve ser maior que zero.");
//////
//////
//////                //Field valorTotalPedido
//////                if ( !( fieldInfo.valorTotalPedido > 0 ) )
//////                   throw new Exception("O campo \"valorTotalPedido\" deve ser maior que zero.");
//////
//////
//////                //Field numeroPedido
//////                if ( !( fieldInfo.numeroPedido > 0 ) )
//////                   throw new Exception("O campo \"numeroPedido\" deve ser maior que zero.");
//////
//////
//////                //Field fkLoja
//////                if ( !( fieldInfo.fkLoja > 0 ) )
//////                   throw new Exception("O campo \"fkLoja\" deve ser maior que zero.");
//////
//////
//////                //Field numeroNF
//////                if (  fieldInfo.numeroNF != string.Empty ) 
//////                   if ( fieldInfo.numeroNF.Trim().Length > 200  )
//////                      throw new Exception("O campo \"numeroNF\" deve ter comprimento máximo de 200 caracter(es).");
//////
//////                return true;
//////
//////            }
//////            catch (Exception e)
//////            {
//////                this._ErrorMessage = e.Message;
//////                return false;
//////            }
//////
//////        }
//////    }
//////
//////}
//////
//////
//////
//////
//////
//////
////////Projeto substituído ------------------------
////////using System;
////////using System.Text;
////////using System.Text.RegularExpressions;
////////
////////namespace SIMLgen
////////{
////////
////////
////////    /// <summary> 
////////    /// Autor: DAL Creator .net  
////////    /// Data de criação: 28/04/2013 19:05:15 
////////    /// Descrição: Classe que valida o objeto "PedidoFields". 
////////    /// </summary> 
////////    public class PedidoValidator 
////////    {
////////
////////
////////        #region Propriedade que armazena erros de execução 
////////        private string _ErrorMessage = string.Empty;
////////        public string ErrorMessage { get { return _ErrorMessage; } }
////////        #endregion
////////
////////
////////        public PedidoValidator() {}
////////
////////
////////        public bool isValid( PedidoFields fieldInfo )
////////        {
////////            try
////////            {
////////
////////
////////                //Field tipoPedido
////////                if (  fieldInfo.tipoPedido != string.Empty ) 
////////                   if ( fieldInfo.tipoPedido.Trim().Length > 50  )
////////                      throw new Exception("O campo \"tipoPedido\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field situacaoPedido
////////                if (  fieldInfo.situacaoPedido != string.Empty ) 
////////                   if ( fieldInfo.situacaoPedido.Trim().Length > 50  )
////////                      throw new Exception("O campo \"situacaoPedido\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field tipoEntregaPedido
////////                if (  fieldInfo.tipoEntregaPedido != string.Empty ) 
////////                   if ( fieldInfo.tipoEntregaPedido.Trim().Length > 50  )
////////                      throw new Exception("O campo \"tipoEntregaPedido\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field fkCliente
////////                if ( !( fieldInfo.fkCliente > 0 ) )
////////                   throw new Exception("O campo \"fkCliente\" deve ser maior que zero.");
////////
////////
////////                //Field fkUsuario
////////                if ( !( fieldInfo.fkUsuario > 0 ) )
////////                   throw new Exception("O campo \"fkUsuario\" deve ser maior que zero.");
////////
////////
////////                //Field fkTipoPagamento
////////                if ( !( fieldInfo.fkTipoPagamento > 0 ) )
////////                   throw new Exception("O campo \"fkTipoPagamento\" deve ser maior que zero.");
////////
////////
////////                //Field fkFormaPagamento
////////                if ( !( fieldInfo.fkFormaPagamento > 0 ) )
////////                   throw new Exception("O campo \"fkFormaPagamento\" deve ser maior que zero.");
////////
////////
////////                //Field valorTotalPedido
////////                if ( !( fieldInfo.valorTotalPedido > 0 ) )
////////                   throw new Exception("O campo \"valorTotalPedido\" deve ser maior que zero.");
////////
////////
////////                //Field numeroPedido
////////                if ( !( fieldInfo.numeroPedido > 0 ) )
////////                   throw new Exception("O campo \"numeroPedido\" deve ser maior que zero.");
////////
////////
////////                //Field fkLoja
////////                if ( !( fieldInfo.fkLoja > 0 ) )
////////                   throw new Exception("O campo \"fkLoja\" deve ser maior que zero.");
////////
////////
////////                //Field numeroNF
////////                if (  fieldInfo.numeroNF != string.Empty ) 
////////                   if ( fieldInfo.numeroNF.Trim().Length > 200  )
////////                      throw new Exception("O campo \"numeroNF\" deve ter comprimento máximo de 200 caracter(es).");
////////
////////                return true;
////////
////////            }
////////            catch (Exception e)
////////            {
////////                this._ErrorMessage = e.Message;
////////                return false;
////////            }
////////
////////        }
////////    }
////////
////////}
////////
