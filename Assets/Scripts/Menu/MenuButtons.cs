using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject SelectLevelPanel;
    public void StartGame(int Level)
    {
        SceneManager.LoadScene(2);
    }

    public void SelectLevel()
    {
        SelectLevelPanel.SetActive(!SelectLevelPanel.activeInHierarchy);
    }
}
