using System;
using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Jsky.Manager {

  public class TalkTachie : MonoBehaviour {
    [SerializeField]
    private Text dialogText = null;
    [SerializeField]
    private Image leftImage = null, rightImage = null, dialogPanelImage = null;
    [SerializeField]
    private TypewriterEffect dialogTypewriter = null;
    [SerializeField]
    private GameObject dialogPanel = null;

    [Header("设置立绘透明度")]
    [SerializeField]
    private Color hightLightColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    private Color defaultColor = new Color(1f, 1f, 1f, 0.5f);
    [SerializeField]
    private Color hideColor = new Color(1f, 1f, 1f, 0f);

    public bool isPlaying = false;
    private Script[] playingScript;
    private int playingScriptIndex;

    public delegate void Finished();
    public event Finished FinishedEvent;

    public enum WhichImage {
      LeftImage,
      RightImage
    }

    [System.Serializable]
    public class Script {
      public string speaker;
      public string panel;
      public string content;
      public TalkTachie.WhichImage leftOrRight;
    }

    [System.Serializable]
    public class ScriptList {
      public Script[] data;
    }

    void Awake() { }

    void Start() {
      FinishedEvent += () => {
        isPlaying = false;
        deactiveDialogPanel();
      };
      deactiveDialogPanel();
    }

    private void ShowDialog(string speaker, string panel, string content,
      WhichImage which = WhichImage.LeftImage) {
      activeDialogPanel();
      ShowTachie(speaker, panel, which);
      // // add Name
      // string text = "<b>" + speaker + "</b>: ";
      dialogText.text = "";
      dialogTypewriter.SetText(content);
    }

    private void InitialTachie() {
      leftImage.sprite = null;
      leftImage.color = hideColor;
      rightImage.sprite = null;
      rightImage.color = hideColor;
    }

    private void ShowTachie(string speaker, string panel, WhichImage which) {
      Sprite tachie = null;
      if (DBManager.Instance.TachieDict.ContainsKey(speaker))
        tachie = DBManager.Instance.TachieDict[speaker];

      var useImage = (which == WhichImage.LeftImage ? leftImage : rightImage);
      var otherImage = (useImage == leftImage ? rightImage : leftImage);

      if (otherImage.sprite != null)
        otherImage.color = defaultColor;
      useImage.sprite = tachie;

      if (DBManager.Instance.TachieDict.ContainsKey(panel))
        dialogPanelImage.sprite = DBManager.Instance.TachieDict[panel];

      if (tachie == null) {
        useImage.color = hideColor;
      } else {
        useImage.color = hightLightColor;
      }
    }

    private void ShowDialog(Sprite avatar, string content) {
      activeDialogPanel();
      dialogTypewriter.SetText(content);
    }

    private void deactiveDialogPanel() {
      if (dialogPanel.activeSelf)
        dialogPanel.SetActive(false);
      leftImage.color = hideColor;
      rightImage.color = hideColor;
    }

    private void activeDialogPanel() {
      if (!dialogPanel.activeSelf)
        dialogPanel.SetActive(true);
      // leftImage.color = defaultColor;
      // rightImage.color = defaultColor;
    }

    public void SkipDialog() {
      dialogTypewriter.Skip();
    }

    public void OnDialogPress() {
      if (dialogTypewriter.typing) {
        dialogTypewriter.Skip();
      } else {
        NextScript();
      }
    }

    public void PlayScript(string scriptName) {
      Debug.Log("Playing " + scriptName);

      Func<string, Script[]> ScriptFromJson = (string script) => {
        TextAsset textAsset = null;
        DBManager.Instance.LoadScripts(script, out textAsset);
        ScriptList scripts = JsonUtility.FromJson<ScriptList>(textAsset.text);
        return scripts.data;
      };

      PlayScript(ScriptFromJson(scriptName));
    }

    private void PlayScript(Script[] scripts) {
      isPlaying = true;
      playingScript = scripts;
      playingScriptIndex = 0;
      InitialTachie();
      NextScript();
    }

    private void NextScript() {
      if (!isPlaying) {
        return;
      }
      if (playingScriptIndex >= playingScript.Length) {
        FinishedEvent();
        return;
      }
      var script = playingScript[playingScriptIndex++];
      ShowDialog(script.speaker, script.panel, script.content, script.leftOrRight);
    }

    public void SkipScript() {
      if (isPlaying) {
        isPlaying = false;
        deactiveDialogPanel();
        FinishedEvent();
      }
    }

    public IEnumerator WaitUnitilScriptFinished(Action action) {
      var trigger = false;
      Action changeTrigger = () => trigger = true;
      FinishedEvent += changeTrigger.Invoke;
      yield return new WaitUntil(() => trigger);
      FinishedEvent -= changeTrigger.Invoke;
      action.Invoke();
    }

    void OnDisable() {
      // DialogEnd -= NextScript;
    }
  }

}