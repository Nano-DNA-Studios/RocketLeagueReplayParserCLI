using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayParserCLI.Commands.Stats
{
    /// <summary>
    /// Handles the Saves Command, Displays the Number of Saves from each team or the individual Player
    /// </summary>
    internal class Saves : DisplayStatsCommand
    {
        /// <inheritdoc/>
        public override string Stat => GameProperties.Saves;
    }
}
