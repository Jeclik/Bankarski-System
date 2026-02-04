using System;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Bank pls choose one");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        switch(choice)
        {
            case "1":
                Register.Registration();
                break;
            case "2":
                // Traits change this to Login
                /* Evo brate */
                Login.LoginClass();

                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;

        }
    }
}