using System;
using System.Collections.Generic;
using Spectre.Console;
using Spectre.Console.Rendering;

public static class InterfazJuego
{   
    /*
    public static void MostrarTextoAnimado(string texto, Color color, int delay = 100)
    {
        foreach (char c in texto)
        {
            AnsiConsole.Markup($"[{color}]{c}[/]");
            System.Threading.Thread.Sleep(delay); // Controla la velocidad de la animación
        }
        AnsiConsole.WriteLine(); // Salto de línea al final
    }
    */

    public static void MostrarMenuPrincipal()
    {   
        //try
        //{        
            AnsiConsole.Clear();
        
            var title = new FigletText("El Ragnarok Inminente")
                .Color(Color.DarkGoldenrod) 
                .Centered();
        
            var shadowedTitle = new Panel(title)
                .BorderColor(Color.Violet) 
                .Padding(2, 2, 2, 2)
                .RoundedBorder()
                .Border(BoxBorder.Double);
            

                // Mostrar el diseño
            AnsiConsole.Write(shadowedTitle);
            
            var opciones = new[] { "Nueva Partida", "Instrucciones", "Salir" };
            var prompt = new SelectionPrompt<string>()
                .Title("[bold cyan3]Menú Principal[/]")
                .AddChoices(opciones);

            var opcionSeleccionada = AnsiConsole.Prompt(prompt);

            switch (opcionSeleccionada)
            {
                case "Nueva Partida":
                    IniciarNuevaPartida();
                    break;
                case "Instrucciones":
                    MostrarInstrucciones();
                    break;
                case "Salir":
                    AnsiConsole.MarkupLine("[bold darkred_1]¡Gracias por jugar![/]");
                    Environment.Exit(0);
                    break;
            }
            /*
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error en el menú: {ex.Message}[/]");
            AnsiConsole.MarkupLine("[yellow]Volviendo al menú principal...[/]");
            MostrarMenuPrincipal();
        }   
        */
    }
    /*
    public static void MostrarTitulo()
    {
        var title = new FigletText("El Ragnarok Inminente")
        .Color(Color.Gold1) // Dorado brillante (#FFD700)
        .Centered();

        var panel = new Panel(title)
        .BorderColor(Color.Blue) // Borde azul para un toque místico
        .Header("[bold yellow]Bienvenido al Ragnarok[/]") // Texto de bienvenida en amarillo
        .Padding(2, 2, 2, 2)
        .RoundedBorder()
        .Border(BoxBorder.Double);

        // Añadir sombra y efectos al título
        var shadowEffect = new Markup("[red]El Ragnarok Inminente[/]") // Sombra roja
        .Overflow(Overflow.Ellipsis)
        .Alignment(Justify.Center);

        // Combinar todo en un layout
        var layout = new Layout("Root")
        .SplitRows(
        new Layout("Title").Update(panel),
        new Layout("Shadow").Update(shadowEffect)
    );

// Mostrar el diseño
AnsiConsole.Write(layout);
            
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
    */

    private static void IniciarNuevaPartida()
    {      
        //try
        //{
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

            SeleccionarFicha(jugador1);
            SeleccionarFicha(jugador2);

            Juego juego = new Juego(tablero, new List<Jugador> { jugador1, jugador2 });
            juego.Iniciar();
        //}
        /*
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            AnsiConsole.MarkupLine("[yellow]Volviendo al menú principal...[/]");
            MostrarMenuPrincipal();
        }
        */
    }

