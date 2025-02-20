using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCientec
{
    internal class App
    {
        public App() { }


        string selected_font = "✅ \u001b[32m";
        string selected_font_Color = "\u001b[32m";
        string reset_font_Color = "\u001b[0m";
        private int option = 1;
        Database dbObj = new Database();
        public void init()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Console.WriteLine("\nUse ⬆️ and ⬇️ para navegar e pressione \u001b[32mENTER\u001b[0m para selecionar 👍👍👍");
            Menu();
        }


        private void Menu()
        {
            while (option != 8)
            {
                option = CrudOptions();

                dbObj.OpenConnection();
                switch (option)
                {

                    case 1:
                        //TRY
                        Console.Clear();
                        Console.WriteLine("CREATE");
                        string inp1, inp2;
                        Console.Write("Digite o Nome: ");
                        inp1 = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Digite o CPF no formato XXXXXXXXXXX ou XXX.XXX.XXX-YY: ");
                        inp2 = Console.ReadLine();
                        Console.WriteLine();
                        UserRepository.Create(inp1, inp2);
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Consulta geral");
                        UserRepository.Read_t();
                        Console.WriteLine();
                        option = 1;
                        break;

                        
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Consulta por Nome");
                        Console.Write("Digite o Nome: ");
                        string? nome= Console.ReadLine();
                        UserRepository.Read_t(nome:nome);
                        Console.WriteLine();
                        option = 1;
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Consulta por CPF");
                        Console.Write("Digite o CPF: ");
                        string? cpf= Console.ReadLine();

                        UserRepository.Read_t(cpf: cpf);
                        Console.WriteLine();
                        option = 1;
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("Consulta por CPF e por Nome");
                        Console.Write("Digite o CPF: ");
                        cpf = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Digite o Nome: ");
                        nome= Console.ReadLine();
                        UserRepository.Read_t(nome:nome, cpf: cpf);
                        Console.WriteLine();
                       
                        Console.WriteLine();
                        option = 1;
                        break;


                    case 6:
                        Console.Clear();
                        Console.WriteLine("UPDATE");
                        Console.WriteLine("Digite o id da linha a ser editada: ");
                        int id;
                        int.TryParse(Console.ReadLine(), out id);

                        Console.Write("Digite o novo nome do artista: ");
                        //artist = Console.ReadLine();
                        Console.WriteLine();

                        Console.Write("Digite o novo Title: ");
                        //title = Console.ReadLine();
                        Console.WriteLine();
                        //UPDATE FUNCTION
                        
                        Console.WriteLine();
                        option = 1;
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine("DELETE");
                        Console.Write("Digite o Artist: ");
                        //artist = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Digite o Title: ");
                        //title = Console.ReadLine();
                        Console.WriteLine();
                        //DELETE FUNCTION

                        
                        Console.WriteLine();
                        option = 1;
                        break;


                    case 8:
                        Console.Clear();
                        Console.WriteLine("Tchau!👋👋👋");
                        dbObj.CloseConnection();
                        return;

                }
                dbObj.CloseConnection();

                option = MenuOptions();
            }
            Console.WriteLine("Tchau!");
        }

        private int CrudOptions()
        {
            ConsoleKeyInfo key;
            bool isSelected = false;
            (int left, int top) = Console.GetCursorPosition();



            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? selected_font : "   ")}Cadastro\u001b[0m");
                Console.WriteLine($"{(option == 2 ? selected_font : "   ")}Consultar todos os usuários cadastrados 🔎\u001b[0m");
                Console.WriteLine($"{(option == 3 ? selected_font : "   ")}Consultar por Nome 🔎\u001b[0m");
                Console.WriteLine($"{(option == 4 ? selected_font : "   ")}Consultar por CPF 🔎\u001b[0m");
                Console.WriteLine($"{(option == 5 ? selected_font : "   ")}Consultar por CPF e Nome 🔎\u001b[0m");
                Console.WriteLine($"{(option == 6 ? selected_font : "   ")}Editar ✏️\u001b[0m");
                Console.WriteLine($"{(option == 7 ? selected_font : "   ")}Deletar 🗑️\u001b[0m");
                Console.WriteLine($"{(option == 8 ? selected_font : "   ")}Sair 😤\u001b[0m");

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 8 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 8 : option - 1);
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
                        option = (option == 2 ? 8 : 1);
                        Console.Clear();
                        isSelected = true;
                        break;

                }
            }
            return option;

        }
    }
}
