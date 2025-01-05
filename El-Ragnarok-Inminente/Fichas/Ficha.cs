using System;
using System.Collections.Generic;

public abstract class Ficha
{
    public string Nombre { get; set; }
    public int Velocidad { get; set; }
    public List<Habilidad> Habilidades { get; set; }
    public bool InmuneATramapa { get; set; }
    public bool Paralizado { get; set; }
    public bool InmuneAInteracciones { get; set; }
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }

    public Ficha(string nombre, int velocidad)
    {
        Nombre = nombre;
        Velocidad = velocidad;
        InmuneATramapa = false;
        Paralizado = false;
        InmuneAInteracciones = false;
        Habilidades = new List<Habilidad>();
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