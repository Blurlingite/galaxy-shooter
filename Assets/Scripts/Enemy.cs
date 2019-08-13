using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  float _speed = 4.0f;
  // variable for Player to be assigned in Start() so we don't have to get the Player more than once
  private Player _p;
  // Start is called before the first frame update
  [SerializeField]
  private Animator _anim;

  private AudioSource _audioSource;

  void Start()
  {
    _p = GameObject.Find("Player").GetComponent<Player>();
    if (_p == null)
    {
      Debug.LogError("Player is NULL");
    }

    _anim = GetComponent<Animator>();

    if (_anim == null)
    {
      Debug.Log("Animator is NULL");
    }

    _audioSource = GetComponent<AudioSource>();

    if (_audioSource == null)
    {
      Debug.Log("Audio Source on Enemy is NULL");
    }



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
      // trigger the explosion animation by passing in the name of the Trigger you created (OnEnemyDeath) before you destroy the enemy. Just make sure to wait the amount of seconds the animation lasts before destroying or the animation will fail. You can do this by passing in a float for the number of seconds in the Destroy() method as a second argument
      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0; // set enemy speed to 0 so the Enemy's Collider won't hit the player while the enemy is moving while being destroyed
      _audioSource.Play();
      Destroy(gameObject, 2.5f); // destroy this enemy object 2.5 secs after being hit
    }

    // if "other" is the laser:
    // Destroy the laser FIRST and then destroy this enemy object. If we destroy the enemy object before the laser, this script(attached to the enemy object) gets destroyed and then we can't destroy the laser b/c the code for that exists in this script
    // We added a tag to the Laser object in Unity called "Laser" so now we can detect a collision with the laser here (when it collides with the enemy) and then destroy the laser, giving the illusion that the laser did damage to the enemy
    if (other.CompareTag("Laser"))
    {
      Destroy(other.gameObject);
      // add 10 to player score
      // We can't use other to find the Player object b/c "other" in this if statement is the Laser (as shown by other.CompareTag("Laser"))

      // Using this commented out code uses too much resources. GetComponent<>() is an expensive call, and what if we have 50 or more enemies? That means 50 GetComponent calls. So instead, we declare a global Player varaible, and the assign it the Player in void Start() so we'll only use 1 call to get the Player and we can use this reference as many times as we want
      // Player p = GameObject.Find("Player").GetComponent<Player>();

      // Checking if the player is null here is better than doing this i the Start() method b/c Start() only gets called once when the game starts, so if the Player became null after that (by dying) we would get an error here
      if (_p != null)
      {
        // 5-12 range we need to pass in 5 and 13 b/c ints don't include the max
        int randomPoints = Random.Range(5, 13);
        _p.AddScore(randomPoints);
      }
      else
      {
        Debug.Log("Player is null cannot add score");
      }
      // trigger the explosion animation by passing in the name of the Trigger you created (OnEnemyDeath) before you destroy the enemy. Just make sure to wait the amount of seconds the animation lasts before destroying or the animation will fail. You can do this by passing in a float for the number of seconds in the Destroy() method as a second argument
      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      _audioSource.Play();
      Destroy(this.gameObject, 2.5f);

    }
  }

}