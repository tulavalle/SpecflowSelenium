namespace SpecflowSelenium.Support
{
    /// <summary>
    /// Classe responsável por retornar a descrição de um enumerador.
    /// </summary>
    public static class SetLists
    {
        /// <summary>
        /// Busca as tags do cenário.
        /// </summary>
        /// <param name="scenarioContext"></param>
        /// <returns>Retorna um array listando as tags encontradas</returns>
        public static string[] ReturnTagsList(ScenarioContext scenarioContext)
        {
            return scenarioContext.ScenarioInfo.Tags;
        }
    }
}
