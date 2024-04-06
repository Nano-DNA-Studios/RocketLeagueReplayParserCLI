using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands
{
    internal class Goals : Command
    {
        public override string CommandName => "Goals";

        public override string CommandDescription => "Displays the Goals in the Replay";

        public override void Execute(string[] args)
        {
            ReplayParserDataManager data = ApplicationData<ReplayParserDataManager>.Instance();

            if (args.Length == 0)
            {
                Console.WriteLine($"Blue Team Goals: {data.Replay.BlueTeamGoals}");
                Console.WriteLine($"Orange Team Goals: {data.Replay.OrangeTeamGoals}");
                return;
            }

            if (args.Length == 1)
            {
                string playerName = args[0];

                PlayerInfo? player = data.Replay.Players.Where(p => p.PlayerName.Replace(" ", "") == playerName).FirstOrDefault();

                if (player == null)
                {
                    Console.WriteLine($"Player {playerName} not found in the Replay");
                    return;
                }

                Console.WriteLine($"{player.PlayerName} Has Scored {player.Goals} Goals");
            }
        }
    }
}
