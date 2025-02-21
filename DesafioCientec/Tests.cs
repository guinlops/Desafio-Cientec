namespace DesafioCientec
{
    internal class Tests
    {
        public static Database? db;

        public  Tests() {    
        }
        public void TestInit()
        {

            if (!TestFileExists())
            {
                db = TestCriacaoDoBanco();
                Test_validaCPF();
                TestInsercao();
                TestUpdate();
                TestLeitura();
                TestDelete();

                Console.WriteLine();
                Console.WriteLine("Aperte qualquer Tecla para continuar");
                Console.ReadKey();
                Console.Clear();
            }

            return;

        }

        private Database TestCriacaoDoBanco()
        {
            Database db = new Database("test_database.sqlite3");
            return db;
        }


        private void TestInsercao()
        {
            db?.OpenConnection();
            int count = 0;

            var originalConsoleOut = Console.Out;
            Console.SetOut(TextWriter.Null); // Bloqueia prints
 
            // Executa as funções Create sem exibir prints
            count += UserRepository.Create("TesteNome1", "000.000.000-11");
            count += UserRepository.Create("Sem Mascara", "00000000022");
            count += UserRepository.Create("TesteNome3", "000.000.000-33");
            count += UserRepository.Create("TesteNome4", "000.000.000-44");
            count += UserRepository.Create("TesteNome5", "000.000.000-55");
            count += UserRepository.Create("Mesmo CPF", "000.000.000-66");


            count += UserRepository.Create("Mesmo CPF", "000.000.000-66");

            // Restaura a saída original do console
            Console.SetOut(originalConsoleOut);

            db?.CloseConnection();
            if (count != 6)
            {
                Console.WriteLine($"Erro: Inserções esperadas = 6, obtidas = {count}");
            }
            else
            {
                Console.WriteLine("Todos os testes Insercao passaram!");
            }

        }

        private void TestLeitura()
        {
            int count = 0;
            var originalConsoleOut = Console.Out;
           
            db?.OpenConnection();
            Console.SetOut(TextWriter.Null); // Bloqueia prints

            // Executa as funções Read sem exibir prints

            
            count += UserRepository.Read_t(nome:"TesteNome1",cpf: "000.000.000-01");
            count += UserRepository.Read_t(nome:"TesteNome2", cpf:"000000.00002");
            count += UserRepository.Read_t(nome:"TesteNome3", cpf:"000.000.000-03");
            count += UserRepository.Read_t(nome:"TesteNome4",cpf: "00000000004");
            count += UserRepository.Read_t(nome:"TesteNome5", cpf:"000.000.000-05");
            count += UserRepository.Read_t(nome:"TesteNome6",cpf: "000.000.000-06");
            
            count += UserRepository.Read_t(nome:"TesteNome1");
            count += UserRepository.Read_t(nome:"TesteNome2");
            count += UserRepository.Read_t(nome:"TesteNome3");
            count += UserRepository.Read_t(nome:"TesteNome4");
            count += UserRepository.Read_t(nome:"TesteNome5");
            count += UserRepository.Read_t(nome:"TesteNome6");
            
            // Executa as funções Read sem exibir prints
            count += UserRepository.Read_t(cpf:"000.000.000-01");
            count += UserRepository.Read_t(cpf:"000.000.000-02");
            count += UserRepository.Read_t(cpf:"000.000.000-03");
            count += UserRepository.Read_t(cpf:"000.000.000-04");
            count += UserRepository.Read_t(cpf:"000.000.000-05");
            count += UserRepository.Read_t(cpf:"000.000.000-06");


            count+= UserRepository.Read_t();
            db?.CloseConnection();
            // Restaura a saída original do console
            Console.SetOut(originalConsoleOut);

            if (count != 19)
            {
                Console.WriteLine($"Erro: Leituras esperadas = 18, obtidas = {count}");
            }
            else
            {
                Console.WriteLine("Todos os testes Read passaram!");
            }

        }


        private void Test_validaCPF()
        {
            db?.OpenConnection();
            int count = 0;

            var originalConsoleOut = Console.Out;
            Console.SetOut(TextWriter.Null); // Bloqueia prints

            // Executa as funções Create sem exibir prints
            count += UserRepository.Create("TesteNome7", "abc");
            count += UserRepository.Create("TesteNome8", "!@#$.#$%-01");
            count += UserRepository.Create("TesteNome9", "xyz.123.456-78");
            count += UserRepository.Create("TesteNome10", "mno.890.abc-23");
            count += UserRepository.Create("TesteNome11", "000.000.000-0");
            count += UserRepository.Create("TesteNome12", "000000000067");

            // Restaura a saída original do console
            Console.SetOut(originalConsoleOut);

            //Debug.Assert(count == 6, $"Falha na inserção, count esperado: 6, obtido: {count}");
            db?.CloseConnection();
            if (count != 0)
            {
                Console.WriteLine($"Erro: Inserções esperadas = 0, obtidas = {count}");
            }
            else
            {
                Console.WriteLine("Todos os testes de validacao de CPF passaram!");
            }
        }

        private void TestDelete(bool reset = false)
        {
            int count = 0;

             var originalConsoleOut = Console.Out;
            Console.SetOut(TextWriter.Null); // Bloqueia prints



            db?.OpenConnection();
            count += UserRepository.Delete("TesteNome1", "000.000.000-01");
            count += UserRepository.Delete("TesteNome2", "000.000.000-02");
            count += UserRepository.Delete("TesteNome3", "000.000.000-03");
            count += UserRepository.Delete("TesteNome4", "000.000.000-04");
            count += UserRepository.Delete("TesteNome5", "000.000.000-05");
            count += UserRepository.Delete("TesteNome6", "000.000.000-06");

            // Restaura a saída original do console
            Console.SetOut(originalConsoleOut);

            if (!reset)
            {
                if (count != 6)
                {
                    Console.WriteLine($"Erro: Deletes esperados = 6, obtidas = {count}");
                }
                else
                {
                    Console.WriteLine("Todos os testes Delete passaram!");
                }
                db?.CloseConnection();

            }
        }

        private void TestUpdate() //Funcionando apenas ao criar o banco pela primeira vez. Já que os Id's sao estáticos. Se for necessário, é preciso excluir o arquivo test_database.sq3lite.
        {
            db?.OpenConnection();
            int count = 0;

            var originalConsoleOut = Console.Out;
            Console.SetOut(TextWriter.Null); // Bloqueia prints


            //Update do nome e do cpf. Obrigatoriametne deve ser inserido o cpf,id e nome.
            //Se não for necessário editar algum campo, é precso inserir mais uma vez
            count += UserRepository.update(1, "TesteNome1", "000.000.000-01");
            count += UserRepository.update(2, "TesteNome2", "000.000.000-02");
            count += UserRepository.update(3, "TesteNome3", "000.000.000-03");
            count += UserRepository.update(4, "TesteNome4", "000.000.000-04");
            count += UserRepository.update(5, "TesteNome5", "000.000.000-05");
            count += UserRepository.update(6, "TesteNome6", "000.000.000-06");

            count += UserRepository.update(1,"TestUpdateCpfInvalido", "abc");
            count += UserRepository.update(2, "TestUpdateCpfInvalido8", "!@#$.#$%-01");
            count += UserRepository.update(3, "TestUpdateCpfInvalido9", "xyz.123.456-78");
            count += UserRepository.update(4, "TestUpdateCpfInvalido10", "mno.890.abc-23");
            count += UserRepository.update(5, "TestUpdateCpfInvalido11", "000.000.000-0");
            count += UserRepository.update(6, "TestUpdateCpfInvalido12", "000000000067");

            // Restaura a saída original do console
            Console.SetOut(originalConsoleOut);

            db?.CloseConnection();
            if (count != 6)
            {
                Console.WriteLine($"Erro: Updates esperados = 6, obtidas = {count}");
            }
            else
            {
                Console.WriteLine("Todos os testes update passaram!");
            }
        }

        private bool TestFileExists()
        {
            if (File.Exists($"./test_database.sqlite3"))
            {
                Console.WriteLine("\u001b[31mExclua o arquivo test_database.sqlite3 localizado em ...\\bin\\Debug\\net8.0\\test_database.sqlite3 para executar testes\u001b[0m ");
                Console.WriteLine("Aperte qualquer Tecla para voltar");
                Console.ReadKey();
                Console.Clear();
                //Environment.Exit(0);
                return true;
            }

            return false;
            
        }


        
        
    }
}


