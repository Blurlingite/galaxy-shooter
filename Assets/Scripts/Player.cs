// These are namespaces(libraries that give you access to code, like import statements in Java/Javascript)
using System.Collections; // lets you write C# code
using System.Collections.Generic; //lets you write C# code
using UnityEngine; // need this to use MonoBehaviour below

// We extend from MonoBehaviour, a Unity specific term. It allows us to drag and drop scripts or behaviors onto game objects to control them in Unity
// We will attach it to the player and will make that player behave like a player
public class Player : MonoBehaviour
{
    // SerializeField will allow this private variable to appear in Unity so someone can change it to test out something, etc.
    [SerializeField]
    private float speed = 3.5f;

    // Start is called before the first frame update, when you start the game
    void Start()
    {
        // take the current position = new position(0,0,0) (x,y,z)
        // How we access the Player object's position? Unity is  component based so in the Unity editor, click on the Player object and you will see that we need to access the "Transform" section (component) to get the Player object's position. Inside Transform, you will see "Position" so we access the position using transform.position
        // Vector3 defines positioning of game objects. We are assigning the position (transform.position) a new position in (x,y,z) format
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    // This is a game loop and it runs typically about 60 frames/second and it runs every frame. This is where all our logic & userInput  will go
    void Update()
    {
        // access the object you want to move, which is our Player, which we are already on
        // 1 unit in Unity is 1 meter in the real world, so "new Vector3.right" makes the Player move 60 meters per second (the Update() makes has a speed of 60 frames per second, so multiply by 1 meter to get 60 meters per second) which is too fast
        // We multiply by Time.deltaTime so that we are not frame dependent but minutes/seconds dependent
        // Time.deltaTime can be thought of as 1 second, so now the Player will move 1 meter per second. If we multiply the vector by 5, it will now move 5 meters per second
        // Time.deltaTime is the time in seconds it took to complete the last frame to the current frame
        // Time.deltaTime is the equivalent of incorporating realtime. To use it, you multiply it by a vector
        // Here is whats happening to make the Player move 5m/s:
        // Vector3.right * 5 * Time.deltaTime === new Vector3(1,0,0) * 5 * real time
        // With distributive property in Unity, each of the Vector3's values get multiply by 5 so now we have: 
        // new Vector(5,0,0) * real time   which equals 5m/s since real time is in seconds

        transform.Translate(Vector3.right * speed * Time.deltaTime);
        
    }
}
