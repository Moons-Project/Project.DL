// 提供一些输入控制相关的方法

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jsky.Manager {

  public class InputManager : ManagerBase<InputManager> {
    public bool MouseMoveRight => Input.GetAxis("Mouse X") > 0;
    public bool MouseMoveTop => Input.GetAxis("Mouse Y") > 0;

    public bool MouseClickDown => Input.GetMouseButton(0);
    public bool SlideRightAction => MouseClickDown && MouseMoveRight;
    public bool SlideTopAction => MouseClickDown && MouseMoveTop;

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

}