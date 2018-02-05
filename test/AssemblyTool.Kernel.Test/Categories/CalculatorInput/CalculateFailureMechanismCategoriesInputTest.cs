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

using AssemblyTool.Kernel.Categories.CalculatorInput;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test.Categories.CalculatorInput
{
    [TestFixture]
    public class CalculateFailureMechanismCategoriesInputTest
    {
        [Test]
        [TestCase(-1.0, ErrorCode.ValueBelowZero)]
        [TestCase(2.4, ErrorCode.ValueAboveOne)]
        [TestCase(double.NaN, ErrorCode.ValueIsNaN)]
        public void ConstructrValidatesProbabilityDistributionFactor(double probabilityDistributionFactor,
            ErrorCode expectedInnerExceptionCode)
        {
            try
            {
                var input = new CalculateFailureMechanismCategoriesInput((Probability) 0.123, (Probability) 0.456,probabilityDistributionFactor);
                Assert.Fail("Expected exception");
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(1,e.Code.Length);
                Assert.AreEqual(ErrorCode.InvalidProbabilityDistributionFactor, e.Code[0]);
                Assert.IsNotNull(e.InnerException);
                Assert.IsInstanceOf<AssemblyToolKernelException>(e.InnerException);
                var innerException = (AssemblyToolKernelException) e.InnerException;
                Assert.AreEqual(1,innerException.Code.Length);
                Assert.AreEqual(expectedInnerExceptionCode, innerException.Code[0]);
            }
        }

        [Test]
        public void ConstructorPassesAllPropertiesCorrectly()
        {
            var probabilityDistributionFactor = 0.4;
            var signalingStandard = (Probability)0.123;
            var lowerBoundaryStandard = (Probability)0.456;

            var input = new CalculateFailureMechanismCategoriesInput(signalingStandard, lowerBoundaryStandard,probabilityDistributionFactor);

            Assert.AreEqual(signalingStandard, input.SignalingStandard);
            Assert.AreEqual(lowerBoundaryStandard, input.LowerBoundaryStandard);
            Assert.AreEqual(probabilityDistributionFactor, input.ProbabilityDistributionFactor);
        }
    }
}
