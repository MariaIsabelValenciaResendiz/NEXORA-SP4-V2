using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;
using NEXORA.Infrastructure.Persistence;

namespace NEXORA.Infrastructure.Repositories;

public class UsuarioRepositoryMemoria : IUsuarioRepository
{
    public Usuario? ObtenerPorCredenciales(string nombre, string contrasena)
    {
        return UsuariosDatosCrudos.Todos.FirstOrDefault(u =>
            string.Equals(u.Nombre, nombre, StringComparison.OrdinalIgnoreCase)
            && u.Contrasena == contrasena);
    }
}