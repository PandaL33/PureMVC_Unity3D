/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-02
    * 描述： UI框架核心参数
    * 功能：
    *           1、系统常量
    *           2、全局性方法
    *           3、系统枚举类型
    *           4、委托定义
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;

namespace Unitech.Net.UnityFramework
{
    public class SysDefine : MonoBehaviour
    {
        /*路径常量 */
        public const string SYS_PATH_UIROOT = @"UIPrefabs/UIRoot";
        public const string SYS_PATH_UIFORMS_CONFIG_INFO = @"JsonConfig/UIFormsConfigInfo";
        public const string SYS_PATH_SysConfigJson = @"JsonConfig/SysConfigInfo";
        /* 标签常量*/
        public const string SYS_TAG_UIROOT = "TagUIRoot";
        public const string SYS_TAG_UICAMERA = "TagUICamera";
        /* 节点常量 */
        public const string SYS_NORMAL_NODE = "Normal";
        public const string SYS_FIXED_NODE = "Fixed";
        public const string SYS_POPUP_NODE = "PopUp";
        public const string SYS_SCRIPTMANAGER_NODE = "ScriptsMgr";
        public const string SYS_UIMASKPANEL_NODE = "UIMaskPanel";
        //完全透明度
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB = 255F / 255F;
        public const float SYS_UIMASK_LUCENCY_COLOR_A = 0F / 255F;
        //半透明度
        public const float SYS_UIMASK_TRANSLUCENCY_COLOR_RGB = 220F / 255F;
        public const float SYS_UIMASK_TRANSLUCENCY_COLOR_A = 50F / 255F;
        //低透明度
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB = 50F / 255F;
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_A = 200F / 255F;
        /// <summary>
        /// UI摄像机，层深增加量
        /// </summary>
        public const int SYS_UICAMERA_DEPTH_INCREMENT = 100;
        /*全局方法*/

        /*委托定义*/

    }
}
