using System;
using Spectre.Console;

public class JotunheimTrap : Trampa
{
    private int turnosRestantes = 2;
    public JotunheimTrap() : base("Trampa de Jotunheim", "Congela al jugador.", "X")
    {
    }

    public override void Activar(Ficha ficha)
    {
        if (turnosRestantes > 0)
        {
            ficha.Velocidad = 1; // Reduce la velocidad de la ficha
            AnsiConsole.MarkupLine($"[bold red]{ficha.Nombre} se ralentiza debido a las Piedras de Jotunheim. Turnos restantes: {turnosRestantes}[/]");
            turnosRestantes--; // Reduce el contador de turnos
            
        }
        else
        {
            AnsiConsole.MarkupLine($"[green]{ficha.Nombre} ya no está afectado por las Piedras de Jotunheim.[/]");
            
        } 
        Thread.Sleep(1000);     
    }
}