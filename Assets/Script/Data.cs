using System;

namespace Model
{
    [Serializable]
    public class Data
    {
        public string sceneName;
        public float curDistance;
        public int resetTimes;
        public float playTime;
        public int collectBlue;
        public int collectYellow;
        public int collectRed;
        public int collectReducePackage;
        public int numHitEnemy;
        public int numBounceEnemy;

        public override string ToString(){
            return UnityEngine.JsonUtility.ToJson (this, true);
        }
    }
}