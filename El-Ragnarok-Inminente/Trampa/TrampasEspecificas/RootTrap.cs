using System;

public class RootTrap : Trampa
{
    public RootTrap() : base("Trampa de Raíces", "Atrapa al jugador con raíces.", '!')
    {
    }

    public override void Activar(Ficha ficha)
    {
        // Lógica para RootTrap
        ficha.Inmovilizado = true; // Inmoviliza la ficha
        Console.WriteLine($"{ficha.Nombre} queda atrapado por las raíces de Yggdrasil.");
    }
}