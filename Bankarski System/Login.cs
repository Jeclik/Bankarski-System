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

        //string for checking if the login was succesfule and then used for checking roles
        string login = "";

        while (true)
        {
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
                continue;
            }

            //if all filds are filled, check in database
            try
            {
                using var conn = new MySqlConnection(connection);
                conn.Open();

                string select = "SELECT employee FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email AND password = @pass";
                using var cmd = new MySqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@ime", ime);
                cmd.Parameters.AddWithValue("@prez", prez);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", pass);
                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    Console.WriteLine("Login failed! Invalid credentials.");
                    continue;
                }
                string emp = result.ToString().Trim();
                Console.WriteLine("Login Successful!");

                switch (emp)
                {
                    case "yes":
                        Console.WriteLine("You are a employee thx for loging in.");
                        break;

                    case "no":
                        Console.WriteLine("You are a customre thx for loging in.");
                        break;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                break;
            }
        }
    }
}