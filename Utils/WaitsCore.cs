using SeleniumExtras.WaitHelpers;

namespace SpecflowSelenium.Utils
{
    /// <summary>
    /// Classe responsável pelas esperas de acordo com a condição em JavaScript e Esperas por condições no Selenium.
    /// </summary>
    public static class WaitsCore
    {
        /// <summary>
        /// Realiza uma espera explícita, ou seja, com tempo fixo. 
        /// </summary>
        /// <param name="milliseconds">Valor em milisegundos para espera</param>
        public static void WaitFor(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        /// <summary>
        /// Efetua uma espera implícita (até que a condição esperada pelo elemento seja atendida, caso contrário, a espera persistirá até atingir o timeout informado).
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="cssSelector">Valor do cssSelector do element desejado.</param>
        /// <param name="condition">Condição a ser atendida</param>
        /// <param name="timeout">Tempo máximo em segundos para aguardar a condição desejada</param>
        public static void WaitForElementCondition(IWebDriver driver, string cssSelector, WaitsCondition condition, int timeout = 10)
        {
            int counter = 0;

            bool Condition()
            {
                return condition switch
                {
                    WaitsCondition.Enable => !Convert.ToBoolean(JavaScriptCore.ExecuteCommand(driver, $"return document.querySelector(\"{cssSelector}\").disabled")),
                    WaitsCondition.Disable => Convert.ToBoolean(JavaScriptCore.ExecuteCommand(driver, $"return document.querySelector(\"{cssSelector}\").disabled")),
                    WaitsCondition.Visible => !Convert.ToBoolean(JavaScriptCore.ExecuteCommand(driver, $"return document.querySelector(\"{cssSelector}\").hidden")),
                    WaitsCondition.NotVisible => Convert.ToBoolean(JavaScriptCore.ExecuteCommand(driver, $"return document.querySelector(\"{cssSelector}\").hidden")),
                    WaitsCondition.Exist => driver.FindElements(By.CssSelector(cssSelector)).Count >= 1,
                    WaitsCondition.NotExist => driver.FindElements(By.CssSelector(cssSelector)).Count == 0,
                    _ => false,
                };
            }

            while (!Condition() && counter < timeout)
            {
                WaitFor(1000);
                counter++;
            }

            WaitFor(500);
        }

        /// <summary>
        /// Pesquise o número de guias e muda o foco para a guia especificada.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="amountOfAbasExpected">Quantidade esperada de abas</param>
        /// <param name="timeout">Tempo máximo de espera em segundos para até que a condição seja atendida</param>
        public static void WaitForAmountOfOpenTabs(IWebDriver driver, int amountOfTabsExpected, int timeout = 10)
        {
            int counter = 0;

            while (driver.WindowHandles.Count != amountOfTabsExpected && counter < timeout)
            {
                WaitFor(1000);
                counter++;
            }
        }

        /// <summary>
        /// Aguarda que a página seja carregada por "document.readyState" (JavaScript).
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        public static void WaitPageLoad(IWebDriver driver)
        {
            var timeout = new TimeSpan(0, 0, 30);
            var wait = new WebDriverWait(driver, timeout);

            if (driver is not IJavaScriptExecutor javascript)
            {
                throw new ArgumentException("O driver deve suportar a execução do javascript. ", nameof(driver));
            }

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                    "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    return e.Message.ToLower().Contains("Não foi possível abrir a janela.");
                }
                catch (WebDriverException e)
                {
                    return e.Message.ToLower().Contains("Não foi possível abrir o navegador.");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Aguarda o Alerta estar presente.
        /// </summary>
        /// <param name="driver">Iinstância do driver corrente</param>
        /// <param name="timeout">Tempo máximo de espera até que a condição seja atendida</param>
        public static void WaitAlert(IWebDriver driver, int timeout = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(ExpectedConditions.AlertIsPresent());
        }

        /// <summary>
        /// Aguardar pelo tempo máximo informado.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="wait">Medida de tempo a ser utilizada na espera</param>
        /// <param name="timeout">Tempo máximo em segundos para espera</param>
        public static void WaitForTime(IWebDriver driver, Waits wait, int timeout = 10)
        {
            var timeSpan = wait switch
            {
                Waits.FromMinutes => TimeSpan.FromMinutes(timeout),
                Waits.FromSeconds => TimeSpan.FromSeconds(timeout),
                Waits.FromMilliseconds => TimeSpan.FromMilliseconds(timeout),
                _ => throw new ArgumentException("Tipo de espera inválida: ", nameof(wait)),
            };

            driver.Manage().Timeouts().ImplicitWait = timeSpan;
        }


        /// <summary>
        /// Aguarda a condição Selenium ser atendida, pelo tempo máximo definido.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="locator">Localizador do elemento desejado</param>
        /// <param name="seleniumCondition">Condição a ser aguardada para o elemento</param>
        /// <param name="timeout">Tempo máximo de segundos para aguardar</param>
        public static void WaitCondition(IWebDriver driver, By locator, SeleniumConditions seleniumCondition, int timeout = 10, IWebElement elementWait = null)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            switch (seleniumCondition)
            {
                case SeleniumConditions.ElementExists:
                    wait.Until(ExpectedConditions.ElementExists(locator));
                    break;
                case SeleniumConditions.ElementIsVisible:
                    wait.Until(ExpectedConditions.ElementIsVisible(locator));
                    break;
                case SeleniumConditions.BeClickableElement:
                    wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                    break;
                case SeleniumConditions.ElementToBeClickable:
                    wait.Until(ExpectedConditions.ElementToBeClickable(elementWait));
                    break;
                case SeleniumConditions.InvisibilityOfElementLocated:
                    var element = driver.FindElements(locator);
                    wait.Until(driver => element.Count == 0);
                    break;
                default:
                    throw new ArgumentException("Condição de espera Selenium inválida: ", nameof(seleniumCondition));
            }
        }

        /// <summary>
        /// Persiste a espera pelo elemento até o tempo definido.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param> 
        /// <param name="localizador">Localizador do elemento desejado</param>
        /// <param name="wait">Unidade de medida de tempo a ser utilizada</param>
        /// <param name="timeout">Tempo máximo de segundos para aguardar</param>
        public static void WaitCondition(IWebDriver driver, By locator, Waits wait, int timeout = 10)
        {
            var timeSpan = wait switch
            {
                Waits.FromMinutes => TimeSpan.FromMinutes(timeout),
                Waits.FromSeconds => TimeSpan.FromSeconds(timeout),
                Waits.FromMilliseconds => TimeSpan.FromMilliseconds(timeout),
                _ => throw new ArgumentException("Tipo de espera inválida: ", nameof(wait)),
            };
            var webDriverWait = new WebDriverWait(driver, timeSpan);
            webDriverWait.Until(e => e.FindElement(locator));
        }


        /// <summary>
        /// Persiste conforme a condição Selenium paro elemento do localizador com o texto informado.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="locator">Localizador do elemento desejado</param>
        /// <param name="seleniumCondition">Condição a ser aguardada para o elemento</param>
        /// <param name="textExpected">Texto esperado</param>
        public static void WaitCondition(IWebDriver driver, By locator, SeleniumConditions seleniumCondition, string textExpected)
        {
            var wait = new WebDriverWait(driver, TimeSpan.Zero);

            switch (seleniumCondition)
            {
                case SeleniumConditions.InvisibilityOfElementWithText:
                    wait.Until(ExpectedConditions.InvisibilityOfElementWithText(locator, textExpected));
                    break;
                case SeleniumConditions.TextToBePresentInElementLocated:
                    var element = driver.FindElement(locator);
                    wait.Until(ExpectedConditions.TextToBePresentInElement(element, textExpected));
                    break;
                default:
                    throw new ArgumentException("Condição do Selenium inválida: ", nameof(seleniumCondition));
            }
        }


        /// <summary>
        /// Lista de condições de espera JS.
        /// </summary>
        public enum WaitsCondition
        {
            Disable,
            Enable,
            Visible,
            NotVisible,
            Exist,
            NotExist
        }

        /// <summary>
        /// Lista de unidade de tempo para aguardar.
        /// </summary>
        public enum Waits
        {
            FromMinutes,
            FromSeconds,
            FromMilliseconds
        }

        /// <summary>
        /// Lista de condições de espera explícita.
        /// </summary>
        public enum SeleniumConditions
        {
            ElementExists,
            ElementIsVisible,
            BeClickableElement,
            ElementToBeClickable,
            InvisibilityOfElementLocated,
            InvisibilityOfElementWithText,
            TextToBePresentInElementLocated
        }
    }
}