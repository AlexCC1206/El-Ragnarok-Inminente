using System;

public class Heimdall : Ficha
{
    public Heimdall() : base("Heimdall, el Guardián de Asgard", 3, 'H')
    {
        Habilidades.Add(new VisionProfetica());
    }

    public override void UsarHabilidad(int indice)
    {
        if (indice >= 0 && indice < Habilidades.Count)
        {
            Habilidades[indice].Usar(this, this);
        }
    }
}