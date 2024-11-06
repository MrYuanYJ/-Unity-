using System;

namespace EasyFramework
{
    public class AFlagsStateMachine<TEnum,TState>: ASM<int,TState>,IFlagsStateMachine<TEnum,TState> where TEnum : Enum where TState : IState
    {
    }
}