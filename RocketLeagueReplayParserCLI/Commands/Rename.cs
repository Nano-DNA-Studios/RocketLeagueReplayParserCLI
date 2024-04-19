using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands
{
    /// <summary>
    /// Handles the Rename Command
    /// </summary>
    internal class Rename : Command
    {
        /// <inheritdoc/>
        public override string CommandName => "Rename";

        /// <inheritdoc/>
        public override string CommandDescription => "Renames the Replay File to the Same Name Given by the Player";

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            ReplayParserDataManager data = ApplicationData<ReplayParserDataManager>.Instance();

            if (data.Replay.ReplayName == GameProperties.UnamedReplay)
            {
                data.Replay.RenameAndSave(GameProperties.UnamedReplay + data.UnnamedCounter);
                data.UnnamedCounter++;
            } else
                data.Replay.RenameAndSave();

            Console.WriteLine($"Replay Renamed From {Path.GetFileName(data.ReplayPath)} To {data.Replay.ReplayName}");
        }
    }
}
