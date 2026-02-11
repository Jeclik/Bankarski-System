using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

class Employees
{
    public static void Teller()
    {

    }
    public static void Representative()
    {

    }
    public static void Clark()
    {
        string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";



        Console.WriteLine("Welcome to the bank, Please choose one of the options below:");
        
        Console.WriteLine("Pleas insert the clients data");
        Console.Write("Ime: ");
        string ime = Console.ReadLine();
        Console.Write("Prezime: ");
        string prez = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prez) || string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("All fields are required and cen't have a space in them!");
        }


        
        Console.WriteLine("1.Whant to loan money \n 2.Whant to chek curent debt \n 3.Whant to chek curent clients status");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("You choose to loan money");
                try
                {
                    using var costumer = new MySqlConnection(connection);
                    costumer.Open();

                    string select_costumer = "SELECT debt FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_loan = new MySqlCommand(select_costumer, costumer);
                    cmd_loan.Parameters.AddWithValue("@ime", ime);
                    cmd_loan.Parameters.AddWithValue("@prez", prez);
                    cmd_loan.Parameters.AddWithValue("@email", email);
                    object result_debt = cmd_loan.ExecuteScalar();

                    int depth = Convert.ToInt32(result_debt);

                    Console.Write("Enter the sum you would like to loan: ");
                    int sum = Convert.ToInt32(Console.ReadLine());
                    int new_debt = depth + sum;

                    string update_debt = "UPDATE korisnici SET debt = @debt WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_update_debt = new MySqlCommand(update_debt, costumer);
                    cmd_update_debt.Parameters.AddWithValue("@debt", new_debt);
                    cmd_update_debt.Parameters.AddWithValue("@ime", ime);
                    cmd_update_debt.Parameters.AddWithValue("@prez", prez);
                    cmd_update_debt.Parameters.AddWithValue("@email", email);
                    cmd_update_debt.ExecuteNonQuery();
                    Console.WriteLine("Loan successful! Your new debt is: " + new_debt);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            case 2:
                Console.WriteLine("You choose to chek debt");
                try
                {
                    using var costumer = new MySqlConnection(connection);
                    costumer.Open();

                    string select_debt = "SELECT debt FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_debt = new MySqlCommand(select_debt, costumer);
                    cmd_debt.Parameters.AddWithValue("@ime", ime);
                    cmd_debt.Parameters.AddWithValue("@prez", prez);
                    cmd_debt.Parameters.AddWithValue("@email", email);
                    object result_debt = cmd_debt.ExecuteScalar();

                    int depth = Convert.ToInt32(result_debt);

                    Console.WriteLine("Your current debt is: " + depth);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            case 3:
                Console.WriteLine("You choose to chek clients status");
                try
                {
                    using var costumer = new MySqlConnection(connection);
                    costumer.Open();

                    string select_costumer = "SELECT status FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_status = new MySqlCommand(select_costumer, costumer);
                    cmd_status.Parameters.AddWithValue("@ime", ime);
                    cmd_status.Parameters.AddWithValue("@prez", prez);
                    cmd_status.Parameters.AddWithValue("@email", email);
                    object result_status = cmd_status.ExecuteScalar();

                    int depth = Convert.ToInt32(result_status);

                    Console.WriteLine("Your current debt is: " + depth);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    public static void Assistant_Menager()
    {

    }
    public static void Menager()
    {

    }
}
