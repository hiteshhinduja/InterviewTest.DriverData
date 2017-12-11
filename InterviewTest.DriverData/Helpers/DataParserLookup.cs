using InterviewTest.DriverData.Entities.Enums;
using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public static class DataParserLookup
    {
        private static Dictionary<ParserType, Func<ICannedDataParser>> parsers = new Dictionary<ParserType, Func<ICannedDataParser>>()
        {
            {ParserType.Csv, () => {return new CsvDataParser(); } }
        };

        /// <summary>
        /// Gets the appropriate data parser based on given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ICannedDataParser GetParser(ParserType type)
        {
            if (parsers.ContainsKey(type))
            {
                return parsers[type]();
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised parser type");
        }
    }
}
