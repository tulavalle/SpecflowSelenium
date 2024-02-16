using SpecFlow.Actions.Configuration;
using System.Drawing;

namespace SpecflowSelenium.Support
{
    public class BaseTestCore
    {
        public ScenarioContext ScenarioContext;
        private readonly bool useSpecflowActions;
        private readonly string currentBrowserTarget;
        private readonly string currentWindowTarget;
        private static string currentUrlTarget;
        private static int sizeWidthTargets;
        private static int sizeHeightTargets;
        private readonly ISpecFlowActionsConfiguration SpecFlowActionsConfiguration;
        public IWebDriver Driver { get; set; }
        public string CurrentTestName { get; set; }
        public static string Browser { get; set; }
        protected static string FilePath { get; private set; }
        public string FullPathToLivingDoc { get; set; }
        public static string ScreenshotDirectory { get; set; }
        public static string ScreenshotS3Directory { get; set; }
        public static bool ShouldUploadToS3 { get; set; }
        private string FileName { get; set; }

        public BaseTestCore(ScenarioContext scenarioContext, ISpecFlowActionsConfiguration specFlowActionsConfiguration)
        {
            ScenarioContext = scenarioContext;
            SpecFlowActionsConfiguration = specFlowActionsConfiguration;
            useSpecflowActions = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:specflowActions:useSpecflowActions"]);

            if (useSpecflowActions)
            {
                currentBrowserTarget = SpecFlowActionsConfiguration.Get("browser_target", GlobalVariables.Configuration["appsettings:browser"]);
                currentUrlTarget = SpecFlowActionsConfiguration.Get("url_target", GlobalVariables.Configuration["appsettings:urlBase"]);
                currentWindowTarget = SpecFlowActionsConfiguration.Get("window_target");
                sizeWidthTargets = Convert.ToInt32(SpecFlowActionsConfiguration.Get("size_width_target", GlobalVariables.Configuration["appsettings:windowWidth"]));
                sizeHeightTargets = Convert.ToInt32(SpecFlowActionsConfiguration.Get("size_height_target", GlobalVariables.Configuration["appsettings:windowHeight"]));
            }
        }

