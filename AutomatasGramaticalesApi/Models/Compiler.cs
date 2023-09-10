namespace AutomatasGramaticalesApi.Models
{
    public class Compiler
    {
        public bool Result { get; set; }
        public List<string> Messages { get; set; }
        public Dictionary<string, List<string>> variablesByType { get; set; }
    }
}
