using InterviewTest.DriverData.Entities.Enums;
using InterviewTest.DriverData.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.Lookups
{
    [TestFixture]
    public class DataParserLookupTest
    {
        [Test]
        public void ShouldCreateCsvDataReaderInstance()
        {
            //Arrange
            var parserType = ParserType.Csv;

            //Act
            var parserInstance = DataParserLookup.GetParser(parserType);

            //Assert
            Assert.IsInstanceOf(typeof(CsvDataParser), parserInstance);
        }

        [Test]
        public void ForInvalidParserType_ShouldThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>
                            (() => DataParserLookup.GetParser((ParserType)Enum.Parse(typeof(ParserType), "SomeOtherParser")));
        }
    }
}
