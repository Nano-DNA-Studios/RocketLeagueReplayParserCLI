using DNA.CLIFramework.Commands;
using DNA.CLIFramework.Data;

namespace RocketLeagueReplayParserCLI.Commands
{
    /// <summary>
    /// Saves the Replay Object that was extracted in a readable JSON Format
    /// </summary>
    internal class SaveReplay : Command
    {
        /// <summary>
        /// The Data Manager for the Replay Parser
        /// </summary>
        private ReplayParserDataManager Data => ApplicationData<ReplayParserDataManager>.Instance();

        /// <inheritdoc/>
        public override string CommandName => "SaveReplay";

        /// <inheritdoc/>
        public override string CommandDescription => "Saves the Replay Object that was extracted in a readable JSON Format";

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Data.Replay.SaveReplayAsJSON();
                return;
            }

            if (args.Length == 1)
            {
                Data.Replay.SaveReplayAsJSON(name: args[0]);
                return;
            }

            if (args.Length == 2)
            {
                Data.Replay.SaveReplayAsJSON(name: args[0], savePath: args[1]);
                return;
            }
        }
    }
}
