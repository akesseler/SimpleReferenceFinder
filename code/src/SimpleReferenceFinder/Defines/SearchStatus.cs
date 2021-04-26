using Plexdata.Utilities.Attributes;

namespace Plexdata.SimpleReferenceFinder.Defines
{
    public enum SearchStatus
    {
        [Annotation]
        Unknown,

        [Annotation("starting", "Operation is starting up.")]
        Starting,

        [Annotation("scanning", "Scanning source folder.")]
        Scanning,

        [Annotation("processing", "Processing scan results.")]
        Processing,

        [Annotation("finished", "Operation finished.")]
        Finished,

        [Annotation("canceled", "Operation canceled.")]
        Canceled,

        [Annotation("failure", "Operation failed.")]
        Failure,
    }
}
