# 代码修改规范 Skill

## 核心规则

### 1. 空值断言规则
- 所有判断空并抛出异常的场景，必须使用 `DuZhi.Common.Util.Utils` 中的断言判断使用this.IsValid()这样的方式
- 不要使用传统的 `if (obj == null) throw new ArgumentNullException()` 写法

### 2. 修改范围限制
- 只修改明确指定的目标方法、属性或构造函数
- 绝对不要删除、修改或重新排列以下内容：
  - 文件顶部的 `using` 语句
  - 类中的其他方法、属性、字段、事件
  - 类特性（Attribute）
  - `#region`/`#endregion` 代码折叠区域
  - XML 文档注释（`///`）
  - `namespace` 命名空间声明

### 3. 新增代码位置
- 添加新方法时，放在类的末尾，或放在标记为 "Methods" 的 `#region` 内

### 4. 修改方法时的约束
- 除非明确要求，否则保持方法签名不变（访问修饰符、返回类型、名称、参数）
- 只替换或插入方法体（花括号内）的代码
- 保持该方法花括号外的所有代码不变

### 5. using 语句管理
- 可以添加新的 `using` 语句
- 绝对不要删除或注释掉已有的 `using` 语句
- 新增的 `using` 按字母顺序放入现有分组中

### 6. 文件编码
- 新增的文件统一采用 UTF-8 格式保存

### 7. 需要确认的操作
- 如果需要删除代码，先询问确认后再操作

### 8. 其他
- 需要逐行分析转换后的代码与原始代码的差异，确保转换后的代码逻辑正确，功能正常
- IList采用ForEach(e => e.xxx = xxx).Save()的方式,List采用ForEachSave(e => e.xxx = xxx)的方式,禁止使用Update()的方式更新数据
- 所有转换还原的时候需要将所有子方法一起完整1:1还原,子方法名与Java文件中原始名称相同，保持一致
- List foreach方式赋值新的列表保存数据时,使用list.Select(item=>{var x=new Entity(){...}; return x;}).Save()的方式保存
- 所有转换的时候排除Mom.Data库中的代码,不对Mom.Data库中的代码进行任何读取与修改
- 不对表对应实体文件.cs与.Biz.cs文件中进行任何修改
- _.XXX.In(Array)的方式一定要判断Array是否为null或者Empty,如果是null或者Empty则不进行任何操作
- Transactional标记的方法采用事务进行处理,事务采用using(var tran=Entity.Meta.CreateTrans()){... tran.Commit(); }的方式进行处理,确保事务的正确使用和管理
### 9. 转换完后挨个子方法进行对比验证，确保转换后的代码逻辑正确，功能正常。
- 转换完后挨个子方法进行对比验证，确保转换后的代码逻辑正确，功能正常,输出转换对比结果，输出转换对比结果包括每个子方法的名称、原始代码、转换后的代码、对比结果（是否一致）等信息，确保转换的准确性和完整性。

## 应用场景

当用户请求以下操作时自动应用此规则：
- 修改现有代码
- 添加空值检查
- 重构方法
- 添加新方法

## 示例

**错误示例**（不允许）：
```csharp
if (param == null) 
    throw new ArgumentNullException(nameof(param));