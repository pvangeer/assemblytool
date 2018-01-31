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

using NUnit.Framework;

namespace AssemblyTool.Kernel.ErrorHandling.Test
{
    [TestFixture]
    public class ErrorCodeExtensionsTest
    {
        [Test]
        
        [TestCase(ErrorCode.ValueBelowZero, "The value of this parameter is supposed not to be lower than 0, but it is.")]
        [TestCase(ErrorCode.ValueAboveOne, "The value of this parameter is supposed not to exceed 1, but it does.")]
        [TestCase(ErrorCode.SignallingStandardExceedsLowerBoundary,"The entered signaling standard exceeds the lower boundary. This should bot be the case.")]
        [TestCase(ErrorCode.InvalidSignalingStandard,"The entered signaling standard is not valid. See the innerexception for more details.")]
        [TestCase(ErrorCode.InvalidLowerBoundaryStandard,"The entered lower boundary standard is not valid. See the innerexception for more details.")]
        [TestCase(ErrorCode.InvalidProbabilityDistributionFactor,"The entered probability distribution factor is not valid. See the inner exception for more details.")]
        [TestCase(ErrorCode.ValueIsNaN, "The value of this double equals NaN.")]
        [TestCase(ErrorCode.CategoryLowerBoundaryExceedsUpperBoundary,"The lower boundary (probability) of a category should be lower than the upperboundary (probability), but it is not.")]
        [TestCase(ErrorCode.ValueBelowOne, "Value should be above one (or equal to one), but it is not")]
        [TestCase(ErrorCode.InvalidNValue, "The specified N - value is invalid. See the inner exception for more details.")]
        [TestCase(ErrorCode.NoMatchingCategory,"No category was found that included the specified probability. Possibly the category list is empty or not complete.")]
        [TestCase(ErrorCode.ImpossibleResultCombination,"It is not possible a low category fails at the same time a high category passes.")]
        [TestCase(ErrorCode.NoProbabilityAllowedInConstructor,"The provided result specifies a probability without specifying a probability")]
        public void GetMessageReturnsCorrectMessage(ErrorCode code, string expectedMessage)
        {
            Assert.AreEqual(expectedMessage,code.GetMessage());
        }
    }
}
