using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidGridPartition
{
    private float voxelSize;

    private int partitionOffset = 1000;

    private Dictionary<Vector3Int, List<Boid>> VoxelToBoids = new Dictionary<Vector3Int, List<Boid>>();

    private Dictionary<Boid, Vector3Int> BoidToVoxel = new Dictionary<Boid, Vector3Int>();


    public BoidGridPartition(float voxelSize)
    {
        this.voxelSize = voxelSize;
    }



    public void UpdateBoid(Boid b)
    {
        Vector3Int newVoxel = GetVoxel(b.transform.position);
        if (BoidToVoxel.TryGetValue(b, out Vector3Int previousVoxel))
        {
            if (newVoxel != previousVoxel)
            {
                BoidToVoxel.Remove(b);
                BoidToVoxel.Add(b, newVoxel);



                if (VoxelToBoids[previousVoxel].Count <= 1)
                {
                    VoxelToBoids.Remove(previousVoxel);
                }
                else
                {
                    VoxelToBoids[previousVoxel].Remove(b);
                }


                if (VoxelToBoids.TryGetValue(newVoxel, out List<Boid> boids))
                {
                    boids.Add(b);
                }
                else
                {
                    VoxelToBoids.Add(newVoxel, new List<Boid> { b });
                }
            }
        }
        else//if the boid has not been intitialized
        {
            BoidToVoxel.Add(b, newVoxel);
            if (VoxelToBoids.TryGetValue(newVoxel, out List<Boid> boids))
            {
                boids.Add(b);
            }
            else
            {
                VoxelToBoids.Add(newVoxel, new List<Boid> { b });
            }


        }
    }
    public Vector3Int GetVoxel(Boid boid)
    {
        return BoidToVoxel[boid];
    }

    public Vector3Int GetVoxel(Vector3 position)
    {
        Vector3 scaledPosition = (position + new Vector3(partitionOffset, partitionOffset, partitionOffset)) / voxelSize;
        Vector3Int voxel = new Vector3Int((int)scaledPosition.x, (int)scaledPosition.y, (int)scaledPosition.z);
        return voxel;
    }

    public Vector3 GetVoxelPosition(Vector3Int voxel)
    {
        float totalOffset = partitionOffset - 0.5f * voxelSize;
        Vector3 UnscaledPosition = new Vector3(voxel.x, voxel.y, voxel.z) * voxelSize - new Vector3(totalOffset, totalOffset, totalOffset);
        return UnscaledPosition;
    }

    public Dictionary<Vector3Int, List<Boid>> GetVoxelToBoids()
    {
        return VoxelToBoids;
    }

    public float GetVoxelSize()
    {
        return voxelSize;
    }

    public List<Boid> GetBoidsInRange(Boid boid, float range, float viewAngle)
    {
        List<Boid> boidsInRange = new List<Boid>();
        Vector3Int originVoxel = GetVoxel(boid);
        int distanceInVoxels = Mathf.CeilToInt(range / voxelSize);// 0,0,0 is the boids origin
        Vector3Int offset;
        for(int x  = -distanceInVoxels; x <= distanceInVoxels; x++)
        {
            for(int y = -distanceInVoxels; y <= distanceInVoxels; y++)
            {
                for(int z = -distanceInVoxels; z <= distanceInVoxels; z++)
                {
                    offset = new Vector3Int(x, y, z);

                    if (VoxelToBoids.TryGetValue(originVoxel + offset, out List<Boid> boids))
                    {
                        for (int i = 0; i < boids.Count; i++)
                        {
                            Boid neighbor = VoxelToBoids[originVoxel + offset][i];
                            Vector3 posOffset =  neighbor.transform.position- boid.transform.position;
                            if (boid != neighbor &&
                                posOffset.magnitude <= range &&
                                Vector3.Angle(boid.transform.forward, posOffset) <= viewAngle *0.5f)
                            {
                                boidsInRange.Add(neighbor);
                                

                            }
                        }
                    }
                }
            }
        }
        boid.neighbors = boidsInRange;
        return boidsInRange;
    }
    public List<Boid> GetBoidsInRange(Vector3 position, float range)
    {
        List<Boid> boidsInRange = new List<Boid>();
        Vector3Int originVoxel = GetVoxel(position);
        int distanceInVoxels = Mathf.CeilToInt(range / voxelSize);// 0,0,0 is the boids origin
        Vector3Int offset;
        for (int x = -distanceInVoxels; x <= distanceInVoxels; x++)
        {
            for (int y = -distanceInVoxels; y <= distanceInVoxels; y++)
            {
                for (int z = -distanceInVoxels; z <= distanceInVoxels; z++)
                {
                    offset = new Vector3Int(x, y, z);

                    if (VoxelToBoids.TryGetValue(originVoxel + offset, out List<Boid> boids))
                    {
                        for (int i = 0; i < boids.Count; i++)
                        {
                            Boid neighbor = VoxelToBoids[originVoxel + offset][i];
                            boidsInRange.Add(neighbor);
                        }
                    }
                }
            }
        }

        return boidsInRange;
    }

}