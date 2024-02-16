namespace SpecflowSelenium.Support
{
    /// <summary>    
    /// Classe responsável por fazer uma captura de tela no momento da execução do teste e salvar um arquivo .png na pasta padr�o com o nome do cen�rio atual.
    /// </summary>
    public class ScreenshotCore
    {
        public static ScenarioContext ScenarioContext { get; set; }

        protected ScreenshotCore() { }

        /// <summary>
        /// Realiza o screenshot e salva no diretório desejado.
        /// </summary>
        /// <param name="driver">Instância do driver corrente</param>
        /// <param name="scenarioContext">Contexto do cenário em execução</param>
        /// <param name="screenshotsFolder">Diretório para salvar o screenshot</param>
        /// <param name="screenshotS3Directory">Diretório para salvar o screenshot no S3 bucket (Amazon)</param>
        /// <param name="browser">Navegador da execução</param>
        /// <param name="isStep">Indica se o hook é relacionado a step do cenário</param>
        /// <param name="stepCounter">Valor do contador de steps do contexto de cenário</param>
        /// <param name="fileName">Nome do arquivo</param>
        /// <exception cref="Exception">Não foi possível retornar o filepath pois não foi informado o filename</exception>
        public static void MakeScreenshot(IWebDriver driver, ScenarioContext scenarioContext, string screenshotsFolder, string screenshotS3Directory, string browser, bool isStep, int stepCounter, bool isShouldUploadToS3, string filePath = null, string fileName = null)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            if (driver is not ITakesScreenshot)
                return;

            if (string.IsNullOrEmpty(filePath))
                filePath = ReturnFilePathFormatted(scenarioContext, screenshotsFolder, fileName, browser, isStep, stepCounter);
            else if (string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(fileName))
                throw new Exception("Não foi possível retornar o filepath pois não foi informado o filename.");

            screenshot.SaveAsFile(filePath);

            if (isShouldUploadToS3)
            {
                var filePathSplit = filePath.Split(screenshotsFolder);
                fileName = filePathSplit[1];

                var filePathS3 = $"{screenshotS3Directory}{fileName}";
#pragma warning disable CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
                new S3BucketAmazon().UploadFile(scenarioContext, filePath, filePathS3);
#pragma warning restore CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
            }
        }

        /// <summary>        
        /// Monta o caminho completo para salvar o arquivo.
        /// </summary>
        /// <param name="scenarioContext">Contexto do cenário corrente</param>
        /// <param name="screenshotsFolder">Diretório para salvar o arquivo png.</param>
        /// <param name="fileName">Nome do arquivo a selvo</param>
        /// <param name="browser">Navegador do screenshor</param>
        /// <param name="isStep">Indica se o hook é relacionado a step do cenário</param>
        /// <param name="stepCounter">Valor do contador de steps do contexto de cenário</param>
        /// <returns>Retorna o Caminho do arquivo formatado no formato .png</returns>
        public static string ReturnFilePathFormatted(ScenarioContext scenarioContext, string screenshotsFolder, string fileName, string browser, bool isStep, int stepCounter)
        {
            var scenarioExecutionStatus = scenarioContext.ScenarioExecutionStatus.ToString();

            var filePath = isStep
                ? $"{screenshotsFolder}\\Step{stepCounter}_{browser}_{fileName}_"
                : $"{screenshotsFolder}\\{scenarioExecutionStatus}_{browser}_{fileName}_";

            var limitCharacter = 100;

            if (filePath.Length > limitCharacter)
                filePath = filePath[..limitCharacter];

            return $"{filePath}{DateTime.Now:ddMMyyyy_HHmmssffff}.png".Replace("<", "_").Replace(">", "_").Replace("__", "_");
        }

        /// <summary>
        /// Busca a descrição do cenário no contexto.
        /// </summary>
        /// <param name="scenarioContext">Contexto do cenário corrente</param>
        /// <returns>Retorna a descrição do cenário corrente</returns>
        public static string ReturnScenarioName(ScenarioContext scenarioContext)
        {
            object scenarioOutline = null;

            if (scenarioContext.ScenarioInfo.Arguments.Count > 0)
                scenarioOutline = scenarioContext.ScenarioInfo.Arguments[0];

            var scenarioName = scenarioContext.ScenarioInfo.Title;

            scenarioName = scenarioOutline != null
                ? $"{scenarioName}_{scenarioOutline}"
                : scenarioName;

            if (scenarioName.Length > 110)
                scenarioName = scenarioName[..110];

            return scenarioName;
        }

        /// <summary>
        /// Busca a lista de tags e verifica se contém a tag do Test Case (TC_)
        /// </summary>
        /// <param name="scenarioContext"></param>
        /// <returns>Retorna se possui ou não a tag do Test Case (TC_) (true/false)</returns>
        public static bool ReturnIfContainsTagTestCase(ScenarioContext scenarioContext)
        {
            var tagList = SetLists.ReturnTagsList(scenarioContext);
            var containsTag = false;

            foreach (var tag in tagList)
            {
                if (tag.ToString().Contains("TC_"))
                {
                    containsTag = true;
                    break;
                }
            }

            return containsTag;
        }
    }
}