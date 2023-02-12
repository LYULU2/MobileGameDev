using System;

namespace Model
{
    [Serializable]
    public class ResetData
    {
        public string sceneName;
        public float curDistance;
        public float playTime;
        public int collectBlue;
        public int collectYellow;

        public override string ToString(){
            return UnityEngine.JsonUtility.ToJson (this, true);
        }
    }
}