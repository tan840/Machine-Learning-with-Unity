using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_Script : MonoBehaviour
{
    // Gene for colour( variables to store colour values)
    public float r;
    public float g;
    public float b;
    public float s;

    bool isDead = false;

    public float timeToDie = 0;

    Collider2D sCollider;
    SpriteRenderer sRenderer;






    // Start is called before the first frame update

	void OnMouseDown()
	{
		isDead = true;
		sRenderer.enabled = false;
		sCollider.enabled = false;
        timeToDie = PopulationManager.elapsed;
		
	}

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();

        sCollider = GetComponent<Collider2D>();

        sRenderer.color = new Color(r,g,b);

        this.transform.localScale = new Vector3(s,s,s);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
