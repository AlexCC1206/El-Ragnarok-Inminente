using System;
using Spectre.Console;

public class VisionProfetica : Habilidad
{
    public VisionProfetica() : base("Visión Profética", 4)
    {
    }

    public override void Usar(Ficha ficha)
    {
        
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            Explorar(ficha.Tablero.celdas);

        }
        else
        {
            AnsiConsole.MarkupLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
            Thread.Sleep(1000);
        }
    }
    public void Explorar(string[,] tablero)
    {
        int x, y;
        bool entradaValida;

        do
        {
            Console.WriteLine("Ingrese la coordenada x:");
            entradaValida = int.TryParse(Console.ReadLine(), out x);
            if (!entradaValida)
            {
                AnsiConsole.MarkupLine("[red]Coordenada x inválida. Por favor, ingrese un número entero.[/]");
            }
        } while (!entradaValida);

        do
        {
            Console.WriteLine("Ingrese la coordenada y:");
            entradaValida = int.TryParse(Console.ReadLine(), out y);
            if (!entradaValida)
            {
                AnsiConsole.MarkupLine("[red]Coordenada y inválida. Por favor, ingrese un número entero.[/]");
            }
        } while (!entradaValida);

        if (x >= 0 && x < tablero.GetLength(0) && y >= 0 && y < tablero.GetLength(1))
        {
            string contenido = tablero[x, y];
            AnsiConsole.MarkupLine($"[yellow]El contenido de la casilla ({x}, {y}) es: {contenido}[/]");
            Thread.Sleep(1000);
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Coordenadas inválidas.[/]");
            Thread.Sleep(1000);
        }
    }    
}