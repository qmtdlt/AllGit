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

### 2018-02-26

> 角色修改方案

```sql
select * from Sys_Role
select * from Sys_RoleAuthorize

--Sys_RoleAuthorize.F_ObjectId 角色id
select * from Sys_Role where F_Id = '3f97c8f1-5030-4c9f-a290-eef7dc8f94af'

--Sys_RoleAuthorize.F_ItemId 菜单、按钮、字段等对象在数据库的id
--F_ItemType 1菜单id2；按钮id；3字段id
select * from Sys_Module where F_Id = '462027E0-0848-41DD-BCC3-025DCAE65555'
```

<p style="color:red;font-size:24px">问题</p>
<input type="button" value="确认">

> 多选角色后生成组合角色，原子角色修改，组合角色无法相应更改