using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
  [SerializeField]
  private float _speed = 3.0f;


  // Update is called once per frame
  void Update()
  {
    // Move down at the speed of 3m/s
    transform.Translate(Vector3.down * _speed * Time.deltaTime);

    // if we hit the bottom of screen, destroy us
    if (transform.position.y < -4.5f)
    {
      Destroy(this.gameObject);
    }

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Player player = other.transform.GetComponent<Player>();

      if (player == null)
      {
        Debug.LogError("Powerup cannot find Player");
      }
      else
      {
        // enable triple shot
        player.TripleShotActive();
      }

      // destroy the powerup giving the illusion that we collected it
      Destroy(this.gameObject);
    }

  }
}
