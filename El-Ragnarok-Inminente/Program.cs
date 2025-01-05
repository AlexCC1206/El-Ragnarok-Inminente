using System;

class Program
{
    static void Main()
    {
        Tablero tablero = new Tablero(20);
        Jugador jugador1 = new Jugador("Jugador 1");
        Jugador jugador2 = new Jugador("Jugador 2");

        Ficha loki = new Loki();
        Ficha odin = new Odin();
        Ficha baldur = new Baldur();
        Ficha heimdall = new Heimdall();
        Ficha tyr = new Tyr();

        jugador1.AñadirFicha(loki);
        jugador1.AñadirFicha(odin);
        jugador2.AñadirFicha(baldur);
        jugador2.AñadirFicha(heimdall);

        tablero.AñadirFicha(loki, 1, 1);
        tablero.AñadirFicha(odin, 1, 18);
        tablero.AñadirFicha(baldur, 18, 1);
        tablero.AñadirFicha(heimdall, 18,18);

        Juego juego = new Juego(tablero, new List<Jugador> { jugador1, jugador2 });
        juego.Iniciar();
        
    }
}
