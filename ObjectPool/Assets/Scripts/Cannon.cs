using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Utils.Pool;
using Random = UnityEngine.Random;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Animator cannonAnimator;
    [SerializeField] private Transform cannonAim;
    [SerializeField] private ParticleSystem[] explosionEffectPrefabs;
    [SerializeField] private DamageScreen damageScreenPrefab;
    [SerializeField] private float fireSpeed = 1f;
    [SerializeField] private float spreadAngle = 10f;
    [Header("Effects pool")]
    [SerializeField] private int poolSizeOnEffect = 10;
    [SerializeField] private int shotsAtTime = 1;
    [SerializeField] private bool fillAtStart = true;
    [SerializeField] private bool expandable = true;
    [SerializeField] private bool badPool = false;
    [Header("Damage screens pool")]

    private List<ObjectPool<ParticleSystem>> _particlePools;
    private int _explosionEffectsCount;
    private ObjectPool<DamageScreen> _damageScreenPool;
    
    private void Start()
    {
        cannonAnimator.speed = fireSpeed;
        _explosionEffectsCount = explosionEffectPrefabs.Length;
        _particlePools = new List<ObjectPool<ParticleSystem>>();
        
        foreach (var effect in explosionEffectPrefabs)
        {
            _particlePools.Add(new ObjectPool<ParticleSystem>(effect, poolSizeOnEffect, fillAtStart, expandable));
        }
        
        _damageScreenPool = new ObjectPool<DamageScreen>(damageScreenPrefab, poolSizeOnEffect*2, fillAtStart, expandable);
        
        ActionTimer.Instance.InfinityCall(MakeShotsAtTime, 0.25f/fireSpeed);
    }

    private void MakeShotsAtTime()
    {
        for (int i = 0; i < shotsAtTime; i++)
        {
            Shoot();
        }
    }
    
    private void Shoot()
    {
        var fireDirection =
            Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f)*cannonAim.forward;
        
        if (!Physics.Raycast(cannonAim.position, fireDirection, out var hit, float.MaxValue)) return;
        
        var effectIndex = Random.Range(0, _explosionEffectsCount);
        var particle = _particlePools[effectIndex].GetObject();
        particle.Clear();
        particle.transform.position = hit.point + hit.normal;
        particle.Play();
        
        if (badPool)
        {
            ActionTimer.Instance.SingleCall(() => { Destroy(particle.gameObject); }, particle.main.startLifetime.constant );
            var damageScreen = _damageScreenPool.GetObject();
            damageScreen.ShowDamage(
                Random.Range(0,100),
                2f/fireSpeed, 
                hit.point + 2*hit.normal,
                Camera.main.transform.position,
                () => { Destroy(damageScreen.gameObject); });
        }
        else
        {
            ActionTimer.Instance.SingleCall(() => { _particlePools[effectIndex].DeactivateObject(particle); }, particle.main.startLifetime.constant );
            var damageScreen = _damageScreenPool.GetObject();
            damageScreen.ShowDamage(
                Random.Range(0,100),
                2f/fireSpeed, 
                hit.point + 2*hit.normal,
                Camera.main.transform.position,
                () => { _damageScreenPool.DeactivateObject(damageScreen); });
        }
    }
    
}
