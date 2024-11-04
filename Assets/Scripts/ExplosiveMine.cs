using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeForActivate;

    private bool _isActive;

    private float _time;

    private void Update()
    {
        if (_isActive == false)
            if (CanActivate())
                ActivateMine();
            
        if (_isActive)
        {
            _time += Time.deltaTime;

            if (_time >= _timeForActivate)
            {
                Explode();
            }
        }
    }

    private bool CanActivate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
            IDamagable damagableObject = collider.GetComponent<IDamagable>();

            if (damagableObject != null)
                return true;
        }

        return false;
    }

    private void ActivateMine()
    {
        _isActive = true;
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
            IDamagable damagableObject = collider.GetComponent<IDamagable>();

            if (damagableObject != null)
                damagableObject.TakeDamage(_damage);
        }

        Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity, null);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
