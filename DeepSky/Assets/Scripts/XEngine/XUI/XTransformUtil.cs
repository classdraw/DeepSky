using UnityEngine;
using DG.Tweening;
using System.Reflection;

namespace XEngine.UI
{

    public class XTransformUtil
    {
        //static private XTransformUtil _Instance;
        //private void Awake()
        //{
        //    _Instance = this;
        //}
        //void Update()
        //{
        //    for (int i = 0; i < _list.Size; i++)
        //    {
        //        FollowInfo followPos = (FollowInfo)_list.GetItemAt(i);
        //        if (followPos.target1 != null && followPos.target2 != null)
        //        {
        //            Vector3 screenPos = followPos.camera2.WorldToScreenPoint(followPos.target2.position + followPos.offset);
        //            Vector3 worldPos = followPos.camera1.ScreenToWorldPoint(screenPos);
        //            followPos.target1.position = worldPos;
        //        }
        //    }
        //}

        //private XDataList _list = new XDataList();

        //private void _AddFollow(Transform t1, Transform t2, Vector3 offset, Camera c1, Camera c2)
        //{
        //    FollowInfo followInfo = new FollowInfo(t1, t2, offset, c1, c2);

        //    bool finded = false;
        //    for (int i = _list.Size - 1; i >= 0; i--)
        //    {
        //        FollowInfo followPos = (FollowInfo)_list.GetItemAt(i);
        //        if (followPos.target1 == t1)
        //        {
        //            _list.SetItemAt(i, followPos);
        //            finded = true;
        //            break;
        //        }
        //    }
        //    if (!finded)
        //        _list.Add(followInfo);
        //}
        //private void _RemoveFollow(Transform t1)
        //{
        //    for (int i = _list.Size - 1; i >= 0; i--)
        //    {
        //        FollowInfo followPos = (FollowInfo)_list.GetItemAt(i);
        //        if (followPos.target1 == t1)
        //        {
        //            _list.RemoveAt(i);
        //        }
        //    }
        //}
        //private void _RemoveAllFollow()
        //{
        //    _list.Clear();
        //}
        //class FollowInfo
        //{
        //    public FollowInfo(Transform t1, Transform t2, Vector3 offset, Camera c1, Camera c2)
        //    {
        //        this.target1 = t1;
        //        this.target2 = t2;
        //        this.camera1 = c1;
        //        this.camera2 = c2;
        //        this.offset = offset;
        //    }
        //    public Transform target1;
        //    public Transform target2;
        //    public Camera camera1;
        //    public Camera camera2;
        //    public Vector3 offset;
        //}


        //static public void AddFollow(Transform t1, Transform t2, Vector3 offset, Camera c1, Camera c2)
        //{
        //    _Instance._AddFollow(t1, t2, offset, c1, c2);
        //}
        //static public void RemoveFollow(Transform t1)
        //{
        //    _Instance._RemoveFollow(t1);
        //}
        //static public void RemoveAllFollow()
        //{
        //    _Instance._RemoveAllFollow();
        //}

