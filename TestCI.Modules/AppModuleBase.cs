using Discord.Commands;
using System;

namespace TestCI.Modules
{
    public abstract class AppModuleBasee : ModuleBase<ICommandContext>, IDisposable
    {
        public abstract void Dispose();
    }
}
