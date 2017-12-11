using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers.Interfaces;

namespace InterviewTest.DriverData.Helpers
{
    internal class LinearSpeedRatingCalculator : IPeriodRatingCalculator
    {
        /// <summary>
        /// Calculates linearly mapped rating between 0 and maximum permitted speed for the period as per the analyser configuration
        /// </summary>
        /// <param name="period"></param>
        /// <param name="analyserConfiguration"></param>
        /// <returns></returns>
        public decimal CalculatePeriodRating(Period period, AnalyserConfiguration analyserConfiguration)
        {

            //Check if average speed is greater than maximum permitted speed.
            //If yes, then assign rating configured for exceeding maximum speed
            //If no, then calculate the rating by linearly mapping the average speeds between 0 and maximum speed to 0-1
            var rating = (period.AverageSpeed > analyserConfiguration.MaxSpeed)
                            ? analyserConfiguration.RatingForExceedingMaxSpeed 
                            : (period.AverageSpeed / analyserConfiguration.MaxSpeed);
            return rating;
        }
    }
}
