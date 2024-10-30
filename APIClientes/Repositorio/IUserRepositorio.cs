using APIClientes.Modelos;

namespace APIClientes.Repositorio
{
    public interface IUserRepositorio
    {
        Task<int> Register(User user, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExiste(string userName);
    }
}
