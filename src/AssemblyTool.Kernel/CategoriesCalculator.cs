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
using AssemblyTool.Kernel.Services;

namespace AssemblyTool.Kernel
{
    public static class CategoriesCalculator
    {
        /// <summary>
        /// WBI-2-1
        /// </summary>
        /// <returns></returns>
        public static AssessmentSectionCategoriesOutput[] CalculateAssessmentSectionCategories(double signalingStandard, double lowerBoundaryStandard)
        {
            ValidateStandards(signalingStandard, lowerBoundaryStandard);

            var aPlusToA = 1/30.0*signalingStandard;
            var cToD = 30 * lowerBoundaryStandard;

            return new[]
            {
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.APlus,0,aPlusToA),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.A,aPlusToA, signalingStandard),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.B,signalingStandard, lowerBoundaryStandard),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.C,lowerBoundaryStandard, cToD),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.D, cToD, 1), 
            };
        }

        /// <summary>
        /// WBI-1-1
        /// </summary>
        /// <returns></returns>
        public static FailureMechanismCategoriesOutput[] CalculateFailureMechanismCategories(double signalingStandard, double lowerBoundaryStandard, double probabilityDistributionFactor)
        {
            ValidateStandards(signalingStandard, lowerBoundaryStandard);

            var iToII = 1 / 30.0 * probabilityDistributionFactor * signalingStandard;
            var iItoIII = probabilityDistributionFactor * signalingStandard;

            return new[]
            {
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.It,0,iToII),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIt,iToII, iItoIII),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIIt,iItoIII, signalingStandard),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IVt,probabilityDistributionFactor * lowerBoundaryStandard, lowerBoundaryStandard),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.Vt, lowerBoundaryStandard, 30 * lowerBoundaryStandard),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.VIt, 30 * lowerBoundaryStandard, 1),
            };
        }

        private static void ValidateStandards(double signalingStandard, double lowerBoundaryStandard)
        {
            try
            {
                ProbabilityValidator.Validate(signalingStandard);
            }
            catch (ProbabilityValidationException e)
            {
                throw new ProbabilityValidationException(ErrorCode.IncorrectSignalingStandard, e);
            }

            try
            {
                ProbabilityValidator.Validate(lowerBoundaryStandard);
            }
            catch (ProbabilityValidationException e)
            {
                throw new ProbabilityValidationException(ErrorCode.IncorrectLowerBoundaryStandard, e);
            }

            if (signalingStandard > lowerBoundaryStandard)
            {
                throw new ProbabilityValidationException(ErrorCode.SignallingStandardExceedsLowerBoundary);
            }
        }
    }
}
