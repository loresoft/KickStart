namespace Test.Core;

public interface IUserService : IRepository<User>
{
    IConnection Connection { get; }
}