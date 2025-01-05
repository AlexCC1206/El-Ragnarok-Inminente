using System;

class Program
{
    static void Main()
    {
        Tablero tablero = new Tablero(20);
        tablero.Imprimir();

        Ficha loki = new Loki();
        Ficha odin = new Odin();
        Ficha baldur = new Baldur();
        Ficha heimdall = new Heimdall();
        Ficha tyr = new Tyr();

        loki.UsarHabilidad();
        odin.UsarHabilidad();
        baldur.UsarHabilidad();
        heimdall.UsarHabilidad();
        tyr.UsarHabilidad();
    }
}
