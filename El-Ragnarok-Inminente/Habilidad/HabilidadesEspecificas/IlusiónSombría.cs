using System;

public class IlusionSombria : Habilidad
{
    public IlusionSombria() : base("Ilusión Sombría", 2)
    {
    }

    public override void Usar(Ficha ficha, Ficha objetivo)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            ficha.InmuneAInteracciones = true; // Marca a la ficha como inmune a interacciones
            Console.WriteLine($"{ficha.Nombre} usa {Nombre} y evita ser atacado o bloqueado durante el próximo turno.");
        }
        else
        {
            Console.WriteLine($"{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}");
        }
    }
}