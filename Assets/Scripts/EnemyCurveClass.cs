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

    // Constuructor and calling base constructor
    public EnemyCurveClass (byte _tip, Vector3 _startPos, Vector3 _endPos, float _speed, float _timeStart, Vector3 _userPosition) : 
           base(_tip, _startPos, _endPos, _speed) {

        this.timeStart = _timeStart;
        this.userPosition = _userPosition;

    }

    public override Vector3 nextMove (Vector3 currentPos) {
        float timeAlive = Mathf.Clamp01((Time.time - this.timeStart));
        // Bezeir formula
        float curveX = (((1 - timeAlive) * (1 - timeAlive)) * startPos.x) + (2 * timeAlive * (1 - timeAlive) * userPosition.x) + ((timeAlive * timeAlive) * endPos.x);
        float curveY = (((1 - timeAlive) * (1 - timeAlive)) * startPos.y) + (2 * timeAlive * (1 - timeAlive) * userPosition.y) + ((timeAlive * timeAlive) * endPos.y);
        currentPos.x = curveX;
        currentPos.y = curveY;

        return base.nextMove(currentPos);
    }
}
