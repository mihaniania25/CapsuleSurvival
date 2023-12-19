using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class InputReaderGetter : MonoBehaviour
    {
        [SerializeField] private PCInputReader _pcInputReader;
        [SerializeField] private TouchesInputReader _touchesInputReader;

        public IUserInputReader GetUserInputReader()
        {
            _pcInputReader.gameObject.SetActive(false);
            _touchesInputReader.gameObject.SetActive(false);

#if UNITY_EDITOR || UNITY_STANDALONE
            _pcInputReader.gameObject.SetActive(true);
            return _pcInputReader;
#else
            _touchesInputReader.gameObject.SetActive(true);
            return _touchesInputReader;
#endif
        }
    }
}