using System;

namespace EasyFramework
{
    public interface IFlagsSMachine<TEnum, TState> : ISMachine<int, TState> where TEnum : Enum where TState : IStateBase
    {
        bool ISMachine<int, TState>.BeforeAdd(int key, TState state)
        {
            if (((key - 1) & key) == 0)
            {
                return OnBeforeAdd(key, state) && state.BeforeAdd(this);
            }

            return false;
        }

        private static void RemoveCurrentUpdateEvent(IFlagsSMachine<TEnum, TState> self,int key)
        {
            if (key!=0)
            {
                if (self.States[key] is IStateUpdate update)
                    self.MUpdateAction-=update.Update;
                if (self.States[key] is IStateFixedUpdate fixedUpdate)
                    self.MFixedUpdateAction+=fixedUpdate.FixedUpdate;
            }
        }
        private static void AddCurrentUpdateEvent(IFlagsSMachine<TEnum, TState> self,int key)
        {
            if (key!=0)
            {
                if (self.States[key] is IStateUpdate update)
                    self.MUpdateAction+=update.Update;
                if (self.States[key] is IStateFixedUpdate fixedUpdate)
                    self.MFixedUpdateAction+=fixedUpdate.FixedUpdate;
            }
        }

        public static bool SingleEnter(IFlagsSMachine<TEnum, TState> self, int key)
        {
            if (((key - 1) & key) == 0
                && (self.CurrentState & key) == 0
                && self.BeforeStateChange(key, self.States[key]))
            {
                self.CurrentState |= key;
                AddCurrentUpdateEvent(self, key);
                self.StateChange();
                return true;
            }

            return false;
        }

        public static bool SingleExit(IFlagsSMachine<TEnum, TState> self, int key)
        {
            if (((key - 1) & key) == 0
                && (self.CurrentState & key) == 0
                && self.BeforeStateChange(key, self.States[key]))
            {
                self.CurrentState &= ~key;
                RemoveCurrentUpdateEvent(self, key);
                self.StateChange();
                return true;
            }

            return false;
        }
    }

    public interface IFlagsStateMachine<TEnum, TState> : IFlagsSMachine<TEnum, TState> where TEnum : Enum where TState : IState
    {
    }

    public static class FlagsStateMachineExtension
    {
        public static bool EnterSingleState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key) where TEnum : Enum where TState : IState
        {
            var keyHash = key.GetHashCode();
            if (!self.IsPause 
                && self.States.TryGetValue(keyHash,out var state) 
                && state.EnterCondition(self)
                && IFlagsSMachine<TEnum, TState>.SingleEnter(self,keyHash))
            {
                state.Enter(self);
                return true;
            }

            return false;
        }
        public static bool ExitSingleState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key) where TEnum : Enum where TState : IState
        {
            var keyHash = key.GetHashCode();
            if (!self.IsPause
                && self.States.TryGetValue(keyHash,out var state) 
                && state.ExitCondition(self)
                && IFlagsSMachine<TEnum, TState>.SingleExit(self,keyHash))
            {
                state.Exit(self);
                return true;
            }

            return false;
        }
        public static bool ChangeState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key) where TEnum : Enum where TState : IState
        {
            bool success = true;
            if (!self.IsPause
                && key.GetHashCode() != self.CurrentState)
            {
                var add = key.GetHashCode() & (~self.CurrentState);
                var remove = self.CurrentState & (~key.GetHashCode());
                var i = 0;
                while (add != 0 && i < 32)
                {
                    if ((add & (1 << i)) != 0)
                    {
                        success = success && EnterSingleState(self, (TEnum)(object)(1 << i));
                        add -= 1 << i;
                    }

                    i++;
                }

                i = 0;
                while (remove != 0 && i < 32)
                {
                    if ((remove & (1 << i)) != 0)
                    {
                        success = success && ExitSingleState(self, (TEnum)(object)(1 << i));
                        remove -= 1 << i;
                    }

                    i++;
                }

                return success;
            }

            return false;
        }
        public static bool ReplaceSingleState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self, TEnum key, TState state) where TEnum : Enum where TState : IState
        {
            if (!self.IsPause
                && ISMachine<int, TState>.StateReplace(self, key.GetHashCode(), state, out var oldState))
            {
                oldState.Exit(self);
                state.Enter(self);
                return true;
            }

            return false;
        }
        public static bool ExitCurrentState<TEnum, TState>(this IFlagsStateMachine<TEnum, TState> self) where TEnum : Enum where TState : IState
        {
            return ChangeState(self, default(TEnum));
        }
    }

    public interface IFlagsStateMachine<TEnum, TState, TValue> : IFlagsSMachine<TEnum, TState> where TEnum : Enum where TState : IState<TValue>
    {
    }
    public static class FlagsStateMachineValueExtension
    {
        public static bool EnterSingleState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TValue param) where TEnum : Enum where TState : IState<TValue>
        {
            var keyHash = key.GetHashCode();
            if (!self.IsPause 
                && self.States.TryGetValue(keyHash,out var state) 
                && state.EnterCondition(self,param)
                &&((keyHash - 1) & keyHash) == 0
                && (self.CurrentState & keyHash) == 0)
            {
                IFlagsSMachine<TEnum, TState>.SingleEnter(self,keyHash);
                state.Enter(self,param);
                return true;
            }

            return false;
        }
        public static bool ExitSingleState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TValue param) where TEnum : Enum where TState : IState<TValue>
        {
            var keyHash = key.GetHashCode();
            if (!self.IsPause)
            {
                var canExit = true;
                if (self.States.TryGetValue(keyHash, out var state))
                    canExit = state.ExitCondition(self, param);
                if (canExit
                    && ((keyHash - 1) & keyHash) == 0
                    && (self.CurrentState & keyHash) != 0)
                {
                    IFlagsSMachine<TEnum, TState>.SingleExit(self, keyHash);
                    state?.Exit(self, param);
                    return true;

                }
            }

            return false;
        }
        public static bool ChangeState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TValue param) where TEnum : Enum where TState : IState<TValue>
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
                        success = success && EnterSingleState(self, (TEnum)(object)(1 << i), param);
                        add -= 1 << i;
                    }

                    i++;
                }

                i = 0;
                while (remove != 0 && i < 32)
                {
                    if ((remove & (1 << i)) != 0)
                    {
                        success = success && ExitSingleState(self, (TEnum)(object)(1 << i), param);
                        remove -= 1 << i;
                    }

                    i++;
                }

                return success;
            }

            return false;
        }
        public static bool ReplaceSingleState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TEnum key, TState state, TValue param) where TEnum : Enum where TState : IState<TValue>
        {
            if (!self.IsPause
                && ISMachine<int, TState>.StateReplace(self, key.GetHashCode(), state, out var oldState))
            {
                oldState.Exit(self,param);
                state.Enter(self,param);
                return true;
            }

            return false;
        }
        public static bool ExitCurrentState<TEnum, TState, TValue>(this IFlagsStateMachine<TEnum, TState, TValue> self, TValue param) where TEnum : Enum where TState : IState<TValue>
        {
            return ChangeState(self, default(TEnum), param);
        }
        
    }
}