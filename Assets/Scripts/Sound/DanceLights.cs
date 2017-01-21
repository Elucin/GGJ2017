using UnityEngine;
using System.Collections;

public class DanceLights : MonoBehaviour
{
    public Material floorLight;
    void OnEnable()
    {
        BeatTest.onBeat += Strobe;
    }

    void OnDisable()
    {
        BeatTest.onBeat -= Strobe;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Strobe()
    {
        float dir_hue = 0f;

        Light[] lights = GameObject.FindObjectsOfType<Light>();
        foreach (Light l in lights)
        {
            Color rand = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            float hue;
            float sat;
            float val;
            Color.RGBToHSV(rand, out hue, out sat, out val);
            if (l.transform.name.Contains("Directional"))
                dir_hue = hue;
            l.color = Color.HSVToRGB(hue, 1f, 1f);

            if (l.name.Contains("Spot"))
            {
                int en = Random.Range(0, 2);
                l.enabled = en == 1;
            }
        }
        //Color rand1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //float hue1;
        //float sat1;
        //float val1;
        //Color.RGBToHSV(rand1, out hue1, out sat1, out val1);

        floorLight.color = Color.HSVToRGB(1f - dir_hue, 1f, 1f);
    }
}
