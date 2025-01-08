using System;

public class IlusionSombria : Habilidad
{
    public IlusionSombria() : base("Ilusión Sombría", 2)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            ficha.Invisible = true; // Nueva propiedad en Ficha
            Console.WriteLine($"{ficha.Nombre} usa {Nombre} y se vuelve invisible durante el próximo turno.");
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }
}