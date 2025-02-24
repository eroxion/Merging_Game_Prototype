using UnityEngine;

public class MergingBehaviour : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Player") && this.gameObject.GetComponent<PlayerController>()?.IsDragging() == true) {
            GameObject collidingObject1 = other.gameObject;
            GameObject collidingObject2 = this.gameObject;

            GameObject draggedObject = GetDraggedObject(collidingObject1, collidingObject2);
            GameObject stationaryObject = (draggedObject == collidingObject1) ? collidingObject2 : collidingObject1;
            stationaryObject.transform.localScale = stationaryObject.transform.localScale * 1.2f;
            SpriteRenderer childSpriteRenderer = stationaryObject.GetComponentInChildren<SpriteRenderer>();
            if (childSpriteRenderer != null) {
                Color childColor = childSpriteRenderer.color;
                childColor.a = 0.2f;
                childSpriteRenderer.color = childColor;
            }
            draggedObject.GetComponent<PlayerController>().SendNewBlockInfo(stationaryObject.transform, collidingObject1, collidingObject2);
            draggedObject.GetComponent<PlayerController>().SetOverColliderTrue();
            
            
        }  
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Player") && this.gameObject.GetComponent<PlayerController>()?.IsDragging() == true) {
            GameObject collidingObject1 = other.gameObject;
            GameObject collidingObject2 = this.gameObject;

            GameObject draggedObject = GetDraggedObject(collidingObject1, collidingObject2);
            GameObject stationaryObject = (draggedObject == collidingObject1) ? collidingObject2 : collidingObject1;
            stationaryObject.transform.localScale = stationaryObject.transform.localScale / 1.2f;
            SpriteRenderer childSpriteRenderer = stationaryObject.GetComponentInChildren<SpriteRenderer>();
            if (childSpriteRenderer != null) {
                Color childColor = childSpriteRenderer.color;
                childColor.a = 1f;
                childSpriteRenderer.color = childColor;
            }
            draggedObject.GetComponent<PlayerController>()._resetNewBlockInfo();


        }
        
    }

    private GameObject GetDraggedObject(GameObject obj1, GameObject obj2) {
        if (obj1.GetComponent<PlayerController>()?.IsDragging() == true) {
            return obj1;
        }
        if (obj2.GetComponent<PlayerController>()?.IsDragging() == true) {
            return obj2;
        }
        return null;
    }
}
