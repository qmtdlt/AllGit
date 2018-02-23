### 2017-02-23
```C#
/// <summary>
/// 函数功能：判断当前菜单最父级是否需要显示，返回值true显示，false不显示
/// </summary>
/// <returns></returns>
private bool ModuleCanShow(ModuleEntity module)
{
    if (module.F_ParentId != "0" && module.F_ParentId != null)
    {
        bool bTmp = ModuleCanShow(moduleApp.GetForm(module.F_ParentId));
        return bTmp;
    }
    else if (module.F_ParentId == null)
    {
        return false;
    }
    else
    {
        //如果最父级为这三个，不用显示
        if (module.F_FullName.Contains("系统管理")||module.F_FullName.Contains("基础档案")||module.F_FullName.Contains("审批流"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
```

[附件](http://www.google.com/)