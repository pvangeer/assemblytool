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
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.CalculationResults;
using AssemblyTool.Kernel.ErrorHandling;
using System;
using System.ComponentModel;
using System.Linq;
using AssemblyTool.Kernel.Categories;

namespace AssemblyTool.Kernel.Assembly
{
    public class FailureMechanismSectionAssemblyCalculator : IFailureMechanismSectionAssemblyCalculator
    {
        public FailureMechanismSectionAssemblyCalculator() { }

        #region Simple assessment

        /// <summary>
        /// This method implements WBI-0E-1 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/>.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentDirectFailureMechanisms(SimpleCalculationResult result)
        {
            switch (result)
            {
                case SimpleCalculationResult.NVT:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.None);
                case SimpleCalculationResult.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                case SimpleCalculationResult.VB:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// This method implements WBI-0E-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentIndirectFailureMechanisms(SimpleCalculationResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method implements WBI-0E-3 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/>.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentDirectFailureMechanismsRelevanceOnly(SimpleCalculationResultValidityOnly result)
        {
            switch (result)
            {
                case SimpleCalculationResultValidityOnly.NVT:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.None);
                case SimpleCalculationResultValidityOnly.WVT:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        #endregion

        #region DetailedAssessment

        /// <summary>
        /// This method implements WBI-0G-1 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/>.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromResult(DetailedCalculationResult result)
        {
            switch (result)
            {
                case DetailedCalculationResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                case DetailedCalculationResult.V:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
                case DetailedCalculationResult.VN:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// This method implements WBI-0G-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentIndirectFailureMechanismsFromResult(DetailedCalculationResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method implements WBI-0G-3 from the functional design. Is calculates a <seealso cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/>.
        /// </summary>
        /// <param name="probability">The calculated probability.</param>
        /// <param name="categories">The list of categories for this failure mechanism obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> equals null or is an empty list.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> does not contain a category that encloses the specified <see cref="probability"/>.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromProbability(Probability probability, FailureMechanismSectionCategory[] categories)
        {
            if (categories == null || categories.Length == 0)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            var resultCategory = categories.FirstOrDefault(c => c.LowerBoundary <= probability && c.UpperBoundary >= probability);
            if (resultCategory == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.NoMatchingCategory);
            }

            return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultCategory.CategoryGroup);
        }

        /// <summary>
        /// WBI-0G-4. This method differs from the FO, since we (Ringtoets) register the results differently.
        /// </summary>
        /// <param name="calculationResults">Calculation results for all category boundaries for this failure mechanism.</param>
        /// <returns></returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when an impossible combination of qualitative results was specified (the result for a boundary between two high classes is positive, whereas the result at the boundary between two lower classes was not.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromCategoryBoundaries(DetailedCategoryBoundariesCalculationResult calculationResults)
        {
            if (calculationResults.ResultItoII == DetailedCalculationResult.V)
            {
                if (calculationResults.ResultIItoIII == DetailedCalculationResult.VN ||
                    calculationResults.ResultIIItoIV == DetailedCalculationResult.VN ||
                    calculationResults.ResultIVtoV == DetailedCalculationResult.VN ||
                    calculationResults.ResultVtoVI == DetailedCalculationResult.VN)
                {
                    throw new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination);
                }
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
            }

            if (calculationResults.ResultIItoIII == DetailedCalculationResult.V)
            {
                if (calculationResults.ResultIIItoIV == DetailedCalculationResult.VN ||
                    calculationResults.ResultIVtoV == DetailedCalculationResult.VN ||
                    calculationResults.ResultVtoVI == DetailedCalculationResult.VN)
                {
                    throw new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination);
                }
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
            }

            if (calculationResults.ResultIIItoIV == DetailedCalculationResult.V)
            {
                if (calculationResults.ResultIVtoV == DetailedCalculationResult.VN ||
                    calculationResults.ResultVtoVI == DetailedCalculationResult.VN)
                {
                    throw new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination);
                }
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIIv);
            }

