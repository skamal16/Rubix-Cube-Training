using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RubixAgent : Agent
{
    private RubixCube rubixCube;
    public int scramble = 20;
    public bool curriculum = false;
    private RubixAcademy academy;

    // Start is called before the first frame update
    void Start()
    {
        rubixCube = GetComponent<RubixCube>();
        academy = GetComponentInParent<RubixAcademy>();
    }

    public override void AgentReset()
    {
        base.AgentReset();
        if (curriculum)
        {
            scramble = (int)academy.resetParameters["scramble"];
            agentParameters.maxStep = (int)academy.resetParameters["steps"];
        }

        //Debug.Log("AgentReset");
        Restore();
    }

    public void Restore()
    {
        rubixCube.Restore();
        rubixCube.Scramble(scramble);
        //Debug.Log(scramble);
    }

    public override void CollectObservations()
    {
        base.CollectObservations();

        List<int> state = rubixCube.GetState();

        //Debug.Log(state.Count);
        
        for(int i = 0; i < state.Count; i++)
        {
            AddVectorObs(state[i]);
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        base.AgentAction(vectorAction, textAction);

        ReadAction((int)vectorAction[0]);

        // Solved Cube
        if (rubixCube.UpdateState())
        {
            SetReward(1.0f);
            Done();
        }
        else
        {
            SetReward(-0.02f);
        }
    }

    void ReadAction(int action)
    {
        switch (action)
        {
            case 0:
                break;
            case 1:
                rubixCube.RotateFront();
                break;
            case 2:
                rubixCube.RotateBack();
                break;
            case 3:
                rubixCube.RotateLeft();
                break;
            case 4:
                rubixCube.RotateRight();
                break;
            case 5:
                rubixCube.RotateTop();
                break;
            case 6:
                rubixCube.RotateBottom();
                break;
            case 7:
                rubixCube.RotateFront(true);
                break;
            case 8:
                rubixCube.RotateBack(true);
                break;
            case 9:
                rubixCube.RotateLeft(true);
                break;
            case 10:
                rubixCube.RotateRight(true);
                break;
            case 11:
                rubixCube.RotateTop(true);
                break;
            case 12:
                rubixCube.RotateBottom(true);
                break;
        }
    }
}
