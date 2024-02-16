using SpecflowSelenium.Support;
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
            ReadOnlyCollection<IWebElement> elements = null;

            for (int i = 0; i < timeout; i++)
            {
                switch (locator)
                {
                    case How.ClassName:
                        {
                            elements = driver.FindElements(By.ClassName(locatorValue));
                            break;
                        }
                    case How.CssSelector:
                        {
                            elements = driver.FindElements(By.CssSelector(locatorValue));
                            break;
                        }
                    case How.DataTestId:
                        {
                            elements = driver.FindElements(By.CssSelector($"[data-testid='{locatorValue}']"));
                            break;
                        }
                    case How.DataTest:
                        {
                            elements = driver.FindElements(By.CssSelector($"[data-test='{locatorValue}']"));
                            break;
                        }
                    case How.Id:
                        {
                            elements = driver.FindElements(By.Id(locatorValue));
                            break;
                        }
                    case How.LinkText:
                        {
                            elements = driver.FindElements(By.LinkText(locatorValue));
                            break;
                        }
                    case How.Name:
                        {
                            elements = driver.FindElements(By.Name(locatorValue));
                            break;
                        }
                    case How.PartialLinkText:
                        {
                            elements = driver.FindElements(By.PartialLinkText(locatorValue));
                            break;
                        }
                    case How.TagName:
                        {
                            elements = driver.FindElements(By.TagName(locatorValue));
                            break;
                        }
                    case How.XPath:
                        {
                            elements = driver.FindElements(By.XPath(locatorValue));
                            break;
                        }
                }

                if (elements.Count > 1)
                    throw new Exception($"Existe mais de um elemento com o seletor '{locator.GetDescription()} = {locator}' na página.");
                else if (elements.Count == 0)
                    WaitsCore.WaitFor(1000);
                else
                    break;
            }

            return elements[0];
        }
    }
}

