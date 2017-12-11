using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    internal class WeightedRatingCalculator : IRatingCalculator
    {
        /// <summary>
        /// Calculates the overall weighted rating from given list of analysed periods
        /// </summary>
        /// <param name="analysedPeriods"></param>
        /// <returns></returns>
        public decimal CalculateRating(IEnumerable<PeriodAnalysisResult> analysedPeriods)
        {
            //Calculate the weighted sum by taking total of products of durations and ratings
            var weightedSum = analysedPeriods.Select(x => x.Duration * x.Rating).Sum();
            //Calculate overall rating by dividing the weighted sum by total duration including undocumented periods.
            return weightedSum / analysedPeriods.Sum(x => x.Duration);
        }
    }
}
