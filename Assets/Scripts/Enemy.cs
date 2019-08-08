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
  private void OnTriggerEnter(Collider other)
  {
    // This line will print to the console, what object collided with the enemy
    // Debug.Log("Hit " + other.transform.name);

    // if "other" is the player:
    // Damage the player FIRST and destroy this enemy object. If we destroy the enemy object before damaging the player, this script(attached to the enemy object) gets destroyed and then we can't damage the player b/c the code for that exists in this script.
    // We added a tag to the Player object in Unity called "Player" so now we can detect a collision with the player here (when it collides with the enemy)
    // We use CompareTag() to see if the tag on the collider is "Player" (the player object). This way is cleaner than saying other.tag == "Player" b/c CompareTag() does not allocate memory on the heap
    if (other.CompareTag("Player"))
    {
      Destroy(gameObject);
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