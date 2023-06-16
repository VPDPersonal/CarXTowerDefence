using UnityEngine;

namespace Projectiles.Cannon
{
    [CreateAssetMenu(fileName = "New Cannon Data", menuName = "Projectiles/Cannon", order = 0)]
    public class CannonData : ProjectileData, ICannonData { }
}
