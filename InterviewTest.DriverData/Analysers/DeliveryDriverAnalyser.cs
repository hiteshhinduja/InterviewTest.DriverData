using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    /*
     * Internal classes are visible only inside an assembly and should be restricted access outside
     * the application for better maintainability. Here, a delivery driver analyser is made internal as it shouldn't
     * be modified outside the application. Where as the interface IAnalyser is kept public which can be extended.
     * This refers to Open-Closed Principle: A class should be open for extension and closed for modification.
    */
    internal class DeliveryDriverAnalyser : DriverAnalyser, IAnalyser
    {

        public DeliveryDriverAnalyser(AnalyserConfiguration analyserConfiguration, IPeriodRatingCalculator periodRatingCalculator, IRatingCalculator ratingCalculator)
            :base(analyserConfiguration, periodRatingCalculator, ratingCalculator)
        {
        }

        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
        {
            //Initialize default analysis result
            var analysisResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            if (IsValidInput(history))
            {
                var validPeriods = GetValidPeriods(history.OrderBy(x => x.Start));
                var analysedPeriods = AnalysePeriods(validPeriods);
                analysedPeriods = analysedPeriods.Concat(AnalyzeUndocumentedPeriods(validPeriods));
                ComputeOverallResult(analysisResult, analysedPeriods.OrderBy(p => p.StartTime));
            }
            return analysisResult;
        }

        
    }
}