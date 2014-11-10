using System;
using GameNetwork;
using JungleTribesImplementation;

namespace JungleTribes
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            MessageManager.addMessageType(typeof(MessageInitConnection));
            MessageManager.addMessageType(typeof(MessageHostConnected));
            MessageManager.addMessageType(typeof(MessageHostDisconnected));
            MessageManager.addMessageType(typeof(MessagePing));
            MessageManager.addMessageType(typeof(MessageChat));
            MessageManager.addMessageType(typeof(MessageUpdateElements));
            MessageManager.addMessageType(typeof(MessageUserCommand));
            MessageManager.addMessageType(typeof(MessageSelectionHero));
            MessageManager.addMessageType(typeof(MessageSkillLaunched));
            MessageManager.addMessageType(typeof(MessageHeroTeam));
            ElementManager.initialize();
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

