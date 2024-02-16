namespace SpecflowSelenium.Utils
{
    public static class ScenarioContextExtensions
    {
        public static void SetIfNotExists<TValue>(this ScenarioContext scenarioContext, string key, TValue value)
        {
            if (!scenarioContext.ContainsKey(key))
            {
                scenarioContext.Add(key, value);
            }
        }

        public static TValue GetIfExists<TValue>(this ScenarioContext scenarioContext, string key)
        {
            if (scenarioContext.ContainsKey(key) && scenarioContext.TryGetValue(key, out var value))
            {
                return (TValue)value;
            }

            return default;
        }
    }
}
