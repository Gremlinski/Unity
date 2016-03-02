using UnityEngine;

public class AutoMap : MonoBehaviour
{
    //Hud skin
    public GUISkin myAutoMapSkin = null;

    //Inventory on or off
    public static bool showAutoMap = true;

    public static int[,] AutoMapArray = null;

    //public Texture2D mapTileTexture;
    public Texture2D borderTexture;
    public Texture2D wallTileTexture;
    public Texture2D playerTileTexture;
    public Texture2D enemyTileTexture;
    public Texture2D stairsTileTexture;

    public Texture2D increaseTexture;
    public Texture2D decreaseTexture;

    public LayerMask wallLayerMask;
    public LayerMask stairsLayerMask;

    Rect mapTile;
    Rect playerMarker;
    Rect enemyMarker;

    //This is the set of the square size on the automap
    private int sizeOfSquare = 8;

    public static string autoMapLocation = "BottomRight";

    public Rect automapWindowRect = new Rect(20, 20, 640, 480);

    private int offset = 18;

    //private bool setAutomapPos = true;

    public void AutoMapNewLevel()
    {
        AutoMapArray = GameObject.Find("Terrain").GetComponent<TerrainRenderer>().TerrainMap;
        ResetAutomapPos();
    }

    void ResetAutomapPos()
    {
        automapWindowRect.x = Screen.width - AutoMapArray.GetLength(0) * sizeOfSquare - offset;
        automapWindowRect.y = Screen.height - AutoMapArray.GetLength(1) * sizeOfSquare - offset;

        //Automap was reset and doesn't need to be any more
        //setAutomapPos = false;
    }

    void AutomapWindow(int windowID)
    {
        //Used for top left
        int horizontalMapOffset = 200;

        //Reference:
        //0 is empty
        //1 is not seen wall
        //2 is seen wall
        //3 is seen stairs
        //

        automapWindowRect.width = AutoMapArray.GetLength(0) * sizeOfSquare + offset;
        automapWindowRect.height = AutoMapArray.GetLength(1) * sizeOfSquare + offset;

        //Draw a box around the automap
        GUI.skin.label.normal.background = borderTexture;
        GUI.Label(new Rect(automapWindowRect.width - AutoMapArray.GetLength(0) * sizeOfSquare - offset, automapWindowRect.height - AutoMapArray.GetLength(1) * sizeOfSquare - offset, AutoMapArray.GetLength(0) * sizeOfSquare + 24, AutoMapArray.GetLength(1) * sizeOfSquare + 24), "");

        //Walls and stairs automap markers

        //For all spaces on the map vertically
        for (int z = 0; z < AutoMapArray.GetLength(1); z++)
        {
            //For all spaces on the map horizontally
            for (int x = 0; x < AutoMapArray.GetLength(0); x++)
            {
                if (autoMapLocation == "TopLeft")
                {
                    //Calculate the square based on the square size
                    mapTile = new Rect(x * sizeOfSquare + horizontalMapOffset, -z * sizeOfSquare + AutoMapArray.GetLength(1) * sizeOfSquare, sizeOfSquare, sizeOfSquare);
                } else if (autoMapLocation == "BottomRight")
                {
                    //Calculate the square based on the square size
                    mapTile = new Rect(automapWindowRect.width + (x * sizeOfSquare) - AutoMapArray.GetLength(0) * sizeOfSquare - 14, -z * sizeOfSquare + automapWindowRect.height - 14, sizeOfSquare, sizeOfSquare);
                }

                //If the automap coordinate is equal to 2
                if (AutoMapArray [x, z] == 2)
                {
                    //Draw the wall map tile
                    GUI.DrawTexture(mapTile, wallTileTexture);
                }

                //If the automap coordinate is equal to 3
                if (AutoMapArray [x, z] == 3)
                {
                    //Draw the stairs map tile
                    GUI.DrawTexture(mapTile, stairsTileTexture);
                }
            }
        }

        //Player automap marker

        if (autoMapLocation == "TopLeft")
        {
            //Player location map tile
            playerMarker = new Rect(Player.currentPosition.x * sizeOfSquare + horizontalMapOffset, -Player.currentPosition.z * sizeOfSquare + AutoMapArray.GetLength(1) * sizeOfSquare, sizeOfSquare, sizeOfSquare);
        } else if (autoMapLocation == "BottomRight")
        {
            //Calculate the square based on the square size
            playerMarker = new Rect(automapWindowRect.width + (Player.currentPosition.x * sizeOfSquare) - AutoMapArray.GetLength(0) * sizeOfSquare - 14, -Player.currentPosition.z * sizeOfSquare + automapWindowRect.height - 14, sizeOfSquare, sizeOfSquare);
            //mapTile = new Rect(Screen.width + (x * sizeOfSquare) - AutoMapArray.GetLength(0) * sizeOfSquare, -z * sizeOfSquare + Screen.height, sizeOfSquare, sizeOfSquare);
        }

        //Set label texture to player map tile texture
        GUI.skin.label.normal.background = playerTileTexture;

        //Draw the player map tiles
        GUI.Label(playerMarker, "");

        //Enemy automap marker

        //For all enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //If enemy is seen by the player
            if (enemy.GetComponent<Enemy>().enemyIsSeenByPlayer)
            {
                //Put the enemy marker on the automap
                if (autoMapLocation == "TopLeft")
                {
                    //Enemy location map tile based on the square size
                    enemyMarker = new Rect(enemy.transform.position.x * sizeOfSquare + horizontalMapOffset, -enemy.transform.position.z * sizeOfSquare + AutoMapArray.GetLength(1) * sizeOfSquare, sizeOfSquare, sizeOfSquare);
                } else if (autoMapLocation == "BottomRight")
                {
                    //Enemy location map tile based on the square size
                    enemyMarker = new Rect(automapWindowRect.width + (enemy.transform.position.x * sizeOfSquare) - AutoMapArray.GetLength(0) * sizeOfSquare - 14, -enemy.transform.position.z * sizeOfSquare + automapWindowRect.height - 14, sizeOfSquare, sizeOfSquare);
                }
                
                //Set label texture to player map tile texture
                GUI.skin.label.normal.background = enemyTileTexture;
                
                //Draw the player map tiles
                GUI.Label(enemyMarker, "");
            }
        }

