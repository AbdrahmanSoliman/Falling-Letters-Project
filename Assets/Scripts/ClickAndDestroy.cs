using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickAndDestroy : MonoBehaviour
{
    void Update()
    {
        if(GameManager.isGamePlayable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    BoxCollider bc = hit.collider as BoxCollider;
                    if (bc != null)
                    {
                        GameObject child1 = bc.gameObject.transform.GetChild(0).gameObject; // getting a child gameobject (Canvas) of parent gameobject Letter Cube
                        GameObject childOfChild1 = child1.transform.GetChild(0).gameObject; // getting a child gameobject (Text) of parent gameobject (Canvas)
                        string destroyedLetter = childOfChild1.GetComponent<TextMeshProUGUI>().text;

                        WordSelector.Instance.CurrentLetter(1, destroyedLetter);

                        //bc.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                        
                        Explode(bc, childOfChild1); // in order to disable Text(childOfChild1) and Sprite Renderer of cube from its box collider(bc)
                        // then get the position of gameobject from its bc and set it to explosionParticle to play it

                        
                        Destroy(bc.gameObject,0.5f);
                        Score.Instance.UpdateScore();
                    }
                }
            }
        }
    }

    void Explode(BoxCollider bc, GameObject child) // I took child gameobject (Text) to disable it with cube sprite renderer in the same place(Explode function), I could disable it in Update().. but this looks simplier
    {
        child.gameObject.SetActive(false);
        bc.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        ParticleSystem explosionParticle = bc.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        //explosionParticle.transform.position = bc.gameObject.transform.position;
        explosionParticle.Play();
    }
}
