using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    // destroy explosion after 3 secs so it doesn't clutter up the Hierarchy in Unity 9b/c it is still there even if the asteroid is destroyed and even after the animation is done
    Destroy(this.gameObject, 3f);
  }


}
