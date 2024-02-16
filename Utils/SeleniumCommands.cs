using OpenQA.Selenium.Interactions;

namespace SpecflowSelenium.Utils
{
    /// <summary>
    /// Classe para realizar ações do Selenium.
    /// </summary>
    public static class SeleniumCommands
    {
        /// <summary>
        /// Performs Selenium actions.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="action">Ação a ser executada</param>
        /// <param name="element">Indica o element para ações envolvendo o movimento do mouse.</param>
        public static void PerformAction(IWebDriver driver, Action action, IWebElement element = null)
        {
            var actions = new Actions(driver);

            switch (action)
            {
                case Action.CtrlA:
                    actions.KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control).Build().Perform();
                    break;
                case Action.DoubleClick:
                    actions.DoubleClick(element).Build().Perform();
                    break;
                case Action.MoveMouseToElement:
                    actions.MoveToElement(element).Build().Perform();
                    break;
                case Action.MoveTheMouseToTheElementANDClickLikeTheLeftMouseButton:
                    actions.MoveToElement(element).Click().Build().Perform();
                    break;
                case Action.ScrollToElement:
                    actions.ScrollToElement(element).Perform();
                    break;
                case Action.ClickRightButtonInElement:
                    actions.ContextClick(element).Build().Perform();
                    break;
                default:
                    PerformSingleKeyAction(actions, action);
                    break;
            }
        }

        /// <summary>
        /// Lista de ações do Selenium
        /// </summary>
        public enum Action
        {
            Backspace,
            Ctrl,
            CtrlA,
            DoubleClick,
            ElementToBeClickable,
            Enter,
            Esc,
            F1,
            F5,
            MoveMouseToElement,
            MoveTheMouseToTheElementANDClickLikeTheLeftMouseButton,
            Tab, 
            ScrollToElement,
            ClickRightButtonInElement
        }

        private static void PerformSingleKeyAction(Actions actions, Action action)
        {
            var key = GetKeyForAction(action);
            actions.SendKeys(key).Build().Perform();
        }

        private static string GetKeyForAction(Action action)
        {
            return action switch
            {
                Action.Backspace => Keys.Backspace,
                Action.Ctrl => Keys.Control,
                Action.Enter => Keys.Enter,
                Action.Esc => Keys.Escape,
                Action.F1 => Keys.F1,
                Action.F5 => Keys.F5,
                Action.Tab => Keys.Tab,
                _ => throw new Exception($"A ação '{action}' não foi implementada. Verifique com o administrador do pacote.")
            };
        }
    }
}