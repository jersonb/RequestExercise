namespace Exercise.Api;

public record RequestPost
{
    public Guid Uuid { get; init; }

    public static implicit operator Domain(RequestPost p)
        => new(p.Uuid);
}

public record RequestPut
{
    public Guid Uuid { get; set; }
    public int Id { get; init; }

    public static implicit operator Domain(RequestPut p)
        => new(p.Uuid)
        {
            Id = p.Id,
        };
}
public record RequestDelete
{
    public Guid Uuid { get; set; }

    public static implicit operator Domain(RequestDelete p)
        => new(p.Uuid);
}
public record Response
{
    public Guid Uuid { get; init; }
    public int Id { get; init; }

    public static implicit operator Response(Domain d)
        => new()
        {
            Id = d.Id,
            Uuid = d.Uuid,
        };
}

public record Domain(Guid Uuid)
{
    public int Id { get; set; } = Random.Shared.Next(1, 100);
}