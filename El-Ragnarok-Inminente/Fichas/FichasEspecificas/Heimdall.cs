using System;
using System.Drawing;

public class Heimdall : Ficha
{
    public Heimdall() : base("Heimdall, el Guardián de Asgard", 140, 3, "[H]", Color.Silver)
    {
        Habilidades.Add(new VisionProfetica());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this);
        }
    }
}