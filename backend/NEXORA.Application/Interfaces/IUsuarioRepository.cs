using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces;

public interface IUsuarioRepository
{
    Usuario? ObtenerPorCredenciales(string nombre, string contrasena);
}