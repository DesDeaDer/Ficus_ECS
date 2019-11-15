using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

public class EndDragSystem : ComponentSystem
{
    private const float DISTANCE_COLOR_CHANGE = 0.5f;

    protected override void OnUpdate()
    {
        var isMouseUp = Input.GetMouseButtonUp(0);
        if (isMouseUp)
        {
            Entities.WithAll<Follow>().ForEach((Entity entity, RenderMesh renderMesh, ref ClickCount clickCount, ref Follow follow) =>
            {
                if (follow.DragDistance < DISTANCE_COLOR_CHANGE)
                {
                    EntityManager.AddComponent<ChangeColorEvent>(entity);
                }

                PostUpdateCommands.RemoveComponent<Follow>(entity);
            });
        }
    }
}
