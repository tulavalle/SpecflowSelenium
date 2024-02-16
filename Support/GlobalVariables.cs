namespace SpecflowSelenium.Support
{
    /// <summary>
    /// Classe responsável por inicializar variáveis globais para Driver e Timeout.
    /// </summary>
    public class GlobalVariables
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static string DirectoryDefaultProject { get; set; }

        /// <summary>
        /// Driver corrente.
        /// </summary>
        public IWebDriver Driver { get; set; }

        /// <summary>
        /// Timeout padrão.
        /// </summary>
        public static int Timeout { get; set; }

        /// <summary>
        /// WebDriverWait corrente.
        /// </summary>
        public WebDriverWait Wait { get; set; }

        /// <summary>
        /// Initialize global variables.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="timeout">Tempo máximo de espera</param>
        /// <exception cref="Exception">Retorna mensagem caso não seja pssível ler a confioguração de tempo limite</exception>
        public GlobalVariables(IWebDriver driver, int timeout)
        {
            Driver = driver;

            try
            {
                Timeout = timeout;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível ler a configuração de tempo limite.", e);
            }

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Timeout));
        }
    }
}
