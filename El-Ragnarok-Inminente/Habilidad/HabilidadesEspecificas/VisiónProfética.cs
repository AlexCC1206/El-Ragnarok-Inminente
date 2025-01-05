using System;

public class VisionProfetica : Habilidad
{
    public VisionProfetica() : base("Visión Profética", 4)
    {
    }

    public override void Usar(Ficha ficha, Ficha objetivo)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            MoverFicha(ficha);
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }

    private void MoverFicha(Ficha ficha)
    {
        // Aumenta la velocidad de la ficha en 1
        ficha.Velocidad += 1;
        Console.WriteLine($"{ficha.Nombre} aumenta su velocidad a {ficha.Velocidad}.");
    }
}