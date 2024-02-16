using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SpecflowSelenium.Drivers
{
    /// <summary>
    /// Methods responsible for the actions of Selenium Drivers.
    /// </summary>
    public class DriverFactory
    {
        protected DriverFactory() { }

        /// <summary>
        /// Obtém o IWebDriver desejado e configura as opções do navegador. 
        /// </summary>
        /// <param name="browserId">Navegador desejado</param>
        /// <param name="headlessMode">Informa se abre o navegador no modo headless ou não</param>
        /// <param name="directoryDefault">Informa o diretório padrão do projeto</param>     
        /// <param name="directoryDefaultDownload">Informa o diretório padrão para download</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <returns>Driver do Navegador desejado</returns>
        /// <exception cref="NotSupportedException">Navegador não suportado</exception>
        public static IWebDriver GetBrowser(string browserId, bool headlessMode, string directoryDefault, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            string lowerBrowserId = browserId.ToUpper();

            return lowerBrowserId switch
            {
                "CHROME" => GetChromeDriver(headlessMode, directoryDefault, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                "EDGE" => GetEdgeDriver(headlessMode, directoryDefault, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                "FIREFOX" => GetFirefoxDriver(headlessMode, directoryDefault, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                //"SAFARI" => GetSafariDriver(headlessMode, directoryDefault, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                _ => throw new NotSupportedException("navegador não suportado: <null>"),
            };
        }

        /// <summary>
        /// Obtém o RemoteWebDriver desejado e configura as opções do navegador.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="hubSeleniumGridUrl">Hub do Selenium Grid</param>
        /// <param name="browserId">Navegador desejado</param>
        /// <param name="headlessMode">Informa se é para executar o navegador no modo headless</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão para download</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <returns>Retorna o driver para o navegador desejado</returns>
        /// <exception cref="NotSupportedException">Navegador não suportado</exception>
        public static RemoteWebDriver GetBrowser(string plataform, string hubSeleniumGridUrl, string browserId, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            string lowerBrowserId = browserId.ToUpper();
            return lowerBrowserId switch
            {
                "CHROME" => GetChromeRemoteWebDriver(plataform, hubSeleniumGridUrl, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                "EDGE" => GetEdgeRemoteWebDriver(plataform, hubSeleniumGridUrl, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                "FIREFOX" => GetFirefoxRemoteWebDriver(plataform, hubSeleniumGridUrl, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                //"SAFARI" => GetSafariRemoteWebDriver(plataform, hubSeleniumGridUrl, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage, timeout),
                string browser => throw new NotSupportedException($"{browser} não é um navegador suportado"),
                _ => throw new NotSupportedException("navegador não suportado: <null>"),
            };
        }

        /// <summary>
        /// Obtém o IWebDriver do Chrome.
        /// </summary>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefault">Informa o diretório padrão do projeto</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver do Chrome</returns>
        private static IWebDriver GetChromeDriver(bool headlessMode, string directoryDefault, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeServices = ChromeDriverService.CreateDefaultService(directoryDefault);
            var chromeOptions = DriverOptionsFactory.GetChromeOptions(null, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new ChromeDriver(chromeServices, chromeOptions, TimeSpan.FromSeconds(timeout));
        }


        /// <summary>
        /// Obtém o IWebDriver do Edge.
        /// </summary>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefault">Informa o diretório padrão do projeto</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver para o Navegador Edge</returns>
        private static IWebDriver GetEdgeDriver(bool headlessMode, string directoryDefault, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var edgeServices = EdgeDriverService.CreateDefaultService(directoryDefault);
            var edgeOptions = DriverOptionsFactory.GetEdgeOptions(null, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new EdgeDriver(edgeServices, edgeOptions, TimeSpan.FromSeconds(timeout));
        }


        /// <summary>
        /// Obtém o IWebDriver do Firefox.
        /// </summary>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefault">Informa o diretório padrão do projeto</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver para o Navegador Firefox</returns>
        private static IWebDriver GetFirefoxDriver(bool headlessMode, string directoryDefault, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var firefoxServices = FirefoxDriverService.CreateDefaultService(directoryDefault);
            var firefoxOptions = DriverOptionsFactory.GetFirefoxOptions(null, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new FirefoxDriver(firefoxServices, firefoxOptions, TimeSpan.FromSeconds(timeout));
        }


        /// <summary>
        /// Obtém o IWebDriver do Chrome.
        /// </summary>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefault">Informa o diretório padrão do projeto</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver para o Navegador Safari</returns>
        private static IWebDriver GetSafariDriver(bool headlessMode, string directoryDefault, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            var safariServices = SafariDriverService.CreateDefaultService(directoryDefault);
            var safariOptions = DriverOptionsFactory.GetSafariOptions(null, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new SafariDriver(safariServices, safariOptions, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Obtém o RemoteWebDriver do Chrome.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="hubSeleniumGridUrl">Hub do selenium Grid</param>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver para o Navegador Safari</returns>
        private static RemoteWebDriver GetChromeRemoteWebDriver(string plataform, string hubSeleniumGridUrl, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeOptions = DriverOptionsFactory.GetChromeOptions(plataform, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new RemoteWebDriver(new Uri(hubSeleniumGridUrl), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Obtém o RemoteWebDriver do Edge.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="hubSeleniumGridUrl">Hub do selenium Grid</param>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver para o Navegador Edge</returns>
        private static RemoteWebDriver GetEdgeRemoteWebDriver(string plataform, string hubSeleniumGridUrl, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var edgeOptions = DriverOptionsFactory.GetEdgeOptions(plataform, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new RemoteWebDriver(new Uri(hubSeleniumGridUrl), edgeOptions.ToCapabilities(), TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Obtém o RemoteWebDriver do Firefox.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="hubSeleniumGridUrl">Hub do selenium Grid</param>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver para o Navegador Firefox</returns>
        private static RemoteWebDriver GetFirefoxRemoteWebDriver(string plataform, string hubSeleniumGridUrl, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var firefoxOprions = DriverOptionsFactory.GetFirefoxOptions(plataform, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new RemoteWebDriver(new Uri(hubSeleniumGridUrl), firefoxOprions.ToCapabilities(), TimeSpan.FromSeconds(timeout));
        }


        /// <summary>
        /// Obtém o RemoteWebDriver do Safari.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="hubSeleniumGridUrl">Hub do selenium Grid</param>
        /// <param name="headlessMode">Informa se é para executar os testes no modo headless ou não</param>
        /// <param name="directoryDefaultDownload">Informa o diretório padrão de downloads</param>
        /// <param name="acceptInsecureCertificates">Informa se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Informa se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <param name="timeout">Informa timeout default</param>
        /// <returns>Retorna o driver para o Navegador Safari</returns>
        private static RemoteWebDriver GetSafariRemoteWebDriver(string plataform, string hubSeleniumGridUrl, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage, int timeout)
        {
            var safariOprions = DriverOptionsFactory.GetSafariOptions(plataform, headlessMode, directoryDefaultDownload, acceptInsecureCertificates, recordVideo, timeZone, browserLanguage);

            return new RemoteWebDriver(new Uri(hubSeleniumGridUrl), safariOprions.ToCapabilities(), TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Fecha a janela do navegador que está com a instância do driver.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        public static void CloseDriver(IWebDriver driver)
        {
            driver.Close();
        }

        /// <summary>
        /// Fecha todas as janelas do navegador e faz logout com segurança.
        /// </summary>
        /// <param name="driver">Instância do driver corrente.</param>
        public static void QuitDriver(IWebDriver driver)
        {
            driver.Quit();
        }

        /// <summary>
        /// Chama Dispose para encerrar a sessão.
        /// </summary>
        /// <param name="driver">Instância do driver corrente.</param>
        public static void DisposeDriver(IWebDriver driver)
        {
            driver.Dispose();
        }
    }
}