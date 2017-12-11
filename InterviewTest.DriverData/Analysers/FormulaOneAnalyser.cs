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
     * the application for better maintainability. Here, a formula one analyser is made internal as it shouldn't
     * be modified outside the application. Where as the interface IAnalyser is kept public which can be extended.
     * This refers to Open-Closed Principle: A class should be open for extension and closed for modification.
    */
    internal class FormulaOneAnalyser : DriverAnalyser, IAnalyser
	{
        private readonly AnalyserConfiguration _analyserConfiguration;
        private readonly IPeriodRatingCalculator _periodRatingCalculator;
        public FormulaOneAnalyser(AnalyserConfiguration analyserConfiguration, IPeriodRatingCalculator periodRatingCalculator, IRatingCalculator ratingCalculator)
            :base(analyserConfiguration, periodRatingCalculator, ratingCalculator)
        {
            _analyserConfiguration = analyserConfiguration;
            _periodRatingCalculator = periodRatingCalculator;
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

        /// <summary>
        /// Ignores everything before first and after the last period with non-zero average speed
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        protected override IEnumerable<Period> GetValidPeriods(IEnumerable<Period> history)
        {
            return base.TrimPeriodsHavingZeroAverageSpeed(history);
        }

        /// <summary>
        /// Determines duration and rating for each period
        /// </summary>
        /// <param name="validPeriods"></param>
        /// <returns></returns>
        protected override IEnumerable<PeriodAnalysisResult> AnalysePeriods(IEnumerable<Period> validPeriods)
        {
            var analysedPeriods = new List<PeriodAnalysisResult>();

            foreach (var period in validPeriods)
            {
                var result = new PeriodAnalysisResult
                {
                    StartTime = period.Start.TimeOfDay,
                    EndTime = period.End.TimeOfDay
                };
                //Get the duration for the current result set
                result.Duration = (decimal)(result.EndTime - result.StartTime).TotalMinutes;
                result.Rating = _periodRatingCalculator.CalculatePeriodRating(period, _analyserConfiguration);
                analysedPeriods.Add(result);
            }
            return analysedPeriods.OrderBy(x => x.StartTime);
        }

        
        /// <summary>
        /// Identifies gaps between periods, determines duration and rating for all such undocumented periods
        /// </summary>
        /// <param name="validPeriods"></param>
        /// <returns></returns>
        protected override IEnumerable<PeriodAnalysisResult> AnalyzeUndocumentedPeriods(IEnumerable<Period> validPeriods)
        {
            var undocumentedPeriodResults = new List<PeriodAnalysisResult>();
            for (int i = 1; i < validPeriods.Count(); i++)
            {
                //For intermediate records, Check for the gaps between current record's start time and previous record's end time
                //If there is a gap, then calculate undocumented period and rating
                if (validPeriods.ElementAt(i).Start.TimeOfDay > _analyserConfiguration.StartTime && validPeriods.ElementAt(i).Start.TimeOfDay > validPeriods.ElementAt(i - 1).End.TimeOfDay)
                {
                    var duration = (validPeriods.ElementAt(i).Start.TimeOfDay - validPeriods.ElementAt(i - 1).End.TimeOfDay).TotalMinutes;
                    undocumentedPeriodResults.Add(new PeriodAnalysisResult() { StartTime = validPeriods.ElementAt(i - 1).End.TimeOfDay, EndTime = validPeriods.ElementAt(i).Start.TimeOfDay, Duration = (decimal)duration, Rating = _analyserConfiguration.RatingForUndocumentedPeriods, IsUndocumented = true });
                }
            }
            return undocumentedPeriodResults;
        }
    }
}