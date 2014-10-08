using LinFu.DynamicProxy;
using MyRibbonAOP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Presentation
{
    public class Aging_Point : IInvokeWrapper
    {
        private void afterConstructorCall()
        {
            _point.timestamp = DateTime.Now;            
        }

        #region AOP Tricks
        protected readonly dynamic _point;
        public Aging_Point(dynamic target)
        {
            _point = target;
        }

        public void AfterInvoke(InvocationInfo info, object returnValue) { }
        public void BeforeInvoke(InvocationInfo info) { }

        public object DoInvoke(InvocationInfo info)
        {
            if (info.TargetMethod.Name == "init")
            {
                info.TargetMethod.Invoke(_point, info.Arguments);
                afterConstructorCall();
            }

            return null;
        }


        #endregion
    }

    public class Aging_RibbonSet : IInvokeWrapper
    {
        // after refreshRibbons
        private void truncateTheTracks()
        {
            var now = DateTime.Now;
            var tobeRemoved = new List<Track>();
            TrackSet tracks = (TrackSet)_ribbonSet.tracks;
            foreach(Track t in tracks)
            {
                cleanup(t, now);
                if( t.isClosed() && t.Count == 0 )
                {
                    tobeRemoved.Add(t);
                }
            }

            tobeRemoved.ForEach(t => ((TrackSet)_ribbonSet.tracks).remove(t));
        }

         private void cleanup(Track t, DateTime now)
         {
            var tobeRemoved = new List<Point>();
             foreach(dynamic p in t)
             {
                 if( p.timestamp < now.Subtract(TimeSpan.FromMilliseconds(600)))
                 {
                     tobeRemoved.Add(p);
                 }
             }
             tobeRemoved.ForEach(p => t.remove(p));
         }

        #region AOP Tricks
        protected readonly dynamic _ribbonSet;
        public Aging_RibbonSet(dynamic target)
        {
            _ribbonSet = target;
        }

        public void AfterInvoke(InvocationInfo info, object returnValue) { }
        public void BeforeInvoke(InvocationInfo info) { }

        public object DoInvoke(InvocationInfo info)
        {
            if (info.TargetMethod.Name == "refreshRibbons")
            {
                info.TargetMethod.Invoke(_ribbonSet, info.Arguments);
                truncateTheTracks();
            }

            return null;
        }


        #endregion
    }
}


