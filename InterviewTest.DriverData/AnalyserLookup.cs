using System;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities;
using System.Collections.Generic;
using InterviewTest.DriverData.Entities.Enums;
using InterviewTest.DriverData.Helpers;

namespace InterviewTest.DriverData
{
	public static class AnalyserLookup
	{
        private static Dictionary<AnalyserType, Func<IAnalyser>> analysers = new Dictionary<AnalyserType, Func<IAnalyser>>()
        {
            {AnalyserType.Delivery, DeliveryDriverAnalyser },
            {AnalyserType.FormulaOne, FormulaOneAnalyser },
            {AnalyserType.Getaway,  GetawayAnalyser},
            {AnalyserType.Friendly, FriendlyAnalyser }
        };

        /// <summary>
        /// Gets appropriate analyser instance based on given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IAnalyser GetAnalyser(AnalyserType type)
        {
            if (analysers.ContainsKey(type))
            {
                return analysers[type]();
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");
        }

        private static IAnalyser DeliveryDriverAnalyser()
        {
            var analyserConfiguration = new AnalyserConfiguration
            {
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                MaxSpeed = 30m,
                RatingForExceedingMaxSpeed = 0,
                PenaltyForFaultyRecording = 0.5m,
                RatingForUndocumentedPeriods = 0
            };
            var periodRatingCalculator = new LinearSpeedRatingCalculator();
            var ratingCalculator = new WeightedRatingCalculator();
            return new DeliveryDriverAnalyser(analyserConfiguration, periodRatingCalculator, ratingCalculator);
        }

        private static IAnalyser FormulaOneAnalyser()
        {
            var analyserConfiguration = new AnalyserConfiguration
            {
                MaxSpeed = 200m,
                RatingForExceedingMaxSpeed = 1,
                PenaltyForFaultyRecording = 0.5m,
                RatingForUndocumentedPeriods = 0
            };
            var periodRatingCalculator = new LinearSpeedRatingCalculator();
            var ratingCalculator = new WeightedRatingCalculator();
            return new FormulaOneAnalyser(analyserConfiguration, periodRatingCalculator, ratingCalculator);
        }

        private static IAnalyser GetawayAnalyser()
        {
            var analyserConfiguration = new AnalyserConfiguration
            {
                StartTime = new TimeSpan(13, 0, 0),
                EndTime = new TimeSpan(14, 0, 0),
                MaxSpeed = 80m,
                RatingForExceedingMaxSpeed = 1,
                PenaltyForFaultyRecording = 0.5m,
                RatingForUndocumentedPeriods = 0
            };
            var periodRatingCalculator = new LinearSpeedRatingCalculator();
            var ratingCalculator = new WeightedRatingCalculator();
            return new GetawayDriverAnalyser(analyserConfiguration, periodRatingCalculator, ratingCalculator);
        }

        private static IAnalyser FriendlyAnalyser()
        {
            return new FriendlyAnalyser();
        }
    }
}
