using System;

public class Tyr : Ficha
{
    public Tyr() : base("Tyr, el Dios de la Guerra", 3, "Justicia Implacable", 3)
    {
    }

    public override void UsarHabilidad()
    {
        Console.WriteLine("Tyr usa Justicia Implacable y reduce la velocidad de una ficha enemiga adyacente a 1 casilla por turno durante los próximos 2 turnos.");
    }
}