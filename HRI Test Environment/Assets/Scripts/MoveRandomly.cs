using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour{

        NavMeshAgent navMeshAgent;
        NavMeshPath path;
        public float timerForNewPath;
        bool inCoRoutine;
        Vector3 target;
        bool validPath;

        void Start(){
                navMeshAgent = GetComponent<NavMeshAgent>();
                path = new NavMeshPath();
        }

        void Update(){
                if (!inCoRoutine){
                        StartCoroutine(randomize());
                }
        }
        Vector3 getNewRandomPosition(){
                float x = Random.Range(-50, 50);
                float z = Random.Range(-50, 50);

                Vector3 pos = new Vector3(x, 0, z);
                return pos;
        }

        IEnumerator randomize(){
                inCoRoutine = true;
                yield return new WaitForSeconds(timerForNewPath);
                GetNewPath();
                validPath = navMeshAgent.CalculatePath(target,path);
                if(!validPath)
                Debug.Log("Found an invalid path");

                while(!validPath){
                        yield return new WaitForSeconds(0.01f);
                        GetNewPath();
                        validPath = navMeshAgent.CalculatePath(target,path);
                }
                inCoRoutine = false;
        }

        void GetNewPath(){
                target = getNewRandomPosition();
                navMeshAgent.SetDestination(target);
        }

}

// public class MoveRandomly : MonoBehaviour{
//
//         public float timer;
//         public int newTarget;
//         public float speed;
//         public UnityEngine.AI.NavMeshAgent nav;
//         public Vector3 Target;
//
//     // Start is called before the first frame update
//     void Start(){
//         nav = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
//     }
//
//     // Update is called once per frame
//     void Update(){
//             timer += Time.deltaTime;
//
//             if (timer >= newTarget){
//                     setNewTarget();
//                     timer = 0;
//             }
//     }
//
//     void setNewTarget(){
//             float myX = gameObject.transform.position.x;
//             float myZ = gameObject.transform.position.z;
//
//             float xPos = myX + Random.Range(myX - 100, myX + 100);
//             float zPos = myZ + Random.Range(myZ - 100, myZ + 100);
//
//             Target = new Vector3(xPos, gameObject.transform.position.y,zPos);
//
//             nav.SetDestination(Target);
//     }
// }
