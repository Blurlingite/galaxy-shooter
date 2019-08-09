using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  float _speed = 4.0f;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // move down 4m/s
    transform.Translate(Vector3.down * _speed * Time.deltaTime);

    // if it hits bottom of screen, respawn at the top with a new random x position (b/c having it spawn in the same place everytime is pretty boring)

    if (transform.position.y < -5f)
    {
      // Random.Range is a Unity thing, not a C# thing and the C# version of Random class won;t work b/c we are using the UnityEngine.
      // Random.Range() gives us a random float value between 2 numbers (min & max) that we pass in. 
      // BEWARE: When used with ints the max random generated is always 1 less than the max you pass in
      float randomX = Random.Range(-8f, 8f);
      transform.position = new Vector3(randomX, 7, 0);
    }

  }

  // "Collider other" will be the object that collided with the Enemy object
  private void OnTriggerEnter2D(Collider2D other)
  {
    // This line will print to the console, what object collided with the enemy
    // Debug.Log("Hit " + other.transform.name);

    // if "other" is the player:
    // Damage the player FIRST (by removing 1 of their lives) and destroy this enemy object. If we destroy the enemy object before damaging the player, this script(attached to the enemy object) gets destroyed and then we can't damage the player b/c the code for that exists in this script.
    // We added a tag to the Player object in Unity called "Player" so now we can detect a collision with the player here (when it collides with the enemy)
    // We use CompareTag() to see if the tag on the collider is "Player" (the player object). This way is cleaner than saying other.tag == "Player" b/c CompareTag() does not allocate memory on the heap
    if (other.CompareTag("Player"))
    {
      // get the Player component. First we access the variable "other", which should be the Player object we collided into. Then we need the transform of the Player object so we put ".transform". Then we use GetComponent<>() and pass in the name of the component (that you can get in Unity) into the T brackets<>. Then after that you can see you have access to the Damage() method you made in the Player script (when you called it below) from here in the Enemy script. Accessing 1 script from another is called "script communication"
      // Within this script, the only component whose transform you have direct access to is the Enemy component. But we can indirectly access another component off of a variable like how we do with the Collider variable here with ".transform"
      // The Transform is the root of the object where you can access other components like MeshRenderer, but you must say ".transform" first. I'm only writing this comment b/c in Unity it doesn't look like the components like "MeshRenderer" are in the "Transform" dropdown
      // We stored the getting of the component in a variable so we can null check it right afterwards. Because, what if the Player component doesn't exist yet or was destroyed and doesn't exist anymore? You may get a null reference exception if you tried to run the Damage() method on the Player script (which also wouldn't exist if the Player was destroyed). Null checking is an important optimized way to avoid these errors
      Player player = other.transform.GetComponent<Player>();

      if (player != null)
      {
        // remove 1 life from player
        player.Damage();
      }
      Destroy(gameObject);  // destroy this enemy object
    }


    // if "other" is the laser:
    // Destroy the laser FIRST and then destroy this enemy object. If we destroy the enemy object before the laser, this script(attached to the enemy object) gets destroyed and then we can't destroy the laser b/c the code for that exists in this script
    // We added a tag to the Laser object in Unity called "Laser" so now we can detect a collision with the laser here (when it collides with the enemy) and then destroy the laser, giving the illusion that the laser did damage to the enemy
    if (other.CompareTag("Laser"))
    {
      Destroy(other.gameObject);
      Destroy(this.gameObject);
    }
  }

}