using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayer : MonoBehaviour
{
    public Rigidbody2D rig;
    public float speed = 1.0f;
    public Animator anim;
    public float jumpSpeed = 1.0f;
    public int MaxJumpNum = 2;
    private int leftJumpNum = 2;
    private float jumpTimeLimit = 0.35f;
    private float jumpTimeLeft = 0.2f;

    public List<Color> collectedColors = new List<Color>();
    private bool pauseMove = false;

    void Start()
    {
        if (rig == null)
            rig = this.GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = this.GetComponent<Animator>();
        leftJumpNum = MaxJumpNum;
        jumpTimeLeft = -1.0f;
    }

    void Update()
    {
        if(!pauseMove)
            Move();

        if (pauseMove && Input.GetKeyDown(KeyCode.P))
            PassLevel();
    }

    void Move()
    {
        if (jumpTimeLeft > 0.0f)
            jumpTimeLeft -= Time.deltaTime;

        if (rig.velocity.y.Equals(0.0f))
        {
            leftJumpNum = MaxJumpNum;
            anim.SetBool("isJumpping", false);
        }

        float horizontalmove = Input.GetAxisRaw("Horizontal"); // 从 input manager 接收“水平”输入的值。
        rig.velocity = new Vector2(horizontalmove * speed, rig.velocity.y);
        if (horizontalmove != 0)
        {
            transform.localScale = new Vector3(horizontalmove, 1, 1); // 控制角色转身
        }
        anim.SetFloat("walkSpeed", Mathf.Abs(horizontalmove));

        if(Input.GetKeyDown(KeyCode.Space) && leftJumpNum > 0 && jumpTimeLeft <= 0.0f)
        {
            rig.AddForce(new Vector2(0, jumpSpeed));
            leftJumpNum--;
            jumpTimeLeft = jumpTimeLimit;
            anim.SetBool("isJumpping", true);
        }

    }

    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if(collidedObject.gameObject.layer==LayerMask.NameToLayer("Aim"))
        {
            AimController ac = collidedObject.GetComponent<AimController>();
            if (ac == null)
                return;
            collectedColors.Add(ac.color);

            pauseMove = true;
            //send message

        }
    }

    public void PassLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        if(name.Equals("Level_2"))
        {
            SceneManager.LoadScene("EndScene");
        }
        else if(name.Contains("Level"))
        {
            string levelCount = name.Replace("Level_", "");
            int index = int.Parse(levelCount);
            SceneManager.LoadScene("Level_" + (index + 1));
        }
    }
}
