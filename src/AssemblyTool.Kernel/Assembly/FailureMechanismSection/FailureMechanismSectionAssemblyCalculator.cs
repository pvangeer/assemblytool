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

namespace AssemblyTool.Kernel.Assembly.FailureMechanismSection
{
    public static class FailureMechanismSectionAssemblyCalculator
    {
        #region Simple assessment

        /// <summary>
        /// WBI-0E-1 / WBI-0E-3
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> SimpleAssessmentDirectFailureMechanisms(SimpleAssessmentResult result)
        {
            switch (result)
            {
                case SimpleAssessmentResult.NVT:
                    return null;
                case SimpleAssessmentResult.FV:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Iv);
                case SimpleAssessmentResult.VB:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// WBI-0E-2
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> SimpleAssessmentIndirectFailureMechanisms(SimpleAssessmentResult result)
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
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromResult(DetailedAssessmentResult result)
        {
            switch (result)
            {
                case DetailedAssessmentResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
                case DetailedAssessmentResult.V:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IIv);
                case DetailedAssessmentResult.VN:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Vv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// WBI-0G-2
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> DetailedAssessmentIndirectFailureMechanismsFromResult(SimpleAssessmentResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// WBI-0G-3 / WBI-0G-5
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromProbability(Probability probability, FailureMechanismSectionCategory[] categories)
        {
            var resultCategory = categories.FirstOrDefault(c => c.LowerBoundary <= probability && c.UpperBoundary >= probability);
            return resultCategory == null
                ? new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(
                    new AssemblyToolKernelException(ErrorCode.NoMatchingCategory))
                : new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(resultCategory.CategoryGroup);
        }

        /// <summary>
        /// WBI-0G-4. This method differs from the FO, since we register the results differently.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> DetailedAssessmentDirectFailureMechanismsFromCategoryBoundaries(DetailedAssesmentCategoryBoundariesResult results)
        {
            if (results.ResultItoII == DetailedAssessmentResult.V)
            {
                if (results.ResultIItoIII == DetailedAssessmentResult.VN ||
                    results.ResultIIItoIV == DetailedAssessmentResult.VN ||
                    results.ResultIVtoV == DetailedAssessmentResult.VN ||
                    results.ResultVtoVI == DetailedAssessmentResult.VN)
                {
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination));
                }
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Iv);
            }

            if (results.ResultIItoIII == DetailedAssessmentResult.V)
            {
                if (results.ResultIIItoIV == DetailedAssessmentResult.VN ||
                    results.ResultIVtoV == DetailedAssessmentResult.VN ||
                    results.ResultVtoVI == DetailedAssessmentResult.VN)
                {
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination));
                }
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IIv);
            }

            if (results.ResultIIItoIV == DetailedAssessmentResult.V)
            {
                if (results.ResultIVtoV == DetailedAssessmentResult.VN ||
                    results.ResultVtoVI == DetailedAssessmentResult.VN)
                {
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination));
                }
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IIIv);
            }

            if (results.ResultIVtoV == DetailedAssessmentResult.V)
            {
                return results.ResultVtoVI == DetailedAssessmentResult.VN
                    ? new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(new AssemblyToolKernelException(ErrorCode.ImpossibleResultCombination))
                    : new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IVv);
            }

            if (results.ResultVtoVI == DetailedAssessmentResult.V)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Vv);
            }

            if (results.ResultVtoVI == DetailedAssessmentResult.VN)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIv);
            }

            return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
        }

        #endregion

        #region TailorMade assessment

        /// <summary>
        /// WBI-0T-1
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromResult(TailorMadeAssessmentResult result)
        {
            switch (result)
            {
                case TailorMadeAssessmentResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
                case TailorMadeAssessmentResult.V:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IIv);
                case TailorMadeAssessmentResult.VN:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Vv);
                case TailorMadeAssessmentResult.FV:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Iv);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// WBI-0T-2
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> TailorMadeAssessmentIndirectFailureMechanismsFromResult(SimpleAssessmentResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// WBI-0T-3 / WBI-0T-5
        /// </summary>
        /// <param name="result"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromProbability(TailoreMadeProbabilityAssessmentResult result, FailureMechanismSectionCategory[] categories)
        {
            if (result.AssessmentResult == TailorMadeProbabilisticAssessmentResult.FV)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Iv);
            }

            if (result.AssessmentResult == TailorMadeProbabilisticAssessmentResult.NGO)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
            }

            return DetailedAssessmentDirectFailureMechanismsFromProbability(result.Probability, categories);
        }

        /// <summary>
        /// WBI-0T-4
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup> TailorMadeAssessmentDirectFailureMechanismsFromCategoryResult(TailoreMadeAssessmentCategoryResult result)
        {
            switch (result)
            {
                case TailoreMadeAssessmentCategoryResult.Iv:
                case TailoreMadeAssessmentCategoryResult.FV:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Iv);
                case TailoreMadeAssessmentCategoryResult.IIv:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IIv);
                case TailoreMadeAssessmentCategoryResult.IIIv:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IIIv);
                case TailoreMadeAssessmentCategoryResult.IVv:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.IVv);
                case TailoreMadeAssessmentCategoryResult.Vv:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.Vv);
                case TailoreMadeAssessmentCategoryResult.VIv:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIv);
                case TailoreMadeAssessmentCategoryResult.VIIv:
                case TailoreMadeAssessmentCategoryResult.NGO:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
                default:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
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
        public static CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>
            CombinedAssessmentFromFailureMechanismSectionResults(
                FailureMechanismSectionAssemblyCategoryGroup resultSimpleAssessment,
                FailureMechanismSectionAssemblyCategoryGroup resultDetailedAssessment,
                FailureMechanismSectionAssemblyCategoryGroup resultTailorMadeAssessment)
        {
            if (resultTailorMadeAssessment != FailureMechanismSectionAssemblyCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(resultTailorMadeAssessment);
            }
            if (resultDetailedAssessment != FailureMechanismSectionAssemblyCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(resultDetailedAssessment);
            }
            if (resultSimpleAssessment != FailureMechanismSectionAssemblyCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(resultSimpleAssessment);
            }

            return new CalculationOutput<FailureMechanismSectionAssemblyCategoryGroup>(FailureMechanismSectionAssemblyCategoryGroup.VIIv);
        }

        #endregion
    }
}
