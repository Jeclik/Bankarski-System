using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

/*
 * Hello proggramer when i made this code only god and i knew how it works.
 * So if you fail pls increas this count.
 * Faild attempts = 0
*/

public class Login
{
    public static string ime;
    public static string prez;
    public static string email;
    public static string pass;

    public static void LoginClass()
    {
        //connection string
        string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";

        //login
        Console.WriteLine("\n");
        Console.WriteLine("Please Login: ");

        Console.Write("Name: ");
        ime = Console.ReadLine();

        Console.Write("Last name: ");
        prez = Console.ReadLine();

        Console.Write("Email: ");
        email = Console.ReadLine();

        Console.Write("Password: ");
        pass = Console.ReadLine();
        Console.WriteLine("\n");

        //validation
        if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prez) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
        {
            Console.WriteLine("All fields are required and cen't have a space in them!");
            LoginClass();
        }

        //if all filds are filled, check in database
        try
        {
            using var conn_admin = new MySqlConnection(connection);
            conn_admin.Open();

            string select_admin = "SELECT admin FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
            using var cmd_admin = new MySqlCommand(select_admin, conn_admin);
            cmd_admin.Parameters.AddWithValue("@ime", ime);
            cmd_admin.Parameters.AddWithValue("@prez", prez);
            cmd_admin.Parameters.AddWithValue("@email", email);
            object result_admin = cmd_admin.ExecuteScalar();

            if (result_admin == null || result_admin == DBNull.Value)
            {
                Console.WriteLine("Login failed! Invalid credentials.");
            }

            string admin = result_admin.ToString().Trim();

            switch (admin)
            {
                case "yes":
                    Console.WriteLine("Welcome back admin!");
                    Admin.AdminClass();
                    break;

                case "no":
                    try
                    {
                        using var conn = new MySqlConnection(connection);
                        conn.Open();

                        string select = "SELECT employee FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
                        using var cmd = new MySqlCommand(select, conn);
                        cmd.Parameters.AddWithValue("@ime", ime);
                        cmd.Parameters.AddWithValue("@prez", prez);
                        cmd.Parameters.AddWithValue("@email", email);
                        object result = cmd.ExecuteScalar();

                        string emp = result.ToString().Trim();
                        Console.WriteLine("Login Successful!");

                        switch (emp)
                        {
                            case "yes":
                                Employees.EmployeeClass();
                                break;

                            case "no":
                                Customers.CustomerClass();
                                break;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Database error: " + ex.Message);
                        break;
                    }
                    break;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Database error: " + ex.Message);
        }

    }
}