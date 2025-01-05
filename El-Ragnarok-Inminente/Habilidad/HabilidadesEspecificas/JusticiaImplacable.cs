using System;

public class JusticiaImplacable : Habilidad
{
    public JusticiaImplacable() : base("Justicia Implacable", 3)
    {
    }

    public override void Usar(Ficha ficha, Ficha objetivo)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            objetivo.Velocidad = 1; // Reduce la velocidad de la ficha objetivo a 1
            objetivo.Paralizado = true; // Marca a la ficha objetivo como inmovilizada
            Console.WriteLine($"{ficha.Nombre} usa {Nombre} y reduce la velocidad de {objetivo.Nombre} a 1 casilla por turno durante los próximos 2 turnos.");
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }
}