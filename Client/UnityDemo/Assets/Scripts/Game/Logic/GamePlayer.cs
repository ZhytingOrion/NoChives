using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public KeyCode Key_Collect;
    public KeyCode Key_Open;
    public KeyCode Key_MoveObj;

    public List<Color> collectedColors = new List<Color>();
    public List<Color> collectedKeys = new List<Color>();
    private bool pauseMove = false;
    public TextMesh playerName;

    //key and door
    private bool isInKey = false;
    KeyController inKeyController = null;
    private bool isInDoor = false;
    DoorController inDoorController = null;

    //Canvas
    GameObject LevelCanvas = null;

    void Start()
    {
        if (rig == null)
            rig = this.GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = this.GetComponent<Animator>();
        leftJumpNum = MaxJumpNum;
        jumpTimeLeft = -1.0f;

        LevelCanvas = GameObject.Find("LevelCanvas");
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
            transform.Find("Body").localScale = new Vector3(horizontalmove, 1, 1); // 控制角色转身
        }
        anim.SetFloat("walkSpeed", Mathf.Abs(horizontalmove));

        if(Input.GetKeyDown(KeyCode.Space) && leftJumpNum > 0 && jumpTimeLeft <= 0.0f)
        {
            rig.AddForce(new Vector2(0, jumpSpeed));
            leftJumpNum--;
            jumpTimeLeft = jumpTimeLimit;
            anim.SetBool("isJumpping", true);
        }

        //key and door
        if(isInKey)
        {
            if(Input.GetKeyDown(Key_Collect) && inKeyController!=null)
            {
                CollectAKey(inKeyController.color);
            }
        }
        
        if(isInDoor)
        {
            if(Input.GetKeyDown(Key_Open) && inDoorController!=null)
            {
                if (OpenADoor(inDoorController.color))
                    inDoorController.OnOpened();
            }
        }

    }

    public void CollectAKey(Color color)
    {
        collectedKeys.Add(color);

        Destroy(inKeyController.gameObject);
        isInKey = false;
        inKeyController = null;

        PlayerManager.Instance.SyncPlayerState(this);

        //send message
    }

    public bool OpenADoor(Color color)
    {
        if(collectedKeys.Contains(color) && inDoorController.CanBeOpen)
        {
            collectedKeys.Remove(color);
            collectedColors.Add(color);

            PlayerManager.Instance.SyncPlayerState(this);
            if (LevelCanvas != null)
                LevelCanvas.GetComponent<GlobalCanvasUI>().HideUsefulKey();
            return true;
        }
        else
        {
            return false;
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
            PlayerManager.Instance.SyncPlayerState(this);

            pauseMove = true;
            //send message

        }

        else if(collidedObject.gameObject.layer == LayerMask.NameToLayer("Key"))
        {
            isInKey = true;
            inKeyController = collidedObject.GetComponent<KeyController>();
        }

        else if(collidedObject.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            isInDoor = true;
            inDoorController = collidedObject.GetComponent<DoorController>();
            if(collectedKeys.Contains(inDoorController.color))
            {
                if (LevelCanvas != null)
                {
                    GlobalCanvasUI lc = LevelCanvas.GetComponent<GlobalCanvasUI>();
                    lc.ShowUsefulKey(inDoorController.color);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.gameObject.layer == LayerMask.NameToLayer("Key"))
        {
            isInKey = false;
            inKeyController = null;
        }

        else if(collidedObject.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            isInDoor = false;
            inDoorController = null;
            if(LevelCanvas!=null)
            {
                LevelCanvas.GetComponent<GlobalCanvasUI>().HideUsefulKey();
            }
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
