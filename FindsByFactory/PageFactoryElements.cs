using System.Reflection;

namespace SpecflowSelenium.FindsByFactory
{
    /// <summary>
    /// Class responsible for initializing screen components.
    /// </summary>
    public class PageFactoryElements
    {
        protected PageFactoryElements() { }

        /// <summary>
        /// Carregar propriedades de acordo com os campos do navegador.
        /// </summary>
        /// <param name="instancia">Instância do objeto</param>
        /// <param name="driver">Instância do Driver corrente</param>
        public static void MapPageElements(object instancia, IWebDriver driver)
        {
            WaitsCore.WaitPageLoad(driver);

            var property = instancia.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(FindsElementsBy)));

            foreach (var prop in property)
            {
                var atribute = GetAttribute(prop);
                var tipoDeLocalizador = atribute.How;
                var valorDoLocalizador = atribute.Using;

                // retrieves the property instance
                var instanceProperty = prop.GetValue(instancia);

                if (instanceProperty == null)
                {
                    instanceProperty = Activator.CreateInstance(prop.PropertyType, driver);
                    prop.SetValue(instancia, instanceProperty);
                }

                InitializeElements(instanceProperty, driver, tipoDeLocalizador, valorDoLocalizador);
            }
        }

        private static FindsElementsBy GetAttribute(PropertyInfo prop)
        {
            FindsElementsBy[] Att = (FindsElementsBy[])prop.GetCustomAttributes(typeof(FindsElementsBy), false);

            if (Att.Any())
            {
                return Att.FirstOrDefault();
            }

            return null;
        }

        private static void InitializeElements(object instance, IWebDriver driver, How tipoDeLocalizador, string valorDoLocalizador)
        {
            var method = instance.GetType().GetMethod("InitializeElementPageFactory");

            method.Invoke(instance, new object[] { driver, tipoDeLocalizador, valorDoLocalizador });
        }

        /// <summary>
        /// Waits for the element to exist before returning it.
        /// </summary>
        /// <param name="driver">Current driver instance.</param>
        /// <param name="locator">Selector type for component identification.</param>
        /// <param name="locatorValue">Selector value for component identification.</param>
        /// <param name="timeout">Waiting time (in seconds) for component location on screen.</param>
        /// <returns>Returns the desired element.</returns>
        public static IWebElement WaitAndReturnElement(IWebDriver driver, How locator, string locatorValue, int timeout)
        {
            IWebElement element = null;

            for (int i = 0; i < timeout; i++)
            {
                switch (locator)
                {
                    case How.ClassName:
                        {
                            element = driver.FindElement(By.ClassName(locatorValue));
                            break;
                        }
                    case How.CssSelector:
                        {
                            element = driver.FindElement(By.CssSelector(locatorValue));
                            break;
                        }
                    case How.DataTestId:
                        {
                            element = driver.FindElement(By.CssSelector($"[data-testid='{locatorValue}']"));
                            break;
                        }
                    case How.DataTest:
                        {
                            element = driver.FindElement(By.CssSelector($"[data-test='{locatorValue}']"));
                            break;
                        }
                    case How.Id:
                        {
                            element = driver.FindElement(By.Id(locatorValue));
                            break;
                        }
                    case How.LinkText:
                        {
                            element = driver.FindElement(By.LinkText(locatorValue));
                            break;
                        }
                    case How.Name:
                        {
                            element = driver.FindElement(By.Name(locatorValue));
                            break;
                        }
                    case How.PartialLinkText:
                        {
                            element = driver.FindElement(By.PartialLinkText(locatorValue));
                            break;
                        }
                    case How.TagName:
                        {
                            element = driver.FindElement(By.TagName(locatorValue));
                            break;
                        }
                    case How.XPath:
                        {
                            element = driver.FindElement(By.XPath(locatorValue));
                            break;
                        }
                }

                if (element == null)
                {
                    WaitsCore.WaitFor(1000);
                }
                else
                {
                    break;
                }
            }

            return element;
        }
    }
}

