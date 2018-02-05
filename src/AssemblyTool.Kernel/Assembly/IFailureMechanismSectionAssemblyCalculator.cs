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
using System.ComponentModel;
using AssemblyTool.Kernel.Categories;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.CalculationResults;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Assembly
{
    public interface IFailureMechanismSectionAssemblyCalculator
    {
        /// <summary>
        /// This method implements WBI-0E-1 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/>.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentDirectFailureMechanisms(SimpleCalculationResult result);

        /// <summary>
        /// This method implements WBI-0E-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentIndirectFailureMechanisms(SimpleCalculationResult result);

        /// <summary>
        /// This method implements WBI-0E-3 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/>.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentDirectFailureMechanismsRelevanceOnly(SimpleCalculationResultValidityOnly result);

        /// <summary>
        /// This method implements WBI-0G-1 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/>.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromResult(DetailedCalculationResult result);

        /// <summary>
        /// This method implements WBI-0G-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentIndirectFailureMechanismsFromResult(DetailedCalculationResult result);

        /// <summary>
        /// This method implements WBI-0G-3 from the functional design. Is calculates a <seealso cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/>.
        /// </summary>
        /// <param name="probability">The calculated probability.</param>
        /// <param name="categories">The list of categories for this failure mechanism obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> equals null or is an empty list.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> does not contain a category that encloses the specified <see cref="probability"/>.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromProbability(Probability probability, FailureMechanismSectionCategory[] categories);

        /// <summary>
        /// WBI-0G-4. This method differs from the FO, since we (Ringtoets) register the results differently.
        /// </summary>
        /// <param name="calculationResults">Calculation results for all category boundaries for this failure mechanism.</param>
        /// <returns></returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when an impossible combination of qualitative results was specified (the result for a boundary between two high classes is positive, whereas the result at the boundary between two lower classes was not.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromCategoryBoundaries(DetailedCategoryBoundariesCalculationResult calculationResults);

        /// <summary>
        /// This method implements WBI-0G-5 from the functional design. Is calculates a <seealso cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/>.
        /// </summary>
        /// <param name="probability">The calculated probability.</param>
        /// <param name="categories">The list of categories for this failure mechanism obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> equals null or is an empty list.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="categories"/> does not contain a category that encloses the specified <see cref="probability"/>.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromProbabilityWithLengthFactor(Probability probability, FailureMechanismSectionCategory[] categories);

        /// <summary>
        /// This method implements WBI-0T-1 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 4.
        /// </summary>
        /// <param name="result">The regustered qualitative result.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromResult(TailorMadeCalculationResult result);

        /// <summary>
        /// This method implements WBI-0T-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentIndirectFailureMechanismsFromResult(TailorMadeCalculationResult result);

        /// <summary>
        /// This method implements WBI-0T-3 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 1 and 2.
        /// </summary>
        /// <param name="result">The specified tailor made calculation result.</param>
        /// <param name="categories">The categories for this failure mechanisms obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/>.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when the <see cref="result"/> equals null.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromProbability(TailorMadeProbabilityCalculationResult result, FailureMechanismSectionCategory[] categories);

        /// <summary>
        /// This method implements WBI-0T-4 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 3.
        /// </summary>
        /// <param name="result"></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromCategoryResult(TailorMadeCategoryCalculationResult result);

        /// <summary>
        /// This method implements WBI-0T-5 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms piping and slope stability.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated.</param>
        /// <param name="categories">The list of categories for this failure mechanism that is used in case of a probability result, obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/></param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when the <see cref="result"/> equals null.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromProbabilityIncludingLength(TailorMadeProbabilityCalculationResult result, FailureMechanismSectionCategory[] categories);

        /// <summary>
        /// WBI-0A-1
        /// </summary>
        /// <param name="resultSimpleAssessment"></param>
        /// <param name="resultDetailedAssessment"></param>
        /// <param name="resultTailorMadeAssessment"></param>
        /// <returns></returns>
        CalculationOutput<FailureMechanismSectionCategoryGroup>CombinedAssessmentFromFailureMechanismSectionResults(
            FailureMechanismSectionCategoryGroup resultSimpleAssessment,
            FailureMechanismSectionCategoryGroup resultDetailedAssessment,
            FailureMechanismSectionCategoryGroup resultTailorMadeAssessment);
    }
}