        /// <summary>
        /// Prepara a execução em relação aos diretórios:
        /// - Diretório padrão do projeto;
        /// - Diretório de screenshots
        /// - Diretório de arquivos
        /// - Inicializa o SettingsConfiguration
        /// </summary>
        public static void PrepareExecution()
        {
            GlobalVariables.DirectoryDefaultProject = Environment.CurrentDirectory = Path.GetDirectoryName(typeof(BaseTestCore).Assembly.Location);

            GlobalVariables.Configuration = SettingsConfiguration.Configuration;
            SettingsConfiguration.Initialize(GlobalVariables.DirectoryDefaultProject);

            ScreenshotDirectory = GlobalVariables.Configuration["appsettings:directoriesDefault:screenshot"];
            ScreenshotS3Directory = $"{GlobalVariables.Configuration["appsettings:s3Aws:screenshotS3Directory"]}_{DateTime.Now:ddMMyyyy_HHmm}".Replace("/", "_");

            FilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalVariables.Configuration["appsettings:directoriesDefault:filePathDirectory"]));
        }

        /// <summary>
        /// Realiza a inicialização do driver e aplica as opções desejadas para o navegador.
        /// </summary>
        public void DriverInitialize()
        {
            var plataform = GlobalVariables.Configuration["appsettings:seleniumGrid:plataform"];
            Browser = GlobalVariables.Configuration["appsettings:browser"];
            var useSeleniumGrid = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:seleniumGrid:useSeleniumGrid"]);
            var timeZone = GlobalVariables.Configuration["appsettings:timeZone"];
            var browserLanguage = GlobalVariables.Configuration["appsettings:browserLanguage"];
            var recordVideo = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:recordVideo"]);
            var headlessMode = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:headlessMode"]);
            var directoryDefaultDownload = GlobalVariables.Configuration["appsettings:directoriesDefault:download"];
            var useAcceptInsecureCertificatesGrid = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:useAcceptInsecureCertificates"]);
            var hubSeleniumGridUrl = GlobalVariables.Configuration["appsettings:seleniumGrid:hubSeleniumGridUrl"];
            var deleteCookies = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:deleteCookies"]);
            var useBrowserTargets = useSpecflowActions && Convert.ToBoolean(GlobalVariables.Configuration["appsettings:specflowActions:useBrowserTargets"]);
            var useBrowserByTag = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:useBrowserByTag"]);
            var timeout = Convert.ToInt32(GlobalVariables.Configuration["appsettings:timeout"]);

            if (useBrowserTargets)
            {
                Browser = currentBrowserTarget;
            }
            else if (useBrowserByTag)
            {
                var tags = ScenarioContext.ScenarioInfo.Tags;
                var browserMapping = new Dictionary<string, string>
                    {
                        { "CHROME", "chrome" },
                        { "EDGE", "edge" },
                        { "FIREFOX", "firefox" },
                        { "SAFARI", "safari" }
                    };

                foreach (var tag in tags)
                {
                    var upperTag = tag.ToUpper();
                    if (browserMapping.TryGetValue(upperTag, out var mappedBrowser))
                    {
                        Browser = mappedBrowser;
                        break;
                    }
                }
            }

            if (useSeleniumGrid)
            {
                Driver = DriverFactory.GetBrowser(plataform, hubSeleniumGridUrl, Browser, headlessMode, directoryDefaultDownload, useAcceptInsecureCertificatesGrid, recordVideo, timeZone, browserLanguage, timeout);
                FilePath = "/home/seluser/shared";
            }
            else
            {
                Driver = DriverFactory.GetBrowser(Browser, headlessMode, GlobalVariables.DirectoryDefaultProject, directoryDefaultDownload, useAcceptInsecureCertificatesGrid, recordVideo, timeZone, browserLanguage, timeout);
            }

            if (deleteCookies) Driver.Manage().Cookies.DeleteAllCookies();

            ScenarioContext.Set(Driver);
        }

        /// <summary>
        /// Insere a url da página desejada e redimensiona a tela conforme desejado.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="url">Descrição da url desejada</param>
        public void AccessPage(IWebDriver driver, string url = null)
        {
            ExceptionsFactory.FailReportedDefectOrInDevelopmentScenario(ScenarioContext);

            var useWindowTarget = useSpecflowActions && Convert.ToBoolean(GlobalVariables.Configuration["appsettings:specflowActions:useWindowTarget"]);
            var windowMaximize = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:windowMaximize"]);
            var windowFullscreen = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:windowFullscreen"]);
            var windowSize = Convert.ToBoolean(GlobalVariables.Configuration["appsettings:windowSize"]);
            var windowHeight = Convert.ToInt32(GlobalVariables.Configuration["appsettings:windowHeight"]);
            var windowWidth = Convert.ToInt32(GlobalVariables.Configuration["appsettings:windowWidth"]);

            if (string.IsNullOrEmpty(url))
            {
                url = useSpecflowActions && Convert.ToBoolean(GlobalVariables.Configuration["appsettings:specflowActions:useUrlTargets"])
                    ? currentUrlTarget
                    : GlobalVariables.Configuration["appsettings:urlBase"];
            }

            driver?.Navigate().GoToUrl(url);

            ResizeWindow(driver, windowMaximize, windowFullscreen, windowSize, windowWidth, windowHeight, useWindowTarget, currentWindowTarget);
        }

        /// <summary>
        /// Monta o caminho completo para salvar o arquivo.
        /// </summary>
        /// <param name="isStep">Indica se o hook é relacionado a step do cenário</param>
        /// <param name="stepCounter">Valor do contador de steps do contexto de cenário</param>
        /// <param name="filename">Nome do arquivo</param>
        /// <returns>Retorna o caminho do arquivo formatado na extensão .png</returns>
        public string ReturnFilePathFormatted(bool isStep, int stepCounter, string filename = null)
        {
            if (string.IsNullOrEmpty(filename)) filename = ReturnFileName();

            var filePathFormatted = ScreenshotCore.ReturnFilePathFormatted(ScenarioContext, ScreenshotDirectory, filename, Browser, isStep, stepCounter);

            return filePathFormatted;
        }

        /// <summary>
        /// Realiza screenshot da tela.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="isStep">Indica se o hook é relacionado a step do cenário</param>
        /// <param name="stepCounter">Valor do contador de steps do contexto de cenário</param>
        /// <param name="isShouldUploadToS3">Informa se o screenshot deve ser copiado para o S3 bucket (Amazon)</param>
        /// <param name="filepath">caminho do arquivo completo</param>
        /// <param name="filename">Nome do arquivo</param>
        public void MakeScreenshot(IWebDriver driver, bool isStep, int stepCounter, bool isShouldUploadToS3, string filepath = null, string filename = null)
        {
            if (!Directory.Exists(ScreenshotDirectory))
                Directory.CreateDirectory(ScreenshotDirectory);

            if (string.IsNullOrEmpty(filepath))
                filename ??= ReturnFileName();

            ScreenshotCore.MakeScreenshot(driver, ScenarioContext, ScreenshotDirectory, ScreenshotS3Directory, Browser, isStep, stepCounter, isShouldUploadToS3, filepath, filename);
        }

        /// <summary>
        /// Realiza screenshot e encerra o driver corrente.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="isStep">Indica se o hook é relacionado a step do cenário</param>
        /// <param name="stepCounter">Valor do contador de steps do contexto de cenário</param>
        /// <param name="isShouldUploadToS3">Informa se o screenshot deve ser copiado para o S3 bucket (Amazon)</param>
        /// <param name="filepath">caminho do arquivo completo</param>
        /// <param name="filename">Nome do arquivo</param>
        public void MakeScreenshotAndClosedDriver(IWebDriver driver, bool isStep, int stepCounter, bool isShouldUploadToS3, string filepath = null, string filename = null)
        {
            Driver = driver;

            try
            {
                MakeScreenshot(Driver, isStep, stepCounter, isShouldUploadToS3, filepath, filename);
            }
            catch (Exception)
            {
                CloseDriver(Driver);
            }

            if (Driver != null)
                CloseDriver(Driver);
        }

        /// <summary>
        /// Encerra o driver corrente
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        public void CloseDriver(IWebDriver driver)
        {
            Driver = driver;

            DriverFactory.CloseDriver(Driver);
            DriverFactory.QuitDriver(Driver);
            DriverFactory.DisposeDriver(Driver);
            Driver = null;
        }

        private void ResizeWindow(IWebDriver driver, bool windowMaximize, bool windowFullscreen, bool windowSize, int windowWidth, int windowHeight, bool useWindowTarget, string currentWindowTarget)
        {
            Driver = driver;

            if (windowMaximize || (useWindowTarget && currentWindowTarget.ToUpper() == "MAXIMIZE"))
            {
                Driver.Manage().Window.Maximize();
            }
            else if (windowFullscreen || (useWindowTarget && currentWindowTarget.ToUpper() == "FULLSCREEN"))
            {
                Driver.Manage().Window.FullScreen();
            }
            else if (windowSize || (useSpecflowActions && useWindowTarget && currentWindowTarget.ToUpper() == "SIZE"))
            {
                Driver.Manage().Window.Size = useSpecflowActions && currentWindowTarget.ToUpper() == "SIZE"
                    ? new Size(sizeWidthTargets, sizeHeightTargets)
                    : new Size(windowWidth, windowHeight);
            }
            else
            {
                Driver.Manage().Window.Maximize();
            }
        }

        private string ReturnFileName()
        {
            FileName = ScreenshotCore.ReturnScenarioName(ScenarioContext);

            return FileName.Replace("*", "").Replace("\\", "").Replace("/", "");
        }
    }
}
