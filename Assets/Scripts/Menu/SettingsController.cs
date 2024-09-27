using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] GameObject SettingsPanel, GaragePanel;
    [SerializeField] Camera _camera;
    public void ShowSettings()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeInHierarchy);
        GaragePanel.SetActive(false);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(0, 8, -12), 2f);
    }

    public void ChangeQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
