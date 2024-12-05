namespace APP2024P4;


public class Result
{
	public bool Ok { get; set; }
	public string? Message { get; set; }

	public static Result Success(string Message = "OK") => new Result() { Ok = true, Message = Message };
	public static Result Failure(string Message = "Something was wrong") => new Result() { Message = Message };

}
public class Result<T> : Result
{
	public T? Data { get; set; }

	public static Result<T> Success(T data, string Message = "OK") => new Result<T>() { Ok = true, Message = Message, Data = data };
	public static Result<T> Failure(string Message = "Something was wrong") => new Result<T>() { Message = Message };
}
public class ResultList<T> : Result
{
	public ICollection<T>? Data { get; set; }

	public static ResultList<T> Success(ICollection<T>? data, string Message = "OK") => new ResultList<T>() { Ok = true, Message = Message, Data = data };
	public static ResultList<T> Failure(string Message = "Something was wrong") => new ResultList<T>() { Message = Message };
}