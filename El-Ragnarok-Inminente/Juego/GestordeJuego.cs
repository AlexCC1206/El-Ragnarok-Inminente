using System;
using System.Collections.Generic;

public class Juego
{
    private Tablero tablero;
    private Turno turno;

    public Juego(Tablero tablero, List<Jugador> jugadores)
    {
        this.tablero = tablero;
        this.turno = new Turno(jugadores);
    }

    public void Iniciar()
    {
        while (true)
        {
            Jugador jugadorActual = turno.ObtenerJugadorActual();
            Console.WriteLine($"Turno de {jugadorActual.Nombre}");

            // Selección de ficha
            Console.WriteLine("Selecciona una ficha:");
            for (int i = 0; i < jugadorActual.Fichas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {jugadorActual.Fichas[i].Nombre}");
            }
            int indiceFicha = int.Parse(Console.ReadLine()) - 1;
            Ficha fichaSeleccionada = jugadorActual.Fichas[indiceFicha];

            // Movimiento de ficha
            Console.WriteLine("Usa WASD para mover la ficha (Jugador 1) o las flechas (Jugador 2). Presiona Spacebar (Jugador 1) o Enter (Jugador 2) para usar la habilidad.");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                int nuevaX = fichaSeleccionada.PosicionX;
                int nuevaY = fichaSeleccionada.PosicionY;

                if (jugadorActual.Nombre == "Jugador 1")
                {
                    switch (key)
                    {
                        case ConsoleKey.W:
                            nuevaX--;
                            break;
                        case ConsoleKey.A:
                            nuevaY--;
                            break;
                        case ConsoleKey.S:
                            nuevaX++;
                            break;
                        case ConsoleKey.D:
                            nuevaY++;
                            break;
                        case ConsoleKey.Spacebar:
                            fichaSeleccionada.UsarHabilidad(0);
                            break;
                    }
                }
                else if (jugadorActual.Nombre == "Jugador 2")
                {
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            nuevaX--;
                            break;
                        case ConsoleKey.LeftArrow:
                            nuevaY--;
                            break;
                        case ConsoleKey.DownArrow:
                            nuevaX++;
                            break;
                        case ConsoleKey.RightArrow:
                            nuevaY++;
                            break;
                        case ConsoleKey.Enter:
                            fichaSeleccionada.UsarHabilidad(0);
                            break;
                    }
                }

                if (nuevaX != fichaSeleccionada.PosicionX || nuevaY != fichaSeleccionada.PosicionY)
                {
                    try
                    {
                        tablero.MoverFicha(fichaSeleccionada, nuevaX, nuevaY);
                        break;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            tablero.Imprimir();
            turno.SiguienteTurno();
        }
    }
}