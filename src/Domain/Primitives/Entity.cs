namespace SpaceReservationSystem.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; private init; }
    protected Entity(Guid id)
    {
        Id = id;
    }
    // !important Es el constructor vacío que EF Core necesita para poder reconstruir tus entidades cuando las carga desde la base de datos.
    protected Entity(){ }

    //contrato de la interfaz IEquatable<Entity> para comparar dos entidades por su Id

    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        if (other.GetType() != GetType()) return false;
        return other.Id == Id;
    }
    //todo: Sirve para comparar dos objetos de tipo Entity y determinar si son iguales. Primero verifica si el objeto pasado como parámetro es nulo, luego compara los tipos de ambos objetos para asegurarse de que sean del mismo tipo, y finalmente compara sus Id para determinar si son iguales.
    public override bool Equals(object? obj) =>
    obj is Entity entity && Equals(entity);    
    public override int GetHashCode() =>
            Id.GetHashCode() * 41;

    public static bool operator ==(Entity? left, Entity? right) =>
        left is not null && right is not null && left.Equals(right);

    public static bool operator !=(Entity? left, Entity? right) =>
        !(left == right);
}