using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesafioCientec
{
    internal class CpfTreatment
    {

        public static bool ValidarCPF(string cpf)
        {
            string pattern = @"^[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}-?[0-9]{2}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(cpf);
        }

        public static string RemoverMascaraCPF(string cpf)
        {
            // Remove tudo que não seja número
            string apenasNumeros = new string(cpf.Where(char.IsDigit).ToArray());
            return apenasNumeros;
        }

        public static string FormatCPF(string cpf)
        {
            // Certifique-se de que o CPF tem 11 números
            if (cpf.Length != 11)
            {
                throw new ArgumentException("O CPF deve conter 11 dígitos.");
            }

            // Formata o CPF
            return string.Format("{0}.{1}.{2}-{3}",
                cpf.Substring(0, 3),
                cpf.Substring(3, 3),
                cpf.Substring(6, 3),
                cpf.Substring(9, 2));
        }

    }
}
