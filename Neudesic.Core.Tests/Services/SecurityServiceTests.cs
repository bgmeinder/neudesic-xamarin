using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neudesic.Core.Services;

namespace Neudesic.Core.Tests.Services
{
    [TestClass]
    public class SecurityServiceTests
    {
        /// <summary>
        /// The password max length.
        /// </summary>
        const int maxLength = 12;

        /// <summary>
        /// The password min length
        /// </summary>
        const int minLength = 5;

        /// <summary>
        /// A password that passes all complexity rules
        /// </summary>
        const string validPassword = "Abcdefil123";

        [TestMethod]
        public void PasswordValidationShouldValidateMinLength()
        {
            const string password = "Abc1";

            var result = SecurityService.ValidatePasswordRules(password);

            password.Length.Should().BeLessThan(minLength);
            result.Should().Be(SecurityService.ValidationResult.length);
        }

        [TestMethod]
        public void PasswordValidationShouldValidateMaxLength()
        {
            const string password = "Abcdefghil123";

            var result = SecurityService.ValidatePasswordRules(password);

            password.Length.Should().BeGreaterThan(maxLength);
            result.Should().Be(SecurityService.ValidationResult.length);
        }

        [TestMethod]
        public void PasswordValidationShouldValidateValidLength()
        {
            var result = SecurityService.ValidatePasswordRules(validPassword);

            validPassword.Length.Should().BeInRange(5, 12);
            result.Should().Be(SecurityService.ValidationResult.valid);
        }

        [TestMethod]
        public void PasswordValidationShouldOnlyAllowAlphaNumeric()
        {
            const string password = "@bcdefil123!23";

            var result = SecurityService.ValidatePasswordRules(password);

            result.Should().Be(SecurityService.ValidationResult.alphaNumeric);
        }

        [TestMethod]
        public void PasswordValidationShouldRequireANumber()
        {
            const string password = "Abcdefg";

            var result = SecurityService.ValidatePasswordRules(password);

            result.Should().Be(SecurityService.ValidationResult.alphaNumeric);
        }

        [TestMethod]
        public void PasswordValidationShouldRequireALetter()
        {
            const string password = "123456";

            var result = SecurityService.ValidatePasswordRules(password);

            result.Should().Be(SecurityService.ValidationResult.alphaNumeric);
        }

        [TestMethod]
        public void PasswordValidationShouldNotAllowARepeatedSequence()
        {
            const string password = "A1b2cA1b2c";

            var result = SecurityService.ValidatePasswordRules(password);

            result.Should().Be(SecurityService.ValidationResult.sequence);
        }

        [TestMethod]
        public void PasswordValidationShouldAllowSequenceNotRepeated()
        {
            const string password = "A1b2c1A1b2c";

            var result = SecurityService.ValidatePasswordRules(password);

            result.Should().Be(SecurityService.ValidationResult.valid);
        }
    }
}
