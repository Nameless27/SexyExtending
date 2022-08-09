using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SexyExtending.Modpack
{
    public class ModpackManager
    {
        public static readonly ModpackManager Instance = new ModpackManager();

        public bool Add(IMod mod) => AddMod(mod);

        public bool AddMod(IMod mod)
        {
            if (mod == null)
                return false;
            if (mods.Contains(mod))
            {
                return false;
            }
            mods.Add(mod);
            return true;
        }

        public IMod GetMod(int index)
        {
            if (index < 0)
                return null;
            if (mods.Count > index)
                return mods[index];
            return null;
        }

        public IMod GetMod(string name)
        {
            return mods.Where(m => m.name == name).FirstOrDefault();
        }

        internal ObservableCollection<IMod> mods = new ObservableCollection<IMod>();

        public IEnumerable<IMod> Mods => mods;
    }
}
