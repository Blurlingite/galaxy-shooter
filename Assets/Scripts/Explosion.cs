using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

  [SerializeField]
  private AudioClip _explosionSound;
  private AudioSource _audioSource;
  // Start is called before the first frame update
  void Start()
  {

    _audioSource = GetComponent<AudioSource>();

    if (_audioSource == null)
    {
      Debug.LogError("Audio Source on Explosion Prefab is NULL");
    }
    else
    {
      _audioSource.clip = _explosionSound;
    }

    _audioSource.Play();


    // destroy explosion after 3 secs so it doesn't clutter up the Hierarchy in Unity b/c it is still there even if the asteroid is destroyed and even after the animation is done
    if (!this.gameObject.CompareTag("Player"))
    {
      Destroy(this.gameObject, 3f);

    }
  }


}
