/**********************************
*	作者：李国煌
*  时间：2017-07-19
*  描述：
*  修改人：
*  修改时间：
**********************************/
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;

namespace Unitech.Net.UnityDemo
{
    public class MarketMediator : Mediator
    {
        public new const string NAME = "MarketMediator";
        private MarketProxy marketProxy;
        private MarketUIForm View
        {
            get { return (MarketUIForm)ViewComponent; }
        }

        public MarketMediator(MarketUIForm viewComponent)
            : base(NAME, viewComponent)
        {

        }

        public override void OnRegister()
        {
            base.OnRegister();
            marketProxy = Facade.RetrieveProxy(MarketProxy.NAME) as MarketProxy;
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(ProConst.PRO_MVC_MARKETINFO);
            return list;
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case ProConst.PRO_MVC_MARKETINFO:
                    View.ShowData(marketProxy.Data as MarketData);
                    break;
            }
        }
    }
}
