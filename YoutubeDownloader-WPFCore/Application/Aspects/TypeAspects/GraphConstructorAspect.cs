using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metalama.Extensions.DependencyInjection;

namespace YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects
{
    internal class GraphConstructorAspect : TypeAspect
    {

        [IntroduceDependency] private Dictionary<int, object> _graph;

        public override void BuildAspect(IAspectBuilder<INamedType> builder)
        {
            foreach (var constructor in builder.Target.Constructors)
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
