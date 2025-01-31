using System;
using System.Drawing;


public class Loki : Ficha
{
    public Loki() : base("Loki, el Embaucador", 3, "L")
    {
        Habilidades.Add(new VelocidadDelViento());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this);
        }
    }
}