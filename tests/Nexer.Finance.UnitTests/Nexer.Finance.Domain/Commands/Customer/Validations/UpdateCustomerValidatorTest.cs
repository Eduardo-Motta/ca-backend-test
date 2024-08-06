using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Customer.Validations;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class UpdateCustomerValidatorTest
    {
        [Fact]
        public async void ShouldValidateWithoutErrors()
        {
            var command = new UpdateCustomerCommand
            {
                Name = "Ana Maria",
                Email = "anamaria@outlook.com",
                Address = "Av. Recife, 5502 - Quiosq03 - Centro, Rolim de Moura - RO, 76940-000",
            };
            command.SetId(Guid.NewGuid());

            var validate = await new UpdateCustomerValidator().ValidateAsync(command);

            Assert.True(validate.IsValid);
            Assert.Empty(validate.Errors);
        }

        [Fact]
        public async void ShouldReturnErrorWhenNameIsEmpty()
        {
            var command = new UpdateCustomerCommand
            {
                Name = string.Empty,
                Email = "contato@email.com",
                Address = "Av. Padre Adolpho Rohl, 2478 - Centro, Jaru - RO, 76890-000",
            };
            command.SetId(Guid.NewGuid());

            var validate = await new UpdateCustomerValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("Name", error1.PropertyName);
                Assert.Equal("Required field", error1.ErrorMessage);
            });
        }

        [Fact]
        public async void ShouldReturnErrorWhenNameIsNull()
        {
            var command = new UpdateCustomerCommand
            {
                Name = null!,
                Email = "carlosjunior@gmail.com",
                Address = "R. Dr. Gilberto Studart, 55 - Torre Norte - Cocó, Fortaleza - CE, 60192-105",
            };
            command.SetId(Guid.NewGuid());

            var validate = await new UpdateCustomerValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("Name", error1.PropertyName);
                Assert.Equal("Required field", error1.ErrorMessage);
            });
        }

        [Fact]
        public async void ShouldReturnErrorWhenEmailIsInvalid()
        {
            var command = new UpdateCustomerCommand
            {
                Name = "Juca Silva",
                Email = "jsilva.gmail.com",
                Address = "Al. Rio Negro, 161 - 4° andar - Alphaville Industrial, Barueri - SP, 06454-000",
            };
            command.SetId(Guid.NewGuid());

            var validate = await new UpdateCustomerValidator().ValidateAsync(command);

            Assert.False(validate.IsValid);
            Assert.Single(validate.Errors);
            Assert.Collection(validate.Errors, error1 =>
            {
                Assert.Equal("Email", error1.PropertyName);
                Assert.Equal("Invalid email address", error1.ErrorMessage);
            });
        }
    }
}
