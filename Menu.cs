using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 

public class Menu : MonoBehaviour
{
	private GUISkin Hoofdmenu;
	private GUISkin SettingsMenu;
	public Texture2D[] tweeDskins = new Texture2D[1];
	public GUISkin GuiSkin;
	public GUISkin GuiSkinInput;
	public Texture2D background, LogoGroup, Document, Title;
 
	// knoppen
	public float buttonWidth = 250;
	public float buttonHeight = 30;
	public float closeWidth = 20;
	public float closeHeight = 20;
 
 	private float yOffset = 10;
    private float Spacing  = 25;
 	private float Gap;
 
 	// scherm
 	private bool MAINMENU = false;
 	private bool SETTINGS = false;
 	private bool RESOLUTION = false;
 	public bool INPUTMANAGER = false;
 	private bool VOLUME = false;
 
 	// sound options
	private float volume = 1.0f;
 
 	private const int SubMID = 0;
 	private Rect SettingsMRect = new Rect((Screen.width /2) + (335 /2), (Screen.height /2) - (300/2), 300, 300);
	private Rect MainMRect = new Rect((Screen.width /2) + (335 /2), (Screen.height /2) - (300/2), 300, 420);
 	private Rect InputMRect = new Rect((Screen.width /2) - (800 /2), (Screen.height /2) - (500/2), 800, 400);
 	private Rect ViewerRect;
 

 	string[] MenuArray = new string[]{"Start Game","Hall Of Fame","Hall Of Shame","Settings","Help","Credits","Quit Game",};
 	string[] SettingsArray = new string[]{"Resolution","Volume","Controls","Back"};
 	string[] InputArray = new string[]{"Back"};
 	string[] ResolutionArray = new string[]{"Back"};
 	string[] VolumeArray = new string[]{"Back"};
 	string clicked = "";
 
 	private static string _menuHeaderString = "label";
 
 	Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 200, 200);
 
 	private Vector2 Slider = Vector2.zero;
 
 	public static bool menuable = true;
 
 	int WarpIndex;
 
   	// Use this for initialization
 	void Start () 
 		{ 
  			MAINMENU = true;
 		}
 
 	void OnGUI()
 	{
  		if(background != null)
  		{
   			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),background); 
  		}
  
  		GUI.skin =GuiSkin;
  		if(MAINMENU)
        	{
          		MainMRect = GUI.Window(SubMID, MainMRect, Smenu, "Main Menu");
     		}
  		if (SETTINGS)
  			{
   				SettingsMRect = GUI.Window(SubMID, SettingsMRect, Settingsmenu, "Settings Menu");
  			}
  		if (RESOLUTION)
  			{
   				SettingsMRect = GUI.Window(SubMID, SettingsMRect, Resolutionmenu, "Resolution Settings"); 
  			}
  		if (INPUTMANAGER)
  			{
   				InputMRect = GUI.Window(SubMID, InputMRect, cInputManager, "Input Manager");
  			}
  		if (VOLUME)
  			{
   				SettingsMRect = GUI.Window(SubMID, SettingsMRect, VolumeSettings, "Volume");
  			}
 }
 
//=========================== De Update ===============================\\
 	void Update () 
 	{
		
 	}
 
// Warp Menu 
// ============== Het menu + Button array ============== \\
 	public void Smenu(int id)
 		{
  			GUI.skin = GuiSkin;
  			ViewerRect = new Rect(5, 15, 800, 400);
  			Slider = GUI.BeginScrollView(new Rect(ViewerRect), Slider, new Rect(0,25,0,Gap - 40));
  			Gap = yOffset + Spacing;
  
  			GUILayout.BeginVertical();
  			for(int i = 0; i < MenuArray.Length; i++)
        		{    
         			if(GUI.Button (new Rect (5, Gap, buttonWidth + 10, buttonHeight),new GUIContent(MenuArray[i])))
              		{
    					if(MenuArray[i] == "Start Game")
    					{
     						Application.LoadLevel("Lvl_X");
    					}
    					if(MenuArray[i] == "Hall of Fame")
    					{
     						Debug.Log("Hall of Fame");
    					}
					    if(MenuArray[i] == "Hall of Shame")
					    {
					     	Debug.Log("Hall of Fame");
					    }
					    if(MenuArray[i] == "Settings")
					    {
						     SETTINGS = true;
					    }
					    if(MenuArray[i] == "Help")
					    {
					     	Debug.Log("Help");
					    }
					    if(MenuArray[i] == "Credits")
					    {
					     	Application.LoadLevel("Credits");
					    }
					    if(MenuArray[i] == "Quit Game")
					    {
					     	Application.Quit();
					    }    
     				}
   				Gap += 55;
      			}
		  GUILayout.EndVertical();
		  GUI.EndScrollView();
 		}
 
