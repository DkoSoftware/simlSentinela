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
    /// Autor: DAL Creator .net  
    /// Data de criação: 23/06/2013 15:53:57 
    /// Descrição: Classe que valida o objeto "ClienteFields". 
    /// </summary> 
    public class ClienteValidator 
    {
        #region Propriedade que armazena erros de execução 
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage { get { return _ErrorMessage; } }
        #endregion

        public ClienteValidator() {}

        public bool isValid( ClienteFields fieldInfo )
        {
            try
            {


                //Field nomeCliente
                if (  fieldInfo.nomeCliente != string.Empty ) 
                   if ( fieldInfo.nomeCliente.Trim().Length > 255  )
                      throw new Exception("O campo \"nomeCliente\" deve ter comprimento máximo de 255 caracter(es).");
                if ( ( fieldInfo.nomeCliente == string.Empty ) || ( fieldInfo.nomeCliente.Trim().Length < 1 ) )
                   throw new Exception("O campo \"nomeCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");


                //Field enderecoClienteA
                if (  fieldInfo.enderecoClienteA != string.Empty ) 
                   if ( fieldInfo.enderecoClienteA.Trim().Length > 255  )
                      throw new Exception("O campo \"enderecoClienteA\" deve ter comprimento máximo de 255 caracter(es).");


                //Field enderecoClienteB
                if (  fieldInfo.enderecoClienteB != string.Empty ) 
                   if ( fieldInfo.enderecoClienteB.Trim().Length > 255  )
                      throw new Exception("O campo \"enderecoClienteB\" deve ter comprimento máximo de 255 caracter(es).");


                //Field enderecoClienteC
                if (  fieldInfo.enderecoClienteC != string.Empty ) 
                   if ( fieldInfo.enderecoClienteC.Trim().Length > 255  )
                      throw new Exception("O campo \"enderecoClienteC\" deve ter comprimento máximo de 255 caracter(es).");


                //Field bairroClienteA
                if (  fieldInfo.bairroClienteA != string.Empty ) 
                   if ( fieldInfo.bairroClienteA.Trim().Length > 255  )
                      throw new Exception("O campo \"bairroClienteA\" deve ter comprimento máximo de 255 caracter(es).");


                //Field bairroClienteB
                if (  fieldInfo.bairroClienteB != string.Empty ) 
                   if ( fieldInfo.bairroClienteB.Trim().Length > 255  )
                      throw new Exception("O campo \"bairroClienteB\" deve ter comprimento máximo de 255 caracter(es).");


                //Field bairroClientec
                if (  fieldInfo.bairroClientec != string.Empty ) 
                   if ( fieldInfo.bairroClientec.Trim().Length > 255  )
                      throw new Exception("O campo \"bairroClientec\" deve ter comprimento máximo de 255 caracter(es).");


                //Field cidadeClienteA
                if (  fieldInfo.cidadeClienteA != string.Empty ) 
                   if ( fieldInfo.cidadeClienteA.Trim().Length > 255  )
                      throw new Exception("O campo \"cidadeClienteA\" deve ter comprimento máximo de 255 caracter(es).");


                //Field cidadeClienteB
                if (  fieldInfo.cidadeClienteB != string.Empty ) 
                   if ( fieldInfo.cidadeClienteB.Trim().Length > 255  )
                      throw new Exception("O campo \"cidadeClienteB\" deve ter comprimento máximo de 255 caracter(es).");


                //Field cidadeClienteC
                if (  fieldInfo.cidadeClienteC != string.Empty ) 
                   if ( fieldInfo.cidadeClienteC.Trim().Length > 255  )
                      throw new Exception("O campo \"cidadeClienteC\" deve ter comprimento máximo de 255 caracter(es).");


                //Field estadoClienteA
                if (  fieldInfo.estadoClienteA != string.Empty ) 
                   if ( fieldInfo.estadoClienteA.Trim().Length > 2  )
                      throw new Exception("O campo \"estadoClienteA\" deve ter comprimento máximo de 2 caracter(es).");


                //Field estadoClienteB
                if (  fieldInfo.estadoClienteB != string.Empty ) 
                   if ( fieldInfo.estadoClienteB.Trim().Length > 2  )
                      throw new Exception("O campo \"estadoClienteB\" deve ter comprimento máximo de 2 caracter(es).");


                //Field estadoClienteC
                if (  fieldInfo.estadoClienteC != string.Empty ) 
                   if ( fieldInfo.estadoClienteC.Trim().Length > 2  )
                      throw new Exception("O campo \"estadoClienteC\" deve ter comprimento máximo de 2 caracter(es).");


                //Field cepClienteA
                if (  fieldInfo.cepClienteA != string.Empty ) 
                   if ( fieldInfo.cepClienteA.Trim().Length > 9  )
                      throw new Exception("O campo \"cepClienteA\" deve ter comprimento máximo de 9 caracter(es).");


                //Field cepClienteB
                if (  fieldInfo.cepClienteB != string.Empty ) 
                   if ( fieldInfo.cepClienteB.Trim().Length > 9  )
                      throw new Exception("O campo \"cepClienteB\" deve ter comprimento máximo de 9 caracter(es).");


                //Field cepClienteC
                if (  fieldInfo.cepClienteC != string.Empty ) 
                   if ( fieldInfo.cepClienteC.Trim().Length > 9  )
                      throw new Exception("O campo \"cepClienteC\" deve ter comprimento máximo de 9 caracter(es).");


                //Field telefoneClienteA
                if (  fieldInfo.telefoneClienteA != string.Empty ) 
                   if ( fieldInfo.telefoneClienteA.Trim().Length > 50  )
                      throw new Exception("O campo \"telefoneClienteA\" deve ter comprimento máximo de 50 caracter(es).");


                //Field telefoneClienteB
                if (  fieldInfo.telefoneClienteB != string.Empty ) 
                   if ( fieldInfo.telefoneClienteB.Trim().Length > 50  )
                      throw new Exception("O campo \"telefoneClienteB\" deve ter comprimento máximo de 50 caracter(es).");


                //Field telefoneClienteC
                if (  fieldInfo.telefoneClienteC != string.Empty ) 
                   if ( fieldInfo.telefoneClienteC.Trim().Length > 50  )
                      throw new Exception("O campo \"telefoneClienteC\" deve ter comprimento máximo de 50 caracter(es).");


                //Field telefoneClienteD
                if (  fieldInfo.telefoneClienteD != string.Empty ) 
                   if ( fieldInfo.telefoneClienteD.Trim().Length > 50  )
                      throw new Exception("O campo \"telefoneClienteD\" deve ter comprimento máximo de 50 caracter(es).");


                //Field celularClienteA
                if (  fieldInfo.celularClienteA != string.Empty ) 
                   if ( fieldInfo.celularClienteA.Trim().Length > 50  )
                      throw new Exception("O campo \"celularClienteA\" deve ter comprimento máximo de 50 caracter(es).");


                //Field celularClienteB
                if (  fieldInfo.celularClienteB != string.Empty ) 
                   if ( fieldInfo.celularClienteB.Trim().Length > 50  )
                      throw new Exception("O campo \"celularClienteB\" deve ter comprimento máximo de 50 caracter(es).");


                //Field celularClienteC
                if (  fieldInfo.celularClienteC != string.Empty ) 
                   if ( fieldInfo.celularClienteC.Trim().Length > 50  )
                      throw new Exception("O campo \"celularClienteC\" deve ter comprimento máximo de 50 caracter(es).");


                //Field complementoCliente
                if (  fieldInfo.complementoCliente != string.Empty ) 
                   if ( fieldInfo.complementoCliente.Trim().Length > 100  )
                      throw new Exception("O campo \"complementoCliente\" deve ter comprimento máximo de 100 caracter(es).");


                //Field emailClienteA
                if (  fieldInfo.emailClienteA != string.Empty ) 
                   if ( fieldInfo.emailClienteA.Trim().Length > 255  )
                      throw new Exception("O campo \"emailClienteA\" deve ter comprimento máximo de 255 caracter(es).");


                //Field emailClienteB
                if (  fieldInfo.emailClienteB != string.Empty ) 
                   if ( fieldInfo.emailClienteB.Trim().Length > 255  )
                      throw new Exception("O campo \"emailClienteB\" deve ter comprimento máximo de 255 caracter(es).");


                //Field contatoClienteA
                if (  fieldInfo.contatoClienteA != string.Empty ) 
                   if ( fieldInfo.contatoClienteA.Trim().Length > 255  )
                      throw new Exception("O campo \"contatoClienteA\" deve ter comprimento máximo de 255 caracter(es).");


                //Field contatoClienteB
                if (  fieldInfo.contatoClienteB != string.Empty ) 
                   if ( fieldInfo.contatoClienteB.Trim().Length > 255  )
                      throw new Exception("O campo \"contatoClienteB\" deve ter comprimento máximo de 255 caracter(es).");


                //Field contatoClienteC
                if (  fieldInfo.contatoClienteC != string.Empty ) 
                   if ( fieldInfo.contatoClienteC.Trim().Length > 255  )
                      throw new Exception("O campo \"contatoClienteC\" deve ter comprimento máximo de 255 caracter(es).");


                //Field cnpjCliente
                if (  fieldInfo.cnpjCliente != string.Empty ) 
                   if ( fieldInfo.cnpjCliente.Trim().Length > 50  )
                      throw new Exception("O campo \"cnpjCliente\" deve ter comprimento máximo de 50 caracter(es).");


                //Field cpfCliente
                if (  fieldInfo.cpfCliente != string.Empty ) 
                   if ( fieldInfo.cpfCliente.Trim().Length > 50  )
                      throw new Exception("O campo \"cpfCliente\" deve ter comprimento máximo de 50 caracter(es).");


                //Field rgCliente
                if (  fieldInfo.rgCliente != string.Empty ) 
                   if ( fieldInfo.rgCliente.Trim().Length > 50  )
                      throw new Exception("O campo \"rgCliente\" deve ter comprimento máximo de 50 caracter(es).");


                //Field inscEstadualCliente
                if (  fieldInfo.inscEstadualCliente != string.Empty ) 
                   if ( fieldInfo.inscEstadualCliente.Trim().Length > 50  )
                      throw new Exception("O campo \"inscEstadualCliente\" deve ter comprimento máximo de 50 caracter(es).");


                //Field observacoesCliente
                if (  fieldInfo.observacoesCliente != string.Empty ) 
                   if ( fieldInfo.observacoesCliente.Trim().Length > 300  )
                      throw new Exception("O campo \"observacoesCliente\" deve ter comprimento máximo de 300 caracter(es).");


                //Field tipoCliente
                if (  fieldInfo.tipoCliente != string.Empty ) 
                   if ( fieldInfo.tipoCliente.Trim().Length > 20  )
                      throw new Exception("O campo \"tipoCliente\" deve ter comprimento máximo de 20 caracter(es).");


                //Field statusCliente
                if (  fieldInfo.statusCliente != string.Empty ) 
                   if ( fieldInfo.statusCliente.Trim().Length > 2  )
                      throw new Exception("O campo \"statusCliente\" deve ter comprimento máximo de 2 caracter(es).");
                if ( ( fieldInfo.statusCliente == string.Empty ) || ( fieldInfo.statusCliente.Trim().Length < 1 ) )
                   throw new Exception("O campo \"statusCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");


                //Field fkSubGrupoCliente
                if ( !( fieldInfo.fkSubGrupoCliente > 0 ) )
                   throw new Exception("O campo \"fkSubGrupoCliente\" deve ser maior que zero.");


                //Field numeroCasaCliente
                if (  fieldInfo.numeroCasaCliente != string.Empty ) 
                   if ( fieldInfo.numeroCasaCliente.Trim().Length > 30  )
                      throw new Exception("O campo \"numeroCasaCliente\" deve ter comprimento máximo de 30 caracter(es).");


                //Field faxCliente
                if (  fieldInfo.faxCliente != string.Empty ) 
                   if ( fieldInfo.faxCliente.Trim().Length > 50  )
                      throw new Exception("O campo \"faxCliente\" deve ter comprimento máximo de 50 caracter(es).");


                //Field emailPrincipalCliente
                if (  fieldInfo.emailPrincipalCliente != string.Empty ) 
                   if ( fieldInfo.emailPrincipalCliente.Trim().Length > 150  )
                      throw new Exception("O campo \"emailPrincipalCliente\" deve ter comprimento máximo de 150 caracter(es).");

                return true;

            }
            catch (Exception e)
            {
                this._ErrorMessage = e.Message;
                return false;
            }

        }

        #region [ Jobs]

        public void ClassificationClientsJOB()
        {
            //new ClienteControl().c
           // new ClienteControl().SortClientsByGroup(DateTime.Now.AddMonths(-6), DateTime.Now.AddDays(-1));
        }

        #endregion
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
//    /// Data de criação: 23/06/2013 15:09:16 
//    /// Descrição: Classe que valida o objeto "ClienteFields". 
//    /// </summary> 
//    public class ClienteValidator 
//    {
//
//
//        #region Propriedade que armazena erros de execução 
//        private string _ErrorMessage = string.Empty;
//        public string ErrorMessage { get { return _ErrorMessage; } }
//        #endregion
//
//
//        public ClienteValidator() {}
//
//
//        public bool isValid( ClienteFields fieldInfo )
//        {
//            try
//            {
//
//
//                //Field nomeCliente
//                if (  fieldInfo.nomeCliente != string.Empty ) 
//                   if ( fieldInfo.nomeCliente.Trim().Length > 255  )
//                      throw new Exception("O campo \"nomeCliente\" deve ter comprimento máximo de 255 caracter(es).");
//                if ( ( fieldInfo.nomeCliente == string.Empty ) || ( fieldInfo.nomeCliente.Trim().Length < 1 ) )
//                   throw new Exception("O campo \"nomeCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
//
//
//                //Field enderecoClienteA
//                if (  fieldInfo.enderecoClienteA != string.Empty ) 
//                   if ( fieldInfo.enderecoClienteA.Trim().Length > 255  )
//                      throw new Exception("O campo \"enderecoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field enderecoClienteB
//                if (  fieldInfo.enderecoClienteB != string.Empty ) 
//                   if ( fieldInfo.enderecoClienteB.Trim().Length > 255  )
//                      throw new Exception("O campo \"enderecoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field enderecoClienteC
//                if (  fieldInfo.enderecoClienteC != string.Empty ) 
//                   if ( fieldInfo.enderecoClienteC.Trim().Length > 255  )
//                      throw new Exception("O campo \"enderecoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field bairroClienteA
//                if (  fieldInfo.bairroClienteA != string.Empty ) 
//                   if ( fieldInfo.bairroClienteA.Trim().Length > 255  )
//                      throw new Exception("O campo \"bairroClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field bairroClienteB
//                if (  fieldInfo.bairroClienteB != string.Empty ) 
//                   if ( fieldInfo.bairroClienteB.Trim().Length > 255  )
//                      throw new Exception("O campo \"bairroClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field bairroClientec
//                if (  fieldInfo.bairroClientec != string.Empty ) 
//                   if ( fieldInfo.bairroClientec.Trim().Length > 255  )
//                      throw new Exception("O campo \"bairroClientec\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field cidadeClienteA
//                if (  fieldInfo.cidadeClienteA != string.Empty ) 
//                   if ( fieldInfo.cidadeClienteA.Trim().Length > 255  )
//                      throw new Exception("O campo \"cidadeClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field cidadeClienteB
//                if (  fieldInfo.cidadeClienteB != string.Empty ) 
//                   if ( fieldInfo.cidadeClienteB.Trim().Length > 255  )
//                      throw new Exception("O campo \"cidadeClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field cidadeClienteC
//                if (  fieldInfo.cidadeClienteC != string.Empty ) 
//                   if ( fieldInfo.cidadeClienteC.Trim().Length > 255  )
//                      throw new Exception("O campo \"cidadeClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field estadoClienteA
//                if (  fieldInfo.estadoClienteA != string.Empty ) 
//                   if ( fieldInfo.estadoClienteA.Trim().Length > 2  )
//                      throw new Exception("O campo \"estadoClienteA\" deve ter comprimento máximo de 2 caracter(es).");
//
//
//                //Field estadoClienteB
//                if (  fieldInfo.estadoClienteB != string.Empty ) 
//                   if ( fieldInfo.estadoClienteB.Trim().Length > 2  )
//                      throw new Exception("O campo \"estadoClienteB\" deve ter comprimento máximo de 2 caracter(es).");
//
//
//                //Field estadoClienteC
//                if (  fieldInfo.estadoClienteC != string.Empty ) 
//                   if ( fieldInfo.estadoClienteC.Trim().Length > 2  )
//                      throw new Exception("O campo \"estadoClienteC\" deve ter comprimento máximo de 2 caracter(es).");
//
//
//                //Field cepClienteA
//                if (  fieldInfo.cepClienteA != string.Empty ) 
//                   if ( fieldInfo.cepClienteA.Trim().Length > 9  )
//                      throw new Exception("O campo \"cepClienteA\" deve ter comprimento máximo de 9 caracter(es).");
//
//
//                //Field cepClienteB
//                if (  fieldInfo.cepClienteB != string.Empty ) 
//                   if ( fieldInfo.cepClienteB.Trim().Length > 9  )
//                      throw new Exception("O campo \"cepClienteB\" deve ter comprimento máximo de 9 caracter(es).");
//
//
//                //Field cepClienteC
//                if (  fieldInfo.cepClienteC != string.Empty ) 
//                   if ( fieldInfo.cepClienteC.Trim().Length > 9  )
//                      throw new Exception("O campo \"cepClienteC\" deve ter comprimento máximo de 9 caracter(es).");
//
//
//                //Field telefoneClienteA
//                if (  fieldInfo.telefoneClienteA != string.Empty ) 
//                   if ( fieldInfo.telefoneClienteA.Trim().Length > 50  )
//                      throw new Exception("O campo \"telefoneClienteA\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field telefoneClienteB
//                if (  fieldInfo.telefoneClienteB != string.Empty ) 
//                   if ( fieldInfo.telefoneClienteB.Trim().Length > 50  )
//                      throw new Exception("O campo \"telefoneClienteB\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field telefoneClienteC
//                if (  fieldInfo.telefoneClienteC != string.Empty ) 
//                   if ( fieldInfo.telefoneClienteC.Trim().Length > 50  )
//                      throw new Exception("O campo \"telefoneClienteC\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field telefoneClienteD
//                if (  fieldInfo.telefoneClienteD != string.Empty ) 
//                   if ( fieldInfo.telefoneClienteD.Trim().Length > 50  )
//                      throw new Exception("O campo \"telefoneClienteD\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field celularClienteA
//                if (  fieldInfo.celularClienteA != string.Empty ) 
//                   if ( fieldInfo.celularClienteA.Trim().Length > 50  )
//                      throw new Exception("O campo \"celularClienteA\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field celularClienteB
//                if (  fieldInfo.celularClienteB != string.Empty ) 
//                   if ( fieldInfo.celularClienteB.Trim().Length > 50  )
//                      throw new Exception("O campo \"celularClienteB\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field celularClienteC
//                if (  fieldInfo.celularClienteC != string.Empty ) 
//                   if ( fieldInfo.celularClienteC.Trim().Length > 50  )
//                      throw new Exception("O campo \"celularClienteC\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field complementoCliente
//                if (  fieldInfo.complementoCliente != string.Empty ) 
//                   if ( fieldInfo.complementoCliente.Trim().Length > 100  )
//                      throw new Exception("O campo \"complementoCliente\" deve ter comprimento máximo de 100 caracter(es).");
//
//
//                //Field emailClienteA
//                if (  fieldInfo.emailClienteA != string.Empty ) 
//                   if ( fieldInfo.emailClienteA.Trim().Length > 255  )
//                      throw new Exception("O campo \"emailClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field emailClienteB
//                if (  fieldInfo.emailClienteB != string.Empty ) 
//                   if ( fieldInfo.emailClienteB.Trim().Length > 255  )
//                      throw new Exception("O campo \"emailClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field contatoClienteA
//                if (  fieldInfo.contatoClienteA != string.Empty ) 
//                   if ( fieldInfo.contatoClienteA.Trim().Length > 255  )
//                      throw new Exception("O campo \"contatoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field contatoClienteB
//                if (  fieldInfo.contatoClienteB != string.Empty ) 
//                   if ( fieldInfo.contatoClienteB.Trim().Length > 255  )
//                      throw new Exception("O campo \"contatoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field contatoClienteC
//                if (  fieldInfo.contatoClienteC != string.Empty ) 
//                   if ( fieldInfo.contatoClienteC.Trim().Length > 255  )
//                      throw new Exception("O campo \"contatoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//
//
//                //Field cnpjCliente
//                if (  fieldInfo.cnpjCliente != string.Empty ) 
//                   if ( fieldInfo.cnpjCliente.Trim().Length > 50  )
//                      throw new Exception("O campo \"cnpjCliente\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field cpfCliente
//                if (  fieldInfo.cpfCliente != string.Empty ) 
//                   if ( fieldInfo.cpfCliente.Trim().Length > 50  )
//                      throw new Exception("O campo \"cpfCliente\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field rgCliente
//                if (  fieldInfo.rgCliente != string.Empty ) 
//                   if ( fieldInfo.rgCliente.Trim().Length > 50  )
//                      throw new Exception("O campo \"rgCliente\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field inscEstadualCliente
//                if (  fieldInfo.inscEstadualCliente != string.Empty ) 
//                   if ( fieldInfo.inscEstadualCliente.Trim().Length > 50  )
//                      throw new Exception("O campo \"inscEstadualCliente\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field observacoesCliente
//                if (  fieldInfo.observacoesCliente != string.Empty ) 
//                   if ( fieldInfo.observacoesCliente.Trim().Length > 300  )
//                      throw new Exception("O campo \"observacoesCliente\" deve ter comprimento máximo de 300 caracter(es).");
//
//
//                //Field tipoCliente
//                if (  fieldInfo.tipoCliente != string.Empty ) 
//                   if ( fieldInfo.tipoCliente.Trim().Length > 20  )
//                      throw new Exception("O campo \"tipoCliente\" deve ter comprimento máximo de 20 caracter(es).");
//
//
//                //Field statusCliente
//                if (  fieldInfo.statusCliente != string.Empty ) 
//                   if ( fieldInfo.statusCliente.Trim().Length > 2  )
//                      throw new Exception("O campo \"statusCliente\" deve ter comprimento máximo de 2 caracter(es).");
//                if ( ( fieldInfo.statusCliente == string.Empty ) || ( fieldInfo.statusCliente.Trim().Length < 1 ) )
//                   throw new Exception("O campo \"statusCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
//
//
//                //Field fkSubGrupoCliente
//                if ( !( fieldInfo.fkSubGrupoCliente > 0 ) )
//                   throw new Exception("O campo \"fkSubGrupoCliente\" deve ser maior que zero.");
//
//
//                //Field numeroCasaCliente
//                if (  fieldInfo.numeroCasaCliente != string.Empty ) 
//                   if ( fieldInfo.numeroCasaCliente.Trim().Length > 30  )
//                      throw new Exception("O campo \"numeroCasaCliente\" deve ter comprimento máximo de 30 caracter(es).");
//
//
//                //Field faxCliente
//                if (  fieldInfo.faxCliente != string.Empty ) 
//                   if ( fieldInfo.faxCliente.Trim().Length > 50  )
//                      throw new Exception("O campo \"faxCliente\" deve ter comprimento máximo de 50 caracter(es).");
//
//
//                //Field emailPrincipalCliente
//                if (  fieldInfo.emailPrincipalCliente != string.Empty ) 
//                   if ( fieldInfo.emailPrincipalCliente.Trim().Length > 1  )
//                      throw new Exception("O campo \"emailPrincipalCliente\" deve ter comprimento máximo de 1 caracter(es).");
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
////    /// Data de criação: 22/05/2013 09:16:07 
////    /// Descrição: Classe que valida o objeto "ClienteFields". 
////    /// </summary> 
////    public class ClienteValidator 
////    {
////
////
////        #region Propriedade que armazena erros de execução 
////        private string _ErrorMessage = string.Empty;
////        public string ErrorMessage { get { return _ErrorMessage; } }
////        #endregion
////
////
////        public ClienteValidator() {}
////
////
////        public bool isValid( ClienteFields fieldInfo )
////        {
////            try
////            {
////
////
////                //Field nomeCliente
////                if (  fieldInfo.nomeCliente != string.Empty ) 
////                   if ( fieldInfo.nomeCliente.Trim().Length > 255  )
////                      throw new Exception("O campo \"nomeCliente\" deve ter comprimento máximo de 255 caracter(es).");
////                if ( ( fieldInfo.nomeCliente == string.Empty ) || ( fieldInfo.nomeCliente.Trim().Length < 1 ) )
////                   throw new Exception("O campo \"nomeCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
////
////
////                //Field enderecoClienteA
////                if (  fieldInfo.enderecoClienteA != string.Empty ) 
////                   if ( fieldInfo.enderecoClienteA.Trim().Length > 255  )
////                      throw new Exception("O campo \"enderecoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field enderecoClienteB
////                if (  fieldInfo.enderecoClienteB != string.Empty ) 
////                   if ( fieldInfo.enderecoClienteB.Trim().Length > 255  )
////                      throw new Exception("O campo \"enderecoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field enderecoClienteC
////                if (  fieldInfo.enderecoClienteC != string.Empty ) 
////                   if ( fieldInfo.enderecoClienteC.Trim().Length > 255  )
////                      throw new Exception("O campo \"enderecoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field bairroClienteA
////                if (  fieldInfo.bairroClienteA != string.Empty ) 
////                   if ( fieldInfo.bairroClienteA.Trim().Length > 255  )
////                      throw new Exception("O campo \"bairroClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field bairroClienteB
////                if (  fieldInfo.bairroClienteB != string.Empty ) 
////                   if ( fieldInfo.bairroClienteB.Trim().Length > 255  )
////                      throw new Exception("O campo \"bairroClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field bairroClientec
////                if (  fieldInfo.bairroClientec != string.Empty ) 
////                   if ( fieldInfo.bairroClientec.Trim().Length > 255  )
////                      throw new Exception("O campo \"bairroClientec\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field cidadeClienteA
////                if (  fieldInfo.cidadeClienteA != string.Empty ) 
////                   if ( fieldInfo.cidadeClienteA.Trim().Length > 255  )
////                      throw new Exception("O campo \"cidadeClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field cidadeClienteB
////                if (  fieldInfo.cidadeClienteB != string.Empty ) 
////                   if ( fieldInfo.cidadeClienteB.Trim().Length > 255  )
////                      throw new Exception("O campo \"cidadeClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field cidadeClienteC
////                if (  fieldInfo.cidadeClienteC != string.Empty ) 
////                   if ( fieldInfo.cidadeClienteC.Trim().Length > 255  )
////                      throw new Exception("O campo \"cidadeClienteC\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field estadoClienteA
////                if (  fieldInfo.estadoClienteA != string.Empty ) 
////                   if ( fieldInfo.estadoClienteA.Trim().Length > 2  )
////                      throw new Exception("O campo \"estadoClienteA\" deve ter comprimento máximo de 2 caracter(es).");
////
////
////                //Field estadoClienteB
////                if (  fieldInfo.estadoClienteB != string.Empty ) 
////                   if ( fieldInfo.estadoClienteB.Trim().Length > 2  )
////                      throw new Exception("O campo \"estadoClienteB\" deve ter comprimento máximo de 2 caracter(es).");
////
////
////                //Field estadoClienteC
////                if (  fieldInfo.estadoClienteC != string.Empty ) 
////                   if ( fieldInfo.estadoClienteC.Trim().Length > 2  )
////                      throw new Exception("O campo \"estadoClienteC\" deve ter comprimento máximo de 2 caracter(es).");
////
////
////                //Field cepClienteA
////                if (  fieldInfo.cepClienteA != string.Empty ) 
////                   if ( fieldInfo.cepClienteA.Trim().Length > 9  )
////                      throw new Exception("O campo \"cepClienteA\" deve ter comprimento máximo de 9 caracter(es).");
////
////
////                //Field cepClienteB
////                if (  fieldInfo.cepClienteB != string.Empty ) 
////                   if ( fieldInfo.cepClienteB.Trim().Length > 9  )
////                      throw new Exception("O campo \"cepClienteB\" deve ter comprimento máximo de 9 caracter(es).");
////
////
////                //Field cepClienteC
////                if (  fieldInfo.cepClienteC != string.Empty ) 
////                   if ( fieldInfo.cepClienteC.Trim().Length > 9  )
////                      throw new Exception("O campo \"cepClienteC\" deve ter comprimento máximo de 9 caracter(es).");
////
////
////                //Field telefoneClienteA
////                if (  fieldInfo.telefoneClienteA != string.Empty ) 
////                   if ( fieldInfo.telefoneClienteA.Trim().Length > 50  )
////                      throw new Exception("O campo \"telefoneClienteA\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field telefoneClienteB
////                if (  fieldInfo.telefoneClienteB != string.Empty ) 
////                   if ( fieldInfo.telefoneClienteB.Trim().Length > 50  )
////                      throw new Exception("O campo \"telefoneClienteB\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field telefoneClienteC
////                if (  fieldInfo.telefoneClienteC != string.Empty ) 
////                   if ( fieldInfo.telefoneClienteC.Trim().Length > 50  )
////                      throw new Exception("O campo \"telefoneClienteC\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field telefoneClienteD
////                if (  fieldInfo.telefoneClienteD != string.Empty ) 
////                   if ( fieldInfo.telefoneClienteD.Trim().Length > 50  )
////                      throw new Exception("O campo \"telefoneClienteD\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field celularClienteA
////                if (  fieldInfo.celularClienteA != string.Empty ) 
////                   if ( fieldInfo.celularClienteA.Trim().Length > 50  )
////                      throw new Exception("O campo \"celularClienteA\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field celularClienteB
////                if (  fieldInfo.celularClienteB != string.Empty ) 
////                   if ( fieldInfo.celularClienteB.Trim().Length > 50  )
////                      throw new Exception("O campo \"celularClienteB\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field celularClienteC
////                if (  fieldInfo.celularClienteC != string.Empty ) 
////                   if ( fieldInfo.celularClienteC.Trim().Length > 50  )
////                      throw new Exception("O campo \"celularClienteC\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field complementoCliente
////                if (  fieldInfo.complementoCliente != string.Empty ) 
////                   if ( fieldInfo.complementoCliente.Trim().Length > 100  )
////                      throw new Exception("O campo \"complementoCliente\" deve ter comprimento máximo de 100 caracter(es).");
////
////
////                //Field emailClienteA
////                if (  fieldInfo.emailClienteA != string.Empty ) 
////                   if ( fieldInfo.emailClienteA.Trim().Length > 255  )
////                      throw new Exception("O campo \"emailClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field emailClienteB
////                if (  fieldInfo.emailClienteB != string.Empty ) 
////                   if ( fieldInfo.emailClienteB.Trim().Length > 255  )
////                      throw new Exception("O campo \"emailClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field contatoClienteA
////                if (  fieldInfo.contatoClienteA != string.Empty ) 
////                   if ( fieldInfo.contatoClienteA.Trim().Length > 255  )
////                      throw new Exception("O campo \"contatoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field contatoClienteB
////                if (  fieldInfo.contatoClienteB != string.Empty ) 
////                   if ( fieldInfo.contatoClienteB.Trim().Length > 255  )
////                      throw new Exception("O campo \"contatoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field contatoClienteC
////                if (  fieldInfo.contatoClienteC != string.Empty ) 
////                   if ( fieldInfo.contatoClienteC.Trim().Length > 255  )
////                      throw new Exception("O campo \"contatoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
////
////
////                //Field cnpjCliente
////                if (  fieldInfo.cnpjCliente != string.Empty ) 
////                   if ( fieldInfo.cnpjCliente.Trim().Length > 50  )
////                      throw new Exception("O campo \"cnpjCliente\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field cpfCliente
////                if (  fieldInfo.cpfCliente != string.Empty ) 
////                   if ( fieldInfo.cpfCliente.Trim().Length > 50  )
////                      throw new Exception("O campo \"cpfCliente\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field rgCliente
////                if (  fieldInfo.rgCliente != string.Empty ) 
////                   if ( fieldInfo.rgCliente.Trim().Length > 50  )
////                      throw new Exception("O campo \"rgCliente\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field inscEstadualCliente
////                if (  fieldInfo.inscEstadualCliente != string.Empty ) 
////                   if ( fieldInfo.inscEstadualCliente.Trim().Length > 50  )
////                      throw new Exception("O campo \"inscEstadualCliente\" deve ter comprimento máximo de 50 caracter(es).");
////
////
////                //Field observacoesCliente
////                if (  fieldInfo.observacoesCliente != string.Empty ) 
////                   if ( fieldInfo.observacoesCliente.Trim().Length > 300  )
////                      throw new Exception("O campo \"observacoesCliente\" deve ter comprimento máximo de 300 caracter(es).");
////
////
////                //Field tipoCliente
////                if (  fieldInfo.tipoCliente != string.Empty ) 
////                   if ( fieldInfo.tipoCliente.Trim().Length > 20  )
////                      throw new Exception("O campo \"tipoCliente\" deve ter comprimento máximo de 20 caracter(es).");
////
////
////                //Field statusCliente
////                if (  fieldInfo.statusCliente != string.Empty ) 
////                   if ( fieldInfo.statusCliente.Trim().Length > 2  )
////                      throw new Exception("O campo \"statusCliente\" deve ter comprimento máximo de 2 caracter(es).");
////                if ( ( fieldInfo.statusCliente == string.Empty ) || ( fieldInfo.statusCliente.Trim().Length < 1 ) )
////                   throw new Exception("O campo \"statusCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
////
////
////                //Field fkSubGrupoCliente
////                if ( !( fieldInfo.fkSubGrupoCliente > 0 ) )
////                   throw new Exception("O campo \"fkSubGrupoCliente\" deve ser maior que zero.");
////
////
////                //Field numeroCasaCliente
////                if (  fieldInfo.numeroCasaCliente != string.Empty ) 
////                   if ( fieldInfo.numeroCasaCliente.Trim().Length > 30  )
////                      throw new Exception("O campo \"numeroCasaCliente\" deve ter comprimento máximo de 30 caracter(es).");
////
////
////                //Field faxCliente
////                if (  fieldInfo.faxCliente != string.Empty ) 
////                   if ( fieldInfo.faxCliente.Trim().Length > 50  )
////                      throw new Exception("O campo \"faxCliente\" deve ter comprimento máximo de 50 caracter(es).");
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
//////    /// Data de criação: 22/05/2013 09:08:26 
//////    /// Descrição: Classe que valida o objeto "ClienteFields". 
//////    /// </summary> 
//////    public class ClienteValidator 
//////    {
//////
//////
//////        #region Propriedade que armazena erros de execução 
//////        private string _ErrorMessage = string.Empty;
//////        public string ErrorMessage { get { return _ErrorMessage; } }
//////        #endregion
//////
//////
//////        public ClienteValidator() {}
//////
//////
//////        public bool isValid( ClienteFields fieldInfo )
//////        {
//////            try
//////            {
//////
//////
//////                //Field nomeCliente
//////                if (  fieldInfo.nomeCliente != string.Empty ) 
//////                   if ( fieldInfo.nomeCliente.Trim().Length > 255  )
//////                      throw new Exception("O campo \"nomeCliente\" deve ter comprimento máximo de 255 caracter(es).");
//////                if ( ( fieldInfo.nomeCliente == string.Empty ) || ( fieldInfo.nomeCliente.Trim().Length < 1 ) )
//////                   throw new Exception("O campo \"nomeCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
//////
//////
//////                //Field enderecoClienteA
//////                if (  fieldInfo.enderecoClienteA != string.Empty ) 
//////                   if ( fieldInfo.enderecoClienteA.Trim().Length > 255  )
//////                      throw new Exception("O campo \"enderecoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field enderecoClienteB
//////                if (  fieldInfo.enderecoClienteB != string.Empty ) 
//////                   if ( fieldInfo.enderecoClienteB.Trim().Length > 255  )
//////                      throw new Exception("O campo \"enderecoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field enderecoClienteC
//////                if (  fieldInfo.enderecoClienteC != string.Empty ) 
//////                   if ( fieldInfo.enderecoClienteC.Trim().Length > 255  )
//////                      throw new Exception("O campo \"enderecoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field bairroClienteA
//////                if (  fieldInfo.bairroClienteA != string.Empty ) 
//////                   if ( fieldInfo.bairroClienteA.Trim().Length > 255  )
//////                      throw new Exception("O campo \"bairroClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field bairroClienteB
//////                if (  fieldInfo.bairroClienteB != string.Empty ) 
//////                   if ( fieldInfo.bairroClienteB.Trim().Length > 255  )
//////                      throw new Exception("O campo \"bairroClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field bairroClientec
//////                if (  fieldInfo.bairroClientec != string.Empty ) 
//////                   if ( fieldInfo.bairroClientec.Trim().Length > 255  )
//////                      throw new Exception("O campo \"bairroClientec\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field cidadeClienteA
//////                if (  fieldInfo.cidadeClienteA != string.Empty ) 
//////                   if ( fieldInfo.cidadeClienteA.Trim().Length > 255  )
//////                      throw new Exception("O campo \"cidadeClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field cidadeClienteB
//////                if (  fieldInfo.cidadeClienteB != string.Empty ) 
//////                   if ( fieldInfo.cidadeClienteB.Trim().Length > 255  )
//////                      throw new Exception("O campo \"cidadeClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field cidadeClienteC
//////                if (  fieldInfo.cidadeClienteC != string.Empty ) 
//////                   if ( fieldInfo.cidadeClienteC.Trim().Length > 255  )
//////                      throw new Exception("O campo \"cidadeClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field estadoClienteA
//////                if (  fieldInfo.estadoClienteA != string.Empty ) 
//////                   if ( fieldInfo.estadoClienteA.Trim().Length > 2  )
//////                      throw new Exception("O campo \"estadoClienteA\" deve ter comprimento máximo de 2 caracter(es).");
//////
//////
//////                //Field estadoClienteB
//////                if (  fieldInfo.estadoClienteB != string.Empty ) 
//////                   if ( fieldInfo.estadoClienteB.Trim().Length > 2  )
//////                      throw new Exception("O campo \"estadoClienteB\" deve ter comprimento máximo de 2 caracter(es).");
//////
//////
//////                //Field estadoClienteC
//////                if (  fieldInfo.estadoClienteC != string.Empty ) 
//////                   if ( fieldInfo.estadoClienteC.Trim().Length > 2  )
//////                      throw new Exception("O campo \"estadoClienteC\" deve ter comprimento máximo de 2 caracter(es).");
//////
//////
//////                //Field cepClienteA
//////                if (  fieldInfo.cepClienteA != string.Empty ) 
//////                   if ( fieldInfo.cepClienteA.Trim().Length > 9  )
//////                      throw new Exception("O campo \"cepClienteA\" deve ter comprimento máximo de 9 caracter(es).");
//////
//////
//////                //Field cepClienteB
//////                if (  fieldInfo.cepClienteB != string.Empty ) 
//////                   if ( fieldInfo.cepClienteB.Trim().Length > 9  )
//////                      throw new Exception("O campo \"cepClienteB\" deve ter comprimento máximo de 9 caracter(es).");
//////
//////
//////                //Field cepClienteC
//////                if (  fieldInfo.cepClienteC != string.Empty ) 
//////                   if ( fieldInfo.cepClienteC.Trim().Length > 9  )
//////                      throw new Exception("O campo \"cepClienteC\" deve ter comprimento máximo de 9 caracter(es).");
//////
//////
//////                //Field telefoneClienteA
//////                if (  fieldInfo.telefoneClienteA != string.Empty ) 
//////                   if ( fieldInfo.telefoneClienteA.Trim().Length > 50  )
//////                      throw new Exception("O campo \"telefoneClienteA\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field telefoneClienteB
//////                if (  fieldInfo.telefoneClienteB != string.Empty ) 
//////                   if ( fieldInfo.telefoneClienteB.Trim().Length > 50  )
//////                      throw new Exception("O campo \"telefoneClienteB\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field telefoneClienteC
//////                if (  fieldInfo.telefoneClienteC != string.Empty ) 
//////                   if ( fieldInfo.telefoneClienteC.Trim().Length > 50  )
//////                      throw new Exception("O campo \"telefoneClienteC\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field telefoneClienteD
//////                if (  fieldInfo.telefoneClienteD != string.Empty ) 
//////                   if ( fieldInfo.telefoneClienteD.Trim().Length > 50  )
//////                      throw new Exception("O campo \"telefoneClienteD\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field celularClienteA
//////                if (  fieldInfo.celularClienteA != string.Empty ) 
//////                   if ( fieldInfo.celularClienteA.Trim().Length > 50  )
//////                      throw new Exception("O campo \"celularClienteA\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field celularClienteB
//////                if (  fieldInfo.celularClienteB != string.Empty ) 
//////                   if ( fieldInfo.celularClienteB.Trim().Length > 50  )
//////                      throw new Exception("O campo \"celularClienteB\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field celularClienteC
//////                if (  fieldInfo.celularClienteC != string.Empty ) 
//////                   if ( fieldInfo.celularClienteC.Trim().Length > 50  )
//////                      throw new Exception("O campo \"celularClienteC\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field complementoCliente
//////                if (  fieldInfo.complementoCliente != string.Empty ) 
//////                   if ( fieldInfo.complementoCliente.Trim().Length > 100  )
//////                      throw new Exception("O campo \"complementoCliente\" deve ter comprimento máximo de 100 caracter(es).");
//////
//////
//////                //Field emailClienteA
//////                if (  fieldInfo.emailClienteA != string.Empty ) 
//////                   if ( fieldInfo.emailClienteA.Trim().Length > 255  )
//////                      throw new Exception("O campo \"emailClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field emailClienteB
//////                if (  fieldInfo.emailClienteB != string.Empty ) 
//////                   if ( fieldInfo.emailClienteB.Trim().Length > 255  )
//////                      throw new Exception("O campo \"emailClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field contatoClienteA
//////                if (  fieldInfo.contatoClienteA != string.Empty ) 
//////                   if ( fieldInfo.contatoClienteA.Trim().Length > 255  )
//////                      throw new Exception("O campo \"contatoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field contatoClienteB
//////                if (  fieldInfo.contatoClienteB != string.Empty ) 
//////                   if ( fieldInfo.contatoClienteB.Trim().Length > 255  )
//////                      throw new Exception("O campo \"contatoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field contatoClienteC
//////                if (  fieldInfo.contatoClienteC != string.Empty ) 
//////                   if ( fieldInfo.contatoClienteC.Trim().Length > 255  )
//////                      throw new Exception("O campo \"contatoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//////
//////
//////                //Field cnpjCliente
//////                if (  fieldInfo.cnpjCliente != string.Empty ) 
//////                   if ( fieldInfo.cnpjCliente.Trim().Length > 50  )
//////                      throw new Exception("O campo \"cnpjCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field cpfCliente
//////                if (  fieldInfo.cpfCliente != string.Empty ) 
//////                   if ( fieldInfo.cpfCliente.Trim().Length > 50  )
//////                      throw new Exception("O campo \"cpfCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field rgCliente
//////                if (  fieldInfo.rgCliente != string.Empty ) 
//////                   if ( fieldInfo.rgCliente.Trim().Length > 50  )
//////                      throw new Exception("O campo \"rgCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field inscEstadualCliente
//////                if (  fieldInfo.inscEstadualCliente != string.Empty ) 
//////                   if ( fieldInfo.inscEstadualCliente.Trim().Length > 50  )
//////                      throw new Exception("O campo \"inscEstadualCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////
//////
//////                //Field observacoesCliente
//////                if (  fieldInfo.observacoesCliente != string.Empty ) 
//////                   if ( fieldInfo.observacoesCliente.Trim().Length > 300  )
//////                      throw new Exception("O campo \"observacoesCliente\" deve ter comprimento máximo de 300 caracter(es).");
//////
//////
//////                //Field tipoCliente
//////                if (  fieldInfo.tipoCliente != string.Empty ) 
//////                   if ( fieldInfo.tipoCliente.Trim().Length > 20  )
//////                      throw new Exception("O campo \"tipoCliente\" deve ter comprimento máximo de 20 caracter(es).");
//////
//////
//////                //Field statusCliente
//////                if (  fieldInfo.statusCliente != string.Empty ) 
//////                   if ( fieldInfo.statusCliente.Trim().Length > 2  )
//////                      throw new Exception("O campo \"statusCliente\" deve ter comprimento máximo de 2 caracter(es).");
//////                if ( ( fieldInfo.statusCliente == string.Empty ) || ( fieldInfo.statusCliente.Trim().Length < 1 ) )
//////                   throw new Exception("O campo \"statusCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
//////
//////
//////                //Field fkSubGrupoCliente
//////                if ( !( fieldInfo.fkSubGrupoCliente > 0 ) )
//////                   throw new Exception("O campo \"fkSubGrupoCliente\" deve ser maior que zero.");
//////
//////
//////                //Field numeroCasaCliente
//////                if (  fieldInfo.numeroCasaCliente != string.Empty ) 
//////                   if ( fieldInfo.numeroCasaCliente.Trim().Length > 30  )
//////                      throw new Exception("O campo \"numeroCasaCliente\" deve ter comprimento máximo de 30 caracter(es).");
//////
//////
//////                //Field faxCliente
//////                if (  fieldInfo.faxCliente != string.Empty ) 
//////                   if ( fieldInfo.faxCliente.Trim().Length > 50  )
//////                      throw new Exception("O campo \"faxCliente\" deve ter comprimento máximo de 50 caracter(es).");
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
////////    /// Data de criação: 29/04/2013 12:36:36 
////////    /// Descrição: Classe que valida o objeto "ClienteFields". 
////////    /// </summary> 
////////    public class ClienteValidator 
////////    {
////////
////////
////////        #region Propriedade que armazena erros de execução 
////////        private string _ErrorMessage = string.Empty;
////////        public string ErrorMessage { get { return _ErrorMessage; } }
////////        #endregion
////////
////////
////////        public ClienteValidator() {}
////////
////////
////////        public bool isValid( ClienteFields fieldInfo )
////////        {
////////            try
////////            {
////////
////////
////////                //Field nomeCliente
////////                if (  fieldInfo.nomeCliente != string.Empty ) 
////////                   if ( fieldInfo.nomeCliente.Trim().Length > 255  )
////////                      throw new Exception("O campo \"nomeCliente\" deve ter comprimento máximo de 255 caracter(es).");
////////                if ( ( fieldInfo.nomeCliente == string.Empty ) || ( fieldInfo.nomeCliente.Trim().Length < 1 ) )
////////                   throw new Exception("O campo \"nomeCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
////////
////////
////////                //Field enderecoClienteA
////////                if (  fieldInfo.enderecoClienteA != string.Empty ) 
////////                   if ( fieldInfo.enderecoClienteA.Trim().Length > 255  )
////////                      throw new Exception("O campo \"enderecoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field enderecoClienteB
////////                if (  fieldInfo.enderecoClienteB != string.Empty ) 
////////                   if ( fieldInfo.enderecoClienteB.Trim().Length > 255  )
////////                      throw new Exception("O campo \"enderecoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field enderecoClienteC
////////                if (  fieldInfo.enderecoClienteC != string.Empty ) 
////////                   if ( fieldInfo.enderecoClienteC.Trim().Length > 255  )
////////                      throw new Exception("O campo \"enderecoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field bairroClienteA
////////                if (  fieldInfo.bairroClienteA != string.Empty ) 
////////                   if ( fieldInfo.bairroClienteA.Trim().Length > 255  )
////////                      throw new Exception("O campo \"bairroClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field bairroClienteB
////////                if (  fieldInfo.bairroClienteB != string.Empty ) 
////////                   if ( fieldInfo.bairroClienteB.Trim().Length > 255  )
////////                      throw new Exception("O campo \"bairroClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field bairroClientec
////////                if (  fieldInfo.bairroClientec != string.Empty ) 
////////                   if ( fieldInfo.bairroClientec.Trim().Length > 255  )
////////                      throw new Exception("O campo \"bairroClientec\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field cidadeClienteA
////////                if (  fieldInfo.cidadeClienteA != string.Empty ) 
////////                   if ( fieldInfo.cidadeClienteA.Trim().Length > 255  )
////////                      throw new Exception("O campo \"cidadeClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field cidadeClienteB
////////                if (  fieldInfo.cidadeClienteB != string.Empty ) 
////////                   if ( fieldInfo.cidadeClienteB.Trim().Length > 255  )
////////                      throw new Exception("O campo \"cidadeClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field cidadeClienteC
////////                if (  fieldInfo.cidadeClienteC != string.Empty ) 
////////                   if ( fieldInfo.cidadeClienteC.Trim().Length > 255  )
////////                      throw new Exception("O campo \"cidadeClienteC\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field estadoClienteA
////////                if (  fieldInfo.estadoClienteA != string.Empty ) 
////////                   if ( fieldInfo.estadoClienteA.Trim().Length > 2  )
////////                      throw new Exception("O campo \"estadoClienteA\" deve ter comprimento máximo de 2 caracter(es).");
////////
////////
////////                //Field estadoClienteB
////////                if (  fieldInfo.estadoClienteB != string.Empty ) 
////////                   if ( fieldInfo.estadoClienteB.Trim().Length > 2  )
////////                      throw new Exception("O campo \"estadoClienteB\" deve ter comprimento máximo de 2 caracter(es).");
////////
////////
////////                //Field estadoClienteC
////////                if (  fieldInfo.estadoClienteC != string.Empty ) 
////////                   if ( fieldInfo.estadoClienteC.Trim().Length > 2  )
////////                      throw new Exception("O campo \"estadoClienteC\" deve ter comprimento máximo de 2 caracter(es).");
////////
////////
////////                //Field cepClienteA
////////                if (  fieldInfo.cepClienteA != string.Empty ) 
////////                   if ( fieldInfo.cepClienteA.Trim().Length > 9  )
////////                      throw new Exception("O campo \"cepClienteA\" deve ter comprimento máximo de 9 caracter(es).");
////////
////////
////////                //Field cepClienteB
////////                if (  fieldInfo.cepClienteB != string.Empty ) 
////////                   if ( fieldInfo.cepClienteB.Trim().Length > 9  )
////////                      throw new Exception("O campo \"cepClienteB\" deve ter comprimento máximo de 9 caracter(es).");
////////
////////
////////                //Field cepClienteC
////////                if (  fieldInfo.cepClienteC != string.Empty ) 
////////                   if ( fieldInfo.cepClienteC.Trim().Length > 9  )
////////                      throw new Exception("O campo \"cepClienteC\" deve ter comprimento máximo de 9 caracter(es).");
////////
////////
////////                //Field telefoneClienteA
////////                if (  fieldInfo.telefoneClienteA != string.Empty ) 
////////                   if ( fieldInfo.telefoneClienteA.Trim().Length > 50  )
////////                      throw new Exception("O campo \"telefoneClienteA\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field telefoneClienteB
////////                if (  fieldInfo.telefoneClienteB != string.Empty ) 
////////                   if ( fieldInfo.telefoneClienteB.Trim().Length > 50  )
////////                      throw new Exception("O campo \"telefoneClienteB\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field telefoneClienteC
////////                if (  fieldInfo.telefoneClienteC != string.Empty ) 
////////                   if ( fieldInfo.telefoneClienteC.Trim().Length > 50  )
////////                      throw new Exception("O campo \"telefoneClienteC\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field telefoneClienteD
////////                if (  fieldInfo.telefoneClienteD != string.Empty ) 
////////                   if ( fieldInfo.telefoneClienteD.Trim().Length > 50  )
////////                      throw new Exception("O campo \"telefoneClienteD\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field celularClienteA
////////                if (  fieldInfo.celularClienteA != string.Empty ) 
////////                   if ( fieldInfo.celularClienteA.Trim().Length > 50  )
////////                      throw new Exception("O campo \"celularClienteA\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field celularClienteB
////////                if (  fieldInfo.celularClienteB != string.Empty ) 
////////                   if ( fieldInfo.celularClienteB.Trim().Length > 50  )
////////                      throw new Exception("O campo \"celularClienteB\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field celularClienteC
////////                if (  fieldInfo.celularClienteC != string.Empty ) 
////////                   if ( fieldInfo.celularClienteC.Trim().Length > 50  )
////////                      throw new Exception("O campo \"celularClienteC\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field complementoCliente
////////                if (  fieldInfo.complementoCliente != string.Empty ) 
////////                   if ( fieldInfo.complementoCliente.Trim().Length > 100  )
////////                      throw new Exception("O campo \"complementoCliente\" deve ter comprimento máximo de 100 caracter(es).");
////////
////////
////////                //Field emailClienteA
////////                if (  fieldInfo.emailClienteA != string.Empty ) 
////////                   if ( fieldInfo.emailClienteA.Trim().Length > 255  )
////////                      throw new Exception("O campo \"emailClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field emailClienteB
////////                if (  fieldInfo.emailClienteB != string.Empty ) 
////////                   if ( fieldInfo.emailClienteB.Trim().Length > 255  )
////////                      throw new Exception("O campo \"emailClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field contatoClienteA
////////                if (  fieldInfo.contatoClienteA != string.Empty ) 
////////                   if ( fieldInfo.contatoClienteA.Trim().Length > 255  )
////////                      throw new Exception("O campo \"contatoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field contatoClienteB
////////                if (  fieldInfo.contatoClienteB != string.Empty ) 
////////                   if ( fieldInfo.contatoClienteB.Trim().Length > 255  )
////////                      throw new Exception("O campo \"contatoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field contatoClienteC
////////                if (  fieldInfo.contatoClienteC != string.Empty ) 
////////                   if ( fieldInfo.contatoClienteC.Trim().Length > 255  )
////////                      throw new Exception("O campo \"contatoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
////////
////////
////////                //Field cnpjCliente
////////                if (  fieldInfo.cnpjCliente != string.Empty ) 
////////                   if ( fieldInfo.cnpjCliente.Trim().Length > 50  )
////////                      throw new Exception("O campo \"cnpjCliente\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field cpfCliente
////////                if (  fieldInfo.cpfCliente != string.Empty ) 
////////                   if ( fieldInfo.cpfCliente.Trim().Length > 50  )
////////                      throw new Exception("O campo \"cpfCliente\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field rgCliente
////////                if (  fieldInfo.rgCliente != string.Empty ) 
////////                   if ( fieldInfo.rgCliente.Trim().Length > 50  )
////////                      throw new Exception("O campo \"rgCliente\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field inscEstadualCliente
////////                if (  fieldInfo.inscEstadualCliente != string.Empty ) 
////////                   if ( fieldInfo.inscEstadualCliente.Trim().Length > 50  )
////////                      throw new Exception("O campo \"inscEstadualCliente\" deve ter comprimento máximo de 50 caracter(es).");
////////
////////
////////                //Field observacoesCliente
////////                if (  fieldInfo.observacoesCliente != string.Empty ) 
////////                   if ( fieldInfo.observacoesCliente.Trim().Length > 300  )
////////                      throw new Exception("O campo \"observacoesCliente\" deve ter comprimento máximo de 300 caracter(es).");
////////
////////
////////                //Field tipoCliente
////////                if (  fieldInfo.tipoCliente != string.Empty ) 
////////                   if ( fieldInfo.tipoCliente.Trim().Length > 20  )
////////                      throw new Exception("O campo \"tipoCliente\" deve ter comprimento máximo de 20 caracter(es).");
////////
////////
////////                //Field statusCliente
////////                if (  fieldInfo.statusCliente != string.Empty ) 
////////                   if ( fieldInfo.statusCliente.Trim().Length > 2  )
////////                      throw new Exception("O campo \"statusCliente\" deve ter comprimento máximo de 2 caracter(es).");
////////                if ( ( fieldInfo.statusCliente == string.Empty ) || ( fieldInfo.statusCliente.Trim().Length < 1 ) )
////////                   throw new Exception("O campo \"statusCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
////////
////////
////////                //Field fkSubGrupoCliente
////////                if ( !( fieldInfo.fkSubGrupoCliente > 0 ) )
////////                   throw new Exception("O campo \"fkSubGrupoCliente\" deve ser maior que zero.");
////////
////////
////////                //Field numeroCasaCliente
////////                if (  fieldInfo.numeroCasaCliente != string.Empty ) 
////////                   if ( fieldInfo.numeroCasaCliente.Trim().Length > 30  )
////////                      throw new Exception("O campo \"numeroCasaCliente\" deve ter comprimento máximo de 30 caracter(es).");
////////
////////
////////                //Field faxCliente
////////                if (  fieldInfo.faxCliente != string.Empty ) 
////////                   if ( fieldInfo.faxCliente.Trim().Length > 50  )
////////                      throw new Exception("O campo \"faxCliente\" deve ter comprimento máximo de 50 caracter(es).");
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
////////
////////
////////
////////
////////
//////////Projeto substituído ------------------------
//////////using System;
//////////using System.Text;
//////////using System.Text.RegularExpressions;
//////////
//////////namespace SIMLgen
//////////{
//////////
//////////
//////////    /// <summary> 
//////////    /// Autor: DAL Creator .net  
//////////    /// Data de criação: 25/04/2013 15:38:03 
//////////    /// Descrição: Classe que valida o objeto "ClienteFields". 
//////////    /// </summary> 
//////////    public class ClienteValidator 
//////////    {
//////////
//////////
//////////        #region Propriedade que armazena erros de execução 
//////////        private string _ErrorMessage = string.Empty;
//////////        public string ErrorMessage { get { return _ErrorMessage; } }
//////////        #endregion
//////////
//////////
//////////        public ClienteValidator() {}
//////////
//////////
//////////        public bool isValid( ClienteFields fieldInfo )
//////////        {
//////////            try
//////////            {
//////////
//////////
//////////                //Field idCliente
//////////                if ( !( fieldInfo.idCliente > 0 ) )
//////////                   throw new Exception("O campo \"idCliente\" deve ser maior que zero.");
//////////
//////////
//////////                //Field nomeCliente
//////////                if (  fieldInfo.nomeCliente != string.Empty ) 
//////////                   if ( fieldInfo.nomeCliente.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"nomeCliente\" deve ter comprimento máximo de 255 caracter(es).");
//////////                if ( ( fieldInfo.nomeCliente == string.Empty ) || ( fieldInfo.nomeCliente.Trim().Length < 1 ) )
//////////                   throw new Exception("O campo \"nomeCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
//////////
//////////
//////////                //Field enderecoClienteA
//////////                if (  fieldInfo.enderecoClienteA != string.Empty ) 
//////////                   if ( fieldInfo.enderecoClienteA.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"enderecoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field enderecoClienteB
//////////                if (  fieldInfo.enderecoClienteB != string.Empty ) 
//////////                   if ( fieldInfo.enderecoClienteB.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"enderecoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field enderecoClienteC
//////////                if (  fieldInfo.enderecoClienteC != string.Empty ) 
//////////                   if ( fieldInfo.enderecoClienteC.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"enderecoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field bairroClienteA
//////////                if (  fieldInfo.bairroClienteA != string.Empty ) 
//////////                   if ( fieldInfo.bairroClienteA.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"bairroClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field bairroClienteB
//////////                if (  fieldInfo.bairroClienteB != string.Empty ) 
//////////                   if ( fieldInfo.bairroClienteB.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"bairroClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field bairroClientec
//////////                if (  fieldInfo.bairroClientec != string.Empty ) 
//////////                   if ( fieldInfo.bairroClientec.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"bairroClientec\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field cidadeClienteA
//////////                if (  fieldInfo.cidadeClienteA != string.Empty ) 
//////////                   if ( fieldInfo.cidadeClienteA.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"cidadeClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field cidadeClienteB
//////////                if (  fieldInfo.cidadeClienteB != string.Empty ) 
//////////                   if ( fieldInfo.cidadeClienteB.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"cidadeClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field cidadeClienteC
//////////                if (  fieldInfo.cidadeClienteC != string.Empty ) 
//////////                   if ( fieldInfo.cidadeClienteC.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"cidadeClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field estadoClienteA
//////////                if (  fieldInfo.estadoClienteA != string.Empty ) 
//////////                   if ( fieldInfo.estadoClienteA.Trim().Length > 2  )
//////////                      throw new Exception("O campo \"estadoClienteA\" deve ter comprimento máximo de 2 caracter(es).");
//////////
//////////
//////////                //Field estadoClienteB
//////////                if (  fieldInfo.estadoClienteB != string.Empty ) 
//////////                   if ( fieldInfo.estadoClienteB.Trim().Length > 2  )
//////////                      throw new Exception("O campo \"estadoClienteB\" deve ter comprimento máximo de 2 caracter(es).");
//////////
//////////
//////////                //Field estadoClienteC
//////////                if (  fieldInfo.estadoClienteC != string.Empty ) 
//////////                   if ( fieldInfo.estadoClienteC.Trim().Length > 2  )
//////////                      throw new Exception("O campo \"estadoClienteC\" deve ter comprimento máximo de 2 caracter(es).");
//////////
//////////
//////////                //Field cepClienteA
//////////                if (  fieldInfo.cepClienteA != string.Empty ) 
//////////                   if ( fieldInfo.cepClienteA.Trim().Length > 9  )
//////////                      throw new Exception("O campo \"cepClienteA\" deve ter comprimento máximo de 9 caracter(es).");
//////////
//////////
//////////                //Field cepClienteB
//////////                if (  fieldInfo.cepClienteB != string.Empty ) 
//////////                   if ( fieldInfo.cepClienteB.Trim().Length > 9  )
//////////                      throw new Exception("O campo \"cepClienteB\" deve ter comprimento máximo de 9 caracter(es).");
//////////
//////////
//////////                //Field cepClienteC
//////////                if (  fieldInfo.cepClienteC != string.Empty ) 
//////////                   if ( fieldInfo.cepClienteC.Trim().Length > 9  )
//////////                      throw new Exception("O campo \"cepClienteC\" deve ter comprimento máximo de 9 caracter(es).");
//////////
//////////
//////////                //Field telefoneClienteA
//////////                if (  fieldInfo.telefoneClienteA != string.Empty ) 
//////////                   if ( fieldInfo.telefoneClienteA.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"telefoneClienteA\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field telefoneClienteB
//////////                if (  fieldInfo.telefoneClienteB != string.Empty ) 
//////////                   if ( fieldInfo.telefoneClienteB.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"telefoneClienteB\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field telefoneClienteC
//////////                if (  fieldInfo.telefoneClienteC != string.Empty ) 
//////////                   if ( fieldInfo.telefoneClienteC.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"telefoneClienteC\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field telefoneClienteD
//////////                if (  fieldInfo.telefoneClienteD != string.Empty ) 
//////////                   if ( fieldInfo.telefoneClienteD.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"telefoneClienteD\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field celularClienteA
//////////                if (  fieldInfo.celularClienteA != string.Empty ) 
//////////                   if ( fieldInfo.celularClienteA.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"celularClienteA\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field celularClienteB
//////////                if (  fieldInfo.celularClienteB != string.Empty ) 
//////////                   if ( fieldInfo.celularClienteB.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"celularClienteB\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field celularClienteC
//////////                if (  fieldInfo.celularClienteC != string.Empty ) 
//////////                   if ( fieldInfo.celularClienteC.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"celularClienteC\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field complementoCliente
//////////                if (  fieldInfo.complementoCliente != string.Empty ) 
//////////                   if ( fieldInfo.complementoCliente.Trim().Length > 100  )
//////////                      throw new Exception("O campo \"complementoCliente\" deve ter comprimento máximo de 100 caracter(es).");
//////////
//////////
//////////                //Field emailClienteA
//////////                if (  fieldInfo.emailClienteA != string.Empty ) 
//////////                   if ( fieldInfo.emailClienteA.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"emailClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field emailClienteB
//////////                if (  fieldInfo.emailClienteB != string.Empty ) 
//////////                   if ( fieldInfo.emailClienteB.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"emailClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field contatoClienteA
//////////                if (  fieldInfo.contatoClienteA != string.Empty ) 
//////////                   if ( fieldInfo.contatoClienteA.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"contatoClienteA\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field contatoClienteB
//////////                if (  fieldInfo.contatoClienteB != string.Empty ) 
//////////                   if ( fieldInfo.contatoClienteB.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"contatoClienteB\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field contatoClienteC
//////////                if (  fieldInfo.contatoClienteC != string.Empty ) 
//////////                   if ( fieldInfo.contatoClienteC.Trim().Length > 255  )
//////////                      throw new Exception("O campo \"contatoClienteC\" deve ter comprimento máximo de 255 caracter(es).");
//////////
//////////
//////////                //Field cnpjCliente
//////////                if (  fieldInfo.cnpjCliente != string.Empty ) 
//////////                   if ( fieldInfo.cnpjCliente.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"cnpjCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field cpfCliente
//////////                if (  fieldInfo.cpfCliente != string.Empty ) 
//////////                   if ( fieldInfo.cpfCliente.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"cpfCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field rgCliente
//////////                if (  fieldInfo.rgCliente != string.Empty ) 
//////////                   if ( fieldInfo.rgCliente.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"rgCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field inscEstadualCliente
//////////                if (  fieldInfo.inscEstadualCliente != string.Empty ) 
//////////                   if ( fieldInfo.inscEstadualCliente.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"inscEstadualCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////
//////////                //Field observacoesCliente
//////////                if (  fieldInfo.observacoesCliente != string.Empty ) 
//////////                   if ( fieldInfo.observacoesCliente.Trim().Length > 300  )
//////////                      throw new Exception("O campo \"observacoesCliente\" deve ter comprimento máximo de 300 caracter(es).");
//////////
//////////
//////////                //Field tipoCliente
//////////                if (  fieldInfo.tipoCliente != string.Empty ) 
//////////                   if ( fieldInfo.tipoCliente.Trim().Length > 20  )
//////////                      throw new Exception("O campo \"tipoCliente\" deve ter comprimento máximo de 20 caracter(es).");
//////////
//////////
//////////                //Field statusCliente
//////////                if (  fieldInfo.statusCliente != string.Empty ) 
//////////                   if ( fieldInfo.statusCliente.Trim().Length > 2  )
//////////                      throw new Exception("O campo \"statusCliente\" deve ter comprimento máximo de 2 caracter(es).");
//////////                if ( ( fieldInfo.statusCliente == string.Empty ) || ( fieldInfo.statusCliente.Trim().Length < 1 ) )
//////////                   throw new Exception("O campo \"statusCliente\" não pode ser nulo ou vazio e deve ter comprimento mínimo de 1 caracter(es).");
//////////
//////////
//////////                //Field fkSubGrupoCliente
//////////                if ( !( fieldInfo.fkSubGrupoCliente > 0 ) )
//////////                   throw new Exception("O campo \"fkSubGrupoCliente\" deve ser maior que zero.");
//////////
//////////
//////////                //Field numeroCasaCliente
//////////                if (  fieldInfo.numeroCasaCliente != string.Empty ) 
//////////                   if ( fieldInfo.numeroCasaCliente.Trim().Length > 30  )
//////////                      throw new Exception("O campo \"numeroCasaCliente\" deve ter comprimento máximo de 30 caracter(es).");
//////////
//////////
//////////                //Field faxCliente
//////////                if (  fieldInfo.faxCliente != string.Empty ) 
//////////                   if ( fieldInfo.faxCliente.Trim().Length > 50  )
//////////                      throw new Exception("O campo \"faxCliente\" deve ter comprimento máximo de 50 caracter(es).");
//////////
//////////                return true;
//////////
//////////            }
//////////            catch (Exception e)
//////////            {
//////////                this._ErrorMessage = e.Message;
//////////                return false;
//////////            }
//////////
//////////        }
//////////    }
//////////
//////////}
//////////
