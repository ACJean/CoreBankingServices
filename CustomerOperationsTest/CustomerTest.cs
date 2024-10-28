using CustomerOperations.Domain.Entity;
using Moq;
using SharedOperations.Domain;
using SharedOperations.Domain.Validator;

namespace CustomerOperationsTest
{
    public class CustomerTests
    {
        [Fact]
        public void SetPassword_ShouldSetPassword_WhenPasswordIsValid()
        {
            var customer = new Customer();
            var password = "ValidPassword123";
            var mockValidator = new Mock<IPasswordValidator>();
            mockValidator.Setup(v => v.IsValid(password)).Returns(Unit.Value);

            var result = customer.SetPassword(password, mockValidator.Object);

            Assert.NotNull(result.Value);
        }

        [Fact]
        public void SetPassword_ShouldNotSetPassword_WhenPasswordIsInvalid()
        {
            var customer = new Customer();
            var password = "InvalidPassword";
            var mockValidator = new Mock<IPasswordValidator>();
            var error = new Error("InvalidPassword", "The password does not meet the criteria.");
            mockValidator.Setup(v => v.IsValid(password)).Returns(error);

            var result = customer.SetPassword(password, mockValidator.Object);

            Assert.Null(result.Value);
        }
    }
}