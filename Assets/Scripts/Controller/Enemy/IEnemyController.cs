using UnityEngine;
/// <summary>
/// Interface containing a method to get the selection light from enemies.
/// </summary>
public interface IEnemyController : IController
{
    Light GetSelectionLight();
}
