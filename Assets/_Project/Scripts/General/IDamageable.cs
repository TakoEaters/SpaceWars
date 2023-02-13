namespace _Project.Scripts.General
{
    public interface IDamageable
    {
        public bool IsAlive { get; }
        public void OnTakeDamage(int damage);
    }
}
