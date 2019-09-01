using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace Jsky.Manager {
  public class TipsLayer : MonoBehaviour {
    [SerializeField]
    private GameObject HintText = null;

    public Color HintTextColor;

    void Awake() {
      HintText.SetActive(false);
    }

    public void ShowHintText(string hintText, float delay, float afterTime) {
      StopAllCoroutines();

      var text = HintText.GetComponent<Text>();
      var textUIShiny = HintText.GetComponent<UIShiny>();
      text.text = hintText;
      text.color = HintTextColor;
      StartCoroutine(SetActiveHintText(true, afterTime));
      StartCoroutine(SetActiveHintText(false, afterTime + delay));
    }

    public IEnumerator SetActiveHintText(bool active, float delay) {
      yield return new WaitForSeconds(delay);
      HintText.SetActive(active);
    }
  }
}