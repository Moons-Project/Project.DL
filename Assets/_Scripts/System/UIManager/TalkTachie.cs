using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkTachie : MonoBehaviour {
  [SerializeField]
  private Text dialogText = null;
  [SerializeField]
  private Image leftImage  = null, rightImage = null;
  [SerializeField]
  private TypewriterEffect dialogTypewriter = null;

  [SerializeField]
  private AssetPath TachieFolder = null;
  [SerializeField]
  private AssetPath ScriptsFolder = null;


  [SerializeField]
  private Dictionary<string, Sprite> TachieDict =
    new Dictionary<string, Sprite>();

  [SerializeField]
  private GameObject dialogPanel = null;


  public bool isPlaying = false;
  private Script[] playingScript;
  private int playingScriptIndex;

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
    deactiveDialog();
    DialogEnd += NextScript;
    FinishedEvent += () => {
      isPlaying = false;
      deactiveDialog();
    };
  }

  void ImportResources() {
    Object[] tachies = Resources.LoadAll(
      TachieFolder.ResourcesPath, typeof(Sprite));

    foreach (var img in tachies) {
      TachieDict.Add(img.name, img as Sprite);
      Debug.Log($"TachieDict add {img.name}");
    }
  }

  public void ShowDialog(string speaker, string content, 
    WhichImage which = WhichImage.LeftImage) {
    activeDialog();
    ShowTachie(speaker, which);
    // // add Name
    // string text = "<b>" + speaker + "</b>: ";
    // dialogText.text = text;
    dialogTypewriter.SetText(content);
  }

  public void ShowTachie(string speaker, WhichImage which) {
    Sprite tachie = null;
    if (TachieDict.ContainsKey(speaker)) {
      tachie = TachieDict[speaker];
    }
    var useImage = (which == WhichImage.LeftImage ? leftImage : rightImage);
    var otherImage = (useImage == leftImage ? rightImage : leftImage);

    if (otherImage.sprite != null)
      otherImage.color = new Color(1f, 1f, 1f, 0.5f);
    useImage.sprite = tachie;

    if (tachie == null) {
      useImage.color = new Color(1f, 1f, 1f, 0f);
    } else {
      useImage.color = new Color(1f, 1f, 1f, 1f);
    }
  }

  public void ShowDialog(Sprite avatar, string content) {
    activeDialog();
    dialogTypewriter.SetText(content);
  }

  public void deactiveDialog() {
    if (dialogPanel.activeSelf)
      dialogPanel.SetActive(false);
  }

  public void activeDialog() {
    if (!dialogPanel.activeSelf)
     dialogPanel.SetActive(true);
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
    ShowDialog(script.speaker, script.content, script.leftOrRight);
  }

  public void SkipScript() {
    if (isPlaying) {
      isPlaying = false;
      deactiveDialog();
      FinishedEvent();
    }
  }

  void OnDisable() {
    DialogEnd -= NextScript;
  }
}