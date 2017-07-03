namespace DynamicLight2D
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SticksTest : MonoBehaviour
    {
        public PolygonCollider2D pc2;
        public MeshFilter mf;

        // Use this for initialization
        void Start()
        {
            pc2 = GetComponent<PolygonCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            mf = GetComponent<MeshFilter>();
            pc2.SetPath(0, path(mf.mesh.vertices));

        }

        Vector2[] path(Vector3[] i3)
        {
            Vector2[] i2 = new Vector2[i3.Length];
            for (int i = 0; i < i3.Length; i++)
            {
                i2[i] = new Vector2(i3[i].x, i3[i].y);
            }
            return i2;
        }
    }
}


