using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Neudesic.Core.Services
{
    /// <summary>
    /// Security service.
    /// </summary>
    public static class SecurityService
    {
        /// <summary>
        /// Regex pattern to ensure password must consist of a mixture of letters and numerical digits only, 
        /// with at least one of each.
        /// </summary>
        private const string AlphaNumericPattern = @"^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]+$";

        /// <summary>
        /// Regex pattern to ensure password must not contain any sequence of characters immediately 
        /// followed by the same sequence.
        /// </summary>
        private const string SequencePattern = @"(.+)(?=\1+)";

        /// <summary>
        /// Enum for Validating Password rules
        /// </summary>
        public enum ValidationResult
        {

            /// <summary>
            /// Password fails for password length validation
            /// </summary>
            [Description("Password Length")]
            length = 0,

            /// <summary>
            /// Password is Valid
            /// </summary>
            [Description("Password is Valid")]
            valid = 1,

            /// <summary>
            /// Password fails for alpha numeric character validation
            /// </summary>
            [Description("Must contain only Alpha/Numeric")]
            alphaNumeric = 2,

            /// <summary>
            /// Password fails for sequence validation
            /// </summary>
            [Description("Cannot repeat a sequence")]
            sequence = 3,
        }

        /// <summary>
        /// Validates the password rules.
        /// </summary>
        /// <returns>Returns a <see cref="ValidationResult">Result</see></returns>
        /// <param name="password">Password.</param>
        public static ValidationResult ValidatePasswordRules(string password) {
            var validationResult = ValidationResult.valid;
            var alphaNumericRegex = new Regex(AlphaNumericPattern);
            var sequenceRegex = new Regex(SequencePattern);
            if (!alphaNumericRegex.IsMatch(password))
            {
                validationResult = ValidationResult.alphaNumeric;
            }
            else if (sequenceRegex.IsMatch(password))
            {
                validationResult = ValidationResult.sequence;
            }
            else if (password.Length < 5 || password.Length > 12)
            {
                validationResult = ValidationResult.length;
            }

            return validationResult;
        }
    }
}
