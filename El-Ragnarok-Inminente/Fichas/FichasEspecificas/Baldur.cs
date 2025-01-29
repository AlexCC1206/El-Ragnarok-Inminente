using System;
using System.Drawing;

public class Baldur : Ficha
{
    public Baldur() : base("Baldur, el Dios de la Luz", 3, "B")
    {
        Habilidades.Add(new ResilienciaInmortal());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this);
        }
    }
}