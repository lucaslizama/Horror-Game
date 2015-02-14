using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour {

    public Slider batteryHUD;
    public GameObject barFill;
    public float batteryPower;
    public float drainSpeed;
    public bool isPowered;
    public bool hasBatteryLeft;

    private Light flashLight;

	// Use this for initialization
	void Awake () {
        flashLight = GetComponentInChildren<Light>();
        isPowered = false;
        hasBatteryLeft = true;
        flashLight.enabled = isPowered;
	}
	
	// Update is called once per frame
	void Update () {
        turnOnOff();
        killFlashlight();
	}

    void FixedUpdate() {
        consumeBattery();
    }

    void killFlashlight() {
        if (batteryPower <= 0f && isPowered)
        {
            barFill.SetActive(false);
            batteryPower = 0f;
            isPowered = false;
            flashLight.enabled = false;
        }
    }

    void turnOnOff() {
        if(hasBatteryLeft){
            if (Input.GetKeyDown(KeyCode.F) && !isPowered)
            {
                flashLight.enabled = true;
                isPowered = true;
            }
            else if (Input.GetKeyDown(KeyCode.F) && isPowered)
            {
                flashLight.enabled = false;
                isPowered = false;
            }
        }
    }

    void consumeBattery() {
        if(isPowered){
            batteryHUD.value = batteryPower;
            batteryPower -= drainSpeed * Time.fixedDeltaTime;           
        }
    }
}
