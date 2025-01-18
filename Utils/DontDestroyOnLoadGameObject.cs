using UnityEngine;

namespace Pampero.Tools.Utils {

    public class DontDestroyOnLoadGameObject : MonoBehaviour {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
//EOF.