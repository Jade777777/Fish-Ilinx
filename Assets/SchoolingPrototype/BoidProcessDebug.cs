using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidProcessDebug : MonoBehaviour
{
    /*
    public bool ShowCells;
    private BoidProcess boidProcess;
    private void Awake()
    {
        boidProcess = GetComponent<BoidProcess>();
        Debug.Log(boidProcess);
    }
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        if (ShowCells && boidProcess!=null)
        {
            float voxelSize = boidProcess.boidGridPartition.GetVoxelSize();
            foreach(KeyValuePair<Vector3Int,List<Boid>> kvp in boidProcess.boidGridPartition.GetVoxelToBoids())
            {
                Gizmos.color = new Color(1, 0, 0, 1 - (0.9f / kvp.Value.Count));
                Vector3 voxelPos = new(kvp.Key.x, kvp.Key.y, kvp.Key.z);

                Gizmos.DrawCube(boidProcess.boidGridPartition.GetVoxelPosition(kvp.Key), new Vector3(voxelSize,voxelSize,voxelSize));
               
            }
        }
    }
    */
}
