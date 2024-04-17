using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands.Stats
{
    /// <summary>
    /// Handles the Assists Command, Displays the Number of Assists from each Team or the individual Player
    /// </summary>
    internal class Assists : DisplayStatsCommand
    {
        /// <inheritdoc/>
        public override string Stat => GameProperties.Assists;
    }
}
