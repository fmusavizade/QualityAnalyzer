using System.Collections.Generic;

namespace QualityAnalyzer.Models
{
    public class AnalyzeResult
    {
        public string AxisLable { get; set; }
        public double Variance { get; set; }
        public string Outliers { get; set; }
        public TrendStatus TrendStatus { get; set; }
    }
}
