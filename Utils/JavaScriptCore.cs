namespace SpecflowSelenium.Utils
{
    /// <summary>
    /// Classe respons�vel pela l�gica dos testes para poder usar comandos JS.
    /// </summary>
    public static class JavaScriptCore
    {
        /// <summary>
        /// Executa Comando JS.
        /// </summary>
        /// <param name="driver">Driver instanciado do Selenium.</param>
        /// <param name="command">Comando JS a ser executado.</param>
        /// <returns>Execu��o do comando JS</returns>
        public static object ExecuteCommand(IWebDriver driver, string command)
        {
            var js = (IJavaScriptExecutor)driver;

            try
            {
                return js.ExecuteScript(command);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao executar o seguinte comando:\n" + command, ex);
            }
        }

        /// <summary>
        /// Executa um comando JS com seus argumentos.
        /// </summary>
        /// <param name="driver">Driver instanciado do Selenium.</param>
        /// <param name="command">Comando JS a ser executado.</param>
        /// <param name="args">Array de argumentos passados para execu��o do comando JS.</param>
        /// <returns>Execuçãoo do comando JS</returns>
        public static object ExecuteCommand(IWebDriver driver, string command, params object[] args)
        {
            var js = (IJavaScriptExecutor)driver;

            try
            {
                return js.ExecuteScript(command, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao executar o seguinte comando:\n" + command, ex);
            }
        }

        /// <summary>
        /// Realiza scroll até o element desejado estar centralizado.
        /// </summary>        
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="element">Elemento até onde o scroll deve ir</param>
        /// <param name="timeout">Tempo de espara par carregamento de itens após scroll. por padrão 150 ms</param>
        public static void MoveScrollToElementIsCentered(IWebDriver driver, IWebElement element, int timeout = 150)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });", element);
            WaitsCore.WaitFor(timeout);
        }

        /// <summary>
        /// Realiza scroll até o element desejado.
        /// </summary>        
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="element">Elemento até onde o scroll deve ir</param>
        /// <param name="timeout">Tempo de espara par carregamento de itens após scroll. por padrão 150 ms</param>
        public static void MoveScrollOnElementIsVisible(IWebDriver driver, IWebElement element, int timeout = 150)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({ behavior: 'smooth', block: 'nearest' });", element);
            WaitsCore.WaitFor(timeout);
        }


        /// <summary>
        /// Realiza scroll dentro do container até o element desejado. A açao é no scroll mais próximo do elemento em questão.
        /// </summary>        
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="elementToScroll">Elemento até onde o scroll deve ir</param>
        /// <param name="timeout">Tempo de espara par carregamento de itens após scroll. por padrão 150 ms</param>
        public static void MoveScrollOfContainerToElement(IWebDriver driver, IWebElement containerElement, IWebElement elementToScroll, int timeout = 150)
        {
            if (HasScrollInContainer(driver, containerElement))
            {
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'start', inline: 'nearest' });", elementToScroll);
                WaitsCore.WaitFor(timeout);
            }
        }


        /// <summary>
        /// Realiza scroll até o o final da página.
        /// </summary>        
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="timeout">Tempo de espara par carregamento de itens após scroll. por padrão 150 ms</param>
        public static void MoveScrollToEndPage(IWebDriver driver, int timeout = 150)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            WaitsCore.WaitFor(timeout);
        }

        /// <summary>
        /// Verifica via JS se o elemento está ou não visível na tela.
        /// </summary>        
        /// <param name="driver">Instância do driver corrente</param>
        public static bool ElementIsVisible(IWebDriver driver, string cssSelector)
        {
            var js = (IJavaScriptExecutor)driver;
            var command = $"const elemento = document.querySelector('{cssSelector}'); return elemento !== null && elemento.offsetParent !== null;";

            return (bool)js.ExecuteScript(command);
        }

        /// <summary>
        /// Atribui valor ao atributo informado para o elemento.
        /// </summary>        
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="element">Elemento a ser informado o atributo</param>
        /// <param name="attribute">Descrição do atributo desejado</param>
        /// <param name="attributeValue">Valor a ser atribuído no atributo desejado</param>
        public static void SetValueInAttribute(IWebDriver driver, IWebElement element, string attribute, string attributeValue)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"arguments[0].setAttribute('{attribute}', '{attributeValue}');", element);
        }

        /// <summary>
        /// Busca o href do elemento e abre o link em nova aba.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="element">Elemento a ser informado o atributo</param>
        public static void OpenLinkInNewTab(IWebDriver driver, IWebElement element)
        {
            // Verifica se o tipo do objeto possui um método chamado "scrollIntoView"
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.open(arguments[0].getAttribute('href'), '_blank');", element);
        }

        /// <summary>
        /// Busca o href do elemento e abre o link em nova janela.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="element">Elemento a ser informado o atributo</param>
        /// <param name="height">Valor da altura da janela, por padrão: 600</param>
        /// <param name="width">Valor da largura da janela, por padrão: 800</param>
        /// <returns></returns>
        public static void OpenLinkInNewWindow(IWebDriver driver, IWebElement element, int height = 600, int width = 800)
        {
            // Verifica se o tipo do objeto possui um método chamado "scrollIntoView"
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"window.open(arguments[0].getAttribute('href'), 'nome_unico_da_janela', 'height={height},width={width}');", element);
        }

        private static bool HasScrollInContainer(IWebDriver driver, IWebElement container)
        {
            var js = (IJavaScriptExecutor)driver;
            object result = js.ExecuteScript("return arguments[0].scrollHeight > arguments[0].clientHeight;", container);

            return result is bool v && v;
        }
    }
}
