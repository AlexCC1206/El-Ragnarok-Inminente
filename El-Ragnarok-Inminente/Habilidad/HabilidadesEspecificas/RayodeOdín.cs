using System;

public class RayoDeOdin : Habilidad
{
    public RayoDeOdin() : base("Rayo de Odín", 3)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            ficha.Velocidad += 3; // Aumenta la velocidad en 3 puntos
            Console.WriteLine($"{ficha.Nombre} usa {Nombre} y aumenta su velocidad en 3 puntos durante 2 turnos.");
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }
}