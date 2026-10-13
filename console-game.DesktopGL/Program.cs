using System;
using console_game.Core;

internal class Program
{
    /// <summary>
    /// The main entry point for the application. 
    /// This creates an instance of your game and calls the Run() method 
    /// </summary>
    /// <param name="args">Command-line arguments passed to the application.</param>
    [STAThread]
    private static void Main(string[] args)
    {
        using var game = new console_gameGame();
        game.Run();
    }
}