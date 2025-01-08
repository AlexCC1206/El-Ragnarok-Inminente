using System;
using System.Drawing;

public class Odin : Ficha
{
    public Odin() : base("Odin, el Padre de Todos", 150, 2, "O", Color.Gold)
    {  
        Habilidades.Add(new RayoDeOdin());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this);
        }
    }
}