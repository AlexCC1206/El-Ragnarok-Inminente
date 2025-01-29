using System;

public class JusticiaImplacable : Habilidad
{
    private int turnosActivos = 0;
    public JusticiaImplacable() : base("Justicia Implacable", 3)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            ficha.VelocidadBase = ficha.Velocidad;
            ficha.Velocidad += 2;
            turnosActivos = 2;
            Console.WriteLine($"[yellow]{ficha.Nombre} usa {Nombre} y aumenta su velocidad en 2 casillas por turno durante los próximos {turnosActivos} turnos.[/]");
        }
        else
        {
            Console.WriteLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
        }
        ficha.ReducirEnfriamientoHabilidades();
        ReducirTurnosActivos(ficha);
    }

    public void ReducirTurnosActivos(Ficha ficha)
    {
        if (turnosActivos > 0)
        {
            turnosActivos--;
            if (turnosActivos == 0)
            {
                ficha.Velocidad = ficha.VelocidadBase; // Restaura la velocidad base
                Console.WriteLine($"{ficha.Nombre} ya no tiene la habilidad {Nombre} activa.");
            }
        }
    }
}