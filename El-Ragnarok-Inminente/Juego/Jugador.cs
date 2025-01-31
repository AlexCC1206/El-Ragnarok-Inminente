using System.Collections.Generic;

public class Jugador
{
    public string Nombre { get; set; }
    public List<Ficha> Fichas { get; set; }
    public int Salud { get; set; }
    public int MovimientosRestantes { get; set; }
    
    
    public Jugador(string nombre)
    {
        Nombre = nombre;
        Fichas = new List<Ficha>();
    }

    public void AñadirFicha(Ficha ficha)
    {
        Fichas.Add(ficha);
    }

    
}
