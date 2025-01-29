using System;

public class TormentaHeladadeHelheim : Trampa
{
    int turnosCongelado = 0;
    public TormentaHeladadeHelheim() : base("Tormenta Helada de Helheim", "Pierdes un Turno", "~")
    {
    }

    public override void Activar(Ficha ficha)
    {
        Random rnd = new Random();
        int probabilidad = rnd.Next(0, 2); // 50% de probabilidad de ser afectado (0 o 1)

        if (probabilidad == 0) // 50% de probabilidad de ser afectado
        {
            ficha.Congelado = true;
            turnosCongelado = 2;
            Console.WriteLine($"¡{ficha.Nombre} ha caído en una Tormenta Helada! Congelada por {turnosCongelado} turnos.");
            
        }
        else
        {
            Console.WriteLine($"{ficha.Nombre} evita las Columnas de Fuego y no pierde un turno.");
        }
        System.Threading.Thread.Sleep(3000);  
    }

    public void Descongelar(Ficha ficha)
    {
        if (turnosCongelado > 0)
        {
            turnosCongelado--;
            Console.WriteLine($"Quedan {turnosCongelado} turnos para que {ficha.Nombre} se descongele.");
        }
        else
        {
            ficha.Congelado = false;
            Console.WriteLine($"{ficha.Nombre} se ha descongelado.");
        }
    }
}