using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace YoutubeDownloaderMaui.Core.Aspects.TypeAspects
{
    internal class GraphConstructorAspect : TypeAspect
    {

        [IntroduceDependency] private Dictionary<int, object> _graph;

        public override void BuildAspect(IAspectBuilder<INamedType> builder)
        {
            foreach (var constructor in builder.Target.Constructors.Where(x => x.BelongsToCurrentProject))
            {
                builder.Advice.Override(constructor, nameof(OverrideConstructorTemplate));
            }

            var finalizer = builder.Target.Finalizer;
            builder.Advice.Override(finalizer, nameof(OverrideFinalizerTemplate));
        }

        [Template]
        private void OverrideConstructorTemplate()
        {
            Console.WriteLine($"Executing constructor {meta.Target.Constructor}: started");
            meta.Proceed();
            _graph.Add(meta.This.GetHashCode(), meta.This);
        }

        [Template]
        private void OverrideFinalizerTemplate()
        {
            _graph.Add(meta.This.GetHashCode(), meta.This);
            Console.WriteLine($"Executing finalizer {meta.Target.Type}: started");
            meta.Proceed();
        }
    }
}
