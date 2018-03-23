# **C# 中文转拼音**
```C#
tChanged(object sender, EventArgs e)//自动产生助记码         {              Sunshown.全局.SpellWBCode swcode = new Sunshown.全局.SpellWBCode();             string strcode = swcode.hztowbpy(this.tb_cn.Text.Trim(), Application.StartupPath + "\\Hztopywb.hz");             //true时获取拼音助记码              this.tb_zj.Text = swcode.getstr(strcode, true, "☆");             //false时得到五笔助记码              ////this.txtWB.Text = swCode.getstr(strCode, false, "☆");              Sunshown1.全局.GetPingyin getpy = new Sunshown1.全局.GetPingyin();
```