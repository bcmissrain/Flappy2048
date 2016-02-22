using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public class SABetweenScreen : SAScreen
    {
        public virtual int MAX_COUNT { get { return 60; } }
        public bool CanDrawNextScreen { get { return loadStatus == LoadStatus.Begin ? false : true; } }
        protected enum LoadStatus
        {
            Begin,
            Wait,
            End
        };
        protected LoadStatus loadStatus;
        protected int counter;

        public SABetweenScreen(ChangeScreenDelegate changeScreenDelegate) : base(changeScreenDelegate) { }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            UpdateLoadStatus();
        }
        public void UpdateLoadStatus()
        {
            switch (loadStatus)
            {
                case LoadStatus.Begin:
                    BeginLoad();
                    break;
                case LoadStatus.Wait:
                    WaitLoad();
                    break;
                case LoadStatus.End:
                    EndLoad();
                    break;
            }
        }
        public virtual void BeginLoad()
        {

        }
        public virtual void WaitLoad()
        {

        }
        public virtual void EndLoad()
        {

        }
    }
}