// ============== Instellingen + Button array ============== \\
 	public void Settingsmenu(int id)
 		{
			GUI.skin = GuiSkin;
		  	ViewerRect = new Rect(5, 15, 300, 270);
		  	Slider = GUI.BeginScrollView(new Rect(ViewerRect), Slider, new Rect(0,25,0,Gap - 40));
		  	Gap = yOffset + Spacing;
  
  			for(int i = 0; i < SettingsArray.Length; i++)
        		{    
        			if(GUI.Button (new Rect (5, Gap, buttonWidth + 10, buttonHeight),new GUIContent(SettingsArray[i])))
            		{
    					if (SettingsArray[i] == "Resolution")
    					{
     						RESOLUTION = true;
    					}
       					if (SettingsArray[i] == "Controls")
    					{
     						INPUTMANAGER = true;
    					}
       					if (SettingsArray[i] == "Volume")
    					{
     						VOLUME = true;
    					}
       					if (SettingsArray[i] == "Back")
     					{
      						SETTINGS = false;
      						INPUTMANAGER = false;
     					}
    
   					}
  				Gap += 55;
  				}
  			GUI.EndScrollView();
 		}
	
// ============== Resolutie aanpassen + Button array ============== \\ 
	public void Resolutionmenu(int id)
 		{
			GUI.skin = GuiSkin;
			ViewerRect = new Rect(5, 20, 300, 270);
			Slider = GUI.BeginScrollView(new Rect(ViewerRect), Slider, new Rect(0,25,0,Gap - 40));
			Gap = yOffset + Spacing;
			
			GUILayout.BeginVertical(); 		
	        for (int x = 0; x < Screen.resolutions.Length;x++ )// hier kun je de resolutie instellen voor het spel
	        {
	        	if (GUILayout.Button(Screen.resolutions[x].width + "X" + Screen.resolutions[x].height))
	            {
	            	Screen.SetResolution(Screen.resolutions[x].width,Screen.resolutions[x].height,true);
	            }	
				
			Gap += 55;	
			for(int i = 0; i < ResolutionArray.Length; i++)
	        {		
	      		if(GUI.Button (new Rect (5, Gap, buttonWidth + 10, buttonHeight),new GUIContent(ResolutionArray[i])))
				{
					if (ResolutionArray[i] == "Back")
	        		{
	               	 	RESOLUTION = false;
	        		}
	        
				}
			}
		}
		
		GUILayout.EndVertical();
 		}
 
// ============== Input Manager + Button array ============== \\ 
 	public void cInputManager(int id)
 		{
  			GUI.skin = GuiSkinInput;
  			ViewerRect = new Rect(5, 20, 790, 380);
  			Slider = GUI.BeginScrollView(new Rect(ViewerRect), Slider, new Rect(0,25,0,Gap - 40));
  			Gap = yOffset + Spacing;
  
  			/// Button Header
  			GUILayout.BeginHorizontal();
  			if(GUILayout.Button("Actions")){
  			}
  			if(GUILayout.Button("Primary")){
  			}
  			if(GUILayout.Button("Secondary")){
  			}
  			GUILayout.EndHorizontal();
  			/// einde Header 
  
  			/// Toetseninstellingen Button 
  			GUILayout.BeginVertical("Title"); 
    		Gap+= 55;
    		for (int i =0; i< cInput.length; i++)
     			{
       				GUILayout.BeginHorizontal("textfield");
       				Gap += 30;// scroll balk
       				GUILayout.Label(cInput.GetText(i));
        			if (GUILayout.Button(cInput.GetText(i,1)))
        			{
         				cInput.ChangeKey(i,1); 
        			}
        			if(GUILayout.Button(cInput.GetText(i,2)))
        			{
         				cInput.ChangeKey(i,2); 
        			} 
       				GUILayout.EndHorizontal();
     			}
    		GUILayout.EndVertical();
  			/// Einde toetseninstellingen
  
  			/// Begin Footer Button
  			GUILayout.BeginHorizontal("textfield");
  			if (GUILayout.Button("Reset to Defaults"))
  			{
   				cInput.ResetInputs(); 
  			}
  			if (GUILayout.Button("Back"))
  			{
   				INPUTMANAGER = false;
    			Debug.Log("back"); 
  			}
  			GUILayout.EndHorizontal();
  			GUI.EndScrollView();
 		}
 
// ============== Volume + Button array ============== \\ 
 
 	public void VolumeSettings(int id)
 	{
  		Debug.Log("Volume menu");
  		
  		GUILayout.Label("Volume");
        volume = GUILayout.HorizontalSlider(volume ,0.0f,1.0f); // volume aanpassen
        AudioListener.volume = volume;
  
  
  		GUI.skin = GuiSkin;
  		ViewerRect = new Rect(5, 15, 300, 270);
  		Slider = GUI.BeginScrollView(new Rect(ViewerRect), Slider, new Rect(0,25,0,Gap - 40));
  		Gap = yOffset + Spacing;
  
  		for(int i = 0; i < VolumeArray.Length; i++)
        	{  
   				Gap+= 60;
        		if(GUI.Button (new Rect (5, Gap, buttonWidth + 10, buttonHeight),new GUIContent(VolumeArray[i])))
            	{    
    				if (VolumeArray[i] == "Back")
     				{
      					INPUTMANAGER = false;
      					VOLUME = false;
     
      					Debug.Log("back");
     				}
   				}
  			Gap += 60;
  			}
  		GUI.EndScrollView();  
 	}
}