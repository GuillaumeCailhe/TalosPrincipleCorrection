using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Variables d'interactions
    public float interactionDistance = 3f;
    public GameObject UI;
    private GameObject objectInteractingWith; // représente l'objet avec lequel on interagit

    // Variables pour gérer le déplacement d'objets
    private GameObject pickedObject; // l'objet tenu en main par le joueur
    private GameObject pickedObjectGhost; // prévisualisation de l'objet ramassé
    public float dropDistance = 3.5f;

    public GameObject heldCube;

    // Update is called once per frame
    void Update()
    {
        /*
            Gestion des interactions
        */

        // On cache le message d'interaction à chaque update et on l'affichera plus tard, si nécessaire.
        showInteractionMessage(false);
        
        // Gestion des interactions
        if(!pickedObject)
        {
                look_for_interaction(); // ToDo 1 dans cette fonction

            /* 
                ToDo 2 : Interaction avec l'objet
                    - Récupérer l'input "Interact" (voir Input Manager dans les Project Settings)
                        voir doc : https://docs.unity3d.com/ScriptReference/Input.html
                    - Interagir avec l'objet
                    - Gérer le ramassage des objets "Pickable"
                        - Récupérer l'objet "Ghost" (la prévisualisation) et le spawn
                        - Faire tenir au joueur le bon objet en main (déjà paramétré, il reste à activer la bonne visualisation)
                            - Pour cela, compléter la fonction setHeldObjectActivity()
                        - mettre à jour la variable pickedObject
            */
            if(Input.GetButtonDown("Interact"))
            {
                if(objectInteractingWith != null){
                    objectInteractingWith.GetComponent<Interactive>().interactWith();

                    Pickable pickableObject = objectInteractingWith.GetComponent<Pickable>();
                    if(pickableObject != null)
                    {
                        pickedObjectGhost = pickableObject.getGhostObject();
                        setHeldObjectActivity(true, pickableObject.objectName);
                    }
                }
            }
        }
        else{
            /*
            Poser les objets (les "Pickable")
            ToDo 3
                - Lancer un Raycast et vérifier qu'on touche le sol (il y a un tag Floor)
                - Téléporter l'objet ghost à la position du raycast
                - Si l'objet Ghost a des collisions avec d'autres objets, le cacher, sinon l'afficher.
                - Permettre au joueur de poser l'objet si la position est correcte.
                Note : l'objet Ghost a un Mesh Collider avec la case Trigger de cochée
            */
            if(pickedObject)
            {
                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                RaycastHit hit;

                if (Physics.Raycast(transform.position, fwd, out hit, dropDistance))
                {
                    
                }
            }
        }

        // On remet l'objet avec lequel on interragit à zéro à chaque update.
        // Note : En C# null représente un objet vide, qui n'a aucune valeur.
        objectInteractingWith = null;

    }

    // Cherche les objets avec lesquels on peut interragir.
    // Affiche un message sur l'UI quand il y en a un de détecté.
    void look_for_interaction()
    {
        /*
        ToDo 1 : 
            - Lancer un raycast : https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
            - Vérifier que l'objet touché implémente l'interface voulue : https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
            - Montrer l'UI avec le bon message (voir la fonction showPickObjectUI plus bas)
            - Mettre à jour la variable objectInteractingWith
        */
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, interactionDistance))
        {    
            Interactive interactiveObject = hit.collider.gameObject.GetComponent(typeof(Interactive)) as Interactive;

            if(interactiveObject  != null)
            {
                showInteractionMessage(true, interactiveObject.getInteractionMessage());
                objectInteractingWith = hit.collider.gameObject;
            }
        }
    }

    /*
        Affiche l'UI d'interaction avec un objet
        bool new_activity : affiche quand l'objet quand égal à true
        string new_text : le message à afficher (rien si non renseigné)
    */
    void showInteractionMessage(bool new_activity, string new_text = "")
    {
        TextMeshProUGUI textComponent = UI.GetComponent<TextMeshProUGUI>();
        textComponent.text = new_text;
        textComponent.enabled = new_activity;
    }

    /*
        (Dés)active l'objet tenu en main par le joueur
        bool activity : si on doit afficher ou non l'objet
        string objectName : nom de l'objet pour savoir quel objet afficher
    */
    void setHeldObjectActivity(bool activity, string objectName)
    {
        // ToDo : compléter cette fonction
        // Rappel : pensez à la structure switch !
        switch(objectName)
        {
            case "Cube":
                heldCube.SetActive(activity);
                break;
        }
    }
}
