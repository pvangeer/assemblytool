// Copyright (C) Stichting Deltares 2018. All rights reserved.
//
// This file is part of AssemblyTool.
//
// AssemblyTool is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
// All names, logos, and references to "Deltares" are registered trademarks of
// Stichting Deltares and remain full property of Stichting Deltares at all times.
// All rights reserved.

using System;
using System.Globalization;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Data.Test
{
    [TestFixture]
    public class ProbabilityTest
    {
        [TestCase(-1, ErrorCode.ValueBelowZero)]
        [TestCase(2, ErrorCode.ValueAboveOne)]
        [TestCase(double.NaN, ErrorCode.ValueIsNaN)]
        public void ValidateProbabilityTest(double probabilityValue, ErrorCode expectedErrorCode)
        {
            try
            {
                new Probability(probabilityValue);
                Assert.Fail(string.Format("An exception with error {0} was expected.", expectedErrorCode));
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(1,e.Code.Length);
                Assert.AreEqual(expectedErrorCode, e.Code[0]);
            }
        }

        [Test]
        [TestCase(0.01)]
        [TestCase(0)]
        [TestCase(1)]
        public void Constructor_ExpectedValues(double probabilityValue)
        {
            // Call
            var probability = new Probability(probabilityValue);

            // Assert
            Assert.IsInstanceOf<IEquatable<Probability>>(probability);
            Assert.IsInstanceOf<IEquatable<double>>(probability);
            Assert.AreEqual(probabilityValue, probability.Value);
            Assert.AreEqual((int)Math.Round(1/ probabilityValue),probability.ReturnPeriod);
        }

        [Test]
        [SetCulture("nl-NL")]
        [TestCase(1.0, "1")]
        [TestCase(0, "0")]
        [TestCase(0.01, "1/100")]
        public void ToString_VariousScenarios_ExpectedText(double value, string expectedText)
        {
            // Setup
            var probability = new Probability(value);

            // Call
            string text = probability.ToString();

            // Assert
            Assert.AreEqual(expectedText, text);
        }

        [Test]
        [SetCulture("nl-NL")]
        [TestCase("N", 0.5156, "0,52")]
        [TestCase("N0", 0.5156, "1")]
        [TestCase("N1", 0.5156, "0,5")]
        [TestCase("G", 0.5156, "0,5156")]
        [TestCase("G3", 0.5156, "0,516")]
        [TestCase("G9", 0.5156, "0,5156")]
        [TestCase("P3", 0.5156, "51,560 %")]
        [TestCase("P1", 0.5156, "51,6 %")]
        [TestCase("P", 0.5156, "51,56 %")]
        [TestCase("P3", 0.5156, "51,560 %")]
        [TestCase("F1", 0.5156, "0,5")]
        [TestCase("F3", 0.5156, "0,516")]
        [TestCase("F5", 0.5156, "0,51560")]
        [TestCase("F2", 0.5156, "0,52")]
        public void ToString_WithFormatAndCurrentCultureVariousScenarios_ExpectedText(string format,double value, string expectedText)
        {
            // Setup
            var probability = new Probability(value);

            // Call
            string text = probability.ToString(format, null);

            // Assert
            Assert.AreEqual(expectedText, text);
        }

        [Test]
        [SetCulture("nl-NL")]
        [TestCase("N", 0.5156, "0.52")]
        [TestCase("N0", 0.5156, "1")]
        [TestCase("N1", 0.5156, "0.5")]
        [TestCase("G", 0.5156, "0.5156")]
        [TestCase("G3", 0.5156, "0.516")]
        [TestCase("G9", 0.5156, "0.5156")]
        [TestCase("P3", 0.5156, "51.560 %")]
        [TestCase("P1", 0.5156, "51.6 %")]
        [TestCase("P", 0.5156, "51.56 %")]
        [TestCase("P3", 0.5156, "51.560 %")]
        [TestCase("F1", 0.5156, "0.5")]
        [TestCase("F3", 0.5156, "0.516")]
        [TestCase("F5", 0.5156, "0.51560")]
        [TestCase("F2", 0.5156, "0.52")]
        public void ToString_WithFormatAndDifferentCultureVariousScenarios_ExpectedText(string format,double value, string expectedText)
        {
            // Setup
            var probability = new Probability(value);

            // Call
            string text = probability.ToString(format, CultureInfo.GetCultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedText, text);
        }

        [Test]
        public void Equals_ToNull_ReturnFalse()
        {
            // Setup
            var probability = new Probability(5.186e-9);

            // Call
            bool isEqual = probability.Equals(null);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [Test]
        public void Equals_ToSameInstance_ReturnTrue()
        {
            // Setup
            var probability = new Probability(5.186e-9);

            // Call
            bool isEqual = probability.Equals(probability);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [Test]
        public void Equals_ToSameObjectInstance_ReturnTrue()
        {
            // Setup
            var probability = new Probability(5.186e-9);

            // Call
            bool isEqual = probability.Equals((object)probability);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [Test]
        public void Equals_ObjectOfSomeOtherType_ReturnFalse()
        {
            // Setup
            var probability = new Probability(1);
            var someOtherObject = new object();

            // Call
            bool isEqual1 = probability.Equals(someOtherObject);
            bool isEqual2 = someOtherObject.Equals(probability);

            // Assert
            Assert.IsFalse(isEqual1);
            Assert.IsFalse(isEqual2);
        }

        [Test]
        [TestCase(0.234)]
        public void Equals_ToOtherEqualProbability_ReturnTrue(double value)
        {
            // Setup
            var baseProbability = new Probability(0.234);
            object comparisonProbability = new Probability(value);

            // Call
            bool isEqual1 = baseProbability.Equals(comparisonProbability);
            bool isEqual2 = comparisonProbability.Equals(baseProbability);

            // Assert
            Assert.IsTrue(isEqual1);
            Assert.IsTrue(isEqual2);
        }

        [Test]
        public void GetHashCode_TwoEqualInstances_ReturnSameHash()
        {
            // Setup
            var baseProbability = new Probability(0.234);
            object comparisonProbability = new Probability(0.234);

            // Call
            int hash1 = baseProbability.GetHashCode();
            int hash2 = comparisonProbability.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void EqualityOperator_TwoUnequalRoundedValues_ReturnFalse()
        {
            // Setup
            var probability1 = new Probability(1.0/101);
            var probability2 = new Probability(1.0/100);

            // Call
            bool isEqual1 = probability1 == probability2;
            bool isEqual2 = probability2 == probability1;

            // Assert
            Assert.IsFalse(isEqual1);
            Assert.IsFalse(isEqual2);
        }

        [Test]
        public void EqualityOperator_TwoEqualRoundedValues_ReturnFalse()
        {
            // Setup
            var probability1 = new Probability(0.20);
            var probability2 = new Probability(0.20);

            // Call
            bool isEqual1 = probability1 == probability2;
            bool isEqual2 = probability2 == probability1;

            // Assert
            Assert.IsTrue(isEqual1);
            Assert.IsTrue(isEqual2);
        }

        [Test]
        public void InequalityOperator_TwoUnequalRoundedValues_ReturnTrue()
        {
            // Setup
            var probability1 = new Probability(0.23);
            var probability2 = new Probability(0.2);

            // Precondition:
            Assert.IsFalse(probability1.Equals(probability2));

            // Call
            bool isNotEqual1 = probability1 != probability2;
            bool isNotEqual2 = probability2 != probability1;

            // Assert
            Assert.IsTrue(isNotEqual1);
            Assert.IsTrue(isNotEqual2);
        }

        [Test]
        public void InequalityOperator_TwoEqualRoundedValues_ReturnFalse()
        {
            // Setup
            var probability1 = new Probability(0.2);
            var probability2 = new Probability(0.2);

            // Precondition:
            Assert.IsTrue(probability1.Equals(probability2));

            // Call
            bool isNotEqual1 = probability1 != probability2;
            bool isNotEqual2 = probability2 != probability1;

            // Assert
            Assert.IsFalse(isNotEqual1);
            Assert.IsFalse(isNotEqual2);
        }

        [Test]
        public void Equals_ProbabilityEqualToDouble_ReturnTrue()
        {
            // Setup
            var value = 1.0 / 114534.0;
            var probability = new Probability(value);

            // Call
            bool isEqual1 = probability.Equals(value);
            bool isEqual2 = value.Equals(probability);

            // Assert
            Assert.IsTrue(isEqual1);
            Assert.IsTrue(isEqual2);
        }

        [Test]
        public void Equals_ProbabilityNotEqualToDouble_ReturnFalse()
        {
            // Setup
            var value = 1.0 / 123456;
            var probability = new Probability(1.0 / 654321);

            // Call
            bool isEqual1 = probability.Equals(value);
            bool isEqual2 = value.Equals(probability);

            // Assert
            Assert.IsFalse(isEqual1);
            Assert.IsFalse(isEqual2);
        }

        [Test]
        public void Equals_ProbabilityTotallyDifferentFromDouble_ReturnFalse()
        {
            // Setup
            var probability = new Probability(0.23);
            const double otherValue = 4.56;

            // Call
            bool isEqual1 = probability.Equals(otherValue);
            bool isEqual2 = otherValue.Equals(probability);

            // Assert
            Assert.IsFalse(isEqual1);
            Assert.IsFalse(isEqual2);
        }

        [Test]
        public void GetHashCode_ProbabilityEqualToDouble_ReturnSameHashCode()
        {
            // Setup
            const double otherValue = 0.56;
            var probability = new Probability(otherValue);

            // Precondition:
            Assert.IsTrue(otherValue.Equals(probability));

            // Call
            int hash1 = probability.GetHashCode();
            int hash2 = otherValue.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void DoubleEqualityOperator_DoubleIsEqualToProbability_ReturnTrue()
        {
            // Setup
            const double value = 0.234;
            var probability = new Probability(value);

            // Precondition
            Assert.IsTrue(probability.Equals(value));

            // Call
            bool isEqual1 = value == probability;
            bool isEqual2 = probability == value;

            // Assert
            Assert.IsTrue(isEqual1);
            Assert.IsTrue(isEqual2);
        }

        [Test]
        public void DoubleEqualityOperator_DoubleIsNotEqualToProbability_ReturnFalse()
        {
            // Setup
            const double value = 0.234;
            var probability = new Probability(0.21543);

            // Precondition
            Assert.IsFalse(probability.Equals(value));

            // Call
            bool isEqual1 = value == probability;
            bool isEqual2 = probability == value;

            // Assert
            Assert.IsFalse(isEqual1);
            Assert.IsFalse(isEqual2);
        }

        [Test]
        public void DoubleInequalityOperator_DoubleIsEqualToProbability_ReturnFalse()
        {
            // Setup
            const double value = 0.234;
            var probability = new Probability(value);

            // Precondition
            Assert.IsTrue(probability.Equals(value));

            // Call
            bool isEqual1 = value != probability;
            bool isEqual2 = probability != value;

            // Assert
            Assert.IsFalse(isEqual1);
            Assert.IsFalse(isEqual2);
        }

        [Test]
        public void DoubleInequalityOperator_DoubleIsNotEqualToProbability_ReturnTrue()
        {
            // Setup
            const double value = 0.234;
            var probability = new Probability(0.21543);

            // Precondition
            Assert.IsFalse(probability.Equals(value));

            // Call
            bool isEqual1 = value != probability;
            bool isEqual2 = probability != value;

            // Assert
            Assert.IsTrue(isEqual1);
            Assert.IsTrue(isEqual2);
        }

        [Test]
        public void ImplicitConversion_FromProbabilityToDouble_ConvertedValueIsEqual()
        {
            // Setup
            var probability = new Probability(0.2154);

            // Call
            double convertedValue = probability;

            // Assert
            Assert.AreEqual(probability.Value, convertedValue);
        }

        [Test]
        public void ExplicitConversion_FromDoubleToProbability_ConvertedValueIsEqual()
        {
            // Setup
            var returnPeriod = 12345;
            double doubleValue = 1.0 / returnPeriod;

            // Call
            var probability = (Probability)doubleValue;

            // Assert
            Assert.AreEqual(doubleValue, probability.Value);
            Assert.AreEqual(returnPeriod, probability.ReturnPeriod);
        }

        [Test]
        public void OperatorMinus_LeftHasLowestPrecision_ReturnProbabilityWithDifferenceRoundedToLeastNumberOfDecimalPlaces()
        {
            // Setup
            var value1 = new Probability(0.12);
            var value2 = new Probability(0.0456);

            // Call
            Probability diff = value1 - value2;

            // Assert
            Assert.AreEqual(0.0744, diff.Value);
        }

/*
        [Test]
        public void OperatorPlus_LeftHasLowestPrecision_ReturnProbabilityWithSumRoundedToLeastNumberOfDecimalPlaces()
        {
            // Setup
            const int lowestNumberOfDecimalPlaces = 2;

            var value1 = new Probability(lowestNumberOfDecimalPlaces, 1.12);
            var value2 = new Probability(3, 3.456);

            // Call
            Probability diff = value1 + value2;

            // Assert
            Assert.AreEqual(lowestNumberOfDecimalPlaces, diff.NumberOfDecimalPlaces);
            Assert.AreEqual(4.58, diff.Value);
        }
*/

        [Test]
        public void OperatorTimes_ProbabilityTimesDouble_ReturnResultAsProbabilityPreservingNumberOfDecimalPlaces()
        {
            // Setup
            var probability = new Probability(0.234);
            const double doubleValue = 2;

            // Call
            Probability result1 = probability * doubleValue;
            Probability result2 = doubleValue * probability;

            // Assert
            const double expectedValue = 0.468;
            Assert.AreEqual(expectedValue, result1.Value);
            Assert.AreEqual(expectedValue, result2.Value);
        }

        [Test]
        public void OperatorTimes_TwoProbabilitys_MultiplicationIsCommutative()
        {
            // Setup
            var probability1 = new Probability(0.1);
            var probability2 = new Probability(0.22);

            // Call
            Probability result1 = probability1 * probability2;
            Probability result2 = probability2 * probability1;

            // Assert
            Assert.AreEqual(result1.Value, result2.Value);
        }

        [Test]
        public void CompareTo_Null_ReturnsExpectedResult()
        {
            // Setup
            var probability = new Probability(0);

            // Call
            int result = probability.CompareTo(null);

            // Assert
            Assert.AreEqual(1, result);
        }
/*
        [Test]
        public void CompareTo_Object_ThrowsArgumentException()
        {
            // Setup
            var probability = new Probability(0);

            // Call
            TestDelegate call = () => probability.CompareTo(new object());

            // Assert
            TestHelper.AssertThrowsArgumentExceptionAndTestMessage<ArgumentException>(call, "Arg must be double or Probability");
        }*/

        [Test]
        public void CompareTo_Itself_ReturnsZero()
        {
            // Setup
            var probability = new Probability(0);

            // Call
            int result = probability.CompareTo(probability);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        [TestCase(0.123, 0.123, 0)]
        [TestCase(0.456, 0.123, 1)]
        [TestCase(0.123, 0.456, -1)]
        public void CompareTo_ProbabilityToDouble_ReturnsExpectedResult(double probabilityValue, double value,int expectedIndex)
        {
            // Setup
            var probability = new Probability(probabilityValue);

            // Call
            int probabilityResult = probability.CompareTo(value);
            int doubleResult = value.CompareTo(probability);

            // Assert
            Assert.AreEqual(expectedIndex, probabilityResult);
            Assert.AreEqual(-1 * expectedIndex, doubleResult);
        }

        [Test]
        [TestCase(0.123, 0.123, 0)]
        [TestCase(0.456, 0.123, 1)]
        [TestCase(0.123, 0.456, -1)]
        public void CompareTo_ProbabilityToProbability_ReturnsExpectedResult(double probabilityValue, double probabilityValue2,
            int expectedProbabilityIndex)
        {
            // Setup
            var probability1 = new Probability(probabilityValue);
            var probability2 = new Probability(probabilityValue2);

            // Call
            int probability1Result = probability1.CompareTo(probability2);
            int probability2Result = probability2.CompareTo(probability1);

            // Assert
            Assert.AreEqual(expectedProbabilityIndex, probability1Result);
            Assert.AreEqual(-1 * expectedProbabilityIndex, probability2Result);
        }

        [Test]
        [TestCase(0.123, 0.234, 0.456, -1)]
        [TestCase(0.123, 0.123, 0.123, 0)]
        [TestCase(0.456, 0.234, 0.123, 1)]
        public void CompareTo_TransitiveProbability_ReturnsExpectedResult(double probabilityValue1, double probabilityValue2,
            double probabilityValue3, int expectedValue)
        {
            // Setup
            var probability1 = new Probability(probabilityValue1);
            var probability2 = new Probability(probabilityValue2);
            var probability3 = new Probability(probabilityValue3);

            // Call
            int probabilityResult12 = probability1.CompareTo(probability2);
            int probabilityResult23 = probability2.CompareTo(probability3);
            int probabilityResult13 = probability1.CompareTo(probability3);

            // Assert
            Assert.AreEqual(expectedValue, probabilityResult12);
            Assert.AreEqual(expectedValue, probabilityResult23);
            Assert.AreEqual(expectedValue, probabilityResult13);
        }

        [Test]
        [TestCase(0.123, 0.234, 0.456, -1)]
        [TestCase(0.123, 0.123, 0.123, 0)]
        [TestCase(0.456, 0.234, 0.123, 1)]
        public void CompareTo_TransitiveDouble_ReturnsExpectedResult(double probabilityValue, double value2,
            double value3, int expectedValue)
        {
            // Setup
            var probability1 = new Probability(probabilityValue);

            // Call
            int probabilityResult12 = probability1.CompareTo(value2);
            int probabilityResult23 = value2.CompareTo(value3);
            int probabilityResult13 = probability1.CompareTo(value3);

            // Assert
            Assert.AreEqual(expectedValue, probabilityResult12);
            Assert.AreEqual(expectedValue, probabilityResult23);
            Assert.AreEqual(expectedValue, probabilityResult13);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(0.123, 0.123)]
        public void OperatorLess_ProbabilityEqualProbability_ReturnsFalse(double probabilityValue1, double probabilityValue2)
        {
            // Setup
            var probability1 = new Probability(probabilityValue1);
            var probability2 = new Probability(probabilityValue2);

            // Call 
            bool probabilityOneIsLess = probability1 < probability2;
            bool probabilityTwoIsLess = probability2 < probability1;

            // Assert
            Assert.IsFalse(probabilityOneIsLess);
            Assert.IsFalse(probabilityTwoIsLess);
        }

        [Test]
        [TestCase(0.456, 0.123, false)]
        [TestCase(0.123, 0.489, true)]
        public void OperatorLessOrEqual_VaryingDouble_ReturnsExpectedValues(double probabilityValue, double value,
            bool isProbabilityLess)
        {
            // Setup
            var probability = new Probability(probabilityValue);

            // Call 
            bool probabilityIsLess = probability <= value;
            bool doubleIsLess = value <= probability;

            // Assert
            Assert.AreEqual(isProbabilityLess, probabilityIsLess);
            Assert.AreEqual(!isProbabilityLess, doubleIsLess);
        }

        [Test]
        [TestCase(0.123, 0.123)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        public void OperatorLessOrEqual_ProbabilityEqualDouble_ReturnsTrue(double probabilityValue, double value)
        {
            // Setup
            var probability = new Probability(probabilityValue);

            // Call 
            bool probabilityIsLess = probability <= value;
            bool doubleIsLess = value <= probability;

            // Assert
            Assert.IsTrue(probabilityIsLess);
            Assert.IsTrue(doubleIsLess);
        }

        [Test]
        [TestCase(0.489, 0.1237, false)]
        [TestCase(0.489, 0.891, true)]
        public void OperatorLessOrEqual_VaryingProbability_ReturnsExpectedValues(double probabilityValue1, double probabilityValue2,
            bool isProbabilityOneLess)
        {
            // Setup
            var probabilityOne = new Probability(probabilityValue1);
            var probabilityTwo = new Probability(probabilityValue2);

            // Call 
            bool probabilityIsLess = probabilityOne <= probabilityTwo;
            bool isLessDouble = probabilityTwo <= probabilityOne;

            // Assert
            Assert.AreEqual(isProbabilityOneLess, probabilityIsLess);
            Assert.AreEqual(!isProbabilityOneLess, isLessDouble);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(0.123, 0.123)]
        public void OperatorLessOrEqual_ProbabilityEqualProbability_ReturnsTrue(double probabilityValue1, double probabilityValue2)
        {
            // Setup
            var probability1 = new Probability(probabilityValue1);
            var probability2 = new Probability(probabilityValue2);

            // Call 
            bool probabilityOneIsLess = probability1 <= probability2;
            bool probabilityTwoIsLess = probability2 <= probability1;

            // Assert
            Assert.IsTrue(probabilityOneIsLess);
            Assert.IsTrue(probabilityTwoIsLess);
        }

        [Test]
        [TestCase(0.456, 0.123, true)]
        [TestCase(0.123, 0.456, false)]
        public void OperatorGreater_VaryingDouble_ReturnsExpectedValues(double probabilityValue, double value,
            bool isProbabilityGreater)
        {
            // Setup
            var probability = new Probability(probabilityValue);

            // Call 
            bool probabilityIsGreater = probability > value;
            bool doubleIsGreater = value > probability;

            // Assert
            Assert.AreEqual(isProbabilityGreater, probabilityIsGreater);
            Assert.AreEqual(!isProbabilityGreater, doubleIsGreater);
        }

        [Test]
        [TestCase(0.123, 0.123)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        public void OperatorGreater_ProbabilityEqualDouble_ReturnsFalse(double probabilityValue, double value)
        {
            // Setup
            var probability = new Probability(probabilityValue);

            // Call 
            bool probabilityIsLess = probability > value;
            bool doubleIsLess = value > probability;

            // Assert
            Assert.IsFalse(probabilityIsLess);
            Assert.IsFalse(doubleIsLess);
        }

        [Test]
        [TestCase(0.4856, 0.126, true)]
        [TestCase(0.126, 0.786, false)]
        public void OperatorGreater_VaryingProbability_ReturnsExpectedValues(double probabilityValue1, double probabilityValue2,
            bool isProbabilityOneGreater)
        {
            // Setup
            var probabilityOne = new Probability(probabilityValue1);
            var probabilityTwo = new Probability(probabilityValue2);

            // Call 
            bool probabilityOneIsGreater = probabilityOne > probabilityTwo;
            bool probabilityTwoIsGreater = probabilityTwo > probabilityOne;

            // Assert
            Assert.AreEqual(isProbabilityOneGreater, probabilityOneIsGreater);
            Assert.AreEqual(!isProbabilityOneGreater, probabilityTwoIsGreater);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(0.123, 0.123)]
        public void OperatorGreater_ProbabilityEqualProbability_ReturnsFalse(double probabilityValue1, double probabilityValue2)
        {
            // Setup
            var probability1 = new Probability(probabilityValue1);
            var probability2 = new Probability(probabilityValue2);

            // Call 
            bool probabilityOneIsGreater = probability1 > probability2;
            bool probabilityTwoIsGreater = probability2 > probability1;

            // Assert
            Assert.IsFalse(probabilityOneIsGreater);
            Assert.IsFalse(probabilityTwoIsGreater);
        }

        [Test]
        [TestCase(0.123, 0.456, false)]
        [TestCase(0.456, 0.123, true)]
        public void OperatorGreaterOrEqual_VaryingDouble_ReturnsExpectedValues(double probabilityValue, double value,
            bool isProbabilityGreater)
        {
            // Setup
            var probability = new Probability(probabilityValue);

            // Call 
            bool probabilityIsGreater = probability >= value;
            bool doubleIsGreater = value >= probability;

            // Assert
            Assert.AreEqual(isProbabilityGreater, probabilityIsGreater);
            Assert.AreEqual(!isProbabilityGreater, doubleIsGreater);
        }

        [Test]
        [TestCase(0.123, 0.123)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        public void OperatorGreaterOrEqual_ProbabilityEqualDouble_ReturnsTrue(double probabilityValue, double value)
        {
            // Setup
            var probability = new Probability(probabilityValue);

            // Call 
            bool probabilityIsLess = probability >= value;
            bool doubleIsLess = value >= probability;

            // Assert
            Assert.IsTrue(probabilityIsLess);
            Assert.IsTrue(doubleIsLess);
        }

        [Test]
        [TestCase(0.456, 0.123, true)]
        [TestCase(0.123, 0.456, false)]
        public void OperatorGreaterOrEqual_VaryingProbability_ReturnsExpectedValues(double probabilityValue1, double probabilityValue2,
            bool isProbabilityOneGreater)
        {
            // Setup
            var probabilityOne = new Probability(probabilityValue1);
            var probabilityTwo = new Probability(probabilityValue2);

            // Call 
            bool probabilityOneIsGreater = probabilityOne >= probabilityTwo;
            bool probabilityTwoIsGreater = probabilityTwo >= probabilityOne;

            // Assert
            Assert.AreEqual(isProbabilityOneGreater, probabilityOneIsGreater);
            Assert.AreEqual(!isProbabilityOneGreater, probabilityTwoIsGreater);
        }

        [Test]
        [TestCase(0.123, 0.123)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        public void OperatorGreaterOrEqual_ProbabilityEqualProbability_ReturnsFalse(double probabilityValue1, double probabilityValue2)
        {
            // Setup
            var probability1 = new Probability(probabilityValue1);
            var probability2 = new Probability(probabilityValue2);

            // Call 
            bool probabilityOneIsGreater = probability1 >= probability2;
            bool probabilityTwoIsGreater = probability2 >= probability1;

            // Assert
            Assert.IsTrue(probabilityOneIsGreater);
            Assert.IsTrue(probabilityTwoIsGreater);
        }
    }
}