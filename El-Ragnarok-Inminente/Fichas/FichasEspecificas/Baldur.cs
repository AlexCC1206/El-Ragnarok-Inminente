using System;

public class Baldur : Ficha
{
    public Baldur() : base("Baldur, el Dios de la Luz", 3)
    {
        Habilidades.Add(new ResilienciaInmortal());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this, this);
        }
    }
}