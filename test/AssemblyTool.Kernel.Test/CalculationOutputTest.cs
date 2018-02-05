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

using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test
{
    [TestFixture]
    public class CalculationOutputTest
    {
        [Test]
        public void ConstructorPassesResultCorrectly()
        {
            var result = new TestResult(){SomeInt = 5};
            var output = new CalculationOutput<TestResult>(result);

            Assert.IsNotNull(output);
            Assert.IsNotNull(output.Result);
            Assert.IsNotNull(output.WarningMessages);
            Assert.IsEmpty(output.WarningMessages);
            Assert.IsInstanceOf<TestResult>(output.Result);
            Assert.AreEqual(result,output.Result);
        }

        [Test]
        public void ConstructorPassesResultWithWarningsCorrectly()
        {
            var result = new TestResult() { SomeInt = 5 };
            var warnings = new[] { WarningMessage.CorrectedSectionSpecificNValue };
            var output = new CalculationOutput<TestResult>(result,warnings);

            Assert.IsNotNull(output);
            Assert.IsNotNull(output.Result);
            Assert.IsNotNull(output.WarningMessages);
            Assert.AreEqual(warnings,output.WarningMessages);
            Assert.IsInstanceOf<TestResult>(output.Result);
            Assert.AreEqual(result, output.Result);
        }
    }

    public class TestResult
    {
        public int SomeInt { get; set; }
    }
}
