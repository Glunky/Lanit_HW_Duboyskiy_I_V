namespace Homework_4._5.Mappers;

public interface IMapper<T, V>
{
    V Map(T v);
}