using Contactos.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Contactos.Repositories
{
    public class UsuarioRepository:IUsuarioRepository
    {
        //private readonly IConfiguration _config;
        //private readonly string _connectionString;
        private readonly IDbConnection _db;
        public UsuarioRepository(IConfiguration config)
        {
            //_config = config;
            //_connectionString = _config.GetConnectionString("Default")!;
            _db = new MySqlConnection(config.GetConnectionString("Default"));
        }

        private IDbConnection GetConnection()
        {
            return _db;
        
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            var connection = GetConnection();
            string sql = "select UsuarioPK,Rut,Nombre,Edad from usuario";
            var usuarios= await connection.QueryAsync<Usuario>(sql);
            return usuarios;
        }
        public async Task<Usuario> ObtenerUsuarioAsync(int UsuarioPK)
        {
            var connection = GetConnection();
            string sql = "select UsuarioPK, Rut, Nombre, Edad from usuario where UsuarioPK=@UsuarioPK";
            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { UsuarioPK });
            return usuario;
        }

        public async Task<Usuario> CrearUsuarioAsync(Usuario usuario)
        {
            var connection = GetConnection();
            string query = @"insert into usuario(Rut, Nombre, Edad) values (@Rut, @Nombre, @Edad);
                            select Last_Insert_Id()";
            int idCreado=await connection.ExecuteScalarAsync<int>(query, new {usuario.Rut, usuario.Nombre, usuario.Edad});
            usuario.UsuarioPK= idCreado;
            return usuario;
        }

        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            var connection = GetConnection();
            string query = @"update usuario set Rut=@Rut, Nombre=@Nombre, Edad=@Edad where UsuarioPK=@UsuarioPK";
            await connection.ExecuteAsync(query, usuario);
        }

        public async Task EliminarUsuarioAsync(int UsuarioPK)
        {
            var connection = GetConnection();
            string query = @"delete from usuario where UsuarioPK=@UsuarioPK";
            await connection.ExecuteAsync(query, new {UsuarioPK});
        }
    }
}
