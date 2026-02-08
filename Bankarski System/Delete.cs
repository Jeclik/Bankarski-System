using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

    public class Delete
    {
        static void DeleteClass()
        {
            Console.WriteLine("You are in the Delete Menu please pick what you want to delete:");
            Console.WriteLine("1. Ime, 2. Pass");
            string Pick = Console.ReadLine();
            if (Pick == "1")
            {
                Console.WriteLine("You picked to Delete your Ime!");
                Console.Write("Please add what Ime you want to delete: ");
                string Wanted = Console.ReadLine();
            if (Wanted == "")
            {
                Console.WriteLine("You didnt add a Ime!");
            }

            string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            // Tu mozno budes misiet premenit alebo zotriet username
            string Delete = "DELETE ime FROM korisnici WHERE username = @ime;";
            MySqlCommand command = new MySqlCommand(Delete, conn);
            command.Parameters.AddWithValue("@ime", Wanted);

            int rows = command.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("Username deleted succesfully.");
            }
            else
            {
                Console.WriteLine("No matching username found.");
            }
            conn.Close();

        }
        else if (Pick == "2")
        {
            Console.WriteLine("You picked to Delete your Password!");
            Console.Write("Please add what Password you want to delete: ");
            string Wanted = Console.ReadLine();
            if (Wanted == "")
            {
                Console.WriteLine("You didnt add a Password!");
            }

            string connection = "server = 127.0.0.1; database = banka; user = root; password = ;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string Delete = "DELETE FROM korisnici WHERE pass = @pass";
            MySqlCommand command = new MySqlCommand(Delete, conn);
            command.Parameters.AddWithValue("@pass", Wanted);

            int rows = command.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("Password deleted succesfully.");
                /* Tu nacim nak sa spravi Admin class, co jest admin.menu de sa lem vrati */
                /* Admin(); */
            }
            else
            {
                Console.WriteLine("No matching Password found.");
                Console.WriteLine("Do you wish to exit? Y/N");
                string Pick1 = Console.ReadLine();
                if (Pick1 == "Y")
                {
                    command.Dispose();
                    conn.Close();
                    Environment.Exit(0);
                }
                else if (Pick1 == "N")
                {
                    command.Dispose();
                    conn.Close();
                    /* Tu nacim nak sa spravi Admin class, co jest admin.menu de sa lem vrati */
                    /* Admin(); */
                }
            }
            conn.Close();
        }
    }
}
