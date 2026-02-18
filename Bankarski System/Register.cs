using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Mysqlx.Prepare;

public class Register
{
    public static void Registration()
    {
        //connection string
        string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";

        //registration
        Console.WriteLine("Welcome to the bank registration.");
        Console.Write("Name: ");
        string ime = Console.ReadLine();
        Console.Write("Last name: ");
        string prez = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string pass = Console.ReadLine();

        string emp = "no";
        string admin = "no";

        //validation
        if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prez) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
        {
            Console.WriteLine("All fields are required and cen't have a space it them!");
            Registration();
        }

        //check if user already exists
        try
        {
            using var conn = new MySqlConnection(connection);
            conn.Open();
            
            string select = "SELECT * FROM korisnici WHERE email = @email";
            using var cmd = new MySqlCommand(select, conn);

            cmd.Parameters.AddWithValue("@email", email);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("User already exist, please try again.");
                Registration();
            }
            else
            {
                //if all filds are filled and user dosen't already exist, insertion to database
                try
                {
                    conn.Open();

                    string insert = "INSERT INTO korisnici (ime, prezime, email, password, employee, admin) VALUES (@ime, @prez, @email, @pass, @emp, @admin)";
                    using var cmd_insert = new MySqlCommand(insert, conn);

                    cmd_insert.Parameters.AddWithValue("@ime", ime);
                    cmd_insert.Parameters.AddWithValue("@prez", prez);
                    cmd_insert.Parameters.AddWithValue("@email", email);
                    cmd_insert.Parameters.AddWithValue("@pass", pass);
                    cmd_insert.Parameters.AddWithValue("@emp", emp);
                    cmd_insert.Parameters.AddWithValue("@amind", admin);
                    cmd_insert.ExecuteNonQuery();

                    Console.WriteLine("You have sucessfuly registrated.");
                    Login.LoginClass();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Database error: " + ex.Message);
        }
    }
}
