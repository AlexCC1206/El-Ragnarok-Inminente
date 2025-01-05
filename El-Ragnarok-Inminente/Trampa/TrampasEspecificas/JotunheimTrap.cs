using System;

public class JotunheimTrap : Trampa
{
    public JotunheimTrap() : base("Trampa de Jotunheim", "Congela al jugador.", 'J')
    {
    }

    public override void Activar()
    {
        Console.WriteLine("¡La trampa de Jotunheim se activa y congela al jugador!");
    }
}