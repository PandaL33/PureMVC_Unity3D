/**********************************
*	作者：李国煌
*  时间：2017-07-03
*  描述：
*  修改人：
*  修改时间：
**********************************/
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace Unitech.Net.UnityDemo
{
	public class MarketCommand : SimpleCommand {

        public override void Execute(INotification notification)
        {
            MarketProxy marketProxy = Facade.RetrieveProxy(MarketProxy.NAME) as MarketProxy;
            marketProxy.UpdateData(notification.Body as MarketData);
            SendNotification(ProConst.PRO_MVC_MARKETINFO);
        }
	}
}
