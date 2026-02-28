namespace RootCauseAI.Models
{
    public enum PipelineLayer
    {
        Collection,
        Transport,
        Processing,
        Storage,
        Query,
        Visualization
    }

    public class Ticket
    {
        public int Id { get; set; }

        // NEW
        public string TenantId { get; set; } = "";

        public string CustomerDescription { get; set; } = "";
        public string Logs { get; set; } = "";

        public PipelineLayer? PredictedLayer { get; set; }
        public float Confidence { get; set; }

        public string SuggestedAction { get; set; } = "";
    }
}