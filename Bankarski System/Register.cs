using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Register
{
    public static void Registration()
    {
        //connection string
        string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";

        //registration
        while (true)
        {
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

            //validation
            if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prez) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                Console.WriteLine("All fields are required and cen't have a space it them!");
                continue;
            }

            //if all filds are filled, insertion to database
            try
            {
                using var conn = new MySqlConnection(connection);
                conn.Open();

                string insert = "INSERT INTO korisnici (ime, prezime, email, password, employee) VALUES (@ime, @prez, @email, @pass, @emp)";
                using var cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@ime", ime);
                cmd.Parameters.AddWithValue("@prez", prez);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@emp", emp);
                cmd.ExecuteNonQuery();

                Console.WriteLine("You have sucessfuly registrated.");
                Login.LoginClass();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
        }
    }
}
