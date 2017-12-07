/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-02
    * 描述： 资源加载管理器
    * 功能：在  Resources类的基础上，增加缓存功能
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;

namespace Unitech.Net.UnityFramework
{
    public class ResourcesMgr : MonoBehaviour
    {

        private static ResourcesMgr _Instance;
        private Hashtable ht = default(Hashtable);

        public static ResourcesMgr Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GameObject("ResourcesMgr").AddComponent<ResourcesMgr>();
                }
                return _Instance;
            }
        }

        void Awake()
        {
            ht = new Hashtable();
        }

        public T LoadResource<T>(string path, bool isCatch) where T : UnityEngine.Object
        {
            if (ht.Contains(path))
            {
                return ht[path] as T;
            }

            T TResource = Resources.Load<T>(path);
            if (TResource == null)
            {
                Debug.LogError(GetType() + "提取资源找不到，Path=" + path);
            }
            else if (isCatch)
            {
                ht.Add(path, TResource);
            }
            return TResource;
        }

        public GameObject LoadAsset(string path, bool isCatch)
        {
            GameObject goObj = LoadResource<GameObject>(path, isCatch);
            GameObject goObjClone = Instantiate<GameObject>(goObj);
            if (goObjClone == null)
            {
                Debug.LogError("LoadAsset克隆资源不成功，Path=" + path);
            }
            return goObjClone;
        }
    }
}
