using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Customer.Validations;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class FindCustomerByIdValidatorTest
    {
        [Fact]
        public async void ShouldValidateWithoutErrors()
        {
            var command = new FindCustomerByIdCommand
            {
                Id = Guid.NewGuid()
            };

            var validate = await new FindCustomerByIdValidator().ValidateAsync(command);

            Assert.True(validate.IsValid);
            Assert.Empty(validate.Errors);
        }

        [Fact]
        public async void ShouldReturnErrorWhenPageNumberIsEqualsZero()
        {
            var command = new FindCustomerByIdCommand
            {
                Id = Guid.Empty
            };

            var validate = await new FindCustomerByIdValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("Id", error1.PropertyName);
                Assert.Equal("Required field", error1.ErrorMessage);
            });
        }
    }
}
