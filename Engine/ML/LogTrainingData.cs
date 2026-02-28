using Microsoft.ML.Data;

namespace RootCauseAI.Engine.ML
{
    public class LogTrainingData
    {
        [LoadColumn(0)]
        public string LogText { get; set; } = "";

        [LoadColumn(1)]
        public string Label { get; set; } = "";
    }

    public class LogPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLayer { get; set; } = "";

        public float[] Score { get; set; } = [];
    }
}