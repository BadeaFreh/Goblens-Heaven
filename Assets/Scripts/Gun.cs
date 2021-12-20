using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public int currentAmmo;
    public int pickupAmount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetAmmo()
    {
        currentAmmo += pickupAmount;

        UIController.instance.ammoText.text = "AMMO: " + currentAmmo;
    }
}
