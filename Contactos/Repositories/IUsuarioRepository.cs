using Contactos.Models;

namespace Contactos.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario> ObtenerUsuarioAsync(int UsuarioPK);
        Task<Usuario> CrearUsuarioAsync(Usuario usuario);
        Task ActualizarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(int UsuarioPK);
    }
}
