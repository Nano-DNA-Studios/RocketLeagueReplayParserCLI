using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using static RocketLeagueReplayParserAPI.Replay;

namespace RocketLeagueReplayParserCLI.Commands
{
    internal class SaveReplay : Command
    {
        /// <summary>
        /// The Data Manager for the Replay Parser
        /// </summary>
        private ReplayParserDataManager Data => ApplicationData<ReplayParserDataManager>.Instance();

        /// <inheritdoc/>
        public override string CommandName => "SaveReplay";

        /// <inheritdoc/>
        public override string CommandDescription => "Saves the Replay Object that was extracted in a readable JSON Format";

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            int lastFrameNumber = 0;
            foreach (GameObjectState gameObjectState in Data.Replay.BallPosition)
            {
                try
                {

                    if (gameObjectState.RigidBody != null && gameObjectState.RigidBody.LinearVelocity != null)
                    {
                        float x, y, z;
                        double v;

                        x = gameObjectState.RigidBody.LinearVelocity.X / 10000;
                        y = gameObjectState.RigidBody.LinearVelocity.Y / 10000;
                        z = gameObjectState.RigidBody.LinearVelocity.Z / 10000;

                        v = Math.Sqrt(x * x + y * y + z * z);

                        v = v * 3.6;

                        Console.WriteLine($"Ball Speed (km/h) at frame ({gameObjectState.FrameNumber}): {v}   (Frame Delta = {gameObjectState.FrameNumber - lastFrameNumber}) (Time : {gameObjectState.Time})  (X :{x}   Y :{y}  Z :{z})   (Pos: {gameObjectState.RigidBody.Position.ToString()}");

                        lastFrameNumber = gameObjectState.FrameNumber;




                    }

                }
                finally
                {

                }

            }

            foreach (BallHit ballHit in Data.Replay.BallHits)
            {
                if (ballHit.team ==0)
                    Console.WriteLine($"Ball Hit at frame ({ballHit.frameNumber}) by Blue Team");
                else
                    Console.WriteLine($"Ball Hit at frame ({ballHit.frameNumber}) by Orange Team");
            }


            if (args.Length == 0)
            {
                Data.Replay.SaveReplayAsJSON();
                return;
            }

            if (args.Length == 1)
            {
                Data.Replay.SaveReplayAsJSON(name: args[0]);
                return;
            }

            if (args.Length == 2)
            {
                Data.Replay.SaveReplayAsJSON(name: args[0], savePath: args[1]);
                return;
            }
        }
    }
}
