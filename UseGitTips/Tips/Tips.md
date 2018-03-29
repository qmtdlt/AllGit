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

### 2018 03 06

- [ ] findout how authroize="yes" works

### 2018 03 07

- [ ] findout how ahthorize="yes" works
> 与之相关的js逻辑代码所在文件名framework-ui.js,代码如下

```js
$.fn.authorizeButton = function () {
    var moduleId = top.$(".NFine_iframe:visible").attr("id").substr(6);
    var dataJson = top.clients.authorizeButton[moduleId];
    var $element = $(this);
    $element.find('a[authorize=yes]').attr('authorize', 'no');
    if (dataJson != undefined) {
        $.each(dataJson, function (i) {
            $element.find("#" + dataJson[i].F_EnCode).attr('authorize', 'yes');
        });
    }
    $element.find("[authorize=no]").parents('li').prev('.split').remove();
    $element.find("[authorize=no]").parents('li').remove();
    $element.find('[authorize=no]').remove();
}
```

- [ ] authorizeButton函数怎样被触发？
> 对应函数的html代码
```html
<script>$('.toolbar').authorizeButton()</script>
```

> 第一次成功控制按钮
# 步骤一
```html
<link href="~/Content/css/framework-theme.css" rel="stylesheet" />
<script src="~/Content/js/jquery/jquery-2.1.1.min.js"></script>
<link href="~/Content/js/bootstrap/bootstrap.min.css" rel="stylesheet" />
<script src="~/Content/js/framework-ui.js"></script>

<div class="toolbar">
<div class="btn-group">
    <a authorize="yes" class="btn btn-primary dropdown-text" id="btnSearch" style="font-size:16px;"><i class="icon icon-search"></i> 查询</a>
    <a name="btnAddAsset" authorize="yes" class="btn btn-primary dropdown-text" id="btnAddAsset" onclick="btnAddAsset()" style="font-size:16px;"><i class="icon icon-ok"></i> 新增</a>
    <a name="btnLoadAsset" authorize="yes" class="btn btn-primary dropdown-text" id="btnLoadAsset" onclick="btnLoadAsset()" style="font-size:16px;"><i class="icon icon-upload"></i> 导入</a> 
    <a name="btnUpLoadAsset" authorize="yes" class="btn btn-primary dropdown-text" id="btnUpLoadAsset" onclick="btnUpLoadAsset()" style="font-size:16px;"><i class="icon icon-download"></i> 导出</a>
    <a name="btnUpLoadAssetAll" authorize="yes" class="btn btn-primary dropdown-text" id="btnUpLoadAssetAll" onclick="btnUpLoadAssetAll()" style="font-size:16px;"><i class="icon icon-download"></i> 全部导出</a>
    <a name="btnDelAsset" authorize="yes" class="btn btn-primary dropdown-text" id="btnDelAsset" style="font-size:16px;"><i class="icon icon-remove"></i> 删除</a>
    <a name="btnToTempAsset" authorize="yes" class="btn btn-primary dropdown-text" id="btnToTempAsset" style="font-size:16px;"><i class="icon icon-camera"></i>发布至预选</a>
    <a name="btnToTempAssetall" authorize="yes" class="btn btn-primary dropdown-text" id="btnToTempAssetall" style="font-size:16px;"><i class="icon icon-camera"></i>版权节目发布至预选</a>
</div>  
    <script>$('.toolbar').authorizeButton()</script>
</div>
```

### 以上代码解释
按照以上格式，注意
> 1. 按钮写为<a></a>标签

> 2. 添加属性 authorize="yes" 和类 class="btn btn-primary dropdown-text"，authorize="yes"表示按钮需要判断权限，class="btn btn-primary dropdown-text"控制按钮同意样式

> 3. 按钮全部放在<div class="btn-group"></div>内，该div外写<script>$('.toolbar').authorizeButton()</script>，以此触发权限判断代码即可
# 步骤二
> 4. 按钮按照上述代码div嵌套格式

