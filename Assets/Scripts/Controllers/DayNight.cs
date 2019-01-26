using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
public class DayNight: MonoBehaviour {
 
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0,1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;

	public float timeOfSunrise = 0.1f;
	public float timeOfSunset = 0.9f;

	public float lastCurrentTimeOfDay = 0.5f;
	public float yAngle = -30f;
    
    float sunInitialIntensity;
    
    void Start() {
        sunInitialIntensity = sun.intensity;
    }
    
    void Update() {


		if (Application.isPlaying) {

			UpdateSun ();

			currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

			if (currentTimeOfDay >= 1) {
				currentTimeOfDay = 0;
			}
		} else {

			if (lastCurrentTimeOfDay != currentTimeOfDay) {
				lastCurrentTimeOfDay = currentTimeOfDay;
				UpdateSun ();
			}
			
		}
    }
    
    void UpdateSun() {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 240f) - 60f, yAngle, 0);
 
        float intensityMultiplier = 1;
        if (currentTimeOfDay <= timeOfSunrise - 0.02f || currentTimeOfDay >= timeOfSunset) {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= timeOfSunrise) {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - timeOfSunrise - 0.02f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= timeOfSunset -0.02f) {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - timeOfSunset - 0.02f) * (1 / 0.02f)));
        }
 
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
