using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
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

        private void DisplayReplayInfo()
        {
            Console.WriteLine($"Replay Name: {Data.Replay.ReplayName}");

            DisplayBlueTeamData();
            DisplayOrangeTeamData();
        }

        /// <summary>
        /// Displays the Blue Team Data
        /// </summary>
        private void DisplayBlueTeamData()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Blue Team");
                Console.WriteLine($"Goals: {Data.Replay.BlueTeamGoals}");
                Console.WriteLine($"Saves: {Data.Replay.GetTeamSaves(true)}");

                foreach (PlayerInfo player in Data.Replay.Players)
                {
                    if (player.Team == Replay.BLUE_TEAM)
                        Console.WriteLine(string.Format("{0}   | Score : {1} Goals : {2} Assists : {3} Saves : {4} Shots : {5}", player.GetScoreboardInfo()));
                }
            }
            finally
            {
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Displays the Orange Team Data
        /// </summary>
        private void DisplayOrangeTeamData()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Orange Team");
                Console.WriteLine($"Goals: {Data.Replay.OrangeTeamGoals}");
                Console.WriteLine($"Saves: {Data.Replay.GetTeamSaves(false)}");
                foreach (PlayerInfo player in Data.Replay.Players)
                {
                    if (player.Team == Replay.ORANGE_TEAM)
                        Console.WriteLine(string.Format("{0}   | Score : {1} Goals : {2} Assists : {3} Saves : {4} Shots : {5}", player.GetScoreboardInfo()));
                }
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
                return;

            Data.Replay = new Replay(fullPath);
            Data.ReplayPath = fullPath;
        }
    }
}
