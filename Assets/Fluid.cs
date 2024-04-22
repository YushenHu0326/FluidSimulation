using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour
{
    // gravity
    public float g = -9.8f;
    // rendering of the particles
    public Mesh particleMesh;
    public Material particleMat;
    public int particleNum=5000;
    GameObject[] particles;
    // particle properties
    Vector3[] p_pos;
    Vector3[] p_vel;

    // boundary
    public int boundXMin = -20;
    public int boundXMax = 20;
    public int boundYMin = -20;
    public int boundYMax = 20;
    public int boundZMin = -20;
    public int boundZMax = 20;

    void AddGravity()
    {
        for (int i = 0; i < particleNum; i++)
        {
            p_vel[i].y += g * Time.deltaTime;
        }
    }

    void AddMovement()
    {
        for (int i = 0; i < particleNum; i++)
        {
            p_pos[i] = p_pos[i] + p_vel[i];
            particles[i].transform.position = p_pos[i];
        }
    }

    void BoundaryCheck()
    {
        for (int i = 0; i < particleNum; i++)
        {
            if (p_pos[i].x < boundXMin)
            {
                p_pos[i].x = boundXMin + 0.01f;
                p_vel[i].x = -p_vel[i].x * 0.5f;
            }
            if (p_pos[i].x > boundXMax)
            {
                p_pos[i].x = boundXMax - 0.01f;
                p_vel[i].x = -p_vel[i].x * 0.5f;
            }

            if (p_pos[i].y < boundYMin)
            {
                p_pos[i].y = boundYMin + 0.01f;
                p_vel[i].y = -p_vel[i].y * 0.5f;
            }
            if (p_pos[i].y > boundYMax)
            {
                p_pos[i].y = boundYMax - 0.01f;
                p_vel[i].y = -p_vel[i].y * 0.5f;
            }

            if (p_pos[i].z < boundZMin)
            {
                p_pos[i].z = boundZMin + 0.01f;
                p_vel[i].z = -p_vel[i].z * 0.5f;
            }
            if (p_pos[i].z > boundZMax)
            {
                p_pos[i].z = boundZMax - 0.01f;
                p_vel[i].z = -p_vel[i].z * 0.5f;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // initialize particle
        particles = new GameObject[particleNum];
        p_pos = new Vector3[particleNum];
        p_vel = new Vector3[particleNum];

        for (int i = 0; i < particleNum; i++)
        {
            p_pos[i] = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

            GameObject p = new GameObject();
            p.transform.position = p_pos[i];
            p.AddComponent<MeshFilter>();
            p.AddComponent<MeshRenderer>();
            p.GetComponent<MeshFilter>().mesh = particleMesh;
            p.GetComponent<MeshRenderer>().material = particleMat;
            particles[i] = p;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AddGravity();

        BoundaryCheck();
        AddMovement();
    }
}
