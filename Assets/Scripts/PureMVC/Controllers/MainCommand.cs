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
    public class MainCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            MainUIForm view = notification.Body as MainUIForm;
            if (view != null)
            {
               view.OpenUIForm(ProConst.PRO_UIFORM_MARKETUIFORM);
               switch (notification.Name)
               {
                   case ProConst.PRO_MVC_MAIN_BTNINFO:
                       SendNotification(ProConst.PRO_MVC_MARKET_MAIN, new MarketData() { Name = "主窗体__信息", Age = "20", Sex = "男" });
                       break;
                   case ProConst.PRO_MVC_MAIN_BTNSKILL:
                       SendNotification(ProConst.PRO_MVC_MARKET_MAIN, new MarketData() { Name = "主窗体__技能", Age = "40", Sex = "男" });
                       break;
               }
            }
        }
	}
}
