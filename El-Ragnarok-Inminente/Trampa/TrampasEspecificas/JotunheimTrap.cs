using System;

public class JotunheimTrap : Trampa
{
    private int turnosRestantes = 2;
    public JotunheimTrap() : base("Trampa de Jotunheim", "Congela al jugador.", "X")
    {
    }

    public override void Activar(Ficha ficha)
    {
        if (turnosRestantes > 0)
        {
            ficha.Velocidad = 1; // Reduce la velocidad de la ficha
            Console.WriteLine($"{ficha.Nombre} se ralentiza debido a las Piedras de Jotunheim. Turnos restantes: {turnosRestantes}");
            turnosRestantes--; // Reduce el contador de turnos
        }
        else
        {
            Console.WriteLine($"{ficha.Nombre} ya no está afectado por las Piedras de Jotunheim.");
        }           
    }
}