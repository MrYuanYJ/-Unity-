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
       private IStructure _structure;

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

       public IStructure GetStructure()=> _structure;

       IStructure ISetStructureAbleAble.Structure
       {
           get => _structure;
           set => _structure = value;
       }
   }
   public abstract class AReturnCommand<TReturn>: ICommand<TReturn>
   {
       private IStructure _structure;

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

       public IStructure GetStructure() => _structure;

       IStructure ISetStructureAbleAble.Structure
       {
           get => _structure;
           set => _structure = value;
       }
   }
}