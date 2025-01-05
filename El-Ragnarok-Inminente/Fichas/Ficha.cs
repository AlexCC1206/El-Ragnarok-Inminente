using System;

public abstract class Ficha
{
    public string Nombre { get; set; }
    public int Velocidad { get; set; }
    public string Habilidad { get; set; }
    public int Enfriamiento { get; set; }
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }

    public Ficha(string nombre, int velocidad, string habilidad, int enfriamiento)
    {
        Nombre = nombre;
        Velocidad = velocidad;
        Habilidad = habilidad;
        Enfriamiento = enfriamiento;
    }

    public abstract void UsarHabilidad();
}