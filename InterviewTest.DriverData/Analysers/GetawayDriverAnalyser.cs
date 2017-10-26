﻿using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class GetawayDriverAnalyser : IAnalyser
	{
        public AnalyserConfiguration AnalyserConfiguration { get; set; }

        public GetawayDriverAnalyser(AnalyserConfiguration _analysisConfiguration)
        {
            AnalyserConfiguration = _analysisConfiguration;
        }
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
		{
            List<Result> ratings = new List<Result>();

            Result result;
            double unDocumentedDuration = 0;
            double duration = 0;
            decimal rating = 0;

            //Return 0 duration and 0 rating if no periods available or if configuration is not set
            if (AnalyserConfiguration == null || history == null || history.Count == 0)
            {
                return new HistoryAnalysis

                {
                    AnalysedDuration = new TimeSpan(0, 0, 0),
                    DriverRating = 0
                };
            }

            //Sort the given list of periods in the ascending order of start time
            history = history.OrderBy(x => x.Start).ToArray();
            var entriesToConsider = history.ToList();
            //Take the index of record with first non zero average speed
            var begin = entriesToConsider.IndexOf(history.FirstOrDefault(x => x.AverageSpeed > 0));
            //Take the index of record with last non zero average speed
            var end = entriesToConsider.IndexOf(history.LastOrDefault(x => x.AverageSpeed > 0));

            //Check if there is any period with non zero average speed, if yes then proceed to calculate duration and ratings
            if (begin != -1)
            {
                for (int i = begin; i <= end; i++)
                {
                    //Ignore anything outside the permitted time slot
                    if ((history.ElementAt(i).Start.TimeOfDay >= AnalyserConfiguration.EndTime)
                        || history.ElementAt(i).End.TimeOfDay <= AnalyserConfiguration.StartTime)
                        continue;

                    //Check for undocumented periods for the first valid record by determining gap between record's start time and permitted start time
                    if (i == begin && history.ElementAt(i).Start.TimeOfDay > AnalyserConfiguration.StartTime)
                    {
                        duration = (history.ElementAt(i).Start.TimeOfDay - AnalyserConfiguration.StartTime).TotalMinutes;

                        unDocumentedDuration += duration;

                        ratings.Add(new Result() { StartTime = AnalyserConfiguration.StartTime, EndTime = history.ElementAt(i).Start.TimeOfDay, Duration = (decimal)duration, Rating = 0 });
                    }
                    //Check for undocumented periods for the last valid record by determining gap between record's end time and permitted end time
                    else if (i == end && history.ElementAt(i).End.TimeOfDay < AnalyserConfiguration.EndTime)
                    {
                        duration = (AnalyserConfiguration.EndTime - history.ElementAt(i).End.TimeOfDay).TotalMinutes;

                        unDocumentedDuration += duration;

                        ratings.Add(new Result() { StartTime = history.ElementAt(i).End.TimeOfDay, EndTime = AnalyserConfiguration.EndTime, Duration = (decimal)duration, Rating = 0 });
                    }
                    //For intermediate valid records, Check for the gaps between current record's start time and previous record's end time
                    //If there is a gap, then calculate undocumented period and rating
                    if (i > begin && history.ElementAt(i).Start.TimeOfDay > AnalyserConfiguration.StartTime && history.ElementAt(i).Start > history.ElementAt(i - 1).End)
                    {
                        duration = (history.ElementAt(i).Start - history.ElementAt(i - 1).End).TotalMinutes;

                        unDocumentedDuration += duration;

                        ratings.Add(new Result() { StartTime = history.ElementAt(i - 1).End.TimeOfDay, EndTime = history.ElementAt(i).Start.TimeOfDay, Duration = (decimal)duration, Rating = 0 });
                    }

                    //Check if average speed is greater than maximum permitted speed.
                    //If yes, then assign rating configured for exceeding maximum speed
                    //If no, then calculate the rating by linearly mapping the average speeds between 0 and maximum speed to 0-1
                    rating = (history.ElementAt(i).AverageSpeed > AnalyserConfiguration.MaxSpeed) ? AnalyserConfiguration.RatingForExceedingMaxSpeed : (history.ElementAt(i).AverageSpeed / AnalyserConfiguration.MaxSpeed);

                    //Create result set containing Start and End time along with calculated rating
                    result = new Result() { StartTime = history.ElementAt(i).Start.TimeOfDay, EndTime = history.ElementAt(i).End.TimeOfDay, Rating = rating };

                    //Set start time to permitted start time if it starts before permitted start time
                    if (history.ElementAt(i).Start.TimeOfDay < AnalyserConfiguration.StartTime)
                        result.StartTime = AnalyserConfiguration.StartTime;

                    //Set end time to permitted end time if it ends after permitted end time
                    if (history.ElementAt(i).End.TimeOfDay > AnalyserConfiguration.EndTime)
                        result.EndTime = AnalyserConfiguration.EndTime;

                    //Get the duration for the current result set
                    result.Duration = (decimal)(result.EndTime - result.StartTime).TotalMinutes;

                    ratings.Add(result);
                }
            }

            //If no period is considered valid during analysis, return 0 duration and 0 rating
            if (!ratings.Any())
            {
                return new HistoryAnalysis

                {
                    AnalysedDuration = new TimeSpan(0, 0, 0),
                    DriverRating = 0
                };
            }

            //Sort the rating resultsets in ascending order of start time
            ratings = ratings.OrderBy(x => x.StartTime).ToList();

            //Calculate overall rating for periods considered
            var overallRating = RatingCalculator.CalculateOverallRating(ratings);

            //Return analysis duration (excluding undocumented duration) with overall rating
            return new HistoryAnalysis
            {
                AnalysedDuration = ratings.Last().EndTime - ratings.First().StartTime - new TimeSpan(0, (int)unDocumentedDuration, 0),
                DriverRating = overallRating
            };
        }
	}
}