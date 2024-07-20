using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Microsoft.Extensions.Logging;

namespace YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects
{

    public class OverrideInitAspect : OverrideEventAspect
    {
        [IntroduceDependency] private ILogger _logger;

        public override void BuildAspect(IAspectBuilder<IEvent> builder)
        {
            base.BuildAspect(builder);


        }

        public override void OverrideAdd(dynamic value)
        {
            // _logger.LogInformation("Overriding :" + meta.Target.Type);
        }

        public override void OverrideRemove(dynamic value)
        {
            //_logger.LogInformation("Overriding :" + meta.Target.Type);
        }
    }


    public class ControlAspect : TypeAspect
    {

        [IntroduceDependency]
        private ControlStore _controlStore;

        public override void BuildAspect(IAspectBuilder<INamedType> builder)
        {
            base.BuildAspect(builder);

            var events = builder.Target.AllEvents.Where(x => x.Definition.Name == "EndInit");
            foreach (var e  in events)
            {
                // e.R
            }

            /*foreach (var field in builder.Target.FieldsAndProperties.Where(f =>
                         !f.IsImplicitlyDeclared && f.Writeability != Writeability.None))
            {
                builder.Advice.IntroduceMethod(
                    builder.Target,
                    nameof(Set),
                    args: new
                    {
                        field = field, T = field.Type
                    },
                    buildMethod: m =>
                    {
                        m.Name = "Set" + CamelCase(field.Name);
                        m.ReturnType = builder.Target;
                    });
            }*/
        }

        private static string CamelCase(string s)
        {
            s = s.TrimStart('_');

            return s[0].ToString().ToUpperInvariant() + s[1..];
        }

        [Template]
        public dynamic Set<[CompileTime] T>(IFieldOrProperty field, T value)
        {
            field.Value = value;
            return meta.This;
        }
    }
}