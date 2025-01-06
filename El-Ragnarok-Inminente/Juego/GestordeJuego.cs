using System;
using System.Collections.Generic;
using Spectre.Console;

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
        var prompt1 = new SelectionPrompt<string>()
            .Title($"{jugador1.Nombre}, selecciona una ficha:")
            .AddChoices(jugador1.Fichas.ConvertAll(f => f.Nombre));
        string seleccion1 = AnsiConsole.Prompt(prompt1);
        fichaJugador1 = jugador1.Fichas.Find(f => f.Nombre == seleccion1);
        tablero.AñadirFicha(fichaJugador1, 1, 1);

        // Selección de ficha para Jugador 2
        var prompt2 = new SelectionPrompt<string>()
            .Title($"{jugador2.Nombre}, selecciona una ficha:")
            .AddChoices(jugador2.Fichas.ConvertAll(f => f.Nombre));
        string seleccion2 = AnsiConsole.Prompt(prompt2);
        fichaJugador2 = jugador2.Fichas.Find(f => f.Nombre == seleccion2);
        tablero.AñadirFicha(fichaJugador2, tablero.tamaño - 2, tablero.tamaño - 2);
    }

    public void Iniciar()
    {
        AnsiConsole.MarkupLine("[bold]Instrucciones:[/]");
        AnsiConsole.MarkupLine("Usa WASD para mover la ficha (Jugador 1) o las flechas (Jugador 2). Presiona Spacebar (Jugador 1) o Enter (Jugador 2) para usar la habilidad.");
        
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
                AnsiConsole.MarkupLine("[red]No se puede limpiar la consola en este entorno.[/]");
            }
            tablero.Imprimir(); // Imprime el tablero actualizado
            AnsiConsole.MarkupLine($"[bold]Turno de {jugadorActual.Nombre}[/]");

            int movimientosRestantes = fichaSeleccionada.Velocidad;

            // Movimiento de ficha
            AnsiConsole.MarkupLine("[bold]Usa WASD para mover la ficha (Jugador 1) o las flechas (Jugador 2). Presiona Spacebar (Jugador 1) o Enter (Jugador 2) para usar la habilidad.[/]");
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
                        try
                        {
                            Console.Clear(); // Limpia la consola después de cada movimiento
                        }
                        catch (IOException)
                        {
                            // Maneja la excepción si Console.Clear() no es compatible
                            AnsiConsole.MarkupLine("[red]No se puede limpiar la consola en este entorno.[/]");
                        }
                        tablero.Imprimir(); // Imprime el tablero actualizado después de cada movimiento

                        // Verifica si la ficha ha llegado a la salida
                        if (tablero.EsSalida(fichaSeleccionada.PosicionX, fichaSeleccionada.PosicionY))
                        {
                            AnsiConsole.MarkupLine($"[bold green]{jugadorActual.Nombre} ha ganado al llegar a la salida con {fichaSeleccionada.Nombre}![/]");
                            return; // Termina el juego
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                    }
                }
            }

            turno.SiguienteTurno();
        }
    }
/*
    private void MostrarTablero()
    {
        var table = new Table();
        for (int i = 0; i < tablero.tamaño; i++)
        {
            var row = new List<string>();
            for (int j = 0; j < tablero.tamaño; j++)
            {
                row.Add(tablero.ObtenerCelda(i, j).ToString());
            }
            table.AddRow(row.ToArray());
        }
        AnsiConsole.Render(table);
    }
    */
}