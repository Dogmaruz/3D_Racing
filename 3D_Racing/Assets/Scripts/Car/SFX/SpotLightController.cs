using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_frontLights;

    [SerializeField] private GameObject[] m_rearLights;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < m_frontLights.Length; i++)
            {
                m_frontLights[i].GetComponent<Light>().enabled = !m_frontLights[i].GetComponent<Light>().enabled;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < m_rearLights.Length; i++)
            {
                m_rearLights[i].GetComponent<Light>().enabled = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < m_rearLights.Length; i++)
            {
                m_rearLights[i].GetComponent<Light>().enabled = false;
            }
        }
    }
}
