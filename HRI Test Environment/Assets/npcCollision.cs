using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class npcCollision : MonoBehaviour{

        public GameObject UIobject;
        //public GameObject canvas;

        void start(){
                UIobject.SetActive(false);
        }

        void OnTriggerEnter(Collider other){
                if(other.tag == "Player"){
                        UIobject.SetActive(true);
                }
        }

        void Update(){

        }

        void OnTriggerExit (Collider other){
                UIobject.SetActive(false);
                //Destroy(canvas);
        }
}
