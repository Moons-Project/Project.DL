using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkTachie : MonoBehaviour {
  public Text dialogText;
  public Image leftImage, rightImage;
  public TypewriterEffect dialogTypewriter;


  private Dictionary<string, Sprite> TachieDict =
    new Dictionary<string, Sprite>();

  public GameObject dialogPanel;

  public enum WhichImage {
    LeftImage,
    RightImage
  }

  void Awake() {
    ImportResources();
  }

  void Start() {
    HideDialog();
  }

  void ImportResources() {
    Object[] tachies = Resources.LoadAll("Sprites/Tachie", typeof(Sprite));

    foreach (var img in tachies) {
      TachieDict.Add(img.name, img as Sprite);
    }
  }

  public void ShowDialog(string speaker, string content, 
    WhichImage which = WhichImage.LeftImage) {

    ShowTachie(speaker, which);

    string text = "<b>" + speaker + "</b>: ";
    dialogText.text = text;
    dialogTypewriter.SetText(content, true);
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
    dialogPanel.SetActive(true);
    dialogTypewriter.SetText(content);
  }

  public void HideDialog() {
    dialogPanel.SetActive(false);
  }

  // public void SkipDialog() {
  //   dialogTypewriter.Skip();
  // }

  public delegate void DialogEndHandler();
  public event DialogEndHandler DialogEnd;

  public void OnDialogPress() {
    if (dialogTypewriter.typing) {
      dialogTypewriter.Skip();
    } else {
      if (DialogEnd != null) {
        DialogEnd();
      }
    }
  }

  public void SystemDialog(string content) {
    ShowDialog(null, content);
    DialogEnd += SystemDialogHide;
  }

  private void SystemDialogHide() {
    this.DialogEnd -= SystemDialogHide;
    this.HideDialog();
  }
}