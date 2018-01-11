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

namespace AssemblyTool.Kernel.ErrorHandling
{
    public static class ErrorCodeExtensions
    {
        private static Dictionary<ErrorCode, string> CodeTranslations = new Dictionary<ErrorCode, string>
        {
            {ErrorCode.ValueBelowZero, "The value of this parameter is supposed not to be lower than 0, but it is."},
            {ErrorCode.ValueAboveOne, "The value of this parameter is supposed not to exceed 1, but it does."},
            {ErrorCode.SignallingStandardExceedsLowerBoundary,"The entered signaling standard exceeds the lower boundary. This should bot be the case."},
            {ErrorCode.InvalidSignalingStandard,"The entered signaling standard is not valid. See the innerexception for more details."},
            {ErrorCode.InvalidLowerBoundaryStandard,"The entered lower boundary standard is not valid. See the innerexception for more details."},
            {ErrorCode.InvalidProbabilityDistributionFactor,"The entered probability distribution factor is not valid. See the inner exception for more details."},
            {ErrorCode.ValueIsNaN, "The value of this double equals NaN."},
            {ErrorCode.CategoryLowerBoundaryExceedsUpperBoundary,"The lower boundary (probability) of a category should be lower than the upperboundary (probability), but it is not."},
            {ErrorCode.ValueBelowOne, "Value should be above one (or equal to one), but it is not"},
            {ErrorCode.InvalidNValue, "The specified N - value is invalid. See the inner exception for more details."}
        };
        
        /// <summary>
        /// Translates the error code to a readable string message that can be used in the exception that uses this code.
        /// </summary>
        /// <param name="errorCode"><see cref="ErrorCode"/> that needs to be translated to a meaningfull message.</param>
        /// <returns></returns>
        public static string GetMessage(this ErrorCode errorCode)
        {
            return CodeTranslations.ContainsKey(errorCode) ? CodeTranslations[errorCode] : "Unspecified error occured.";
        }
    }
}