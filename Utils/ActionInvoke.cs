using System.Diagnostics;

namespace SpecflowSelenium.Utils
{
    public static class ActionInvoke
    {
        public static void TryAndHandleException(Action action, string actionDescription)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Erro durante {actionDescription}: {ex.Message}");
                throw new Exception($"Não foi possível realizar corretamente a {actionDescription} pois: {ex}.");
            }
        }
    }
}
