using System;

public class PasoDeLasSombras : Habilidad
{
    private Random random = new Random();
    public Tablero tablero;
    public int x, y;

    public PasoDeLasSombras(Tablero tablero) : base("Paso de las Sombras", 2)
    {
        this.tablero = tablero;
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            // Buscar una casilla vacía aleatoria
            int maxIntentos = 100; // Límite de iteraciones
            int intentos = 0;
            bool encontrado = false;

            while (intentos < maxIntentos && !encontrado)
            {
                x = random.Next(0, tablero.tamaño);
                y = random.Next(0, tablero.tamaño);

                if (tablero.celdas[x, y] == " " )//&& tablero.EsMovimientoValido(ficha, x, y))
                {
                    encontrado = true;
                }
                else
                {
                    intentos++;
                }
            }

            if (encontrado)
            {
                
                // Actualizar posición de la ficha
                tablero.celdas[ficha.PosicionX, ficha.PosicionY] = " "; // Liberar la casilla anterior
                ficha.PosicionX = x;
                ficha.PosicionY = y;
                tablero.celdas[x, y] = ficha.Simbolo; // Marcar la nueva posición
                //
            
                
                
                
                Console.WriteLine($"{Nombre} se teletransportó a ({x}, {y}).");
            }
            else
            {
                Console.WriteLine("No se pudo encontrar una casilla vacía aleatoria.");
            }
        }
        else
        {
            Console.WriteLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
        }
        
    }
}