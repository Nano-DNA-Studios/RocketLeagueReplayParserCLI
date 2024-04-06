﻿using DNA_CLI_Framework;
using DNA_CLI_Framework.Data;

namespace RocketLeagueReplayParserCLI
{
    /// <summary>
    /// Class Representing the Rocket League Replay Parser CLI Application
    /// </summary>
    internal class RocketLeagueReplayParser<T> : CLIApplication<T> where T : DataManager, new()
    {
        /// <inheritdoc/>
        public override string ApplicationName => "Rocket League Replay Parser";


        public RocketLeagueReplayParser() : base()
        {
        }
    }
}
