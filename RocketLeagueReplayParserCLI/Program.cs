using RocketLeagueReplayParserCLI;
using DNA_CLI_Framework.CommandHandlers;

namespace RocketLeagueReplayExtractorCLI
{
    /// <summary>
    /// Main Program Class for the Rocket League Replay Extractor CLI
    /// </summary>
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

        /// <summary>
        /// The Main Program Thread
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RocketLeagueReplayParser<ReplayParserDataManager> parser = new RocketLeagueReplayParser<ReplayParserDataManager>();
            parser.SetCommandHandler<FileOrDirectoryCommandHandler>();
            parser.RunApplication(args);
        }
    }
}
