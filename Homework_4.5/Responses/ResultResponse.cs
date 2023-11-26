namespace Homework_4._5.Responces;

public class ResultResponse<T>
{
    public T Body { get; set; }
    public List<string> Errors { get; set; } = new();
}