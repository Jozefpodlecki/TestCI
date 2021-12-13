using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace TestCI.Modules
{
    /// <inheritdoc/>
    public class BoolTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            switch (input.ToLower())
            {
                case "1":
                    return Task.FromResult(TypeReaderResult.FromSuccess(true));
                case "0":
                    return Task.FromResult(TypeReaderResult.FromSuccess(false));
                default:
                    return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Invalid value"));
            }
        }
    }
}