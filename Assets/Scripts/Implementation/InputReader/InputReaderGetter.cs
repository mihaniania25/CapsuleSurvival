using UnityEngine;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class InputReaderGetter : MonoBehaviour
    {
        [SerializeField] private PCInputReader _pcInputReader;
        
        public IUserInputReader GetUserInputReader()
        {
            _pcInputReader.gameObject.SetActive(false);

            GameLog.Error("[InputReaderGetter] TODO touch input reader");

#if UNITY_EDITOR || UNITY_STANDALONE
            _pcInputReader.gameObject.SetActive(true);
            return _pcInputReader;
#endif
        }
    }
}