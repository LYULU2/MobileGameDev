using System;

namespace Model
{
    //editable in the inspector
    [Serializable]
    public class Data
    {
        public float curDistance;
        public int resetTimes;
        public float playTime;
        public int collectBlue;
        public int collectYellow;

        public override string ToString(){
            return UnityEngine.JsonUtility.ToJson (this, true);
        }
    }
}