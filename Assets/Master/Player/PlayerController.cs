using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject Projectile;
    public Vector2 projectileVelocity;
    public Vector2 projectileOffset;
    public float cooldown = 0.5f;

    private bool _isCanShoot = true;
    private bool _isJump = true;
    private bool _isDead = false;
    private int _idMove = 0;
    private Animator _anim;

    // Use this for initialization
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Jump "+isJump);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Idle();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Idle();
        }

		if (Input.GetKeyDown(KeyCode.Z))
		{
            Fire();
		}

        Move();
        Dead();
    }

    void Fire()
	{
        if (_isCanShoot)
        {
            GameObject bullet = Instantiate(Projectile, (Vector2)transform.position - projectileOffset * transform.localScale.x, Quaternion.identity);
            
            Vector2 velocity = new Vector2(projectileVelocity.x * transform.localScale.x, projectileVelocity.y);
            bullet.GetComponent<Rigidbody2D>().velocity = velocity * -1;
            //Menyesuaikan scale dari projectile dengan scale karakter
            Vector3 scale = transform.localScale;
            bullet.transform.localScale = scale * -1;
            
            StartCoroutine(CanShoot());
            _anim.SetTrigger("Shoot");
        }
    }

    IEnumerator CanShoot()
	{
        _anim.SetTrigger("Shoot");
        _isCanShoot = false;
        yield return new WaitForSeconds(cooldown);
        _isCanShoot = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Kondisi ketika menyentuh tanah
        if (_isJump)
        {
            _anim.ResetTrigger("Jump");
            if (_idMove == 0) _anim.SetTrigger("Idle");
            _isJump = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Kondisi ketika menyentuh tanah
        _anim.SetTrigger("Jump");
        _anim.ResetTrigger("Run");
        _anim.ResetTrigger("Idle");
        _isJump = true;
    }

    public void MoveRight()
    {
        _idMove = 1;
    }

    public void MoveLeft()
    {
        _idMove = 2;
    }

    private void Move()
    {
        if (_idMove == 1 && !_isDead)
        {
            // Kondisi ketika bergerak ke kekanan
            if (!_isJump) _anim.SetTrigger("Run");
            transform.Translate(1 * Time.deltaTime * 5f, 0,
            0);
            transform.localScale = new Vector3(-1f, 1f, 1f)
            ;
        }
        if (_idMove == 2 && !_isDead)
        {
            // Kondisi ketika bergerak ke kiri



            if (!_isJump) _anim.SetTrigger("Run");
            transform.Translate(-1 * Time.deltaTime * 5f, 0
            , 0);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void Jump()
    {
        if (!_isJump)
        {
            // Kondisi ketika Loncat
            gameObject.GetComponent<Rigidbody2D>().AddForce
            (Vector2.up * 300f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Peluru"))
        {
            _isCanShoot = true;
        }

        if (collision.transform.tag.Equals("Enemy"))
        {
            _isDead = true;
            SceneManager.LoadScene("GameOver");
        }
        
        if (collision.transform.tag.Equals("Coin"))
        {
            Data.score += 15;
            Destroy(collision.gameObject);
        }
    }

    public void Idle()
    {
        // kondisi ketika idle/diam
        if (!_isJump)
        {
            _anim.ResetTrigger("Jump");
            _anim.ResetTrigger("Run");
            _anim.SetTrigger("Idle");
        }
        _idMove = 0;
    }

    private void Dead()
    {
        if (!_isDead)
        {
            if (transform.position.y < -10f)
            {
                // kondisi ketika jatuh
                _isDead = true;
            }
        }
    }
}
