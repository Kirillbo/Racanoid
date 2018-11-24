using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {

    public AudioClip audioClip;
    private Button _button;
    public BttnType ButtonType;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void ButtonPressed(string buttonName)
    {
        StartCoroutine(PressedCoroutine(buttonName));
    }


    private IEnumerator PressedCoroutine(string buttonName)
    {
        if (audioClip != null)
        {
            _button.interactable = false;
//            ManagerSound.Instance.PlayEffect(audioClip);
            // Wayt for sound effect end
            yield return new WaitForSecondsRealtime(audioClip.length);
            _button.interactable = true;
        }

        // Send global event about button preesing
        EventManager.TriggerEvent("ClickButtons", buttonName, gameObject);
    }

    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    void OnDestroy()
    {
        StopAllCoroutines();
    }


}

public enum BttnType
{
    MainMenu,
    GameProcess,
    GameOver
}
