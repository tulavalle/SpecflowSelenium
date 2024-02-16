using System.Drawing;

namespace SpecflowSelenium.Utils
{
    /// <summary>
    /// Classe destinada a ações relacionadas ao navegador.
    /// </summary>
    public static class Browser
    {
        /// <summary>
        /// Executa atualização da página.
        /// </summary>
        public static void Refresh(IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        /// <summary>
        /// Abre uma nova aba na janela do navegador.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        public static void OpenNewTab(IWebDriver driver)
        {
            driver.SwitchTo().NewWindow(WindowType.Tab);
        }

        /// <summary>
        /// Abre uma nova janela do navegador.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        public static void OpenNewWindow(IWebDriver driver)
        {
            driver.SwitchTo().NewWindow(WindowType.Window);
        }

        /// <summary>
        /// Seta o tamanho da janela do browser.
        /// </summary>
        /// <param name="width">Valor de largura da janela</param>
        /// <param name="height">Valor de altura da janela</param>
        public static void SetSizeWindow(IWebDriver driver, int width, int height)
        {
            driver.Manage().Window.Size = new Size(width, height);
        }

        /// <summary>
        /// Muda o foco do driver para a janela corrente.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        public static void ChangeToTabCurrent(IWebDriver driver)
        {
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
            string currentWindow = driver.CurrentWindowHandle;
            string newWindow = windowHandles.FirstOrDefault(handle => handle != currentWindow);

            if (!string.IsNullOrEmpty(newWindow)) driver.SwitchTo().Window(newWindow);
        }

        /// <summary>
        /// Navega para a URL desejada.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="url">URL desejada</param>
        public static void NavigateToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Obtém a URL da página atual.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <returns>Retorna a url da pagina em foco</returns>
        public static string GetCurrentURL(IWebDriver driver)
        {
            return driver.Url;
        }

        /// <summary>
        /// Obtém o número de abas ou janelas e muda para a aba ou janela desejada. A contagem inicia em 1.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="numberHandle">Número da aba ou janela para onde o foco deve ser direcionado.</param>
        public static void SwitchTo(IWebDriver driver, int numberHandle)
        {
            var handles = driver.WindowHandles;
            driver.SwitchTo().Window(handles[numberHandle - 1]);
        }

        /// <summary>
        /// Muda o foco do driver para o alerta realiza a ação desejada (confirmar ou cancelar) para o alerta.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="confirm">Confirma a mensagem de alerta</param>
        /// <param name="cancel">Cancela a mensagem de alerta</param>
        public static void ClickBrowserAlertButton(IWebDriver driver, bool confirm = true, bool cancel = false)
        {
            IAlert alert = driver.SwitchTo().Alert();

            if (confirm) alert.Accept();
            else if (cancel) alert.Dismiss();
        }

        /// <summary>
        /// Muda o foco do driver para o alerta e insere o o valor desejado no input do alert.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="value">Valor a ser inserido no alerta</param>
        /// <param name="confirm">Informa para clicar no botão que confirma o alerta</param>
        /// <param name="cancel">Informa para clicar no botão que cancela o alerta</param>
        public static void SetValueAlert(IWebDriver driver, string value, bool confirm = true, bool cancel = false)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.SendKeys(value);

            if (confirm) ClickBrowserAlertButton(driver, true);
            else if (cancel) ClickBrowserAlertButton(driver, false, true);
        }

        /// <summary>
        /// Muda o foco do driver para o alerta Busca a mensagem exibida no mesmo.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="confirm">Informa para clicar no botão que confirma o alerta</param>
        /// <param name="cancel">Informa para clicar no botão que cancela o alerta</param>
        public static string GetMessageAlert(IWebDriver driver, bool confirm = true, bool cancel = false)
        {
            var textAlert = driver.SwitchTo().Alert().Text;

            if (confirm) ClickBrowserAlertButton(driver);
            else if (cancel) ClickBrowserAlertButton(driver, false, true);

            return textAlert;
        }

        /// <summary>
        /// Move a instância do driver para o iframe. 
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="bySelectorIframe">CssSelector desejado. Se null busca pela Tagname "iframe"</param>
        public static void MoveToIFrame(IWebDriver driver, By bySelectorIframe = null)
        {
            var selector = bySelectorIframe == null
                ? By.TagName("iframe")
                : bySelectorIframe;

            var iframe = driver.FindElement(selector);
            driver.SwitchTo().Frame(iframe);
        }

        /// <summary>
        /// Move a instância do driver para o iframe. 
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="index">Index do iframe desejado</param>
        public static void MoveToIFrame(IWebDriver driver, int index)
        {
            driver.SwitchTo().Frame(index);
        }

        /// <summary>
        /// Move a instância do driver do o iframe para o driver default.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        public static void LeavingIFrame(IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }
    }
}