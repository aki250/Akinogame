using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife instance;  // 添加单例 instance

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private float respawnDelay = 2f;  // 重生延迟时间

    public Image healthBar;
    public float currentHealth, maxHealth, healthRegen;

    private void Awake()
    {
        // 实现单例模式
        if (instance == null)
        {
            instance = this;  // 如果 instance 为空，将当前对象设置为实例
        }
        else
        {
            Destroy(gameObject);  // 如果已经有实例，销毁当前对象，确保只存在一个 PlayerLife 实例
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // 确保 healthBar 已赋值，避免空引用错误
        if (healthBar != null)
        {
            healthBar.fillAmount = GetPercentage();  // 更新血条
        }

        Heal(healthRegen * Time.deltaTime);  // 不断恢复健康值
    }

    // 处理受伤害逻辑
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0.0f);

        if (currentHealth == 0)
        {
            Die();  // 如果健康值为0，触发死亡
        }
    }

    // 处理回血逻辑
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);  // 限制健康值不能超过上限

    }

    // 获取当前健康百分比，避免除以零
    public float GetPercentage()
    {
        return maxHealth > 0 ? currentHealth / maxHealth : 0;
    }

    // 死亡处理
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;  // 设置刚体为静止
        anim.SetTrigger("death");  // 触发死亡动画
        deathSoundEffect.Play();

        // 禁用 PlayerMovement 脚本，避免角色死亡后出现报错
        GetComponent<PlayerMovement>().enabled = false;
        // 延迟重生
        Invoke(nameof(RestartLevel), respawnDelay);  // 在 respawnDelay 秒后重新加载关卡
    }

    // 重生方法，重新加载当前关卡
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 重新加载当前场景
    }
}
