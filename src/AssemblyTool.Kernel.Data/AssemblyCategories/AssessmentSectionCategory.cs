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

namespace AssemblyTool.Kernel.Data.AssemblyCategories
{
    public class AssessmentSectionCategory : CategoryBase<AssessmentSectionAssemblyCategoryGroup>
    {
        /// <summary>
        /// Creates a new instance of <see cref="AssessmentSectionCategory"/>.
        /// </summary>
        /// <param name="categoryGroup">The input to calculate the derived macro stability inwards input.</param>
        /// <param name="lowerBoundary">The input to calculate the derived macro stability inwards input.</param>
        /// <param name="upperBoundary">The input to calculate the derived macro stability inwards input.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundary"/> is not a valid probability.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="upperBoundary"/> is not a valid probability.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundary"/> exceeds <paramref name="upperBoundary"/>.</exception>
        public AssessmentSectionCategory(AssessmentSectionAssemblyCategoryGroup categoryGroup, Probability lowerBoundary, Probability upperBoundary) 
            : base(categoryGroup,lowerBoundary,upperBoundary)
        {
        }
    }
}
