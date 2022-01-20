using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.Tetris;
using System.Collections;
using System.Collections.Generic;
public class MyGame : Game
{
    public static Pivot playField;// this is where tetris plays,it drops down
    public static Pivot uI;// This is the UI
    private List<Button> buttonsList = new List<Button>();
    public Scene mainMenu;

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();

    }

    public MyGame() : base(1920, 1080, false,true)		// Create a window that's 800x600 and NOT fullscreen
	{

        mainMenu = new Scene("main_menu.tmx");
        LoadScene(mainMenu);


        //level = new Scene("Level");

        // LoadLevel();
        //levelSelect = new Scene("Level Select");

        /*
       playField = new Pivot();
       AddChild(playField);

       
       //LoadScene(mainMenu);
       GameManager.CreatePlayField(playField);

       GameManager.StartTetris(playField);
       //GameManager.grid[5,5].setOccupied("green_block.png");

       // this.loadScene = loadScene;
       // Create a small button canvas (EasyDraw):
       */



    }
    public void LoadScene(Scene scene)
    {
        Console.WriteLine("Loading scene: " + scene);
        DestroyAll();
        List<GameObject> children = scene.GetChildren();
        foreach (GameObject child in children)
        {
            //Console.WriteLine("object");
            AddChild(child);
        }
        //buttonsList = scene.GetButtons() ;
    }
    public void LoadLevel()
    {
        Scene levelUI = new Scene("scene_level");
        LoadScene(levelUI);// load a level
    }

	// For every game object, Update is called every frame, by the engine:
    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach(GameObject child in children)
        {
            child.Destroy();
        }
        
    }

    void Update()
    {
        GameManager.Update();
        foreach (Button button in buttonsList)
        {
            button.Update();
        }
    }



}