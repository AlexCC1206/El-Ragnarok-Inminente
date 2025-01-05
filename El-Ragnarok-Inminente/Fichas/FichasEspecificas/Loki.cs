using System;

public class Loki : Ficha
{
    public Loki() : base("Loki, el Embaucador", 3)
    {
        Habilidades.Add(new IlusionSombria());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this, this);
        }
    }
}