# unity-skill-system-framework
A Unity-based data-driven skill system and combat framework.  Includes state machine design, event system, object pooling optimization,  and extensible skill effect modules for ARPG games.
# Unity 数据驱动技能系统与战斗框架

## 项目简介

本项目基于 Unity3D 开发，主要实现了一套可扩展的数据驱动技能系统与战斗框架。  
针对传统技能系统中技能逻辑与代码强耦合、扩展困难、维护成本高的问题，项目采用 **数据驱动 + 状态机 + 事件系统 + 对象池优化** 的设计思路，对技能配置、技能逻辑与战斗效果进行了模块化拆分。

该项目主要用于练习 Unity 游戏开发中的系统设计能力、代码解耦能力与性能优化能力，适用于 ARPG 类游戏的技能系统实现。

<img width="1920" height="997" alt="image" src="https://github.com/user-attachments/assets/5f0668d9-a3fb-4240-b1dc-1161fedafc12" />


---

## 技术栈

- Unity3D
- C#
- ScriptableObject / JSON
- 状态机
- 事件系统
- 对象池

---

## 项目背景

在传统 Unity 技能系统开发中，常见问题包括：

- 技能逻辑直接写死在代码中，扩展新技能时需要频繁修改核心逻辑
- 技能效果之间复用性较差，系统耦合度高
- 技能特效频繁创建与销毁，容易引发 GC，影响运行性能
- 战斗系统与角色系统之间依赖较强，后续维护困难

为解决以上问题，本项目设计并实现了一套数据驱动技能系统框架，用于提升技能系统的扩展性、可维护性与运行效率。

---

## 核心功能

### 1. 数据驱动技能配置

使用 ScriptableObject / JSON 对技能数据进行配置管理，包括：

- 技能 ID
- 技能名称
- 技能伤害
- 技能冷却时间
- 技能效果类型
- 动画触发时机

通过数据配置驱动技能行为，实现新增技能时无需频繁修改核心代码。

---

### 2. 技能状态机

设计技能状态机，对技能生命周期进行统一管理。

技能执行流程如下：

`Idle -> Cast -> Effect -> Cooldown`

支持以下能力：

- 技能释放
- 技能打断
- 技能冷却控制
- 动画与技能时机同步

---

### 3. 可扩展技能效果模块

对技能效果进行抽象，定义统一接口：

`ISkillEffect`

在此基础上实现多种技能效果模块，例如：

- DamageEffect（伤害效果）
- BuffEffect（增益效果）
- KnockBackEffect（击退效果）
- HealEffect（治疗效果）

通过接口与模块化设计，支持后续快速扩展新的技能效果类型。

---

### 4. 事件驱动战斗系统

引入事件系统，降低技能系统、角色系统与战斗系统之间的耦合度。

典型流程如下：

`角色释放技能 -> 技能系统触发事件 -> 战斗系统计算伤害 -> 角色系统更新状态`

该设计能够提升系统可维护性，并便于后续增加更多业务逻辑。

---

### 5. 对象池优化

针对技能特效对象频繁 `Instantiate / Destroy` 带来的性能问题，使用对象池对技能特效进行统一管理与复用。

优化效果：

- 减少频繁创建和销毁对象带来的性能开销
- 降低 GC 调用频率
- 提升运行稳定性

---

## 项目结构

```text
Assets
├── Scripts
│   ├── SkillSystem       // 技能系统
│   ├── CombatSystem      // 战斗系统
│   ├── StateMachine      // 状态机
│   ├── EventSystem       // 事件系统
│   └── Common            // 通用工具类
├── Prefabs               // 预制体资源
├── Config                // 技能配置
├── Effects               // 技能特效
└── Scenes                // 场景
