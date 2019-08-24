using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
  private AudioSource reflectSound;
  // Start is called before the first frame update
  void Start()
  {
    reflectSound = GetComponent<AudioSource>();

    if (reflectSound == null)
    {
      Debug.LogError("AudioSource for Reflect Sound is NULL ::Player.cs::Damage()");
    }

  }




  void OnTriggerEnter2D(Collider2D other)
  {

    if (other.gameObject.CompareTag("Laser"))
    {
      Laser laser = other.gameObject.GetComponent<Laser>();

      // When the laser's direction is reversed play the reflect sound
      if (laser.getIsDirectionReversed() == true)
      {
        reflectSound.Play();
      }

    }

  }
}
