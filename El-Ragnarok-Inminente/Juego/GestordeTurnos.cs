using System;
using System.Collections.Generic;

public class Turno
{
    public List<Jugador> jugadores; // Lista de jugadores en el juego
    private int indiceJugadorActual; // Índice del jugador actual

    // Constructor: Inicializa la lista de jugadores y establece el primer turno
    public Turno(List<Jugador> jugadores)
    {
        this.jugadores = jugadores;
        indiceJugadorActual = 0; // Comienza con el primer jugador
    }

    // Método para obtener el jugador actual
    public Jugador ObtenerJugadorActual()
    {
        return jugadores[indiceJugadorActual];
    }

    // Método para avanzar al siguiente turno
    public void SiguienteTurno()
    {
        indiceJugadorActual = (indiceJugadorActual + 1) % jugadores.Count; // Alterna entre jugadores
    }
}