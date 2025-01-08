using System;

public class RootTrap : Trampa
{
    public RootTrap() : base("Trampa de Raíces", "Atrapa al jugador con raíces.", "!")
    {
    }

    public override void Activar(Ficha ficha)
    {
        Random rnd = new Random();
        int probabilidad = rnd.Next(0, 2); // 50% de probabilidad de ser afectado (0 o 1)

        if (probabilidad == 0) // 50% de probabilidad de ser afectado
        {
            // Aplicar daño directo
            int dano = 10; // Cantidad de daño fijo
            ficha.Vida -= dano; // Reducir la vida de la ficha
            Console.WriteLine($"{ficha.Nombre} ha sido herido por las Espinas de Hel. ¡Pierde {dano} puntos de vida!");
        }
        else
        {
            Console.WriteLine($"{ficha.Nombre} evita las Espinas de Hel y no sufre daño.");
        }
    }
}
    

    