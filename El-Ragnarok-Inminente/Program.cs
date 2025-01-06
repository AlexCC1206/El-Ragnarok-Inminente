using System;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        InterfazJuego.MostrarTitulo();

        string opcion = InterfazJuego.ObtenerOpcionUsuario();
        InterfazJuego.ProcesarOpcion(opcion);
    }
}

public static class InterfazJuego
{
    public static void MostrarTitulo()
    {
        AnsiConsole.Write(new FigletText("Bienvenido a El Ragnarok Inminente!")
            .Centered()
            .Color(Color.Green));
    }

    public static string ObtenerOpcionUsuario()
    {
        var opciones = new[] { "Nueva Partida", "Instrucciones", "Salir" };
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Seleccione una opción:")
                .AddChoices(opciones));
    }

    public static void ProcesarOpcion(string opcion)
    {
        switch (opcion)
        {
            case "Nueva Partida":
                AnsiConsole.MarkupLine("[yellow]Has seleccionado nueva partida.[/]");
                IniciarNuevaPartida();
                break;
            case "Instrucciones":
                AnsiConsole.MarkupLine("[yellow]Has seleccionado instrucciones.[/]");
                MostrarInstrucciones();
                break;
            case "Salir":
                AnsiConsole.MarkupLine("[red]Has seleccionado salir.[/]");
                break;
            default:
                AnsiConsole.MarkupLine("[red]Opción no válida.[/]");
                break;
        }
    }

    private static void IniciarNuevaPartida()
    {
        Tablero tablero = new Tablero(21);
        Jugador jugador1 = new Jugador("Jugador 1");
        Jugador jugador2 = new Jugador("Jugador 2");

        // Añadir fichas a los jugadores
        jugador1.AñadirFicha(new Loki());
        jugador1.AñadirFicha(new Odin());
        jugador1.AñadirFicha(new Baldur());
        jugador1.AñadirFicha(new Heimdall());
        jugador1.AñadirFicha(new Tyr());

        jugador2.AñadirFicha(new Loki());
        jugador2.AñadirFicha(new Odin());
        jugador2.AñadirFicha(new Baldur());
        jugador2.AñadirFicha(new Heimdall());
        jugador2.AñadirFicha(new Tyr());

        Juego juego = new Juego(tablero, new List<Jugador> { jugador1, jugador2 });
        juego.Iniciar();
    }

    public static void MostrarInstrucciones()
    {
        var reglas = new Rule("[yellow]Instrucciones del Juego[/]");
        AnsiConsole.Write(reglas);

        var panel = new Panel(@"
        1. El objetivo del juego es escapar del tablero mientras sobreviven al caos del fin del mundo, enfrentándose a dioses y criaturas mitológicas.
        2. Cada jugador tiene una de ficha con una habilidad.
        3. En cada turno, puedes mover una ficha o activar su habilidad.
        4. El juego termina cuando logran escapar del tablero.
        5. ¡Buena suerte y que gane el mejor!
        ")
        .BorderColor(Color.Blue)
        .Header("Reglas del Juego");

        AnsiConsole.Write(panel);
    }
}
        