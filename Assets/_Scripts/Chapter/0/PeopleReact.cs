using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleReact : MonoBehaviour {
  public delegate void EnterDelegate();
  public EnterDelegate EnterEvent;
  public string otherTag = "";

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == otherTag)
      EnterEvent();
  }
}