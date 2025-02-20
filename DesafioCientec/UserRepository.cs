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
    }
}
