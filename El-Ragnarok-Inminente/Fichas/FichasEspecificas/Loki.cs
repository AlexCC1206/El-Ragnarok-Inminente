using System;
using System.Drawing;


public class Loki : Ficha
{
    public Loki() : base("Loki, el Embaucador", 3, "L")
    {
        //Habilidades.Add(new IlusionSombria());
        Habilidades.Add(new PasoDeLasSombras(new Tablero(27)));
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this);
        }
    }
}