using System;
using System.Drawing;

public class Tyr : Ficha
{
    public Tyr() : base("Tyr, el Dios de la Guerra", 130, 3, "[T]", Color.Red)
    {   
        Habilidades.Add(new JusticiaImplacable());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this);
        }
    }
}