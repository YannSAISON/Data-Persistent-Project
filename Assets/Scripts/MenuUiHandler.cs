using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUiHandler : MonoBehaviour
{
    public TMP_InputField nameInput;

    private void Start()
    {
        if (PersistentManager.Instance != null)
        {
            string defaultName = PersistentManager.Instance.PlayerName;

            if (defaultName.Length > 0)
                nameInput.text = defaultName;
        }

    }

    public void StartNew()
    {
        string playerName = nameInput.text;

        if (playerName.Length == 0)
            return;

        SceneManager.LoadScene(1);
        PersistentManager.Instance.SetUserName(playerName);
    }
}
