namespace Homework_5.Publishers;

public interface IMessagePublisher<T, U>
{
    U SendMessage(T request);
}