using System;
using Spectre.Console;

public class CasillaVelocidad : Casilla
{
    public int IncrementoVelocidad { get; set; }

    public CasillaVelocidad(int posicionX, int posicionY, int incrementoVelocidad)
        : base(posicionX, posicionY, "^")
    {
        IncrementoVelocidad = incrementoVelocidad;
    }

    public override void AplicarEfecto(Ficha ficha)
    {
        ficha.Velocidad += IncrementoVelocidad;
        AnsiConsole.MarkupLine($"[green]{ficha.Nombre} ha ganado {IncrementoVelocidad} de velocidad.[/]");
        Thread.Sleep(1000);
    }
}