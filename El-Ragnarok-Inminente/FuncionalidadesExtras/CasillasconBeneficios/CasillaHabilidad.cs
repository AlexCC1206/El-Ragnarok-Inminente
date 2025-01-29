using System;

public class CasillaHabilidad : Casilla
{
    public Habilidad HabilidadAdicional { get; set; }

    public CasillaHabilidad(int posicionX, int posicionY, Habilidad habilidadAdicional)
        : base(posicionX, posicionY, "*")
    {
        HabilidadAdicional = habilidadAdicional;
    }

    public override void AplicarEfecto(Ficha ficha)
    {
        ficha.Habilidades.Add(HabilidadAdicional);
        Console.WriteLine($"{ficha.Nombre} ha obtenido la habilidad {HabilidadAdicional.Nombre}");
        System.Threading.Thread.Sleep(3000);
    }
}