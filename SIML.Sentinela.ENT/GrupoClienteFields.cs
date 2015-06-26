using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIML.Sentnela
{


    /// <summary> 
    /// Tabela: GrupoCliente  
    /// Autor: DAL Creator .net  
    /// Data de cria��o: 14/10/2012 15:48:52 
    /// Descri��o: Classe que retorna todos os campos/propriedades da tabela GrupoCliente 
    /// </summary> 
    public class GrupoClienteFields 
    {

        private int _idGrupoCliente = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): Int 
        /// Somente Leitura/Auto Incremental
        /// </summary> 
        public int idGrupoCliente
        {
            get { return _idGrupoCliente; }
            set { _idGrupoCliente = value; }
        }

        private string _descricaoGrupoCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigat�rio:  Sim 
        /// Estilo: Normal  
        /// Tamanho M�ximo: 50 
        /// </summary> 
        public string descricaoGrupoCliente
        {
            get { return _descricaoGrupoCliente.Trim(); }
            set { _descricaoGrupoCliente = value.Trim(); }
        }

        private string _TipoClienteGrupoCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): char 
        /// Preenchimento obrigat�rio:  N�o 
        /// Estilo: Normal  
        /// Tamanho M�ximo: 2 
        /// </summary> 
        public string TipoClienteGrupoCliente
        {
            get { return _TipoClienteGrupoCliente.Trim(); }
            set { _TipoClienteGrupoCliente = value.Trim(); }
        }



        public GrupoClienteFields() {}

        public GrupoClienteFields(
                        string Param_descricaoGrupoCliente, 
                        string Param_TipoClienteGrupoCliente)
        {
               this._descricaoGrupoCliente = Param_descricaoGrupoCliente;
               this._TipoClienteGrupoCliente = Param_TipoClienteGrupoCliente;
        }
    }

}
