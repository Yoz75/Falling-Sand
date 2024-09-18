using System.Collections;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;

    private int FramesCount;

    private void Start()
    {
        StartCoroutine(UpdateCounter());
    }

    private void Update()
    {
        FramesCount++;        
    }

    private IEnumerator UpdateCounter()
    {
        while(true)
        {
            Text.text = FramesCount.ToString();
            FramesCount = 0;
            yield return new WaitForSeconds(1f);
        }
    }
}
