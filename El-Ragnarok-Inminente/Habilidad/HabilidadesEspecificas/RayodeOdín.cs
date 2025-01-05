using System;

public class RayoDeOdin : Habilidad
{
    public RayoDeOdin() : base("Rayo de Odín", 3)
    {
    }

    public override void Usar(Ficha ficha, Ficha objetivo)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            objetivo.Paralizado = true; // Marca a la ficha objetivo como paralizada
            Console.WriteLine($"{ficha.Nombre} usa {Nombre} y paraliza a {objetivo.Nombre} durante un turno.");
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }
}