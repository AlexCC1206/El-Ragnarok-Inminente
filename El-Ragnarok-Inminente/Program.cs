using System;

class Program
{
    static void Main()
    {
        Tablero tablero = new Tablero(20);
        Ficha loki = new Loki();
        Ficha odin = new Odin();
        Ficha baldur = new Baldur();
        Ficha heimdall = new Heimdall();
        Ficha tyr = new Tyr();

        tablero.AñadirFicha(loki, 1, 1);
        tablero.AñadirFicha(odin, 2, 2);
        tablero.AñadirFicha(baldur, 3, 3);
        tablero.AñadirFicha(heimdall, 4, 4);
        tablero.AñadirFicha(tyr, 5, 5);

        tablero.Imprimir();

        // Mueve las fichas y aplica efectos de trampas
        tablero.MoverFicha(loki, 1, 2);
        tablero.MoverFicha(odin, 2, 3);
        tablero.MoverFicha(baldur, 3, 4);
        tablero.MoverFicha(heimdall, 4, 5);
        tablero.MoverFicha(tyr, 5, 6);

        tablero.Imprimir();
    }
}
