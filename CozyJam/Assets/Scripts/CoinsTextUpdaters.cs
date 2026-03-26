using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsTextUpdaters : MonoBehaviour
{
    public int coins = 0;

    public TextMeshProUGUI coinText;
    public GameManager gameManager;

    private void Update()
    {
        coins = gameManager.coins;

        coinText.text = coins.ToString();
    }
}
