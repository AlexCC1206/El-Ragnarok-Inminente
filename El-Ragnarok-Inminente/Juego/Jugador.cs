using System.Collections.Generic;

public class Jugador
{
    public string Nombre { get; set; } // Nombre del jugador
    public List<Ficha> Fichas { get; set; } // Lista de fichas asignadas al jugador
    public int Salud { get; set; }
    public int MovimientosRestantes { get; set; }
    
    // Constructor: Inicializa un jugador con un nombre y una lista vacía de fichas
    public Jugador(string nombre)
    {
        Nombre = nombre;
        Fichas = new List<Ficha>();
    }

    // Método para añadir una ficha al jugador
    public void AñadirFicha(Ficha ficha)
    {
        Fichas.Add(ficha);
    }

    
}
