using System;
using Spectre.Console;

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
            ficha.Velocidad = ficha.VelocidadBase; // Restaurar velocidad base
            ficha.Paralizado = false;          // Quitar parálisis si existe
            AnsiConsole.MarkupLine($"[yellow]{ficha.Nombre} fue curado y todas las penalizaciones fueron eliminadas.[/]");
            Thread.Sleep(1000);
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
            Thread.Sleep(1000);
        }
    }
}