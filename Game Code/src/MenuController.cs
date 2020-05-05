using System;
using SwinGameSDK;

namespace MyGame
{
    /// <summary>
    /// The menu controller handles the drawing and user interactions
    /// from the menus in the game. These include the main menu, game
    /// menu and the settings menu.
    /// </summary>
    static class MenuController
    {
        /// <summary>
        /// The menu structure for the game.
        /// <remarks>
        /// These are the text captions for the menu items.
        /// </remarks>
        /// </summary>
        private readonly static string[][] _menuStructure = new[] { new string[] { "PLAY", "SETUP", "VOLUME", "RULES", "CONTROLS", "SCORES", "QUIT" }, new string[] { "RETURN", "VOLUME", "SURRENDER", "QUIT" }, new string[] { "EASY", "MEDIUM", "HARD" } };

        private static int MENU_TOP = 575;
        private static int MENU_LEFT = 30;
        private static int MENU_GAP = 0;
        private static int BUTTON_WIDTH = 75;
        private static int BUTTON_HEIGHT = 15;
        private static int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;
        private static int TEXT_OFFSET = 0;
        private static int MENU_VALUE = 0;
        private const int MAIN_MENU = 0;
        private const int GAME_MENU = 1;
        private const int SETUP_MENU = 2;
        private const int VOLUME_MENU = 3;
        private const int MAIN_MENU_PLAY_BUTTON = 0;
        private const int MAIN_MENU_SETUP_BUTTON = 1;
        private const int MAIN_MENU_VOLUME_BUTTON = 2;
        private const int MAIN_MENU_RULES_BUTTON = 3;
        private const int MAIN_MENU_CONTROLS_BUTTON = 4;
        private const int MAIN_MENU_TOP_SCORES_BUTTON = 5;
        private const int MAIN_MENU_QUIT_BUTTON = 6;

        private const int SETUP_MENU_EASY_BUTTON = 0;
        private const int SETUP_MENU_MEDIUM_BUTTON = 1;
        private const int SETUP_MENU_HARD_BUTTON = 2;
        private const int SETUP_MENU_EXIT_BUTTON = 3;

        private const int GAME_MENU_RETURN_BUTTON = 0;
        private const int GAME_MENU_VOLUME_BUTTON = 1;
        private const int GAME_MENU_SURRENDER_BUTTON = 2;
        private const int GAME_MENU_QUIT_BUTTON = 3;

        private readonly static int VOLUME_BUTTON_X = MENU_LEFT + BUTTON_SEP * 2 + 17;
        private readonly static int VOLUME_BUTTON_Y = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT);
        private readonly static int VOLUME_BUTTON_SIZE = BUTTON_HEIGHT;
        private readonly static int VOLUME_BUTTON_GAP = 8;

