using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

public class EndDragSystem : ComponentSystem
{
    private const float DISTANCE_COLOR_CHANGE = 0.5f;

    protected override void OnUpdate()
    {
        Entities.WithAll<Follow>().ForEach((Entity entity, RenderMesh renderMesh, ref ClickCount clickCount, ref Follow follow) =>
        {
            var isMouseUp = Input.GetMouseButtonUp(0);

            if (isMouseUp)
            {
                if (follow.DragDistance < DISTANCE_COLOR_CHANGE)
                {
                    EntityManager.AddComponent<ChangeColorEvent>(entity);
                }

                EntityManager.RemoveComponent<Follow>(entity);
            }
        });
    }
}
