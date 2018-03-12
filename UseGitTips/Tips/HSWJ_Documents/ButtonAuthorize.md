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

> 3. 按钮全部放在```<div class="btn-group"></div>```内，该div外写```<script>$('.toolbar').authorizeButton()</script>```，以此触发权限判断代码

> 4. 按钮按照上述代码div嵌套格式
# 步骤二

```C#
    [HttpGet]
    [HandlerAuthorize]
    public ActionResult Index()
    {}
```

控制器中Index函数上加[HttpGet][HandlerAuthorize]两个属性

# 步骤三
> 在界面设置操作：系统管理=》系统菜单=》选择已登记资源（具体以自己的模块为准）=》按钮管理=》选择按钮=》修改按钮；将所有按钮编号修改为按钮a标签的id；

![Aaron Swartz](https://raw.githubusercontent.com/qmtdlt/AllGit/master/UseGitTips/Tips/Pics/%E6%8C%89%E9%92%AE%E6%B3%A8%E6%84%8F%E4%BA%8B%E9%A1%B9.png)


# 测试方法