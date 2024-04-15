using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands.Stats
{
    /// <summary>
    /// Handles the Shots Command, Displays the Number of Shots from each Team or the individual Player
    /// </summary>
    internal class Shots : DisplayStatsCommand
    {
        /// <inheritdoc/>
        public override GameStats Stat => GameStats.Shots;
    }
}
