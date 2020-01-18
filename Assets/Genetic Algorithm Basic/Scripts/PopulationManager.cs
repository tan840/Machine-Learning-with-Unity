using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PopulationManager : MonoBehaviour
{
    public GameObject personPrefab;
    public int populationSize = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 10;
    int Generation = 1;

	
	 

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9f,9f),Random.Range(-6f,6f),0);
            GameObject Go = Instantiate(personPrefab,pos, Quaternion.identity);
            Go.GetComponent<DNA_Script>().r = Random.Range(0f,1f);
            Go.GetComponent<DNA_Script>().g = Random.Range(0f, 1f);
            Go.GetComponent<DNA_Script>().b = Random.Range(0f, 1f);
            Go.GetComponent<DNA_Script>().s = Random.Range(0.1f,0.3f);
            population.Add(Go);


        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9f,9f), Random.Range(-4.5f, 4.5f),0);
        GameObject offspring = Instantiate(personPrefab,pos,Quaternion.identity);
        DNA_Script dna1 = parent1.GetComponent<DNA_Script>();
        DNA_Script dna2 = parent2.GetComponent<DNA_Script>();


        // swap DNA
        if (Random.Range(0f,1000f)>5)
        {
            offspring.GetComponent<DNA_Script>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA_Script>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA_Script>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<DNA_Script>().s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;
        }
        else
        {
            offspring.GetComponent<DNA_Script>().r = Random.Range(0f, 1f);
            offspring.GetComponent<DNA_Script>().g = Random.Range(0f, 1f);
            offspring.GetComponent<DNA_Script>().b = Random.Range(0f, 1f);
            offspring.GetComponent<DNA_Script>().s = Random.Range(0.1f, 0.3f);
        }

        

        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA_Script>().timeToDie).ToList();

        population.Clear();
        for (int i = (int) (sortedList.Count/2f)-1; i < sortedList.Count-1; i++)
        {
            population.Add(Breed(sortedList[i],sortedList[i+1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        Generation++;

    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed>trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
