using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage : MonoBehaviour
{
  private AudioSource _audioSource;
  // Start is called before the first frame update
  void Start()
  {
    _audioSource = GetComponent<AudioSource>();

    if (_audioSource == null)
    {
      Debug.LogError("Audio Source is NULL");
    }
    _audioSource.Play();

    Destroy(this.gameObject, 3f);
  }

}
