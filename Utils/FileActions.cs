namespace SpecflowSelenium.Utils
{
    /// <summary>
    /// Classe responsável por métodos relacionado a manipulação de arquivos.
    /// </summary> 
    public static class FileActions
    {
        /// <summary>
        /// Obtém o caminho local de um arquivo.
        /// </summary>
        /// <param name="caminho">Caminho relativo para localização do arquivo a ser lido.</param>
        /// <returns>Caminho local do arquivo.</returns>
        public static string GetPathFileLocal(string caminho)
        {
            using FileStream arquivo = File.OpenRead(caminho);
            return arquivo.Name;
        }

        /// <summary>
        /// Realiza a leitura de um arquivo.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho relativo para localização do arquivo a ser lido</param>
        /// <returns>Arquivo lido</returns>
        public static string[] ReadFile(string caminhoArquivo)
        {
            return File.ReadAllLines(caminhoArquivo);
        }
    }
}
