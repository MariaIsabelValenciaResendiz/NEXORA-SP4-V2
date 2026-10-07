using System.Net.Http.Headers;
using System.Text;
using NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

namespace NEXORA.API.Contracts.Historial;

public static class AutorizacionBasica
{
    public static ConsultarHistorialGlobalQuery? Leer(string? cabecera)
    {
        if (cabecera is null || cabecera.Length > 4096
            || !AuthenticationHeaderValue.TryParse(cabecera, out var autorizacion)
            || !string.Equals(autorizacion.Scheme, "Basic", StringComparison.OrdinalIgnoreCase)
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
                : new ConsultarHistorialGlobalQuery(texto[..separador], texto[(separador + 1)..]);
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