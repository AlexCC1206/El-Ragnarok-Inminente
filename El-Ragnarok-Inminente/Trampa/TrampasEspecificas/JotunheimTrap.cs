using System;
using Spectre.Console;

public class JotunheimTrap : Trampa
{
    private int turnosRestantes = 2;
    private int velocidadOriginal;
    public JotunheimTrap() : base("Trampa de Jotunheim", "Congela al jugador.", "X")
    {
    }

    public override void Activar(Ficha ficha)
    {
        if (turnosRestantes == 2) // Guardar velocidad original solo la primera vez
        {
            velocidadOriginal = ficha.Velocidad;
            ficha.Velocidad = 1;
        }

        if (turnosRestantes > 0)
        {
            AnsiConsole.MarkupLine($"[bold red]{ficha.Nombre} se ralentiza debido a las Piedras de Jotunheim. Turnos restantes: {turnosRestantes}[/]");
            turnosRestantes--; // Reduce el contador de turnos
            
        }
        else
        {
            ficha.Velocidad = velocidadOriginal;
            AnsiConsole.MarkupLine($"[green]{ficha.Nombre} ya no está afectado por las Piedras de Jotunheim.[/]");
        } 
        Thread.Sleep(1000);     
    }
}