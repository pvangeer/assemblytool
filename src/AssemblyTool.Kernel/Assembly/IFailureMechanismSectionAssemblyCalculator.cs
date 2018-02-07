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
using System.Collections.Specialized;
using System.ComponentModel;
using AssemblyTool.Kernel.Assembly.CalculatorInput;
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
        /// This method implements WBI-0E-1 from the functional design. It calculates a <see cref="FailureMechanismSectionAssemblyCategoryResult"/> from the specified <see cref="SimpleCalculationResult"/>.
        /// 
        /// In short, this method returns the following:
        /// * NVT => NotApplicable / 0
        /// * FV => Iv / 0
        /// * VB => VIIv / NaN
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionAssemblyCategoryResult"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> SimpleAssessmentDirectFailureMechanisms(SimpleCalculationResult result);

        /// <summary>
        /// This method implements WBI-0E-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentIndirectFailureMechanisms(SimpleCalculationResult result);

        /// <summary>
        /// This method implements WBI-0E-3 from the functional design. It calculates a <see cref="FailureMechanismSectionAssemblyCategoryResult"/> from the specified <see cref="SimpleCalculationResult"/>.
        /// 
        /// In short, this method returns the following:
        /// * NVT => NotApplicable / 0
        /// * WVT => VIIv / NaN
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionAssemblyCategoryResult"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> SimpleAssessmentDirectFailureMechanisms(SimpleCalculationResultValidityOnly result);

        /// <summary>
        /// This method implements WBI-0G-1 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/>.
        /// 
        /// In short this method does the following:
        /// * NGO => VIIv
        /// * V => IIv
        /// * VN => Vv
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the specified <see cref="result"/> has an invalid enum value.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanisms(DetailedCalculationResult result);

        /// <summary>
        /// This method implements WBI-0G-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="DetailedCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentIndirectFailureMechanisms(DetailedCalculationResult result);

        /// <summary>
        /// This method implements WBI-0G-3 from the functional design. Is calculates a <seealso cref="FailureMechanismSectionAssemblyCategoryResult"/> from the specified <see cref="DetailedCalculationResult"/>. To do so, 
        /// the methods finds the first category that has a lower boundary smaller than or eqaul to the specified probability and an upperboundary greater than or equal to the specified probability.
        /// </summary>
        /// <param name="input">The combined input for this method.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionAssemblyCategoryResult"/>.</returns>
        /// <exception cref="AssemblyToolKernelException"> Thrown when <see cref="input"/> equals null.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="DetailedCalculationInputFromProbability.Categories"/> does not contain a category that encloses the specified <see cref="DetailedCalculationInputFromProbability.Probability"/>.</exception>
        CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> DetailedAssessmentDirectFailureMechanisms(DetailedCalculationInputFromProbability input);

        /// <summary>
        /// This method implements an alternative for WBI-0G-4 from the functional design. It differs from the FO, since Ringtoets registers the results per category boundary instead of the resulting category. Based on a <see cref="DetailedCategoryBoundariesCalculationResult"/>
        /// this method searches for the highest categry boundary that has a <see cref="DetailedCalculationResult.V"/> result. Based on that boundary, the corresponding category is returned
        /// </summary>
        /// <param name="calculationResults">Calculation results for all category boundaries for this failure mechanism.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when an impossible combination of qualitative results was specified (the result for a boundary between two high classes is positive, whereas the result at the boundary between two lower classes was not.</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanisms(DetailedCategoryBoundariesCalculationResult calculationResults);

        /// <summary>
        /// This method implements WBI-0G-5 from the functional design. Is calculates a <seealso cref="FailureMechanismSectionAssemblyCategoryResult"/> from the specified <see cref="DetailedCalculationResult"/>. This method does the same calculation / determination as WBI-0G-3, 
        /// but multiplies the corresponding probability with the <see cref="DetailedCalculationInputFromProbabilityWithLengthEffect.NValue"/>
        /// </summary>
        /// <param name="input">The combined input for this method.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionAssemblyCategoryResult"/>.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="DetailedCalculationInputFromProbabilityWithLengthEffect.Categories"/> does not contain a category that encloses the specified <see cref="DetailedCalculationInputFromProbabilityWithLengthEffect.Probability"/>.</exception>
        CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> DetailedAssessmentDirectFailureMechanisms(DetailedCalculationInputFromProbabilityWithLengthEffect input);

        /// <summary>
        /// This method implements WBI-0T-1 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 4.
        /// 
        /// In short, this method does the following:
        /// * NGO => VIIv
        /// * V => IIv
        /// * VN => Vv
        /// * FV => Iv
        /// </summary>
        /// <param name="result">The registered qualitative result.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCalculationResult result);

        /// <summary>
        /// This method implements WBI-0T-2 from the functional design. It calculates a <see cref="FailureMechanismSectionCategoryGroup"/> from the specified <see cref="SimpleCalculationResult"/> for indirect failure mechanisms.
        /// </summary>
        /// <param name="result">The calculation result that needs to be translated to an assessment category group.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown always</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentIndirectFailureMechanisms(TailorMadeCalculationResult result);

        /// <summary>
        /// This method implements WBI-0T-3 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 1 and 2.
        /// 
        /// In short, this method does the following:
        /// * FV => Iv / 0
        /// * NGO => VIIv / NaN
        /// * Probability => call WBI-0G-3 and find a category for this probability.
        /// </summary>
        /// <param name="input">The combined input for this method.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="DetailedCalculationInputFromProbability.Categories"/> does not contain a category that encloses the specified <see cref="DetailedCalculationInputFromProbability.Probability"/>.</exception>
        CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCalculationInputFromProbability input);

        /// <summary>
        /// This method implements WBI-0T-4 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms in group 3.
        /// 
        /// In short, this method does the following:
        /// * Iv or FV => Iv
        /// * IIv => IIv
        /// * IIIv => IIIv
        /// * IVv => IVv
        /// * Vv => Vv
        /// * VIv => VIv
        /// * VIIv or NGO => VIIv
        /// </summary>
        /// <param name="result">The calculation result.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCategoryCalculationResult result);

        /// <summary>
        /// This method implements WBI-0T-5 from the functional design. It calculates a category group from the result of a tailor made assessment for the failure mechanisms piping and slope stability.
        /// 
        /// In short this method does the following:
        /// * FV => Iv / 0
        /// * NGO => VIIv / NaN
        /// * Probability => call WBI-0G-5 and find a category for this probability (which multiplies the found probability with the spoecified <see cref="TailorMadeCalculationInputFromProbabilityWithLengthEffectFactor.NValue"/>).
        /// </summary>
        /// <param name="input">The combined input for this method.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the CalculationResultGroup has an invalid enum value</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <see cref="TailorMadeCalculationInputFromProbabilityWithLengthEffectFactor.Categories"/> does not contain a category that encloses the specified probability.</exception>
        CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCalculationInputFromProbabilityWithLengthEffectFactor input);

        /// <summary>
        /// This method implements WBI-0A-1 from the functional design. It combines results for the three levels of assessment to a result (<see cref="FailureMechanismSectionCategoryGroup"/>) for that specific failure mechanism section.
        /// 
        /// In short the function walks the following path:
        /// 1. If there is a tailormade result => return this
        /// 2. If there is a detailed result => return this
        /// 3. If there is a simple result => return this
        /// 4. If one of the results is of category VIIv => return this result
        /// 5. In case of no results at all => return category NotApplicable
        /// </summary>
        /// <param name="resultSimpleAssessment">The result of the simple assessment</param>
        /// <param name="resultDetailedAssessment">The result of the detailed assessment</param>
        /// <param name="resultTailorMadeAssessment">The result of the tailor made assessment</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        CalculationOutput<FailureMechanismSectionCategoryGroup>CombinedAssessmentFromFailureMechanismSectionResults(
            FailureMechanismSectionCategoryGroup resultSimpleAssessment,
            FailureMechanismSectionCategoryGroup resultDetailedAssessment,
            FailureMechanismSectionCategoryGroup resultTailorMadeAssessment);

        /// <summary>
        /// This method implements WBI-0A-1 from the functional design, including the estimated probabilities of failure. It combines results for the three levels of assessment to a result (<see cref="FailureMechanismSectionAssemblyCategoryResult"/>) for that specific failure mechanism section.
        /// 
        /// In short the function walks the following path:
        /// 1. If there is a tailormade result => return this
        /// 2. If there is a detailed result => return this
        /// 3. If there is a simple result => return this
        /// 4. If one of the results is of category VIIv => return this result
        /// 5. In case of no results at all => return category NotApplicable
        /// </summary>
        /// <param name="resultSimpleAssessment">The result of the simple assessment</param>
        /// <param name="resultDetailedAssessment">The result of the detailed assessment</param>
        /// <param name="resultTailorMadeAssessment">The result of the tailor made assessment</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionAssemblyCategoryResult"/>.</returns>
        CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> CombinedAssessmentFromFailureMechanismSectionResults(
            FailureMechanismSectionAssemblyCategoryResult resultSimpleAssessment,
            FailureMechanismSectionAssemblyCategoryResult resultDetailedAssessment,
            FailureMechanismSectionAssemblyCategoryResult resultTailorMadeAssessment);
    }
}