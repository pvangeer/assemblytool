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

using AssemblyTool.Kernel.Data.AssemblyCategories;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Data.Test.AssemblyCategories
{
    [TestFixture]
    public class AssessmentSectionCategoryTest
    {
        [Test]
        public void ConstructorCallsBaseCorrect()
        {
            var upperBoundary = (Probability)(1 / 100.0);
            var category = AssessmentSectionAssemblyCategoryGroup.B;
            var lowerBoundary = (Probability)(1 / 1000.0);

            var output = new AssessmentSectionCategory(category, lowerBoundary, upperBoundary);
            Assert.IsNotNull(output);
            Assert.AreEqual(category,output.CategoryGroup);
            Assert.AreEqual(lowerBoundary, output.LowerBoundary);
            Assert.AreEqual(upperBoundary, output.UpperBoundary);
        }
    }
}
