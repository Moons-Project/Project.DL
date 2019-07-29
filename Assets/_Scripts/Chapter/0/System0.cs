using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;

public class System0 : MonoBehaviour {

  public AssetName firstScript = null;
  public GameObject textGameObject = null;
  public UnityEngine.Object circle = null;
  public GameObject circleHolder = null;
  public Police police = null;
  public Collider2D areaCollider;

  [SerializeField]
  private bool isClickRunPart = false;
  [SerializeField]
  private float clickCD = kClickCD;

  private static float kClickCD = 0.5f;

  // private Vector3 lastCirclePosition;
  private TalkTachie talkTachie => UIManager.Instance.talkTachie;

  // Start is called before the first frame update
  void Start() {
    textGameObject.SetActive(false);

    talkTachie.PlayScript(firstScript.assetName);
    StartCoroutine(
      talkTachie.WaitUnitilScriptFinished(() => {
        Debug.Log("警察对话完成");
        StartCoroutine(ShowTextOnRoad(() => {
          isClickRunPart = true;
          Debug.Log("展示提示完成");
        }));
      }));
  }

  // Update is called once per frame
  void Update() {
    if (isClickRunPart) ClickRunPart();
  }

  void ClickRunPart() {
    if (InputManager.Instance.MouseClickDown &&
      clickCD >= kClickCD) {
      var position = InputManager.Instance.MousePosition();
      if (areaCollider.OverlapPoint(new Vector2(position.x, position.y))) {
        var newCircle = Instantiate(circle, position,
          new Quaternion()) as GameObject;
        if (!newCircle.activeSelf) newCircle.SetActive(true);
        newCircle.transform.position = new Vector3(
          newCircle.transform.position.x,
          newCircle.transform.position.y,
          0.0f);
        newCircle.transform.parent = circleHolder.transform;
        police.TargetPoint = newCircle.transform.position;
        clickCD = 0.0f;
      } else {
        clickCD += Time.deltaTime;
      }
    } else {
      clickCD += Time.deltaTime;
    }
  }

  IEnumerator ShowTextOnRoad(Action action,
    float waitTime = 0.5f, float duration = 5.0f) {
    action.Invoke();
    yield return new WaitForSeconds(waitTime);
    textGameObject.SetActive(true);
    var effect = textGameObject.GetComponent<UITransitionEffect>();
    effect.Show();
    yield return new WaitForSeconds(duration);
    effect.Hide();
  }
}