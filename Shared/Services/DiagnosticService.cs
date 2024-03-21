using System.Diagnostics;

namespace FridgeLight.Shared.Services
{
    public class DiagnosticService
    {
        public bool IsInDebugMode()
        {
            return Debugger.IsAttached;
        }
    }
}
