using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Reborn
{
    public class NavigationBaker : MonoBehaviour
    {

        public NavMeshSurface[] surfaces;

        public void BakeNavMesh()
        {
            for (int i = 0; i < surfaces.Length; i++)
            {
                surfaces[i].BuildNavMesh();
            }
        }

        public void DelayRebake()
        {
            StartCoroutine(RebakeLater());
        }

        IEnumerator RebakeLater()
        {
            yield return new WaitForSeconds(0.5f);
            BakeNavMesh();
        }
    }
}
