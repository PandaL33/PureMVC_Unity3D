/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-19
    * 描述：  Demo启动应用程序    
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Unitech.Net.UnityFramework;

namespace Unitech.Net.UnityDemo
{
    public class StartProject : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
           ApplicationFacade facade = ApplicationFacade.Instance as ApplicationFacade;
           facade.StartUp(ProConst.PRO_UIFORM_LOGINUIFORM);
        }

    }
}
