using UnityEngine;

public class Garage : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject GaragePanel, SettingsPanel;

    public void OpenGarage()
    {
        if (!GaragePanel.activeInHierarchy)
        {
            GaragePanel.SetActive(true);
            SettingsPanel.SetActive(false);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(0, 3, -6), 2f);
        }
        else
        {
            GaragePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(0, 8, -12), 2f);
        }   
    }
}
