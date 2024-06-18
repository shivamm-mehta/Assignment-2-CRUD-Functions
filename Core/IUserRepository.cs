namespace Core.Entity
{
    public interface IUserRepository
    {
        void AddUser(User user);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
