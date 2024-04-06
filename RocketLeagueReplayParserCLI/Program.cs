using System.Reflection;
using RocketLeagueReplayParserCLI;
using DNA_CLI_Framework.CommandHandlers;

namespace RocketLeagueReplayExtractorCLI
{
    internal class Program
    { 
        //Commands to work on for now

        //Goals - Get the number of goals for each team, next argument is player name which will be used to get the goals for that player
        //Saves - Get the number of saves for each team, next argument is player name which will be used to get the saves for that player
        //Assists - Get the number of assists for each team, next argument is player name which will be used to get the assists for that player
        //Score - Get the score for each player, next argument is player name which will be used to get the score for that player
        //Shots - Get the number of shots for each team, next argument is player name which will be used to get the shots for that player

        //Possesion / Ball Touches?
        //Possession - Get the possession for each team, next argument is player name which will be used to get the possession for that player

        static void Main(string[] args)
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "RocketLeagueReplayParserAPI").ToList().ForEach(Console.WriteLine);

            string[] testArgs = { "Replays" };

            ReplayParserDataManager dataManager = new ReplayParserDataManager();

            RocketLeagueReplayParser<ReplayParserDataManager> parser = new RocketLeagueReplayParser<ReplayParserDataManager>();
            parser.SetCommandHandler<FileOrDirectoryCommandHandler>();

            if (args.Length == 0)
                parser.RunApplication(testArgs);
            else
                parser.RunApplication(args);

        }
    }
}
