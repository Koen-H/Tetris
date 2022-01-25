using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.Tetris;
using System.Collections;
using System.Collections.Generic;
public class MyGame : Game
{
    public Pivot playField = new Pivot();// this is where tetris plays,it drops down
    //public Pivot uI = new Pivot();// This is the UI
    private List<Button> buttonsList = new List<Button>();
    public Scene mainMenu;
    public GameManager gameManager;
    public string currentLevel;
    public SoundChannel backgroundSound;
    Boolean disableMusic;


   /* CONTROLS:
    *  A = LEFT 
    *  D = RIGHT
    *  Q = ROTATE LEFT
    *  E = ROTATE RIGHT
    *  W = HARD DROP
    *  S = SOFT DROP
    *  F = SAVE BLOCK
    *  SPACE = SAVE BLOCK
    *  M = MUTE AUDIO
    *  
    *  F1 = Diagnostics in console
    * 
    * 
    * */


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();

    }

    public MyGame() : base(1920, 1080, false,true, 640, 360)		// Create a window that's 640 x 360, with a resolution of 1920 x 1080
	{
        backgroundSound = new SoundChannel(0);
        LoadMainMenu();


    }
    public void LoadScene(GameObject scene)// used to load Scenes, and playfield levels
    {
        Console.WriteLine("Loading scene: " + scene);
        //
        List<GameObject> children = scene.GetChildren();
        foreach (GameObject child in children)
        {
            AddChild(child);
        }
    }
    public void LoadLevel(string loadLevel)
    {
        DestroyAll();
        Level level = new Level(loadLevel);
        currentLevel = loadLevel;
        Scene UI = new Scene("scene_level.tmx");// this is the ui
        LoadScene(UI);// load the UI
        LoadScene(level);// load the level
        gameManager.StartTetris();
    }
    public void LoadMainMenu()
    {
        gameManager = new GameManager(this);
        DestroyAll();
        mainMenu = new Scene("main_menu.tmx");
        LoadScene(mainMenu);
        gameManager.QuitTetris();
        PlayBackgroundMusic("Main_Menu_Music.wav");

    }

    public void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach(GameObject child in children)
        {
            child.Destroy();
        }
        
    }

    void Update()
    {
        gameManager.Update();
        foreach (Button button in buttonsList)
        {
            button.Update();
        }
        if (Input.GetKeyDown(Key.F1))
        {
            Console.WriteLine(GetDiagnostics());
        }
        if (Input.GetKeyDown(Key.M))
        {
            disableMusic = !disableMusic;
            if (backgroundSound.IsPlaying)
            {
                backgroundSound.Stop();
            }
        }
    }

    public void PlayBackgroundMusic(string musicToPlay)
    {
        if (!disableMusic) {
            if (backgroundSound.IsPlaying)
            {
                backgroundSound.Stop();
            }
            backgroundSound = new Sound(musicToPlay, true, true).Play();
        }
    }


}