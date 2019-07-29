using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SystemManagerBase<InputManager> {
  public bool MouseMoveRight => Input.GetAxis("Mouse X") > 0;
  public bool MouseClickDown => Input.GetMouseButton(0);
  public bool SlideRightAction => MouseClickDown && MouseMoveRight;

  public Vector3 MousePosition() {
    if (MouseClickDown) {
      return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    } 
    return new Vector3();
  }

  void Start() { }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown("escape")) Application.Quit();
  }
}