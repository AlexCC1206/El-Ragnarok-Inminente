using System;
using System.Collections.Generic;

public class Turno
{
    private List<Jugador> jugadores;
    private int indiceJugadorActual;

    public Turno(List<Jugador> jugadores)
    {
        this.jugadores = jugadores;
        indiceJugadorActual = 0;
    }

    public Jugador ObtenerJugadorActual()
    {
        return jugadores[indiceJugadorActual];
    }

    public void SiguienteTurno()
    {
        indiceJugadorActual = (indiceJugadorActual + 1) % jugadores.Count;
    }
}