        //Increase button texture and style
        GUIStyle increaseSizeStyle = new GUIStyle();
        increaseSizeStyle.border = new RectOffset(4, 4, 4, 4);
        increaseSizeStyle.normal.background = increaseTexture;
        increaseSizeStyle.normal.textColor = Color.white;
        increaseSizeStyle.alignment = TextAnchor.MiddleCenter;

        //Decrease button texture and style
        GUIStyle decreaseSizeStyle = new GUIStyle();
        decreaseSizeStyle.border = new RectOffset(4, 4, 4, 4);
        decreaseSizeStyle.normal.background = decreaseTexture;
        decreaseSizeStyle.normal.textColor = Color.white;
        decreaseSizeStyle.alignment = TextAnchor.MiddleCenter;

        //AutoMap increase and decrease buttons size
        int buttonSize = 20;

        //Increase and decrease buttons' offset from the corner
        int buttonOffsetCorner = 4;

        //If increase size button is pressed
        if (GUI.Button(new Rect(automapWindowRect.width - buttonSize - buttonOffsetCorner, buttonOffsetCorner, buttonSize, buttonSize), "", increaseSizeStyle))
        {
            //Increase the size of the square
            sizeOfSquare += 1;

            //Move the map window position so mouse remains in the button
            automapWindowRect.x -= TerrainMap.mapWidth;
        }

        //If decrease size button is pressed
        if (GUI.Button(new Rect(automapWindowRect.width - buttonSize * 2 - buttonOffsetCorner, buttonOffsetCorner, buttonSize, buttonSize), "", decreaseSizeStyle))
        {
            //Decrease the size of the square
            sizeOfSquare -= 1;

            //Move the map window position so mouse remains in the button
            automapWindowRect.x += TerrainMap.mapWidth;
        }

        //5000 is so no matter how far horizontally window goes, it's still draggable
        GUI.DragWindow(new Rect(0, 0, 5000, 5000));
    }

    // Use this for initialization
    void OnGUI()
    {
        //Automap skin
        GUI.skin = myAutoMapSkin;

        if (AutoMapArray != null && showAutoMap == true)
        {
            automapWindowRect = GUI.Window(0, automapWindowRect, AutomapWindow, "");
        }
    }

    public void UpdateAutoMap()
    {
        //For each wall
        foreach (GameObject wallObject in GameObject.FindGameObjectsWithTag("Wall"))
        {
            //Declare hit variable
            RaycastHit found;

            //Get position
            Vector3 wallPos = wallObject.transform.position;

            //Shoot a ray from player towards the walls
            if (Physics.Raycast(GameObject.FindGameObjectWithTag("Player").transform.position, wallPos - GameObject.FindGameObjectWithTag("Player").transform.position, out found, Player.playerSightDistance + Mathf.CeilToInt(PlayerLight.torchModifier), wallLayerMask.value))
            {

                //If the wall is in line of sight
                if (found.transform.gameObject == wallObject)
                {
                    //Set it visible on the automap
                    markMap(wallObject.transform.position, wallObject.tag);
                }
            }
        }

        //For each stairs
        foreach (GameObject stairsObject in GameObject.FindGameObjectsWithTag("Stairs"))
        {
            //Declare hit variable
            RaycastHit found;

            //Get position
            Vector3 wallPos = stairsObject.transform.position;

            //Shoot a ray from player towards the stairs
            if (Physics.Raycast(GameObject.FindGameObjectWithTag("Player").transform.position, wallPos - GameObject.FindGameObjectWithTag("Player").transform.position, out found, Player.playerSightDistance + Mathf.CeilToInt(PlayerLight.torchModifier), stairsLayerMask.value))
            {
                //If is in line of sight
                if (found.transform.gameObject == stairsObject)
                {
                    //Set it visible on the automap
                    markMap(stairsObject.transform.position, stairsObject.tag);
                }
            }
        }

    }

    void markMap(Vector3 wallPosition, string tag)
    {
        //If automap exists
        if (AutoMapArray != null)
        {
            //For all spaces on the map vertically
            for (int z = 0; z < AutoMapArray.GetLength(1); z++)
            {
                //For all spaces on the map horizontally
                for (int x = 0; x < AutoMapArray.GetLength(0); x++)
                {
                    //If the automap coordinate is a wall
                    if (wallPosition == new Vector3(x, 0, z) && tag == "Wall")
                    {
                        //Make the coordinate equal 2
                        AutoMapArray [x, z] = 2;
                    }
                    //If the automap coordinate is stairs
                    else if (wallPosition == new Vector3(x, 0, z) && tag == "Stairs")
                    {
                        //Make the coordinate equal 3
                        AutoMapArray [x, z] = 3;
                    }
                }
            }
        }
    }
}
