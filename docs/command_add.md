---
title: Add
slug: /commands/add
parent: Commands
---

# Add
The add command returns the sum of all the numbres passed. 
```
add [number, number, number, number]
```
---
## Overview
```yaml
name: 'add'
min: 2
max: 4
arguments:
    - number
    - number
    - number
    - number
return: number
```
---
## Example 
```
>>> add 10 20 30
60
```