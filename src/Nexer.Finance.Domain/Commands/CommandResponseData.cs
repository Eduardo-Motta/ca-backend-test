using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands
{
    public class CommandResponseData<T> : ICommandResult
    {
        public CommandResponseData(T data)
        {
            Data = data;
            Success = true;
        }

        public T Data { get; set; }
        public bool Success { get; }
    }
}
