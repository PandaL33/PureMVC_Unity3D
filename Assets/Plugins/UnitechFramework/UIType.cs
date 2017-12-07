/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-02
    * 描述：       
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;

namespace Unitech.Net.UnityFramework
{
    public class UIType
    {
        /// <summary>
        /// 是否清空UI窗体栈
        /// </summary>
        public bool IsClearStack = false;

        /// <summary>
        /// UI窗体（位置）类型
        /// </summary>
        public UIFormType UIForm_Type = UIFormType.Normal;

        /// <summary>
        /// UI窗体显示类型
        /// </summary>
        public UIFormShowMode UIForm_ShowMode = UIFormShowMode.Normal;

        /// <summary>
        /// UI窗体透明度类型
        /// </summary>
        public UIFormLucencyType UIForm_LucencyType = UIFormLucencyType.Lucency;

    }
}
