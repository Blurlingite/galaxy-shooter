using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
  [SerializeField]
  private float _rotateSpeed = 19.0f;

  [SerializeField]
  private GameObject _explosionPrefab;

  [SerializeField]
  private SpawnManager _spawnManager;

  // Start is called before the first frame update

  void Start()
  {
    _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

    if (_spawnManager == null)
    {
      Debug.LogError("Spawn Manager is NULL ::Asteroid.cs::Start()");
    }
  }

  // Update is called once per frame
  void Update()
  {

    // rotate object on the zed axis
    // Vector3.forward is invilved with the zed axis. It's the same as new Vector3(0,0,1)
    transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
  }

  // check for laser collision (Trigger)
  // instantiate explosion object at position of asteroid & destroy the laser
  // destroy the explosion after 3 secs

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Laser"))
    {
      // transform.position is the position of the object this script is attached to (the Asteroid). Since this is attached to Asteroid, the Transform is the Asteroid's Transform
      Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

      Destroy(other.gameObject);

      // set a custom delay of the destruction of asteroid to 0.15 secs so there isn't  a breif pause between the explosiion animation and the destruction of the asteroid
      // Start spawning enemies using Spawn Manager
      _spawnManager.StartSpawning();
      Destroy(this.gameObject, 0.15f);
    }

  }
}
