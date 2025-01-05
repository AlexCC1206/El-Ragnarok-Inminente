using System;

public class WindTrap : Trampa
{
    public WindTrap() : base("Trampa de Viento", "Empuja al jugador hacia atrás.", 'W')
    {
    }

    public override void Activar()
    {
        Console.WriteLine("¡La trampa de viento se activa y empuja al jugador hacia atrás!");
    }
}