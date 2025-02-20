using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace DesafioCientec
{
    internal class Database
    {
        private static SQLiteConnection? myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source= database.sqlite3");

            //Criar pela primeira vez
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                System.Console.WriteLine("Database file created");
                OpenConnection();
                createTable();
                CloseConnection();
                //Create Tables;
            }
        }


        public void OpenConnection()
        {
            if (myConnection?.State != System.Data.ConnectionState.Open)
            {
                //Console.WriteLine("Database aberto!");
                myConnection?.Open();
            }
        }

        public void CloseConnection()
        {

            if (myConnection?.State != System.Data.ConnectionState.Closed)
            {

                myConnection?.Close();

            }
        }

        public static SQLiteConnection GetConnection()
        {

            if (myConnection == null)
            {
                throw new InvalidOperationException("A conexão com o banco de dados não foi inicializada.");
            }

            return myConnection;
        }


        public static void createTable()
        {
            
            string query = @"CREATE TABLE Cidadaos (
                                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                    Nome TEXT NOT NULL,
                                                    Cpf CHAR(11) NOT NULL UNIQUE );";

            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.GetConnection()))
            {
                myCommand.ExecuteNonQuery();
                Console.WriteLine("Tabela 'Cidadaos' verificada/criada com sucesso.");
            }
        }
    }
}

