using System;

public class Odin : Ficha
{
    public Odin() : base("Odin, el Padre de Todos", 2, "Rayo de Odín", 3)
    {
    }

    public override void UsarHabilidad()
    {
        Console.WriteLine("Odin usa Rayo de Odín y paraliza una ficha enemiga en línea recta durante un turno.");
    }
}