namespace DriverMatching;

public static class DistanceCalculator
{
    /// Квадрат евклидова расстояния
    public static double SquaredEuclidean(Position a, Position b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return dx * dx + dy * dy;
    }
    /// Полное евклидово расстояние
    public static double Euclidean(Position a, Position b)
    {
        return Math.Sqrt(SquaredEuclidean(a, b));
    }
}