            if (calculationResults.ResultIVtoV == DetailedCalculationResult.V)
            {
                if (calculationResults.ResultVtoVI == DetailedCalculationResult.VN)
                {
                    throw new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination);
                }
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IVv);
            }

            if (calculationResults.ResultVtoVI == DetailedCalculationResult.V)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
            }

            if (calculationResults.ResultVtoVI == DetailedCalculationResult.VN)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIv);
            }

            return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
        }

        /// <summary>
        /// This method implements WBI-0G-5 from the functional design. Is calculates a <seealso cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/>.
        /// </summary>
        /// <param name="probability">The calculated probability.</param>
        /// <param name="categories">The list of categories for this failure mechanism obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> equals null or is an empty list.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> does not contain a category that encloses the specified <see cref="probability"/>.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromProbabilityWithLengthFactor(Probability probability, FailureMechanismSectionCategory[] categories)
        {
            if (categories == null || categories.Length == 0)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            var resultCategory = categories.FirstOrDefault(c => c.LowerBoundary <= probability && c.UpperBoundary >= probability);
            if (resultCategory == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.NoMatchingCategory);
            }

            return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultCategory.CategoryGroup);
        }
        #endregion

        #region TailorMade assessment

        /// <summary>
        /// This method implements WBI-0T-1 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 4.
        /// </summary>
        /// <param name="result">The regustered qualitative result.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromResult(TailorMadeCalculationResult result)
        {
            switch (result)
            {
                case TailorMadeCalculationResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                case TailorMadeCalculationResult.V:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
                case TailorMadeCalculationResult.VN:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
                case TailorMadeCalculationResult.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// This method implements WBI-0T-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentIndirectFailureMechanismsFromResult(TailorMadeCalculationResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method implements WBI-0T-3 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 1 and 2.
        /// </summary>
        /// <param name="result">The specified tailor made calculation result.</param>
        /// <param name="categories">The categories for this failure mechanisms obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/>.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when the <see cref="result"/> equals null.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromProbability(TailorMadeProbabilityCalculationResult result, FailureMechanismSectionCategory[] categories)
        {
            if (result == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            switch (result.CalculationResultGroup)
            {
                case TailorMadeProbabilityCalculationResultGroup.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                case TailorMadeProbabilityCalculationResultGroup.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                case TailorMadeProbabilityCalculationResultGroup.Probability:
                    return DetailedAssessmentDirectFailureMechanismsFromProbability(result.Probability, categories);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// This method implements WBI-0T-4 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 3.
        /// </summary>
        /// <param name="result"></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromCategoryResult(TailorMadeCategoryCalculationResult result)
        {
            switch (result)
            {
                case TailorMadeCategoryCalculationResult.Iv:
                case TailorMadeCategoryCalculationResult.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                case TailorMadeCategoryCalculationResult.IIv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
                case TailorMadeCategoryCalculationResult.IIIv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIIv);
                case TailorMadeCategoryCalculationResult.IVv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IVv);
                case TailorMadeCategoryCalculationResult.Vv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
                case TailorMadeCategoryCalculationResult.VIv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIv);
                case TailorMadeCategoryCalculationResult.VIIv:
                case TailorMadeCategoryCalculationResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
        #endregion

        /// <summary>
        /// This method implements WBI-0T-5 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms piping and slope stability.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated.</param>
        /// <param name="categories">The list of categories for this failure mechanism that is used in case of a probability result, obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when the <see cref="result"/> equals null.</exception>
        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromProbabilityIncludingLength(TailorMadeProbabilityCalculationResult result, FailureMechanismSectionCategory[] categories)
        {
            if (result == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            switch (result.CalculationResultGroup)
            {
                case TailorMadeProbabilityCalculationResultGroup.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                case TailorMadeProbabilityCalculationResultGroup.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                case TailorMadeProbabilityCalculationResultGroup.Probability:
                    return DetailedAssessmentDirectFailureMechanismsFromProbability(result.Probability, categories);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        #region Combined over failure mechanism section

        /// <summary>
        /// WBI-0A-1
        /// </summary>
        /// <param name="resultSimpleAssessment"></param>
        /// <param name="resultDetailedAssessment"></param>
        /// <param name="resultTailorMadeAssessment"></param>
        /// <returns></returns>
        public CalculationOutput<FailureMechanismSectionCategoryGroup>CombinedAssessmentFromFailureMechanismSectionResults(
                FailureMechanismSectionCategoryGroup resultSimpleAssessment,
                FailureMechanismSectionCategoryGroup resultDetailedAssessment,
                FailureMechanismSectionCategoryGroup resultTailorMadeAssessment)
        {
            if (resultTailorMadeAssessment != FailureMechanismSectionCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultTailorMadeAssessment);
            }
            if (resultDetailedAssessment != FailureMechanismSectionCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultDetailedAssessment);
            }
            if (resultSimpleAssessment != FailureMechanismSectionCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultSimpleAssessment);
            }

            return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
        }

        #endregion
    }
}
