using System.Collections.Generic;
using EXFunctionKit;

namespace EasyFramework
{
    public struct CollisionListenerGroupData
    {
        public ColliderBaseData colliderBaseData;
        public HashSet<CollisionBoxListener> listeners;

        public CollisionListenerGroupData(ColliderBaseData colliderBaseData)
        {
            this.colliderBaseData = colliderBaseData;
            listeners = new();
        }
    }
    public class HitBoxEntity: AMonoEntity<HitBox>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        
        private readonly Dictionary<string,CollisionListenerGroupData> _collisionListenerGroupDic = new();
        private Dictionary<string, CollisionInfo> _collision2DInfoDic = new();
        KeyEvent<string> collisionEvent = new ();
        
        protected override void OnActive()
        {
            _collisionListenerGroupDic.Clear();
            foreach (var colliderData in Mono.colliderData)
            {
                foreach (var data in colliderData.colliderDataLst)
                {
                    var listener=data.collider.Component<CollisionBoxListener>();
                    listener.Init(colliderData.colliderBaseData, data, (IUnitEntity)Root);
                    _collisionListenerGroupDic[colliderData.colliderBaseData.groupName] =
                        new CollisionListenerGroupData(colliderData.colliderBaseData);
                    _collisionListenerGroupDic[colliderData.colliderBaseData.groupName].listeners.Add(listener);
                    listener.OnEnter.Register(OnEnter);
                    listener.OnExit.Register(OnExit);
                }
            }
            CollisionFunc.GetCollisionInfo.RegisterFunc((IGetEasyFuncDic) Root, _collision2DInfoDic.GetValueOrDefault);
            CollisionFunc.GetColliderBaseData.RegisterFunc((IGetEasyFuncDic) Root, GetColliderBaseData);
        }
        protected override void OnUnActive()
        {
            foreach (var group in _collisionListenerGroupDic.Values)
            {
                foreach (var listener in group.listeners)
                {
                    listener.OnEnter.UnRegister(OnEnter);
                    listener.OnExit.UnRegister(OnExit);
                }
            }
            _collisionListenerGroupDic.Clear();
            CollisionFunc.GetCollisionInfo.UnRegisterFunc((IGetEasyFuncDic)Root,_collision2DInfoDic.GetValueOrDefault);
            CollisionFunc.GetColliderBaseData.UnRegisterFunc((IGetEasyFuncDic) Root, GetColliderBaseData);
        }

        public void ModifyColliders(ColliderGroupData colliderData)
        {
            if (!_collisionListenerGroupDic.TryGetValue(colliderData.colliderBaseData.groupName, out var groupData))
            {
                _collisionListenerGroupDic[colliderData.colliderBaseData.groupName] =
                    new CollisionListenerGroupData(colliderData.colliderBaseData);
            }
            foreach (var data in colliderData.colliderDataLst)
            {
                if (!data.collider.gameObject.TryGetComponent(out CollisionBoxListener listener))
                {
                    listener= data.collider.Component<CollisionBoxListener>();
                    listener.OnEnter.Register(OnEnter);
                    listener.OnExit.Register(OnExit);
                }
                listener.Init(colliderData.colliderBaseData, data, (IUnitEntity)Root);
                
                _collisionListenerGroupDic[colliderData.colliderBaseData.groupName].listeners.Add(listener);
            }
        }
        public void RemoveColliders(ColliderGroupData colliderData)
        {
            foreach (var data in colliderData.colliderDataLst)
            {
                if (data.collider.gameObject.TryGetComponent(out CollisionBoxListener listener))
                {
                    listener.OnEnter.UnRegister(OnEnter);
                    listener.OnExit.UnRegister(OnExit);
                    _collisionListenerGroupDic[colliderData.colliderBaseData.groupName].listeners.Remove(listener);
                }
            }
            if(_collisionListenerGroupDic[colliderData.colliderBaseData.groupName].listeners.Count == 0)
            {
                _collisionListenerGroupDic.Remove(colliderData.colliderBaseData.groupName);
            }
        }

