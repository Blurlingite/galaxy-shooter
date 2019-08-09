using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  [SerializeField]
  // So we know which object we are spawning (instantiating)
  // Right after this, you will go back to Unity and drap & drop the object into this variable's field that should have appeared (in this case the Enemy prefab object)
  private GameObject _enemyPrefab;

  [SerializeField]
  // hold container that will hold spawned enemies so hierarchy in Unity isn't cluttered with too many spawns
  private GameObject _enemyContainer;
  // Start is called before the first frame update
  void Start()
  {
    // We use "StartCoroutine" to start the coroutine that spawns the enemies, which we wrote below (SpawnRoutine). We put this in the Start() b/c we want enemies to spawn as soon as the game starts running
    StartCoroutine(SpawnRoutine());
  }

  // Update is called once per frame
  void Update()
  {

  }

  // spawn gameobjects every 5 secs
  // Create a coroutine of type IEnumerator -- Yield events (we get to use the "yield" keyword, which allows us to wait for the amount of seconds you pass in)

  // while loop (infinite loop). We want an infinite while loop b/c we have access to the "yield" keyword in this coroutine so we can pause this loop. And we want this loop to keep running so we can keep spawning enemies
  IEnumerator SpawnRoutine()
  {
    // this will wait for 1 frame and then run the next line in this function
    // yield return null;



    // Instantiate Enemy prefab
    while (true)
    {
      // variable to hold a randomly generated position on the x axis (within a certain range)
      Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
      // Instantiate an enemy object by passing in the object (that we dragged and dropped from Unity and that got stored in the GameObject variable in this script, _enemyPrefab), the position (posToSpawn), and a Quaternion (but we don't care about those now so use Quaternion.identity as a default)
      // we assigned the Instantiate() to a GameObject variable so we can store the enemy object that spawns from it into the _enemyContainer variable above
      GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

      // STORE SPAWNED ENEMY IN ENEMY CONTAINER 
      // *********************************************
      // We assign the newEnemy to the _enemyContainer's transform. We are giving the newly spawned enemy a parent (The Enemy Container) so it gets stored in the Enemy Container so we say "newEnemy.transform.parent". We access the parent from the object's transform and parent is of type Transform. This is why we can't assign it to _enemyContainer directly we have to assign it to _enemyContainer's Transform with _enemyContainer.transform
      newEnemy.transform.parent = _enemyContainer.transform;

      // yield wait for 5 seconds
      // "WaitForSeconds" will take in the amount of seconds this function will wait before performing the next line of code. You must use the "new" keyword also
      yield return new WaitForSeconds(5.0f);
      // right after we yield for 5 seconds we will go back to the beginning of the while loop if there is no more code after the yield statement
    }

    // WE WILL NEVER GET HERE B/C THE WHILE LOOP IS INFINITE

  }

}
