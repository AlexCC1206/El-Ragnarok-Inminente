using System;

public class JusticiaImplacable : Habilidad
{
    public JusticiaImplacable() : base("Justicia Implacable", 3)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            ficha.Velocidad += 2;
            Console.WriteLine($"{ficha.Nombre} usa {Nombre} y aumenta su velocidad en 2 casillas por turno durante los próximos 2 turnos.");
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }
}