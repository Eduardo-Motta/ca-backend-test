namespace Nexer.Finance.Domain.Commands
{
    public class ErrorMessage
    {
        public ErrorMessage(string property, string message, string? attemptedValue = null)
        {
            Property = property;
            Message = message;
            AttemptedValue = attemptedValue;
        }

        public string Property { get; }
        public string Message { get; }
        public string? AttemptedValue { get; } = null;
    }
}
