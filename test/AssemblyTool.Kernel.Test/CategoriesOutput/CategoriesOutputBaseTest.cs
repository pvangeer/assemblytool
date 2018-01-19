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
using System.Collections.Generic;
using System.Text;
using AssemblyTool.Kernel.CategoriesOutput;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test.CategoriesOutput
{
    [TestFixture]
    public class CategoriesOutputBaseTest
    {
        [Test]
        public void ConstructorValidatesBoundaries()
        {
            Probability upperBoundaryStandard = (Probability)0;
            Probability lowerBoundaryStandard = (Probability)(1 / 1000.0);

            try
            {
                var output = new TestOutput(TestCategory.Cat1, lowerBoundaryStandard, upperBoundaryStandard);
                Assert.Fail("Expected error message");
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(ErrorCode.CategoryLowerBoundaryExceedsUpperBoundary, e.Code);
            }
        }

        [Test]
        public void ConstructorPassesPropertiesCorrectly()
        {
            Probability upperBoundary = (Probability)(1 / 1000.0);
            Probability lowerBoundary = (Probability)0;
            var category = TestCategory.Cat2;

            var output = new TestOutput(category, lowerBoundary, upperBoundary);
            Assert.AreEqual(upperBoundary, output.UpperBoundary);
            Assert.AreEqual(lowerBoundary, output.LowerBoundary);
            Assert.AreEqual(category, output.Category);
        }
    }

    public class TestOutput : CategoriesOutputBase<TestCategory>
    {
        public TestOutput(TestCategory category, Probability lowerBoundary, Probability upperBoundary) : base(category, lowerBoundary, upperBoundary)
        {
        }
    }

    public enum TestCategory
    {
        Cat1,
        Cat2
    }
}
