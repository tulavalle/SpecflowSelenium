namespace SpecflowSelenium.FindsByFactory
{
    /// <summary>
    /// Classe responsável pelos enumeradores que definem as opções do seletor css a serem utilizadas para a localização dos componentes da tela.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3376:Attribute, EventArgs, and Exception type names should end with the type being extended", Justification = "<Pendente>")]
    public class FindsElementsBy : Attribute
    {
        public How How { get; set; }

        public string Using { get; set; }
    }

    /// <summary>
    /// Lista de atributos css para localização de elementos na tela.
    /// </summary>
    public enum How
    {
        ClassName,
        CssSelector,
        DataTestId,
        DataTest,
        Id,
        LinkText,
        Name,
        PartialLinkText,
        TagName,
        XPath
    };
}

