using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class Person : MonoBehaviour {

    public bool open;
    public GameManager game;
    public CommunityHealth health;
    public int happiness;
    public int sadness;
    //persons below will most likely have to be individual gameobjects so each 
    //person animation spritesheet is associated with each individual.
    public Sprite[] persons;
    private SpriteRenderer personRenderer;
    public GameObject reaction;
    Animator anim;
    AnimatorStateInfo stateInfo;

    int happyHash = Animator.StringToHash("reaction-happy");
    int sadHash = Animator.StringToHash("reaction-unhappy");

    int idleHash = Animator.StringToHash("Idle");




    // Use this for initialization
    void Start()
    {
        anim = reaction.gameObject.GetComponent<Animator>();
        game = FindObjectOfType<GameManager>();
        health = game.GetComponent<CommunityHealth>();
        personRenderer = gameObject.transform.Find("personSprite").GetComponent<SpriteRenderer>();
        personRenderer.sprite = persons[0];
        //for random person
        //RandomPerson();
        //stateInfo = anim.GetCurrentAnimatorStateInfo(0);

    }


    public void Happy(){
        Debug.Log("Happy!");
        //if (stateInfo.fullPathHash == idleHash)
        //{
            
            //anim.SetTrigger(happyHash);
        anim.Play("reaction-happy");
        //}

    }

    public void Sad(){
        Debug.Log("Sad!");
        //if (stateInfo.fullPathHash == idleHash)
        //{
            anim.Play("reaction-unhappy");
        //}

    }



    void RandomPerson()
    {
        Debug.Log("random person");
        personRenderer.sprite = persons[Random.Range(0, persons.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(stateInfo.fullPathHash);

    }
}
