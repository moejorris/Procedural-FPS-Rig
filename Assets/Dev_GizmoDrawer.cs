using UnityEngine;

//Share this on the internet? Could be free LinkedIn attention...?

public class Dev_GizmoDrawer : MonoBehaviour
{
    [SerializeField] GizmoType gizmoType = GizmoType.Sphere;
    [SerializeField] bool isWireframe = true;
    [SerializeField] bool useLocalSpace = true;

    [SerializeField] Color color = Color.blue;
    [SerializeField] float Width = 0.5f;
    [SerializeField] float Height = 0.5f;
    [SerializeField] float Depth = 0.5f;
    [SerializeField] Mesh mesh;

    [SerializeField] Vector3 positionOffset;

    Vector3 position;
    Quaternion rotation;
    Vector3 scale;
    Vector3 size;

    enum GizmoType
    {
        Sphere,
        Box,
        Mesh
    }

    void OnDrawGizmos()
    {
        if (!enabled) return;

        if (useLocalSpace)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            position = Vector3.zero;
            rotation = Quaternion.identity;
            scale = Vector3.one;
        }
        else
        {
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }

        position += positionOffset;

        if (gizmoType == GizmoType.Sphere)
        {
            Height = Width;
            Depth = Width;
        }

        size = new Vector3(Width * scale.x, Height * scale.x, Depth * scale.z);

        Gizmos.color = color;

        switch (gizmoType)
        {
            case GizmoType.Box:
                DrawBox();
                break;

            case GizmoType.Mesh:
                DrawMesh();
                break;

            case GizmoType.Sphere:
                DrawSphere();
                break;
        }
    }

    void DrawSphere()
    {
        if (isWireframe)
        {
            Gizmos.DrawWireSphere(position, size.x);// * transform.localScale.x);
        }
        else
        {
            Gizmos.DrawSphere(position, size.x);// * transform.localScale.x);
        }
    }

    void DrawBox()
    {
        if (isWireframe)
        {
            Gizmos.DrawWireCube(position, size);
        }
        else
        {
            Gizmos.DrawCube(position, size);
        }
    }

    void DrawMesh()
    {
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            if (isWireframe)
            {
                Gizmos.DrawWireMesh(mesh, 0, position, rotation, size);
            }
            else
            {
                Gizmos.DrawMesh(mesh, 0, position, rotation, size);
            }
        }
    }
}