        private readonly static Color MENU_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);
        private readonly static Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);

        /// <summary>
        /// Handles the processing of user input when the main menu is showing.
        /// </summary>
        public static void HandleMainMenuInput()
        {
            HandleMenuInput(MAIN_MENU, 0, 0);
        }

        /// <summary>
        /// Handles the processing of user input when the setup menu is showing.
        /// </summary>
        public static void HandleSetupMenuInput()
        {
            bool handled;
            handled = HandleMenuInput(SETUP_MENU, 1, 1);

            if (!handled)
                HandleMenuInput(MAIN_MENU, 0, 0);
        }

        

        /// <summary>
        /// Handle input in the game menu.
        /// <remarks>
        /// Player can return to the game, surrender, or quit entirely
        /// </remarks>
        /// </summary>
        public static void HandleGameMenuInput()
        {
            HandleMenuInput(GAME_MENU, 0, 0);
        }

        /// <summary>
        /// Handles input for the specified menu.
        /// <param name="menu">the identifier of the menu being processed</param>
        /// <param name="level">the vertical level of the menu</param>
        /// <param name="xOffset">the xoffset of the menu</param>
        /// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
        /// </summary>
        private static bool HandleMenuInput(int menu, int level, int xOffset)
        {
            if (SwinGame.KeyTyped(KeyCode.EscapeKey))
            {
                GameController.EndCurrentState();
                return true;
            }

            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                int i;
                for (i = 0; i <= _menuStructure[menu].Length - 1; i++)
                {
                    // IsMouseOver the i'th button of the menu
                    if (IsMouseOverMenu(i, level, xOffset))
                    {
                        PerformMenuAction(menu, i);
                        return true;
                    }
                }

                if (level > 0)
                    // none clicked - so end this sub menu
                    GameController.EndCurrentState();
            }

            return false;
        }

        public static void HandleVolumeMenuInput(int primaryMenu)
        {
            if (SwinGame.KeyTyped(KeyCode.EscapeKey))
            {
                GameController.EndCurrentState();
                return;
            }
            else if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                if(UtilityFunctions.IsMouseInRectangle(VOLUME_BUTTON_X, VOLUME_BUTTON_Y, VOLUME_BUTTON_SIZE, VOLUME_BUTTON_SIZE))
                {
                    // Plus button
                    UtilityFunctions.VolumeLevel += 0.1f;
                    Audio.SetMusicVolume(UtilityFunctions.VolumeLevel);
                }
                else if(UtilityFunctions.IsMouseInRectangle(VOLUME_BUTTON_X + VOLUME_BUTTON_SIZE + VOLUME_BUTTON_GAP,
                    VOLUME_BUTTON_Y, VOLUME_BUTTON_SIZE, VOLUME_BUTTON_SIZE))
                {
                    // Minus button
                    UtilityFunctions.VolumeLevel -= 0.1f;
                    Audio.SetMusicVolume(UtilityFunctions.VolumeLevel);
                }
                else
                {
                    // None clicked, so exit menu
                    GameController.EndCurrentState();
                    // And handle any clicks for primary menu
                    if(primaryMenu == MAIN_MENU)
                    {
                        HandleMainMenuInput();
                    }
                    else if(primaryMenu == GAME_MENU)
                    {
                        HandleGameMenuInput();
                    }
                }
            }

            //return false;
        }

        /// <summary>
        /// Draws the main menu to the screen.
        /// </summary>
        public static void DrawMainMenu()
        {
            DrawButtons(MAIN_MENU);
        }

        /// <summary>
        /// Draws the Game menu to the screen.
        /// </summary>
        public static void DrawGameMenu()
        {
            DrawButtons(GAME_MENU);
        }

        /// <summary>
        /// Draws the settings menu to the screen.
        /// <remarks>Also shows the main menu</remarks>
        /// </summary>
        public static void DrawSettings()
        {
            DrawButtons(MAIN_MENU);
            DrawButtons(SETUP_MENU, 1, 1);
        }

        /// <summary>
        /// Draws the volume settings menu to the screen.
        /// </summary>
        /// <remarks>Also shows whatever menu it was opened from</remarks>
        /// <param name="menu">The menu that the volume settings were opened from</param>
        public static void DrawVolumeSettings(int menu)
        {
            DrawButtons(menu);
            DrawVolumeButtons();
        }

        /// <summary>
        /// Draw the buttons associated with a top level menu.
        /// <param name="menu">the index of the menu to draw</param>
        /// </summary>
        private static void DrawButtons(int menu)
        {
            DrawButtons(menu, 0, 0);
        }

        /// <summary>
        /// Draws the menu at the indicated level.
        /// <param name="menu">the menu to draw</param>
        /// <param name="level">the level (height) of the menu</param>
        /// <param name="xOffset">the offset of the menu</param>
        /// <remarks>
        /// The menu text comes from the _menuStructure field. The level indicates the height
        /// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
        /// to allow the submenus to be positioned correctly.
        /// </remarks>
        /// </summary>
        private static void DrawButtons(int menu, int level, int xOffset)
        {
            int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
            Rectangle toDraw = new Rectangle();

            int i;
            for (i = 0; i <= _menuStructure[menu].Length - 1; i++)
            {
                int btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
                // SwinGame.FillRectangle(Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT)
                toDraw.X = btnLeft + TEXT_OFFSET;
                toDraw.Y = btnTop + TEXT_OFFSET;
                toDraw.Width = BUTTON_WIDTH;
                toDraw.Height = BUTTON_HEIGHT;
                SwinGame.DrawText(_menuStructure[menu][i], MENU_COLOR, Color.Black, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, toDraw);

                if (SwinGame.MouseDown(MouseButton.LeftButton) && IsMouseOverMenu(i, level, xOffset))
                    SwinGame.DrawRectangle(HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
            }
        }

        private static void DrawVolumeButtons()
        {
            int level = 1;
            int xOffset = 1;
            Rectangle toDraw = new Rectangle();
            int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
            int btnLeft = MENU_LEFT + BUTTON_SEP * xOffset;
            toDraw.X = btnLeft + TEXT_OFFSET - 12;
            toDraw.Y = btnTop + TEXT_OFFSET;
            toDraw.Width = BUTTON_WIDTH + 17;
            toDraw.Height = BUTTON_HEIGHT;
            
            int volumePercent = Convert.ToInt32(UtilityFunctions.VolumeLevel * 100);

            SwinGame.DrawText("VOLUME: %" + volumePercent.ToString(), MENU_COLOR, Color.Black, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, toDraw);
            SwinGame.DrawBitmap(GameResources.GameImage("Plus", GameResources.GameTheme), VOLUME_BUTTON_X, VOLUME_BUTTON_Y);
            SwinGame.DrawBitmap(GameResources.GameImage("Minus", GameResources.GameTheme), VOLUME_BUTTON_X + VOLUME_BUTTON_SIZE + VOLUME_BUTTON_GAP, VOLUME_BUTTON_Y);
        }

        /// <summary>
        /// Determined if the mouse is over one of the button in the main menu.
        /// <param name="button">the index of the button to check</param>
        /// <returns>true if the mouse is over that button</returns>
        /// </summary>
        private static bool IsMouseOverButton(int button)
        {
            return IsMouseOverMenu(button, 0, 0);
        }

        /// <summary>
        /// Checks if the mouse is over one of the buttons in a menu.
        /// <param name="button">the index of the button to check</param>
        /// <param name="level">the level of the menu</param>
        /// <param name="xOffset">the xOffset of the menu</param>
        /// <returns>true if the mouse is over the button</returns>
        /// </summary>
        private static bool IsMouseOverMenu(int button, int level, int xOffset)
        {
            int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
            int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

            return UtilityFunctions.IsMouseInRectangle(btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
        }

        /// <summary>
        /// A button has been clicked, perform the associated action.
        /// <param name="menu">the menu that has been clicked</param>
        /// <param name="button">the index of the button that was clicked</param>
        /// </summary>
        private static void PerformMenuAction(int menu, int button)
        {
            switch (menu)
            {
                case MAIN_MENU:
                    {
                        PerformMainMenuAction(button);
                        break;
                    }

                case SETUP_MENU:
                    {
                        PerformSetupMenuAction(button);
                        break;
                    }

                case GAME_MENU:
                    {
                        PerformGameMenuAction(button);
                        break;
                    }
            }
        }

        /// <summary>
        /// The main menu was clicked, perform the button's action.
        /// <param name="button">the button pressed</param>
        /// </summary>
        /// 

        public static void HandleControlersMenu()
        {
            drawControls();
            if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.EscapeKey) || SwinGame.KeyTyped(KeyCode.ReturnKey))
            {
                MENU_VALUE++;
                drawControls();
                if (MENU_VALUE > 2)
                {
                    MENU_VALUE = 0;
                    GameController.EndCurrentState();
                }
            }
              
        }
        public static void drawControls()
        {
            switch(MENU_VALUE)
            {
                case 0:
                    SwinGame.DrawBitmap(GameResources.GameImage("Tutorial1"), 0, 0);
                    break;
                case 1:
                    SwinGame.DrawBitmap(GameResources.GameImage("Tutorial2"), 0, 0);
                    break;
                case 2:
                    SwinGame.DrawBitmap(GameResources.GameImage("Tutorial3"), 0, 0);
                    break;
            }
         
        }
         public static void drawRules() {
            SwinGame.DrawBitmap(GameResources.GameImage("RulesPanel"), 0, 0);
    }

        private static void PerformMainMenuAction(int button)
        {
            switch (button)
            {
                case MAIN_MENU_PLAY_BUTTON:
                    {
                        GameController.StartGame();
                        break;
                    }

                case MAIN_MENU_SETUP_BUTTON:
                    {
                        GameController.AddNewState(GameState.AlteringSettings);
                        break;
                    }
                case MAIN_MENU_RULES_BUTTON:
                    {
                        GameController.AddNewState(GameState.Rules);
                        break;
                    }
                case MAIN_MENU_CONTROLS_BUTTON:
                    {
                        GameController.AddNewState(GameState.Controls);
                        break;
                    }

                case MAIN_MENU_VOLUME_BUTTON:
                    {
                        GameController.AddNewState(GameState.AlteringVolume);
                        break;
                    }

                case MAIN_MENU_TOP_SCORES_BUTTON:
                    {
                        GameController.AddNewState(GameState.ViewingHighScores);
                        break;
                    }

                case MAIN_MENU_QUIT_BUTTON:
                    {
                        GameController.EndCurrentState();
                        break;
                    }
            }
        }

        /// <summary>
        /// The setup menu was clicked, perform the button's action.
        /// <param name="button">the button pressed</param>
        /// </summary>
        private static void PerformSetupMenuAction(int button)
        {
            switch (button)
            {
                case SETUP_MENU_EASY_BUTTON:
                    {
                        GameController.SetDifficulty(AIOption.Easy);
                        break;
                    }

                case SETUP_MENU_MEDIUM_BUTTON:
                    {
                        GameController.SetDifficulty(AIOption.Medium);
                        break;
                    }

                case SETUP_MENU_HARD_BUTTON:
                    {
                        GameController.SetDifficulty(AIOption.Hard);
                        break;
                    }
            }
            // Always end state
            GameController.EndCurrentState();
        }

        /// <summary>
        /// The game menu was clicked, perform the button's action.
        /// <param name="button">the button pressed</param>
        /// </summary>
        private static void PerformGameMenuAction(int button)
        {
            switch (button)
            {
                case GAME_MENU_RETURN_BUTTON:
                    {
                        GameController.EndCurrentState();
                        break;
                    }

                case GAME_MENU_VOLUME_BUTTON:
                    {
                        GameController.AddNewState(GameState.AlteringVolume);
                        break;
                    }

                case GAME_MENU_SURRENDER_BUTTON:
                    {
                        GameController.EndCurrentState(); // end game menu
                        GameController.EndCurrentState(); // end game
                        break;
                    }

                case GAME_MENU_QUIT_BUTTON:
                    {
                        GameController.AddNewState(GameState.Quitting);
                        break;
                    }
            }
        }
    }
}