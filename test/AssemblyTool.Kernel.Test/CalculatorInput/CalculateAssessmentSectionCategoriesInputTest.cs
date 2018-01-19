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

using AssemblyTool.Kernel.CalculatorInput;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test.CalculatorInput
{
    [TestFixture]
    public class CalculateAssessmentSectionCategoriesInputTest
    {
        [Test]
        public void ConstructorValidatesValidStandardsWrongBoundaries()
        {
            var signalingStandard = (Probability)(1.0 / 1000);
            var lowerBoundaryStandard = (Probability)(1.0 / 3000);

            void TestDelegate()
            {
                var input = new CalculateAssessmentSectionCategoriesInput(signalingStandard, lowerBoundaryStandard);
            }

            Assert.Throws<AssemblyToolKernelException>(TestDelegate, ErrorCode.SignallingStandardExceedsLowerBoundary.GetMessage());
        }

        [Test]
        [TestCase(1/1000.0, 1/3000.0)]
        [TestCase(1 / 100.0, 1 / 100.0)]
        public void ConstructorPassessValidValuesToPropertiesCorrectly(double lowerBoundaryStandardValue, double signalingStandardValue)
        {
            var input = new CalculateAssessmentSectionCategoriesInput((Probability)signalingStandardValue, (Probability)lowerBoundaryStandardValue);
            Assert.AreEqual(signalingStandardValue,input.SignalingStandard.Value);
            Assert.AreEqual(lowerBoundaryStandardValue, input.LowerBoundaryStandard.Value);
        }
    }
}
