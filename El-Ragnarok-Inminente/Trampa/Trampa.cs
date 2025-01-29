using System;

public abstract class Trampa
{
    public string Nombre { get; set; }
    public string Simbolo { get; set; }
    public string Descripcion { get; set; }
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }

    public Trampa(string nombre, string descripcion, string simbolo)
    {
        Nombre = nombre;
        Simbolo = simbolo;
        Descripcion = descripcion;
    }

    public abstract void Activar(Ficha ficha);

    public void VerificarEscudo(Ficha ficha)
    {
        if (ficha.InmuneATramapa)
        {
            Console.WriteLine($"{ficha.Nombre} está protegido y no sufrirá penalización la próxima vez que caiga en una trampa.");
            ficha.InmuneATramapa = false; // El escudo dura solo un turno
        }   
    }
}