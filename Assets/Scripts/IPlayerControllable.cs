using UnityEngine;

public interface IPlayerControllable
{
    public void Move(Vector2 axis);
    public void MoveCamera(Vector2 axis);
    public void Attack1();
    public void Attack2();
    public void Jump();
}
