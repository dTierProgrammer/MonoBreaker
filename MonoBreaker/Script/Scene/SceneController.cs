namespace MonoBreaker.Script.Scene;

public static class SceneController
{
    public static Scene CurrentScene = Scene.TITLE;

    public static void GoToTitle()
    {
        CurrentScene = Scene.TITLE;
    }

    public static void StartGame()
    {
        CurrentScene = Scene.PLAYING;
    }
    
    /*
    public static void PauseGame()
    {
        CurrentScene = Scene.PAUSE;
    }

    public static void GoToMenu()
    {
        CurrentScene = Scene.MENU;
    }

    public static void NextRound()
    {
        CurrentScene = Scene.NEXTROUND;
    }

    public static void EndGame()
    {
        CurrentScene = Scene.GAMEOVER;
    }
    */
}