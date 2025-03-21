namespace TortilleriaSucursales.Dtos
{
    public class SucursalDTO
    {
        public int Id_Sucursal { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono_Contacto { get; set; } = string.Empty;
        public string Nombre_Encargado { get; set; } = string.Empty;
    }
}
