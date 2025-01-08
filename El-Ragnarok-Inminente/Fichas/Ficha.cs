using System;
using System.Collections.Generic;

public abstract class Ficha
{
    public string Nombre { get; set; }
    public int Vida { get; set; }
    public string Simbolo { get; set; }
    public int Velocidad { get; set; }
    public List<Habilidad> Habilidades { get; set; }
    public bool InmuneATramapa { get; set; }
    public bool Paralizado { get; set; }
    public bool Invisible { get; set; }
    public bool Inmovilizado { get; set; }
    public int TiempoInmovilizacion { get; set; }
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }

    public Ficha(string nombre,int vida, int velocidad, string simbolo)
    {
        Nombre = nombre;
        Vida = vida;
        Velocidad = velocidad;
        Simbolo = simbolo;
        InmuneATramapa = false;
        Paralizado = false;
        Invisible = false;
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