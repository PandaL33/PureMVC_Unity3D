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

namespace Unitech.Net.UnityDemo
{
    public class InfoCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            InfoUIForm view = notification.Body as InfoUIForm;
            if (view != null)
            {
                view.OpenUIForm(ProConst.PRO_UIFORM_MARKETUIFORM);
                SendNotification(ProConst.PRO_MVC_MARKET_INFO, new MarketData() { Name = "信息窗体", Age = "18", Sex = "女" });
            }
        }
	}
}