    public static void MostrarInstrucciones()
    {
        var reglas = new Rule("[cyan3]Instrucciones del Juego[/]");
        AnsiConsole.Write(reglas);

        var panel = new Panel(@"
        1. Usa WASD para mover la ficha (Jugador 1) o las flechas (Jugador 2).
        2. Evita las trampas y obstáculos para llegar a la salida.
        3. ¡Diviértete!
        4. [bold yellow]Presiona cualquier tecla para volver al menú principal...[/]
        ")
        .BorderColor(Color.DeepSkyBlue1)
        .Header("[cyan3]Reglas[/]");

        AnsiConsole.Write(panel);

        Console.ReadKey(true);
        MostrarMenuPrincipal();
    }

    public static void MostrarLeyenda()
    {
        var leyenda = new Panel(new Table()
            .AddColumn("Símbolo")
            .AddColumn("Descripción")
            .AddRow("[bold gold1 on navy]+[/]", "Salida")
            .AddRow("[deepskyblue1]F[/]", "Ficha")
            .AddRow("[blink bold mediumpurple2]![/]", "Trampa")
            .AddRow("[white]■[/]", "Obstáculo"))
        .Header("[cyan3]Leyenda[/]")
        .BorderColor(Color.DeepSkyBlue1);

        AnsiConsole.Write(leyenda);
    }
/*
    public static Panel MostrarInformacionJugador(Jugador jugador)
    {
        var panel = new Panel(new Table()
            .AddColumn("Estadística")
            .AddColumn("Valor")
            .AddRow("Nombre", jugador.Nombre)
            .AddRow("Salud", jugador.Salud.ToString())
            .AddRow("Movimientos Restantes", jugador.MovimientosRestantes.ToString()))
        .Header("[cyan3]Estadísticas en Tiempo Real[/]")
        .BorderColor(Color.DeepSkyBlue1);

        AnsiConsole.Write(panel);
        return panel;
    }
*/

    public static string MostrarOpciones()
    {
        var opciones = new[] { "Moverse", "Usar habilidad", "Pasar turno" };
        var prompt = new SelectionPrompt<string>()
            .Title("Opciones:")
            .AddChoices(opciones);
        var opcionSeleccionada = AnsiConsole.Prompt(prompt);
        AnsiConsole.MarkupLine($"Has seleccionado: [white]{opcionSeleccionada}[/]");
        
        return opcionSeleccionada;
    }
/*
    public static void MostrarMensajeEvento(string mensaje, string tipo)
    {
        switch (tipo)
        {
            case "negativo":
                AnsiConsole.MarkupLine($"[bold darkred_1]{mensaje}[/]");
                break;
            case "exitoso":
                AnsiConsole.MarkupLine($"[bold springgreen2_1]{mensaje}[/]");
                break;
            case "alerta":
                AnsiConsole.MarkupLine($"[bold yellow3_1]{mensaje}[/]");
                break;
            default:
                AnsiConsole.MarkupLine(mensaje);
                break;
        }
    }
    */

    private static void SeleccionarFicha(Jugador jugador)
    {
        var prompt = new SelectionPrompt<string>()
            .Title($"{jugador.Nombre}, selecciona una ficha:")
            .AddChoices(jugador.Fichas.ConvertAll(f => f.Nombre));

        string seleccion = AnsiConsole.Prompt(prompt);
        Ficha fichaSeleccionada = jugador.Fichas.Find(f => f.Nombre == seleccion);
        
        if (fichaSeleccionada == null)
        {
            AnsiConsole.MarkupLine("[red]Error: La ficha seleccionada no es válida.[/]");
            SeleccionarFicha(jugador); // Volver a solicitar la selección
            return;
        }

        AnsiConsole.MarkupLine($"[green]{jugador.Nombre} ha seleccionado a {fichaSeleccionada.Nombre}.[/]");
        
        // Limpiar fichas del jugador y añadir la ficha seleccionada
        jugador.Fichas.Clear();
        jugador.AñadirFicha(fichaSeleccionada);
    }
    

    

    /*
    public void MostrarIndicadoresVisuales()
    {
        var tablero = new Tablero(10);
        tablero.AñadirFicha(new Ficha("Ficha1"), 1, 1);
        tablero.AñadirTrampa(3, 3);
        tablero.AñadirObstaculo(5, 5);

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold yellow]Juego de Tablero[/]");
        tablero.Imprimir();
        MostrarLeyenda();
        MostrarInformacionJugador(new Jugador("Jugador 1"));
        var opcion = MostrarMenuOpciones();
        AnsiConsole.MarkupLine($"Has seleccionado: [bold]{opcion}[/]");
    }
    */

    
/*
    public static void MostrarMenuPrincipal()
    {
        var opciones = new[] { "Moverse", "Usar Habilidad", "Pasar Turno", "Salir" };
        var opcionSeleccionada = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Menú Principal[/]")
                .PageSize(10)
                .AddChoices(opciones)
                .HighlightStyle(new Style(Color.Yellow)));

        switch (opcionSeleccionada)
        {
            case "Moverse":
                // Lógica para moverse
                ConfirmAction("Moverse");
                break;
            case "Usar Habilidad":
                MostrarSubmenuUsarHabilidad();
                break;
            case "Pasar Turno":
                ConfirmAction("Pasar Turno");
                break;
            case "Salir":
                ConfirmAction("Salir del juego");
                break;
        }
    }
*/
/*
    public static void MostrarSubmenuUsarHabilidad()
    {
        var habilidades = new[] { "Ataque", "Defensa", "Curación", "Volver" };
        var habilidadSeleccionada = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Usar Habilidad[/]")
                .PageSize(10)
                .AddChoices(habilidades)
                .HighlightStyle(new Style(Color.Yellow)));

        if (habilidadSeleccionada == "Volver")
        {
            MostrarMenuPrincipal();
        }
        else
        {
            ConfirmAction($"Habilidad usada: {habilidadSeleccionada}");
        }
    }
*/
/*
    public static void ConfirmAction(string action)
    {
        AnsiConsole.MarkupLine($"[green]Acción realizada: {action}[/]");
    }

    public static void ShowErrorMessage(string message)
    {
        AnsiConsole.MarkupLine($"[red]Error: {message}[/]");
    }

    public static void UpdateStatus(int turnsRemaining, int healthPoints)
    {
        AnsiConsole.MarkupLine($"[blue]Turnos restantes: {turnsRemaining}[/]");
        AnsiConsole.MarkupLine($"[blue]Puntos de salud: {healthPoints}[/]");
    }

    

    public static void ShowNotification(string message)
    {
        AnsiConsole.MarkupLine($"[yellow]{message}[/]");
    }

    public static void MostrarMensajeEvento(string mensaje, string tipoEvento)
{
    switch (tipoEvento)
    {
        case "negativo":
            AnsiConsole.MarkupLine($"[red]{mensaje}[/]");
            break;
        case "exitoso":
            AnsiConsole.MarkupLine($"[green]{mensaje}[/]");
            break;
        case "alerta":
            AnsiConsole.MarkupLine($"[yellow]{mensaje}[/]");
            break;
        default:
            AnsiConsole.MarkupLine(mensaje);
            break;
    }
}
*/
    
    
    
    
    
}