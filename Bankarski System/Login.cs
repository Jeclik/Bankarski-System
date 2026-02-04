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

        //string for checking if the login was succesfule and then used for checking roles
        string login = "";

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
        if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prez) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
        {
            Console.WriteLine("All fields are required and cen't have a space in them!");
            LoginClass();
        }
        //if all filds are filled, check in database
        else
        {
            conn.Open();

            string Select = "SELECT* FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email AND @password = @pass";
            MySqlCommand command = new MySqlCommand(Select, conn);
            command.Parameters.AddWithValue("@ime", ime);
            command.Parameters.AddWithValue("@prez", prez);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@pass", pass);

            MySqlDataReader reader = command.ExecuteReader();
            {
                if (reader.HasRows)
                {
                    //success message
                    Console.WriteLine("Login successful!");
                    login = "success";
                }
                else
                {
                    //failure message, and restart login
                    Console.WriteLine("Login failed! Invalid username or password.");
                    login = "failed";
                    LoginClass();
                }
            }
            conn.Close();

            //employee and role check (in progress, DON'T TOUCH!)

            /*
            switch (login)
            {
                case "success":
                    string empcheck = "SELECT* FROM korisnici WHERE ime = @ime AND prezime = @prez AND employee = @emp";
                    MySqlCommand cmd = new MySqlCommand(empcheck, conn);
                    cmd.Parameters.AddWithValue("@ime", ime);
                    cmd.Parameters.AddWithValue("@prez", prez);
                    cmd.Parameters.AddWithValue("@emp", emp);
                    break;
            }
            */

        }
    }
}