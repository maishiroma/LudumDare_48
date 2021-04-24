namespace Obstacle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PointSystem : MonoBehaviour
    {
        [SerializeField]
        private int pointAmount;

        public int GetPointAmount
        {
               get { return pointAmount; }
        }

    }

}