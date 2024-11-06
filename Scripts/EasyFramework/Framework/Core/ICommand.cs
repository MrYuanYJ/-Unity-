namespace EasyFramework
{
    public interface IExecuteAble
    {
        void Execute();
    }
    public interface ICanReturnExecute<R>
    {
        R Execute();
    }
    public interface IUndoAble
    {
        void Undo();
    }
    public interface ICommand: IExecuteAble,IUndoAble,ISetStructureAbleAble,IGetModelAble,IGetSystemAble,ISendEventAble,ISendCommandAble
    {
    }
    public interface ICommand<TReturn>: ICanReturnExecute<TReturn>,IUndoAble,ISetStructureAbleAble,IGetModelAble,IGetSystemAble,ISendEventAble,ISendCommandAble
    {
    }
    public interface ISendCommandAble: IGetStructureAble { }
   public abstract class ACommand: ICommand
   {
       public void Execute()
       {
           OnExecute();
           UndoMgr.Add(this);
       }

       public void Undo()
       {
           OnUndo();
           UndoMgr.Remove(this);
       }
       

       protected abstract void OnExecute();
       protected abstract void OnUndo();

       IStructure ISetStructureAbleAble.Structure { get; set; }
   }
   public abstract class AReturnCommand<TReturn>: ICommand<TReturn>
   {
       public TReturn Execute()
       {
           var @return= OnExecute();
           UndoMgr.Add(this);
           return @return;
       }

       public void Undo()
       {
           OnUndo();
           UndoMgr.Remove(this);
       }

       protected abstract TReturn OnExecute();
       protected abstract void OnUndo();

       IStructure ISetStructureAbleAble.Structure { get; set; }
   }
}