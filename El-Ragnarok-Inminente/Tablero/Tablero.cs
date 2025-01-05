using System;

public class Tablero
{
    private char[,] celdas;
    private int tamaño;
    private Random random;
    private List<Trampa> trampas;


    public Tablero(int n)
    {
        tamaño = n;
        celdas = new char[n, n];
        random = new Random();
        trampas = new List<Trampa>();
        Inicializar();
    }

    private void Inicializar()
    {
        // Inicializa todas las celdas con un punto
        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                celdas[i, j] = '.'; 
            }
        }

        // Establece los bordes como obstáculos
        for (int i = 0; i < tamaño; i++)
        {
            celdas[0, i] = '#'; // Primera fila
            celdas[tamaño - 1, i] = '#'; // Última fila
            celdas[i, 0] = '#'; // Primera columna
            celdas[i, tamaño - 1] = '#'; // Última columna
        }

        AñadirObstaculos();
        AñadirTrampas();
    }

    private void AñadirObstaculos()
    {
        int numObstaculos = (tamaño - 2) * (tamaño - 2) / 5; // Ejemplo: 20% del tablero serán obstáculos
        for (int i = 0; i < numObstaculos; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, tamaño - 1); // Evita los bordes
                y = random.Next(1, tamaño - 1); // Evita los bordes
            } while (celdas[x, y] != '.' || !EsAccesible(x, y)); // Asegura que no se sobreescriba un obstáculo existente y que el tablero siga siendo accesible

            celdas[x, y] = '#'; // Representa un obstáculo con '#'
        }
    }

    private void AñadirTrampas()
    {
        int numTrampas = (tamaño - 2) * (tamaño - 2) / 10; // Ejemplo: 10% del tablero interno serán trampas
        for (int i = 0; i < numTrampas; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, tamaño - 1); // Evita los bordes
                y = random.Next(1, tamaño - 1); // Evita los bordes
            } while (celdas[x, y] != '.' || !EsAccesible(x, y)); // Asegura que no se sobreescriba una trampa existente y que el tablero siga siendo accesible

            Trampa trampa = GenerarTrampaAleatoria();
            celdas[x, y] = trampa.Simbolo; // Representa una trampa con su símbolo
            trampas.Add(trampa);
        }
    }

    private Trampa GenerarTrampaAleatoria()
    {
        int tipoTrampa = random.Next(3);
        switch (tipoTrampa)
        {
            case 0:
                return new WindTrap();
            case 1:
                return new RootTrap();
            case 2:
                return new JotunheimTrap();
            default:
                return new WindTrap();
        }
    }

    private bool EsAccesible(int obstaculoX, int obstaculoY)
    {
        // Temporarily place the obstacle
        celdas[obstaculoX, obstaculoY] = '#';

        // Check if there's a path from (1, 1) to (tamaño-2, tamaño-2)
        bool accesible = HayCamino(1, 1, tamaño - 2, tamaño - 2);

        // Remove the temporary obstacle
        celdas[obstaculoX, obstaculoY] = '.';

        return accesible;
    }

    public bool HayCamino(int startX, int startY, int endX, int endY)
    {
        if (celdas[startX, startY] == '#' || celdas[endX, endY] == '#')
            return false;

        bool[,] visitado = new bool[tamaño, tamaño];
        Queue<(int, int)> cola = new Queue<(int, int)>();
        cola.Enqueue((startX, startY));
        visitado[startX, startY] = true;

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        while (cola.Count > 0)
        {
            var (x, y) = cola.Dequeue();
            if (x == endX && y == endY)
                return true;

            for (int i = 0; i < 4; i++)
            {
                int nuevoX = x + dx[i];
                int nuevoY = y + dy[i];

                if (nuevoX >= 0 && nuevoX < tamaño && nuevoY >= 0 && nuevoY < tamaño && !visitado[nuevoX, nuevoY] && celdas[nuevoX, nuevoY] != '#')
                {
                    cola.Enqueue((nuevoX, nuevoY));
                    visitado[nuevoX, nuevoY] = true;
                }
            }
        }
        
        return false;
    }


    public void Imprimir()
    {
        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                Console.Write(celdas[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}