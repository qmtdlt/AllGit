### Step1. 用户多选角色

- [x] 修改用户表F_RoleId字段为4000大小(需要重新创建表，先备份表数据)

> 2. <p style="color:red;">用户F_RoleId字段保存多个角色id，以逗号隔开，完成后，怎么办？</p>

> 3. <p style="color:red;">另一中方法，原子角色修改后，找到使用过该原子角色的组合角色，一并修改，这要求组合角色能够分析出包含了哪些原子角色<span style="color:blue;">暂时排除</span></p>
- [x] 完成角色多选，保存多个角色id，以逗号隔开
- [x] 修改ClientsDataController.cs中GetMenuButtonList()函数
- [x] 查看角色时，对应用户显示有多个角色

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