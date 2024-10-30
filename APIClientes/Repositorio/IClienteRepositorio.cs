using APIClientes.Modelos.DTO;

namespace APIClientes.Repositorio
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteDTO>> GetClientes();
        Task<ClienteDTO> GetClienteById(int id);
        Task<ClienteDTO> CreateUpdate(ClienteDTO cliente);
        Task<bool> DeleteCliente(int id);
    }
}
