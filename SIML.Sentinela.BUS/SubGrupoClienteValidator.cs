using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SIML.Sentnela
{


    /// <summary> 
    /// Autor: DAL Creator .net  
    /// Data de cria��o: 02/05/2013 19:05:56 
    /// Descri��o: Classe que valida o objeto "SubGrupoClienteFields". 
    /// </summary> 
    public class SubGrupoClienteValidator 
    {


        #region Propriedade que armazena erros de execu��o 
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage { get { return _ErrorMessage; } }
        #endregion


        public SubGrupoClienteValidator() {}


        public bool isValid( SubGrupoClienteFields fieldInfo )
        {
            try
            {


                //Field descricaoSubGrupoCliente
                if (  fieldInfo.descricaoSubGrupoCliente != string.Empty ) 
                   if ( fieldInfo.descricaoSubGrupoCliente.Trim().Length > 150  )
                      throw new Exception("O campo \"descricaoSubGrupoCliente\" deve ter comprimento m�ximo de 150 caracter(es).");
                if ( ( fieldInfo.descricaoSubGrupoCliente == string.Empty ) || ( fieldInfo.descricaoSubGrupoCliente.Trim().Length < 1 ) )
                   throw new Exception("O campo \"descricaoSubGrupoCliente\" n�o pode ser nulo ou vazio e deve ter comprimento m�nimo de 1 caracter(es).");


                //Field fkGrupoCliente
                if ( !( fieldInfo.fkGrupoCliente > 0 ) )
                   throw new Exception("O campo \"fkGrupoCliente\" deve ser maior que zero.");


                //Field valorIndiceInicial
                if ( !( fieldInfo.valorIndiceInicial > 0 ) )
                   throw new Exception("O campo \"valorIndiceInicial\" deve ser maior que zero.");


                //Field valorIndiceFinal
                if ( !( fieldInfo.valorIndiceFinal > 0 ) )
                   throw new Exception("O campo \"valorIndiceFinal\" deve ser maior que zero.");

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






//Projeto substitu�do ------------------------
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
//    /// Data de cria��o: 25/04/2013 16:44:06 
//    /// Descri��o: Classe que valida o objeto "SubGrupoClienteFields". 
//    /// </summary> 
//    public class SubGrupoClienteValidator 
//    {
//
//
//        #region Propriedade que armazena erros de execu��o 
//        private string _ErrorMessage = string.Empty;
//        public string ErrorMessage { get { return _ErrorMessage; } }
//        #endregion
//
//
//        public SubGrupoClienteValidator() {}
//
//
//        public bool isValid( SubGrupoClienteFields fieldInfo )
//        {
//            try
//            {
//
//
//                //Field descricaoSubGrupoCliente
//                if (  fieldInfo.descricaoSubGrupoCliente != string.Empty ) 
//                   if ( fieldInfo.descricaoSubGrupoCliente.Trim().Length > 150  )
//                      throw new Exception("O campo \"descricaoSubGrupoCliente\" deve ter comprimento m�ximo de 150 caracter(es).");
//                if ( ( fieldInfo.descricaoSubGrupoCliente == string.Empty ) || ( fieldInfo.descricaoSubGrupoCliente.Trim().Length < 1 ) )
//                   throw new Exception("O campo \"descricaoSubGrupoCliente\" n�o pode ser nulo ou vazio e deve ter comprimento m�nimo de 1 caracter(es).");
//
//
//                //Field fkGrupoCliente
//                if ( !( fieldInfo.fkGrupoCliente > 0 ) )
//                   throw new Exception("O campo \"fkGrupoCliente\" deve ser maior que zero.");
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
