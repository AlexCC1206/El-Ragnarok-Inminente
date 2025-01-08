using System;

public abstract class Habilidad
{
    public string Nombre { get; set; }
    public int Enfriamiento { get; set; }
    public int TurnosRestantes { get; set; }

    public Habilidad(string nombre, int enfriamiento)
    {
        Nombre = nombre;
        Enfriamiento = enfriamiento;
        TurnosRestantes = 0;
    }

    public abstract void Usar(Ficha ficha);

    public void ReducirEnfriamiento()
    {
        if (TurnosRestantes > 0)
        {
            TurnosRestantes--;
        }
    }

    public bool EstaDisponible()
    {
        return TurnosRestantes == 0;
    }
}