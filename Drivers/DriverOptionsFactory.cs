using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace SpecflowSelenium.Drivers
{
    public class DriverOptionsFactory
    {
        protected DriverOptionsFactory() { }

        /// <summary>
        /// Search the desired options for the Chrome browser.. 
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="headlessMode">Indicates whether or not the test will run in the browser's Headless mode.</param>
        /// <param name="directoryDefaultDownload">Informs the default directory for download files.</param>
        /// <param name="acceptInsecureCertificates">Indica se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Indica se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <returns>Returns the desired options for the Chrome browser.</returns>
        public static ChromeOptions GetChromeOptions(string plataform, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage)
        {
            var chromeOptions = new ChromeOptions();

            if (!string.IsNullOrEmpty(plataform))
                chromeOptions.PlatformName = plataform;

            if (!string.IsNullOrEmpty(directoryDefaultDownload))
                chromeOptions.AddArguments("download.default_directory", directoryDefaultDownload);

            if (headlessMode)
                chromeOptions.AddArguments("--headless");

            chromeOptions.AcceptInsecureCertificates = acceptInsecureCertificates;
            chromeOptions.AddArguments("--disable-dev-shm-usage", "--no-sandbox", "--disable-popup-blocking", "--disable-infobars", $"--lang={browserLanguage}");
            chromeOptions.AddAdditionalOption("se:recordVideo", recordVideo);
            chromeOptions.AddAdditionalOption("se:timeZone", timeZone);
            chromeOptions.AddUserProfilePreference("intl.accept_languages", browserLanguage);

            return chromeOptions;
        }

        /// <summary>
        /// Search the desired options for the Edge browser.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="headlessMode">Indicates whether or not the test will run in the browser's Headless mode.</param>
        /// <param name="directoryDefaultDownload">Informs the default directory for download files.</param>
        /// <param name="acceptInsecureCertificates">Indica se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Indica se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <returns>Returns the desired options for the Edge browser.</returns>
        public static EdgeOptions GetEdgeOptions(string plataform, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage)
        {
            var edgeOptions = new EdgeOptions();

            if (!string.IsNullOrEmpty(plataform))
                edgeOptions.PlatformName = plataform;

            if (!string.IsNullOrEmpty(directoryDefaultDownload))
                edgeOptions.AddArguments("download.default_directory", directoryDefaultDownload);

            if (headlessMode)
            {
                edgeOptions.AddArguments("headless");
                edgeOptions.AddArguments("disable-gpu");
            }

            edgeOptions.AcceptInsecureCertificates = acceptInsecureCertificates;
            edgeOptions.AddArguments("--disable-dev-shm-usage", "--no-sandbox", "--requireWindowFocus", $"--lang={browserLanguage}");
            edgeOptions.AddAdditionalOption("se:recordVideo", recordVideo);
            edgeOptions.AddAdditionalOption("se:timeZone", timeZone);
            edgeOptions.AddUserProfilePreference("intl.accept_languages", browserLanguage);

            return edgeOptions;
        }

        /// <summary>
        /// Search the desired options for the Firefox browser.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="headlessMode">Indicates whether or not the test will run in the browser's Headless mode.</param>
        /// <param name="directoryDefaultDownload">Informs the default directory for download files.</param>
        /// <param name="acceptInsecureCertificates">Indica se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Indica se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <returns>Returns the desired options for the Firefox browser.</returns>
        public static FirefoxOptions GetFirefoxOptions(string plataform, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage)
        {
            var firefoxOptions = new FirefoxOptions();

            if (!string.IsNullOrEmpty(plataform))
                firefoxOptions.PlatformName = plataform;

            if (!string.IsNullOrEmpty(directoryDefaultDownload))
                firefoxOptions.AddAdditionalOption("download.default_directory", directoryDefaultDownload);


            if (headlessMode)
                firefoxOptions.AddArguments("--headless");

            firefoxOptions.AcceptInsecureCertificates = acceptInsecureCertificates;
            firefoxOptions.AddAdditionalOption("se:recordVideo", recordVideo);
            firefoxOptions.AddAdditionalOption("se:timeZone", timeZone);
            firefoxOptions.AddArguments($"--lang={browserLanguage}");
            firefoxOptions.AddArguments($"--no-sandbox");
            firefoxOptions.SetPreference("intl.accept_languages", browserLanguage);

            return firefoxOptions;
        }


        /// <summary>
        /// Search the desired options for the Safari browser.
        /// </summary>
        /// <param name="plataform">Informa em quel plataforma é para executar o Selenium Grid</param>
        /// <param name="headlessMode">Indicates whether or not the test will run in the browser's Headless mode.</param>
        /// <param name="directoryDefaultDownload">Informs the default directory for download files.</param>
        /// <param name="acceptInsecureCertificates">Indica se é para aceitar certificado de segurança</param>
        /// <param name="recordVideo">Indica se deve ser gravado vídeo da execução</param>
        /// <param name="timeZone">Informa o timezone desejado para o navegador</param>
        /// <param name="browserLanguage">Informa o idioma desejado para o navegador</param>
        /// <returns>Returns the desired options for the Safari browser.</returns>
        public static SafariOptions GetSafariOptions(string plataform, bool headlessMode, string directoryDefaultDownload, bool acceptInsecureCertificates, bool recordVideo, string timeZone, string browserLanguage)
        {
            var safariOptions = new SafariOptions();

            if (!string.IsNullOrEmpty(plataform))
                safariOptions.PlatformName = plataform;

            if (!string.IsNullOrEmpty(directoryDefaultDownload))
                safariOptions.AddAdditionalOption("download.default_directory", directoryDefaultDownload);

            safariOptions.AcceptInsecureCertificates = acceptInsecureCertificates;
            safariOptions.AddAdditionalOption("--headless", headlessMode);
            safariOptions.AddAdditionalOption("se:recordVideo", recordVideo);
            safariOptions.AddAdditionalOption("--disable-dev-shm-usage", true);
            safariOptions.AddAdditionalOption("se:timeZone", timeZone);
            safariOptions.AddAdditionalOption($"--lang", browserLanguage);
            safariOptions.AddAdditionalOption("--no-sandbox", true);
            safariOptions.AddAdditionalOption("--safari.cleanSession", true);
            safariOptions.AddAdditionalOption("se:timeZone", "America/Sao_Paulo");
            safariOptions.AddAdditionalOption("intl.accept_languages", browserLanguage);

            return safariOptions;
        }
    }
}