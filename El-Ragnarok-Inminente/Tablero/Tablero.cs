using System;
using Spectre.Console;
using System.Text;

public class Tablero
{
    public string[,] celdas;
    public int tamaño;
    private Random random;
    private List<Trampa> trampas;
    private List<Ficha> fichas;
    private (int, int) salida; // Coordenadas de la salida     
    public List<Casilla> casillas;

    public Tablero(int n)
    {
        tamaño = n;
        celdas = new string[n, n];
        random = new Random();
        trampas = new List<Trampa>();
        fichas = new List<Ficha>();
        casillas = new List<Casilla>();
        
        Inicializar();
        GenerarSalida();
        AñadirObstaculos();
        AñadirTrampas();
        AñadirCasillasBeneficios();
    }

    private void Inicializar()
    {
        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                celdas[i, j] = " "; 
            }
        }

        // Establece los bordes como obstáculos
        for (int i = 0; i < tamaño; i++)
        {
            celdas[0, i] = "■"; // Primera fila
            celdas[tamaño - 1, i] = "■"; // Última fila
            celdas[i, 0] = "■"; // Primera columna
            celdas[i, tamaño - 1] = "■"; // Última columna
        }
    }

    private void GenerarSalida()
    {
        // Establece la salida en la posición (13, 13)
        int x = 13;
        int y = 13;

        salida = (x, y);
        celdas[x, y] = "+"; // Representación de la salida
    }

    public bool EsSalida(int x, int y)
    {
        return salida.Item1 == x && salida.Item2 == y;
    }

    private void AñadirObstaculos()
    {
        int numObstaculos = (tamaño - 2) * (tamaño - 2) * 30 / 100; // Cantidad de obstáculos en porcentaje
        for (int i = 0; i < numObstaculos; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, tamaño - 1); // Evita los bordes
                y = random.Next(1, tamaño - 1); // Evita los bordes
            } while (celdas[x, y] != " " || !EsAccesible(x, y)); 

            celdas[x, y] = "■"; 
        }
    }

    private void AñadirTrampas()
    {
        int numTrampas = (tamaño - 2) * (tamaño - 2) *  3 / 100; // Cantidad de trampas en porcentaje
        for (int i = 0; i < numTrampas; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, tamaño - 1); // Evita los bordes
                y = random.Next(1, tamaño - 1); // Evita los bordes
            } while (celdas[x, y] != " " || !EsAccesible(x, y));

            Trampa trampa = GenerarTrampaAleatoria();
            trampa.PosicionX = x;
            trampa.PosicionY = y;
            celdas[x, y] = trampa.Simbolo; 
            trampas.Add(trampa);
        }
    }

    private Trampa GenerarTrampaAleatoria()
    {
        int tipoTrampa = random.Next(3);
        switch (tipoTrampa)
        {
            case 0:
                return new ColumnasdeFuegodeSurtur();
            case 1:
                return new WindTrap();
            case 2:
                return new JotunheimTrap();
            default:
                return new ColumnasdeFuegodeSurtur();
        }
    }

    public void AplicarEfectoTrampa(Ficha ficha)
    {
        foreach (var trampa in trampas)
        {
            if (ficha.PosicionX == trampa.PosicionX && ficha.PosicionY == trampa.PosicionY)
            {
                trampa.VerificarEscudo(ficha);
                if (!ficha.InmuneATramapa)
                {
                    trampa.Activar(ficha);
                } 
            }
        }
    }

    private void AñadirCasillasBeneficios()
    {
        int numCasillasBeneficios = (tamaño - 2) * (tamaño - 2) * 2 / 100; // Ejemplo: 10% del tablero interno serán casillas con beneficios
        for (int i = 0; i < numCasillasBeneficios; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, tamaño - 1); // Evita los bordes
                y = random.Next(1, tamaño - 1); // Evita los bordes
            } while (celdas[x, y] != " " || !EsAccesible(x, y)); // Asegura que no se sobreescriba una casilla existente y que el tablero siga siendo accesible

            Casilla casilla = GenerarCasillaAleatoria();
            casilla.PosicionX = x;
            casilla.PosicionY = y;
            celdas[x, y] = casilla.Simbolo; 
            casillas.Add(casilla);
        }
    }

    private Casilla GenerarCasillaAleatoria()
    {
        int tipoCasilla = random.Next(2); 
        switch (tipoCasilla)
        {
            case 0:
                return new CasillaVelocidad(0, 0 , 1); 
            case 1:
                return new CasillaHabilidad(0 , 0 , new JusticiaImplacable()); 
            default:
                return new CasillaVelocidad(0, 0, 1); 
        }
    }

    public void AplicarEfectoCasillas(Ficha ficha)
    {
        foreach (var casilla in casillas)
        {
            if (ficha.PosicionX == casilla.PosicionX && ficha.PosicionY == casilla.PosicionY)
            {
                casilla.AplicarEfecto(ficha);
            }
        }
    }

    private bool EsAccesible(int obstaculoX, int obstaculoY)
    {
        celdas[obstaculoX, obstaculoY] = "■";

        bool desdeJugador1 = HayCamino(1, 1, salida.Item1, salida.Item2);
        bool desdeJugador2 = HayCamino(tamaño - 2, tamaño - 2, salida.Item1, salida.Item2);

        celdas[obstaculoX, obstaculoY] = " ";

        return desdeJugador1 && desdeJugador2; 
    }

    private bool HayCamino(int startX, int startY, int endX, int endY)
    {
        // Verificación inicial: si inicio o fin son obstáculos ("■"), no hay camino
        if (celdas[startX, startY] == "■" || celdas[endX, endY] == "■")
            return false;

        bool[,] visitado = new bool[tamaño, tamaño];

        // Pila para DFS
        Stack<(int, int)> pila = new Stack<(int, int)>();

        // Iniciamos con el punto de partida
        pila.Push((startX, startY));
        visitado[startX, startY] = true;

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        while (pila.Count > 0)
        {
            // Extraemos coordenada actual (LIFO - último en entrar, primero en salir)
            var (x, y) = pila.Pop();
            if (x == endX && y == endY)
                return true;

            // Exploramos las 4 direcciones posibles
            for (int i = 0; i < 4; i++)
            {
                int nuevoX = x + dx[i];
                int nuevoY = y + dy[i];

                if (nuevoX >= 0 && nuevoX < tamaño && nuevoY >= 0 && nuevoY < tamaño && !visitado[nuevoX, nuevoY] && celdas[nuevoX, nuevoY] != "■")
                {
                    // Agregamos a la pila y marcamos como visitado
                    pila.Push((nuevoX, nuevoY));
                    visitado[nuevoX, nuevoY] = true;
                }
            }
        }
        // Si vaciamos la pila sin encontrar destino: no hay camino
        return false;
    }
    
    public void Imprimir()
    {   
        var canvas = new Canvas(celdas.GetLength(0), celdas.GetLength(1));
        
        // Dibujar casillas del tablero según su tipo
        for (int fila = 0; fila < celdas.GetLength(0); fila++)
        {
            for (int columna = 0; columna < celdas.GetLength(1); columna++)
            {
                Color color = celdas[fila, columna] switch
                {
                    " " => Color.Black, // Casilla vacía
                    "■" => Color.Grey, // Obstáculo
                    "+" => Color.Green, // Salida
                    "X" => Color.DarkOrange, // Trampa1
                    "!" => Color.DarkOrange, // Trampa2
                    "~" => Color.DarkOrange, // Trampa3
                    "T" => Color.Red, // Ficha Tyr
                    "H" => Color.Silver, // Ficha Heimdall
                    "O" => Color.Gold1, // Ficha Odin
                    "L" => Color.Magenta1, // Ficha Loki
                    "B" => Color.White, // Ficha Bladur
                    "^" => Color.DeepSkyBlue1, // Casilla de beneficio1
                    "*" => Color.DeepSkyBlue1, // Casilla de beneficio2
                    _ => Color.Grey // Casilla desconocida
                };
            
            canvas.SetPixel(columna, fila, color);
            }
        }
        AnsiConsole.Write(canvas);

        // Mostrar estado de las fichas
        foreach (var ficha in fichas)
        {
            //AnsiConsole.MarkupLine($"[bold {ficha.Color.ToMarkup()}]{ficha.Nombre} está en ({ficha.PosicionX}, {ficha.PosicionY})[/]");
            AnsiConsole.MarkupLine($"[bold yellow]{ficha.Nombre} está en ({ficha.PosicionX}, {ficha.PosicionY})[/]");
        }
    }
    
    public void AñadirFicha(Ficha ficha, int x, int y)
    {
        fichas.Add(ficha);
        ficha.PosicionX = x;
        ficha.PosicionY = y;
        celdas[x, y] = ficha.Simbolo; 
    }

    public void MoverFicha(Ficha ficha, int nuevaX, int nuevaY)
    {
        if (nuevaX < 0 || nuevaX >= tamaño || nuevaY < 0 || nuevaY >= tamaño)
        {
            AnsiConsole.MarkupLine("[red]Error: Movimiento fuera de los límites del tablero.[/]");
            return;
        }

        if (EsMovimientoValido(ficha, nuevaX, nuevaY))
        {
            celdas[ficha.PosicionX, ficha.PosicionY] = " "; // Limpia la posición anterior
            ficha.PosicionX = nuevaX;
            ficha.PosicionY = nuevaY;
            celdas[nuevaX, nuevaY] = ficha.Simbolo; // Coloca la ficha en la nueva posición
            AplicarEfectoTrampa(ficha);
            AplicarEfectoCasillas(ficha);
        }
        else
        {
            throw new InvalidOperationException("Movimiento no válido.");
        }
    }

    public bool EsMovimientoValido(Ficha ficha, int nuevaX, int nuevaY)
    {
        // Verifica que la nueva posición esté dentro de los límites del tablero
        if (nuevaX < 0 || nuevaX >= tamaño || nuevaY < 0 || nuevaY >= tamaño)
        {
            return false;
        }

        // Verifica que la nueva posición no esté bloqueada por un obstáculo
        if (celdas[nuevaX, nuevaY] == "■")
        {
            return false;
        }

        // Movimiento de la ficha según su velocidad
        int distancia = Math.Abs(nuevaX - ficha.PosicionX) + Math.Abs(nuevaY - ficha.PosicionY);
        if (distancia > ficha.Velocidad)
        {
            return false;
        }

        return true;
    }
}