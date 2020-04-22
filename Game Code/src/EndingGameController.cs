﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
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
            Rectangle toDraw = new Rectangle();
            string whatShouldIPrint;

            UtilityFunctions.DrawField(GameController.ComputerPlayer.PlayerGrid, GameController.ComputerPlayer, true);
            UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);

            toDraw.X = 0;
            toDraw.Y = 250;
            toDraw.Width = SwinGame.ScreenWidth();
            toDraw.Height = SwinGame.ScreenHeight();

            if (GameController.HumanPlayer.IsDestroyed)
                whatShouldIPrint = "YOU LOSE!";
            else
                whatShouldIPrint = "-- WINNER --";

            SwinGame.DrawText(whatShouldIPrint, Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, toDraw);
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
                HighScoreController.ReadHighScore(GameController.HumanPlayer.Score);
                GameController.EndCurrentState();
            }
        }
    }
}