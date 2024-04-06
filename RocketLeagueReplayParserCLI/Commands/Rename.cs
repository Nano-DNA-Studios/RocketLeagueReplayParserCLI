using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using RocketLeagueReplayParserAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeagueReplayParserCLI.Commands
{
    internal class Rename : Command
    {
        public override string CommandName => "Rename";

        public override string CommandDescription => "Renames the Replay File to the Same Name Given by the Player";

        public override void Execute(string[] args)
        {
            ReplayParserDataManager data = ApplicationData<ReplayParserDataManager>.Instance();

            if (data.Replay.ReplayName == Replay.UNNAMED_REPLAY)
            {
                data.Replay.RenameAndSave(Replay.UNNAMED_REPLAY + data.UnnamedCounter);
                data.UnnamedCounter++;
            } else
                data.Replay.RenameAndSave();

            Console.WriteLine($"Replay Renamed From {Path.GetFileName(data.ReplayPath)} To {data.Replay.ReplayName}");


        }
    }
}
