// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using UnityEngine;

namespace RadarComponents
{
    [CreateAssetMenu(fileName = "New TypeRadar Icon", menuName = "RadarComponents/TypeRadarIcon",order = 0)]
    public class TypeIconTarget : ScriptableObject
    {
        [SerializeField]
        private string idType = default;

        public string IdType => idType;

        [SerializeField]
        private Sprite iconTarget = default;

        public Sprite IconTarget => iconTarget;
    }
}