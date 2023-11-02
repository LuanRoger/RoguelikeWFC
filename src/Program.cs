using RoguelikeWFC;
using SadConsole.Configuration;

Settings.WindowTitle = "Roguelike Wave Function Colapse";

Builder gameStartup = new Builder()
    .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    .SetStartingScreen<RootScreen>()
    .IsStartingScreenFocused(true)
    .ConfigureFonts(true);

Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();