using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {

    public GameObject PanelMessage;
    private Text text;
    private bool visibile = false;

    private void Awake() {
        if (!PanelMessage)
        {
            Debug.LogError("Could not locate message panel!");
        }
        else {
            text = PanelMessage.transform.FindChild("Panel_TextBorder").FindChild("Text").GetComponent<Text>();
            PanelMessage.SetActive(visibile);
        }
    }

    private void Update() {
        if (visibile && Input.GetMouseButtonDown(0)) {
            visibile = false;
            PanelMessage.SetActive(visibile);
        }
    }

    public void throwMessage(string msg) {
        text.text = msg;
        visibile = true;
        PanelMessage.SetActive(visibile);
    }


}
