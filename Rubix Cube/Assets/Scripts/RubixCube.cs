using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubixCube : MonoBehaviour
{

    private Transform core;
    private Dictionary<string, Transform> blocks;
    private Face top, bottom, left, right, front, back;
    private List<Face> faces;
    private Dictionary<string, Vector3> Pos;
    private Dictionary<string, Quaternion> Rot;
    private Dictionary<string, int> Values;
    private List<int> state;
    private List<int> doneState;

    // Start is called before the first frame update
    void Start()
    {
        core = transform.GetChild(0);
        InitBlocks();
        InitFaces();
        doneState = new List<int>(state);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckInput();
        //Debug.Log("Running");
    }

    public List<int> GetState()
    {
        return state;
    }

    public bool UpdateState()
    {
        state.Clear();

        //foreach (Face f in faces)
        //{
        //    foreach (Transform t in f.edges)
        //    {
        //        state.Add(Values[t.name]);
        //    }
        //}

        foreach (Transform t in top.edges)
        {
            state.Add(Values[t.name]);
        }

        state.Add(Values[back.edges[4].name]);
        state.Add(Values[back.edges[3].name]);

        state.Add(Values[front.edges[3].name]);
        state.Add(Values[front.edges[4].name]);

        foreach (Transform t in bottom.edges)
        {
            state.Add(Values[t.name]);
        }

        for (int i = 0; i < state.Count; i++)
        {
            if (state[i] != doneState[i])
                return false;
        }

        return true;
    }

    void InitBlocks()
    {
        Transform t = core.GetChild(0);
        blocks = new Dictionary<string, Transform>();
        Pos = new Dictionary<string, Vector3>();
        Rot = new Dictionary<string, Quaternion>();
        Values = new Dictionary<string, int>();

        for (int i = 0; i < t.childCount; i++)
        {
            Transform x = t.GetChild(i);
            blocks.Add(x.name, x);
            Pos.Add(x.name, x.position);
            Rot.Add(x.name, x.rotation);
            Values.Add(x.name, i);
        }
    }

    void InitFaces()
    {
        top = new Face(blocks["Yellow"], blocks["YellowOrangeBlue"], blocks["YellowOrange"], blocks["YellowGreenOrange"], blocks["YellowBlue"], blocks["YellowGreen"], blocks["YellowBlueRed"], blocks["YellowRed"], blocks["YellowRedGreen"]);
        bottom = new Face(blocks["White"], blocks["WhiteBlueRed"], blocks["WhiteRed"], blocks["WhiteRedGreen"], blocks["WhiteBlue"], blocks["WhiteGreen"], blocks["WhiteOrangeBlue"], blocks["WhiteOrange"], blocks["WhiteGreenOrange"]);
        front = new Face(blocks["Red"], blocks["YellowBlueRed"], blocks["YellowRed"], blocks["YellowRedGreen"], blocks["BlueRed"], blocks["RedGreen"], blocks["WhiteBlueRed"], blocks["WhiteRed"], blocks["WhiteRedGreen"]);
        back = new Face(blocks["Orange"], blocks["YellowGreenOrange"], blocks["YellowOrange"], blocks["YellowOrangeBlue"], blocks["GreenOrange"], blocks["OrangeBlue"], blocks["WhiteGreenOrange"], blocks["WhiteOrange"], blocks["WhiteOrangeBlue"]);
        left = new Face(blocks["Blue"], blocks["YellowOrangeBlue"], blocks["YellowBlue"], blocks["YellowBlueRed"], blocks["OrangeBlue"], blocks["BlueRed"], blocks["WhiteOrangeBlue"], blocks["WhiteBlue"], blocks["WhiteBlueRed"]);
        right = new Face(blocks["Green"], blocks["YellowRedGreen"], blocks["YellowGreen"], blocks["YellowGreenOrange"], blocks["RedGreen"], blocks["GreenOrange"], blocks["WhiteRedGreen"], blocks["WhiteGreen"], blocks["WhiteGreenOrange"]);

        faces = new List<Face>
        {
            top,
            bottom,
            front,
            back,
            left,
            right
        };

        state = new List<int>();

        //foreach (Face f in faces)
        //{
        //    foreach (Transform t in f.edges)
        //    {
        //        state.Add(Values[t.name]);
        //    }
        //}

        foreach (Transform t in top.edges)
        {
            state.Add(Values[t.name]);
        }

        state.Add(Values[back.edges[4].name]);
        state.Add(Values[back.edges[3].name]);

        state.Add(Values[front.edges[3].name]);
        state.Add(Values[front.edges[4].name]);

        foreach (Transform t in bottom.edges)
        {
            state.Add(Values[t.name]);
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            RotateX1();
        else if (Input.GetKeyDown(KeyCode.S))
            RotateX2();
        else if (Input.GetKeyDown(KeyCode.A))
            RotateY1();
        else if (Input.GetKeyDown(KeyCode.D))
            RotateY2();
        else if (Input.GetKeyDown(KeyCode.Q))
            RotateZ1();
        else if (Input.GetKeyDown(KeyCode.E))
            RotateZ2();
        else if (Input.GetKeyDown(KeyCode.Keypad7))
            RotateFront();
        else if (Input.GetKeyDown(KeyCode.Keypad9))
            RotateBack();
        else if (Input.GetKeyDown(KeyCode.Keypad4))
            RotateLeft();
        else if (Input.GetKeyDown(KeyCode.Keypad6))
            RotateRight();
        else if (Input.GetKeyDown(KeyCode.Keypad8))
            RotateTop();
        else if (Input.GetKeyDown(KeyCode.Keypad5))
            RotateBottom();
        else if (Input.GetKeyDown(KeyCode.R))
            Restore();
        else if (Input.GetKeyDown(KeyCode.T))
            Scramble();
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(UpdateState());
        }
    }

    public void Restore()
    {
        foreach ( string t in blocks.Keys)
        {
            blocks[t].position = Pos[t];
            blocks[t].rotation = Rot[t];
        }

        InitFaces();
    }

    public void Scramble(int x = 20)
    {

        while(x > 0)
        {

            switch (Random.Range(0, 12))
            {
                case 0:
                    RotateFront();
                    break;
                case 1:
                    RotateBack();
                    break;
                case 2:
                    RotateLeft();
                    break;
                case 3:
                    RotateRight();
                    break;
                case 4:
                    RotateTop();
                    break;
                case 5:
                    RotateBottom();
                    break;
                case 6:
                    RotateFront(true);
                    break;
                case 7:
                    RotateBack(true);
                    break;
                case 8:
                    RotateLeft(true);
                    break;
                case 9:
                    RotateRight(true);
                    break;
                case 10:
                    RotateTop(true);
                    break;
                case 11:
                    RotateBottom(true);
                    break;

            }

            x--;
        }

    }

    void RotateX1()
    {
        core.Rotate(90, 0, 0);
    }

    void RotateX2()
    {
        core.Rotate(-90, 0, 0);
    }

    void RotateY1()
    {
        core.Rotate(0, 90, 0);
    }

    void RotateY2()
    {
        core.Rotate(0, -90, 0);
    }

    void RotateZ1()
    {
        core.Rotate(0, 0, 90);
    }

    void RotateZ2()
    {
        core.Rotate(0, 0, -90);
    }

    public void RotateFront(bool reverse = false)
    {
        if (reverse)
            front.RotateAntiClockwise();
        else
            front.RotateClockwise();

        top.edges[5] = front.edges[0];
        top.edges[6] = front.edges[1];
        top.edges[7] = front.edges[2];

        left.edges[2] = front.edges[0];
        left.edges[4] = front.edges[3];
        left.edges[7] = front.edges[5];

        bottom.edges[0] = front.edges[5];
        bottom.edges[1] = front.edges[6];
        bottom.edges[2] = front.edges[7];

        right.edges[0] = front.edges[2];
        right.edges[3] = front.edges[4];
        right.edges[5] = front.edges[7];
    }

    public void RotateBack(bool reverse = false)
    {
        if (reverse)
            back.RotateAntiClockwise();
        else
            back.RotateClockwise();

        top.edges[2] = back.edges[0];
        top.edges[1] = back.edges[1];
        top.edges[0] = back.edges[2];

        left.edges[0] = back.edges[2];
        left.edges[3] = back.edges[4];
        left.edges[5] = back.edges[7];

        bottom.edges[7] = back.edges[5];
        bottom.edges[6] = back.edges[6];
        bottom.edges[5] = back.edges[7];

        right.edges[2] = back.edges[0];
        right.edges[4] = back.edges[3];
        right.edges[7] = back.edges[5];
    }

    public void RotateLeft(bool reverse = false)
    {
        if (reverse)
            left.RotateAntiClockwise();
        else
            left.RotateClockwise(); ;

        top.edges[0] = left.edges[0];
        top.edges[3] = left.edges[1];
        top.edges[5] = left.edges[2];

        front.edges[0] = left.edges[2];
        front.edges[3] = left.edges[4];
        front.edges[5] = left.edges[7];

        bottom.edges[5] = left.edges[5];
        bottom.edges[3] = left.edges[6];
        bottom.edges[0] = left.edges[7];

        back.edges[2] = left.edges[0];
        back.edges[4] = left.edges[3];
        back.edges[7] = left.edges[5];
    }

    public void RotateRight(bool reverse = false)
    {
        if (reverse)
            right.RotateAntiClockwise();
        else
            right.RotateClockwise();

        top.edges[7] = right.edges[0];
        top.edges[4] = right.edges[1];
        top.edges[2] = right.edges[2];

        front.edges[2] = right.edges[0];
        front.edges[4] = right.edges[3];
        front.edges[7] = right.edges[5];

        bottom.edges[2] = right.edges[5];
        bottom.edges[4] = right.edges[6];
        bottom.edges[7] = right.edges[7];

        back.edges[0] = right.edges[2];
        back.edges[3] = right.edges[4];
        back.edges[5] = right.edges[7];
    }

    public void RotateTop(bool reverse = false)
    {
        if (reverse)
            top.RotateAntiClockwise();
        else
            top.RotateClockwise();

        left.edges[0] = top.edges[0];
        left.edges[1] = top.edges[3];
        left.edges[2] = top.edges[5];

        front.edges[0] = top.edges[5];
        front.edges[1] = top.edges[6];
        front.edges[2] = top.edges[7];

        right.edges[2] = top.edges[2];
        right.edges[1] = top.edges[4];
        right.edges[0] = top.edges[7];

        back.edges[2] = top.edges[0];
        back.edges[1] = top.edges[1];
        back.edges[0] = top.edges[2];
    }

    public void RotateBottom(bool reverse = false)
    {
        if (reverse)
            bottom.RotateAntiClockwise();
        else
            bottom.RotateClockwise();

        left.edges[7] = bottom.edges[0];
        left.edges[6] = bottom.edges[3];
        left.edges[5] = bottom.edges[5];

        front.edges[5] = bottom.edges[0];
        front.edges[6] = bottom.edges[1];
        front.edges[7] = bottom.edges[2];

        right.edges[5] = bottom.edges[2];
        right.edges[6] = bottom.edges[4];
        right.edges[7] = bottom.edges[7];

        back.edges[7] = bottom.edges[5];
        back.edges[6] = bottom.edges[6];
        back.edges[5] = bottom.edges[7];
    }

    class Face
    {
        Transform center;
        public List<Transform> edges;

        public Face(Transform center, Transform tl, Transform t, Transform tr, Transform l, Transform r, Transform bl, Transform b, Transform br)
        {
            this.center = center;
            edges = new List<Transform> { tl, t, tr, l, r, bl, b, br };
        }

        public void RotateClockwise()
        {
            center.RotateAround(center.position, center.up, 90);
            foreach (Transform i in edges)
            {
                i.RotateAround(center.position, center.up, 90);
            }

            List<Transform> temp = new List<Transform> { edges[5], edges[3], edges[0], edges[6], edges[1], edges[7], edges[4], edges[2] };
            edges = temp;
        }

        public void RotateAntiClockwise()
        {
            center.RotateAround(center.position, center.up, -90);
            foreach (Transform i in edges)
            {
                i.RotateAround(center.position, center.up, -90);
            }

            List<Transform> temp = new List<Transform> { edges[2], edges[4], edges[7], edges[1], edges[6], edges[0], edges[3], edges[5] };
            edges = temp;
        }
    }

}
