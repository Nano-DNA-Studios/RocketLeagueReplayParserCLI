using DNA_CLI_Framework.Data;
using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI
{
    /// <summary>
    /// The Specialised Data Manager for the Rocket League Replay Parser CLI Application
    /// </summary>
    internal class ReplayParserDataManager : DataManager
    {
        /// <summary>
        /// The Loaded instance of the Replay to Analyze
        /// </summary>
        public Replay Replay { get; set; }

        /// <summary>
        /// The Path to the Loaded Replay
        /// </summary>
        public string ReplayPath { get; set; }

        /// <inheritdoc/>
        public override string COMMAND_PREFIX => DEFAULT_COMMAND_PREFIX;

        /// <summary>
        /// A Counter for the Unnamed Files
        /// </summary>
        public int UnnamedCounter = 0;
    }
}
