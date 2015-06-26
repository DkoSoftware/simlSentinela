using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIML.Sentnela
{


    /// <summary> 
    /// Tabela: SubGrupoCliente  
    /// Autor: DAL Creator .net  
    /// Data de criação: 02/05/2013 19:05:56 
    /// Descrição: Classe que retorna todos os campos/propriedades da tabela SubGrupoCliente 
    /// </summary> 
    public class SubGrupoClienteFields 
    {

        private int _idSubGrupoCliente = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): Int 
        /// Somente Leitura/Auto Incremental
        /// </summary> 
        public int idSubGrupoCliente
        {
            get { return _idSubGrupoCliente; }
            set { _idSubGrupoCliente = value; }
        }

        private string _descricaoSubGrupoCliente = string.Empty;


        /// <summary>  
        /// Tipo de dados (DataBase): nvarchar 
        /// Preenchimento obrigatório:  Sim 
        /// Estilo: Normal  
        /// Tamanho Máximo: 150 
        /// </summary> 
        public string descricaoSubGrupoCliente
        {
            get { return _descricaoSubGrupoCliente.Trim(); }
            set { _descricaoSubGrupoCliente = value.Trim(); }
        }

        private int _fkGrupoCliente = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): int 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public int fkGrupoCliente
        {
            get { return _fkGrupoCliente; }
            set { _fkGrupoCliente = value; }
        }

        private decimal _valorIndiceInicial = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): decimal 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public decimal valorIndiceInicial
        {
            get { return _valorIndiceInicial; }
            set { _valorIndiceInicial = value; }
        }

        private decimal _valorIndiceFinal = 0;


        /// <summary>  
        /// Tipo de dados (DataBase): decimal 
        /// Preenchimento obrigatório:  Não 
        /// Permitido:  Maior que zero 
        /// </summary> 
        public decimal valorIndiceFinal
        {
            get { return _valorIndiceFinal; }
            set { _valorIndiceFinal = value; }
        }



        public SubGrupoClienteFields() {}

        public SubGrupoClienteFields(
                        string Param_descricaoSubGrupoCliente, 
                        int Param_fkGrupoCliente, 
                        decimal Param_valorIndiceInicial, 
                        decimal Param_valorIndiceFinal)
        {
               this._descricaoSubGrupoCliente = Param_descricaoSubGrupoCliente;
               this._fkGrupoCliente = Param_fkGrupoCliente;
               this._valorIndiceInicial = Param_valorIndiceInicial;
               this._valorIndiceFinal = Param_valorIndiceFinal;
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
//    /// Tabela: SubGrupoCliente  
//    /// Autor: DAL Creator .net  
//    /// Data de criação: 25/04/2013 16:44:06 
//    /// Descrição: Classe que retorna todos os campos/propriedades da tabela SubGrupoCliente 
//    /// </summary> 
//    public class SubGrupoClienteFields 
//    {
//
//        private int _idSubGrupoCliente = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): Int 
//        /// Somente Leitura/Auto Incremental
//        /// </summary> 
//        public int idSubGrupoCliente
//        {
//            get { return _idSubGrupoCliente; }
//            set { _idSubGrupoCliente = value; }
//        }
//
//        private string _descricaoSubGrupoCliente = string.Empty;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): nvarchar 
//        /// Preenchimento obrigatório:  Sim 
//        /// Estilo: Normal  
//        /// Tamanho Máximo: 150 
//        /// </summary> 
//        public string descricaoSubGrupoCliente
//        {
//            get { return _descricaoSubGrupoCliente.Trim(); }
//            set { _descricaoSubGrupoCliente = value.Trim(); }
//        }
//
//        private int _fkGrupoCliente = 0;
//
//
//        /// <summary>  
//        /// Tipo de dados (DataBase): int 
//        /// Preenchimento obrigatório:  Não 
//        /// Permitido:  Maior que zero 
//        /// </summary> 
//        public int fkGrupoCliente
//        {
//            get { return _fkGrupoCliente; }
//            set { _fkGrupoCliente = value; }
//        }
//
//
//
//        public SubGrupoClienteFields() {}
//
//        public SubGrupoClienteFields(
//                        string Param_descricaoSubGrupoCliente, 
//                        int Param_fkGrupoCliente)
//        {
//               this._descricaoSubGrupoCliente = Param_descricaoSubGrupoCliente;
//               this._fkGrupoCliente = Param_fkGrupoCliente;
//        }
//    }
//
//}
