using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DAL
{
    public class ClienteDAL : IAcessoDB<Cliente>
    {
        private CadastroEntities cadastroEntities;

        public bool AtualizarRegistro(Cliente registro)
        {
            EntityKey key;
            object originalItem;

            using (cadastroEntities = new CadastroEntities()) 
            {
                 var resultado = cadastroEntities.Clientes.SingleOrDefault(c=>c.id == registro.id);
                  if (resultado != null)
                  {
                      cadastroEntities.Entry(resultado).CurrentValues.SetValues(registro);
                      cadastroEntities.SaveChanges();
                  }
                //cadastroEntities.Books.AddOrUpdate(book); //requires using System.Data.Entity.Migrations;
                //cadastroEntities.SaveChanges();
            }

            return true;
        }

        public bool DeletarRegistro(Cliente registro)
        {
            using(cadastroEntities = new CadastroEntities())
            {
                var cliente = cadastroEntities.Clientes.FirstOrDefault(c => c.id == registro.id);
                cadastroEntities.Clientes.Remove(cliente);
                cadastroEntities.SaveChanges();
                return true;

                /*
                    Book  deptBook  = new Book  { Id  = bookId };  
                    Context.Entry(deptBook).State = EntityState.Deleted;  
                    Context.SaveChanges();  
                 */
            }
        }

        /// <summary>
        /// GetRegistro - Obtem uma lista de clientes de acordo com o criterio de filtro
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        public List<Cliente> ObterRegistro(Cliente registro)
        {
            cadastroEntities = new CadastroEntities();
            IQueryable<Cliente> consultaCliente = cadastroEntities.Clientes.AsQueryable<Cliente>();
            if (registro.id > 0)
            {
                consultaCliente = consultaCliente.Where(x => x.id == registro.id);
            }
            if (!string.IsNullOrEmpty(registro.nome))
            {
                consultaCliente = consultaCliente.Where(x => x.nome == registro.nome);
            }
            if (!string.IsNullOrEmpty(registro.email1))
            {
                consultaCliente = consultaCliente.Where(x => x.email1 == registro.email1);
            }
            if (!string.IsNullOrEmpty(registro.email2))
            {
                consultaCliente = consultaCliente.Where(x => x.email2 == registro.email2);
            }
            if (!string.IsNullOrEmpty(registro.obs))
            {
                consultaCliente = consultaCliente.Where(x => x.obs == registro.obs);
            }
            return consultaCliente.ToList();
        }

        public Cliente ObterRegistroPorCodigo(int codigo)
        {
            cadastroEntities = new CadastroEntities();
            Cliente clienteRetorno = cadastroEntities.Clientes.FirstOrDefault(x => x.id == codigo);
            return clienteRetorno;
        }

        public List<Cliente> ObterTodos()
        {
            cadastroEntities = new CadastroEntities();
            IQueryable<Cliente> consultaClientes = cadastroEntities.Clientes.AsQueryable<Cliente>();
            return consultaClientes.ToList();
        }

        public int SalvarRegistro(Cliente registro)
        {
            using (cadastroEntities = new CadastroEntities())
            {
                cadastroEntities.Clientes.Add(registro);
                cadastroEntities.SaveChanges();
            }
            return registro.id;
        }
    }
}
