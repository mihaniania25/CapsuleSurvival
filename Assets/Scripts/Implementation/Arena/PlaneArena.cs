using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class PlaneArena : MonoBehaviour, IArena
    {
        private const int CORNER_VERTICE_INDEX_1 = 0;
        private const int CORNER_VERTICE_INDEX_2 = 10;
        private const int CORNER_VERTICE_INDEX_3 = 110;
        private const int CORNER_VERTICE_INDEX_4 = 120;

        private const int MAX_SPACE_SEARCH_ATTEMPTS = 1000;

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

            Vector3 corner1 = _planeTransform.TransformPoint(planeMesh.vertices[CORNER_VERTICE_INDEX_1]);
            Vector3 corner2 = _planeTransform.TransformPoint(planeMesh.vertices[CORNER_VERTICE_INDEX_2]);
            Vector3 corner3 = _planeTransform.TransformPoint(planeMesh.vertices[CORNER_VERTICE_INDEX_3]);
            Vector3 corner4 = _planeTransform.TransformPoint(planeMesh.vertices[CORNER_VERTICE_INDEX_4]);

            _minX = Mathf.Min(corner1.x, corner2.x, corner3.x, corner4.x);
            _maxX = Mathf.Max(corner1.x, corner2.x, corner3.x, corner4.x);
            _minZ = Mathf.Min(corner1.z, corner2.z, corner3.z, corner4.z);
            _maxZ = Mathf.Max(corner1.z, corner2.z, corner3.z, corner4.z);
        }

        public Vector3 GetFreeRandomPosition(ISimpleVolumetric volumetric)
        {
            float minAllowableX = _minX + volumetric.Radius;
            float maxAllowableX = _maxX - volumetric.Radius;
            float minAllowableZ = _minZ + volumetric.Radius;
            float maxAllowableZ = _maxZ - volumetric.Radius;

            int attempt = 0;
            Vector3 randomPosition;

            do
            {
                attempt++;

                randomPosition = new Vector3
                (
                    Random.Range(minAllowableX, maxAllowableX),
                    _planeTransform.position.y,
                    Random.Range(minAllowableZ, maxAllowableZ)
                );
            }
            while (attempt <= MAX_SPACE_SEARCH_ATTEMPTS && HasVolumetricIntersections(volumetric, randomPosition));

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