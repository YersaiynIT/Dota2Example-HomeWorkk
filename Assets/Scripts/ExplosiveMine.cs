using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionSound;

    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeForActivate;

    private bool _isActive;

    private float _time;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = _radius;
    }

    private void Update()
    {
        if (_isActive)
        {
            _time += Time.deltaTime;

            if (_time >= _timeForActivate)
                Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive == false)
            ActivateMine();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
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
        _audioSource.PlayOneShot(_explosionSound);

        Destroy(gameObject);
    }
}
