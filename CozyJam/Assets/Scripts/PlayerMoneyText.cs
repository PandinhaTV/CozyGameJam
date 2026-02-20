using UnityEngine;

public class PlayerMoneyText : MonoBehaviour
{
    public TMPro.TMP_Text moneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = GameManager.instance.playerMoney.ToString();
    }
}
