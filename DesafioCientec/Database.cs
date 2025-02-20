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

        public Database(string db_url)
        {
            try
            {
                myConnection = new SQLiteConnection($"Data Source={db_url}");

            //Criar pela primeira vez
            if (!File.Exists($"./{db_url}"))
            {
                SQLiteConnection.CreateFile(db_url);
                System.Console.WriteLine("Database file created");
                OpenConnection();
                createTable();
                CloseConnection();
                //Create Tables;
            }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no SQLite ao inicializar o banco de dados: {ex.Message}\u001b[0m");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"\u001b[31mErro de IO ao criar/verificar o arquivo do banco de dados: {ex.Message}\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[31mErro inesperado ao inicializar o banco de dados: {ex.Message}\u001b[0m");
            }
        }


        public void OpenConnection()
        {
            try
            {
                if (myConnection?.State != System.Data.ConnectionState.Open)
                {
                    //Console.WriteLine("Database aberto!");
                    myConnection?.Open();
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no SQLite ao abrir conexão: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\u001b[31mErro de operação inválida ao abrir conexão: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            try
            {

                if (myConnection?.State != System.Data.ConnectionState.Closed)
                {

                    myConnection?.Close();

                }

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no SQLite ao fechar conexão: {ex.Message}\u001b[0m");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\u001b[31mErro de operação inválida ao fechar conexão: {ex.Message}\u001b[0m");
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
            try
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
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no SQLite ao criar/verificar a tabela: {ex.Message}\u001b[0m");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\u001b[31mErro de operação inválida ao criar/verificar a tabela: {ex.Message}\u001b[0m");

            }
        }

        
    }
}

