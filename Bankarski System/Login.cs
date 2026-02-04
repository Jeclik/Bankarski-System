using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

public class Login
{
    public static void LoginClass()
    {
        //connection string
        string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";
        MySqlConnection conn = new MySqlConnection(connection);

        //login
        Console.WriteLine("\n");
        Console.WriteLine("Please Login: ");

        Console.Write("Name: ");
        string ime = Console.ReadLine();

        Console.Write("Last name: ");
        string prez = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string pass = Console.ReadLine();
        Console.WriteLine("\n");

        //validation
        if (ime == null || prez == null || email == null || pass == null)
        {
            Console.WriteLine("All fields are required!");
            LoginClass();
        }
        //if all filds are filled, check in database
        else
        {
            conn.Open();

            string Select = "SELECT* FROM korisnici WHERE ime=@ime AND prez=@prezime";
            MySqlCommand command = new MySqlCommand(Select, conn);
            command.Parameters.AddWithValue("@ime", ime);
            command.Parameters.AddWithValue("@prezime", prez);

            MySqlDataReader reader = command.ExecuteReader();
            {
                if (reader.HasRows)
                {
                    //success message
                    Console.WriteLine("Login successful!");

                }
                else
                {
                    //failure message, and restart login
                    Console.WriteLine("Login failed! Invalid username or password.");
                    LoginClass();
                }
            }
            conn.Close();
        }
    }
}