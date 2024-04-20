using CadastroClientes.Model;
using CadastroClientes.Repository;
using System.Windows.Forms;

namespace CadastroClientes.Services
{
    public class ClientesService
    {
        private readonly ClientesRepository _clientesRepository;

        public ClientesService(ClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        public void AdicionaCliente(ClientesModel cliente, TextBox txtId)
        {
            _clientesRepository.AdicionaCliente(cliente, txtId);
        }

        public void AtualizaCliente(ClientesModel cliente)
        {
            _clientesRepository.AtualizaCliente(cliente);
        }
    }
}
