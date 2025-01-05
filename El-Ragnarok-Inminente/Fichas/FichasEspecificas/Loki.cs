using System;

public class Loki : Ficha
{
    public Loki() : base("Loki, el Embaucador", 3, "Ilusión Sombría", 2)
    {
    }

    public override void UsarHabilidad()
    {
        Console.WriteLine("Loki activa Ilusión Sombría y evita ser atacado o bloqueado durante el próximo turno.");
    }
}