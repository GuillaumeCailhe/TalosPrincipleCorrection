using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, Interactive
{
    public string objectName = "Object";
    public GameObject ghostObject;

    public string getInteractionMessage()
    {
        return "Pick " + objectName;
    }

    public GameObject getGhostObject()
    {
        return null;
    }

    /*
        Désactive l'objet quand il est ramassé par le joueur.
        On ne le supprime pas pour conserver une trace de l'objet et le téléporter plus tard.
    */
    public void interactWith()
    {
        /* ToDo :
            Désactiver les collisions et le rendu du mesh.
        */
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
