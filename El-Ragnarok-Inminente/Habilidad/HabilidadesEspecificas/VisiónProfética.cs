using System;

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
            Console.WriteLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
        }
    }
    public void Explorar(string[,] tablero)
    {
        Console.WriteLine("Ingrese las coordenadas a explorar (x y):");
        string[] input = Console.ReadLine().Split();
        int x = int.Parse(input[0]);
        int y = int.Parse(input[1]);

        if (x >= 0 && x < tablero.GetLength(0) && y >= 0 && y < tablero.GetLength(1))
        {
            string contenido = tablero[x, y];
            Console.WriteLine($"[yellow]El contenido de la casilla ({x}, {y}) es: {contenido}[/]");
        }
        else
        {
            Console.WriteLine("Coordenadas inválidas.");
        }
    }
    
}