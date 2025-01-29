using System;

public class PiedrasdeJotunheim : Trampa
{
    public Tablero tablero;
    private bool[,] bloqueado; // Arreglo de booleanos para mantener el estado de bloqueo
    private int turnosRestantes = 1; 
    public PiedrasdeJotunheim(int filas, int columnas) : base("Piedras de Jotunheim", "Bloquea el movimiento del jugador", "X")
    {
        tablero = tablero;
        bloqueado = new bool[filas, columnas];
    }

    public override void Activar(Ficha ficha)
    {
        Random rnd = new Random();
        int probabilidad = rnd.Next(0, 2); // 50% de probabilidad de ser afectado (0 o 1)

        if (probabilidad == 0) // 50% de probabilidad de ser afectado
        {
            if (ficha.PosicionX >= 0 && ficha.PosicionX < bloqueado.GetLength(0) && ficha.PosicionY >= 0 && ficha.PosicionY < bloqueado.GetLength(1))
            {
                bloqueado[ficha.PosicionX, ficha.PosicionY] = true; // Se bloquea la casilla
                tablero.celdas[ficha.PosicionX, ficha.PosicionY] = "X";
                Console.WriteLine($"¡La {ficha.Nombre} ha caído en Piedras de Jotunheim! La casilla está bloqueada durante {turnosRestantes} turno.");
            }
            //System.Threading.Thread.Sleep(3000);  
        }
        else
        {
            Console.WriteLine($"{ficha.Nombre} evita las Piedras de Jotunheim y no se bloquea la casilla.");
        }
           
    }

    public bool EstaBloqueado(int x, int y)
    {
        return bloqueado[x, y];
    }

    public void Desbloquear(int x, int y)
    {
        bloqueado[x, y] = false;
    }
}