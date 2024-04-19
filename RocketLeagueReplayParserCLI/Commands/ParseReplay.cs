using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using DNA_CLI_Framework;
using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands
{
    /// <summary>
    /// The Default Command for the CLI Tool, used to Load a Replay
    /// </summary>
    internal class ParseReplay : DefaultCommand
    {
        /// <summary>
        /// A Reference to the Replay Parser Data Manager
        /// </summary>
        ReplayParserDataManager Data => ApplicationData<ReplayParserDataManager>.Instance();

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            LoadReplay(args[0]);
        }

        /// <inheritdoc/>
        public override void ExecuteSolo(string[] args)
        {
            LoadReplay(args[0]);
            DisplayReplayInfo();
        }

        /// <summary>
        /// Displays a Summary of the Replays Info
        /// </summary>
        private void DisplayReplayInfo()
        {
            Console.WriteLine($"Replay File Name: {Data.Replay.GetReplayFileName()}");
            Console.WriteLine($"Replay Name: {Data.Replay.ReplayName}");

            DisplayTeamData(GameProperties.BlueTeamID);
            DisplayTeamData(GameProperties.OrangeTeamID);
            DisplayTeamPercentage(GameProperties.BallTouches, "Team Touches");
            DisplayTeamPercentage(GameProperties.BallPossessionTime, "Team Ball Possession Time");
        }

        private void DisplayTeamPercentage(string stat, string title)
        {
            float blueStat = Data.Replay.MatchRoster.Teams[GameProperties.BlueTeamID].GetTeamStat(stat);
            float orangeStat = Data.Replay.MatchRoster.Teams[GameProperties.OrangeTeamID].GetTeamStat(stat);

            ProgressBar.PrintProgressBar(blueStat, ProgressBar.BlueVsBlue, title, maxValue: blueStat + orangeStat, useBorder: true);
        }

        /// <summary>
        /// Displays the Entire Teams Data plus individual Player Data in the Score Board Format
        /// </summary>
        /// <param name="teamID"> The Team ID, 0 for Blue, 1 for Orange </param>
        private void DisplayTeamData(int teamID)
        {
            try
            {
                bool isBlue = teamID == GameProperties.BlueTeamID;
                Table scoreboardTable = new Table();

                Console.ForegroundColor = isBlue ? ConsoleColor.Blue : ConsoleColor.Red;
                scoreboardTable.SetTitle(isBlue ? "Blue Team Scoreboard" : "Orange Team Scoreboard");
                scoreboardTable.AddRow("PlayerName", "Score", "Goals", "Assists", "Saves", "Shots", "Touches", "Touch Percentage (%)", "Ball Possesion Time (s)", "Ball Possession Percentage (%)");
                foreach (PlayerInfo player in Data.Replay.MatchRoster.GetAllPlayers())
                {
                    if (player.Team == teamID)
                        scoreboardTable.AddRow(player.GetScoreboardInfo());
                }

                scoreboardTable.AddRow(["Total", .. Data.Replay.MatchRoster.Teams[teamID].GetTeamScoreboard()]);
                scoreboardTable.PrintTable();
            }
            finally
            {
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Loads the Replay from the given file path and sets the Replay in the DataManager
        /// </summary>
        /// <param name="filePath"> The File Path to the Replay </param>
        private void LoadReplay(string filePath)
        {
            string fullPath = Path.GetFullPath(filePath);

            if (!File.Exists(fullPath))
                return;

            if (Path.GetExtension(fullPath) != ".replay")
            {
                Console.WriteLine("Invalid File Type, Please Provide a Replay File");
                return;
            }

            Data.Replay = new Replay(fullPath);
            Data.ReplayPath = fullPath;
        }
    }
}
