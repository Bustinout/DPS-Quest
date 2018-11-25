using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blademaster : MonoBehaviour {

    public Character self;

    public float resourceCap = 100;
    public float resourceGen = 2; // per second

    public Slider ResourceBar;
    public UnityEngine.UI.Text ResourceText;


    IEnumerator ResourceTick()
    {
        while (true)
        {
            if (self.resource < resourceCap)
            {
                self.resource += resourceGen / 20;
            }
            else
            {
                self.resource = resourceCap;
            }
            updateResourceBar();
            updateResourceShades();
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void updateResourceBar()
    {
        ResourceBar.value = self.resource / resourceCap * 100;
        ResourceText.text = "Rage: " + self.resource.ToString("f0") + "/" + resourceCap;
    }

    void Start () {
        self.resource = 0;
        StartCoroutine("ResourceTick");
	}
	
    public void updateResourceShades()
    {
        self.updateResourceShade();
    }
}
