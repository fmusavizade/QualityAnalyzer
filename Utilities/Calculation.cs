using QualityAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class Calculation
    {
        /// <summary>
        /// Computes the Variance of an input of double values
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double CalculateVariance(this List<double> input)
        {
            double average = input.Average();
            double sigma = input.Sum(x => Math.Pow((x - average), 2));
            return Math.Round(sigma / (input.Count - 1), 4);
        }
        /// <summary>
        /// Computes the OutLiers of an input of double values
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<double> CalculateOutLiers(this List<double> input)
        {

            var ortedList = input.OrderBy(x => x).ToList();
            var q2 = ortedList.CalculateMedianForSortedList();
            var q1 = ortedList.Where(x => x < q2).ToList().CalculateMedianForSortedList();
            var q3 = ortedList.Where(x => x > q2).ToList().CalculateMedianForSortedList();

            var IQR = q3 - q1;
            var thresholdLower = q1 - 1.5 * IQR;
            var thresholdUpper = q3 + 1.5 * IQR;
            return ortedList.Where(x => x < thresholdLower || x > thresholdUpper);

        }
        /// <summary>
        /// Calculate median for an sorted input of double values
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double CalculateMedianForSortedList(this List<double> input)
        {
            if (input == null || input.Count == 0)
                throw new ArgumentException("Array is empty.");
            if (input.Count == 1)
                return input[0];

            var sortedList = input;//.OrderBy(x=> x).ToList();
            int listLenght = sortedList.Count();
            int mid = listLenght / 2;

            if (listLenght % 2 != 0)
                return sortedList[mid];

            return (sortedList[mid] + sortedList[mid - 1]) / 2;
        }
        /// <summary>
        /// Checks the changing trend of an input of double values
        /// if that is Increasing it will return TrendStatus.Increasing
        /// if that is Decreasing it will return TrendStatus.Decreasing
        /// Otherwise it will return TrendStatus.Unknown
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TrendStatus DetectValueChangeTrend(this List<double> input)
        {
            if (input.CheckIncreasing())
                return TrendStatus.Increasing;
            return input.CheckDecreasing()==true ? TrendStatus.Decreasing : TrendStatus.Unknown;
        }
        /// <summary>
        /// Checks the changing trend of an input of double values if that is Increasing it will return true
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckIncreasing(this List<double> input)
        {
            for (int i = 0; i < input.Count() - 1; i++)
            {
                if (!(input[i] < input[i + 1]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks the changing trend of an input of double values if that is Decreasing it will return true
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckDecreasing(this List<double> input)
        {
            for (int i = 0; i < input.Count() - 1; i++)
            {
                if (!(input[i] > input[i + 1]))
                    return false;
            }
            return true;
        }
    }
}
