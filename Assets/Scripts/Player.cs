// These are namespaces(libraries that give you access to code, like import statements in Java/Javascript)
using System.Collections; // lets you write C# code
using System.Collections.Generic; //lets you write C# code
using UnityEngine; // need this to use MonoBehaviour below

// We extend from MonoBehaviour, a Unity specific term. It allows us to drag and drop scripts or behaviors onto game objects to control them in Unity
// We will attach it to the player and will make that player behave like a player
public class Player : MonoBehaviour
{
  // SerializeField will allow this private variable to appear in Unity so someone can change it to test out something, etc.
  // This variable controls how fast the game object will move
  // Since this is a private variable we name it starting with a "_" just so it;s easier to see that it's private.
  [SerializeField]
  private float _speed = 3.5f;

  [SerializeField]
  // amount of lives the player has
  // We don't want anything (other than the player) to change this value so we added a method below called "Damage"
  private int _lives = 3;

  // This variable is private b/c we don't plan to swap out this GameObject with another (like say a stronger version of a laser or a laser with a different animation,etc.). If the value won't change, the variable should be private so that other objects in the game can't change the value

  // In Unity sometimes you can’t write code without first updating the components in the Unity editor. For example this variable holds a laser by first assigning the Laser Prefab to the Player object so that when we press the space key, a laser will spawn. 

  // In Text Editor:

  // We wrote an if statement that said “if we press the space key, spawn a laser”

  // We declared a GameObject called laser. 

  // Then in the Unity Editor, we clicked on the Player object, then dragged the Laser Prefab into the Player object’s “Player Script” component.

  // Then we went back to the text editor and wrote code to instantiate the laser in the if statement (since we just dragged and dropped the Laser Prefab, when we run this code and then press the space key in game mode, it will instantiate a copy of the Laser Prefab (the laser to fire)
  // But we couldn't do any of this is if this _laserPrefab varibale wasn't here, b/c then we wouldn't have a field on the Player object to drag and drop the laser prefab onto
  [SerializeField]
  private GameObject _laserPrefab;

  // This variable determines how much time must pass before you can use the laser again (you shouldn't be able to spam it). In this case its 0.5 seconds

  [SerializeField]
  private float _fireRate = 0.15f;
  // We need another varibale to check againt _fireRate so we know that 0.15 seconds have actually passed
  private float _canFire = -1f;

