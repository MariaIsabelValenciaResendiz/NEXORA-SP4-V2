using System.Net.Http.Headers;
using System.Text;

namespace NEXORA.API.Contracts;

public static class AutorizacionBasica
{
    public static (string Nombre, string Contrasena)? Leer(string? cabecera)
    {
        if (cabecera is null || cabecera.Length > 4096
            || !AuthenticationHeaderValue.TryParse(cabecera, out var autorizacion)
            || !string.Equals(
                autorizacion.Scheme,
                "Basic",
                StringComparison.OrdinalIgnoreCase)
            || string.IsNullOrWhiteSpace(autorizacion.Parameter))
        {
            return null;
        }

        try
        {
            var bytes = Convert.FromBase64String(autorizacion.Parameter);
            var texto = new UTF8Encoding(false, true).GetString(bytes);
            var separador = texto.IndexOf(':');

            return separador <= 0
                ? null
                : (texto[..separador], texto[(separador + 1)..]);
        }
        catch (FormatException)
        {
            return null;
        }
        catch (DecoderFallbackException)
        {
            return null;
        }
    }
}