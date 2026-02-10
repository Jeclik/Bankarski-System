using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

class Customers
{
    public static void CustomerClass()
    {
        string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";

        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1.) Deposit");
        Console.WriteLine("2.) Check status");
        Console.WriteLine("3.) Check debt");
        Console.Write("Your choice: ");
        string choice = Console.ReadLine();
        switch(choice)
        {
            case "1":
                Console.WriteLine("You choose to deposit money");
                Console.Write("Enter the sum you would like to deposit: ");
                string sum = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sum))
                {
                    Console.WriteLine("You must enter a sum to deposit!");
                    CustomerClass();
                }

                try
                {
                    using var conn_deposit = new MySqlConnection(connection);
                    conn_deposit.Open();

                    string select = "SELECT status FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_deposit = new MySqlCommand(select, conn_deposit);
                    cmd_deposit.Parameters.AddWithValue("@ime", Login.ime);
                    cmd_deposit.Parameters.AddWithValue("@prez", Login.prez);
                    cmd_deposit.Parameters.AddWithValue("@email", Login.email);
                    object result = cmd_deposit.ExecuteScalar();

                    if (result == null && result == DBNull.Value)
                    {
                        Console.WriteLine("Status not found!");
                        CustomerClass();
                    }

                    int status = Convert.ToInt32(result);
                    status += Convert.ToInt32(sum);

                    string update = "UPDATE korisnici SET status = @stat";
                    using var cmd_update = new MySqlCommand(update, conn_deposit);
                    cmd_update.Parameters.AddWithValue("@stat", status);
                    cmd_update.ExecuteNonQuery();
                    Console.WriteLine("Deposit successful! Your new status is: " + status);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            case "2":
                Console.WriteLine("You choose to check your status");
                try
                {
                    using var conn_status = new MySqlConnection(connection);
                    conn_status.Open();

                    string select_status = "SELECT status FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_status = new MySqlCommand(select_status, conn_status);
                    cmd_status.Parameters.AddWithValue("@ime", Login.ime);
                    cmd_status.Parameters.AddWithValue("@prez", Login.prez);
                    cmd_status.Parameters.AddWithValue("@email", Login.email);
                    object result_status = cmd_status.ExecuteScalar();
                    if (result_status == null && result_status == DBNull.Value)
                    {
                        Console.WriteLine("Status not found!");
                        CustomerClass();
                    }
                    int status = Convert.ToInt32(result_status);
                    Console.WriteLine("Your current status is: " + status);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            case "3":
                Console.WriteLine("You choose to check your debt");
                try
                {
                    using var conn_debt = new MySqlConnection(connection);
                    conn_debt.Open();

                    string select_debt = "SELECT debt FROM korisnici WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_debt = new MySqlCommand(select_debt, conn_debt);
                    cmd_debt.Parameters.AddWithValue("@ime", Login.ime);
                    cmd_debt.Parameters.AddWithValue("@prez", Login.prez);
                    cmd_debt.Parameters.AddWithValue("@email", Login.email);
                    object result_debt = cmd_debt.ExecuteScalar();
                    if (result_debt == null && result_debt == DBNull.Value)
                    {
                        Console.WriteLine("Debt not found!");
                        CustomerClass();
                    }
                    int debt = Convert.ToInt32(result_debt);
                    Console.WriteLine("Your current debt is: " + debt);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;
        }
    }
}
