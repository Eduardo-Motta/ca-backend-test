using Nexer.Finance.Domain.Commands;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Commands
{
    public class PaginationCommandTest
    {
        [Fact]
        public async void ShouldValidateWithoutErrors()
        {
            var command = new PaginationCommand
            {
                PageNumber = 1,
                PageSize = 12,
            };

            var validate = await new PaginationValidator().ValidateAsync(command);

            Assert.True(validate.IsValid);
            Assert.Empty(validate.Errors);
        }

        [Fact]
        public async void ShouldReturnErrorWhenPageNumberIsEqualsZero()
        {
            var command = new PaginationCommand
            {
                PageNumber = 0,
                PageSize = 10,
            };

            var validate = await new PaginationValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("PageNumber", error1.PropertyName);
                Assert.Equal("The PageNumber must be greater than zero", error1.ErrorMessage);
            });
        }

        [Fact]
        public async void ShouldReturnErrorWhenPageSizeIsLessThanFive()
        {
            var command = new PaginationCommand
            {
                PageNumber = 1,
                PageSize = 3,
            };

            var validate = await new PaginationValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("PageSize", error1.PropertyName);
                Assert.Equal("The PageSize must be greater than or equal to 5", error1.ErrorMessage);
            });
        }
    }
}
