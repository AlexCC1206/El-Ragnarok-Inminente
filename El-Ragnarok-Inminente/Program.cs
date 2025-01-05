using System;

class Program
{
    static void Main()
    {
        Tablero tablero = new Tablero(20);
        tablero.Imprimir();

        Ficha loki = new Loki();
        Ficha odin = new Odin();
        Ficha baldur = new Baldur();
        Ficha heimdall = new Heimdall();
        Ficha tyr = new Tyr();

        loki.UsarHabilidad(0); // Usa Ilusión Sombría
        odin.UsarHabilidad(0); // Usa Rayo de Odín en Loki
        baldur.UsarHabilidad(0); // Usa Resiliencia Inmortal
        heimdall.UsarHabilidad(0); // Usa Visión Profética
        tyr.UsarHabilidad(0); // Usa Justicia Implacable en Loki

        // Simula el paso de turnos
        for (int i = 0; i < 5; i++)
        {
            loki.ReducirEnfriamientoHabilidades();
            odin.ReducirEnfriamientoHabilidades();
            baldur.ReducirEnfriamientoHabilidades();
            heimdall.ReducirEnfriamientoHabilidades();
            tyr.ReducirEnfriamientoHabilidades();
            Console.WriteLine($"Turno {i + 1}:");
        }
    }
}
