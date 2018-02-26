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

--观察Sys_User表角色 50*78=3900,78个角色,初步够用的前提下，F_RoleId设置为4000
select * from Sys_User
```

<p style="color:red;font-size:24px">问题</p>

### 多选角色后生成组合角色，原子角色修改，组合角色无法相应更改

> 1. 用户多选角色

- [x] 修改用户表F_RoleId字段为4000大小

> 2. <p style="color:red;">用户F_RoleId字段保存多个角色id，以逗号隔开，完成后，怎么办？</p>

> 3. <p style="color:red;">另一中方法，原子角色修改后，找到使用过该原子角色的组合角色，一并修改，这要求组合角色能够分析出包含了哪些原子角色<span style="color:blue;">暂时排除</span></p>
- [x] 完成角色多选，保存多个角色id，以逗号隔开
- [ ] 修改ClientsDataController.cs中GetMenuButtonList()函数
- [ ] 查看角色时，对应用户显示有多个角色

### TodayMethod
```C#
    //两个list，使用AddRange合并，使用HashSet去除重复项
    //此结果为打印
    //1
    //2
    //3
    //4
    //5
    List<int> a = new List<int>();
    a.Add(1);
    a.Add(2);
    a.Add(3);
    List<int> b = new List<int>();
    b.Add(3);
    b.Add(4);
    b.Add(5);
    a.AddRange(b);
    HashSet<int> d = new HashSet<int>(a);
    foreach (var item in d)
    {
        Console.WriteLine(item);
    }
```
