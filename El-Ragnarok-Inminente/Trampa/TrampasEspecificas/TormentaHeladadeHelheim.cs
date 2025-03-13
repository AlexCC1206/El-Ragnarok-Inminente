using System;
using Spectre.Console;

public class TormentaHeladadeHelheim : Trampa
{
    int turnosCongelado = 0;
    public TormentaHeladadeHelheim() : base("Tormenta Helada de Helheim", "Pierdes un Turno", "~")
    {
    }

    public override void Activar(Ficha ficha)
    {
        Random rnd = new Random();
        int probabilidad = rnd.Next(0, 2); // 50% de probabilidad de ser afectado (0 o 1)

        if (probabilidad == 0) // 50% de probabilidad de ser afectado
        {
            AnsiConsole.MarkupLine($"[bold red]¡{ficha.Nombre} ha caído en una Tormenta Helada! Congelada por {turnosCongelado} turnos.[/]");
            ficha.Congelado = true;
            turnosCongelado = 2;
        }
        else
        {
            AnsiConsole.MarkupLine($"[green]{ficha.Nombre} evita la Tormenta Helada y no pierde dos turnos.[/]");
            
        }
        Thread.Sleep(1000);
    }
    /*
    public void Descongelar(Ficha ficha)
    {
        if (turnosCongelado > 0)
        {
            turnosCongelado--;
            AnsiConsole.MarkupLine($"[bold white]Quedan {turnosCongelado} turnos para que {ficha.Nombre} se descongele.[/]");
            
        }
        else
        {
            ficha.Congelado = false;
            AnsiConsole.MarkupLine($"[green]{ficha.Nombre} se ha descongelado.[/]");
           
        }
        Thread.Sleep(1000);
    }
    */
}