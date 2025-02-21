
using System.Data.SQLite;


namespace DesafioCientec
{
    internal class UserRepository
    {
        public static int Create(string nome, string cpf)
        {

            try
            {
                if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(cpf))
                {
                    Console.WriteLine("Input inválido");
                    return 0;
                }

                //Expressao Regular para validar CPF
                if (!CpfTreatment.ValidarCPF(cpf))
                {
                    Console.WriteLine("CPF inválido!");
                    return 0;
                }
                cpf = CpfTreatment.RemoverMascaraCPF(cpf);


                if (CpfJaExiste(cpf))
                {
                    Console.WriteLine("O cpf já foi cadastrado.");
                    return 0 ;
                }

                string query = "INSERT INTO Cidadaos ('Nome', 'Cpf') VALUES (@Nome, @Cpf)";



                using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.GetConnection()))
                {
                    myCommand.Parameters.AddWithValue("@Nome", nome);
                    myCommand.Parameters.AddWithValue("@Cpf", cpf);

                    myCommand.ExecuteNonQuery();
                    Console.WriteLine("Cidadao Inserido com Sucesso!");
                    return 1;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no banco de dados ao criar cidadão: {ex.Message}\u001b[0m");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\u001b[31mErro de argumento inválido ao criar cidadão: {ex.Message}\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[31mErro inesperado ao criar cidadão: {ex.Message}\u001b[0m");
            }
            return 0;

        }
       

        public static int Read_t(string? nome = null, string? cpf = null)
        {
            try
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
                        myCommand.Parameters.AddWithValue("@Nome", nome);
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
                            Console.WriteLine("ID: {0} - Nome: {1} - CPF: {2}", result["id"], result["Nome"], CpfTreatment.FormatCPF(result["Cpf"].ToString()??""));
                        }
                        return 1;
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
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no banco de dados ao buscar cidadãos: : {ex.Message}\u001b[0m");

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\u001b[31mErro de operação inválida ao buscar cidadãos: : {ex.Message}\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[31mErro inesperado ao buscar cidadãos: : {ex.Message}\u001b[0m");
            }

            return 0;

        }


        public static int  update(int id, string novoNome, string novoCpf)
        {

            try
            {
                if (string.IsNullOrEmpty(novoNome) && string.IsNullOrEmpty(novoCpf))
                {
                    Console.WriteLine("É necessário fornecer um novo nome ou um novo cpf,");
                    return 0;
                }
                //Expressao Regular para validar CPF
                if (!CpfTreatment.ValidarCPF(novoCpf))
                {
                    Console.WriteLine("CPF inválido!");
                    return 0;
                }
               


                string query = "UPDATE Cidadaos SET ";
                List<string> updates = new List<string>();
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(novoNome))
                {
                    updates.Add("Nome = @novoNome");
                    parameters["@novoNome"] = novoNome;
                }

                if (!string.IsNullOrEmpty(novoCpf))
                {
                    novoCpf = CpfTreatment.RemoverMascaraCPF(novoCpf);
                    if (CpfJaExiste(novoCpf))
                    {

                        Console.WriteLine("O cpf já foi cadastrado.");
                        return 0;
                    }

                    updates.Add("Cpf = @novoCpf");
                    parameters["@novoCpf"] = novoCpf;
                }

                query += string.Join(", ", updates) + " WHERE id = @id";
                parameters["@id"] = id;

                using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.GetConnection()))
                {
                    foreach (var param in parameters)
                    {
                        myCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    int rowsAffected = myCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Cidadão atualizado com sucesso.");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Nenhum Cidadão encontrado com esse id.");
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no banco de dados ao atualizar cidadão: : {ex.Message}\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[31mErro inesperado ao atualizar cidadão: : {ex.Message}\u001b[0m");
            }

            return 0;
        }

        private static bool CpfJaExiste(string cpf)
        {

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Cidadaos WHERE Cpf = @Cpf";

                using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, Database.GetConnection()))
                {
                    checkCommand.Parameters.AddWithValue("@Cpf", cpf);

                    int cpfCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (cpfCount > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no banco de dados ao verificar CPF: {ex.Message}\u001b[0m");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[31mErro inesperado ao verificar CPF: {ex.Message}\u001b[0m");
                return false;
            }



            return false;
        }



        public static int Delete(string nome, string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(cpf))
                {
                    Console.WriteLine("É necessário fornecer nome & cpf a serem deletados");
                    return 0;
                }

                cpf = CpfTreatment.RemoverMascaraCPF(cpf);
                string query = "DELETE FROM Cidadaos WHERE Nome= @Nome AND Cpf= @Cpf";

                using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.GetConnection()))
                {
                    myCommand.Parameters.AddWithValue("@Nome", nome);
                    myCommand.Parameters.AddWithValue("@Cpf", cpf);

                    int rowsAffected = myCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Cidadão deletado com sucesso.");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Nenhum Cidadão encontrado com esse nome e Cpf.");
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"\u001b[31mErro no banco de dados ao deletar cidadão: {ex.Message}\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[31mErro inesperado ao deletar cidadão: {ex.Message}\u001b[0m");
            }

            return 0;
        }
    }
}
