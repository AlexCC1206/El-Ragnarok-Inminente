using System;

public class Baldur : Ficha
{
    public Baldur() : base("Baldur, el Dios de la Luz", 3, "Resiliencia Inmortal", 1)
    {
    }

    public override void UsarHabilidad()
    {
        Console.WriteLine("Baldur usa Resiliencia Inmortal y ignora el efecto de la próxima trampa en la que caiga.");
    }
}