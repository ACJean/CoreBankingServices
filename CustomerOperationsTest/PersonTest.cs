using Moq;
using CustomerOperations.Domain.Entity;
using SharedOperations.Domain;
using SharedOperations.Domain.Validator;

namespace CustomerOperationsTests
{
    public class PersonTests
    {
        [Fact]
        public void SetPhoneNumber_ShouldSetPhoneNumber_WhenPhoneNumberIsValid()
        {
            var person = new Person();
            var phoneNumber = "123-456-7890";
            var mockValidator = new Mock<IPhoneNumberValidator>();
            mockValidator.Setup(v => v.IsValid(phoneNumber)).Returns(Unit.Value);

            var result = person.SetPhoneNumber(phoneNumber, mockValidator.Object);

            Assert.NotNull(result.Value);
        }

        [Fact]
        public void SetPhoneNumber_ShouldNotSetPhoneNumber_WhenPhoneNumberIsInvalid()
        {
            var person = new Person();
            var phoneNumber = "invalid-phone-number";
            var mockValidator = new Mock<IPhoneNumberValidator>();
            var error = new Error("InvalidPhoneNumber", "The phone number is not valid.");
            mockValidator.Setup(v => v.IsValid(phoneNumber)).Returns(error);

            var result = person.SetPhoneNumber(phoneNumber, mockValidator.Object);

            Assert.Null(result.Value);
        }
    }
}
