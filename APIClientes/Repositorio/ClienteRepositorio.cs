using APIClientes.Data;
using APIClientes.Modelos;
using APIClientes.Modelos.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIClientes.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {

        private readonly ApplicationDBContext _db;
        private IMapper _mapper;
        public ClienteRepositorio(ApplicationDBContext dBContext, IMapper mapper)
        {
            _db = dBContext;
            _mapper = mapper;
        }
        public async Task<ClienteDTO> CreateUpdate(ClienteDTO clienteDTO)
        {
            Cliente cliente = _mapper.Map<ClienteDTO, Cliente>(clienteDTO);
            if(cliente.Id > 0)
            {
                _db.Clientes.Update(cliente);
            }
            else
            {
                await _db.Clientes.AddAsync(cliente);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Cliente, ClienteDTO>(cliente);
        }

        public async Task<bool> DeleteCliente(int id)
        {
            try
            {
                Cliente cliente = await _db.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return false;
                }
                _db.Clientes.Remove(cliente);
                await _db.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ClienteDTO> GetClienteById(int id)
        {
            Cliente cliente = await _db.Clientes.FindAsync(id);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<List<ClienteDTO>> GetClientes()
        {
            List<Cliente> lista = await _db.Clientes.ToListAsync();
            return _mapper.Map<List<ClienteDTO>>(lista);    
        }
    }
}
