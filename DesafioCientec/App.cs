using System.Text;
namespace DesafioCientec
{
    internal class App
    {
        public App() { }

        const string selected_font = "✅ \u001b[32m";
        //string selected_font_Color = "\u001b[32m";      
        
        private int option = 1;
        Database dbObj = new Database("database.sqlite3");
        public void init()
        {
            InitConsoleStyles();   
            Menu();
        }
        private void Menu()
        {
            while (option != 9)
            {
                Console.WriteLine("\nUse ⬆️ and ⬇️ para navegar e pressione \u001b[32mENTER\u001b[0m para selecionar 👍👍👍");
                string? nome, cpf;
                option = CrudOptions();

                dbObj.OpenConnection();
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Tests test = new Tests();
                        CallTest();
                        break;
                    case 2:
                        //TRY
                        Console.Clear();
                        Console.WriteLine("CREATE");                    
                        Console.Write("Digite o Nome: ");
                        nome = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Digite o CPF no formato XXXXXXXXXXX ou XXX.XXX.XXX-YY: ");
                        cpf = Console.ReadLine();
                        Console.WriteLine();
                        UserRepository.Create(nome ?? string.Empty, cpf ?? string.Empty);                      
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Consulta geral");
                        UserRepository.Read_t();
                        Console.WriteLine();
                        option = 1;
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Consulta por Nome");
                        Console.Write("Digite o Nome: ");
                        nome = Console.ReadLine();
                        UserRepository.Read_t(nome: nome ?? string.Empty);                       
                        Console.WriteLine();
                        option = 1;
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Consulta por CPF no formato XXXXXXXXXXX ou XXX.XXX.XXX-YY");
                        Console.Write("Digite o CPF: ");
                        cpf = Console.ReadLine();

                        UserRepository.Read_t(cpf: cpf ?? string.Empty);
                        Console.WriteLine();
                        option = 1;
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("Consulta por CPF e por Nome");
                        Console.Write("Digite o CPF no formato XXXXXXXXXXX ou XXX.XXX.XXX-YY: ");
                        cpf = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Digite o Nome: ");
                        nome = Console.ReadLine();
                        UserRepository.Read_t(nome: nome ?? string.Empty, cpf: cpf ?? string.Empty);
                        Console.WriteLine();

                        Console.WriteLine();
                        option = 1;
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("UPDATE");
                        Console.WriteLine("Digite o id da linha a ser editada: ");
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        Console.Write($"Digite o nome corrigido ou aperte \u001b[32menter\u001b[0m para não alterar : ");
                        nome = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write($"Digite o CPF corrigido no formato XXXXXXXXXXX ou XXX.XXX.XXX-YY ou aperte \u001b[32menter\u001b[0m para não alterar : ");
                        cpf = Console.ReadLine();
                        Console.WriteLine();
                        UserRepository.update(id, nome ?? string.Empty, cpf ?? string.Empty);
                        Console.WriteLine();
                        option = 1;
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("DELETE");
                        Console.Write("Digite o Nome: ");
                        nome = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Digite o cpf: ");
                        cpf = Console.ReadLine();
                        Console.WriteLine();
                        UserRepository.Delete(nome ?? string.Empty, cpf ?? string.Empty);
                        Console.WriteLine();
                        option = 1;
                        break;
                    case 9:
                        Console.Clear();
                        CloseApp();
                        return;
                }
                dbObj.CloseConnection();
                option = MenuOptions();
            }
            CloseApp();

        }
        private int CrudOptions()
        {
            ConsoleKeyInfo key;
            bool isSelected = false;
            (int left, int top) = Console.GetCursorPosition();
            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? selected_font : "   ")}\u001b[43mExecutar Testes Automáticos 📝📝📝\u001b[0m");
                Console.WriteLine($"{(option == 2 ? selected_font : "   ")}Cadastro\u001b[0m");
                Console.WriteLine($"{(option == 3 ? selected_font : "   ")}Consultar todos os usuários cadastrados 🔎\u001b[0m");
                Console.WriteLine($"{(option == 4 ? selected_font : "   ")}Consultar por Nome 🔎\u001b[0m");
                Console.WriteLine($"{(option == 5 ? selected_font : "   ")}Consultar por CPF 🔎\u001b[0m");
                Console.WriteLine($"{(option == 6 ? selected_font : "   ")}Consultar por CPF e Nome 🔎\u001b[0m");
                Console.WriteLine($"{(option == 7 ? selected_font : "   ")}Editar ✏️\u001b[0m");
                Console.WriteLine($"{(option == 8 ? selected_font : "   ")}Deletar 🗑️\u001b[0m");
                Console.WriteLine($"{(option == 9 ? selected_font : "   ")}Sair 😤\u001b[0m");
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 9 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 9 : option - 1);
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;

                }
            }
            return option;
        }
        private int MenuOptions()
        {

            ConsoleKeyInfo key;
            bool isSelected = false;
            (int left, int top) = Console.GetCursorPosition();
            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? selected_font : "   ")}Voltar ao menu 🔙🔙🔙\u001b[0m");
                Console.WriteLine($"{(option == 2 ? selected_font : "   ")}Sair 😤\u001b[0m");

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 2 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 2 : option - 1);
                        break;

                    case ConsoleKey.Enter:
                        option = (option == 2 ? 9 : 1);
                        Console.Clear();
                        isSelected = true;
                        break;

                }
            }
            return option;

        }


        private void CallTest()
        {

            Tests test = new Tests();
            test.TestInit();
            test = null!; // Remove a referência (GC desaloca quando necessário)
        }

        private void CloseApp()
        {
            
            Console.WriteLine("Tchau!👋👋👋");
            dbObj = null!;
            dbObj.CloseConnection();

            Environment.Exit(0);
        }

        private void InitConsoleStyles()
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
        }


    }
}
