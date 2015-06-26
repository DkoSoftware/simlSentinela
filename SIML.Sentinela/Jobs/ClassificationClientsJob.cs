using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.IO;
using SIML.Sentinela;
using SIML.Sentnela;

namespace SIMLSentinela.Jobs
{
    internal class ClassificationClientsJob
    {
        #region [ Threads ]

        public void ExecuteJob()
        {
            //TODO create method for callback service
            SortClientsByGroup(DateTime.Now.AddMonths(-6), DateTime.Now);
        }

        private List<PedidoFields> GetListPedidosByPeriodBuys(DateTime startDate, DateTime finalDate)
        {
            var listPedidos = (from p in new PedidoControl().GetAll()
                               where p.dtPedido >= startDate && p.dtPedido <= finalDate
                               && p.situacaoPedido.ToUpper().Equals("FINALIZADO")
                               group p by p.fkCliente into gp
                               select new PedidoFields()
                               {
                                   fkCliente = gp.Key,
                                   valorTotalPedido = gp.Sum(x => x.valorTotalPedido)
                               });

            var listPedidosFinal = from c in new ClienteControl().GetAll()
                                   join p in listPedidos on c.idCliente equals p.fkCliente into result
                                   from t in result.DefaultIfEmpty()
                                   select new PedidoFields()
                                   {
                                      fkCliente = t == null ? c.idCliente : t.fkCliente,
                                      valorTotalPedido = t == null ? 0 : t.valorTotalPedido
                                   };

            return listPedidosFinal.ToList();
        }

        private bool ChangeGroupOfClients(List<PedidoFields> listPedidos)
        {
            try
            {
                foreach (PedidoFields itemListPedido in listPedidos)
                {
                    bool updated = false;
                    ClienteFields cliente = new ClienteControl().GetItem(itemListPedido.fkCliente);

                    var grupoCliente = new GrupoClienteControl().GetItem(new SubGrupoClienteControl().GetItem(cliente.fkSubGrupoCliente).fkGrupoCliente);
                    var listSubGrupos = from sbc in new SubGrupoClienteControl().GetAll()
                                        where sbc.fkGrupoCliente == grupoCliente.idGrupoCliente
                                        select sbc;

                    foreach (SubGrupoClienteFields itemSubGrupo in listSubGrupos)
                    {
                        if (itemListPedido.valorTotalPedido == 0)
                            break;

                        if (itemListPedido.valorTotalPedido >= itemSubGrupo.valorIndiceInicial &&
                            (itemListPedido.valorTotalPedido <= itemSubGrupo.valorIndiceFinal || itemSubGrupo.valorIndiceFinal == 0))
                        {
                            cliente.fkSubGrupoCliente = itemSubGrupo.idSubGrupoCliente;
                            new ClienteControl().Update(cliente);
                            updated = true;
                            break;
                        }
                    }

                    if (!updated)
                    {
                        //set inative group for this client
                        cliente.fkSubGrupoCliente = (from s in listSubGrupos
                                                     where (s.valorIndiceInicial == 0 && s.valorIndiceFinal == 0)
                                                     select s).FirstOrDefault().idSubGrupoCliente;

                        new ClienteControl().Update(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public void SortClientsByGroup(DateTime startDate, DateTime finalDate)
        {
            var listPedidos = GetListPedidosByPeriodBuys(startDate, finalDate);
            if (listPedidos.Count > 0)
                ChangeGroupOfClients(listPedidos);
        }

        #endregion
    }
}
