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
using NUnit.Framework;

namespace AssemblyTool.Kernel.ErrorHandling.Test
{
    [TestFixture]
    public class ErrorCodeTest
    {
        [Test]
        public void Values_ExpectedValues()
        {
            // Assert
            Assert.AreEqual(13, Enum.GetValues(typeof(ErrorCode)).Length);
            Assert.AreEqual(0, (int)ErrorCode.ValueBelowZero);
            Assert.AreEqual(1, (int)ErrorCode.ValueAboveOne);
            Assert.AreEqual(2, (int)ErrorCode.SignallingStandardExceedsLowerBoundary);
            Assert.AreEqual(3, (int)ErrorCode.InvalidSignalingStandard);
            Assert.AreEqual(4, (int)ErrorCode.InvalidLowerBoundaryStandard);
            Assert.AreEqual(5, (int)ErrorCode.InvalidProbabilityDistributionFactor);
            Assert.AreEqual(6, (int)ErrorCode.ValueIsNaN);
            Assert.AreEqual(7, (int)ErrorCode.CategoryLowerBoundaryExceedsUpperBoundary);
            Assert.AreEqual(8, (int)ErrorCode.ValueBelowOne);
            Assert.AreEqual(9, (int)ErrorCode.InvalidNValue);
            Assert.AreEqual(10, (int)ErrorCode.NoMatchingCategory);
            Assert.AreEqual(11, (int)ErrorCode.ImpossibleResultCombination);
            Assert.AreEqual(12, (int)ErrorCode.NoProbabilityAllowedInConstructor);
        }
    }
}
