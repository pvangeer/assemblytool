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

namespace AssemblyTool.Kernel.Data.AssemblyCategories
{
    /// <summary>
    /// Result of assembly methods that return both an estimated probability as well as a <see cref="FailureMechanismSectionCategoryGroup"/>.
    /// </summary>
    public class FailureMechanismSectionAssemblyCategoryResult
    {
        public FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup categoryGroup, Probability estimatedProbabilityOfFailure)
        {
            CategoryGroup = categoryGroup;
            EstimatedProbabilityOfFailure = estimatedProbabilityOfFailure;
        }

        /// <summary>
        /// The <see cref="FailureMechanismSectionCategoryGroup"/> as a result of assembly.
        /// </summary>
        public FailureMechanismSectionCategoryGroup CategoryGroup { get; }

        /// <summary>
        /// The estimated probability of failure as a result of assembly.
        /// </summary>
        public Probability EstimatedProbabilityOfFailure { get; }
    }
}