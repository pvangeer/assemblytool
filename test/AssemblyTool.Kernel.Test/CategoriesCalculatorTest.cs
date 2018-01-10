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
using System.Linq;
using System.Text;
using AssemblyTool.Kernel.Categories;
using AssemblyTool.Kernel.Data;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test
{
    [TestFixture]
    public class CategoriesCalculatorTest
    {
        [Test]
        public void CalculateAssessmentSectionCategoriesTest()
        {
            var signalingStandard = 1 / 3000.0;
            var lowerBoundaryStandard = 1 / 1000.0;
            var result = CategoriesCalculator.CalculateAssessmentSectionCategories(signalingStandard, lowerBoundaryStandard);

            Assert.AreEqual(5,result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.APlus,firstCategory.Category);
            Assert.AreEqual(0,firstCategory.LowerBoundary);
            Assert.AreEqual(1/30.0*signalingStandard,firstCategory.UpperBoundary);
            var category2 = result[1];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.A, category2.Category);
            Assert.AreEqual(1 / 30.0 * signalingStandard, category2.LowerBoundary);
            Assert.AreEqual(signalingStandard, category2.UpperBoundary);
            var category3 = result[2];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.B, category3.Category);
            Assert.AreEqual(signalingStandard, category3.LowerBoundary);
            Assert.AreEqual(lowerBoundaryStandard, category3.UpperBoundary);
            var category4 = result[3];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.C, category4.Category);
            Assert.AreEqual(lowerBoundaryStandard, category4.LowerBoundary);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category4.UpperBoundary);
            var category5 = result[4];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.D, category5.Category);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.LowerBoundary);
            Assert.AreEqual(1, category5.UpperBoundary);
        }
    }
}
