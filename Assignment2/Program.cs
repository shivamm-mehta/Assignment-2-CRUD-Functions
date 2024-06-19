using Core;
using Core.Entity;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DataContext())
            {
                context.Database.EnsureCreated();
                var userRepository = new UserRepository(context);
                bool exit = false;

                Console.WriteLine("Enter commands: add, list, edit <id>, delete <id>, or exit");

                while (!exit)
                {
                    Console.Write("Enter your input> ");
                    string input = Console.ReadLine();
                    var userInput = input.Split(' ');

                    if (userInput[0].ToLower() == "add")
                    {
                        AddUser(userRepository);
                    }
                    else if (userInput[0].ToLower() == "list")
                    {
                        ListUsers(userRepository);
                    }
                    else if (userInput[0].ToLower() == "edit")
                    {
                        if (userInput.Length > 1 && int.TryParse(userInput[1], out int editId))
                        {
                            EditUser(userRepository, editId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command. Use: edit <id>");
                        }
                    }
                    else if (userInput[0].ToLower() == "delete")
                    {
                        if (userInput.Length > 1 && int.TryParse(userInput[1], out int deleteId))
                        {
                            DeleteUser(userRepository, deleteId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command. Use: delete <id>");
                        }
                    }
                    else if (userInput[0].ToLower() == "exit")
                    {
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("Unknown command. Valid commands are: add, list, edit <id>, delete <id>, exit");
                    }
                }
            }
        }

        static void AddUser(IUserRepository userRepository)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            var user = new User { Name = name, Email = email };
            userRepository.AddUser(user);
            Console.WriteLine($"User {name} added.");
        }

        static void ListUsers(IUserRepository userRepository)
        {
            var users = userRepository.GetAllUsers();
            if (!users.Any())
            {
                Console.WriteLine("No users found.");
            }
            else
            {
                Console.WriteLine("ID\tName\t\tEmail");
                Console.WriteLine("--------------------------------------------------");
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id}\t{user.Name}\t\t{user.Email}");
                }
            }
        }

        static void EditUser(IUserRepository userRepository, int id)
        {
            var user = userRepository.GetUserById(id);
            if (user != null)
            {
                Console.Write("Enter new Name: ");
                string updatedName = Console.ReadLine();
                Console.Write("Enter new Email: ");
                string updatedEmail = Console.ReadLine();

                user.Name = updatedName;
                user.Email = updatedEmail;
                userRepository.UpdateUser(user);
                Console.WriteLine($"User updated.");
            }
            else
            {
                Console.WriteLine($"User not found.");
            }
        }

        static void DeleteUser(IUserRepository userRepository, int id)
        {
            var user = userRepository.GetUserById(id);
            if (user != null)
            {
                userRepository.DeleteUser(id);
                Console.WriteLine($"User deleted.");
            }
            else
            {
                Console.WriteLine($"User not found.");
            }
        }
    }
}
