using UnityEngine;

public class AbilitySnipe : MonoBehaviour
{
    GameObject playerGO;
    Player playerScript;

    bool canSnipe = true;
    bool isSniping = false;
    int snipeShotCount = 0;
    int maxSnipeShotCount = 2;

    void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<Player>();
    }

    void Update()
    {
        if (InputManager.isRMB)
            if (canSnipe)
                Snipe(true);
            else
                Snipe(false);
        else
        {
            Snipe(false);
            canSnipe = true;
        }

        if (isSniping)
        {
            if (InputManager.isLMBDown)
            {
                playerScript.GetRightHand().transform.Find("Pistol").GetComponent<Gun>().Fire();
                snipeShotCount++;

                if (snipeShotCount >= maxSnipeShotCount)
                {
                    canSnipe = false;
                    Snipe(false);
                    snipeShotCount = 0;
                }
            }
        }

    }

    void Snipe(bool isSniping)
    {
        Camera.main.GetComponent<MouseOrbitImproved>().Snipe(isSniping);
        playerScript.SetCharacterController(!isSniping);
        this.isSniping = isSniping;
    }
}