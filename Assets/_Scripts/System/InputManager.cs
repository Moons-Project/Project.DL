using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SystemManagerBase<InputManager> {
  public bool MouseMoveRight => Input.GetAxis("Mouse X") > 0;
  public bool SlideRightAction => Input.GetMouseButton(0) && MouseMoveRight;

  void Start() { }

  // Update is called once per frame
  void Update() {
  }
}