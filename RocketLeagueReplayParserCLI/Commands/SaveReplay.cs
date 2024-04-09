using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeagueReplayParserCLI.Commands
{
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