```C#
    [HttpGet]
    [HandlerAuthorize]
    public ActionResult Index()
    {}
```

控制器中Index函数上加[HttpGet][HandlerAuthorize]两个属性

# 步骤三
> 在界面设置操作：系统管理=》系统菜单=》选择已登记资源（具体以自己的模块为准）=》按钮管理=》选择按钮=》修改按钮；将所有按钮编号修改为按钮标签(<a></a>)的id；

[奈文摩尔](https://github.com/qmtdlt/AllGit/blob/master/UseGitTips/Tips/Tulips.jpg)


### 权限测试用例编写

> 1. 测试是否影响到以前的单角色功能

> 2. 测试新的多角色功能

### 单角色功能测试
- [ ] 1. 代码审查（位置：RoleController）
- [ ] 2. 

### 多角色功能测试

### Markdown中插入图片

这是图片（去掉https前的斜杠后食用）：
![][avatar]

[avatar]: https://raw.githubusercontent.com/qmtdlt/AllGit/master/UseGitTips/Tips/Tulips.jpg

这是图片：
![Aaron Swartz](https://raw.githubusercontent.com/qmtdlt/AllGit/master/UseGitTips/Tips/Tulips.jpg)


### 使用Attribute
```C#
using System;
using System.Reflection;

// An enumeration of animals. Start at 1 (0 = uninitialized). 
public enum Animal {
    // Pets.
    Dog = 1,
    Cat,
    Bird,
}

// A custom attribute to allow a target to have a pet. 
public class AnimalTypeAttribute : Attribute {
    // The constructor is called when the attribute is set. 
    public AnimalTypeAttribute(Animal pet) {
        thePet = pet;
    }

    // Keep a variable internally ... 
    protected Animal thePet;

    // .. and show a copy to the outside world. 
    public Animal Pet {
        get { return thePet; }
        set { thePet = value; }
    }
}

// A test class where each method has its own pet. 
class AnimalTypeTestClass {
    [AnimalType(Animal.Dog)]
    public void DogMethod() {}

    [AnimalType(Animal.Cat)]
    public void CatMethod() {}

    [AnimalType(Animal.Bird)]
    public void BirdMethod() {}
}

class DemoClass {
    static void Main(string[] args) {
        AnimalTypeTestClass testClass = new AnimalTypeTestClass();
        Type type = testClass.GetType();
        // Iterate through all the methods of the class. 
        foreach(MethodInfo mInfo in type.GetMethods()) {
            // Iterate through all the Attributes for each method. 
            foreach (Attribute attr in
                Attribute.GetCustomAttributes(mInfo)) {
                // Check for the AnimalType attribute. 
                if (attr.GetType() == typeof(AnimalTypeAttribute))
                    Console.WriteLine(
                        "Method {0} has a pet {1} attribute.",
                        mInfo.Name, ((AnimalTypeAttribute)attr).Pet);
            }

        }
    }
}
/*
 * Output:
 * Method DogMethod has a pet Dog attribute.
 * Method CatMethod has a pet Cat attribute.
 * Method BirdMethod has a pet Bird attribute.
 */
```

> 条款、合同金额

# 主页修改为树形结构

> 查询sys_config表，区分消息类型

> 需求分析：

根据以下sql查询结果，获得所有消息类型
```sql
select * from sys_config where ConfigGroup = 12
```
结论为必要节点如下：

configName | configValue
-|:-
合同审核|1
发行合同审核|2
采购合同执行单审核|3
发行合同执行单审核|4
采购合同介质审核单审核|5
付款申请单审核|6
销售开票通知单审核|7
合同归档审核|8
合同变更单审核|9
系统消息|10

> 直接在界面修改代办显示方式，前提条件，实现一个展开收起的html动作

> 使用layui，手风琴展开demo

```html
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>

</head>
	<script type="text/javascript" src="../jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="../layui/layui.js"></script>
    <link rel="stylesheet" type="text/css" href="../layui/css/layui.css"/>
<body>

    <div class="layui-collapse">
      <div class="layui-colla-item">
        <h2 class="layui-colla-title">杜甫</h2>
        <div class="layui-colla-content layui-show">内容区域</div>
      </div>
      <div class="layui-colla-item">
        <h2 class="layui-colla-title">李清照</h2>
        <div class="layui-colla-content">内容区域</div>
      </div>
      <div class="layui-colla-item">
        <h2 class="layui-colla-title">鲁迅</h2>
        <div class="layui-colla-content">内容区域</div>
      </div>
    </div>
 
<script>
//注意：折叠面板 依赖 element 模块，否则无法进行功能性操作
layui.use('element', function(){
  var element = layui.element;
  
  //…
});
</script>
</body>
</html>

```

> 写死根节点，节点内循环打印消息

> 查询时，按照原结构显示查询结果。并显示结果数量

> 思路

### 不通过消息推送至待办后产生的问题

> 提审动作怎样知道关联的是哪个消息

# **C# 中文转拼音**





# **C# 问题**

1. 一个xml只有一组ufinterface标签
2. 一个凭证，凭证日期，制单人，帐薄，制单人等信息只能出现一次，即如下信息不能出现两组

```xml
<pk_vouchertype>01</pk_vouchertype>
<year>2018</year>
<period>03</period>
<pk_system>GL</pk_system>
<voucherkind>0</voucherkind>
<pk_accountingbook>BJZG250101-0001</pk_accountingbook>
<discardflag>N</discardflag>
<prepareddate>2018-03-23 15:55:36</prepareddate>
<pk_prepared>yonyou1</pk_prepared>
<signflag>N</signflag>
<pk_org>BJZG250101</pk_org>
<pk_group>01</pk_group>
``` 
3. 连个凭证合成一个，所有的分录需要写在details标签内，一个xml只有一组detail标签
4. merge.xml修改方法：删除如下内容：
```xml
</details> </voucher_head> </voucher> </ufinterface><ufinterface account="0001" billtype="vouchergl" businessunitcode="develop" filename="" groupcode="01" isexchange="" orgcode="BJZG0101" receiver="0001A1100000000006EN" replace="" roottag="" sender="default">
<voucher>
<voucher_head>
    <pk_vouchertype>01</pk_vouchertype>
    <year>2018</year>
    <period>03</period>
    <pk_system>GL</pk_system>
    <voucherkind>0</voucherkind>
    <pk_accountingbook>BJZG250101-0001</pk_accountingbook>
    <discardflag>N</discardflag>
    <prepareddate>2018-03-23 15:55:37</prepareddate>
    <pk_prepared>yonyou1</pk_prepared>
    <signflag>N</signflag>
    <pk_org>BJZG250101</pk_org>
    <pk_group>01</pk_group>
    <details>
```
> 说明：删除第一个detail结束标签到第二个detail开始标签之间的内容,即可导入为一张凭证

# **去掉返回按钮**

> 参考发行合同变更单 链接标签
```html
<a  class='menuItem' href='/BusinessSaleManage/ContractManage/Details?id=90936' data-id='/BusinessSaleManage/ContractManage/Details?id=90936'><u style="font-size:14px;color:#ff0000">FX-SZDSYWB(CZTV201706210037)</u></a>
```

> 首页链接
```html
<a class="menuItem" data-id="87f2f557-c3fa-41ed-a36b-a498f299207e" data-index="91" href="/BusinessSaleManage/ContractManage/Details?id=90972&amp;audit=1"><u style="font-size:14px;color:#ff0000">审核</u></a>
```

### **1. 同样链接，主页链接到合同仍然在原页面**
主页解决方案：

```js
$(function () {
        $('.menuItem').on('click', top.$.nfinetab.addTab);
    });
```










