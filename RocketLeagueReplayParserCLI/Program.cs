using RocketLeagueReplayParserCLI;
using DNA_CLI_Framework.CommandHandlers;

namespace RocketLeagueReplayExtractorCLI
{
    /// <summary>
    /// Main Program Class for the Rocket League Replay Extractor CLI
    /// </summary>
    internal class Program
    {
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
