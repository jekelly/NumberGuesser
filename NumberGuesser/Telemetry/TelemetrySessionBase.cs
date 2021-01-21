using Microsoft.VisualStudio.Telemetry;
using System;

namespace VSTelWorkbench
{

    class TelemetrySessionBase : IDisposable
    {
        public TelemetrySessionBase(string aiKey, string asimovKey)
        {
            _ = TelemetryService.CreateAndGetDefaultSession(appInsightsIKey: aiKey, asimovIKey: asimovKey);
            TelemetryService.DefaultSession.IsOptedIn = true;
            TelemetryService.DefaultSession.Start();
        }

        public void Dispose()
        {
            TelemetryService.DefaultSession.Dispose();
        }
    }
}
