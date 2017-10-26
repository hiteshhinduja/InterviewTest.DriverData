﻿using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
	public static class CannedDrivingData
	{
		private static readonly DateTimeOffset _day = new DateTimeOffset(2016, 10, 13, 0, 0, 0, 0, TimeSpan.Zero);

		// BONUS: What's so great about IReadOnlyCollections?
		public static readonly IReadOnlyCollection<Period> History = new[]
		{
			new Period
			{
				Start = _day + new TimeSpan(0, 0, 0),
				End = _day + new TimeSpan(8, 54, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(8, 54, 0),
				End = _day + new TimeSpan(9, 28, 0),
				AverageSpeed = 28m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 28, 0),
				End = _day + new TimeSpan(9, 35, 0),
				AverageSpeed = 33m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 50, 0),
				End = _day + new TimeSpan(12, 35, 0),
				AverageSpeed = 25m
			},
			new Period
			{
				Start = _day + new TimeSpan(12, 35, 0),
				End = _day + new TimeSpan(13, 30, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(13, 30, 0),
				End = _day + new TimeSpan(19, 12, 0),
				AverageSpeed = 29m
			},
			new Period
			{
				Start = _day + new TimeSpan(19, 12, 0),
				End = _day + new TimeSpan(24, 0, 0),
				AverageSpeed = 0m
			}
		};

        public static readonly IReadOnlyCollection<Period> DeliveryDriverDataWithPeriodsOutOfPermittedTimeSlot = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(2, 30, 0),
                AverageSpeed = 30m
            },
            new Period
            {
                Start = _day + new TimeSpan(4, 31, 0),
                End = _day + new TimeSpan(5, 47, 0),
                AverageSpeed = 28m
            },
            new Period
            {
                Start = _day + new TimeSpan(5, 55, 0),
                End = _day + new TimeSpan(7, 10, 0),
                AverageSpeed = 33m
            },
            new Period
            {
                Start = _day + new TimeSpan(7, 10, 0),
                End = _day + new TimeSpan(8, 58, 0),
                AverageSpeed = 25m
            },
            new Period
            {
                Start = _day + new TimeSpan(17, 30, 0),
                End = _day + new TimeSpan(18, 30, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(18, 45, 0),
                End = _day + new TimeSpan(19, 56, 0),
                AverageSpeed = 29m
            },
            new Period
            {
                Start = _day + new TimeSpan(20, 42, 0),
                End = _day + new TimeSpan(21, 0, 0),
                AverageSpeed = 0m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverDataWithPeriodsHavingAverageSpeedMoreThanMaxSpeed = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(10, 30, 0),
                AverageSpeed = 35m
            },
            new Period
            {
                Start = _day + new TimeSpan(10, 50, 0),
                End = _day + new TimeSpan(11, 30, 0),
                AverageSpeed = 38m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(12, 50, 0),
                AverageSpeed = 33m
            },
            new Period
            {
                Start = _day + new TimeSpan(14, 30, 0),
                End = _day + new TimeSpan(16, 0, 0),
                AverageSpeed = 40m
            },
            new Period
            {
                Start = _day + new TimeSpan(16, 30, 0),
                End = _day + new TimeSpan(17, 30, 0),
                AverageSpeed = 31m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingGapsBetweenThem = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 20, 0),
                End = _day + new TimeSpan(10, 0, 0),
                AverageSpeed = 15m
            },
            new Period
            {
                Start = _day + new TimeSpan(10, 30, 0),
                End = _day + new TimeSpan(11, 50, 0),
                AverageSpeed = 30m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(13, 0, 0),
                AverageSpeed = 29m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 0, 0),
                End = _day + new TimeSpan(13, 35, 0),
                AverageSpeed = 32m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 50, 0),
                End = _day + new TimeSpan(15, 0, 0),
                AverageSpeed = 23m
            },
            new Period
            {
                Start = _day + new TimeSpan(15, 10, 0),
                End = _day + new TimeSpan(15, 40, 0),
                AverageSpeed = 40m
            },
            new Period
            {
                Start = _day + new TimeSpan(15, 40, 0),
                End = _day + new TimeSpan(16, 25, 0),
                AverageSpeed = 22m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingNoGapsBetweenPeriods = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(10, 0, 0),
                AverageSpeed = 25m
            },
            new Period
            {
                Start = _day + new TimeSpan(10, 0, 0),
                End = _day + new TimeSpan(12, 0, 0),
                AverageSpeed = 30m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(15, 30, 0),
                AverageSpeed = 29m
            },
            new Period
            {
                Start = _day + new TimeSpan(15, 30, 0),
                End = _day + new TimeSpan(16, 40, 0),
                AverageSpeed = 32m
            },
            new Period
            {
                Start = _day + new TimeSpan(16, 40, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 23m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverDataWithPeriodsHavingSameStartAndEndTime = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(9, 0, 0),
                AverageSpeed = 25m
            },
            new Period
            {
                Start = _day + new TimeSpan(10, 0, 0),
                End = _day + new TimeSpan(10, 0, 0),
                AverageSpeed = 30m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(12, 0, 0),
                AverageSpeed = 29m
            },
            new Period
            {
                Start = _day + new TimeSpan(15, 0, 0),
                End = _day + new TimeSpan(15, 0, 0),
                AverageSpeed = 32m
            },
            new Period
            {
                Start = _day + new TimeSpan(17, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 23m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingZeroAverageSpeed = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 20, 0),
                End = _day + new TimeSpan(10, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(10, 30, 0),
                End = _day + new TimeSpan(11, 50, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(13, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 0, 0),
                End = _day + new TimeSpan(13, 35, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 50, 0),
                End = _day + new TimeSpan(15, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(15, 10, 0),
                End = _day + new TimeSpan(15, 40, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(15, 40, 0),
                End = _day + new TimeSpan(16, 25, 0),
                AverageSpeed = 0m
            }
        };
    }
}
