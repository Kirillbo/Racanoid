using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {

    public AudioClip audioClip;
    private Button _button;

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
            //ManagerSound.Instance.PlayEffect(audioClip);
            yield return new WaitForSecondsRealtime(audioClip.length);
            _button.interactable = true;
        }
        
        EventButtonClick e = new EventButtonClick();
        e.Parametr = buttonName;
        EventManager.Instance.Send(e);
    }


    void OnDestroy()
    {
        StopAllCoroutines();
    }

}

