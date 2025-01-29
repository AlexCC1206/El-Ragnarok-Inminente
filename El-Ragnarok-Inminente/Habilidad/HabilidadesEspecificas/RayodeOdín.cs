using System;

public class RayoDeOdin : Habilidad
{
    public RayoDeOdin() : base("Rayo de Odín", 3)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            ficha.Velocidad = ficha.VelocidadBase; // Restaurar velocidad base
            ficha.Paralizado = false;          // Quitar parálisis si existe
            //ficha.InmuneATramapa = false;      // Quitar inmunidad a trampas si existe
            //ficha.Invisible = false;           // Quitar invisibilidad si existe
            //ficha.Inmovilizado = false;        // Quitar inmovilización si existe
            Console.WriteLine($"[yellow]{ficha.Nombre} fue curado y todas las penalizaciones fueron eliminadas.[/]");
        }
        else
        {
            Console.WriteLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
        }
    }
}