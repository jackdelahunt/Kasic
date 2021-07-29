---
title: Num
slug: /commands/num
parent: Commands
---

# Num
The num command is used to cast and value to a number
```
num [any]
```
---
## Overview
```yaml
name: 'num'
min: 1
max: 1
arguments:
    - any
return: num
```
---
## Example 
```
>>> num "20"
20
>>> num true
1
```