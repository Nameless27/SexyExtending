using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SexyExtending
{
    public interface IExtensionInfo
    {
        string Id { get; }

        string Name { get; }

        string Description { get; }

        string Author { get; }

        string Version { get; }

        string Link { get; }
    }
}
