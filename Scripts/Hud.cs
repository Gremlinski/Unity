using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {
	GUISkin myHUDSkin;
	
	static int health= 100;
	static int maxhealth= 150;
	static int healthpercentage;
	
	
	private int colorRed=250;
	private int colorGreen=0;
	
	static bool coinsCollected= false;

	public GUISkin myInvSkin;
	
	static bool  showInventory = false;
	
	public AudioClip showInvSound;
	AudioSource audio;
	
	private int indicator = 440;
	private int selection = 1;
	
	private bool  submenu = false;
	
	private string invSelectionString = "\n\t\tItems:"; // Example only
	
	void  Awake (){
		//CharStats.Con = 12; // Had to include this here because when it's called earlier than stats, it ends up being previous game's Health = Con*10
		health = 10;
		maxhealth = health;
	}
	void  Update (){
		if(Input.GetKeyDown(KeyCode.I) && showInventory == false)
		{
			audio.PlayOneShot(showInvSound);
			showInventory = true;
		}
		else if(Input.GetKeyDown(KeyCode.I) && showInventory == true)
		{
			audio.PlayOneShot(showInvSound);
			showInventory = false;
		}
		
		if(Input.GetKeyDown(KeyCode.W) && indicator != 440 && showInventory == true || Input.GetKeyDown(KeyCode.UpArrow) && indicator != 440 && showInventory == true)
		{
			indicator -= 20;
			selection = (indicator-100)/20;
		}
		if(Input.GetKeyDown(KeyCode.S) && indicator != 660 && showInventory == true || Input.GetKeyDown(KeyCode.DownArrow) && indicator != 660 && showInventory == true)
		{
			indicator += 20;
			selection = (indicator-100)/20;
		}
	}
	
	void  OnGUI (){
		GUI.skin = myInvSkin;

		GUI.skin = myHUDSkin;
		healthpercentage = (health*100)/maxhealth;
		GUI.contentColor = new Color((colorRed-healthpercentage*2.5f)*0.01f,(colorGreen+healthpercentage*2.5f)*0.01f,0,1);
		
		GUI.Box ( new Rect(Screen.width/2-350,10,200,200), "Health: " + health + "<color=#00ff00ff>/" + maxhealth + "</color>");
		
		GUI.Label ( new Rect(Screen.width/2-150,60,300,100), "You hit " + "MONSTERNAME" + " for " + 2 + " hitpoints.");
		
		GUI.Label ( new Rect(Screen.width/2-150,80,300,100), "You collected " + 3 + " gold coins.");
		
		
		//GUI.Label ( new Rect(10,120,200,200), "\n\nX: " + simplemonster.distanceToPlayer.x.ToString() + "\nY: " + simplemonster.distanceToPlayer.y.ToString() + "\nZ: " + simplemonster.distanceToPlayer.z.ToString() + "\nMoves: " + Movement.monstersmoves);
		
		//GUI.Box ( new Rect(Screen.width - 100,0,100,50), "Hello");
		//GUI.Box ( new Rect(0,Screen.height - 50,100,50), "Bottom-left");
		//GUI.Box ( new Rect(Screen.width - 100,Screen.height - 50,100,50), "Bottom-right");
		
		if(showInventory == true) // Inventory
		{
			if(submenu ==  false)
			{
				
				GUI.Box	( new Rect(Screen.width/2-50,400,500,350), "\t\tInventory" +
				         "\n" +
				         "\n\t\tItems:" // This needs to display a string of items
				         );
				
				GUI.Label ( new Rect(Screen.width/2-350,400,500,350),
				           "\n\nLeft Hand	\t:" + // This will most likely need to go to a lable so it's not a part of the same list
				           "\nRight Hand	:"  +
				           "\nHead			\t:" + 
				           "\nNeck			\t:" + 
				           "\nChest		\t\t:" + 
				           "\nShoulders	\t:" + 
				           "\nArms			\t:" + 
				           "\nWaist		\t\t:" +
				           "\nLegs			\t\t:" +
				           "\nBoots		\t\t:" +
				           "\nLeft Finger	: " +
				           "\nRight Finger	: " );
				
				GUI.Label ( new Rect(Screen.width/2-390,indicator,400,368), "\t->");
				
			}
		}
	}

}