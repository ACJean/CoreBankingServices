using CustomerOperations.Domain;
using SharedOperations.Domain;
using SharedOperations.Infrastructure;

namespace CustomerOperationsTest
{
    public class CustomerTests
    {
        [Fact]
        public void IsValidPassword_ValidPassword_ReturnsTrue()
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();

            var customer = new Customer
            {
                Password = "ValidPassword.123"
            };

            // Act
            var result = customer.IsValidPassword(passwordValidator);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidPassword_InvalidPassword_ReturnsFalse()
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();

            var customer = new Customer
            {
                Password = "InvalidPassword"
            };

            // Act
            var result = customer.IsValidPassword(passwordValidator);

            // Assert
            Assert.False(result);
        }
    }
}