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
    public List<Casilla> casillas { get; set; }
    

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
    }

    private void Inicializar()
    {
        // Inicializa todas las celdas con un punto
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

        AñadirObstaculos();
        AñadirTrampas();
        AñadirCasillasBeneficios();
    }

    private void GenerarSalida()
    {
        // Establece la salida en la posición (9, 9)
        int x = 10;
        int y = 10;

        salida = (x, y);
        celdas[x, y] = "+"; // Representa la salida con 'S'
    }

    public bool EsSalida(int x, int y)
    {
        return salida.Item1 == x && salida.Item2 == y;
    }

    private void AñadirObstaculos()
    {
        int numObstaculos = (tamaño - 2) * (tamaño - 2) * 30 / 100; // Ejemplo: 20% del tablero serán obstáculos
        for (int i = 0; i < numObstaculos; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, tamaño - 1); // Evita los bordes
                y = random.Next(1, tamaño - 1); // Evita los bordes
            } while (celdas[x, y] != " " || !EsAccesible(x, y)); // Asegura que no se sobreescriba un obstáculo existente y que el tablero siga siendo accesible

            celdas[x, y] = "■"; // Representa un obstáculo con '#'
        }
    }

    private void AñadirTrampas()
    {
        int numTrampas = (tamaño - 2) * (tamaño - 2) * 5 / 100; // Ejemplo: 10% del tablero interno serán trampas
        for (int i = 0; i < numTrampas; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, tamaño - 1); // Evita los bordes
                y = random.Next(1, tamaño - 1); // Evita los bordes
            } while (celdas[x, y] != " " || !EsAccesible(x, y)); // Asegura que no se sobreescriba una trampa existente y que el tablero siga siendo accesible

            Trampa trampa = GenerarTrampaAleatoria();
            trampa.PosicionX = x;
            trampa.PosicionY = y;
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

    private void AplicarEfectoTrampa(Ficha ficha)
    {
        foreach (var trampa in trampas)
        {
            if (ficha.PosicionX == trampa.PosicionX && ficha.PosicionY == trampa.PosicionY)
            {
                AnsiConsole.MarkupLine($"[red]{ficha.Nombre} ha caído en una trampa![/]");
                trampa.Activar(ficha);
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

    private void AñadirCasillasBeneficios()
    {
        int numCasillasBeneficios = (tamaño - 2) * (tamaño - 2) * 5 / 100; // Ejemplo: 10% del tablero interno serán casillas con beneficios
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
            celdas[x, y] = casilla.Simbolo; // Representa una casilla con su símbolo
            casillas.Add(casilla);
        }
    }

    private Casilla GenerarCasillaAleatoria()
    {
        int tipoCasilla = random.Next(2); // Puedes ajustar el número según la cantidad de tipos de casillas
        switch (tipoCasilla)
        {
            case 0:
                return new CasillaVelocidad(0, 0 , 2); // Aumenta velocidad
            case 1:
                return new CasillaHabilidad(0 , 0 , new JusticiaImplacable()); // Habilidad adicional
            default:
                return new CasillaVelocidad(0, 0, 2); // Por defecto, aumenta velocidad
        }
    }

    private void AplicarEfectoCasillas(Ficha ficha)
    {
        foreach (var casilla in casillas)
        {
            if (ficha.PosicionX == casilla.PosicionX && ficha.PosicionY == casilla.PosicionY)
            {
                AnsiConsole.MarkupLine($"[green]{ficha.Nombre} ha caído en una casilla de beneficio![/]");
                casilla.AplicarEfecto(ficha);
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

    private bool EsAccesible(int obstaculoX, int obstaculoY)
    {
        // Temporarily place the obstacle
        celdas[obstaculoX, obstaculoY] = "■";

        // Check if there's a path from (1, 1) to (tamaño-2, tamaño-2)
        bool accesible = HayCamino(1, 1, tamaño - 2, tamaño - 2);

        // Remove the temporary obstacle
        celdas[obstaculoX, obstaculoY] = " ";

        return accesible;
    }

    public bool HayCamino(int startX, int startY, int endX, int endY)
    {
        if (celdas[startX, startY] == "■" || celdas[endX, endY] == "■")
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

                if (nuevoX >= 0 && nuevoX < tamaño && nuevoY >= 0 && nuevoY < tamaño && !visitado[nuevoX, nuevoY] && celdas[nuevoX, nuevoY] != "■")
                {
                    cola.Enqueue((nuevoX, nuevoY));
                    visitado[nuevoX, nuevoY] = true;
                }
            }
        }
        
        return false;
    }
/*
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
*/
    
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
            "+" => Color.Gold1, // Salida
            "X" => Color.Red, // Trampa
            "!" => Color.Red, // Trampa
            "~" => Color.Red, // Trampa
            "T" => Color.Cyan1, // Casilla de beneficio
            "H" => Color.Cyan1, // Casilla de beneficio
            "O" => Color.Cyan1, // Casilla de beneficio
            "L" => Color.Cyan1, // Casilla de beneficio
            "B" => Color.Cyan1, // Casilla de beneficio
            "^" => Color.Magenta1, // Casilla de beneficio
            "*" => Color.Magenta1, // Casilla de beneficio
            _ => Color.Grey // Casilla desconocida
        };

        canvas.SetPixel(columna, fila, color);
    }
    
}
    AnsiConsole.Write(canvas);

        /*
        // Determinar el ancho máximo de los símbolos
        int maxWidth = 1;
        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                if (celdas[i, j].Length > maxWidth)
                {
                    maxWidth = celdas[i, j].Length;
                }
            }
        }
        
    
    
        var table = new Table().Border(TableBorder.None)
            .Title("[bold gold1]Tablero del Juego[/]")
            .HideHeaders();
        

        // Definir las columnas de la tabla
        for (int i = 0; i < tamaño; i++)
        {
            table.AddColumn(new TableColumn(" "));//.Width(maxWidth + 2)); // +2 para espacios adicionales
        }

        // Agregar filas a la tabla
        for (int i = 0; i < tamaño; i++)
        {
            var row = new List<string>();
            for (int j = 0; j < tamaño; j++)
            {
                string celda = celdas[i, j];
                string celdaConColor = celda;

    
                // Aplicar colores según el contenido de la celda
                if (celda == "■") // Obstáculo
                {
                    celdaConColor = $"[white]██[/]";
                }
                else if (celda == "+") // Salida
                {
                    celdaConColor = $"[bold gold1 on navy]{celda}[/]";
                }
                else if (celda == "X" || celda == "!" || celda == "~") 
                {
                    celdaConColor = $"[blink bold mediumpurple2]{celda}[/]";
                }
                else if(celda == "T" || celda == "H" || celda == "O" || celda == "L" || celda == "B")
                {
                    celdaConColor = $"[deepskyblue1]{celda}[/]";
                }
                else if (celda == "^" || celda == "*")
                {
                    celdaConColor = $"[bold orange3]{celda}[/]";
                }
                // Centrar el símbolo dentro de la celda
                //int padding = (maxWidth - celda.Length) / 2;
                //celdaConColor = celdaConColor.PadLeft(celdaConColor.Length + padding).PadRight(maxWidth + 2);

                row.Add(celdaConColor);
            }
            table.AddRow(row.ToArray());
        }

        // Configuración del borde
        table.Border(TableBorder.Rounded); // Desactivar bordes predeterminados

        AnsiConsole.Write(table);
*/
        // Mostrar estado de las fichas
        foreach (var ficha in fichas)
        {
            AnsiConsole.MarkupLine($"{ficha.Nombre} está en ({ficha.PosicionX}, {ficha.PosicionY})");
        }
    }
    

    
    public void AñadirFicha(Ficha ficha, int x, int y)
    {
        // Forzar la colocación de la ficha en la posición especificada
        fichas.Add(ficha);
        ficha.PosicionX = x;
        ficha.PosicionY = y;
        celdas[x, y] = ficha.Simbolo; // Representa una ficha con 'F'
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

        // Verifica que la ficha no se mueva más allá de su velocidad permitida
        int distancia = Math.Abs(nuevaX - ficha.PosicionX) + Math.Abs(nuevaY - ficha.PosicionY);
        if (distancia > ficha.Velocidad)
        {
            return false;
        }

        return true;
    }
    
}