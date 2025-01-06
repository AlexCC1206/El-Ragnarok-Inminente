using System;

public class WindTrap : Trampa
{
    public WindTrap() : base("Trampa de Viento", "Empuja al jugador hacia atrás.", '~')
    {
    }

    public override void Activar(Ficha ficha)
    {
        // Lógica para WindTrap
        ficha.PosicionX -= 2; // Ejemplo: mueve la ficha dos casillas hacia atrás
        Console.WriteLine($"{ficha.Nombre} es empujado hacia atrás por el Viento de Jörmungandr.");
    }
}