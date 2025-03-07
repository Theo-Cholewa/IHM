using UnityEngine;

public class Action : Interraction
{
    
    public override void Interract()
    {
        // Déplace l'objet de 1 en x
        transform.position += new Vector3(1, 0, 0);
        Debug.Log($"{gameObject.name} s'est déplacé de 1 en x.");
    }
}
