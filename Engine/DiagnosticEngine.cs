using RootCauseAI.Models;

namespace RootCauseAI.Engine
{
    public class DiagnosticEngine
    {
        public Ticket Analyze(Ticket ticket)
        {
            // Simple rule-based classification
            if (ticket.CustomerDescription.Contains("missing", StringComparison.OrdinalIgnoreCase))
            {
                ticket.PredictedLayer = PipelineLayer.Storage;
                ticket.SuggestedAction = "Check database for missing partitions or failed writes.";
            }
            else if (ticket.CustomerDescription.Contains("slow", StringComparison.OrdinalIgnoreCase) ||
                     ticket.CustomerDescription.Contains("lag", StringComparison.OrdinalIgnoreCase))
            {
                ticket.PredictedLayer = PipelineLayer.Processing;
                ticket.SuggestedAction = "Check ETL jobs and batch processing queues.";
            }
            else if (ticket.CustomerDescription.Contains("dashboard", StringComparison.OrdinalIgnoreCase) ||
                     ticket.CustomerDescription.Contains("visual", StringComparison.OrdinalIgnoreCase))
            {
                ticket.PredictedLayer = PipelineLayer.Visualization;
                ticket.SuggestedAction = "Check dashboard configuration and query outputs.";
            }
            else
            {
                ticket.PredictedLayer = PipelineLayer.Query;
                ticket.SuggestedAction = "Run query diagnostics and check stored procedure results.";
            }

            return ticket;
        }
    }
}
