using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.Helpers.Interfaces
{
    internal interface IPeriodRatingCalculator
    {
        decimal CalculatePeriodRating(Period period, AnalyserConfiguration analyserConfiguration);
    }
}
