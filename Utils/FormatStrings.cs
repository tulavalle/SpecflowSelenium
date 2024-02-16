using System.Globalization;
using System.Text;

namespace SpecflowSelenium.Utils
{
    public static partial class FormatStrings
    {
        /// <summary>
        /// Permite realizar replace da quebra de linha desejada pelo padrão de quebra de linha do ambiente de execução do teste.
        /// </summary>
        /// <param name="value">Informa texto com a quebra de linha a ser convertida para nova linha com formato do ambiente</param>
        /// <param name="newLine">Informa o formato que deve ser convertido para variável de ambiente. por padrão temos:"\r\n"</param>
        /// <returns>O texto desejado com a quebra de linha formatado conforme padrão do ambiente</returns>
        public static string ReplaceNewLineForEnvironmentNewLine(this string value, string newLine = "\r\n")
        {
            return value.Replace(newLine, ReturnEnvironmentNewLine());
        }

        /// <summary>
        /// Formata o caminho informado.
        /// </summary>
        /// <param name="path">Caminho a ser formatado</param>
        /// <returns></returns>
        public static string FormatPath(this string path)
        {
            path = path.Replace('\\', '/');
            path = path.Replace("//", "/");
            return FormatPath().Replace(path, string.Empty);
        }

        /// <summary>
        /// Busca o valor que o ambiente usa para Nova linha.
        /// </summary>
        /// <returns>O texto com formato da quebra de linha do ambiente</returns>
        public static string ReturnEnvironmentNewLine()
        {
            return Environment.NewLine;
        }

        /// <summary>
        /// Remova os acentos do valor esperado.
        /// </summary>
        /// <param name="value">Valor a set formatado</param>
        /// <returns>Valor sem acentuação</returns>
        public static string RemoveAccents(string value)
        {
            string normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Remova os caracteres especiais do valor esperado.
        /// </summary>
        /// <param name="value">Valor a set formatado</param>
        /// <returns>Valor sem acentuação</returns>
        public static string RemoveSpecialChars(string value)
        {
            string pattern = "[^a-zA-Z0-9 ]";
            return Regex.Replace(value, pattern, "");
        }

        /// <summary>
        /// Remova os caracteres especiais e espaços do valor esperado.
        /// </summary>
        /// <param name="value">Valor a set formatado</param>
        /// <returns>Valor sem caracteres especiais e sem espaços</returns>
        public static string RemoveSpecialCharsAndSpaces(string value)
        {
            string pattern = "[^a-zA-Z0-9]";
            return Regex.Replace(value, pattern, "");
        }

        /// <summary>
        /// Verifica se o texto contém exatamente a palavra desejada.
        /// </summary>
        /// <param name="tesxtFull"></param>
        /// <param name="textContains"></param>
        /// <returns>Retorna se texto contém exatamente a palavra desejada.</returns>
        public static bool ContainsSubstring(string tesxtFull, string textContains)
        {
            string pattern = $@"\b{Regex.Escape(textContains)}\b";
            return Regex.IsMatch(tesxtFull, pattern);
        }

        /// <summary>
        /// Verifica se a data informada atente ao formato do regex informado.
        /// </summary>
        /// <param name="regextFormat">Regex do formato da data.</param>
        /// <param name="data">Data a ter o formato verificado</param>
        /// <returns>Retorna se a data informada atende ao formato informado no regex</returns>
        public static bool CheckDateTimeFormat(string regextFormat, string data)
        {
            return Regex.IsMatch(data, regextFormat);
        }

        [GeneratedRegex("([a-zA-Z]+):/")]
        private static partial Regex RegexPath();
        [GeneratedRegex("[a-zA-Z]:.*?\\\\")]
        private static partial Regex FormatPath();
    }
}
