using System;
using System.Collections.Generic;
using Spectre.Console;
using Spectre.Console.Rendering;

public static class InterfazJuego
{ 
    public static void MostrarMenuPrincipal()
    {   
        //try
        //{        
            AnsiConsole.Clear();
        
            var title = new FigletText("El Ragnarok Inminente")
                .Color(Color.CadetBlue) 
                .Centered();
        
            var shadowedTitle = new Panel(title)
                .BorderColor(Color.Gold1) 
                .Padding(2, 2, 2, 2)
                .RoundedBorder()
                .Border(BoxBorder.Double);
            

                // Mostrar el diseño
            AnsiConsole.Write(shadowedTitle);
            
            var opciones = new[] { "Nueva Partida", "Instrucciones", "Salir" };
            var prompt = new SelectionPrompt<string>()
                .Title("[bold navy]Menú Principal[/]")
                .AddChoices(opciones)
                .HighlightStyle(new Style(Color.Gold1));

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
                    Console.Clear();
                
                    var textoSalida = new FigletText($"¡Gracias por jugar!")
                    .Color(Color.Red)
                    .Centered();

                    AnsiConsole.Write(textoSalida);
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

    private static void IniciarNuevaPartida()
    {      
        //try
        //{
            Tablero tablero = new Tablero(27);
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
        
        var reglas = new Rule("[italic cyan3]Instrucciones del Juego[/]");
        AnsiConsole.Write(reglas);

        var panel = new Panel(@"
        1. Usa las flechas para mover la ficha (Jugador 1) y (Jugador 2).
        2. Evita las trampas y obstáculos para llegar a la salida.
        3. ¡Diviértete!
        4. [bold yellow]Presiona cualquier tecla para volver al menú principal...[/]
        ")
        .BorderColor(Color.Silver)
        .Header("[italic silver]Reglas[/]");

        AnsiConsole.Write(panel);
        Console.ReadKey(true);
        MostrarMenuPrincipal();
    }

    public static string MostrarOpciones()
    {
        var opciones = new[] { "Moverse", "Usar habilidad"};
        var prompt = new SelectionPrompt<string>()
            .Title("Opciones:")
            .AddChoices(opciones)
            .HighlightStyle(new Style(Color.Gold1));
        var opcionSeleccionada = AnsiConsole.Prompt(prompt);
        
        return opcionSeleccionada;
    }

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
}