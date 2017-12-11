Task wise implementation details

Followed TDD Approach for each task:
Added entities, interfaces, skeleton classes as applicable for the task
Added unit test case for all possible scenarios
Executed test cases for unimplemented code which resulted in failure
Implemented business logic for the task
Re-ran unit test cases and checked whether they passed
Refactored code: 
	Simplified analyse method for all analysers.
	Extracted common logic for all analysers into a base class.
	Injected period rating calculator and overall rating calculator into analysers to provide flexibility for changing rating calculation logic in future

Task 1
1.1 Delivery Driver Analysis

Created AnalysisConfiguration entity for storing Permitted start and end times along with maximum speed allowed, rating to be given when average speed exceeds maximum speed and rating to be given for undocumented periods
Created PeriodAnalysisResult entity for storing rating, duration, start and end times for each period considered (documented and undocumented)
Implemented code to analyse data in DeliveryDriverAnalyser by considering documented and undocumented periods as per the configuration given.
Created Helper class RatingCalculator (now refactored to WeightedRatingCalculator) to calculate overall rating from the given result set
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of PeriodAnalysisResult entity to get the overall rating.


1.2 Formula One Driver Analysis

Implemented code to analyse data in FormulaOneAnalyser by considering documented and undocumented periods as per the configuration given.
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of PeriodAnalysisResult entity to get the overall rating.

1.3 Getaway Driver Analysis

Implemented code to analyse data in GetawayDriverAnalyser by considering documented and undocumented periods as per the configuration given.
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of PeriodAnalysisResult entity to get the overall rating.

1.4 Penalise Faulty Recording

Added a property PenaltyForFaultyRecording in AnalysisConfiguration entity to determine how much penalty is to be applied if undocumented periods exist
Added a property DriverRatingAfterPenalty in HistoryAnalysis entity to store the rating after penalty is applied (if not applied, it will be same as DriverRating)
Updated the code for all analysers to apply penalty after calculating overall rating if any undocumented periods exist.

Task 2 Better Analyser Lookup(Dependency Inversion)

Added a dictionary in AnalyserLookup class which contains registered analysers along with a delegate call to respective analyser constructor which takes AnalysisConfiguration, PeriodRatingCalculator and RatingCalculator as parameters and returns analyser instance
AnalyserLookup refers this dictionary with the analyser type received to return appropriate analyser instance.

Task 3 Read Canned Data from file

Chose .csv as data file containing CannedData
Added unit test cases for each analyser where data is read from file
Created .csv file containing information for different periods
Added base directory path in app.config of tests and console. This path points to the directory where .csv data files will be available
Created a FileReader which accepts absolute path containing file name as input and returns file content as string
Created a CsvDataParser which accepts string data as input, parses the data and returns list of periods

Task 4 Improve the tests

Added few more negative scenario test cases as majority of the test cases were already in place.