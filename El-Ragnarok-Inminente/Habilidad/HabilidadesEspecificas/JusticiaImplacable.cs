using System;
using Spectre.Console;

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
            AnsiConsole.MarkupLine($"[yellow]{ficha.Nombre} usa {Nombre} y aumenta su velocidad en 2 casillas por turno durante los próximos {turnosActivos} turnos.[/]");
            Thread.Sleep(1000);
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
            Thread.Sleep(1000);
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
                AnsiConsole.WriteLine($"{ficha.Nombre} ya no tiene la habilidad {Nombre} activa.");
                Thread.Sleep(1000);
            }
        }
    }
}