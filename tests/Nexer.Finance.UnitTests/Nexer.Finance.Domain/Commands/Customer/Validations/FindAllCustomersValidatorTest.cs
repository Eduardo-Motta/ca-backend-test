using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Customer.Validations;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class FindAllCustomersValidatorTest
    {
        [Fact]
        public async void ShouldValidateWithoutErrors()
        {
            var command = new FindAllCustomersCommand
            {
                Pagination = new PaginationCommand
                {
                    PageNumber = 1,
                    PageSize = 5,
                }
            };

            var validate = await new FindAllCustomersValidator().ValidateAsync(command);

            Assert.True(validate.IsValid);
            Assert.Empty(validate.Errors);
        }

        [Fact]
        public async void ShouldReturnErrorWhenPageNumberIsEqualsZero()
        {
            var command = new FindAllCustomersCommand
            {
                Pagination = new PaginationCommand
                {
                    PageNumber = 0,
                    PageSize = 10,
                }
            };

            var validate = await new FindAllCustomersValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("Pagination.PageNumber", error1.PropertyName);
                Assert.Equal("The PageNumber must be greater than zero", error1.ErrorMessage);
            });
        }

        [Fact]
        public async void ShouldReturnErrorWhenPageSizeIsLessThanFive()
        {
            var command = new FindAllCustomersCommand
            {
                Pagination = new PaginationCommand
                {
                    PageNumber = 5,
                    PageSize = 1,
                }
            };

            var validate = await new FindAllCustomersValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("Pagination.PageSize", error1.PropertyName);
                Assert.Equal("The PageSize must be greater than or equal to 5", error1.ErrorMessage);
            });
        }
    }
}
