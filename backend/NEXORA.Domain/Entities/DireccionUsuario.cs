namespace NEXORA.Domain.Entities;

public class DireccionUsuario
{
    public string Ciudad { get; set; } = string.Empty;
    public string Calle { get; set; } = string.Empty;
    public int Numero { get; set; }
    public string CodigoPostal { get; set; } = string.Empty;
    public CoordenadasUsuario Geolocalizacion { get; set; } = new();
}