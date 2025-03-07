using UnityEngine;

public class Dialogue : Interraction
{
    public override void Interract()
    {
        // Affiche le nom de l'objet dans la console
        Debug.Log($"Nom de l'objet : {gameObject.name}");
    }
}
