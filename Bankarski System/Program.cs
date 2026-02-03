using System;
using MySql.Data;
using MySql.Data.MySqlClient;

class Program
{
    public void Data()
    {
        string connection = "server = 127.0.0.1; database = mojzo; user = root; password = ;";
        MySqlConnection conn = new MySqlConnection(connection);

    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello");
    }
}