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
using System.Linq;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.AssessmentResults;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Assembly
{
    public static class FailureMechanismSectionAssemblyCalculator
    {
        #region Simple assessment

        /// <summary>
        /// WBI-0E-1 / WBI-0E-3
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentDirectFailureMechanisms(SimpleAssessmentResult result)
        {
            switch (result)
            {
                case SimpleAssessmentResult.NVT:
                    return null;
                case SimpleAssessmentResult.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                case SimpleAssessmentResult.VB:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// WBI-0E-2
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentIndirectFailureMechanisms(SimpleAssessmentResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DetailedAssessment

        /// <summary>
        /// WBI-0G-1
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromResult(DetailedAssessmentResult result)
        {
            switch (result)
            {
                case DetailedAssessmentResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                case DetailedAssessmentResult.V:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
                case DetailedAssessmentResult.VN:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// WBI-0G-2
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentIndirectFailureMechanismsFromResult(DetailedAssessmentResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// WBI-0G-3 / WBI-0G-5
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromProbability(Probability probability, FailureMechanismSectionCategory[] categories)
        {
            var resultCategory = categories.FirstOrDefault(c => c.LowerBoundary <= probability && c.UpperBoundary >= probability);
            return resultCategory == null
                ? new CalculationOutput<FailureMechanismSectionCategoryGroup>(
                    new AssemblyToolKernelException(ErrorCode.NoMatchingCategory))
                : new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultCategory.CategoryGroup);
        }

        /// <summary>
        /// WBI-0G-4. This method differs from the FO, since we (Ringtoets) register the results differently.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromCategoryBoundaries(DetailedAssesmentCategoryBoundariesResult results)
        {
            if (results.ResultItoII == DetailedAssessmentResult.V)
            {
                if (results.ResultIItoIII == DetailedAssessmentResult.VN ||
                    results.ResultIIItoIV == DetailedAssessmentResult.VN ||
                    results.ResultIVtoV == DetailedAssessmentResult.VN ||
                    results.ResultVtoVI == DetailedAssessmentResult.VN)
                {
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination));
                }
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
            }

            if (results.ResultIItoIII == DetailedAssessmentResult.V)
            {
                if (results.ResultIIItoIV == DetailedAssessmentResult.VN ||
                    results.ResultIVtoV == DetailedAssessmentResult.VN ||
                    results.ResultVtoVI == DetailedAssessmentResult.VN)
                {
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination));
                }
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
            }

            if (results.ResultIIItoIV == DetailedAssessmentResult.V)
            {
                if (results.ResultIVtoV == DetailedAssessmentResult.VN ||
                    results.ResultVtoVI == DetailedAssessmentResult.VN)
                {
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination));
                }
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIIv);
            }

            if (results.ResultIVtoV == DetailedAssessmentResult.V)
            {
                return results.ResultVtoVI == DetailedAssessmentResult.VN
                    ? new CalculationOutput<FailureMechanismSectionCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination))
                    : new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IVv);
            }

            if (results.ResultVtoVI == DetailedAssessmentResult.V)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
            }

            if (results.ResultVtoVI == DetailedAssessmentResult.VN)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIv);
            }

            return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
        }

        #endregion

        #region TailorMade assessment

        /// <summary>
        /// WBI-0T-1
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromResult(TailorMadeAssessmentResult result)
        {
            switch (result)
            {
                case TailorMadeAssessmentResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                case TailorMadeAssessmentResult.V:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
                case TailorMadeAssessmentResult.VN:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
                case TailorMadeAssessmentResult.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// WBI-0T-2
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentIndirectFailureMechanismsFromResult(TailorMadeAssessmentResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// WBI-0T-3 / WBI-0T-5
        /// </summary>
        /// <param name="result"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromProbability(TailorMadeProbabilityAssessmentResult result, FailureMechanismSectionCategory[] categories)
        {
            if (result.AssessmentResultGroup == TailorMadeProbabilityAssessmentResultGroup.FV)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
            }

            if (result.AssessmentResultGroup == TailorMadeProbabilityAssessmentResultGroup.NGO)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
            }

            return DetailedAssessmentDirectFailureMechanismsFromProbability(result.Probability, categories);
        }

        /// <summary>
        /// WBI-0T-4
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromCategoryResult(TailorMadeCategoryAssessmentResult result)
        {
            switch (result)
            {
                case TailorMadeCategoryAssessmentResult.Iv:
                case TailorMadeCategoryAssessmentResult.FV:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Iv);
                case TailorMadeCategoryAssessmentResult.IIv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIv);
                case TailorMadeCategoryAssessmentResult.IIIv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IIIv);
                case TailorMadeCategoryAssessmentResult.IVv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.IVv);
                case TailorMadeCategoryAssessmentResult.Vv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.Vv);
                case TailorMadeCategoryAssessmentResult.VIv:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIv);
                case TailorMadeCategoryAssessmentResult.VIIv:
                case TailorMadeCategoryAssessmentResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
                default:
                    return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
            }
        }
        #endregion

        #region Combined over failure mechanism section

        /// <summary>
        /// WBI-0A-1
        /// </summary>
        /// <param name="resultSimpleAssessment"></param>
        /// <param name="resultDetailedAssessment"></param>
        /// <param name="resultTailorMadeAssessment"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionCategoryGroup>CombinedAssessmentFromFailureMechanismSectionResults(
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
