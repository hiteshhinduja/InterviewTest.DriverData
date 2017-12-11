using InterviewTest.DriverData.Entities;
using System.Collections.Generic;

namespace InterviewTest.DriverData.Helpers.Interfaces
{
    internal interface IRatingCalculator
    {
        decimal CalculateRating(IEnumerable<PeriodAnalysisResult> resultSets);
    }
}
