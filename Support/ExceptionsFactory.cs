namespace SpecflowSelenium.Support
{
    /// <summary>
    /// O teste falha e retorna uma exceção informando que o erro ocorreu.
    /// </summary>
    public static class ExceptionsFactory
    {
        /// <summary>
        /// Falha no teste e relata que o teste está desatualizado.
        /// </summary>
        /// <exception cref="Exception">Exceção informando que o teste está desatualizado.</exception>
        public static void Depreciated()
        {
            throw new Exception("O teste está desatualizado. Verifique a documentação e atualize a implementação");
        }

        /// <summary>
        /// O teste falha informando que não foi implementado.
        /// </summary>
        /// <exception cref="Exception">Exceção informando que o teste não foi implementado.</exception>
        public static void Pending()
        {
            throw new Exception("O teste não foi implementado. Implemente o teste ou coloque a tag '@Ignore' se precisar ser ignorado pela execução.");
        }

        /// <summary>
        /// Verifica se existe a tag defeito ou bug e falha o teste.~´Util para testes manuais e para n"ao execu~c"ao de testes que á possuem defeito em aberto.
        /// </summary>
        /// <param name="scenarioContext">Contexto do cenário</param>
        /// <exception cref="Exception">Exceção informando que o cenário está em desenvolvimento ou que possui defeito(s) relatado(s) e o(s) link(s) do(s) defeito(s).</exception>
        public static void FailReportedDefectOrInDevelopmentScenario(ScenarioContext scenarioContext)
        {
            var currentArguments = scenarioContext.ScenarioInfo.Arguments;
            var scenarioOutline = currentArguments[0];
            var scenarioTags = SetLists.ReturnTagsList(scenarioContext);
            var scenario = string.Empty;

            foreach (var tag in scenarioTags)
            {
                var actualTag = tag;

                if (actualTag.Contains("BUG", StringComparison.CurrentCultureIgnoreCase))
                {
                    scenario = actualTag.Split("(")[1].Split(",")[0].Replace("_", " ");
                    string[] linksDoDefeito = actualTag.Replace(")", "").Split(",")[1].Split(';');
                    var defectLinksFormatted = ReturnScenarioDefectLinks(linksDoDefeito);

                    FailTestDefective(scenarioContext, scenario, scenarioOutline, defectLinksFormatted);
                }
                else if (actualTag.Contains("DEVELOPMENT", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!actualTag.Equals("DEVELOPMENT", StringComparison.CurrentCultureIgnoreCase))
                        scenario = actualTag.Split("(")[1].Replace("_", " ").Replace(")", "");

                    FailTestDevelopment(scenarioContext, scenario, actualTag, scenarioOutline);
                }
            }
        }

        private static string ReturnScenarioDefectLinks(string[] linksExpected)
        {
            var formattedLinks = string.Empty;

            for (int i = 0; i < linksExpected.Length; i++)
            {
                formattedLinks = string.IsNullOrEmpty(formattedLinks)
                    ? linksExpected[i]
                    : $"{formattedLinks}; \r\n {linksExpected[i]}";
            }

            return formattedLinks;
        }

        private static void FailTestDefective(ScenarioContext scenarioContext, string scenario, object scenarioOutline, string defectLinksFormatted)
        {
            if (scenarioContext.ScenarioInfo.Title.Contains(scenario))
                throw new Exception($@"O cenário de teste - {scenarioContext.ScenarioInfo.Title} - falhou. Veja o(s) defeito(s) reportados: {defectLinksFormatted}");
            else if (scenarioOutline != null && scenarioOutline.ToString().Contains(scenario))
                throw new Exception($@"O cenário de teste - {scenarioOutline} - falhou. Veja o(s) defeito(s) reportados: {defectLinksFormatted}");
        }

        private static void FailTestDevelopment(ScenarioContext scenarioContext, string scenario, string actualTag, object scenarioOutline)
        {
            if (scenarioContext.ScenarioInfo.Title.Contains(scenario) || actualTag.Equals("DEVELOPMENT", StringComparison.CurrentCultureIgnoreCase))
                throw new Exception($@"O cenário de teste - {scenarioContext.ScenarioInfo.Title} - falhou pois a funcionalidade não foi implementada ou a tag '@development' não foi removida, por favor, verifique.");

            else if (scenarioOutline != null && scenarioOutline.ToString().Contains(scenario) || actualTag.Equals("DEVELOPMENT", StringComparison.CurrentCultureIgnoreCase))
                throw new Exception($@"O cenário de teste - {scenarioOutline} - falhou pois a funcionalidade não foi implementada ou a tag '@development' não foi removida, por favor, verifique.");
        }
    }
}
