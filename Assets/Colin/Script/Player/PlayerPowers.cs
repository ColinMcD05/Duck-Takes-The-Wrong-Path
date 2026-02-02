using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public void SwitchPower(string gainedPower)
    {
        switch (gainedPower)
        {
            case "Grow":
                if (gameObject.GetComponent<PlayerController>().currentPower == "Defualt")
                {
                    gameObject.GetComponent<PlayerController>().currentPower = "Grow";
                    Debug.Log(gameObject.GetComponent<PlayerController>().currentPower);
                }
                break;
            case "Water":
                gameObject.GetComponent<PlayerController>().currentPower = "Water";
                Debug.Log(gameObject.GetComponent<PlayerController>().currentPower);
                break;
            case "Invincible":
                gameObject.GetComponent<PlayerController>().invincible = true;
                Debug.Log(gameObject.GetComponent<PlayerController>().currentPower);
                break;
            case null:
                gameObject.GetComponent<PlayerController>().currentPower = "Defualt";
                Debug.Log(gameObject.GetComponent<PlayerController>().currentPower);
                break;
        }
    }
}
