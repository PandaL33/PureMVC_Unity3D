/**********************************
*	作者：李国煌
*  时间：2017-07-03
*  描述：系统启动命令
*  修改人：
*  修改时间：
**********************************/
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Unitech.Net.UnityFramework;

public class StartUpCommand : SimpleCommand {

    public override void Execute(INotification notification)
    {
        UIManager.Instance.ShowUIForms(notification.Body.ToString());
    }
}
