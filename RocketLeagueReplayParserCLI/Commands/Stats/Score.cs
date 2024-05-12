using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands.Stats
{
    /// <summary>
    /// Handles the Score Command, Displays the Score from each Team or the individual Player
    /// </summary>
    internal class Score : DisplayStatsCommand
    {
        /// <inheritdoc/>
        public override string Stat => GameProperties.Score;
    }
}
