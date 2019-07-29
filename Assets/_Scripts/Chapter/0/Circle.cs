using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {
  void Dead() {
    Destroy(this.gameObject);
  }
}