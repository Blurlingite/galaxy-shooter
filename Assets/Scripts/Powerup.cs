using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
  [SerializeField]
  private float _speed = 3.0f;
  [SerializeField]
  //We assigned th IDs of powerups in the Inspector in Unity 
  // 0 = Triple Shot Powerup
  // 1 = Speed Powerup
  // 2 = Shield Powerup
  // 3 = Reflector Powerup
  private float powerupID;

  private AudioSource _audioSource;

  void Start () {
    _audioSource = GameObject.Find ("Audio_Manager").transform.GetChild (1).GetComponent<AudioSource> ();

    if (_audioSource == null) {
      Debug.LogError ("Audio Source on Powerup is NULL");
    }

  }

  // Update is called once per frame
  void Update () {
    // Move down at the speed of 3m/s
    transform.Translate (Vector3.down * _speed * Time.deltaTime);

    // if we hit the bottom of screen, destroy us
    if (transform.position.y < -4.5f) {
      Destroy (this.gameObject);
    }

  }

  private void OnTriggerEnter2D (Collider2D other) {
    if (other.gameObject.CompareTag ("Player")) {
      Player player = other.transform.GetComponent<Player> ();

      if (player == null) {
        Debug.LogError ("Powerup cannot find Player");
      } else {
        switch (powerupID) {
          // if powerupID = 0 enable triple shot
          case 0:
            if (player.getIsTripleShotActive () == true) {
              player.setIsAnotherTripleShot ();
            }
            player.TripleShotActive ();
            break;
            // if 1 enable speed powerup

          case 1:
            if (player.getIsSpeedBoostActive () == true) {
              player.setIsAnotherSpeedBoost ();
            }
            player.SpeedBoostActive ();
            // Debug.Log("SPEED IS KEY");
            break;

            // if 2 enable shield powerup

          case 2:
            player.ShieldsActive ();
            break;
          case 3:
            if (player.getIsReflectorActive () == true) {
              player.setIsAnotherReflector ();
            }
            player.ReflectorActive ();
            break;
          default:
            Debug.Log ("Default Value");
            break;

        }

      }
      _audioSource.Play ();
      // destroy the powerup giving the illusion that we collected it
      Destroy (this.gameObject);
    }

  }
}