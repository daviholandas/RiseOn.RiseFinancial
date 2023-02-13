namespace RiseOn.RiseFinancial.Core.Entities;

public readonly struct Category
{
    private readonly string _name;

    public Guid Id => Guid.NewGuid();

    private Category(string name)
        => _name = name;

    public static implicit operator Category(string  name)
        => new Category(name);
}