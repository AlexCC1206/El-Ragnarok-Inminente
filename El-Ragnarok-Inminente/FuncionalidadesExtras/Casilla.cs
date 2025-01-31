using Spectre.Console;
using System;

public class Casilla
{
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }
    public string Simbolo { get; set; } 
    //public int DuracionEfecto { get; set; } // Duración del efecto en turnos

    public Casilla(int posicionX, int posicionY, string simbolo)
    {
        PosicionX = posicionX;
        PosicionY = posicionY;
        Simbolo = simbolo;
        //DuracionEfecto = duracionEfecto;
    }

    public virtual void AplicarEfecto(Ficha ficha)
    {
        
    }
    
}