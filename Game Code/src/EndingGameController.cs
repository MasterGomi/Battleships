using SwinGameSDK;

namespace MyGame
{
    /// <summary>
    /// The EndingGameController is responsible for managing the interactions at the end
    /// of a game.
    /// </summary>
    static class EndingGameController
    {
        /// <summary>
        /// Draw the end of the game screen, shows the win/lose state.
        /// </summary>
        public static void DrawEndOfGame()
        {
            const int TITLE_TOP = 100;
            const int MESSAGE_TOP = 400;

            Color messageColor;

            string resultTitle = "";
            string resultMessage = "";

            Rectangle resultTitleRectangle = new Rectangle
            {
                X = 0,
                Y = TITLE_TOP,
                Width = SwinGame.ScreenWidth(),
                Height = SwinGame.ScreenHeight()
            };

            Rectangle resultMessageRectangle = new Rectangle
            {
                X = 0,
                Y = MESSAGE_TOP,
                Width = SwinGame.ScreenWidth(),
                Height = SwinGame.ScreenHeight()
            };


            if (GameController.HumanPlayer.IsDestroyed)
            {
                resultTitle = "-- LOSER --";
                resultMessage = "Better luck next time!";

                messageColor = Color.Red;
            }
            else
            {
                resultTitle = "-- WINNER --";
                resultMessage = "Congratulations on the win!";

                messageColor = Color.Green;
            }

            SwinGame.DrawText(resultTitle, Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, resultTitleRectangle);
            SwinGame.DrawText(resultMessage, Color.White, messageColor, GameResources.GameFont("Arial"), FontAlignment.AlignCenter, resultMessageRectangle);
        }

        /// <summary>
        /// Handle the input during the end of the game. Clicking the left mouse
        /// button, pressing enter key or pressing escape key will result in it
        /// reading in the highscore.
        /// </summary>
        public static void HandleEndOfGameInput()
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.ReturnKey) || SwinGame.KeyTyped(KeyCode.EscapeKey))
            {
                GameController.EndCurrentState();
                HighScoreController.ReadHighScore(GameController.HumanPlayer.Score);
            }
        }
    }
}