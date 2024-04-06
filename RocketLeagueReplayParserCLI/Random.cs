/*
using DNARocketLeagueReplayParser.ReplayStructure;
using DNARocketLeagueReplayParser.ReplayStructure.Actors;
using DNARocketLeagueReplayParser.ReplayStructure.Frames;
using DNARocketLeagueReplayParser.ReplayStructure.Mapping;
using DNARocketLeagueReplayParser.ReplayStructure.UnrealEngineObjects;


namespace RocketLeagueReplayExtractorCLI
{
    public class ExtractReplayInfo
    {
        //Constants
        private const string CAR = "TAGame.Car_TA";
        private const string BALL = "TAGame.Ball_TA";
        private const string PLAYER_NAME = "Engine.PlayerReplicationInfo:PlayerName";
        private const string PLAYER_REPLICATION_INFO = "Engine.Pawn:PlayerReplicationInfo";
        private const string VEHICLE = "TAGame.CarComponent_TA:Vehicle";
        private const string BALL_HIT = "TAGame.Ball_TA:HitTeamNum"; //Or TAGame.GameEvent_Soccar_TA:bBallHasBeenHit
        private const int RIGID_BODY = 42;
        private const string PLAYER_STATS = "PlayerStats";

        /// <summary>
        /// A Game Objects State, Contains the RigidBody State, Frame Number, Time, and Actor ID
        /// </summary>
        public struct GameObjectState
        {
            /// <summary>
            /// Rigid Body State of the Object
            /// </summary>
            public RigidBodyState RigidBody { get; set; }

            /// <summary>
            /// The Frame Number the Object is in
            /// </summary>
            public int FrameNumber { get; set; }

            /// <summary>
            /// The Time in the Match the Object is in
            /// </summary>
            public float Time { get; set; }

            /// <summary>
            /// The Actor ID of the Object, for Players only
            /// </summary>
            public uint ActorID { get; set; }
        }

        /// <summary>
        /// The Settings for the JSON Serializer
        /// </summary>
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        /// <summary>
        /// The Entire Replay File Deserialized
        /// </summary>
        public PsyonixReplay ReplayInfo { get; private set; }

        /// <summary>
        /// List of Positions of the Ball over the course of the game
        /// </summary>
        public IEnumerable<GameObjectState> Ball { get; private set; }

        /// <summary>
        /// Stores the Positions of the Players at all Time
        /// </summary>
        public Dictionary<string, List<GameObjectState>> Players { get; private set; }

        /// <summary>
        /// Disctionary Mapping the Actor ID to the Player Name
        /// </summary>
        public Dictionary<uint, string> ActorIDToName { get; private set; }


        /// <summary>
        /// Initializes a new Replay Extractor, will deserialize the file provided and will convert it to a more suitable format
        /// </summary>
        /// <param name="path"></param>
        public ExtractReplayInfo(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(stream))
                ReplayInfo = PsyonixReplay.Deserialize(reader);

            ActorIDToName = GetActorToPlayerMap();

            ExtractRigidBodies();

            CreateNewReplayFile();

            Console.WriteLine("Done");
        }

        /// <summary>
        /// Saves the Info of Individual Components of the Replay
        /// </summary>
        private void SaveInfo()
        {
            string savePath = "D:\\NanoDNA Studios\\Programming\\DNA-Rocket-League-Replay-Parser\\DNARocketLeagueReplayParser\\ExtractedResources\\";

            string replayInfoJSON = JsonConvert.SerializeObject(ReplayInfo, settings);
            File.WriteAllText(savePath + "ReplayInfo.json", replayInfoJSON);

            string ballPositionJSON = JsonConvert.SerializeObject(Ball, settings);
            File.WriteAllText(savePath + "BallPosition.json", ballPositionJSON);

            string CarPositionsJSON = JsonConvert.SerializeObject(Players, settings);
            File.WriteAllText(savePath + $"CarPositions.json", CarPositionsJSON);
        }

        /// <summary>
        /// Creates a new <see cref="Replay"/> based on the Info Extracted from the <see cref="PsyonixReplay"/> file
        /// </summary>
        public void CreateNewReplayFile()
        {
            string replayName = (string)ReplayInfo.Properties["ReplayName"].Value;
            float recordFPS = (float)ReplayInfo.Properties["RecordFPS"].Value;
            int frameCount = ReplayInfo.Frames.Count;

            Replay replay = new Replay(replayName, recordFPS, frameCount);

            SetScoreLine(replay);

            replay.SetMapName((string)ReplayInfo.Properties["MapName"].Value);

            replay.SetMatchType((string)ReplayInfo.Properties["MatchType"].Value);

            AddPlayerStats(replay);

            AddFrames(replay);

            SaveReplay(replay);
        }

        /// <summary>
        /// Saves the Replay Info to a JSON File
        /// </summary>
        /// <param name="replay"></param>
        private void SaveReplay(Replay replay)
        {
            //Save the Replay Info
            string savePath = "D:\\NanoDNA Studios\\Programming\\DNA-Rocket-League-Replay-Parser\\DNARocketLeagueReplayParser\\ExtractedResources\\";

            string replayInfoJSON = JsonConvert.SerializeObject(replay, settings);
            File.WriteAllText(savePath + "DNAReplayInfo.json", replayInfoJSON);
        }

        /// <summary>
        /// Adds the Player Stats to the Replay
        /// </summary>
        /// <param name="replay"> The Replay </param>
        public void AddPlayerStats(Replay replay)
        {
            int playerID = 0;

            ArrayProperty playerStatsArray = (ArrayProperty)ReplayInfo.Properties[PLAYER_STATS];

            List<PropertyDictionary> playerStatsList = (List<PropertyDictionary>)playerStatsArray.Value;

            foreach (PropertyDictionary playerStat in playerStatsList)
            {
                PlayerInfo player = new PlayerInfo(playerID, (string)playerStat["Name"].Value);

                player.SetScore((int)playerStat["Score"].Value);
                player.SetGoals((int)playerStat["Goals"].Value);
                player.SetAssists((int)playerStat["Assists"].Value);
                player.SetSaves((int)playerStat["Saves"].Value);
                player.SetShots((int)playerStat["Shots"].Value);

                replay.AddPlayer(player);

                playerID++;
            }
        }

        /// <summary>
        /// Adds all the Frames to the Replay
        /// </summary>
        /// <param name="replay"> The new Replay </param>
        public void AddFrames(Replay replay)
        {
            float time = 0;
            float timePerFrame = 1f / replay.RecordFPS;

            for (int i = 0; i < replay.FrameCount; i++)
            {
                Frame frame = new Frame(i, time);

                replay.AddFrame(frame);

                time += timePerFrame;
            }

            foreach (GameObjectState ballState in Ball)
            {
                BallState ball = new BallState();

                ball.SetRigidBodyState(ConvertRigidBodyState(ballState.RigidBody));
                replay.Frames[ballState.FrameNumber].SetBallState(ball);
            }

            foreach (List<GameObjectState> playerStates in Players.Values)
            {
                foreach (GameObjectState playerState in playerStates)
                {
                    uint playerID = (uint)replay.Players.FindIndex(player => player.PlayerName == ActorIDToName[playerState.ActorID]);

                    PlayerState player = new PlayerState(playerID);

                    player.SetRigidBodyState(ConvertRigidBodyState(playerState.RigidBody));

                    replay.Frames[playerState.FrameNumber].AddPlayerState(player);
                }
            }
        }

        /// <summary>
        /// Converts an <see cref="RigidBodyState"/> to a <see cref="GameEngine.RigidBodyState"/>
        /// </summary>
        /// <param name="rigidBodyState"> The <see cref="RigidBodyState"/> </param>
        /// <returns> A Converted instance of a <see cref="GameEngine.RigidBodyState"/> </returns>
        private GameEngine.RigidBodyState ConvertRigidBodyState(RigidBodyState rigidBodyState)
        {
            bool sleeping = rigidBodyState.Sleeping;
            GameEngine.Vector3D position = new GameEngine.Vector3D(rigidBodyState.Position.X, rigidBodyState.Position.Y, rigidBodyState.Position.Z);
            GameEngine.Quaternion rotation = new GameEngine.Quaternion(((Quaternion)rigidBodyState.Rotation).X, ((Quaternion)rigidBodyState.Rotation).Y, ((Quaternion)rigidBodyState.Rotation).Z, ((Quaternion)rigidBodyState.Rotation).W);

            GameEngine.Vector3D? velocity;
            GameEngine.Vector3D? angularVelocity;

            if (rigidBodyState.LinearVelocity != null && rigidBodyState.AngularVelocity != null)
            {
                velocity = new GameEngine.Vector3D(rigidBodyState.LinearVelocity.X, rigidBodyState.LinearVelocity.Y, rigidBodyState.LinearVelocity.Z);
                angularVelocity = new GameEngine.Vector3D(rigidBodyState.AngularVelocity.X, rigidBodyState.AngularVelocity.Y, rigidBodyState.AngularVelocity.Z);
                return new GameEngine.RigidBodyState(sleeping, position, rotation, velocity, angularVelocity);
            }

            if (rigidBodyState.LinearVelocity != null)
            {
                velocity = new GameEngine.Vector3D(rigidBodyState.LinearVelocity.X, rigidBodyState.LinearVelocity.Y, rigidBodyState.LinearVelocity.Z);
                return new GameEngine.RigidBodyState(sleeping, position, rotation, linearVelocity: velocity);
            }

            if (rigidBodyState.AngularVelocity != null)
            {
                angularVelocity = new GameEngine.Vector3D(rigidBodyState.AngularVelocity.X, rigidBodyState.AngularVelocity.Y, rigidBodyState.AngularVelocity.Z);
                return new GameEngine.RigidBodyState(sleeping, position, rotation, angularVelocity: angularVelocity);
            }

            return new GameEngine.RigidBodyState(sleeping, position, rotation);
        }

        /// <summary>
        /// Sets the Games Scoreline at the end of the game
        /// </summary>
        /// <param name="replay"> The Replay to Set the Score line </param>
        private void SetScoreLine(Replay replay)
        {
            int blueScore;
            int orangeScore;

            blueScore = (int)ReplayInfo.Properties["Team0Score"].Value;
            orangeScore = (int)ReplayInfo.Properties["Team1Score"].Value;

            replay.SetEndgameScoreline(blueScore, orangeScore);
        }

        /// <summary>
        /// Extracts all the Rigid Body components from the Replay
        /// </summary>
        public void ExtractRigidBodies()
        {
            List<GameObjectState> ballPosition = new List<GameObjectState>();

            Dictionary<string, List<GameObjectState>> carPositions = new Dictionary<string, List<GameObjectState>>();

            uint frameNumber = 0;

            foreach (PsyonixFrame frame in ReplayInfo.Frames)
            {
                frameNumber++;
                foreach (ActorState actorState in frame.ActorStates)
                {
                    foreach (ActorStateProperty property in actorState.Properties.Values)
                    {
                        if (property.PropertyId == RIGID_BODY)
                        {

                            GameObjectState gameObjectState = new GameObjectState
                            {
                                RigidBody = (RigidBodyState)property.Data,
                                FrameNumber = (int)frameNumber,
                                Time = frame.Time,
                                ActorID = actorState.Id
                            };

                            if (ReplayInfo.Objects[property.GetClassCache().ObjectIndex] == CAR)
                            {
                                if (carPositions.TryGetValue(ActorIDToName[actorState.Id], out List<GameObjectState> positions))
                                    positions.Add(gameObjectState);
                                else
                                {
                                    carPositions.Add(ActorIDToName[actorState.Id], new List<GameObjectState>());
                                    carPositions[ActorIDToName[actorState.Id]].Add(gameObjectState);
                                }
                            }

                            if (ReplayInfo.Objects[property.GetClassCache().ObjectIndex] == BALL)
                                ballPosition.Add(gameObjectState);
                        }
                    }
                }
            }

            Ball = ballPosition;
            Players = carPositions;
        }

        /// <summary>
        /// Extracts the Info necessary to map an Actor / GameObject to a Player Name
        /// </summary>
        /// <returns> A Dictionary Map of ActorID to Player Name </returns>
        public Dictionary<uint, string> GetActorToPlayerMap()
        {
            Dictionary<uint, string> ActorToPlayerNameMap = new Dictionary<uint, string>();

            Dictionary<uint, string> IDtoName = new Dictionary<uint, string>();

            Dictionary<uint, int> ActorIDtoNameID = new Dictionary<uint, int>();

            foreach (PsyonixFrame frame in ReplayInfo.Frames)
            {
                foreach (ActorState actorState in frame.ActorStates)
                {
                    foreach (ActorStateProperty property in actorState.Properties.Values)
                    {
                        if (property.PropertyName == PLAYER_REPLICATION_INFO)
                        {
                            if (!ActorIDtoNameID.ContainsKey(actorState.Id))
                                ActorIDtoNameID.Add(actorState.Id, ((ActiveActor)property.Data).ActorId);
                        }

                        if (property.PropertyName == PLAYER_NAME)
                        {
                            if (!IDtoName.ContainsKey(actorState.Id))
                                IDtoName.Add(actorState.Id, (string)property.Data);
                        }
                    }
                }
            }

            foreach (uint actorID in ActorIDtoNameID.Keys)
                ActorToPlayerNameMap.Add(actorID, IDtoName[(uint)ActorIDtoNameID[actorID]]);

            return ActorToPlayerNameMap;
        }
    }
}
*/