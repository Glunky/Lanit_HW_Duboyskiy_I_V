namespace Homework_4.Controllers;

public interface IController
{
    public Task CreateItem();

    public Task ReadAllItems();
    public Task ReadItem();

    public Task UpdateItem();

    public Task DeleteItem();
}
