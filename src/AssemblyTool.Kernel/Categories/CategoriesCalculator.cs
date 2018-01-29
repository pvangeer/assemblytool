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

using System.Collections.Generic;
using AssemblyTool.Kernel.CalculatorInput;
using AssemblyTool.Kernel.CategoriesOutput;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel
{
    public static class CategoriesCalculator
    {
        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on assessment section level. 
        /// This method implements "WBI-2-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard and a lower boundary standard.</param>
        /// <returns>A collection of <see cref="AssessmentSectionCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        public static CalculationOutput<AssessmentSectionCategoriesOutput[]> CalculateAssessmentSectionCategories(CalculateAssessmentSectionCategoriesInput input)
        {
            try
            {
                var aPlusToA = 1 / 30.0 * input.SignalingStandard;
                var cToD = 30 * input.LowerBoundaryStandard;

                var categories =  new[]
                {
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.APlus,(Probability)0,aPlusToA),
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.A,aPlusToA, input.SignalingStandard),
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.B,input.SignalingStandard, input.LowerBoundaryStandard),
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.C,input.LowerBoundaryStandard, cToD),
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.D, cToD, (Probability)1),
                };

                return new CalculationOutput<AssessmentSectionCategoriesOutput[]>(categories);
            }
            catch (AssemblyToolKernelException e)
            {
                return new CalculationOutput<AssessmentSectionCategoriesOutput[]>(e);
            }
        }

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-1-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        public static CalculationOutput<FailureMechanismCategoriesOutput[]> CalculateFailureMechanismCategories(CalculateFailureMechanismCategoriesInput input)
        {
            try
            {
                var iToII = 1 / 30.0 * input.ProbabilityDistributionFactor * input.SignalingStandard;
                var iItoIII = input.ProbabilityDistributionFactor * input.SignalingStandard;
                var iIItoIV = input.ProbabilityDistributionFactor * input.LowerBoundaryStandard;

                var categories = new[]
                {
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.It,(Probability)0,iToII),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIt,iToII, iItoIII),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIIt,iItoIII, iIItoIV),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IVt,iIItoIV, input.LowerBoundaryStandard),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.Vt, input.LowerBoundaryStandard, 30 * input.LowerBoundaryStandard),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.VIt, 30 * input.LowerBoundaryStandard, (Probability)1),
                };

                return new CalculationOutput<FailureMechanismCategoriesOutput[]>(categories);
            }
            catch (AssemblyToolKernelException e)
            {
                return new CalculationOutput<FailureMechanismCategoriesOutput[]>(e);
            }
        }

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-0-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor and an N-value that takes the length-effect into account.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        public static CalculationOutput<FailureMechanismSectionCategoriesOutput[]> CalculateFailureMechanismSectionCategories(CalculateFailureMechanismSectionCategoriesInput input)
        {
            try
            {
                Probability iToII = (Probability)(1 / 30.0 * input.ProbabilityDistributionFactor * input.SignalingStandard / input.NValue);
                Probability iItoIII = (Probability)(input.ProbabilityDistributionFactor * input.SignalingStandard / input.NValue);
                Probability iIItoIV = (Probability)(input.ProbabilityDistributionFactor * input.LowerBoundaryStandard / input.NValue);

                var categories = new[]
                {
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Iv,(Probability)0,iToII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIv,iToII, iItoIII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIIv,iItoIII, iIItoIV),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IVv,iIItoIV, input.LowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Vv, input.LowerBoundaryStandard, 30 * input.LowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.VIv, 30 * input.LowerBoundaryStandard, (Probability)1),
                };

                return new CalculationOutput<FailureMechanismSectionCategoriesOutput[]>(categories);
            }
            catch (AssemblyToolKernelException e)
            {
                return new CalculationOutput<FailureMechanismSectionCategoriesOutput[]>(e);
            }
        }

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-0-2" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor and an N-value that takes the length-effect into account.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        public static CalculationOutput<FailureMechanismSectionCategoriesOutput[]> CalculateGeotechnicFailureMechanismSectionCategories(CalculateFailureMechanismSectionCategoriesInput input)
        {
            try
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
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Iv,(Probability)0,iToII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIv,iToII, iItoIII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIIv,iItoIII, iIItoIV),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IVv,iIItoIV, input.LowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Vv, input.LowerBoundaryStandard, 30 * input.LowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.VIv, 30 * input.LowerBoundaryStandard, (Probability)1),
                };
                return new CalculationOutput<FailureMechanismSectionCategoriesOutput[]>(categories,warnings.ToArray());
            }
            catch (AssemblyToolKernelException e)
            {
                return new CalculationOutput<FailureMechanismSectionCategoriesOutput[]>(e);
            }
        }
    }
}
