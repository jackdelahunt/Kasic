---
title: Less
slug: /commands/Less
parent: Commands
---

# Less
The less command is used to determine if a given number is less then another
```
less [number, number]
```
---
## Overview
```yaml
name: 'less'
min: 2
max: 2
arguments:
    - number
    - number
return: bool
```
---
## Example 
```
>>> less 100 99
True
>>> less 10 120
False
```