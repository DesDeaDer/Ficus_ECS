using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class BeginDragSystem : ComponentSystem
{
    //use "inject" in the future
    private Camera Camera => GamePoint.Instance.Camera;

    private Vector3 MousePosition => Camera.ScreenToWorldPoint(Input.mousePosition);

    private const float SIZE_DETECTION = 0.5f;

    protected override void OnUpdate()
    {
        var mousePosition = (float3)MousePosition;
        Entities.ForEach((Entity entity, RenderMesh renderMesh, ref Translation translation, ref ClickCount clickCount) =>
        {
            var isMouseDown = Input.GetMouseButtonDown(0);
            if (isMouseDown)
            {
                var distance = math.distance(mousePosition.xy, translation.Value.xy);
                var isDetection = distance < SIZE_DETECTION;
                if (isDetection)
                {
                    ++clickCount.Count;

                    EntityManager.AddComponent<Follow>(entity);

                    var offset = mousePosition.xy - translation.Value.xy;

                    EntityManager.SetComponentData(entity, new Follow() { Offset = new float3(offset.x, offset.y, 0) });
                }
            }
        });
    }
}
