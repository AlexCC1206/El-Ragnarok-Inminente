using System;

public class WindTrap : Trampa
{
    public WindTrap() : base("Trampa de Viento", "Empuja al jugador hacia atrás.", "~")
    {
    }

    public override void Activar(Ficha ficha)
    {
        Random rnd = new Random();
        int probabilidad = rnd.Next(0, 2); // 50% de probabilidad de ser afectado (0 o 1)

        if (probabilidad == 0) // 50% de probabilidad de ser afectado
        {
            // Aplicar efecto de reducción de velocidad
            ficha.Velocidad /= 2; // Reduce la velocidad a la mitad
            Console.WriteLine($"{ficha.Nombre} ha sido afectado por el Viento de Jörmungandr. ¡Su velocidad se reduce a la mitad durante este turno!");
            
        }
        else
        {
            Console.WriteLine($"{ficha.Nombre} resiste el efecto del Viento de Jörmungandr y continúa sin cambios.");
        }
        System.Threading.Thread.Sleep(2000);
    }
}