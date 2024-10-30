using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife instance;  // ��ӵ��� instance

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private float respawnDelay = 2f;  // �����ӳ�ʱ��

    public Image healthBar;
    public float currentHealth, maxHealth, healthRegen;

    private void Awake()
    {
        // ʵ�ֵ���ģʽ
        if (instance == null)
        {
            instance = this;  // ��� instance Ϊ�գ�����ǰ��������Ϊʵ��
        }
        else
        {
            Destroy(gameObject);  // ����Ѿ���ʵ�������ٵ�ǰ����ȷ��ֻ����һ�� PlayerLife ʵ��
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // ȷ�� healthBar �Ѹ�ֵ����������ô���
        if (healthBar != null)
        {
            healthBar.fillAmount = GetPercentage();  // ����Ѫ��
        }

        Heal(healthRegen * Time.deltaTime);  // ���ϻָ�����ֵ
    }

    // �������˺��߼�
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0.0f);

        if (currentHealth == 0)
        {
            Die();  // �������ֵΪ0����������
        }
    }

    // �����Ѫ�߼�
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);  // ���ƽ���ֵ���ܳ�������

    }

    // ��ȡ��ǰ�����ٷֱȣ����������
    public float GetPercentage()
    {
        return maxHealth > 0 ? currentHealth / maxHealth : 0;
    }

    // ��������
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;  // ���ø���Ϊ��ֹ
        anim.SetTrigger("death");  // ������������
        deathSoundEffect.Play();

        // ���� PlayerMovement �ű��������ɫ��������ֱ���
        GetComponent<PlayerMovement>().enabled = false;
        // �ӳ�����
        Invoke(nameof(RestartLevel), respawnDelay);  // �� respawnDelay ������¼��عؿ�
    }

    // �������������¼��ص�ǰ�ؿ�
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // ���¼��ص�ǰ����
    }
}
