using DNA.CLIFramework.Commands;
using DNA.CLIFramework.Data;

namespace RocketLeagueReplayParserCLI.Commands
{
    /// <summary>
    /// Handles the SavePsyonixReplay Command, Saves the Psyonix Replay file that was extracted in a readable JSON Format
    /// </summary>
    internal class SavePsyonixReplay : Command
    {
        /// <summary>
        /// The Data Manager for the Replay Parser
        /// </summary>
        private ReplayParserDataManager Data => ApplicationData<ReplayParserDataManager>.Instance();

        /// <inheritdoc/>
        public override string CommandName => "SavePsyonixReplay";

        /// <inheritdoc/>
        public override string CommandDescription => "Saves the Psyonix Replay file that was extracted in a readable JSON Format";

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Data.Replay.SavePsyonixReplayAsJSON();
                return;
            }

            if (args.Length == 1)
            {
                Data.Replay.SavePsyonixReplayAsJSON(name: args[0]);
                return;
            }

            if (args.Length == 2)
            {
                Data.Replay.SavePsyonixReplayAsJSON(name: args[0], savePath: args[1]);
                return;
            }
        }
    }
}
