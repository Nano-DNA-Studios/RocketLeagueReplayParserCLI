using DNA.CLIFramework;
using DNA.CLIFramework.Commands;
using DNA.CLIFramework.Data;
using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands.Stats
{
    /// <summary>
    /// Intermediary Class for Displaying a Players Stats
    /// </summary>
    internal abstract class DisplayStatsCommand : Command
    {
        /// <summary>
        /// The Data Manager for the Replay Parser
        /// </summary>
        private ReplayParserDataManager Data => ApplicationData<ReplayParserDataManager>.Instance();

        /// <summary>
        /// The Stat Type to Display
        /// </summary>
        public abstract string Stat { get; }

        /// <inheritdoc/>
        public override string CommandName => Stat.ToString();

        /// <inheritdoc/>
        public override string CommandDescription => $"Displays the Number of {Stat} from each Team or the individual Player";

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                DisplayTeamStat(Stat);
                return;
            }

            if (args.Length == 1)
            {
                DisplayPlayerStat(args, Stat);
                return;
            }
        }

        /// <summary>
        /// Displays the Saves of a certain Player
        /// </summary>
        /// <param name="args"></param>
        private void DisplayPlayerStat(string[] args, string stat)
        {
            string playerName = args[0];

            PlayerInfo? player = Data.Replay.MatchRoster.GetAllPlayers().Where(p => p.PlayerName.Replace(" ", "") == playerName).FirstOrDefault();

            if (player == null)
            {
                Console.WriteLine($"Player {playerName} not found in the Replay");
                return;
            }

            Console.ForegroundColor = player.Team == GameProperties.BlueTeamID ? ConsoleColor.Blue : ConsoleColor.Red;
            Console.WriteLine($"{player.PlayerName} Has {player.GetStat<float>(stat)} {stat}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays each Teams individual Saves
        /// </summary>
        private void DisplayTeamStat(string stat)
        {
            Table statTable = new Table();

            statTable.SetTitle($"{stat}");
            statTable.AddRow("Blue Team", "Orange Team");
            statTable.AddRow($"{Data.Replay.MatchRoster.BlueTeam.GetTeamStat(stat)}", $"{Data.Replay.MatchRoster.OrangeTeam.GetTeamStat(stat)}");
            statTable.PrintTable();
        }
    }
}
