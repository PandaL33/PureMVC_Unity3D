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
using Unitech.Net.UnityFramework;

namespace Unitech.Net.UnityDemo
{
    public class LoginCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            UIManager.Instance.ShowUIForms(ProConst.PRO_UIFORM_MAINUIFORM);
            UIManager.Instance.ShowUIForms(ProConst.PRO_UIFORM_INFOUIFORM);
        }
    }
}
