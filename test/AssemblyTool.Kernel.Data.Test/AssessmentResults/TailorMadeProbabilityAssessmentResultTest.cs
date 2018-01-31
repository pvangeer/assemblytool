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

using AssemblyTool.Kernel.Data.AssessmentResults;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Data.Test.AssessmentResults
{
    [TestFixture]
    public class TailorMadeProbabilityAssessmentResultTest
    {
        [Test]
        [TestCase(TailorMadeProbabilityAssessmentResultGroup.FV)]
        [TestCase(TailorMadeProbabilityAssessmentResultGroup.NGO)]
        public void EnumConstructorPassesInputCorrectly(TailorMadeProbabilityAssessmentResultGroup resultGroup)
        {
            var probabilityAssessmentResult = new TailorMadeProbabilityAssessmentResult(resultGroup);

            Assert.AreEqual(resultGroup,probabilityAssessmentResult.AssessmentResultGroup);
            Assert.AreEqual(default(Probability),probabilityAssessmentResult.Probability);
        }

        [Test]
        public void EnumConstructorThrowsOnWrongEnumValue()
        {
            try
            {
                var probabilityAssessmentResult = new TailorMadeProbabilityAssessmentResult(TailorMadeProbabilityAssessmentResultGroup.Probability);
                Assert.Fail("Exception was expoected.");
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(ErrorCode.NoProbabilityAllowedInConstructor,e.Code);
            }
        }

        [Test]
        public void ProbabilityConstructorPassesInputCorrectly()
        {
            var probability = (Probability) 0.01;
            var probabilityAssessmentResult = new TailorMadeProbabilityAssessmentResult(probability);

            Assert.AreEqual(TailorMadeProbabilityAssessmentResultGroup.Probability, probabilityAssessmentResult.AssessmentResultGroup);
            Assert.AreEqual(probability, probabilityAssessmentResult.Probability);
        }
    }
}
