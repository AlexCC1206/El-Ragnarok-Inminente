using System;

public class JotunheimTrap : Trampa
{
    public JotunheimTrap() : base("Trampa de Jotunheim", "Congela al jugador.", 'X')
    {
    }

    public override void Activar(Ficha ficha)
    {
        // Lógica para JotunheimTrap
        ficha.Velocidad = 1; // Reduce la velocidad de la ficha
        Console.WriteLine($"{ficha.Nombre} se ralentiza debido a las Piedras de Jotunheim.");
    }
}