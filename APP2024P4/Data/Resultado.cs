namespace APP2024P4.Data
{
    /* Resultado Simple */
    public class Resultado
    {
        public bool Successx { get; set; }
        public string Message { get; set; } = "";

        public static Resultado Success(string message) => new()
        {
            Successx = true,
            Message = message
        };
        public static Resultado Failure(string message) => new()
        {
            Message = message
        };
    }

    public class Resultado<T> : Resultado
    {
        public T? Data { get; set; }
        public static Resultado<T> Success(T data, string message = "Ok") => new()
        {
            Data = data,
            Successx = true,
            Message = message
        };
        public static Resultado<T> Failure(string message) => new()
        {
            Message = message
        };
    }

    public class ResultadoList<T> : Resultado
    {
        public ICollection<T>? Data { get; set; }
        public static ResultadoList<T> Success(ICollection<T> data, string message = "Ok") => new()
        {
            Data = data,
            Successx = true,
            Message = message
        };
        public static ResultadoList<T> Failure(string message) => new()
        {
            Message = message
        };
    }
}
