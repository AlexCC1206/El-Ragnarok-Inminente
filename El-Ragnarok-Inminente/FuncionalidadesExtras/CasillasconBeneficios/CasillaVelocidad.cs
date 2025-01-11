using System;

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
        Console.WriteLine($"{ficha.Nombre} ha ganado {IncrementoVelocidad} de velocidad.");
        System.Threading.Thread.Sleep(2000);
    }
}