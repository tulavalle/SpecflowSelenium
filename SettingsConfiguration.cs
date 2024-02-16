namespace SpecflowSelenium
{
    /// <summary>
    /// Class responsible for reading the appsettings.json configuration file of the test project.
    /// </summary>
    public class SettingsConfiguration
    {
        protected SettingsConfiguration() { }

        private static string testDirectory = string.Empty;
        public static string TestDirectory { get => testDirectory; }

        private static IConfigurationRoot configuration;

        /// <summary>
        /// Verifica de já possui as configurações, se não, as procura no appsettings.json.
        /// </summary>
        public static IConfigurationRoot Configuration
        {
            get
            {
                if (configuration != null)
                    return configuration;

                Setup();
                return configuration;
            }
        }

        /// <summary>
        /// Inicializa o diretório padrão, onde o arquivo de configuração appsettings.json está localizado.
        /// </summary>
        /// <param name="testDirectory">Diretório padrão do projeto</param>
        public static void Initialize(string testDirectory)
        {
            SettingsConfiguration.testDirectory = testDirectory;
        }

        /// <summary>
        /// Busca as configurações do appsettings.json.
        /// </summary>
        public static void Setup(string configFile = "appsettings.json")
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile(configFile);

            configuration = builder.Build();
        }
    }
}
