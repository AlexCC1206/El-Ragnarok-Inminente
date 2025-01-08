using System;

public class ResilienciaInmortal : Habilidad
{
    public ResilienciaInmortal() : base("Resiliencia Inmortal", 1)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            ficha.InmuneATramapa = true; // Marca a la ficha como inmune a la próxima trampa
            Console.WriteLine($"{ficha.Nombre} usa {Nombre} y no sufrirá penalización la próxima vez que caiga en una trampa.");
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }
}