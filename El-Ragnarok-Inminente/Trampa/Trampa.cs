using System;

public abstract class Trampa
{
    public string Nombre { get; set; }
    public string Simbolo { get; set; }
    public string Descripcion { get; set; }
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }

    public Trampa(string nombre, string descripcion, string simbolo)
    {
        Nombre = nombre;
        Simbolo = simbolo;
        Descripcion = descripcion;
    }

    public abstract void Activar(Ficha ficha);
}