namespace SpaceReservationSystem.Domain.Primitives;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (!isSuccess && error == Error.None)
        throw new InvalidOperationException("A failure result must contain an error.");
        if (isSuccess && error != Error.None)
        throw new InvalidOperationException("A secces result cannot contain an error.");

        // * Aqui solo nos da dos resultado: exito o fracaso. No puede ser ambos o ninguno
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error Error { get; }
    public bool IsFailure => !IsSuccess;

    //* factory methods: Un factory es simplemente un método que crea objetos por ti
    //* En vez de que tú tengas que saber cómo construir el objeto: en este caso de tipo Result.
    //* Sin factory — tienes que saber los detalles internos
    //* new Result(false, new Error("User.NotFound", "User with id '123' was not found."))

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Success<TValue>(TValue value)  => new(value, true, Error.None);
    public static Result<TValue> Failure<TValue>(Error error) =>new(default, false, error);

}

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)  //* base llama al constructor de la clase padre
    {
        _value = value;
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failure result.");

    public static implicit operator Result<TValue>(TValue value) => Success(value);
}