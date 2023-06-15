using System;

namespace faxnocapBPbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "firestoreConfig.json");
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
