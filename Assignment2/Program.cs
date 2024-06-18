using Assignment2;
using Core.Entity;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new DataContext())
        {
            context.Database.EnsureCreated();
            bool exit = false;

            Console.WriteLine("Enter commands: add, list, edit <id>, delete <id>, or exit");

            while (!exit)
            {
                Console.Write("Enter your input> ");
                string input = Console.ReadLine();
                var userInput = input.Split(' ');

                if (userInput[0].ToLower() == "add")
                {
                    AddUser(context);
                }
                else if (userInput[0].ToLower() == "list")
                {
                    ListUsers(context);
                }
                else if (userInput[0].ToLower() == "edit")
                {
                    if (userInput.Length > 1 && int.TryParse(userInput[1], out int editId))
                    {
                        EditUser(context, editId);
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
                        DeleteUser(context, deleteId);
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

    static void AddUser(DataContext context)
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        var user = new User { Name = name, Email = email };
        context.Users.Add(user);
        context.SaveChanges();
        Console.WriteLine($"User {name} added.");
    }

    static void ListUsers(DataContext context)
    {
        var users = context.Users.ToList();
        if (users.Count == 0)
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

    static void EditUser(DataContext context, int id)
    {
        var user = context.Users.Find(id);
        if (user != null)
        {
            Console.Write("Enter new Name: ");
            string updatedName = Console.ReadLine();
            Console.Write("Enter new Email: ");
            string updatedEmail = Console.ReadLine();

            user.Name = updatedName;
            user.Email = updatedEmail;
            context.SaveChanges();
            Console.WriteLine($"User updated.");
        }
        else
        {
            Console.WriteLine($"User not found.");
        }
    }

    static void DeleteUser(DataContext context, int id)
    {
        var user = context.Users.Find(id);
        if (user != null)
        {
            context.Users.Remove(user);
            context.SaveChanges();
            Console.WriteLine($"User deleted.");
        }
        else
        {
            Console.WriteLine($"User not found.");
        }
    }
}
