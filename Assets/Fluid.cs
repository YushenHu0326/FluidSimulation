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

    // grid
    public int gridX = 50;
    public int gridY = 50;
    public int gridZ = 50;
    int[,,] grid;
    float[] grid_vx;
    float[] grid_vy;
    float[] grid_vz;
    float grid_size_x;
    float grid_size_y;
    float grid_size_z;

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
            if (p_pos[i].x < 0)
            {
                p_pos[i].x = 0.01f;
                p_vel[i].x = -p_vel[i].x * 0.5f;
            }
            if (p_pos[i].x > gridX)
            {
                p_pos[i].x = gridX - 0.01f;
                p_vel[i].x = -p_vel[i].x * 0.5f;
            }

            if (p_pos[i].y < 0)
            {
                p_pos[i].y = 0.01f;
                p_vel[i].y = -p_vel[i].y * 0.5f;
            }
            if (p_pos[i].y > gridY)
            {
                p_pos[i].y = gridY - 0.01f;
                p_vel[i].y = -p_vel[i].y * 0.5f;
            }

            if (p_pos[i].z < 0)
            {
                p_pos[i].z = 0.01f;
                p_vel[i].z = -p_vel[i].z * 0.5f;
            }
            if (p_pos[i].z > gridZ)
            {
                p_pos[i].z = gridZ - 0.01f;
                p_vel[i].z = -p_vel[i].z * 0.5f;
            }
        }
    }

    void UpdateGrid()
    {
        for (int i = 0; i < particleNum; i++)
        {
            float gx = Mathf.Floor(p_pos[i].x);
            float gy = Mathf.Floor(p_pos[i].y);
            float gz = Mathf.Floor(p_pos[i].z);
            grid[(int)gx, (int)gy, (int)gz] += 1;
        }
    }

    void ParticleVelToGrid()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        // initialize grid
        grid = new int[gridX, gridY, gridZ];
        grid_vx = new float[(gridX - 1) * gridY * gridZ];
        grid_vy = new float[gridX * (gridY - 1) * gridZ];
        grid_vz = new float[gridX * gridY * (gridZ - 1)];

        // initialize particle
        particles = new GameObject[particleNum];
        p_pos = new Vector3[particleNum];
        p_vel = new Vector3[particleNum];

        for (int i = 0; i < particleNum; i++)
        {
            p_pos[i] = new Vector3(Random.Range(3f, 7f), Random.Range(3f, 7f), Random.Range(3f, 7f));

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
        UpdateGrid();


        AddMovement();
    }
}
