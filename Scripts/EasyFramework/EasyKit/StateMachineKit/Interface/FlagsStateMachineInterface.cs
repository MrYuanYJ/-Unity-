using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace EasyFramework.StateMachineKit
{
    public interface IFlagsSMachine<TEnum, TState> : ISMachine<int, TState> where TEnum : Enum where TState : IState
    {
        bool ISMachine<int, TState>.BeforeAdd(int key) => ((key - 1) & key) == 0;
        private static void RemoveCurrentUpdateEvent(IFlagsSMachine<TEnum, TState> self,int key)
        {
            if (key!=0)
            {
                if (self.States[key] is IStateUpdate update)
                    self.MUpdateEvent.UnRegister(update.Update);
                if (self.States[key] is IStateFixedUpdate fixedUpdate)
                    self.MFixedUpdateEvent.UnRegister(fixedUpdate.FixedUpdate);
            }
        }
        private static void AddCurrentUpdateEvent(IFlagsSMachine<TEnum, TState> self,int key)
        {
            if (key!=0)
            {
                if (self.States[key] is IStateUpdate update)
                    self.MUpdateEvent.Register(update.Update);
                if (self.States[key] is IStateFixedUpdate fixedUpdate)
                    self.MFixedUpdateEvent.Register(fixedUpdate.FixedUpdate);
            }
        }
        public static bool SingleEnter(IFlagsSMachine<TEnum, TState> self,int key)
        {
            if (!self.IsPause 
                && self.States.TryGetValue(key,out var state) 
                &&((key - 1) & key) == 0
                && (self.CurrentState & key) == 0)
            {
                if (!state.EnterCondition(self)) return false;
                
                self.CurrentState |= key;
                AddCurrentUpdateEvent(self,key);
                return true;
            }

            return false;
        }
        public static bool SingleExit(IFlagsSMachine<TEnum, TState> self,int key)
        {
            if (!self.IsPause 
                && self.States.TryGetValue(key,out var state) 
                &&((key - 1) & key) == 0
                && (self.CurrentState & key) == 0)
            {
                if (!state.ExitCondition(self)) return false;
                
                self.CurrentState &= ~key;
                RemoveCurrentUpdateEvent(self,key);
                return true;
            }

            return false;
        }
    }

    public interface IFlagsStateMachine<TEnum, TState> : IFlagsSMachine<TEnum, TState> where TEnum : Enum where TState : IEasyState
    {
    }

    public static class FlagsStateMachineExtension
    {
        public static bool EnterSingleState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key,
            params object[] data) where TEnum : Enum where TState : IEasyState
        {
            if (IFlagsSMachine<TEnum, TState>.SingleEnter(self,key.GetHashCode()))
            {
                self.States[key.GetHashCode()].Enter(self,data);
                return true;
            }

            return false;
        }
        public static bool ExitSingleState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key,
            params object[] data) where TEnum : Enum where TState : IEasyState
        {
            if (IFlagsSMachine<TEnum, TState>.SingleExit(self,key.GetHashCode()))
            {
                self.States[key.GetHashCode()].Exit(self,data);
                return true;
            }

            return false;
        }
        public static bool ChangeState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key,
            params object[] data) where TEnum : Enum where TState : IEasyState
        {
            bool success = true;
            if (!self.IsPause && key.GetHashCode() != self.CurrentState)
            {
                var add = key.GetHashCode() & (~self.CurrentState);
                var remove = self.CurrentState & (~key.GetHashCode());
                var i = 0;
                while (add != 0 && i < 32)
                {
                    if ((add & (1 << i)) != 0)
                    {
                        success = success && EnterSingleState(self, (TEnum)(object)(1 << i), data);
                        add -= 1 << i;
                    }

                    i++;
                }

                i = 0;
                while (remove != 0 && i < 32)
                {
                    if ((remove & (1 << i)) != 0)
                    {
                        success = success && ExitSingleState(self, (TEnum)(object)(1 << i), data);
                        remove -= 1 << i;
                    }

                    i++;
                }

                return success;
            }

            return false;
        }
        public static bool ReplaceSingleState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key, TState state,
            params object[] data) where TEnum : Enum where TState : IEasyState
        {
            var oldState = ISMachine<int, TState>.StateReplace(self, key.GetHashCode(), state);
            if (oldState != null)
            {
                oldState.Exit(self, data);
                state.Enter(self, data);
                return true;
            }

            return false;
        }
        public static bool ExitCurrentState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self,
            params object[] data) where TEnum : Enum where TState : IEasyState
        {
            return ChangeState(self, default(TEnum), data);
        }
    }

    public interface IFlagsStateMachine<TEnum, TState, TValue> : IFlagsSMachine<TEnum, TState> where TEnum : Enum where TState : IEasyState<TValue>
    {
    }
    public static class FlagsStateMachineValueExtension
    {
        public static bool EnterSingleState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TValue t,
            params object[] data) where TEnum : Enum where TState : IEasyState<TValue>
        {
            if (IFlagsSMachine<TEnum, TState>.SingleEnter(self,key.GetHashCode()))
            {
                self.States[key.GetHashCode()].Enter(self, t, data);
                return true;
            }

            return false;
        }
        public static bool ExitSingleState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TValue t,
            params object[] data) where TEnum : Enum where TState : IEasyState<TValue>
        {
            if (IFlagsSMachine<TEnum, TState>.SingleExit(self,key.GetHashCode()))
            {
                self.States[key.GetHashCode()].Exit(self, t, data);
                return true;
            }

            return false;
        }
        public static bool ChangeState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TValue t,
            params object[] data) where TEnum : Enum where TState : IEasyState<TValue>
        {
            bool success = true;
            if (!self.IsPause && key.GetHashCode() != self.CurrentState)
            {
                var add = key.GetHashCode() & (~self.CurrentState);
                var remove = self.CurrentState & (~key.GetHashCode());
                var i = 0;
                while (add != 0 && i < 32)
                {
                    if ((add & (1 << i)) != 0)
                    {
                        success = success && EnterSingleState(self, (TEnum)(object)(1 << i), t, data);
                        add -= 1 << i;
                    }

                    i++;
                }

                i = 0;
                while (remove != 0 && i < 32)
                {
                    if ((remove & (1 << i)) != 0)
                    {
                        success = success && ExitSingleState(self, (TEnum)(object)(1 << i), t, data);
                        remove -= 1 << i;
                    }

                    i++;
                }

                return success;
            }

            return false;
        }
        public static bool ReplaceSingleState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TState state, TValue t,
            params object[] data) where TEnum : Enum where TState : IEasyState<TValue>
        {
            var oldState = ISMachine<int, TState>.StateReplace(self, key.GetHashCode(), state);
            if (oldState != null)
            {
                oldState.Exit(self, t, data);
                state.Enter(self, t, data);
                return true;
            }

            return false;
        }
        public static bool ExitCurrentState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TValue t,
            params object[] data) where TEnum : Enum where TState : IEasyState<TValue>
        {
            return ChangeState(self, default(TEnum), t, data);
        }
    }
}