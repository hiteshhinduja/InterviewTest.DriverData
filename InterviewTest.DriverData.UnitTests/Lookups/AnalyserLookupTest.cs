using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.Lookups
{
    [TestFixture]
    public class AnalyserLookupTest
    {
        [Test]
        public void ShouldCreateDeliveryDriverAnalyserInstance()
        {
            //Arrange
            var analyserType = AnalyserType.Delivery;
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(DeliveryDriverAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldCreateFormulaOneDriverAnalyserInstance()
        {
            //Arrange
            var analyserType = AnalyserType.FormulaOne;
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(FormulaOneAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldCreateGetawayDriverAnalyserInstance()
        {
            //Arrange
            var analyserType = AnalyserType.Getaway;
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(GetawayDriverAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldCreateFriendlyAnalyserInstance()
        {
            //Arrange
            var analyserType = AnalyserType.Friendly;
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(FriendlyAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldCreateInstancesForAllSupportedTypes()
        {
            //Arrange
            var supportedAnalyserTypes = Enum.GetValues(typeof(AnalyserType));

            //Act & Assert
            foreach (var value in supportedAnalyserTypes)
            {
                var analyser = AnalyserLookup.GetAnalyser((AnalyserType)value);
                Assert.IsNotNull(analyser);
            }
        }

        [Test]
        public void ForInvalidAnalyserType_ShouldThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>
                            (() => AnalyserLookup.GetAnalyser((AnalyserType)Enum.Parse(typeof(AnalyserType), "SomeOtherAnalyser")));
        }
    }
}
