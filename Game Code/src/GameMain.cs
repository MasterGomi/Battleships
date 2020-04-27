using static SwinGameSDK.SwinGame;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            // Opens a new Graphics Window
            OpenGraphicsWindow("Battle Ships", 800, 600);

            // Load Resources
            GameResources.LoadResources();
            
            PlayMusic(GameResources.GameMusic("Background"));
            SetMusicVolume(UtilityFunctions.VolumeLevel);

            // Game Loop
            do
            {
                GameController.HandleUserInput();
                GameController.DrawScreen();
            }
            //Only run the loop until a window close request is processed, or the game state becomes 'quitting'
            while (!WindowCloseRequested() && GameController.CurrentState != GameState.Quitting);

            StopMusic();

            // Free Resources and Close Audio, to end the program.
            GameResources.FreeResources();
        }
    }
}
