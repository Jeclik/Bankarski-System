using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

class Employees
{
    public static string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";

    public static void EmployeeClass()
    {
        Console.WriteLine("Welcome back out employee");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1.) Check status");
        Console.WriteLine("2.) Check debt");
        Console.WriteLine("3.) Deposit");
        Console.WriteLine("4.) Start working");
        Console.Write("Your answer: ");
        string choice = Console.ReadLine();
        switch(choice)
        {
            case "1":
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
                        EmployeeClass();
                    }
                    int status = Convert.ToInt32(result_status);
                    Console.WriteLine("Your current status is: " + status);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            case "2":

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
                        EmployeeClass();
                    }
                    int debt = Convert.ToInt32(result_debt);
                    Console.WriteLine("Your current debt is: " + debt);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            case "3":
                Console.WriteLine("You choose to deposit money");
                Console.Write("Enter the sum you would like to deposit: ");
                string sum = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sum))
                {
                    Console.WriteLine("You must enter a sum to deposit!");
                    EmployeeClass();
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
                        EmployeeClass();
                    }

                    int status = Convert.ToInt32(result);
                    status += Convert.ToInt32(sum);

                    string update = "UPDATE korisnici SET status = @stat";
                    using var cmd_update = new MySqlCommand(update, conn_deposit);
                    cmd_update.Parameters.AddWithValue("@stat", status);
                    cmd_update.ExecuteNonQuery();
                    Console.WriteLine("Deposit successful! Your new status is: " + status);
                    EmployeeClass();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;

            case "4":
                try
                {
                    using var conn_role = new MySqlConnection(connection);
                    conn_role.Open();

                    string select_role = "SELECT role FROM employees WHERE ime = @ime AND prezime = @prez AND email = @email";
                    using var cmd_role = new MySqlCommand(select_role, conn_role);
                    cmd_role.Parameters.AddWithValue("@ime", Login.ime);
                    cmd_role.Parameters.AddWithValue("@prez", Login.prez);
                    cmd_role.Parameters.AddWithValue("@email", Login.email);
                    object role_result = cmd_role.ExecuteScalar();
                    if (role_result == null || role_result == DBNull.Value)
                    {
                        Console.WriteLine("Could not retrieve employee role.");
                        break;
                    }

                    string role = role_result.ToString().Trim();
                    switch (role)
                    {

                        case "teller":
                            Employees.Teller();
                            break;

                        case "representative":
                            Employees.Representative();
                            break;

                        case "clark":
                            Employees.Clark();
                            break;

                        case "assistmenager":
                            Employees.Assistant_Menager();
                            break;

                        case "menager":
                            Employees.Menager();
                            break;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                break;
        }
    }
    public static void Teller()
    {

    }
    public static void Representative()
    {

    }
    public static void Clark()
    {
        Console.WriteLine("Welcome our clark pls insert the clients data.");
        Console.Write("Ime: ");
        string ime = Console.ReadLine();
        Console.Write("Prezime: ");
        string prez = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prez) || string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("All fields are required and cen't have a space in them!");
            Clark();
        }
        
        Console.WriteLine("1.) Whant to loan money");
        Console.WriteLine("2.) Whant to chek curent debt");
        Console.WriteLine("3.) Whant to chek curent clients status");
        Console.Write("Your choice: ");
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
