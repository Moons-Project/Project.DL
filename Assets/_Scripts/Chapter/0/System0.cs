using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;

public class System0 : MonoBehaviour {

  public AssetName policeScript = null;
  public AssetName studentScript = null;

  public GameObject hintText_1 = null;
  public GameObject hintText_2 = null;

  public UnityEngine.Object circle = null;
  public GameObject circleHolder = null;
  public Police police = null;
  public PeopleReact peopleReact = null;
  public GameObject MainCamera = null;
  public Collider2D areaCollider;

  [SerializeField]
  private bool isClickRunPart = false;
    [SerializeField]
  private bool isSlideTopPart = false;
  [SerializeField]
  private float clickCD = kClickCD;

  private static float kClickCD = 0.5f;

  // private Vector3 lastCirclePosition;
  private TalkTachie talkTachie => UIManager.Instance.talkTachie;

  // Start is called before the first frame update
  void Start() {
    hintText_1.SetActive(false);
    hintText_2.SetActive(false);
    peopleReact.EnterEvent += PeopleTalkPart;

    talkTachie.PlayScript(policeScript.assetName);
    StartCoroutine(
      talkTachie.WaitUnitilScriptFinished(() => {
        Debug.Log("警察对话完成");
        StartCoroutine(ShowHintText_1(() => {
          isClickRunPart = true;
          Debug.Log("展示提示1完成");
        }));
      }));
  }

  // Update is called once per frame
  void Update() {
    if (isClickRunPart) ClickRunPart();
    if (isSlideTopPart) SlideTopPart();
  }

  void SlideTopPart() {
    if (InputManager.Instance.SlideTopAction) {
      isSlideTopPart = false;
      MainCamera.GetComponent<Animator>().Play("SlideTop");
    }
  }


  void PeopleTalkPart() {
    isClickRunPart = false;

    Debug.Log("Start 学生对话");
    MainCamera.GetComponent<Animator>().Play("LookPeople");
    talkTachie.PlayScript(studentScript.assetName);
    StartCoroutine(
      talkTachie.WaitUnitilScriptFinished(() => {
        Debug.Log("学生对话完成");
        StartCoroutine(ShowHintText_2(() => {
          isSlideTopPart = true;
          Debug.Log("展示提示2完成");
        }));
      }));
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

  IEnumerator ShowHintText_1(Action action,
    float waitTime = 0.5f, float duration = 5.0f) {
    action.Invoke();
    yield return new WaitForSeconds(waitTime);
    hintText_1.SetActive(true);
    var effect = hintText_1.GetComponent<UITransitionEffect>();
    effect.Show();
    yield return new WaitForSeconds(duration);
    effect.Hide();
  }

  IEnumerator ShowHintText_2(Action action,
    float waitTime = 0.5f, float duration = 5.0f) {
    action.Invoke();
    yield return new WaitForSeconds(waitTime);
    hintText_2.SetActive(true);
    var effect = hintText_2.GetComponent<UITransitionEffect>();
    effect.Show();
    yield return new WaitForSeconds(duration);
    effect.Hide();
  }
}