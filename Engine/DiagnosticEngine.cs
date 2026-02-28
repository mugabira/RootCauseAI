using RootCauseAI.Models;
using RootCauseAI.Engine.ML;

namespace RootCauseAI.Engine
{
    public class DiagnosticEngine
    {
        private readonly LayerPredictionModel _mlModel;

        public DiagnosticEngine()
        {
            _mlModel = new LayerPredictionModel();
            _mlModel.Train("training-data.csv");
        }

        public async Task<Ticket> AnalyzeAsync(Ticket ticket)
        {
            var (layer, confidence) =
                _mlModel.Predict(ticket.Logs ?? ticket.CustomerDescription);

            ticket.PredictedLayer = layer;
            ticket.Confidence = confidence;

            ticket.SuggestedAction = layer switch
            {
                PipelineLayer.Storage => "Check DB partitions and write failures.",
                PipelineLayer.Processing => "Inspect ETL job history.",
                PipelineLayer.Transport => "Check queues and brokers.",
                PipelineLayer.Visualization => "Validate dashboard queries.",
                _ => "Run general diagnostics."
            };

            await Task.CompletedTask;
            return ticket;
        }
    }
}