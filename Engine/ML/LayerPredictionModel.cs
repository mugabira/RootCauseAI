using Microsoft.ML;
using RootCauseAI.Models;

namespace RootCauseAI.Engine.ML
{
    public class LayerPredictionModel
    {
        private readonly MLContext _ml = new();
        private ITransformer? _model;
        private PredictionEngine<LogTrainingData, LogPrediction>? _engine;

        public void Train(string dataPath)
        {
            var data = _ml.Data.LoadFromTextFile<LogTrainingData>(
                dataPath,
                separatorChar: ',',
                hasHeader: true);

            var pipeline =
                _ml.Transforms.Text.FeaturizeText("Features", nameof(LogTrainingData.LogText))
                .Append(_ml.Transforms.Conversion.MapValueToKey("Label"))
                .Append(_ml.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(_ml.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _model = pipeline.Fit(data);
            _engine = _ml.Model.CreatePredictionEngine<LogTrainingData, LogPrediction>(_model);
        }

        public (PipelineLayer layer, float confidence) Predict(string logs)
        {
            if (_engine == null)
                throw new Exception("Model not trained.");

            var prediction = _engine.Predict(new LogTrainingData
            {
                LogText = logs
            });

            Enum.TryParse(prediction.PredictedLayer, out PipelineLayer layer);

            float confidence = prediction.Score.Max();

            return (layer, confidence);
        }
    }
}