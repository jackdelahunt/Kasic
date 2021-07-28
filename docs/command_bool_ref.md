---
title: BoolRef
slug: /commands/bool_ref
parent: Commands
---

# BoolRef
The boolRef command is used to create new or change the value of variables with a given name with the passed bool. To retrive the data in the variable the derefrence operator(`*`) is used. BoolRef also returns the value passed to it.
```
&bool [string, bool]
```
---
## Overview
```yaml
name: '&bool'
min: 2
max: 2
arguments:
    - string
    - bool
return: bool
```
---
## Example 
```
>>> &bool my_bool true
true
>>> print *my_bool
100
>>> &num my_bool false
false
>>> print *my_bool
false
```