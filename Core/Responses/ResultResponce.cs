namespace Core.Responses;

public class ResultResponse<T>
{
    public T Body { get; set; }
    public List<string> Errors { get; set; } = new();
}