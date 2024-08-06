using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Customer.Validations;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class CreateCustomerValidatorTest
    {
        [Fact]
        public async void ShouldValidateWithoutErrors()
        {
            var command = new CreateCustomerCommand
            {
                Name = "Ana Maria",
                Email = "anamaria@outlook.com",
                Address = "Av. Recife, 5502 - Quiosq03 - Centro, Rolim de Moura - RO, 76940-000",
            };
            var validate = await new CreateCustomerValidator().ValidateAsync(command);

            Assert.True(validate.IsValid);
            Assert.Empty(validate.Errors);
        }

        [Fact]
        public async void ShouldReturnErrorWhenNameIsEmpty()
        {
            var command = new CreateCustomerCommand
            {
                Name = string.Empty,
                Email = "contato@email.com",
                Address = "Av. Padre Adolpho Rohl, 2478 - Centro, Jaru - RO, 76890-000",
            };
            var validate = await new CreateCustomerValidator().ValidateAsync(command);

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
            var command = new CreateCustomerCommand
            {
                Name = null!,
                Email = "carlosjunior@gmail.com",
                Address = "R. Dr. Gilberto Studart, 55 - Torre Norte - Cocó, Fortaleza - CE, 60192-105",
            };
            var validate = await new CreateCustomerValidator().ValidateAsync(command);

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
            var command = new CreateCustomerCommand
            {
                Name = "Juca Silva",
                Email = "jsilva.gmail.com",
                Address = "Al. Rio Negro, 161 - 4° andar - Alphaville Industrial, Barueri - SP, 06454-000",
            };
            var validate = await new CreateCustomerValidator().ValidateAsync(command);

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
