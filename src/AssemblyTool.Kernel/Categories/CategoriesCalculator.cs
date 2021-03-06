﻿// Copyright (C) Stichting Deltares 2018. All rights reserved.
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

using System.Collections.Generic;
using AssemblyTool.Kernel.Categories.CalculatorInput;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Categories
{
    public class CategoriesCalculator : ICategoriesCalculator
    {
        public CategoriesCalculator() { }

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on assessment section level. 
        /// This method implements "WBI-2-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard and a lower boundary standard.</param>
        /// <returns>A collection of <see cref="AssessmentSectionCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were derived.</exception>
        public CalculationOutput<AssessmentSectionCategory[]> CalculateAssessmentSectionCategories(CalculateAssessmentSectionCategoriesInput input)
        {
            var aPlusToA = 1 / 30.0 * input.SignalingStandard;
            var cToD = 30 * input.LowerBoundaryStandard;

            var categories = new[]
            {
                new AssessmentSectionCategory(AssessmentSectionCategoryGroup.APlus, (Probability) 0, aPlusToA),
                new AssessmentSectionCategory(AssessmentSectionCategoryGroup.A, aPlusToA, input.SignalingStandard),
                new AssessmentSectionCategory(AssessmentSectionCategoryGroup.B, input.SignalingStandard,
                    input.LowerBoundaryStandard),
                new AssessmentSectionCategory(AssessmentSectionCategoryGroup.C, input.LowerBoundaryStandard, cToD),
                new AssessmentSectionCategory(AssessmentSectionCategoryGroup.D, cToD, (Probability) 1),
            };

            return new CalculationOutput<AssessmentSectionCategory[]>(categories);
        }

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-1-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were calculated.</exception>
        public CalculationOutput<FailureMechanismCategory[]> CalculateFailureMechanismCategories(CalculateFailureMechanismCategoriesInput input)
        {
            var iToII = 1 / 30.0 * input.ProbabilityDistributionFactor * input.SignalingStandard;
            var iItoIII = input.ProbabilityDistributionFactor * input.SignalingStandard;
            var iIItoIV = input.ProbabilityDistributionFactor * input.LowerBoundaryStandard;

            var categories = new[]
            {
                new FailureMechanismCategory(FailureMechanismCategoryGroup.It, (Probability) 0, iToII),
                new FailureMechanismCategory(FailureMechanismCategoryGroup.IIt, iToII, iItoIII),
                new FailureMechanismCategory(FailureMechanismCategoryGroup.IIIt, iItoIII, iIItoIV),
                new FailureMechanismCategory(FailureMechanismCategoryGroup.IVt, iIItoIV, input.LowerBoundaryStandard),
                new FailureMechanismCategory(FailureMechanismCategoryGroup.Vt, input.LowerBoundaryStandard,
                    30 * input.LowerBoundaryStandard),
                new FailureMechanismCategory(FailureMechanismCategoryGroup.VIt, 30 * input.LowerBoundaryStandard,
                    (Probability) 1),
            };

            return new CalculationOutput<FailureMechanismCategory[]>(categories);
        }

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-0-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor and an N-value that takes the length-effect into account.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were calculated.</exception>
        public CalculationOutput<FailureMechanismSectionCategory[]> CalculateFailureMechanismSectionCategories(CalculateFailureMechanismSectionCategoriesInput input)
        {
            Probability iToII = (Probability) (1 / 30.0 * input.ProbabilityDistributionFactor * input.SignalingStandard / input.NValue);
            Probability iItoIII = (Probability) (input.ProbabilityDistributionFactor * input.SignalingStandard / input.NValue);
            Probability iIItoIV = (Probability) (input.ProbabilityDistributionFactor * input.LowerBoundaryStandard / input.NValue);

            var categories = new[]
            {
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Iv, (Probability) 0, iToII),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIv, iToII, iItoIII),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIIv, iItoIII, iIItoIV),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IVv, iIItoIV, input.LowerBoundaryStandard),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Vv, input.LowerBoundaryStandard, 30 * input.LowerBoundaryStandard),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.VIv, 30 * input.LowerBoundaryStandard, (Probability) 1)
            };

            return new CalculationOutput<FailureMechanismSectionCategory[]>(categories);
        }

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-0-2" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor and an N-value that takes the length-effect into account.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were calculated.</exception>
        public CalculationOutput<FailureMechanismSectionCategory[]> CalculateGeotechnicFailureMechanismSectionCategories(CalculateFailureMechanismSectionCategoriesInput input)
        {
            var warnings = new List<WarningMessage>();

            var factor = input.ProbabilityDistributionFactor * 10 / input.NValue;
            if (factor > 1)
            {
                warnings.Add(WarningMessage.CorrectedSectionSpecificNValue);
                factor = 1;
            }

            var iToII = 1 / 30.0 * factor * input.SignalingStandard;
            var iItoIII = factor * input.SignalingStandard;
            var iIItoIV = factor * input.LowerBoundaryStandard;

            var categories = new[]
            {
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Iv, (Probability) 0, iToII),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIv, iToII, iItoIII),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIIv, iItoIII, iIItoIV),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IVv, iIItoIV, input.LowerBoundaryStandard),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Vv, input.LowerBoundaryStandard, 30 * input.LowerBoundaryStandard),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.VIv, 30 * input.LowerBoundaryStandard, (Probability) 1)
            };
            return new CalculationOutput<FailureMechanismSectionCategory[]>(categories, warnings.ToArray());
        }
    }
}
