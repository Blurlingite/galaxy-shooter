// These are namespaces(libraries that give you access to code, like import statements in Java/Javascript)
using System.Collections; // lets you write C# code
using System.Collections.Generic; //lets you write C# code
using UnityEngine; // need this to use MonoBehaviour below

// We extend from MonoBehaviour, a Unity specific term. It allows us to drag and drop scripts or behaviors onto game objects to control them in Unity
// We will attach it to the player and will make that player behave like a player
public class Player : MonoBehaviour
{
    // SerializeField will allow this private variable to appear in Unity so someone can change it to test out something, etc.
    // This variable how fast the game object will move
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
        // Get the horizontal axis so you can move the game object with the left/right arrows or the a/d keys
        // To see how we get to the horizontal axis in Unity go to Edit > Project Settings > Input Manager > Axes > Horizontal
        // That is what we are doing here. "Input" calls the Input Manager. "GetAxis" is what we use to get an axis by it's name (capitalization is important)
        // Now we have the horizontal axis and when we add this to the formula below by multiplying by this variable, we can now move the game object using the left/right arrows or a/d keys
        float horizontalInput = Input.GetAxis("Horizontal");

        // Get vertical axis in Unity
        float verticalInput = Input.GetAxis("Vertical");

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
        // We multiply by our horizontalInput variable which has the horizontal axis so we can move the game object using the left/right arrows or a/d keys. The reason this works is b/c when you hit the left arrow, horizontalInput becomes -1 and when you hit the right arrow it becomes 1. The -1 will make you move in a "negative" direction and the 1 will make you move in a positive direction, since we are multiplying each number in our Vector3(1,0,0) by horizontalInput
        // Ex. Vector3(1,0,0) when hitting left arrow becomes Vector3(-1,0,0)
        // If you want to see the horizontal axis number return -1 or 1 in Unity, temporairily change the horizontalInput variable to public and put it at the top of the class. Then check the game object's script section for the horizontal axis and see it change when you run the game and hit the arrow keys
        // The code for this comment was commented out b/c I wrote a more optimal way of doing the same thing below
        // transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        // This will control the vertical movement and let us move the game object up and down with the up and down arrow keys. Notice instead of Vector3.right, we used Vector3.up, since this is vertical movement and not horizontal
        // The code for this comment was commented out b/c I wrote a more optimal way of doing the same thing below
        // transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        // This variable controls our direction of travel
        // This Vector3 variable uses the game object's horizontal position as the x value and the vertical position as the y value in this Vector3(x,y,z) to accomplish what the 2 commented out lines above did: Allow us to move the game object both horizontally and vertically using the arrow keys once we plug in the variable into the transform formula.
        // As you press the left/right arrows, horizontalInput's value changes causing the game object to move to the left or right.
        // As you press the up/down arrows, verticalInput's value changes causing the game object to move up or down.
        // "speed" controls how fast the game object moves and when we combine it with the Vector3 that controls the game object's horizontal and vertical axes, it will apply that speed when you press the arrow keys

        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);

        // plug in the "direction" variable for cleaner looking code (we could've just put what is assigned to the "direction" variable into this formula but this way is easier to look at)
        transform.Translate(direction * speed * Time.deltaTime);
        
    }
}
