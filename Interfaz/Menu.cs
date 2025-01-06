
using System;



public static class InterfazJuego

{

    public static void MostrarTitulo()

    {

        Console.WriteLine("Bienvenido a El Ragnarok Inminente!");

    }



    public static void MostrarTablero()

    {

        Console.WriteLine("Mostrando el tablero del juego...");

    }



    public static void MostrarOpciones()

    {

        Console.WriteLine("1. Iniciar Juego");

        Console.WriteLine("2. Salir");

    }



    public static int ObtenerOpcionUsuario()

    {

        Console.Write("Seleccione una opción: ");

        return int.Parse(Console.ReadLine());

    }



    public static void ProcesarOpcion(int opcion)

    {

        switch (opcion)

        {

            case 1:

                Console.WriteLine("Iniciando el juego...");

                break;

            case 2:

                Console.WriteLine("Saliendo del juego...");

                break;

            default:

                Console.WriteLine("Opción no válida.");

                break;

        }

    }

}
