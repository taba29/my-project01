using UnityEngine;
using TMPro;

public class HPUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [SerializeField] private TextMeshProUGUI hpText;

    void Update()
    {
        if (player == null) return;

        hpText.text = "HP: " + player.GetCurrentHp();
    }
}