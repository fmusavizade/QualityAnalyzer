using QualityAnalyzer.Models;
using Utilities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QualityAnalyzer.Test
{
    public class CalculationTests
    {
        [Fact]
        public void CalculateVarianceTest()
        {
            var listData = new List<double>() { 17, 15, 14, 16, 19, 58 };
            Assert.Equal(294.1667, listData.CalculateVariance());
        }
        [Fact]
        public void CalculateOuterlineTest()
        {
            var listData = new List<double>() { 17, 15, 14, 16, 19, 58 };
            Assert.Equal(58, listData.CalculateOutLiers().First());
        }
        [Fact]
        public void DetectValueChangeTrendTest_Unknown()
        {
            var listData = new List<double>() { 17, 15, 14, 16, 19, 58 };
            Assert.Equal(TrendStatus.Unknown, listData.DetectValueChangeTrend());
        }
        [Fact]
        public void DetectValueChangeTrendTest_Increasing()
        {
            var listData = new List<double>() { 14, 15, 16, 17, 19, 58 };
            Assert.True(listData.CheckIncreasing());
        }

        [Fact]
        public void DetectValueChangeTrendTest_Decreasing()
        {
            var listData = new List<double>() { 58, 19, 17, 16, 15, 14 };
            Assert.True(listData.CheckDecreasing());
        }

    }
}
