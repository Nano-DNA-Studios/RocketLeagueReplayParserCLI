using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands
{
    /// <summary>
    /// Handles the Goals Command
    /// </summary>
    internal class Goals : Command
    {
        private ReplayParserDataManager Data => ApplicationData<ReplayParserDataManager>.Instance();

        /// <inheritdoc/>
        public override string CommandName => "Goals";

        /// <inheritdoc/>
        public override string CommandDescription => "Displays the Goals in the Replay";

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                DisplayTeamGoals();
                return;
            }

            if (args.Length == 1)
            {
                DisplayPlayerGoals(args);
                return;
            }
        }

        /// <summary>
        /// Displays the Goals of a certain Player
        /// </summary>
        /// <param name="args"></param>
        private void DisplayPlayerGoals(string[] args)
        {
            string playerName = args[0];

            PlayerInfo? player = Data.Replay.Players.Where(p => p.PlayerName.Replace(" ", "") == playerName).FirstOrDefault();

            if (player == null)
            {
                Console.WriteLine($"Player {playerName} not found in the Replay");
                return;
            }

            Console.ForegroundColor = player.Team == Replay.BLUE_TEAM ? ConsoleColor.Blue : ConsoleColor.Red;
            Console.WriteLine($"{player.PlayerName} Has Scored {player.Goals} Goals");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the Team Goals with Colors
        /// </summary>
        private void DisplayTeamGoals()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Blue Team Goals: {Data.Replay.BlueTeamGoals}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Orange Team Goals: {Data.Replay.OrangeTeamGoals}");

            Console.ResetColor();
        }
    }
}
