using System;

public class RootTrap : Trampa
{
    public RootTrap() : base("Trampa de Raíces", "Atrapa al jugador con raíces.", 'R')
    {
    }

    public override void Activar()
    {
        Console.WriteLine("¡La trampa de raíces se activa y atrapa al jugador!");
    }
}