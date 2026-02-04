namespace DriverMatching;

public class Driver
{
    public string Id { get; }
    public Position Position { get; private set; }

    public Driver(string id, Position position)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("ID не может быть пустым");

        Id = id;
        Position = position;
    }

    public void UpdatePosition(Position newPosition)
    {
        Position = newPosition;
    }

    public override string ToString() => $"{Id,-6} → {Position}";
}