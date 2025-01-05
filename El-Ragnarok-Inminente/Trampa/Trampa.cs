using System;

public abstract class Trampa
{
    public string Nombre { get; set; }
    public char Simbolo { get; set; }
    public string Descripcion { get; set; }

    public Trampa(string nombre, string descripcion, char simbolo)
    {
        Nombre = nombre;
        Simbolo = simbolo;
        Descripcion = descripcion;
    }

    public abstract void Activar();
}