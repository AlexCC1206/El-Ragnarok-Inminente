using System;

public class Heimdall : Ficha
{
    public Heimdall() : base("Heimdall, el Guardián de Asgard", 3, "Visión Profética", 4)
    {
    }

    public override void UsarHabilidad()
    {
        Console.WriteLine("Heimdall usa Visión Profética y se mueve una casilla adicional inmediatamente, ignorando trampas o efectos de habilidades durante ese turno.");
    }
}