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
using AssemblyTool.Kernel.Data.AssessmentResults;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Data.Test.AssessmentResults
{
    [TestFixture]
    public class DetailedAssessmentCategoryBoundariesResultTest
    {
        [Test]
        public void ConstructorPassesPropertiesCorrectly()
        {
            var iToII = DetailedAssessmentResult.NGO;
            var iIToIII = DetailedAssessmentResult.V;
            var iIIToIV = DetailedAssessmentResult.VN;
            var iVToV = DetailedAssessmentResult.NGO;
            var vToVi = DetailedAssessmentResult.NGO;

            var detailedAssessmentCategoryBoundariesResult = new DetailedAssesmentCategoryBoundariesResult(iToII, iIToIII,iIIToIV, iVToV, vToVi);

            Assert.AreEqual(iToII,detailedAssessmentCategoryBoundariesResult.ResultItoII);
            Assert.AreEqual(iIToIII, detailedAssessmentCategoryBoundariesResult.ResultIItoIII);
            Assert.AreEqual(iIIToIV, detailedAssessmentCategoryBoundariesResult.ResultIIItoIV);
            Assert.AreEqual(iVToV, detailedAssessmentCategoryBoundariesResult.ResultIVtoV);
            Assert.AreEqual(vToVi, detailedAssessmentCategoryBoundariesResult.ResultVtoVI);
        }
    }
}
