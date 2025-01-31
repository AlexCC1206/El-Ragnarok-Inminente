using System;
using System.Collections.Generic;
using System.Drawing;

public abstract class Ficha
{
    public string Nombre { get; set; }
    public string Simbolo { get; set; }
    public int Velocidad { get; set; }
    public int VelocidadBase { get; set; }
    public List<Habilidad> Habilidades { get; set; }
    public Tablero Tablero { get; set; }
    public bool InmuneATramapa { get; set; }
    public bool Paralizado { get; set; }
    public bool Invisible { get; set; }
    public bool Atrapado;
    public bool Congelado;
    public int turnosAtrapado;
    public int turnosCongelado;
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }

    public Ficha(string nombre, int velocidad, string simbolo)
    {
        Nombre = nombre;
        Velocidad = velocidad;
        VelocidadBase = velocidad;
        Simbolo = simbolo;
        InmuneATramapa = false;
        Paralizado = false;
        Invisible = false;
        Atrapado = false;
        Congelado = false;
        Habilidades = new List<Habilidad>();
        Tablero = new Tablero(27);
        turnosAtrapado = 0;
        turnosCongelado = 0;
    }

    public void ReducirEnfriamientoHabilidades()
    {
        foreach (var habilidad in Habilidades)
        {
            habilidad.ReducirEnfriamiento();
        }
    }
    
    public abstract void UsarHabilidad(int indice);   
}