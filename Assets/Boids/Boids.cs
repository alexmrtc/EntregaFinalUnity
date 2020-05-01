using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    float distance;

    [SerializeField]
    GameObject agentPrefab;

    [SerializeField]
    int numBoids = 10;

    Agent[] agents;

    [SerializeField]
    float agentRadius = 2.0f;

    [SerializeField]
    float separationWeight = 3.0f, cohesionWeight = 3.0f, alignmentWeight = 3.0f;

    private void Awake()
    {
        List<Agent> agentlist = new List<Agent>();

        for (int i = 0; i < numBoids; i++)
        {
            Vector3 position = Vector3.up * Random.Range(0, 10)
                + Vector3.right * Random.Range(0, 10) + Vector3.forward * Random.Range(0, 10);
            agentlist.Add(Instantiate(agentPrefab, position, Quaternion.identity).GetComponent<Agent>());

        }
        agents = agentlist.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Agent a in agents)
        {
            a.velocity = a.vel;
            //checkForNeightBours(a);
            a.checkNeightbours();
            calculateCohesion(a);
            calculateSeparation(a);
            calculateAlignment(a);
            a.updateAgent();
            a.neightbours.Clear();

        }
    }

    void checkForNeightBours(Agent a)
    {

    }

    void calculateSeparation(Agent a)
    {
        Vector3 separationForce = Vector3.zero;

        foreach (Agent neighbour in a.neightbours)
        {

            distance = Vector3.Distance(neighbour.transform.position, a.transform.position);
            distance /= agentRadius;
            distance = 1 - distance;

            separationForce += distance * (a.transform.position - neighbour.transform.position) * separationWeight;
        }
        a.addForce(separationForce, Agent.DEBUGforceType.SEPARATION);

    }

    void calculateCohesion(Agent a)
    {
        Vector3 centralPosition = Vector3.zero;

        foreach (Agent neightbours in a.neightbours)
        {
            centralPosition += neightbours.transform.position;
        }

        centralPosition += a.transform.position;
        centralPosition /= a.neightbours.Count + 1;

        a.addForce((centralPosition- a.transform.position) * cohesionWeight, Agent.DEBUGforceType.COHESION);
    }

    void calculateAlignment(Agent a)
    {
        Vector3 directorVector = Vector3.zero;

        foreach (Agent neightbour in a.neightbours)
        {
            directorVector += neightbour.velocity;
        }

        directorVector += a.velocity;
        directorVector /= a.neightbours.Count + 1;
        a.addForce(directorVector, Agent.DEBUGforceType.ALIGNMENT);
    }
}
