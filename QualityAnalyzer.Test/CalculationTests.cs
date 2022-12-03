using QualityAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using Xunit;

namespace QualityAnalyzer.Test
{
    public class CalculationTests
    {
        [Theory]
        [InlineData(null)]
        public void CalculateVarianceTest_Exception(IEnumerable<double> listData)
        {
            Assert.Throws<ArgumentNullException>(()=>listData.ToList().CalculateVariance());
        }

        [Theory]
        [InlineData(new double[] { 17, 15, 14, 16, 19, 58 }, 294.1667)]
        [InlineData(new double[] { 0, 0, 0 }, 0)]
        public void CalculateVarianceTest(IEnumerable<double> listData, double expected)
        {
            Assert.Equal(expected, listData.ToList().CalculateVariance());
        }

        [Theory]
        [InlineData(new double[] { double.MaxValue, double.MaxValue, double.MaxValue })]
        [InlineData(new double[] { 0, double.MinValue, double.MaxValue })]
        public void CalculateVarianceTest_Infinity(IEnumerable<double> listData)
        {
            Assert.True(double.IsInfinity(listData.ToList().CalculateVariance()));
        }
        [Theory]
        [InlineData(new double[] { 17, 15, 14, 16, 19, 58 }, new double[] { 58 })]
        [InlineData(new double[] { double.MinValue, 15, 14, 16, 19, 58 }, new double[] { double.MinValue, 58 })]
        [InlineData(new double[] { double.MaxValue, 15, 14, 16, 19, 58 }, new double[] { double.MaxValue })]
        [InlineData(new double[] { double.MaxValue, double.MinValue, 14, 16, 19, 58 }, new double[] { double.MinValue, double.MaxValue })]
        public void CalculateOuterlineTest(IEnumerable<double> listData, IEnumerable<double> expected)
        {
            Assert.Equal<double>(expected.ToList(), listData.ToList().CalculateOutLiers());
        }
        [Theory]
        [InlineData(new double[] { 17, 15, 14, 16, 19, 58 }, TrendStatus.Unknown)]
        [InlineData(new double[] { 17, 15, 14, 13, double.MinValue }, TrendStatus.Decreasing)]
        [InlineData(new double[] { 17, 25, 118, 130, 1200, double.MaxValue }, TrendStatus.Increasing)]
        public void DetectValueChangeTrendTest(IEnumerable<double> listData, TrendStatus expected)
        {
            Assert.Equal(expected, listData.ToList().DetectValueChangeTrend());
        }


    }
}
