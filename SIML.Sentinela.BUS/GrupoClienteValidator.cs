using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SIML.Sentnela
{


    /// <summary> 
    /// Autor: DAL Creator .net  
    /// Data de cria��o: 14/10/2012 15:48:52 
    /// Descri��o: Classe que valida o objeto "GrupoClienteFields". 
    /// </summary> 
    public class GrupoClienteValidator 
    {


        #region Propriedade que armazena erros de execu��o 
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage { get { return _ErrorMessage; } }
        #endregion


        public GrupoClienteValidator() {}


        public bool isValid( GrupoClienteFields fieldInfo )
        {
            try
            {


                //Field descricaoGrupoCliente
                if (  fieldInfo.descricaoGrupoCliente != string.Empty ) 
                   if ( fieldInfo.descricaoGrupoCliente.Trim().Length > 50  )
                      throw new Exception("O campo \"descricaoGrupoCliente\" deve ter comprimento m�ximo de 50 caracter(es).");
                if ( ( fieldInfo.descricaoGrupoCliente == string.Empty ) || ( fieldInfo.descricaoGrupoCliente.Trim().Length < 1 ) )
                   throw new Exception("O campo \"descricaoGrupoCliente\" n�o pode ser nulo ou vazio e deve ter comprimento m�nimo de 1 caracter(es).");


                //Field TipoClienteGrupoCliente
                if (  fieldInfo.TipoClienteGrupoCliente != string.Empty ) 
                   if ( fieldInfo.TipoClienteGrupoCliente.Trim().Length > 2  )
                      throw new Exception("O campo \"TipoClienteGrupoCliente\" deve ter comprimento m�ximo de 2 caracter(es).");

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

