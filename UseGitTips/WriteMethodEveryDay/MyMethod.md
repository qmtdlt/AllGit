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
- [x] 修改ClientsDataController.cs中GetMenuButtonList()函数
- [x] 查看角色时，对应用户显示有多个角色

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
    //当list内容为object
    //去重复失败
    public class mycls
    {
        public mycls(int iid)
        {
            id = iid;
        }
        public int id { get; set; }
    }
    static void Main(string[] args)
    {
        List<mycls> a = new List<mycls>();
        a.Add(new mycls(1));
        a.Add(new mycls(2));
        a.Add(new mycls(3));
        List<mycls> b = new List<mycls>();
        b.Add(new mycls(3));
        b.Add(new mycls(4));
        b.Add(new mycls(5));
        a.AddRange(b);
        HashSet<mycls> d = new HashSet<mycls>(a);
        System.Console.WriteLine(b[0].GetHashCode());
        System.Console.WriteLine(a[2].GetHashCode());
        foreach (var item in d)
        {
            Console.WriteLine(item.id);
        }
    }
```

### 2018-02-27

> 非正常角色导致角色认证错误

### 修改了3个函数内容如下：

```C#
//ClientsDataController
//添加多个角色的情况
private object GetMenuButtonList();
//添加多个角色的情况
private object GetMenuList();

//RoleAuthorizeApp
//添加多个角色的情况
public bool ActionValidate(string roleId, string moduleId, string action)
```

```js
//value值大致为："["3558c1d1-5489-4c09-960d-9d97767a995e","1c5b8148-7f52-417c-a37b-e87996e7e603",""]"
$id.val(value).trigger("change");
```

```js
//浪费时间的点忘记[],导致select2总是只有第一个被选中
$("#F_RoleId").val(['1c5b8148-7f52-417c-a37b-e87996e7e603', '3558c1d1-5489-4c09-960d-9d97767a995e']).trigger('change');
```

```js
//为select2多选框赋值
var value = $.trim(data["F_RoleId"]).replace(/&nbsp;/g, '');
//alert(value);
//value = value.toString().substring(1, value.length - 1);
var i = 0
var value2 = [];
var count = 0;
for (; i< value.toString().length; i++)
{
    var strtmp = "";
    if(value[i] == '"')
    {
        i++;
        while(value[i] != '"')
        {
            strtmp += value[i];
            i++;
        }
    }
    if(strtmp.length > 0)
    {
        value2[count] = strtmp;
        count++;
    }
}
//这里val(),括号内为数组时，显示多个被选中
$("#F_RoleId").val(value2).trigger("change");
```

### 发现bug

角色权限修改失败；
原因：界面上勾选错误，操作问题

- [ ] 多角色功能待测试

### 2018 02 28
> 2月最后一天

- [ ] 发行采购授权区域约束
- [ ] review 多角色代码
- [ ] 测试多角色功能
- [ ] 配置角色时，功能权限去掉勾选，下一步的字段权限也去掉对应勾选项，否则去除勾选失败
> - [ ] 查看系统是怎样通过权限，控制按钮隐藏的

bug
> 系统管理，新增菜单父级显示不全 

### 2018 03 04

> 付款申请单链接合同，看不到合同相关信息

- [ ] 查看合同主页如何解决

> 问题描述：

```js
src += "?ContractSerial=" + '@ViewBag.ContractSerial1' +"&view=1" + "&ContractNumber=" + "@ViewBag.ContractNumber1";
```
> 组织url时使用viewbag变量为字符串是未加单引号，导致变量值改变，加单引号后成功

### 2018 03 05
# 中公教育
> NC创建管理员

> root 集团管理员 动态建模平台 权限管理 用户




