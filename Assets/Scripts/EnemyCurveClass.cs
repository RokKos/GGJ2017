//Author: Rok Kos <kosrok97@gmail.com>
//File: EnemyCurveClass.cs
//File path: /D/Documents/Unity/GGJ2017/EnemyCurveClass.cs
//Date: 20.01.2017
//Description: Little bit advanced enemy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurveClass : EnemyBaseClass {
    protected float timeStart;
    protected Vector3 userPosition;
    //private float intitalDistance; 

    // Constuructor and calling base constructor
    public EnemyCurveClass (byte _tip, Vector3 _startPos, Vector3 _endPos, float _timeStart, Vector3 _userPosition) : 
           base(_tip, _startPos, _endPos) {

        this.timeStart = 0;//_timeStart;
        this.userPosition = _userPosition;
        //this.intitalDistance = Vector3.Distance(this.startPos, this.endPos);
    }

    public override Vector3 nextMove (Vector3 currentPos, float speed) {

        //float currentDistanceToEnd = Vector3.Distance(currentPos, endPos);
        float timeAlive = Mathf.Clamp01(timeStart);//(Time.time - this.timeStart));
        timeStart += Time.deltaTime * 0.1f;
        // Bezeir formula
        float curveX = (((1 - timeAlive) * (1 - timeAlive)) * startPos.x) + (2 * timeAlive * (1 - timeAlive) * userPosition.x) + ((timeAlive * timeAlive) * endPos.x);
        float curveY = (((1 - timeAlive) * (1 - timeAlive)) * startPos.y) + (2 * timeAlive * (1 - timeAlive) * userPosition.y) + ((timeAlive * timeAlive) * endPos.y);
        currentPos.x = curveX;
        currentPos.y = curveY;

        return base.nextMove(currentPos, speed);
    }
}
