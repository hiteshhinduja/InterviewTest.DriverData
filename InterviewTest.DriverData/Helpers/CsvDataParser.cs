using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class CsvDataParser : ICannedDataParser
    {
        /// <summary>
        /// Parses the given csv data in string format into list of periods
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Period> ParseData(string data)
        {
            List<Period> listOfPeriods = new List<Period>();

            try
            {
                using (var reader = GenerateStreamFromString(data))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        //Split the line with comma(,) since the .csv format uses comma as separator for different values.
                        var values = line.Split(',');
                        var period = new Period();
                        //Read the first value which is start time of period
                        period.Start = DateTimeOffset.Parse(values[0]);
                        //Read the second value which is end time of period
                        period.End = DateTimeOffset.Parse(values[1]);
                        //Read the third value which is the Average speed for that period
                        period.AverageSpeed = Convert.ToDecimal(values[2]);

                        listOfPeriods.Add(period);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while parsing the data. {ex.Message}");
            }

            return listOfPeriods;
        }

        private StreamReader GenerateStreamFromString(string content)
        {
            return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(content ?? "")));
        }
    }
}