		static public void SetActiveByScale(GameObject go, bool value)
		{
			if (go.activeSelf == false) {
				go.SetActive (true);
			}
			if (value == true) {
				go.transform.localScale = Vector3.one;
				XTransformUtil.EanbleAnimation (go, true);
			} else {
				go.transform.localScale = Vector3.zero;
				XTransformUtil.EanbleAnimation (go, false);
			}
		}
		static public void SetActiveByScale(Component com, bool value)
		{
			if (com.gameObject.activeSelf == false) {
				com.gameObject.SetActive (true);
			}
			if (value == true) {
				com.gameObject.transform.localScale = Vector3.one;
				XTransformUtil.EanbleAnimation (com.gameObject, true);
			} else {
				com.gameObject.transform.localScale = Vector3.zero;
				XTransformUtil.EanbleAnimation (com.gameObject, false);
			}
		}
		static public void EanbleAnimation(GameObject obj, bool enable)
		{
			Animation animation = obj.GetComponent<Animation> ();
			if (animation == null) {
				return;
			}
			animation.enabled = enable;
		}
        static public void SetActive(GameObject go, bool value)
        {
            go.SetActive(value);
        }
        static public void SetActive(Component com, bool value)
        {
            com.gameObject.SetActive(value);
        }
        static public bool GetActiveSelf(GameObject go)
        {
            return go.activeSelf;
        }
        static public bool GetActiveSelf(Component go)
        {
            return go.gameObject.activeSelf;
        }
        static public bool GetActiveInHierarchy(GameObject go)
        {
            return go.activeInHierarchy;
        }
        static public bool GetActiveInHierarchy(Component go)
        {
            return go.gameObject.activeInHierarchy;
        }
        static public GameObject GetGameObject(Component c)
        {
            return c.gameObject;
        }
        static public Transform GetTransform(Object self)
        {
            Transform trans = null;
            if (self is Transform)
            {
                trans = (Transform)self;
            }
            else if (self is GameObject)
            {
                trans = ((GameObject)self).transform;
            }
            else if (self is Component)
            {
                trans = ((Component)self).transform;
            }
            return trans;
        }
        static public Transform FindTransform(string path, Object trans = null)
        {
            Transform find = null;
            if (trans == null)
            {
                GameObject go = GameObject.Find(path);
                if (go != null)
                {
                    find = go.transform;
                }
            }
            else if (trans is Transform)
            {
                find = ((Transform)trans).Find(path);
            }
            else if (trans is GameObject)
            {
                find = ((GameObject)trans).transform.Find(path);
            }
            else if (trans is MonoBehaviour)
            {
                find = ((MonoBehaviour)trans).transform.Find(path);
            }
            return find;
        }
        static public void RemoveAllChildren(Transform container)
        {
            for (int i = container.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(container.GetChild(i).gameObject);
            }
        }
        static public void SetParent(Transform trans, Transform parent, bool keepPrefabInfo = false)
        {
            if (keepPrefabInfo)
            {
                trans.SetParent(parent, false);
            }
            else
            {
                trans.SetParent(parent);
                trans.localRotation = Quaternion.identity;
                trans.localScale = Vector3.one;
                if (trans is RectTransform)
                    ((RectTransform)trans).anchoredPosition = Vector2.zero;
                else
                    trans.localPosition = Vector3.zero;
            }

        }
        static public Transform GetParent(Transform trans)
        {
            return trans.parent;
        }
        static public void SetLocalPosition(Transform self, Vector3 pos)
        {
            self.localPosition = pos;
        }
        static public Vector3 GetLocalPosition(Transform self)
        {
            return self.localPosition;
        }
        static public void SetLocalRotation(Transform trans, Quaternion qua)
        {
            trans.localRotation = qua;
        }
        static public Quaternion GetLocalRotation(Transform trans)
        {
            return trans.localRotation;
        }
        static public void SetRotation(Transform trans, Quaternion qua)
        {
            trans.rotation = qua;
        }
        static public Quaternion GetRotation(Transform trans)
        {
            return trans.rotation;
        }
        static public void SetPosition(Transform trans, Vector3 pos)
        {
            trans.position = pos;
        }
        static public Vector3 GetPosition(Transform trans)
        {
            return trans.position;
        }
        static public void SetAnchoredPosition(Transform trans, Vector2 pos)
        {
            ((RectTransform)trans).anchoredPosition = pos;
        }
        static public void SetSizeDelta(Transform trans, Vector2 sizeDelta)
        {
            ((RectTransform)trans).sizeDelta = sizeDelta;
        }
        static public void SetAnchorMax(Transform trans, Vector2 max)
        {
            ((RectTransform)trans).anchorMax = max;
        }
        static public void SetAnchorMin(Transform trans, Vector2 min)
        {
            ((RectTransform)trans).anchorMin = min;
        }
        static public void SetPivot(Transform trans, Vector2 pivot)
        {
            ((RectTransform)trans).pivot = pivot;
        }
        static public Vector2 GetAnchoredPosition(Transform trans)
        {
            return ((RectTransform)trans).anchoredPosition;
        }

