using System;
using Spectre.Console;

public class CasillaHabilidad : Casilla
{
    public Habilidad HabilidadAdicional { get; set; }

    public CasillaHabilidad(int posicionX, int posicionY, Habilidad habilidadAdicional)
        : base(posicionX, posicionY, "*")
    {
        HabilidadAdicional = habilidadAdicional;
    }

    public override void AplicarEfecto(Ficha ficha)
    {
        ficha.Habilidades.Add(HabilidadAdicional);
        AnsiConsole.MarkupLine($"[green]{ficha.Nombre} ha obtenido la habilidad {HabilidadAdicional.Nombre}[/]");
        Thread.Sleep(1000);
    }
}