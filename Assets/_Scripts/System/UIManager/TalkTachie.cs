using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkTachie : MonoBehaviour {
  [SerializeField]
  private Text dialogText = null;
  [SerializeField]
  private Image leftImage  = null, rightImage = null, dialogPanelImage = null;
  [SerializeField]
  private TypewriterEffect dialogTypewriter = null;
  [SerializeField]
  private GameObject dialogPanel = null;

  [SerializeField]
  private AssetPath TachieFolder = null;
  [SerializeField]
  private AssetPath ScriptsFolder = null;

  [Header("设置立绘透明度")]
  [SerializeField]
  private Color hightLightColor = new Color(1f, 1f, 1f, 1f);
  [SerializeField]
  private Color defaultColor = new Color(1f, 1f, 1f, 0.5f);
    [SerializeField]
  private Color hideColor = new Color(1f, 1f, 1f, 0f);

  public bool isPlaying = false;
  [SerializeField]
  private Script[] playingScript;
  [SerializeField]
  private int playingScriptIndex;
  [SerializeField]
  private Dictionary<string, Sprite> TachieDict =
    new Dictionary<string, Sprite>();

  public delegate void DialogEndHandler();
  public event DialogEndHandler DialogEnd;

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

  public Script[] ScriptFromJson(string scriptName) {
    string path = ScriptsFolder.ResourcesPath + "/" + scriptName;
    var textAsset = Resources.Load(path, typeof(TextAsset)) as TextAsset;
    ScriptList scripts = JsonUtility.FromJson<ScriptList>(textAsset.text);
    return scripts.data;
  }

  void Awake() {
    ImportResources();
  }

  void Start() {
    DialogEnd += NextScript;
    FinishedEvent += () => {
      isPlaying = false;
      deactiveDialogPanel();
    };
    deactiveDialogPanel();
  }

  void ImportResources() {
    UnityEngine.Object[] tachies = Resources.LoadAll(
      TachieFolder.ResourcesPath, typeof(Sprite));

    foreach (var img in tachies) {
      TachieDict.Add(img.name, img as Sprite);
      Debug.Log($"TachieDict add {img.name}");
    }
  }

  public void ShowDialog(string speaker, string panel, string content, 
    WhichImage which = WhichImage.LeftImage) {
    activeDialogPanel();
    ShowTachie(speaker, panel, which);
    // // add Name
    // string text = "<b>" + speaker + "</b>: ";
    dialogText.text = "";
    dialogTypewriter.SetText(content);
  }

  public void InitialTachie() {
    leftImage.sprite = null;
    leftImage.color = hideColor;
    rightImage.sprite = null;
    rightImage.color = hideColor;
  }

  public void ShowTachie(string speaker, string panel, WhichImage which) {
    Sprite tachie = null;
    if (TachieDict.ContainsKey(speaker))
      tachie = TachieDict[speaker];
    
    var useImage = (which == WhichImage.LeftImage ? leftImage : rightImage);
    var otherImage = (useImage == leftImage ? rightImage : leftImage);

    if (otherImage.sprite != null)
      otherImage.color = defaultColor;
    useImage.sprite = tachie;

    if (TachieDict.ContainsKey(panel))
      dialogPanelImage.sprite = TachieDict[panel];

    if (tachie == null) {
      useImage.color = hideColor;
    } else {
      useImage.color = hightLightColor;
    }
  }

  public void ShowDialog(Sprite avatar, string content) {
    activeDialogPanel();
    dialogTypewriter.SetText(content);
  }

  public void deactiveDialogPanel() {
    if (dialogPanel.activeSelf)
    dialogPanel.SetActive(false);
    leftImage.color = hideColor;
    rightImage.color = hideColor;
  }

  public void activeDialogPanel() {
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
      if (DialogEnd != null) {
        DialogEnd();
      }
    }
  }

  // public void SystemDialog(string content) {
  //   ShowDialog(null, content);
  //   DialogEnd += SystemDialogHide;
  // }

  // private void SystemDialogHide() {
  //   this.DialogEnd -= SystemDialogHide;
  //   HideDialog();
  // }

  public void PlayScript(string scriptName) {
    Debug.Log("Playing " + scriptName);
    PlayScript(ScriptFromJson(scriptName));
  }

  public void PlayScript(Script[] scripts) {
    isPlaying = true;
    playingScript = scripts;
    playingScriptIndex = 0;
    InitialTachie();
    NextScript();
  }

  public void NextScript() {
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
    DialogEnd -= NextScript;
  }
}