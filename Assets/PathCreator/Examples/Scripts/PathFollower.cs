using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public bool isLeader;
        public GameObject leader;

        public Material active;
        public Material inactive;

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public float distanceTravelled = 0;

        void Start() {
            distanceTravelled = 0;
            SetTransform();
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                if (isLeader)
                {
                    distanceTravelled += speed * Time.deltaTime;
                }
                else
                {
                    distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(leader.transform.position);
                }
                SetTransform();
            }
        }

        public void SetTransform()
        {
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }


        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        public void Activate(bool isActive)
        {
            if (isActive)
            {
                GetComponent<MeshRenderer>().material = active;
            }
            else
            {
                GetComponent<MeshRenderer>().material = inactive;
            }
        }
    }
}