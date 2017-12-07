/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-02
    * 描述： UI管理器
    * 功能：UI框架核心，冲虚通过本类实现框架的大多数功能  
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Unitech.Net.UnityFramework
{
    public class UIManager : MonoBehaviour
    {

        //UI管理者（单例）
        private static UIManager _Instance = default(UIManager);

        //UI窗体预设路径（<窗体预设名称，窗体预设路径>)
        private Dictionary<string, string> _DicFormsPaths;
        //缓存所有UI窗体
        private Dictionary<string, BaseUIForm> _DicAllUIForms;
        //当前显示的UI窗体
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
        //定义栈集合（具备“反向切换”属性的窗体类型）
        private Stack<BaseUIForm> _StaCurrentUIForms;

        //UI根节点
        private Transform _TraCanvasTransform = default(Transform);
        //全屏幕显示的节点
        private Transform _TraNormal = default(Transform);
        //固定显示的节点
        private Transform _TraFixed = default(Transform);
        //弹出节点
        private Transform _TraPopUp = default(Transform);
        //UI管理脚本的节点
        private Transform _TraUIScripts = default(Transform);

        /// <summary>
        /// UI管理器（单例）
        /// </summary>
        /// <returns></returns>
        public static UIManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
                return _Instance;
            }
        }

        void Awake()
        {
            //初始化字段
            _DicAllUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
            _DicFormsPaths = new Dictionary<string, string>();
            _StaCurrentUIForms = new Stack<BaseUIForm>();
            //初始化加载根窗体预设
            InitUIRoot();
            //获取UI根窗体节点、全屏节点、固定节点、弹出节点
            _TraCanvasTransform = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_UIROOT).transform;
            _TraNormal = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_NORMAL_NODE);
            _TraFixed = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_FIXED_NODE);
            _TraPopUp = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_POPUP_NODE);
            _TraUIScripts = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_SCRIPTMANAGER_NODE);
            //添加本脚本到脚本节点
            gameObject.transform.SetParent(_TraUIScripts, false);
            //根窗体在场景转换时，设置为不可销毁
            DontDestroyOnLoad(_TraCanvasTransform);
            //初始化UI窗体预设路径
            InitUIFormsPathData();
        }

        /// <summary>
        /// 显示打开窗体
        /// </summary>
        /// <param name="uiFomName">窗体名称</param>
        public void ShowUIForms(string uiFomName)
        {
            BaseUIForm baseUIForms;//UI窗体基类
            //参数检查
            if (string.IsNullOrEmpty(uiFomName))
                return;
            //加载UI窗体
            baseUIForms = LoadFormsToAllUIFormsCatch(uiFomName);
            if (baseUIForms == null)
                return;

            //是否清空“栈集合”中得数据
            if (baseUIForms.CurrentUIType.IsClearStack)
            {
                ClearStackArray();
            }

            switch (baseUIForms.CurrentUIType.UIForm_ShowMode)
            {
                case UIFormShowMode.Normal://普通窗体
                    LoadUIToCurrentCache(uiFomName);
                    break;
                case UIFormShowMode.ReverseChange://反向切换窗体
                    PushUIFormToStack(uiFomName);
                    break;
                case UIFormShowMode.HideOther://隐藏其它窗体
                    EnterUIFormsAndHideOther(uiFomName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 关闭（返回上一个）窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        public void CloseUIForms(string uiFormName)
        {
            BaseUIForm baseUIForm;

            if (string.IsNullOrEmpty(uiFormName))
            {
                return;
            }

            _DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm == null)
                return;
            switch (baseUIForm.CurrentUIType.UIForm_ShowMode)
            {
                case UIFormShowMode.Normal:
                    ExitUIForm(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:
                    PopUIForm();
                    break;
                case UIFormShowMode.HideOther:
                    ExitUIFormsAndDisplayOther(uiFormName);
                    break;
            }
        }

        #region Private Methods

        /// <summary>
        /// 初始化UI窗体根节点
        /// </summary>
        private void InitUIRoot()
        {
            ResourcesMgr.Instance.LoadAsset(SysDefine.SYS_PATH_UIROOT, false);
        }

        private BaseUIForm LoadFormsToAllUIFormsCatch(string uiFomName)
        {
            BaseUIForm baseUIForm = default(BaseUIForm);
            _DicAllUIForms.TryGetValue(uiFomName, out baseUIForm);

            if (baseUIForm == null)
            {
                baseUIForm = LoadForm(uiFomName);
            }
            return baseUIForm;
        }

        /// <summary>
        /// 加载指定名称UI窗体
        /// </summary>
        /// <param name="uiFormName">UIPrefab名称</param>
        private BaseUIForm LoadForm(string uiFormName)
        {
            string strUIFormPaths = string.Empty;
            GameObject goClonePrefabs = default(GameObject);
            BaseUIForm baseUIForm = default(BaseUIForm);

            _DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths);
            if (!string.IsNullOrEmpty(strUIFormPaths))
            {
                goClonePrefabs = ResourcesMgr.Instance.LoadAsset(strUIFormPaths, false);
            }

            if (_TraCanvasTransform != null && goClonePrefabs != null)
            {
                baseUIForm = goClonePrefabs.GetComponent<BaseUIForm>();
                if (baseUIForm == null)
                {
                    Debug.LogError("BaseUIForm为空，窗体预制件上未加载BaseUIForm的子类脚本，Prefab=" + uiFormName);
                    return null;
                }
                switch (baseUIForm.CurrentUIType.UIForm_Type)
                {
                    case UIFormType.Normal:
                        goClonePrefabs.transform.SetParent(_TraNormal, false);
                        break;
                    case UIFormType.Fixed:
                        goClonePrefabs.transform.SetParent(_TraFixed, false);
                        break;
                    case UIFormType.PopUp:
                        goClonePrefabs.transform.SetParent(_TraPopUp, false);
                        break;
                }

                goClonePrefabs.SetActive(false);

                _DicAllUIForms.Add(uiFormName, baseUIForm);
                return baseUIForm;
            }
            else
            {
                Debug.LogError("UIRoot为空或者UIPrefab为空，参数uiFormName=" + uiFormName);
                return null;
            }
        }

        /// <summary>
        /// 将当前窗体加载到“当前窗体”集合中
        /// </summary>
        /// <param name="uiForme">窗体预设名称</param>
        private void LoadUIToCurrentCache(string uiFormName)
        {
            BaseUIForm baseUIForm = default(BaseUIForm);
            BaseUIForm baseUIFormFromAllCache = default(BaseUIForm);
            //如果“正在显示"的集合中，存在UI窗体，则直接返回
            _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm != null)
                return;
            //将当前窗体，加载到正在显示集合中
            _DicAllUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache != null)
            {
                _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();//显示当前窗体
            }
        }

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
        private void PushUIFormToStack(string uiFormName)
        {
            BaseUIForm baseUIForm;

            if (_StaCurrentUIForms.Count > 0)
            {
                BaseUIForm topUIForm = _StaCurrentUIForms.Peek();
                topUIForm.Freeze();
            }

            _DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);

            if (baseUIForm != null)
            {
                baseUIForm.Display();
                _StaCurrentUIForms.Push(baseUIForm);
            }
            else
            {
                Debug.Log("baseUIForm==null，Please Check,Params uiFormName=" + uiFormName);
            }
        }

        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
        private void ExitUIForm(string uiFormName)
        {
            BaseUIForm baseUIForm;
            _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm == null)
            {
                return;
            }

            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(uiFormName);
        }

        /// <summary>
        /// 反向切换窗体的出栈逻辑
        /// </summary>
        private void PopUIForm()
        {
            if (_StaCurrentUIForms.Count >= 2)
            {
                BaseUIForm topUIForm = _StaCurrentUIForms.Pop();
                topUIForm.Hiding();

                BaseUIForm nextUIForm = _StaCurrentUIForms.Peek();
                nextUIForm.ReDisplay();
            }
            else if (_StaCurrentUIForms.Count == 1)
            {
                BaseUIForm topUIForm = _StaCurrentUIForms.Pop();
                topUIForm.Hiding();
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)打开窗体，且隐藏其他窗体
        /// </summary>
        /// <param name="uiFormName">窗体名称</param>
        private void EnterUIFormsAndHideOther(string uiFormName)
        {
            BaseUIForm baseUIForm;                          //UI窗体基类
            BaseUIForm baseUIFormFromALL;                   //从集合中得到的UI窗体基类

            //参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;

            _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm != null) return;

            //把“正在显示集合”与“栈集合”中所有窗体都隐藏。
            foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.Hiding();
            }
            foreach (BaseUIForm staUI in _StaCurrentUIForms)
            {
                staUI.Hiding();
            }

            //把当前窗体加入到“正在显示窗体”集合中，且做显示处理。
            _DicAllUIForms.TryGetValue(uiFormName, out baseUIFormFromALL);
            if (baseUIFormFromALL != null)
            {
                _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromALL);
                //窗体显示
                baseUIFormFromALL.Display();
            }
        }


        /// <summary>
        /// (“隐藏其他”属性)关闭窗体，且显示其他窗体
        /// </summary>
        /// <param name="uiFormName">打开的指定窗体名称</param>
        private void ExitUIFormsAndDisplayOther(string uiFormName)
        {
            BaseUIForm baseUIForm;                          //UI窗体基类

            //参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;

            _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm == null) return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(uiFormName);

            //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
            foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.ReDisplay();
            }
            foreach (BaseUIForm staUI in _StaCurrentUIForms)
            {
                staUI.ReDisplay();
            }
        }

        /// <summary>
        /// 是否清空“栈集合”中得数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count >= 1)
            {
                //清空栈集合
                _StaCurrentUIForms.Clear();
                return true;
            }

            return false;
        }
        #endregion

        #region  显示“UI管理器”内部核心数据，测试使用

        /// <summary>
        /// 显示"所有UI窗体"集合的数量
        /// </summary>
        /// <returns></returns>
        public int ShowALLUIFormCount()
        {
            if (_DicAllUIForms != null)
            {
                return _DicAllUIForms.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 显示"当前窗体"集合中数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentUIFormsCount()
        {
            if (_DicCurrentShowUIForms != null)
            {
                return _DicCurrentShowUIForms.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 显示“当前栈”集合中窗体数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentStackUIFormsCount()
        {
            if (_StaCurrentUIForms != null)
            {
                return _StaCurrentUIForms.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 初始化“UI窗体预设”路径数据
        /// </summary>
        private void InitUIFormsPathData()
        {
            IConfigManager configMgr = new ConfigManagerByJson(SysDefine.SYS_PATH_UIFORMS_CONFIG_INFO);
            if (configMgr != null)
            {
                _DicFormsPaths = configMgr.AppSetting;
            }
        }

        #endregion

    }
}
