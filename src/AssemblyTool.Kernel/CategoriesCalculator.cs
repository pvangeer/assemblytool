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

using System;
using System.Collections.Generic;
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
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <returns>A collection of <see cref="AssessmentSectionCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundaryStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> has a higher probability that <paramref name="lowerBoundaryStandard"/>.</exception>
        public static CalculationOutput<AssessmentSectionCategoriesOutput[]> CalculateAssessmentSectionCategories(Probability signalingStandard, Probability lowerBoundaryStandard)
        {
            try
            {
                ValidateStandards(signalingStandard, lowerBoundaryStandard);

                var aPlusToA = 1 / 30.0 * signalingStandard;
                var cToD = 30 * lowerBoundaryStandard;

                var categories =  new[]
                {
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.APlus,(Probability)0,aPlusToA),
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.A,aPlusToA, signalingStandard),
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.B,signalingStandard, lowerBoundaryStandard),
                    new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.C,lowerBoundaryStandard, cToD),
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
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <param name="probabilityDistributionFactor">The (combined) contribution of this/these failure mechanism(s) to the total probability of failure of the assessment section in the failure probability distribution table.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundaryStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="probabilityDistributionFactor"/> is not in the range [0-1].</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> has a higher probability that <paramref name="lowerBoundaryStandard"/>.</exception>
        public static CalculationOutput<FailureMechanismCategoriesOutput[]> CalculateFailureMechanismCategories(Probability signalingStandard, Probability lowerBoundaryStandard, double probabilityDistributionFactor)
        {
            try
            {
                ValidateStandards(signalingStandard, lowerBoundaryStandard);
                ValidateProbabilityDistributionFactor(probabilityDistributionFactor);

                var iToII = 1 / 30.0 * probabilityDistributionFactor * signalingStandard;
                var iItoIII = probabilityDistributionFactor * signalingStandard;
                var iIItoIV = probabilityDistributionFactor * lowerBoundaryStandard;

                var categories = new[]
                {
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.It,(Probability)0,iToII),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIt,iToII, iItoIII),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIIt,iItoIII, iIItoIV),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IVt,iIItoIV, lowerBoundaryStandard),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.Vt, lowerBoundaryStandard, 30 * lowerBoundaryStandard),
                    new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.VIt, 30 * lowerBoundaryStandard, (Probability)1),
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
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <param name="probabilityDistributionFactor">The (combined) contribution of this/these failure mechanism(s) to the total probability of failure of the assessment section in the failure probability distribution table.</param>
        /// <param name="nValue">The value of N to take into account the length effect.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundaryStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="probabilityDistributionFactor"/> is not in the range [0-1].</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> has a higher probability that <paramref name="lowerBoundaryStandard"/>.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="nValue"/> is smaller than 1 or NaN.<paramref name="lowerBoundaryStandard"/>.</exception>
        public static CalculationOutput<FailureMechanismSectionCategoriesOutput[]> CalculateFailureMechanismSectionCategories(Probability signalingStandard, Probability lowerBoundaryStandard, double probabilityDistributionFactor, double nValue)
        {
            try
            {
                ValidateStandards(signalingStandard, lowerBoundaryStandard);
                ValidateProbabilityDistributionFactor(probabilityDistributionFactor);
                ValidateNValue(nValue);

                Probability iToII = (Probability)(1 / 30.0 * probabilityDistributionFactor * signalingStandard / nValue);
                Probability iItoIII = (Probability)(probabilityDistributionFactor * signalingStandard / nValue);
                Probability iIItoIV = (Probability)(probabilityDistributionFactor * lowerBoundaryStandard / nValue);

                var categories = new[]
                {
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Iv,(Probability)0,iToII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIv,iToII, iItoIII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIIv,iItoIII, iIItoIV),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IVv,iIItoIV, lowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Vv, lowerBoundaryStandard, 30 * lowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.VIv, 30 * lowerBoundaryStandard, (Probability)1),
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
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <param name="probabilityDistributionFactor">The (combined) contribution of this/these failure mechanism(s) to the total probability of failure of the assessment section in the failure probability distribution table.</param>
        /// <param name="nValue">The value of N to take into account the length effect.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategoriesOutput"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundaryStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="probabilityDistributionFactor"/> is not in the range [0-1].</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> has a higher probability that <paramref name="lowerBoundaryStandard"/>.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="nValue"/> is smaller than 1 or NaN.<paramref name="lowerBoundaryStandard"/>.</exception>
        public static CalculationOutput<FailureMechanismSectionCategoriesOutput[]> CalculateGeotechnicFailureMechanismSectionCategories(Probability signalingStandard, Probability lowerBoundaryStandard, double probabilityDistributionFactor, double nValue)
        {
            try
            {
                var warnings = new List<WarningMessage>();

                ValidateStandards(signalingStandard, lowerBoundaryStandard);
                ValidateProbabilityDistributionFactor(probabilityDistributionFactor);
                ValidateNValue(nValue);

                var factor = probabilityDistributionFactor * 10 / nValue;
                if (factor > 1)
                {
                    warnings.Add(WarningMessage.CorrectedSectionSpecificNValue);
                    factor = 1;
                }

                var iToII = 1 / 30.0 * factor * signalingStandard;
                var iItoIII = factor * signalingStandard;
                var iIItoIV = factor * lowerBoundaryStandard;

                var categories = new[]
                {
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Iv,(Probability)0,iToII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIv,iToII, iItoIII),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IIIv,iItoIII, iIItoIV),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.IVv,iIItoIV, lowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.Vv, lowerBoundaryStandard, 30 * lowerBoundaryStandard),
                    new FailureMechanismSectionCategoriesOutput(FailureMechanismSectionAssemblyCategory.VIv, 30 * lowerBoundaryStandard, (Probability)1),
                };
                return new CalculationOutput<FailureMechanismSectionCategoriesOutput[]>(categories,warnings.ToArray());
            }
            catch (AssemblyToolKernelException e)
            {
                return new CalculationOutput<FailureMechanismSectionCategoriesOutput[]>(e);
            }
        }

        /// <summary>
        /// This method validates the specified N - value to take into account the length effect. N needs to be a valid douvle >= 1.
        /// </summary>
        /// <param name="nValue">The N-value that needs to be validated.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="nValue"/> equals NaN</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="nValue"/> is a double smaller than 1.0</exception>
        private static void ValidateNValue(double nValue)
        {
            if (Double.IsNaN(nValue))
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidNValue, new AssemblyToolKernelException(ErrorCode.ValueIsNaN));
            }

            if (nValue < 1)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidNValue,new AssemblyToolKernelException(ErrorCode.ValueBelowOne));
            }
        }

        /// <summary>
        /// Validates the entered probability distrubution factor.
        /// </summary>
        /// <param name="probabilityDistributionFactor">The probability distribution factor to validate</param>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> is NaN</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> is smaller than 0</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> exceeds 1</exception>
        private static void ValidateProbabilityDistributionFactor(double probabilityDistributionFactor)
        {
            if (Double.IsNaN(probabilityDistributionFactor))
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidProbabilityDistributionFactor, new AssemblyToolKernelException(ErrorCode.ValueIsNaN));
            }

            if (probabilityDistributionFactor < 0)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidProbabilityDistributionFactor,new AssemblyToolKernelException(ErrorCode.ValueBelowZero));
            }

            if (probabilityDistributionFactor > 1)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidProbabilityDistributionFactor, new AssemblyToolKernelException(ErrorCode.ValueAboveOne));
            }
        }

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
