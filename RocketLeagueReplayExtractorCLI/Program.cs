namespace RocketLeagueReplayExtractorCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine($"Args : {args[0]}");

            Console.Write("Enter Text: ");
            var text = Console.ReadLine();

            Console.WriteLine($"You entered: {text}");
        }
    }
}
