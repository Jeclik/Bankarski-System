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

        Console.WriteLine("\n");
        Console.WriteLine("Please Login: ");

        Console.Write("Username: ");
        string ime = Console.ReadLine();

        Console.Write("Password: ");
        string prez = Console.ReadLine();
        Console.WriteLine("\n");

        if (ime == "")
        {
            Console.WriteLine("No Username!");
        }
        else if (prez == "")
        {
            Console.WriteLine("No Password!");
        }
        else
        {
            string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string Select = "SELECT* FROM korisnici WHERE ime=@ime AND prez=@prezime";
            MySqlCommand command = new MySqlCommand(Select, conn);
            command.Parameters.AddWithValue("@ime", ime);
            command.Parameters.AddWithValue("@prezime", prez);

            MySqlDataReader reader = command.ExecuteReader();
            {
                if (reader.Read())
                {
                    Console.WriteLine("Login successful!");

                }
                else
                {
                    Console.WriteLine("Login failed! Invalid username or password.");
                    LoginClass();
                }
            }
            conn.Close();
        }
    }
}