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
        MySqlConnection conn = new MySqlConnection(connection);

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

        //validation
        if (ime == null || prez == null|| email == null || pass == null)
        {
            Console.WriteLine("All fields are required!");
            Registration();
        }
        //if all filds are filled, insertion to database
        else
        {
            conn.Open();

            string insert = "INSERT INTO korisnici (ime, prezime, password) VALUES (@ime, @prez, @pass)";
            MySqlCommand cmd = new MySqlCommand(insert, conn);

            cmd.Parameters.AddWithValue("@ime", ime);
            cmd.Parameters.AddWithValue("@prez", prez);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //success message
        Console.WriteLine("You have sucessfuly loged in.");
    }
}