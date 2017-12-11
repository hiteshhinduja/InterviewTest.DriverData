using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Analysers
{
    internal class DriverAnalyser
    {
        private readonly AnalyserConfiguration _analyserConfiguration;
        private readonly IPeriodRatingCalculator _periodRatingCalculator;
        private readonly IRatingCalculator _ratingCalculator;
        public DriverAnalyser(AnalyserConfiguration analyserConfiguration, IPeriodRatingCalculator periodRatingCalculator, IRatingCalculator ratingCalculator)
        {
            _analyserConfiguration = analyserConfiguration;
            _periodRatingCalculator = periodRatingCalculator;
            _ratingCalculator = ratingCalculator;
        }

        /// <summary>
        /// Calculates overall rating, total analysed duration and applies penalty if applicable based on analysed periods
        /// </summary>
        /// <param name="analysisResult"></param>
        /// <param name="analysedPeriods"></param>
        protected virtual void ComputeOverallResult(HistoryAnalysis analysisResult, IEnumerable<PeriodAnalysisResult> analysedPeriods)
        {
            if (analysedPeriods.Any())
            {
                var overallRating = _ratingCalculator.CalculateRating(analysedPeriods);

                //Get total undocumented duration
                var undocumentedDuration = analysedPeriods.Where(p => p.IsUndocumented).Sum(p => p.Duration);

                //Determine analysis duration (excluding undocumented duration)
                analysisResult.AnalysedDuration = analysedPeriods.Last().EndTime - analysedPeriods.First().StartTime - new TimeSpan(0, (int)undocumentedDuration, 0);

                analysisResult.DriverRating = overallRating;
                
                //Apply penalty if any undocumented periods recorded
                analysisResult.DriverRatingAfterPenalty = undocumentedDuration > 0 
                                                            ? overallRating * _analyserConfiguration.PenaltyForFaultyRecording 
                                                            : overallRating;
            }
        }

        /// <summary>
        /// Determines appropriate start time, end time, duration and rating for each period
        /// </summary>
        /// <param name="validPeriods"></param>
        /// <returns></returns>
        protected virtual IEnumerable<PeriodAnalysisResult> AnalysePeriods(IEnumerable<Period> validPeriods)
        {
            var analysedPeriods = new List<PeriodAnalysisResult>();

            foreach (var period in validPeriods)
            {
                var result = new PeriodAnalysisResult();
                //Set start time to permitted start time if it starts before permitted start time
                result.StartTime = period.Start.TimeOfDay < _analyserConfiguration.StartTime
                                    ? _analyserConfiguration.StartTime : period.Start.TimeOfDay;
                //Set end time to permitted end time if it ends after permitted end time
                result.EndTime = period.End.TimeOfDay > _analyserConfiguration.EndTime
                                    ? _analyserConfiguration.EndTime : period.End.TimeOfDay;
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
        protected virtual IEnumerable<PeriodAnalysisResult> AnalyzeUndocumentedPeriods(IEnumerable<Period> validPeriods)
        {
            var undocumentedPeriodResults = new List<PeriodAnalysisResult>();
            double duration = 0;
            for (int i = 0; i < validPeriods.Count(); i++)
            {
                //Check for undocumented periods for the first record by determining gap between record's start time and permitted start time
                if (i == 0 && validPeriods.ElementAt(i).Start.TimeOfDay > _analyserConfiguration.StartTime)
                {
                    duration = (validPeriods.ElementAt(i).Start.TimeOfDay - _analyserConfiguration.StartTime).TotalMinutes;
                    undocumentedPeriodResults.Add(new PeriodAnalysisResult() { StartTime = _analyserConfiguration.StartTime, EndTime = validPeriods.ElementAt(i).Start.TimeOfDay, Duration = (decimal)duration, Rating = _analyserConfiguration.RatingForUndocumentedPeriods, IsUndocumented = true });
                }
                //Check for undocumented periods for the last record by determining gap between record's end time and permitted end time
                else if (i == validPeriods.Count() - 1 && validPeriods.ElementAt(i).End.TimeOfDay < _analyserConfiguration.EndTime)
                {
                    duration = (_analyserConfiguration.EndTime - validPeriods.ElementAt(i).End.TimeOfDay).TotalMinutes;
                    undocumentedPeriodResults.Add(new PeriodAnalysisResult() { StartTime = validPeriods.ElementAt(i).End.TimeOfDay, EndTime = _analyserConfiguration.EndTime, Duration = (decimal)duration, Rating = _analyserConfiguration.RatingForUndocumentedPeriods, IsUndocumented = true });
                }
                //For intermediate records, Check for the gaps between current record's start time and previous record's end time
                //If there is a gap, then calculate undocumented period and rating
                if (i > 0 && validPeriods.ElementAt(i).Start.TimeOfDay > _analyserConfiguration.StartTime && validPeriods.ElementAt(i).Start.TimeOfDay > validPeriods.ElementAt(i - 1).End.TimeOfDay)
                {
                    duration = (validPeriods.ElementAt(i).Start.TimeOfDay - validPeriods.ElementAt(i - 1).End.TimeOfDay).TotalMinutes;
                    undocumentedPeriodResults.Add(new PeriodAnalysisResult() { StartTime = validPeriods.ElementAt(i - 1).End.TimeOfDay, EndTime = validPeriods.ElementAt(i).Start.TimeOfDay, Duration = (decimal)duration, Rating = _analyserConfiguration.RatingForUndocumentedPeriods, IsUndocumented = true });
                }
            }
            return undocumentedPeriodResults;
        }

        /// <summary>
        /// Checks whether given input contains any records
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        protected virtual bool IsValidInput(IReadOnlyCollection<Period> history)
        {
            return history != null && history.Count > 0;
        }

        /// <summary>
        /// Extracts periods that fall in permitted timeslot
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        protected virtual IEnumerable<Period> GetValidPeriods(IEnumerable<Period> history)
        {
            var invalidPeriods = history.Where
                                (p => p.Start.TimeOfDay >= _analyserConfiguration.EndTime
                                || p.End.TimeOfDay <= _analyserConfiguration.StartTime);

            return history.Except(invalidPeriods);
        }

        /// <summary>
        /// Removes leading and trailing periods that have zero average speed
        /// </summary>
        /// <param name="periods"></param>
        /// <returns></returns>
        protected virtual IEnumerable<Period> TrimPeriodsHavingZeroAverageSpeed(IEnumerable<Period> periods)
        {
            var trimmedStartPeriods = periods.SkipWhile(p => p.AverageSpeed == 0).ToList();
            var indexOfLastNonZeroSpeedPeriod = trimmedStartPeriods.IndexOf(trimmedStartPeriods.LastOrDefault(p => p.AverageSpeed > 0));
            return trimmedStartPeriods.Take(indexOfLastNonZeroSpeedPeriod + 1);
        }
    }
}
