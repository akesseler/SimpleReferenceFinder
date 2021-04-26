
using Plexdata.Utilities.Attributes;

namespace Plexdata.SimpleReferenceFinder.Defines
{
    public enum SearchType
    {
        [Annotation("Resolve references of files in other files.", SearchType.FileReferences)]
        FileReferences,
    }
}
