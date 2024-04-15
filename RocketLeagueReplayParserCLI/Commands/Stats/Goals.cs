using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands.Stats
{
    /// <summary>
    /// Handles the Goals Command, Displays the Number of Goals from each Team or the individual Player
    /// </summary>
    internal class Goals : DisplayStatsCommand
    {
        /// <inheritdoc/>
        public override GameStats Stat => GameStats.Goals;
    }
}