        static public void SetOffsetMin(Transform trans,Vector2 offset){
            ((RectTransform)trans).offsetMin = offset;
        }
        static public void SetOffsetMax(Transform trans,Vector2 offset){
            ((RectTransform)trans).offsetMax = offset;
        }
        static public void SetLocalScale(Transform trans, Vector3 value)
        {
            trans.localScale = value;
        }
        static public Vector3 GetLocalScale(Transform trans, Vector3 value)
        {
            return trans.localScale;
        }
        static public void SetAlpha(Transform trans, float alpha)
        {
            CanvasGroup group = trans.GetComponent<CanvasGroup>();
            group.alpha = alpha;
        }
        static public float GetAlpha(Transform trans)
        {
            CanvasGroup group = trans.GetComponent<CanvasGroup>();
            return group.alpha;
        }
        static public void SetAsLastSibling(Transform trans)
        {
            trans.SetAsLastSibling();
        }
        static public void SetAsFirstSibling(Transform trans)
        {
            trans.SetAsFirstSibling();
        }
        static public Transform Clone(Object obj)
        {
            GameObject go = null;
            if (obj is GameObject)
            {
                go = (GameObject)GameObject.Instantiate(obj);
            }
            else if (obj is Component)
            {
                go = (GameObject)GameObject.Instantiate(((Component)obj).gameObject);
            }
            else
            {
                XLogger.LogError("Clone Error: Unknown obj Type:" + obj);
            }
            return go != null ? go.transform : null;
        }
        static public Transform CloneMax(Object obj, Vector3 worldPos, Transform parent)
        {
            GameObject template = null;
            if (obj is GameObject)
            {
                template = (GameObject)obj;
            }
            else if (obj is Component)
            {
                template = (GameObject)((Component)obj).gameObject;
            }
            else
            {
                XLogger.LogError("Clone Error: Unknown obj Type:" + obj);
            }
            GameObject go = GameObject.Instantiate(template, worldPos, Quaternion.identity, parent);
            go.transform.localRotation = Quaternion.identity;
            return go.transform;
        }
        static public void DestroyGameObject(Object obj)
        {
            if (obj == null)
            {
                XLogger.LogWarn("Object TO Destroy Is Already NULL");
                return;
            }
            XLogger.LogTest("DestroyGameObject:" + obj.name);
            if (obj is GameObject)
            {
                GameObject.Destroy(obj);
            }
            else if (obj is Component)
            {
                XLogger.LogWarn("Destory Component will Destroy GameObject:" + obj);
                GameObject.Destroy(((Component)obj).gameObject);
            }
            else
            {
                XLogger.LogError("Destory Error: Unknown obj Type:" + obj);
            }
        }
        static public Component AddComponent(Transform trans, string behavior)
        {
            System.Type type = System.Type.GetType(behavior);
            GameObject go = trans.gameObject;
            Component component = go.GetComponent(type);
            if (component == null)
            {
                component = go.AddComponent(type);
            }
            return component;
        }
        static public Component GetComponent(Transform trans, string behavior)
        {
            System.Type type = System.Type.GetType(behavior);
            if (type == null)
            {
                Assembly unityAssembly = Assembly.GetAssembly(trans.GetType());
                type = unityAssembly.GetType(behavior);
            }
            GameObject go = trans.gameObject;
            Component component = go.GetComponent(type);
            return component;
        }
        static public Vector3 WorldToScreenPoint(Vector3 pos, Camera camera)
        {
            return camera.WorldToScreenPoint(pos);
        }
        static public Vector3 ScreenToWorldPoint(Vector3 pos, Camera camera)
        {
            return camera.ScreenToWorldPoint(pos);
        }

        static public Vector4 GetTransformBounds(Transform trans)
        {
            BoxCollider box = trans.GetComponent<BoxCollider>();

            float radiusX = box.size.x / 2;
            float radiusZ = box.size.z / 2;
            float minX = box.center.x - radiusX;
            float maxX = box.center.x + radiusX;
            float minZ = box.center.z - radiusZ;
            float maxZ = box.center.z + radiusZ;
            return new Vector4(minX, maxX, minZ, maxZ);
        }

        static public void LookAt(Transform trans, Vector3 worldPos)
        {
            trans.LookAt(worldPos);
        }
        static public void LookAt(Transform trans, Transform target)
        {
            trans.LookAt(target);
        }
        static public Transform CreateGameObject(string name)
        {
            GameObject go = new GameObject(name);
            return go.transform;
        }
        static public float PlayAnimation(Transform trans, string clipName = "")
        {
            Animation ani = trans.GetComponent<Animation>();
            if (!ani.enabled) ani.enabled = true;
            ani.Play(clipName);
            float clipTime = ani.GetClip(clipName).length;
            return clipTime;
        }
        static public void StopAnimation(Transform trans)
        {
            Animation ani = trans.GetComponent<Animation>();
            if (ani.enabled) ani.enabled = false;
        }


        static 	public void SetGameObjectLayer(GameObject obj,string layerName){
            obj.SetLayerRecursively(LayerMask.NameToLayer(layerName));
        }

        static public Transform GetAncestorTransform(Transform trans)
        {
            Transform current = trans;
            Transform parent = current.parent;

            while(parent != null)
            {
                current = parent;
                parent = current.parent;
            }
            return current;
        }

    }
}
