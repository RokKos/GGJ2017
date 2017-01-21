//Author: Rok Kos <kosrok97@gmail.com>
//File: EnemyBaseClass.cs
//File path: /D/Documents/Unity/GGJ2017/EnemyBaseClass.cs
//Date: 20.01.2017
//Description: Basic Enemy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass {
    protected byte tip;
    protected Vector3 startPos;
    protected Vector3 endPos;  // Only need with statci enemy

    public EnemyBaseClass (byte _tip, Vector3 _startPos, Vector3 _endPos) {
        this.tip = _tip;
        this.startPos = _startPos;
        this.endPos = _endPos;
    }

    public byte getType () {
        return this.tip;
    }

    public Vector3 getStartPos () {
        return this.startPos;
    }

    public Vector3 getEndPos () {
        return this.endPos;
    }

    public virtual void nextMove (Transform currentPos, float speed) {
        //Vector3 newDir = endPos - currentPos.position;
        //currentPos.rotation = Quaternion.LookRotation(newDir.normalized);
        //currentPos.position += currentPos.up * speed;
        currentPos.position = Vector3.MoveTowards(currentPos.position, this.endPos, speed);
        
    }
}
