using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RubixAcademy : Academy
{
    private List<RubixAgent> agents;

    public override void InitializeAcademy()
    {
        base.InitializeAcademy();

        agents = new List<RubixAgent>();

        for(int i = 0; i < transform.childCount; i++)
            agents.Add(transform.GetChild(i).GetComponent<RubixAgent>());
    }

    public override void AcademyReset()
    {
        base.AcademyReset();

        foreach (RubixAgent agent in agents)
        {
            //agent.Done();
            agent.Restore();
        }
    }
}
