using System;
using System.Collections.Generic;

public class Juego
{
    private Tablero tablero;
    private Turno turno;
    private Ficha fichaJugador1;
    private Ficha fichaJugador2;

    public Juego(Tablero tablero, List<Jugador> jugadores)
    {
        this.tablero = tablero;
        this.turno = new Turno(jugadores);
        InicializarFichas(jugadores);
    }

    private void InicializarFichas(List<Jugador> jugadores)
    {
        Jugador jugador1 = jugadores[0];
        Jugador jugador2 = jugadores[1];

        // Selección de ficha para Jugador 1
        Console.WriteLine($"{jugador1.Nombre}, selecciona una ficha:");
        for (int i = 0; i < jugador1.Fichas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {jugador1.Fichas[i].Nombre}");
        }
        int indiceFicha1 = int.Parse(Console.ReadLine()!) - 1;
        fichaJugador1 = jugador1.Fichas[indiceFicha1];
        tablero.AñadirFicha(fichaJugador1, 1, 1);

        // Selección de ficha para Jugador 2
        Console.WriteLine($"{jugador2.Nombre}, selecciona una ficha:");
        for (int i = 0; i < jugador2.Fichas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {jugador2.Fichas[i].Nombre}");
        }
        int indiceFicha2 = int.Parse(Console.ReadLine()!) - 1;
        fichaJugador2 = jugador2.Fichas[indiceFicha2];
        tablero.AñadirFicha(fichaJugador2, tablero.tamaño - 2, tablero.tamaño - 2);
    }

    public void Iniciar()
    {
        while (true)
        {
            Jugador jugadorActual = turno.ObtenerJugadorActual();
            Ficha fichaSeleccionada = jugadorActual == turno.jugadores[0] ? fichaJugador1 : fichaJugador2;

            try
            {
                Console.Clear(); // Limpia la consola
            }
            catch (IOException)
            {
                // Maneja la excepción si Console.Clear() no es compatible
                Console.WriteLine("No se puede limpiar la consola en este entorno.");
            }
            tablero.Imprimir(); // Imprime el tablero actualizado
            Console.WriteLine($"Turno de {jugadorActual.Nombre}");

            int movimientosRestantes = fichaSeleccionada.Velocidad;

            // Movimiento de ficha
            Console.WriteLine("Usa WASD para mover la ficha (Jugador 1) o las flechas (Jugador 2). Presiona Spacebar (Jugador 1) o Enter (Jugador 2) para usar la habilidad.");
            while (movimientosRestantes > 0)
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
                        movimientosRestantes--;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            // Limpia la consola y vuelve a imprimir el tablero al final del turno
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
                Console.WriteLine("No se puede limpiar la consola en este entorno.");
            }
            tablero.Imprimir();
            turno.SiguienteTurno();
        }
    }
}