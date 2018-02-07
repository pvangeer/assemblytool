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

using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.CalculationResults;
using AssemblyTool.Kernel.ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AssemblyTool.Kernel.Assembly.CalculatorInput;
using AssemblyTool.Kernel.Categories;

namespace AssemblyTool.Kernel.Assembly
{
    public class FailureMechanismSectionAssemblyCalculator : IFailureMechanismSectionAssemblyCalculator
    {
        #region Simple assessment

        public CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> SimpleAssessmentDirectFailureMechanisms(SimpleCalculationResult result)
        {
            switch (result)
            {
                case SimpleCalculationResult.NVT:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.NotApplicable,(Probability) 0));
                case SimpleCalculationResult.FV:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.Iv,(Probability)0));
                case SimpleCalculationResult.VB:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.VIIv, Probability.NaN));
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public CalculationOutput<FailureMechanismSectionCategoryGroup> SimpleAssessmentIndirectFailureMechanisms(SimpleCalculationResult result)
        {
            throw new NotImplementedException();
        }

        public CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> SimpleAssessmentDirectFailureMechanisms(SimpleCalculationResultValidityOnly result)
        {
            switch (result)
            {
                case SimpleCalculationResultValidityOnly.NVT:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.NotApplicable,(Probability) 0));
                case SimpleCalculationResultValidityOnly.WVT:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.VIIv, Probability.NaN));
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        #endregion

        #region DetailedAssessment

        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanisms(DetailedCalculationResult result)
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

        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentIndirectFailureMechanisms(DetailedCalculationResult result)
        {
            throw new NotImplementedException();
        }

        public CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> DetailedAssessmentDirectFailureMechanisms(DetailedCalculationInputFromProbability input)
        {
            if (input == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            var resultCategory = input.Categories.FirstOrDefault(c => c.LowerBoundary <= input.Probability && c.UpperBoundary >= input.Probability);
            if (resultCategory == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.NoMatchingCategory);
            }

            return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(resultCategory.CategoryGroup,input.Probability));
        }

        public CalculationOutput<FailureMechanismSectionCategoryGroup> DetailedAssessmentDirectFailureMechanisms(DetailedCategoryBoundariesCalculationResult calculationResults)
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

        public CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> DetailedAssessmentDirectFailureMechanisms(DetailedCalculationInputFromProbabilityWithLengthEffect input)
        {
            if (input == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            var warnings = new List<WarningMessage>();
            var result = DetailedAssessmentDirectFailureMechanisms(new DetailedCalculationInputFromProbability(input.Probability, input.Categories));

            var adjustedProbability = (double)result.Result.EstimatedProbabilityOfFailure* input.NValue;
            if (adjustedProbability > 1.0)
            {
                warnings.Add(WarningMessage.CorrectedProbability);
            }
            var estimatedProbabilityOfFailure = (Probability)Math.Min(1.0,adjustedProbability);
            return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(result.Result.CategoryGroup, estimatedProbabilityOfFailure), warnings.ToArray());
        }
        #endregion

        #region TailorMade assessment

        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCalculationResult result)
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

        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentIndirectFailureMechanisms(TailorMadeCalculationResult result)
        {
            throw new NotImplementedException();
        }

        public CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCalculationInputFromProbability input)
        {
            if (input == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            switch (input.Result.CalculationResultGroup)
            {
                case TailorMadeProbabilityCalculationResultGroup.FV:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.Iv,(Probability) 0));
                case TailorMadeProbabilityCalculationResultGroup.NGO:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.VIIv,Probability.NaN));
                case TailorMadeProbabilityCalculationResultGroup.Probability:
                    return DetailedAssessmentDirectFailureMechanisms(new DetailedCalculationInputFromProbability(input.Result.Probability, input.Categories));
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public CalculationOutput<FailureMechanismSectionCategoryGroup> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCategoryCalculationResult result)
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

        public CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCalculationInputFromProbabilityWithLengthEffectFactor input)
        {
            if (input == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }

            switch (input.Result.CalculationResultGroup)
            {
                case TailorMadeProbabilityCalculationResultGroup.FV:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.Iv,(Probability) 0));
                case TailorMadeProbabilityCalculationResultGroup.NGO:
                    return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.VIIv,Probability.NaN));
                case TailorMadeProbabilityCalculationResultGroup.Probability:
                    return DetailedAssessmentDirectFailureMechanisms(new DetailedCalculationInputFromProbabilityWithLengthEffect(input.Result.Probability, input.Categories, input.NValue));
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        #region Combined over failure mechanism section

        public CalculationOutput<FailureMechanismSectionCategoryGroup>CombinedAssessmentFromFailureMechanismSectionResults(
                FailureMechanismSectionCategoryGroup resultSimpleAssessment,
                FailureMechanismSectionCategoryGroup resultDetailedAssessment,
                FailureMechanismSectionCategoryGroup resultTailorMadeAssessment)
        {
            var tailorMadeAssessmentCategorySeven = resultTailorMadeAssessment == FailureMechanismSectionCategoryGroup.VIIv;
            if (!tailorMadeAssessmentCategorySeven && resultTailorMadeAssessment != FailureMechanismSectionCategoryGroup.NotApplicable)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultTailorMadeAssessment);
            }

            var detailedAssessmentCategorySeven = resultDetailedAssessment == FailureMechanismSectionCategoryGroup.VIIv;
            if (!detailedAssessmentCategorySeven && resultDetailedAssessment != FailureMechanismSectionCategoryGroup.NotApplicable)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultDetailedAssessment);
            }

            var simpleAssessmentCategorySeven = resultSimpleAssessment == FailureMechanismSectionCategoryGroup.VIIv;
            if (!simpleAssessmentCategorySeven && resultSimpleAssessment != FailureMechanismSectionCategoryGroup.NotApplicable)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(resultSimpleAssessment);
            }

            if (tailorMadeAssessmentCategorySeven || detailedAssessmentCategorySeven || simpleAssessmentCategorySeven)
            {
                return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.VIIv);
            }

            return new CalculationOutput<FailureMechanismSectionCategoryGroup>(FailureMechanismSectionCategoryGroup.NotApplicable);
        }

        public CalculationOutput<FailureMechanismSectionAssemblyCategoryResult> CombinedAssessmentFromFailureMechanismSectionResults(
            FailureMechanismSectionAssemblyCategoryResult resultSimpleAssessment,
            FailureMechanismSectionAssemblyCategoryResult resultDetailedAssessment,
            FailureMechanismSectionAssemblyCategoryResult resultTailorMadeAssessment)
        {
            var tailorMadeAssessmentNoResult = resultTailorMadeAssessment == null || resultTailorMadeAssessment.CategoryGroup == FailureMechanismSectionCategoryGroup.NotApplicable;
            if (!tailorMadeAssessmentNoResult && resultTailorMadeAssessment.CategoryGroup != FailureMechanismSectionCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(resultTailorMadeAssessment);
            }

            var detailedAssessmentNoResult = resultDetailedAssessment == null || resultDetailedAssessment.CategoryGroup == FailureMechanismSectionCategoryGroup.NotApplicable;
            if (!detailedAssessmentNoResult && resultDetailedAssessment.CategoryGroup != FailureMechanismSectionCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(resultDetailedAssessment);
            }

            var simpleAssessmentNoResult = resultSimpleAssessment == null || resultSimpleAssessment.CategoryGroup == FailureMechanismSectionCategoryGroup.NotApplicable;
            if (!simpleAssessmentNoResult && resultSimpleAssessment.CategoryGroup != FailureMechanismSectionCategoryGroup.VIIv)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(resultSimpleAssessment);
            }

            if (tailorMadeAssessmentNoResult && detailedAssessmentNoResult && simpleAssessmentNoResult)
            {
                return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.NotApplicable, Probability.NaN));
            }

            return new CalculationOutput<FailureMechanismSectionAssemblyCategoryResult>(new FailureMechanismSectionAssemblyCategoryResult(FailureMechanismSectionCategoryGroup.VIIv,Probability.NaN));
        }

        #endregion
    }
}
