using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class DragSystem : ComponentSystem
{
    private Camera Camera => GamePoint.Instance.Camera;
    private Vector3 MousePosition => Camera.ScreenToWorldPoint(Input.mousePosition);

    protected override void OnUpdate()
    {
        var mousePosition = (float3)MousePosition;
        Entities.ForEach((Entity entity, ref Translation translation, ref Follow follow) =>
        {
            var to = mousePosition.xy - follow.Offset.xy;
            var distance = math.distance(translation.Value.xy, to);

            translation.Value.xy = to;
            follow.DragDistance += distance;
        });
    }
}
