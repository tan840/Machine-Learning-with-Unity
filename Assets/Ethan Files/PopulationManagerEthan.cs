using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManagerEthan : MonoBehaviour
{
    public GameObject botPrefab;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trailTime = 5;
    int generation = 1;

     void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
                                                this.transform.position.y,
                                                this.transform.position.z + Random.Range(-2, 2));
            GameObject ethanInstansiate = Instantiate(botPrefab, startingPos, this.transform.rotation);
            ethanInstansiate.GetComponent<Brain>().Init();
            population.Add(ethanInstansiate);
        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
                                                this.transform.position.y,
                                                this.transform.position.z + Random.Range(-2, 2));
        GameObject offspring = Instantiate(botPrefab, startingPos, this.transform.rotation);
        Brain brain = offspring.GetComponent<Brain>();
        if (Random.Range(0,100)==1)
        {
            brain.Init();
            brain.dna.Mutate();
        }
        else
        {
            brain.Init();
            brain.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }
        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> sortList = population.OrderBy(o => o.GetComponent<Brain>().timeAlive).ToList();
        population.Clear();

        for (int i = (int)(sortList.Count/2.0f); i < sortList.Count -1; i++)
        {
            population.Add(Breed(sortList[i], sortList[i + 1]));
            population.Add(Breed(sortList[i+1], sortList[i]));

        }

        for (int i = 0; i < sortList.Count; i++)
        {
            Destroy(sortList[i]);
        }
        generation++;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed>= trailTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

}
