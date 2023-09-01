using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnem : MonoBehaviour
{
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Knockback(int dmg, Vector2 knockback)
    {
        rigid.velocity = new Vector2(rigid.velocity.x + knockback.x, rigid.velocity.y + knockback.y);
    }
}
