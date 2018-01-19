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

using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.CalculatorInput
{
    public class CalculateAssessmentSectionCategoriesInput
    {
        /// <summary>
        /// This class specifies the input for a calculation with the <seealso cref="CategoriesCalculator.CalculateAssessmentSectionCategories"/> method.
        /// </summary>
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> has a higher probability than <paramref name="lowerBoundaryStandard"/>.</exception>
        public CalculateAssessmentSectionCategoriesInput(Probability signalingStandard, Probability lowerBoundaryStandard)
        {
            ValidateStandards(signalingStandard,lowerBoundaryStandard);
            SignalingStandard = signalingStandard;
            LowerBoundaryStandard = lowerBoundaryStandard;
        }

        /// <summary>
        /// Lower boundary standard is the legal standard/probability for an assessment section. It should not fail under these conditions.
        /// </summary>
        public Probability LowerBoundaryStandard { get; }

        /// <summary>
        /// Signaling probability standard is the legal standard for an assessment section for which the ministry should be updated in case a section does not meet the standard.
        /// </summary>
        public Probability SignalingStandard { get; }

        /// <summary>
        /// Validates the lower and upper probabilities.
        /// </summary>
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="signalingStandard"/> is not a valid probability</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="lowerBoundaryStandard"/> is not a valid probability</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="signalingStandard"/> exceeds <paramref name="lowerBoundaryStandard"/></exception>
        private static void ValidateStandards(Probability signalingStandard, Probability lowerBoundaryStandard)
        {
            if (signalingStandard > lowerBoundaryStandard)
            {
                throw new AssemblyToolKernelException(ErrorCode.SignallingStandardExceedsLowerBoundary);
            }
        }
    }
}
