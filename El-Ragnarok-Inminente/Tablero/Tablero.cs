using System;
using Spectre.Console;

public class Tablero
{
    public string[,] celdas;
    public int tamaño;
    private Random random;
    private List<Trampa> trampas;
    private List<Ficha> fichas;
    private (int, int) salida; // Coordenadas de la salida
    

    public Tablero(int n)
    {
        tamaño = n;
        celdas = new string[n, n];
        random = new Random();
        trampas = new List<Trampa>();
        fichas = new List<Ficha>();
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
    }

    private void GenerarSalida()
    {
        // Establece la salida en la posición (9, 9)
        int x = 10;
        int y = 10;

        salida = (x, y);
        celdas[x, y] = "+"; // Representa la salida con 'S'
    }

    private void AñadirObstaculos()
    {
        int numObstaculos = (tamaño - 2) * (tamaño - 2) * 35 / 100; // Ejemplo: 20% del tablero serán obstáculos
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
        int numTrampas = (tamaño - 2) * (tamaño - 2) * 15 / 100; // Ejemplo: 10% del tablero interno serán trampas
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
    public bool ValidarAccesibilidad()
    {
        bool[,] visitado = new bool[tamaño, tamaño];
        Queue<(int, int)> cola = new Queue<(int, int)>();
        cola.Enqueue((1, 1)); // Empieza desde (1, 1) para evitar los bordes
        visitado[1, 1] = true;

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        while (cola.Count > 0)
        {
            var (x, y) = cola.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nuevoX = x + dx[i];
                int nuevoY = y + dy[i];

                if (nuevoX >= 1 && nuevoX < tamaño - 1 && nuevoY >= 1 && nuevoY < tamaño - 1 && !visitado[nuevoX, nuevoY] && celdas[nuevoX, nuevoY] != "■")
                {
                    cola.Enqueue((nuevoX, nuevoY));
                    visitado[nuevoX, nuevoY] = true;
                }
            }
        }
        
        // Verifica que todas las celdas accesibles hayan sido visitadas
        for (int i = 1; i < tamaño - 1; i++)
        {
            for (int j = 1; j < tamaño - 1; j++)
            {
                if (celdas[i, j] == " " && !visitado[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }
    */
    public void Imprimir()
    {
        var table = new Table().Border(TableBorder.None)
        .Title("[bold yellow]Tablero del Juego[/]")
        .HideHeaders();

        // Definir las columnas de la tabla
        for (int i = 0; i < tamaño; i++)
        {
            table.AddColumn(new TableColumn(" "));
        }

        // Agregar filas a la tabla
        for (int i = 0; i < tamaño; i++)
        {
            var row = new List<string>();
        for (int j = 0; j < tamaño; j++)
        {
            // Si es la primera o última fila, o la primera o última columna, mostrar el borde
            if (i == 0 || i == tamaño - 1 || j == 0 || j == tamaño - 1)
            {
                row.Add(celdas[i, j].ToString());
            }
            else
            {
                row.Add(celdas[i, j].ToString());
            }
        }
        table.AddRow(row.ToArray());
    }

    // Configuración del borde
    table.Border(TableBorder.Simple); // Desactivar bordes predeterminados
    table.BorderStyle = new Style(Color.Yellow);
    

        AnsiConsole.Render(table);
        
        // Mostrar estado de las fichas
        foreach (var ficha in fichas)
        {
            AnsiConsole.MarkupLine($"[bold green]{ficha.Nombre}[/] está en ({ficha.PosicionX}, {ficha.PosicionY})");
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

    private void AplicarEfectoTrampa(Ficha ficha)
    {
        foreach (var trampa in trampas)
        {
            if (ficha.PosicionX == trampa.PosicionX && ficha.PosicionY == trampa.PosicionY)
            {
                AnsiConsole.MarkupLine($"[red]{ficha.Nombre} ha caído en una trampa![/]");
                trampa.Activar(ficha);
                //System.Threading.Thread.Sleep(3000);
            }
        }
    }
    /*
    private void AplicarEfectoTrampa(Ficha ficha)
    {
        foreach (var trampa in trampas)
        {
            if (ficha.PosicionX == trampa.PosicionX && ficha.PosicionY == trampa.PosicionY)
            {   
                Console.WriteLine($"{ficha.Nombre} ha caído en una trampa!"); // Añadido mensaje de trampa
                trampa.Activar(ficha); // Asegura que la trampa se active correctamente
                // Aquí puedes agregar lógica para aplicar el efecto de la trampa a la ficha
                // Por ejemplo, reducir velocidad, inmovilizar, etc.
                if (trampa is WindTrap)
                {
                    // Lógica para WindTrap
                    ficha.PosicionX -= 2; // Ejemplo: mueve la ficha dos casillas hacia atrás
                    Console.WriteLine($"{ficha.Nombre} es empujado hacia atrás por el Viento de Jörmungandr.");
                }
                else if (trampa is RootTrap)
                {
                    // Lógica para RootTrap
                    ficha.Inmovilizado = true; // Inmoviliza la ficha
                    Console.WriteLine($"{ficha.Nombre} queda atrapado por las raíces de Yggdrasil.");
                }
                else if (trampa is JotunheimTrap)
                {
                    // Lógica para JotunheimTrap
                    ficha.Velocidad = 1; // Reduce la velocidad de la ficha
                    Console.WriteLine($"{ficha.Nombre} se ralentiza debido a las Piedras de Jotunheim.");
                }
            }
        }
    }
    */
    public bool EsSalida(int x, int y)
    {
        return salida.Item1 == x && salida.Item2 == y;
    }
}