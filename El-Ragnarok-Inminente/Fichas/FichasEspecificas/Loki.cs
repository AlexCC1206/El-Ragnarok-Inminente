using System;

public class Loki : Ficha
{
    public Loki() : base("Loki, el Embaucador", 120, 3, "L")
    {
        Habilidades.Add(new IlusionSombria());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this);
        }
    }
}