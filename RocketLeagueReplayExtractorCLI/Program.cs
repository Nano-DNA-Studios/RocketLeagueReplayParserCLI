using RocketLeagueReplayExtractorCLI;
using RocketLeagueReplayParserAPI;

namespace RocketLeagueReplayExtractorCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string fullFilePath = Path.GetFullPath(args[0]);


            if (File.Exists(fullFilePath))
            {
                Replay replay = new Replay(fullFilePath);

                if (args[1] == "Goals")
                {
                    Console.WriteLine($"Blue Team Goals: {replay.BlueTeamGoals}");
                    Console.WriteLine($"Orange Team Goals: {replay.OrangeTeamGoals}");
                }

            }







            Console.WriteLine("Hello, World!");
            Console.WriteLine($"Args : {args[0]}");

            Console.Write("Enter Text: ");
            var text = Console.ReadLine();

            Console.WriteLine($"You entered: {text}");
        }
    }
}
