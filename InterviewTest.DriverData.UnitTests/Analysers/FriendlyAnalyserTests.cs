﻿using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FriendlyAnalyserTests
	{
		[Test]
		public void ShouldAnalyseWholePeriodAndReturn1ForDriverRating()
		{
            // BONUS: What is AAA?
            /*
             * AAA is Arrange, Act, Assert which is a three step fundamental process for writing an ideal unit test case
             * Arrange: Prepare the test data such as inputs to be given, expected output, etc.
             * Act: Execute the code which is to be unit tested using the data prepared in Arrange step.
             * Assert: Check the correctness (or incorrectness) of the actual output obtained in the Act step by comparing with the expected output.
             */

            var data = new[]
			{
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					AverageSpeed = 20m
				},
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 2, 0, 0, 0, TimeSpan.Zero),
					AverageSpeed = 15m
				}
			};

			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromDays(1),
				DriverRating = 1m
			};

			var actualResult = new FriendlyAnalyser().Analyse(data);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
		}
	}
}
