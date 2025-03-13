using System;
using System.Collections.Generic;
using Spectre.Console;

public class Juego
{
    private Tablero tablero; // Instancia del tablero donde se desarrolla el juego
    private Turno turno;     // Gestiona los turnos de los jugadores
    private Ficha fichaJugador1; // Ficha asignada al Jugador 1
    private Ficha fichaJugador2; // Ficha asignada al Jugador 2

    // Constructor: Inicializa el juego con un tablero y una lista de jugadores
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
    }
    
    // Método principal que inicia el juego
    public void Iniciar()
    {   
        while (true)
        {
            Jugador jugadorActual = turno.ObtenerJugadorActual();
            Ficha fichaSeleccionada = jugadorActual == turno.jugadores[0] ? fichaJugador1 : fichaJugador2; // Selecciona la ficha del jugador actual
            
            ActualizarTablero();

            string opcionSeleccionada = InterfazJuego.MostrarOpciones();

            AnsiConsole.MarkupLine($"[bold]Turno de {jugadorActual.Nombre}[/]");
            
            // Realizar la acción seleccionada por el jugador
            switch (opcionSeleccionada)
            {
                case "Moverse":
                    MoverFicha(fichaSeleccionada, jugadorActual);
                    break;
                case "Usar habilidad":
                    UsarHabilidad(fichaSeleccionada, jugadorActual);
                    break;
            }
           
            // Verificar si la ficha ha llegado a la salida
            if (tablero.EsSalida(fichaSeleccionada.PosicionX, fichaSeleccionada.PosicionY))
            {
                /*
                Console.Clear();
                
                var textoVictoria = new FigletText($"¡{jugadorActual.Nombre} ha ganado!")
                    .Color(Color.Green)
                    .Centered();

                AnsiConsole.Write(textoVictoria);
                Environment.Exit(0); // Termina el juego
                */
                AnsiConsole.MarkupLine($"[bold green]{jugadorActual.Nombre} ha ganado al llegar a la salida con {fichaSeleccionada.Nombre}![/]");
                Environment.Exit(0); // Termina el juego
            }

            // Pasar al siguiente turno
            turno.SiguienteTurno();
        }
    }

    private void MoverFicha(Ficha ficha, Jugador jugador)
    {
        int movimientosRestantes = ficha.Velocidad;

        while (movimientosRestantes > 0) // Permite moverse hasta agotar los movimientos
        {
            var key = Console.ReadKey(true).Key; // Captura la tecla presionada
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

            // Si la posición cambió, intenta mover la ficha
            if (nuevaX != ficha.PosicionX || nuevaY != ficha.PosicionY)
            {
                try
                    {
                        tablero.MoverFicha(ficha, nuevaX, nuevaY);
                        movimientosRestantes--;
                        ActualizarTablero();
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message); // Muestra un mensaje de error si el movimiento no es válido
                    }
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
                Thread.Sleep(1000);
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

    public void ActualizarTablero()
    {
        Console.Clear(); // Limpia la consola
        tablero.Imprimir(); // Imprime el tablero actualizado
    }
}

