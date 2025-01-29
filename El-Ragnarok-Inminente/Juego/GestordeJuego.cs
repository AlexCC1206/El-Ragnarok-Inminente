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
        // Asignar fichas directamente sin selección
        fichaJugador1 = jugadores[0].Fichas[0]; // Usa la primera ficha del Jugador 1
        fichaJugador2 = jugadores[1].Fichas[0]; // Usa la primera ficha del Jugador 2

        // Colocar las fichas en el tablero
        tablero.AñadirFicha(fichaJugador1, 1, 1);
        tablero.AñadirFicha(fichaJugador2, tablero.tamaño - 2, tablero.tamaño - 2);

        //InicializarFichas(jugadores);
    }
    
    public void Iniciar()
    {   
        while (true)
        {
            Jugador jugadorActual = turno.ObtenerJugadorActual();
            Ficha fichaSeleccionada = jugadorActual == turno.jugadores[0] ? fichaJugador1 : fichaJugador2;
            
            ActualizarTablero();

            // Mostrar la leyenda
            //InterfazJuego.MostrarLeyenda();

            // Mostrar información del jugador
            //InterfazJuego.MostrarInformacionJugador(jugadorActual);

            string opcionSeleccionada = InterfazJuego.MostrarOpciones();

            AnsiConsole.MarkupLine($"[bold]Turno de {jugadorActual.Nombre}[/]");
            // Realizar la acción seleccionada
            switch (opcionSeleccionada)
            {
                case "Moverse":
                    MoverFicha(fichaSeleccionada, jugadorActual);
                    break;
                case "Usar habilidad":
                    UsarHabilidad(fichaSeleccionada, jugadorActual);
                    break;
                case "Pasar turno":
                    AnsiConsole.MarkupLine($"[yellow]{jugadorActual.Nombre} ha decidido pasar su turno.[/]");
                    break;
            }
            
            //tablero.Imprimir();
            //Console.Clear();

            // Verificar si la ficha ha llegado a la salida
            if (tablero.EsSalida(fichaSeleccionada.PosicionX, fichaSeleccionada.PosicionY))
            {
                Console.Clear();
                
                var textoVictoria = new FigletText($"¡{jugadorActual.Nombre} ha ganado!")
                    .Color(Color.Green)
                    .Centered();

                AnsiConsole.Write(textoVictoria);
                Environment.Exit(0); // Termina el juego
            }

            // Pasar al siguiente turno
            turno.SiguienteTurno();
        }
    }

    private void MoverFicha(Ficha ficha, Jugador jugador)
    {
        int movimientosRestantes = ficha.Velocidad;

        while (movimientosRestantes > 0)
        {
            var key = Console.ReadKey(true).Key;
            int nuevaX = ficha.PosicionX;
            int nuevaY = ficha.PosicionY;

            if (jugador.Nombre == "Jugador 1" || jugador.Nombre == "Jugador 2")
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
                }
            } 

            if (nuevaX != ficha.PosicionX || nuevaY != ficha.PosicionY)
            {
                try
                    {
                        tablero.MoverFicha(ficha, nuevaX, nuevaY);
                        movimientosRestantes--;
                        tablero.AplicarEfectoTrampa(ficha);
                        tablero.AplicarEfectoCasillas(ficha);
                        ActualizarTablero();
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                

                    
                //Console.Clear();

                // Imprimir el tablero actualizado
                

                //AnsiConsole.MarkupLine($"[green]{jugador.Nombre} ha movido su ficha a ({nuevaX}, {nuevaY}).[/]");
                
            }
        }
    }

    private void UsarHabilidad(Ficha ficha, Jugador jugador)
    {
        // Verificar si la ficha tiene habilidades disponibles
        if (ficha.Habilidades.Count > 0)
        {
            // Mostrar las habilidades disponibles
            var prompt = new SelectionPrompt<string>()
                .Title($"{jugador.Nombre}, selecciona una habilidad:")
                .AddChoices(ficha.Habilidades.ConvertAll(h => h.Nombre));

            string habilidadSeleccionada = AnsiConsole.Prompt(prompt);

            // Encontrar la habilidad seleccionada
            var habilidad = ficha.Habilidades.Find(h => h.Nombre == habilidadSeleccionada);

            if (habilidad != null)
            {
                // Usar la habilidad seleccionada
                habilidad.Usar(ficha);
                AnsiConsole.MarkupLine($"[green]{jugador.Nombre} ha usado la habilidad {habilidad.Nombre} con {ficha.Nombre}.[/]");
                System.Threading.Thread.Sleep(2000);
                ActualizarTablero();
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No se pudo encontrar la habilidad seleccionada.[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]{ficha.Nombre} no tiene habilidades disponibles.[/]");
        }
    }

    private void ActualizarTablero()
    {
        Console.Clear(); // Limpia la consola
        tablero.Imprimir(); // Imprime el tablero actualizado
    }

}

            /*
            int movimientosRestantes = fichaSeleccionada.Velocidad;

            // Movimiento de ficha
            //AnsiConsole.MarkupLine("[bold]Usa WASD para mover la ficha (Jugador 1) o las flechas (Jugador 2). Presiona Spacebar (Jugador 1) o Enter (Jugador 2) para usar la habilidad.[/]");
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
                /*
                // Actualizar la posición de la ficha si es válida
                if (tablero.EsMovimientoValido(nuevaX, nuevaY))
                {
                    fichaSeleccionada.PosicionX = nuevaX;
                    fichaSeleccionada.PosicionY = nuevaY;
                    movimientosRestantes--;
                    tablero.Imprimir(); // Imprime el tablero actualizado
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Movimiento inválido.[/]");
                }
            
                // Mostrar opciones del jugador
                InterfazJuego.MostrarOpciones();
                
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
                            Environment.Exit(0); // Termina el juego
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
