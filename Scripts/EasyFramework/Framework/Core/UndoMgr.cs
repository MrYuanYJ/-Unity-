using System.Collections.Generic;

namespace EasyFramework
{
    public class UndoMgr
    {
        private static List<IUndoAble> Undos = new();

        public static void Add(IUndoAble undoAble)
        {
            Undos.Add(undoAble);
            if (Undos.Count > 128)
                Undos.Remove(Undos[0]);
        }
        public static void UnDo()
        {
            var undo=Undos[Undos.Count-1];
            undo.Undo();
        }

        public static void Remove(IUndoAble undoAble)
        {
            Undos.Remove(undoAble);
        }
    }
}