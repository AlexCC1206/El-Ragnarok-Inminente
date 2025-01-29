using System;

public class ColumnasdeFuegodeSurtur : Trampa
{
    public ColumnasdeFuegodeSurtur() : base("Columnas de Fuego de Surtur", "Pierdes un Turno", "~")
    {
    }

    public override void Activar(Ficha ficha)
    {
        Random rnd = new Random();
        int probabilidad = rnd.Next(0, 2); // 50% de probabilidad de ser afectado (0 o 1)

        if (probabilidad == 0) // 50% de probabilidad de ser afectado
        {
            ficha.Atrapado = true;
            ficha.turnosAtrapado = 1;
            Console.WriteLine($"¡La {ficha.Nombre} ha caído en Columnas de Fuego! Pierdes {ficha.turnosAtrapado} turno.");
            
        }
        else
        {
            Console.WriteLine($"{ficha.Nombre} evita las Columnas de Fuego y no pierde un turno.");
        }
        System.Threading.Thread.Sleep(2000);
    }

    
}