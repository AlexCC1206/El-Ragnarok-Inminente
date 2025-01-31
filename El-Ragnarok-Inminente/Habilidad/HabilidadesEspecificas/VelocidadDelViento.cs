using Spectre.Console;

public class VelocidadDelViento : Habilidad
{
    int TurnosRestantesVelocidad;
    public VelocidadDelViento() : base("Velocidad del Viento", 2)
    {
    }

    public override void Usar(Ficha ficha)
    {
        if (EstaDisponible())
        {
            TurnosRestantes = Enfriamiento;
            Random random = new Random();
            int resultadoDado = random.Next(1, 7);
            int velocidadPromediada = (ficha.Velocidad + resultadoDado) / 2;
            ficha.Velocidad += velocidadPromediada;
            AnsiConsole.MarkupLine($"[yellow]Loki ha lanzado un dado y ha obtenido un {resultadoDado}. Su velocidad ha aumentado a {ficha.Velocidad} por 2 turnos.[/]");
            Thread.Sleep(1000);
            // Agregar un temporizador para reducir la velocidad después de 2 turnos
            TurnosRestantesVelocidad = 2;
        }
        else
        {
            Console.WriteLine($"[red]{Nombre} no está disponible. Turnos restantes: {TurnosRestantes}[/]");
        }
    }

    public void ReducirVelocidad(Ficha ficha)
    {
        if (TurnosRestantesVelocidad > 0)
        {
            TurnosRestantesVelocidad--;
            if (TurnosRestantesVelocidad == 0)
            {
                ficha.Velocidad -= (ficha.Velocidad - ficha.VelocidadBase) / 2;
                Console.WriteLine("La velocidad de Loki ha vuelto a su valor original.");
            }
        }
    }
}