using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCientec
{
    internal class UserRepository
    {

        public static void Create(string nome, string cpf)
        {
            if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(cpf))
            {
                Console.WriteLine("Input inválido");
                return;
            }

            //Expressao Regular para validar CPF
            if (!CpfTreatment.ValidarCPF(cpf))
            {
                Console.WriteLine("CPF inválido!");
                return;
            }

            cpf=CpfTreatment.RemoverMascaraCPF(cpf);

            string checkQuery = "SELECT COUNT(*) FROM Cidadaos WHERE Cpf = @Cpf";

            using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, Database.GetConnection()))
            {
                checkCommand.Parameters.AddWithValue("@Cpf", cpf);

                int cpfCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (cpfCount > 0)
                {
                    Console.WriteLine("O cpf já foi cadastrado.");
                    return;
                }
            }

            string query = "INSERT INTO Cidadaos ('Nome', 'Cpf') VALUES (@Nome, @Cpf)";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.GetConnection()))
            {
                myCommand.Parameters.AddWithValue("@Nome", nome);
                myCommand.Parameters.AddWithValue("@Cpf", cpf);

                myCommand.ExecuteNonQuery();
                Console.WriteLine("Cidadao Inserido com Sucesso!");
            }
        }


        public static void Read_t(string nome = null, string cpf= null)
        {
           
            // Cria a query inicial
            string query = "SELECT * FROM Cidadaos WHERE 1=1";  // A condição WHERE 1=1 sempre é verdadeira

            // Adiciona os filtros dinamicamente
            if (!string.IsNullOrEmpty(nome))
            {
                query += " AND Nome = @Nome";
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                query += " AND Cpf = @Cpf";
                cpf = CpfTreatment.RemoverMascaraCPF(cpf);
            }

            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.GetConnection()))
            {
                // Adiciona os parâmetros se existirem
                if (!string.IsNullOrEmpty(nome))
                {
                    myCommand.Parameters.AddWithValue("@Nome",nome);
                }

                if (!string.IsNullOrEmpty(cpf))
                {
                    myCommand.Parameters.AddWithValue("@Cpf", cpf);
                }

                var result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Console.WriteLine("ID: {0} - Nome: {1} - CPF: {2}", result["id"], result["Nome"], CpfTreatment.FormatCPF(result["Cpf"].ToString()));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(cpf))
                    {
                        Console.WriteLine("Nenhum Cidadão encontrado com esse Nome e CPF.");
                    }
                    else if (!string.IsNullOrEmpty(nome))
                    {
                        Console.WriteLine("Nenhum Cidadão encontrado com esse Nome.");
                    }
                    else if (!string.IsNullOrEmpty(cpf))
                    {
                        Console.WriteLine("Nenhum Cidadão encontrado com esse CPF.");
                    }
                }
            }
        }
    }
}
