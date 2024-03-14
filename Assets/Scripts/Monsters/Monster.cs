using UnityEngine;
using Managers;
using Monsters;
using static Models.GameSettings;

public class Monster : MonoBehaviour, IDamageable, IMovable {
	public bool IsAlive { get; private set; }
	public Transform Transform => transform;
	public Transform MoveTargetTransform { get; private set; }
	public float MoveSpeed => m_speed;

	private MonsterSettings m_monsterSettings;
	private GamePool<Monster> m_monsterPool;
	
	private float m_currentHp;
	private float m_speed;
	
	private void Awake() {
		m_monsterPool = PoolManager.GetOrCreatePool<Monster>();
	}

	private void OnDestroy() {
		m_monsterPool.Remove(this);
	}
	
	public void Init(MonsterSettings monsterSettings) {
		m_monsterSettings = monsterSettings;
		IsAlive = true;
		gameObject.SetActive(true);
		InitializeParameters();
	}

	private void InitializeParameters() {
		m_currentHp = m_monsterSettings.m_maxHP;
		m_speed = m_monsterSettings.m_speed;
	}

	public void ApplyDamage(float damage) {
		m_currentHp -= damage;
		if (m_currentHp <= 0) {
			Kill();
		}
	}

	public void Kill() {
		IsAlive = false;
		gameObject.SetActive(false);
	}

	public void Move(Transform target)
	{
		MoveTargetTransform = target;
		transform.position = Vector3.MoveTowards(transform.position, target.position, m_speed);
	}
}
