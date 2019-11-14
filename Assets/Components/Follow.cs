using Unity.Entities;
using Unity.Mathematics;

public struct Follow : IComponentData
{
    public float3 Offset;
    public float DragDistance;
}
