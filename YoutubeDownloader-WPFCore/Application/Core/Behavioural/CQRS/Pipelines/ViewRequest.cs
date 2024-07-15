using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines
{
    public abstract class ViewRequest<TResult, T> : IRequest<TResult> where T : class  where TResult : class
    {
        protected Guid Guid { get; init; } = Guid.NewGuid();
        private WeakReference<T>? ViewReference { get; set; }
        public T View => ViewReference.TryGetTarget(out T target) ? target : target;

        public void Accept(T view)
        {
            ViewReference = new WeakReference<T>(view);
        }

        public bool Visit(out T visitor)
        {
            if (ViewReference!.TryGetTarget(out T target))
            {
                visitor = target;
                return true;
            }

            visitor = null;
            return false;
        }
    }
}
