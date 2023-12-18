using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class PlaneArena : MonoBehaviour, IArena
    {
        [SerializeField] private Transform _playerSpawnPositionHolder;
        [SerializeField] private Transform _planeTransform;
        [SerializeField] private MeshFilter _planeMeshFilter;

        private float _minX;
        private float _maxX;
        private float _minZ;
        private float _maxZ;

        private GameContext _gameContext => AppContext.GameContext;
        private PlayerBase _player => _gameContext.Player.Value;
        private ParticipantsRegister _participantsRegister => _gameContext.ParticipantsRegister;

        public void Setup()
        {
            CalculatePlaneCorners();
        }

        private void CalculatePlaneCorners()
        {
            Mesh planeMesh = _planeMeshFilter.sharedMesh;

            Vector3 corner1 = _planeTransform.TransformPoint(planeMesh.vertices[0]);
            Vector3 corner2 = _planeTransform.TransformPoint(planeMesh.vertices[10]);
            Vector3 corner3 = _planeTransform.TransformPoint(planeMesh.vertices[110]);
            Vector3 corner4 = _planeTransform.TransformPoint(planeMesh.vertices[120]);

            _minX = Mathf.Min(corner1.x, corner2.x, corner3.x, corner4.x);
            _maxX = Mathf.Max(corner1.x, corner2.x, corner3.x, corner4.x);
            _minZ = Mathf.Min(corner1.z, corner2.z, corner3.z, corner4.z);
            _maxZ = Mathf.Max(corner1.z, corner2.z, corner3.z, corner4.z);


            //_planeMesh.vertices

            //Vector3 planePosition = _planeTransform.position;
            //Vector3 planeHalfScale = _planeTransform.lossyScale / 2.0f;

            //_minX = planePosition.x - planeHalfScale.x;
            //_maxX = planePosition.x + planeHalfScale.x;
            //_minZ = planePosition.z - planeHalfScale.z;
            //_maxZ = planePosition.z + planeHalfScale.z;
        }

        public Vector3 GetFreeRandomPosition(ISimpleVolumetric volumetric)
        {
            float minAllowableX = _minX + volumetric.Radius;
            float maxAllowableX = _maxX - volumetric.Radius;
            float minAllowableZ = _minZ + volumetric.Radius;
            float maxAllowableZ = _maxZ - volumetric.Radius;

            Vector3 randomPosition;

            do
            {
                randomPosition = new Vector3
                (
                    Random.Range(minAllowableX, maxAllowableX),
                    _planeTransform.position.y,
                    Random.Range(minAllowableZ, maxAllowableZ)
                );
            }
            while (HasVolumetricIntersections(volumetric, randomPosition));

            return randomPosition;
        }

        private bool HasVolumetricIntersections(ISimpleVolumetric volumetric, Vector3 atPosition)
        {
            if (IsThereIntersection(volumetric, atPosition, _player))
                return true;

            foreach (GameParticipant participant in _participantsRegister.AllParticipants)
            {
                if (IsThereIntersection(volumetric, atPosition, participant))
                    return true;
            }

            return false;
        }

        private bool IsThereIntersection(ISimpleVolumetric volumetricToPlace, Vector3 placePosition, ISimpleVolumetric placedVolumetric)
        {
            float minAllowableDistance = volumetricToPlace.Radius + placedVolumetric.Radius;
            float actualDistance = Vector3.Distance(placePosition, placedVolumetric.Position);

            return actualDistance <= minAllowableDistance;
        }

        public Vector3 GetPlayerSpawnPosition()
        {
            return _playerSpawnPositionHolder.position;
        }
    }
}