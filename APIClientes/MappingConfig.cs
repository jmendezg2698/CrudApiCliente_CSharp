using APIClientes.Modelos;
using APIClientes.Modelos.DTO;
using AutoMapper;

namespace APIClientes
{
    public class MappingConfig
    {
        public static MapperConfiguration ResgisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClienteDTO, Cliente>();
                config.CreateMap<Cliente, ClienteDTO>();
            });
            return mappingConfig;
        }

    }
}
