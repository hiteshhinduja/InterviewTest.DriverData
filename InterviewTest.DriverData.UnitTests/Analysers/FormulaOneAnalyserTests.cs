using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserTests
	{
        private FormulaOneAnalyser analyser;

        [SetUp]
        public void Initialize()
        {
            analyser = new FormulaOneAnalyser(new AnalyserConfiguration() { MaxSpeed = 200m, RatingForExceedingMaxSpeed = 1});
        }

        [Test]
        public void ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.History);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsHavingAverageSpeedZero_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingZeroAverageSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsHavingAverageSpeedMoreThanMaxSpeed_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 0, 0),
                DriverRating = 0.8470m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingAverageSpeedMoreThanMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsHavingAverageSpeedEqualToMaxSpeed_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 0, 0),
                DriverRating = 0.8470m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingAverageSpeedEqualToMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsHavingNoGapsAndAverageSpeedLessThanOrEqualToMaxSpeed_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(5, 0, 0),
                DriverRating = 0.8835m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingNoGapsAndAverageSpeedLessThanOrEqualToMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
