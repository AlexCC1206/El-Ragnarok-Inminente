using System;

class Program
{
    static void Main()
    {
        Tablero tablero = new Tablero(21);
        Jugador jugador1 = new Jugador("Jugador 1");
        Jugador jugador2 = new Jugador("Jugador 2");

        // Añadir fichas a los jugadores
        jugador1.AñadirFicha(new Loki());
        jugador1.AñadirFicha(new Odin());
        jugador2.AñadirFicha(new Baldur());
        jugador2.AñadirFicha(new Heimdall());

        Juego juego = new Juego(tablero, new List<Jugador> { jugador1, jugador2 });
        juego.Iniciar();
    
        
    }
}
