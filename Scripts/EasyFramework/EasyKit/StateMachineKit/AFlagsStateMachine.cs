using System;
using System.Collections.Generic;

namespace EasyFramework.StateMachineKit
{
    public class AFlagsStateMachine<TEnum,TState>: ASM<int,TState>,IFlagsStateMachine<TEnum,TState> where TEnum : Enum where TState : IEasyState
    {
    }
}