using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOutsideOfMask : MonoBehaviour
{
    [SerializeField] SpriteMask spriteMask; // Reference to the SpriteMask component
    [SerializeField] PolygonCollider2D collider1, collider2;

    //! detect collision between spike and mask, disable when not in mask, enable when in mask

}
