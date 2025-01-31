public class PasoDeLasSombras : Habilidad
{
    public PasoDeLasSombras() : base("Paso de las Sombras", 3)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;

            // Desvanecerse en las sombras
            ficha.Tablero.celdas[ficha.PosicionX, ficha.PosicionY] = " ";
        
            // Reaparecer en una posición aleatoria
            Random random = new Random();
            int nuevaX = random.Next(1, ficha.Tablero.tamaño - 1);
            int nuevaY = random.Next(1, ficha.Tablero.tamaño - 1);

            // Verificar que la nueva posición no esté bloqueada
            while (ficha.Tablero.celdas[nuevaX, nuevaY] == "■")
            {
                nuevaX = random.Next(1, ficha.Tablero.tamaño - 1);
                nuevaY = random.Next(1, ficha.Tablero.tamaño - 1);
            }

            // Reaparecer en la nueva posición
            ficha.PosicionX = nuevaX;
            ficha.PosicionY = nuevaY;
            ficha.Tablero.celdas[nuevaX, nuevaY] = ficha.Simbolo;

            Console.WriteLine("Loki ha desaparecido en las sombras y ha reaparecido en una posición aleatoria.");
            Thread.Sleep(1000);
        }
        else
        {
            Console.WriteLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
        }
    }
}