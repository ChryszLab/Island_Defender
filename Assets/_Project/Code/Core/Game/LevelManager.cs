using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    public int KillCount;
    public TMP_Text text_KillCount;
    private void Update()
    {
        text_KillCount.text = KillCount.ToString();
    }

}
