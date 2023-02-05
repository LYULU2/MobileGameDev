using System;

namespace Model
{
    //editable in the inspector
    [Serializable]
    public class Data
    {
        public int id;

        public float curDistance;

        public override string ToString(){
            return UnityEngine.JsonUtility.ToJson (this, true);
        }
    }
}