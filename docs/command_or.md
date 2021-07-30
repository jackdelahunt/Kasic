---
title: Or
slug: /commands/or
parent: Commands
---

# Or
The or command returns true if one of the inputs are true
```
or [bool, bool]
```
---
## Overview
```yaml
name: 'or'
min: 2
max: 2
arguments:
    - bool
    - bool
return: bool
```
---
## Example 
```
>>> or true false
True
>>> or false false
False
```