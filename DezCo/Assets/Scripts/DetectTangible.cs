using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TE;
using IsoTools;

public class DetectTangible : MonoBehaviour, IOnTangibleAdded, IOnTangibleUpdated, IOnTangibleRemoved
    {
        public RectTransform CanvasRForm;
        public Transform ChildRoot;
        public GameObject TangiblePrefab;

        private Queue<TangibleData> _pool = new Queue<TangibleData>();
        private Dictionary<int, TangibleData> _tangibleMap = new Dictionary<int, TangibleData>();
        private Vector3 _offset;
        void Start()
        {
            var r = CanvasRForm.rect;
            _offset = new Vector3(r.width, r.height) * -0.5f;
            TangibleEngine.Subscribe(this);
            Debug.Log("TangibleScript started");
            TangibleEngine.Subscribe(this);
        }

    private TangibleData GetTangibleWithId(int id)
    {
        TangibleData e;
        if (!_tangibleMap.TryGetValue(id, out e))
        {
            if (_pool.Count <= 0)
            {
                var o = Instantiate(TangiblePrefab, ChildRoot, false);
                o.GetComponent<SpawnTangibleCollider>().puckIndex = id;
                e = new TangibleData(o);
                _tangibleMap[id] = e;
                return e;
            }
            else
            {
                e = _pool.Dequeue();
                _tangibleMap[id] = e;
            }
        }
        e.DoShow();
        return e;
    }

    private void ReturnTangible(int id)
    {
        TangibleData e;
        if (_tangibleMap.TryGetValue(id, out e))
        {
            _tangibleMap.Remove(id);
            e.DoHide();
            _pool.Enqueue(e);
        }
    }

        public void OnTangibleAdded(Tangible t)
        {
            TangibleData tangible = GetTangibleWithId(t.Id);
            
            tangible.Update(t, _offset);
        }

        public void OnTangibleUpdated(Tangible t)
        {
            //Debug.Log("Tangible Updated: " + t.Id);
            TangibleData tangible = GetTangibleWithId(t.Id);
            tangible.Update(t, _offset);
            //Debug.Log(tangible.Transform.position);
        }

        public void OnTangibleRemoved(Tangible t)
        {
            Debug.Log("Tangible Removed: " + t.Id);
        TangibleData tangible = GetTangibleWithId(t.Id);
        Destroy(tangible.GameObject);
        //ReturnTangible(t.Id);
        }

    public void OnEngineFailedToConnect(Tangible t)
    {
        Debug.Log("failed to connect");

    }
    }