        public void OpenColliders(string group)
        {
            if (_collisionListenerGroupDic.TryGetValue(group, out var groupData))
            {
                foreach (var listener in groupData.listeners)
                {
                    if (listener.data.collider)
                        listener.data.collider.enabled = true;
                    if (listener.data.collider2D)
                        listener.data.collider2D.enabled = true;
                }
                CollisionEvent.OnOpenCollider.InvokeEvent(group);
            }
        }
        public void CloseColliders(string group)
        {
            if (_collisionListenerGroupDic.TryGetValue(group, out var groupData))
            {
                foreach (var listener in groupData.listeners)
                {
                    if (listener.data.collider)
                        listener.data.collider.enabled = false;
                    if (listener.data.collider2D)
                        listener.data.collider2D.enabled = false;
                }
                CollisionEvent.OnCloseCollider.InvokeEvent(group);
            }
        }


        private ColliderBaseData GetColliderBaseData(string groupName)
        {
            return _collisionListenerGroupDic.TryGetValue(groupName, out var colliderData) ? colliderData.colliderBaseData : default;
        }

        private CollisionInfo GetCollision2DInfo(string groupName)
        {
            return _collision2DInfoDic.GetValueOrDefault(groupName);
        }

        private bool CollisionCondition(CollisionBoxListener listener, CollisionBoxListener other)
        {
            var selfUnit = UnitFunc.GetBelongUnit.InvokeFunc((IGetEasyFuncDic) listener.belongEntity);
            var otherUnit = UnitFunc.GetBelongUnit.InvokeFunc((IGetEasyFuncDic) other.belongEntity);
            var relationship = UnitFunc.GetUnitEntityRelationship(selfUnit, otherUnit);
            if (listener.baseData.checkRelationship.HasFlag(relationship)
                && other.baseData.checkRelationship.HasFlag(relationship))
            {
                return true;
            }
            return false;
        }

        private void OnEnter(CollisionBoxListener listener, CollisionBoxListener other)
        {
            if(!CollisionCondition(listener, other))
                return;
            if (!_collision2DInfoDic.TryGetValue(listener.baseData.groupName, out var collision2DInfo))
            {
                collision2DInfo= new CollisionInfo()
                {
                    OtherCollision2DBoxListeners = new() {[other.belongEntity]=new(){other}},
                    SelfCollision2DBoxListeners = new() {listener}
                };
                _collision2DInfoDic[listener.baseData.groupName] = collision2DInfo;
                collisionEvent.Invoke(listener.baseData.groupName);
                CollisionEvent.CollisionOccurs.InvokeEvent((IGetEasyEventDic) Root, listener.baseData.groupName);
            }

            collision2DInfo.AddOtherCollision2DBoxListener(other);
            collision2DInfo.AddSelfCollision2DBoxListener(listener);
        }

        private void OnExit(CollisionBoxListener listener, CollisionBoxListener other)
        {
            if(!CollisionCondition(listener, other))
                return;
            if (_collision2DInfoDic.TryGetValue(listener.baseData.groupName, out var collision2DInfo))
            {
                collision2DInfo.RemoveOtherCollision2DBoxListener(other);
                collision2DInfo.RemoveSelfCollision2DBoxListener(listener);

                if (collision2DInfo.SelfCollision2DBoxListeners.Count == 0)
                {
                    _collision2DInfoDic.Remove(listener.baseData.groupName);
                    CollisionEvent.CollisionEnd.InvokeEvent((IGetEasyEventDic) Root, listener.baseData.groupName);
                }
            }
        }
    }

    public struct CollisionEvent
    {
        public sealed class OnOpenCollider: AEventIndex<OnOpenCollider,string>{}
        public sealed class OnCloseCollider: AEventIndex<OnCloseCollider,string>{}
        public sealed class CollisionOccurs : AEventIndex<CollisionOccurs, string> { }
        public sealed class CollisionEnd : AEventIndex<CollisionEnd, string> { }
    }
    public struct CollisionFunc
    {
        public sealed class GetCollisionInfo : AFuncIndex<GetCollisionInfo, string, CollisionInfo> { }
        public sealed class GetColliderBaseData : AFuncIndex<GetColliderBaseData, string, ColliderBaseData> { }
    }
}