  // Start is called before the first frame update, when you start the game
  void Start()
  {
    // take the current position = new position(0,0,0) (x,y,z)
    // How we access the Player object's position? Unity is  component based so in the Unity editor, click on the Player object and you will see that we need to access the "Transform" section (component) to get the Player object's position. Inside Transform, you will see "Position" so we access the position using transform.position
    // Vector3 defines positioning of game objects. We are assigning the position (transform.position) a new position in (x,y,z) format
    transform.position = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  // This is a game loop and it runs typically about 60 frames/second and it runs every frame. This is where all our logic & userInput  will go
  void Update()
  {
    // Player's movement
    CalculateMovement();

    // if I hit the SPACE key,
    // Spawn a gameobject (the laser)

    // We access the InputManager with "Input" b/c we need user input to shoot a laser
    // GetKeyDown() is used when you want to press the key down. There is also GetKey() which is when you press & hold the key down and also GetKeyUp(), where you press the key up.
    // "KeyCode" lets us know we are searching for a key. It is an event handler that handles keys on the keyboard. "Space" means the Space key
    // //Instantiate lets us create the laser object by passing it in as the first argument (_laserPrefab). The second arguement is the position the laser will spawn, which we want as the player's position so we pass in "transform.position". The 3rd arguement "Quaternion" is the object's rotation using euler angles (which you can see in the Unity Inspector of the player object. There is a component (a field) called "Rotation. Those x,y and z degrees have to do with the x,y and z euler angles). Since we don't need to make the laser rotate, we can just leave it as it's default using "Quaternion.identity" The "identity" part means "default rotation" and you will use this most of the time, but here is a link to read up on euler angles:
    //https://docs.unity3d.com/ScriptReference/Quaternion-eulerAngles.html

    // the part after the "&&" helps to restrict the player from spamming the laser. It is a "cool down" or "delay" effect.
    // Time.time is how long the game has been running and _canFire is our variable we are using to check against that time. So when the game starts, Time.time = 1 second and _canFire = -1 second, so since Time.time is greater than _canFire, we can fire a laser when pressing the SPACE bar. However, in the FireLaser() we say _canFire = Time.time + _fireRate. So now canFire is 1.15 seconds (since __fireRate is set to 0.15f above). Now when we go to press the SPACE bar again, Time.time changes from 1 second to 1.1 seconds. Since 1.1 is not greater to _canFire's new value of 1.15 seconds, the if statement will not execute, and you can't fire a laser until Time.time is 1.15 or higher

    // Also, the if statement was originally in the FireLaser() method but it's better to put it here before we call FireLaser() otherwise we waste resources calling the FireLaser() method before the cool down effect wares off. It also looks better to the eyes

    if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
    {
      FireLaser();
    }

  }

  void CalculateMovement()
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

    Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

    // plug in the "direction" variable for cleaner looking code (we could've just put what is assigned to the "direction" variable into this formula but this way is easier to look at)
    // This allows the player to move and what speed they move
    transform.Translate(direction * _speed * Time.deltaTime);

    // PLAYER BOUNDS - Areas the Player object can't go
    // If the position on y is greater than 0,
    // y position = 0

    // else if position on the y is less than -3.8f
    // y position = -3.8f

    // The if statement means that the player object will not move up if it will go past y position 0
    // We accessed the "position" component after the transform component with "transform.position". We set it to a new Vector3 and we want the x value to stay the same so we pass in "transform.position.x" for the first value. We set the y and z values to 0 (You could also keep the current z value with "transform.position.z" if you wanted to)

    // The else if statement means that the player object will not move down if it will go below y position -3.8f. NOTE: that we only need "f" b/c this is a decimal (floats) and there is no BigDecimal like how there is in Java

    // THIS CODE CAN BE RE-WRITTEN AS SHOWN BELOW TO DO THE SAME THING
    // if (transform.position.y >= 0) {
    //     transform.position = new Vector3 (transform.position.x, 0, 0);
    // } else if (transform.position.y <= -3.8f) {
    //     transform.position = new Vector3 (transform.position.x, -3.8f, 0);
    // }

    // Mathf.Clamp() allows you to select a property and give it a min and max so that the object this script is attached to cannot go below the low bound you set and cannot go higher than the high bound you set.
    // Params: 
    // 1) The property to clamp (transform.position.y -- The 'y' position of the object)
    // 2) The low bound (-3.8f in this case)
    // 3) The high bound (0 in this case)
    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

    // MAKING PLAYER WARP TO OPPOSITE SIDE WHEN GOING TO THE LEFT OR RIGHT EDGES. (This is called wrapping)

    // if player on x > 11.3
    // x position = -11.3

    // else if player on x < -11.3
    // x position = 11.3

    if (transform.position.x > 11.3f)
    {
      transform.position = new Vector3(-11.3f, transform.position.y, 0);
    }
    else if (transform.position.x < -11.3f)
    {
      transform.position = new Vector3(11.3f, transform.position.y, 0);
    }
  }

  void FireLaser()
  {
    _canFire = Time.time + _fireRate;
    // We want the laser to spawn 0.8 above the player's y position. Since position is a Vector3 we can't simply add 0.8f to transform.position. 
    // What we can do is add a new Vector3 that has a y value of 0.8f and x & z values of 0
    Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

  }

  // method in C# are private by default (by just saying "void") but we want the enemy to do damage to the player so we want the enemy to access this methos, so we made it public
  public void Damage()
  {
    _lives--; // reduce amount of lives by 1

    // checked if player died and if yes, destroy the player
    if (_lives < 1)
    {
      Destroy(this.gameObject);
    }
  }

}