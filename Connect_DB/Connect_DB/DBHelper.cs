using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace Connect_DB
{
    internal class DBHelper
    {
        public String dbFileName;
        public SQLiteConnection m_dbConn;
        public SQLiteCommand m_sqlCmd;

        public void LoadDB()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            dbFileName = "MyDatabase" + ".sqlite";
            Console.WriteLine("Disconnected");
        }

        public void Create_DB()
        {
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT)";
                m_sqlCmd.ExecuteNonQuery();

                Console.WriteLine("Connected");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Disconnected");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void Connect_DB()
        {
            if (!File.Exists(dbFileName))
            {
                Create_DB(); Connect_DB();
            }
                //Console.WriteLine("Please, create DB and blank table (Push \"Create\" button)");

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                Console.WriteLine("Connected");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Disconnected");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void ReadAllDB()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            if (m_dbConn.State != ConnectionState.Open)
            {
                Console.WriteLine("Open connection with database");
                return;
            }

            try
            {
                sqlQuery = "SELECT * FROM Catalog";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    //dgvViewer.Rows.Clear();
                    Console.WriteLine("id|Author|Book");
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        for(int j = 0; j < dTable.Rows[i].ItemArray.Length; j++)
                        {
                            //dgvViewer.Rows.Add(dTable.Rows[i].ItemArray);
                            Console.Write(dTable.Rows[i].ItemArray[j] + "|");//ItemArray[i] - индекс столбцов
                        }
                        Console.WriteLine();
                    }
                }
                else
                    Console.WriteLine("Database is empty");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void AddToDB()
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                Console.WriteLine("Open connection with database");
                return;
            }

            Console.WriteLine("Enter to Author: ");
            string Author = Console.ReadLine();
            Console.WriteLine("Enter to Book: ");
            string Book = Console.ReadLine();

            
                try
                {
                    m_sqlCmd.CommandText = "INSERT INTO Catalog ('author', 'book') values ('" +Author + "' , '" +Book + "')";

                    m_sqlCmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            
        }

        public void DeleteToDB()
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                Console.WriteLine("Open connection with database");
                return;
            }

            Console.WriteLine("введите id:");
            int id = int.Parse(Console.ReadLine());

            try
            {
                m_sqlCmd.CommandText = "DELETE FROM Catalog WHERE id = '" + id + "'";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public void DeleteForNameDB()
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                Console.WriteLine("Open connection with database");
                return;
            }

            Console.WriteLine("введите bookName:");
            string book = Console.ReadLine();

            try
            {
                m_sqlCmd.CommandText = "DELETE FROM Catalog WHERE book = '" + book + "'";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public void UpdateToDB()
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                Console.WriteLine("Open connection with database");
                return;
            }
            Console.WriteLine("введите id записи для её изменения:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter to Author: ");
            string Author = Console.ReadLine();
            Console.WriteLine("Enter to Book: ");
            string Book = Console.ReadLine();


            try
            {
                m_sqlCmd.CommandText = "UPDATE Catalog SET book =  '" + Book + "', author = '" + Author + "' WHERE id = '" + id + "' ";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}
